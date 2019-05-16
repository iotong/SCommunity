using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZFine.Application.SystemManage;
using ZFine.Code;
using ZFine.Domain.Entity.SystemManage;

namespace ZFine.Web.Areas.SystemManage.Controllers
{
    public class EquipmentController : ControllerBase
    {

        private EquipmentApp areaApp = new EquipmentApp();
        //
        // GET: /SystemManage/Equipment/

        /// <summary>
        ///添加设备
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 查询设备
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {

            var rows = areaApp.GetList();


            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            List<EquipmentEntity> data = rows.Where(x => x.CompanyCode == LoginInfo.CompanyId).ToList();
            return Content(data.ToJson());
        }

        /// <summary>
        /// 添加修改设备视图
        /// </summary>
        /// <returns></returns>
        public ActionResult addEquipment()
        {
            return View();
        }

        /// <summary>
        /// 添加修改设备
        /// </summary>
        /// <param name="areaEntity"></param>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult addEquipment(EquipmentEntity areaEntity, string KeyValue,string EquipPosition)
        {
           
            var LoginInfo = OperatorProvider.Provider.GetCurrent();

            if (KeyValue == "undefined")
                KeyValue = null;
            #region 赋值
            areaEntity.F_Id = Common.GuId();
            areaEntity.F_DeleteMark = true;
            areaEntity.F_CreatorTime = Convert.ToDateTime(DateTime.Now.ToString());
            areaEntity.F_CreatorUserId = LoginInfo.UserId.ToString();
            areaEntity.F_LastModifyTime = Convert.ToDateTime(DateTime.Now.ToString());
            areaEntity.F_LastModifyUserId = LoginInfo.UserId.ToString();
            areaEntity.F_DeleteTime = Convert.ToDateTime(DateTime.Now.ToString());
            areaEntity.F_DeleteUserId = LoginInfo.UserId.ToString();
            areaEntity.EquipName = Request.Form["EquipName"];
            areaEntity.EquipType = Request.Form["EquipType"];
            areaEntity.EquipPosition = EquipPosition;
            areaEntity.EquipLongitude = Convert.ToDecimal( Request.Form["EquipLongitude"]);
            areaEntity.EquipLatitude =Convert.ToDecimal(Request.Form["EquipLatitude"]);
            areaEntity.LoginName = LoginInfo.UserName;
            areaEntity.LoginPass = "";
            areaEntity.EquipAddr = Request.Form["EquipAddr"];
            areaEntity.EquipPort = Convert.ToInt32(Request.Form["EquipPort"]);
            areaEntity.EquipSn = Request.Form["EquipSn"];
            areaEntity.Suppliers = Request.Form["Suppliers"];
            areaEntity.EquipModel = Request.Form["EquipModel"];
            areaEntity.EquipCode = Request.Form["EquipCode"];
            areaEntity.FaceAlgorithmVer = Request.Form["FaceAlgorithmVer"];
            areaEntity.SdkVer = Request.Form["SdkVer"];
            areaEntity.Appid = Request.Form["Appid"];
            areaEntity.SdkKey = Request.Form["SdkKey"];
            areaEntity.SoftVer = Request.Form["SoftVer"];
            areaEntity.EquipMask = Request.Form["EquipMask"];
            areaEntity.ServerIp = Request.Form["ServerIp"];
            areaEntity.ServerPort = Request.Form["ServerPort"];
            areaEntity.ServerId = Request.Form["ServerId"];
            areaEntity.ServerMask = Request.Form["ServerMask"];
            areaEntity.AutoUpdate = Convert.ToBoolean(Request.Form["AutoUpdate"]);
            areaEntity.ShowTxt = Request.Form["ShowTxt"];
            areaEntity.AutoRemind = Convert.ToInt32(Request.Form["AutoRemind"]);
            areaEntity.AutoDelete = Convert.ToBoolean(Request.Form["AutoDelete"]);
            areaEntity.UpLoadDelete = Convert.ToBoolean(Request.Form["UpLoadDelete"]);
            areaEntity.RemindTxt = "1";
            areaEntity.FloorCode = Request.Form["FloorCode"];
            areaEntity.CommunityCode = Request.Form["CommunityCode"];
            areaEntity.CompanyCode = LoginInfo.CompanyId;
            areaEntity.Operator = LoginInfo.UserName;
            areaEntity.OperTime = DateTime.Now;
            areaEntity.ImagePath = "1";
            areaEntity.IsDel = true;
            areaEntity.IsUpload = true;
            areaEntity.UnitCode =Request.Form["UnitCode"];
            #endregion

            areaApp.SubmitForm(areaEntity, KeyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 查询一台设备
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
            areaApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
