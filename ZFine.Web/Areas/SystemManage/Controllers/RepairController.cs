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
    /// 报修记录
    /// </summary>
    public class RepairController : ControllerBase
    {
        private RepairApp RepairApp = new RepairApp();//业主信息

        private EquipmentApp areaApp = new EquipmentApp();//设备
        //小区
        private CommunityInfoApp communityInfoapp = new CommunityInfoApp();
        //单元
        private UnitApp unitApp = new UnitApp();
        //楼栋
        private FloorApp floorApp = new FloorApp();

       
        /// <summary>
        /// 查询设报修记录
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            ///查询业主集合

            var data = RepairApp.GetList();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            List<RepairEntity> data1 = data.Where(z => z.CompanyCode == LoginInfo.CompanyId).ToList();


            List<Repair> list = new List<Repair>();
            foreach (var item in data1)
            {
                Repair listmodel = new Repair();

                listmodel.F_Id = item.F_Id;
                listmodel.R_People = item.R_People;
                listmodel.R_Image = item.R_Image;
                listmodel.R_Time = item.R_Time;
                listmodel.R_PickPeople = item.R_PickPeople;
                listmodel.R_PickTime = item.R_PickTime;
                listmodel.R_MainPeople = item.R_MainPeople;
                listmodel.R_MainTime = item.R_MainTime;
                listmodel.R_ManResults = item.R_ManResults;
                listmodel.R_PeopleMY = item.R_PeopleMY;

                //查询小区名称
                var CommunityInfo = communityInfoapp.GetList();
                listmodel.CommunityCode = CommunityInfo.Where(x => x.F_Id == item.CommunityCode).Select(x => x.C_Name).First();

                //查询单元名称
                var unitInfo = unitApp.GetList();
                listmodel.UnitCode = unitInfo.Where(x => x.F_Id == item.UnitCode).Select(x => x.U_Name).First();

                //查询楼栋名称
                var floorInfo = floorApp.GetList();
                listmodel.FloorCode = floorInfo.Where(x => x.F_Id == item.FloorCode).Select(x => x.F_Name).First();
                list.Add(listmodel);


            }
            return Content(list.ToJson());
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        private class Repair
        {
            public string F_Id { get; set; }
            public string UnitCode { get; set; }
            public string FloorCode { get; set; }
            public string CommunityCode { get; set; }
            public string CompanyCode { get; set; }
            public string R_People { get; set; }
            public string R_Image { get; set; }
            public string R_Content { get; set; }
            public DateTime? R_Time { get; set; }
            public string R_PickPeople { get; set; }
            public DateTime? R_PickTime { get; set; }
            public string R_MainPeople { get; set; }
            public DateTime? R_MainTime { get; set; }
            public bool R_ManResults { get; set; }
            public string R_PeopleMY { get; set; }


        }





        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {

            var data = RepairApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(RepairEntity userEntity, string keyValue, string ImgUrl)
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            userEntity.OperTime = DateTime.Now;
            userEntity.Operator = LoginInfo.UserName.ToString();
            userEntity.CompanyCode = LoginInfo.CompanyId;

            RepairApp.SubmitForm(userEntity, keyValue);
            return Success("操作成功。");

        }

       /// <summary>
       /// 删除设备报修记录
       /// </summary>
       /// <param name="keyValue"></param>
       /// <returns></returns>
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {


            RepairApp.DeleteForm(keyValue);


            return Success("删除成功。");
        }

        
        /// <summary>
        /// 查看设备报修记录
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {

            return View();
        }

        /// <summary>
        /// 修改设备报修记录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateRepair()
        {
            return View();
        }


    }
}
