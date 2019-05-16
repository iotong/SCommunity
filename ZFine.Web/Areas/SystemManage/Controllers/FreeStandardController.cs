using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ZFine.Application.SystemManage;
using ZFine.Code;
using ZFine.Domain._03_Entity.SystemManage;
using ZFine.Domain.Entity.SystemManage;
using ZFine.Web.App_Start;

namespace ZFine.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 计费信息
    /// </summary>
    public class FreeStandardController : ControllerBase
    {
        //计费公告
        private FreeStandardApp freeStandardApp = new FreeStandardApp();
        //小区
        private CommunityInfoApp communityInfoapp = new CommunityInfoApp();
        //单元
        private UnitApp unitApp = new UnitApp();
        //楼栋
        private FloorApp floorApp = new FloorApp();


        /// <summary>
        /// 添加计费标准网页
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult AddFreeStandard()
        {

            return View();

        }


        /// <summary>
        /// 查询单元信息绑定下拉列表
        /// </summary>
        /// <returns></returns>
        public string Unitselect()
        {
            var data = new
            {
                rows = unitApp.GetList()
            };
            var LoginInfo = OperatorProvider.Provider.GetCurrent();

            List<UnitEntity> typeList = data.rows;
            StringBuilder sb = new StringBuilder();
            typeList.Where(x => x.CompanyCode == LoginInfo.CompanyId).ToList();
            foreach (var item in typeList)
            {
                sb.Append(string.Format(@"<option selected='selected' value='{0}'>{1}</option>", item.F_Id, item.U_Name));
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
        public ActionResult Index(string Name, string keyValue, string title, string type_Id, string type, string EquipNo)
        {
            NoticeEntity areaEntity = new NoticeEntity();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            List<string> alias = new List<string>();
            alias.Add("11111");

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
                areaEntity.UnitCode = "2asd";
                areaEntity.FloorCode = "as1";
                areaEntity.CommunityCode = "1fa";
                areaEntity.CompanyCode = "asd";
                areaEntity.N_Type =Convert.ToInt32(type);
                areaEntity.N_Type_Id = type_Id;
                areaEntity.N_Title = title;
                areaEntity.N_Content = Name;
                areaEntity.N_Way = "";
               
                areaEntity.N_ReleaseTime = "";
                areaEntity.EquipNo = item;
                areaEntity.N_ContgentAddr = "dsfsdf";
                areaEntity.IsDel = false;
                areaEntity.IsUpload = true;
                areaEntity.Operator = LoginInfo.UserName.ToString();
                areaEntity.OperTime = null;
                #endregion
                //alias.Add(item);
                //   freeStandardApp.SubmitForm(areaEntity, keyValue);
            }

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
        /// 查询计费标准网页
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult selectFreeStamdard()
        {

            return View();

        }

        /// <summary>
        /// 查询计费标准
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            //查询计费标准
            var rows = freeStandardApp.GetList();


            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            List<FreeStandardEntity> data = rows.Where(z => z.CompanyCode == LoginInfo.CompanyId).ToList();
            List<FreeStandard> list = new List<FreeStandard>();

            foreach (var item in data)
            {
                FreeStandard listmodel = new FreeStandard();
                listmodel.F_Name = item.F_Name;
                listmodel.F_Standard = item.F_Standard;
                listmodel.F_Type = item.F_Type;
                listmodel.F_Cycle = item.F_Cycle;
                listmodel.F_Money = item.F_Money;
                listmodel.F_Memo = item.F_Memo;
                listmodel.F_Id = item.F_Id;


                //查询小区名称
                var CommunityInfo = communityInfoapp.GetList();
                listmodel.CommunityName = CommunityInfo.Where(x => x.F_Id == item.CommunityCode).Select(x => x.C_Name).First();

                //查询单元名称
                var unitInfo = unitApp.GetList();
                listmodel.unitName = unitInfo.Where(x => x.F_Id == item.UnitCode).Select(x => x.U_Name).First();

                //查询楼栋名称
                var floorInfo = floorApp.GetList();
                listmodel.floorName = floorInfo.Where(x => x.F_Id == item.FloorCode).Select(x => x.F_Name).First();
                list.Add(listmodel);


            }
            return Content(list.ToJson());
        }
        /// <summary>
        /// 查询集合
        /// </summary>
        private class FreeStandard
        {
            public string F_Id { get; set; }
            public string F_Name { get; set; }
            public string F_Standard { get; set; }
            public string F_Type { get; set; }
            public string F_Cycle { get; set; }
            public decimal F_Money { get; set; }
            public string F_Memo { get; set; }
            public string CommunityName { get; set; }
            public string unitName { get; set; }
            public string floorName { get; set; }
        }
        /// <summary>
        /// 查询计费标准
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = freeStandardApp.GetForm(keyValue);
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
            var data = freeStandardApp.GetForm(keyValue);
            // ViewBag.Message = data.N_Title;
            //  ViewBag.Message1 = data.N_Content;
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
            var data = freeStandardApp.GetForm(keyValue);

            //ExecutePushExample(keyValue, "", 3, "", "");
            return Success("删除成功。");
        }


        public ActionResult test()
        {
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="keyValue"></param>
        /// <param name="ImgUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(FreeStandardEntity userEntity, string keyValue, string ImgUrl)
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();

            userEntity.CompanyCode = LoginInfo.CompanyId;
            userEntity.F_Id = Common.GuId();
            userEntity.OperTime = DateTime.Now;
            userEntity.Operator = LoginInfo.UserName;
            freeStandardApp.SubmitForm(userEntity, keyValue);


            return Success("操作成功。");

        }

    }
}
