using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using ZFine.Application.SystemManage;
using ZFine.Code;
using ZFine.Domain._03_Entity.SystemManage;
using ZFine.Domain.Entity.SystemManage;


using Beyova.JPush.V3;
using Beyova.JPush;
using System.Web;
using ZFine.Web.App_Start;
using System.Linq;

namespace ZFine.Web.Areas.SystemManage.Controllers
{
    public class AnnouncementController : ControllerBase
    {

        private NoticeApp areaApp = new NoticeApp();
        private NoticeTypeApp noticeTypeApp = new NoticeTypeApp();
        private EquipmentApp EquipmentApp = new EquipmentApp();


        //
        // GET: /SystemManage/Announcement/

        public ActionResult Index()
        {

            return View();
        }
        /// <summary>
        /// 多选设备
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            //var rows = EquipmentApp.GetList();
            //StringBuilder sb = new StringBuilder();
            //// sb.Append(string.Format(@"<div><span>请选择需要发布的设备号:</span></div></br>"));
            //sb.Append(string.Format(@"<form class='layui-form'><div class='layui-form-item'><label class='layui-form-label'>请选择需要发布的设备号:</label></br>"));
            //foreach (var item in rows)
            //{
            //    //sb.Append(string.Format(@"<div><label><input name='Fruit' type='checkbox' value='{0}'/>{1}</label></div>", item.F_Id, item.EquipName));
            //    sb.Append(string.Format(@"<div class='layui-input-block'><input type='checkbox' name='like' value='{0}' title='{1}'>{2}</div> ", item.F_Id, item.EquipName, item.EquipName));
            //}

            //sb.Append(string.Format(@"</br></br></br></br></br><div style='text-align:center'> <button id='select' onclick='select()' class='layui-btn'>确定</button></div>"));
            //sb.Append(string.Format(@"</div></form>"));
            //ViewBag.AA = sb;
            return View();
        }

        /// <summary>
        /// 多选设备
        /// </summary>
        /// <returns></returns>
        public string List()
        {
            var rows = EquipmentApp.GetList();
         
            var LoginInfo = OperatorProvider.Provider.GetCurrent();


            List<EquipmentEntity> equipent = rows.Where(z => z.CompanyCode == LoginInfo.CompanyId).ToList(); ;
            StringBuilder sb = new StringBuilder();

            foreach (var item in equipent)
            {
                sb.Append(string.Format(@"<br/>&nbsp;&nbsp;&nbsp<input type='checkbox' name='like' value='{0}' title='{1}'>{2}<br/> ", item.F_Id, item.EquipName, item.EquipName));
            }

            return JsonConvert.SerializeObject(sb);
            ;
        }

        /// <summary>
        /// HTML网页
        /// </summary>
        /// <returns></returns>
        public ActionResult HTML()
        {

            return View();
        }

        /// <summary>
        /// 加载设备下拉列表框
        /// </summary>
        /// <returns></returns>
        public string Equipmentselect()
        {

            var date = EquipmentApp.GetList();

            var LoginInfo = OperatorProvider.Provider.GetCurrent();
           

            List<EquipmentEntity> equipent = date.Where(z => z.CompanyCode == LoginInfo.CompanyId).ToList(); ;
            StringBuilder sb = new StringBuilder();

            foreach (var item in equipent)
            {
                sb.Append(string.Format(@"<option selected='selected' value='{0}'>{1}</option>", item.EquipSn, item.EquipName));
            }
            ViewBag.SSS = sb;
            return JsonConvert.SerializeObject(sb);
        }

        /// <summary>
        /// 查询通知类型绑定下拉列表
        /// </summary>
        /// <returns></returns>
        public string option()
        {
            var data = new
            {
                rows = noticeTypeApp.GetList()
            };

            List<NoticeTypeEntity> typeList = data.rows;
            StringBuilder sb = new StringBuilder();

            foreach (var item in typeList)
            {
                sb.Append(string.Format(@"<option selected='selected' value='{0}'>{1}</option>", item.F_Id, item.TypeName));
            }
            ViewBag.SSS = sb;

            return JsonConvert.SerializeObject(sb);
        }


        /// <summary>
        /// 添加公告
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateInput(false)]
        public ActionResult Index(string Name, string keyValue, string title,
            string type_Id, string type, string EquipNo,
            string N_ReleaseTime, string contentType )
        {
            NoticeEntity areaEntity = new NoticeEntity();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            List<string> alias = new List<string>();
            

            var EquipNos = EquipNo.Split(',');
            foreach (var item in EquipNos)
            {
                #region 设置数据
                areaEntity.F_Id = Common.GuId();
                areaEntity.F_DeleteMark = true;
                areaEntity.F_ParentId = "1";
                areaEntity.F_CreatorTime = Convert.ToDateTime(DateTime.Now.ToString());
                areaEntity.F_CreatorUserId = LoginInfo.UserId.ToString();
                areaEntity.F_LastModifyTime = Convert.ToDateTime(DateTime.Now.ToString());
                areaEntity.F_LastModifyUserId = LoginInfo.UserId.ToString();
                areaEntity.F_DeleteTime = Convert.ToDateTime(DateTime.Now.ToString());
                areaEntity.F_DeleteUserId = LoginInfo.UserId.ToString();
                areaEntity.CompanyCode = LoginInfo.CompanyId;
                areaEntity.N_Type = Convert.ToInt32(type);
                areaEntity.N_Type_Id = type_Id;
                areaEntity.N_Title = title;
                areaEntity.N_Content = Name;
                areaEntity.N_Way = "";
                areaEntity.N_StopTime = Convert.ToInt32(N_ReleaseTime);
                areaEntity.N_ReleaseTime = "";
                areaEntity.EquipNo = item;
                areaEntity.N_ContgentAddr = "dsfsdf";
                areaEntity.IsDel = false;
                areaEntity.IsUpload = true;
                areaEntity.Operator = LoginInfo.UserName.ToString();
                areaEntity.OperTime = null;
                areaEntity.contentType = Convert.ToInt32(contentType);
                #endregion
                alias.Add(item);
                areaApp.SubmitForm(areaEntity, keyValue);
            }

            Dictionary<string, object> counValue = new Dictionary<string, object>();
            counValue.Add("key", "key");

            #region   推送设备
            string N_type = "1";
            if (string.IsNullOrEmpty(keyValue))
                N_type = "2";
            JPushHelper JPushHelper = new JPushHelper();
            var retual = JPushHelper.Send(alias, N_type, areaEntity.N_Content, "公告发布", null);
            #endregion
            return View();

        }

        /// <summary>
        /// 查看公告
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult select()
        {

            return View();

        }

        /// <summary>
        /// 查询公告
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            //查询公告
            var rows = areaApp.GetList();

            //查询设备
            var EquipmentList = EquipmentApp.GetList();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            List<EquipmentEntity> data1 = EquipmentList.Where(z => z.CompanyCode == LoginInfo.CompanyId).ToList();
            var data2 = data1.Select(x => x.EquipSn).ToList();
            var date = rows.Where(x => data2.Contains(x.EquipNo)).ToList();
            return Content(date.ToJson());
        }

        /// <summary>
        /// 查询一条公告
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = areaApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 查询Html需要的数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public dynamic GetFormJson1(string keyValue)
        {
            var data = areaApp.GetForm(keyValue);
            ViewBag.Message = data.N_Title;
            ViewBag.Message1 = data.N_Content;
            return ViewBag;
        }
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            var data = areaApp.GetForm(keyValue);
            List<string> arr = new List<string>();
            arr.Add(data.EquipNo);
            areaApp.DeleteForm(keyValue);

            JPushHelper JPushHelper = new JPushHelper();
            var retual = JPushHelper.Send(arr, "0", "公告删除", "删除公告", null);
            //ExecutePushExample(keyValue, "", 3, "", "");
            return Success("删除成功。");
        }


        public ActionResult test()
        {
            return View();
        }


    }
}
