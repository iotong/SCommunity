using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ZFine.Application.SystemManage;
using ZFine.Code;
using ZFine.Domain.Entity.SystemManage;

namespace ZFine.Web.Areas.SystemManage.Controllers
{
    public class FloorController : ControllerBase
    {
        private FloorApp floorApp = new FloorApp();//楼栋信息

        private CommunityInfoApp communityInfoApp = new CommunityInfoApp();//小区信息

        /// <summary>
        /// 楼栋信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult FloorIndex()
        {

            return View();
        }




        ///// <summary>
        ///// 查询通知类型绑定下拉列表
        ///// </summary>
        ///// <returns></returns>
        //public string option()
        //{
        //    var data = new
        //    {
        //        rows = noticeTypeApp.GetList()
        //    };

        //    List<NoticeTypeEntity> typeList = data.rows;
        //    StringBuilder sb = new StringBuilder();

        //    foreach (var item in typeList)
        //    {
        //        sb.Append(string.Format(@"<option selected='selected' value='{0}'>{1}</option>", item.F_Id, item.TypeName));
        //    }
        //    ViewBag.SSS = sb;

        //    return JsonConvert.SerializeObject(sb);
        //}




        /// <summary>
        /// 添加楼栋信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddFloor()
        {

            return View();
        }


        /// <summary>
        /// 查询小区信息绑定下拉列表
        /// </summary>
        /// <returns></returns>
        public string option()
        {
          var   rows = communityInfoApp.GetList();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();


            var FlooList = rows.Where(x => x.ComanyCode == LoginInfo.CompanyId).ToList();
            StringBuilder sb = new StringBuilder();

            foreach (var item in FlooList)
            {
                sb.Append(string.Format(@"<option selected='selected' value='{0}'>{1}</option>", item.F_Id, item.C_Name));
            }
            ViewBag.SSS = sb;

            return JsonConvert.SerializeObject(sb);
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
        public ActionResult SubmitForm(FloorEntity userEntity, string keyValue, string ImgUrl)
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();

            userEntity.CompanyCode = LoginInfo.CompanyId;
            userEntity.F_Id = Common.GuId();
            userEntity.OperTime = DateTime.Now;
            userEntity.Operator = LoginInfo.UserName;
            floorApp.SubmitForm(userEntity, keyValue);

            return Success("操作成功。");

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {



            floorApp.DeleteForm(keyValue);
          
            return Success("删除成功。");
        }
        /// <summary>
        /// 查询一条
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {

            var data = floorApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            ///查询业主集合

            var data = floorApp.GetList();

            //查询此公司设备集合
            var rows = floorApp.GetList();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            //List<EquipmentEntity> data1 = rows.Where(z => z.CompanyCode == LoginInfo.CompanyId).ToList();
            //var data2 = data1.Select(x => x.EquipSn).ToList();
            //var date = data.Where(x => data2.Contains(x.EquipNO)).ToList();
            return Content(data.ToJson());
        }

    }
}
