/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZFine.Application.SystemManage;
using ZFine.Code;
using ZFine.Domain.Entity.SystemManage;
using ZFine.Web.App_Start;

namespace ZFine.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 家庭成员信息
    /// </summary>
    public class OwnersItemsController : ControllerBase
    {
        private OwnersItemsApp1 userApp = new OwnersItemsApp1();//业主

        private EquipmentApp areaApp = new EquipmentApp();//设备
        /// <summary>
        /// 业主主页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {

            return View();
        }

        /// <summary>
        /// 查询业主信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            ///查询业主集合

            var data = userApp.GetList(keyword);

            //查询此公司设备集合
            var rows = areaApp.GetList();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            List<EquipmentEntity> data1 = rows.Where(z => z.CompanyCode == LoginInfo.CompanyId).ToList();
            var data2 = data1.Select(x => x.EquipSn).ToList();
            var date = data.Where(x => data2.Contains(x.EquipNO)).ToList();
            return Content(date.ToJson());
        }


        /// <summary>
        /// 添加业主信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOwnersItems()
        {
            return View();
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {

            var data = userApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 添加家庭成员
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="keyValue"></param>
        /// <param name="ImgUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(OwnersItemsEntity userEntity, string keyValue, string ImgUrl)
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            userEntity.OperTime = DateTime.Now;
           
               

            userEntity.Operator = LoginInfo.UserName.ToString();
            userEntity.OI_Enable = true;
            userEntity.OI_FaceImgP = "http://dev.iotong.cn/IMG/" + userEntity.OI_FaceImgP;
            userApp.SubmitForm(userEntity, keyValue);

            List<string> alias = new List<string>();
            JPushHelper JPushHelper = new JPushHelper();
            //修改时type=2
            var type = "2";
            alias.Add(userEntity.EquipNO);
            if (string.IsNullOrEmpty(keyValue))
                type = "1";

            var a = JPushHelper.Send(alias, type, null, "添加业主", null);

            if (a > 0)
                return Success("操作成功。");
            else
                return Success("操作失败。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {

            var data = userApp.GetForm(keyValue);
            List<string> arr = new List<string>();
            arr.Add(data.EquipNO);

            userApp.DeleteForm(keyValue);
            ;
            JPushHelper JPushHelper = new JPushHelper();
            var a = JPushHelper.Send(arr, "0", "删除", "删除业主", null);
            return Success("删除成功。");
        }

        [HttpGet]
        public ActionResult RevisePassword()
        {
            return View();
        }
        /// <summary>
        /// 查看业主信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Selectowner()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Rrandom(string keyValue) {

            var date = Common.GuId();
            return Content(date);
           
        }
    }
}
