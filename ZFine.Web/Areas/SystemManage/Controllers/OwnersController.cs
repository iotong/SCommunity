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

namespace ZFine.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 业主信息
    /// </summary>
    public class OwnersController : ControllerBase
    {
        private FreeStandardApp freeStandardApp = new FreeStandardApp();//计费

        private OwnersApp OwnersApp = new OwnersApp();//业主信息

        private EquipmentApp areaApp = new EquipmentApp();//设备

        private OwnerAndBillingApp ownerapp = new OwnerAndBillingApp();//业主和计费关联表
        //小区
        private CommunityInfoApp communityInfoapp = new CommunityInfoApp();
        //单元
        private UnitApp unitApp = new UnitApp();
        //楼栋
        private FloorApp floorApp = new FloorApp();
        //充值记录
        private RechargeApp rechargeApp = new RechargeApp();

        /// <summary>
        /// 业主主页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOwnersList()
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
            var data = OwnersApp.GetList(keyword);
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            List<OwnersEntity> data1 = data.Where(z => z.CompanyCode == LoginInfo.CompanyId).ToList();
            List<Owners> list = new List<Owners>();
            foreach (var item in data1)
            {
                Owners listmodel = new Owners();

                listmodel.F_Id = item.F_Id;
                listmodel.O_No = item.O_No;
                listmodel.O_IdNumber = item.O_IdNumber;
                listmodel.O_PropertyNo = item.O_PropertyNo;
                listmodel.O_Name = item.O_Name;
                listmodel.O_HouseNo = item.O_HouseNo;
                listmodel.O_Properties = item.O_Properties;
                listmodel.O_Type = item.O_Type;
                listmodel.O_BuildingArea = item.O_BuildingArea;
                listmodel.O_UseArea = item.O_UseArea;
                listmodel.O_FreeStatand = item.O_FreeStatand;
                listmodel.O_Peoples = item.O_Peoples;
                listmodel.O_Tel = item.O_Tel;
                listmodel.O_Phone = item.O_Phone;
                listmodel.O_StayTime = item.O_StayTime;
                listmodel.Dalance = item.Dalance;
                listmodel.Utility_Bill = item.Utility_Bill;
                listmodel.Property_fee = item.Property_fee;
                listmodel.Parking_fee = item.Parking_fee;
                listmodel.Other_fee = item.Other_fee;

                // 
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
        private class Owners
        {
            public string F_Id { get; set; }
            public string UnitCode { get; set; }
            public string FloorCode { get; set; }
            public string CommunityCode { get; set; }
            public string CompanyCode { get; set; }
            public string O_No { get; set; }
            public string O_IdNumber { get; set; }
            public string O_PropertyNo { get; set; }
            public string O_Name { get; set; }
            public string O_HouseNo { get; set; }
            public string O_Properties { get; set; }
            public string O_Type { get; set; }
            public decimal O_BuildingArea { get; set; }
            public decimal O_UseArea { get; set; }
            public decimal O_FreeStatand { get; set; }
            public decimal Dalance { get; set; }
            public string O_Peoples { get; set; }
            public string O_Tel { get; set; }
            public string O_Phone { get; set; }
            public DateTime? O_StayTime { get; set; }
            /// <summary>
            /// 水电费
            /// </summary>
            public decimal Utility_Bill { get; set; }
            /// <summary>
            /// 物业费
            /// </summary>
            public decimal Property_fee { get; set; }
            /// <summary>
            /// 停车费
            /// </summary>
            public decimal Parking_fee { get; set; }
            /// <summary>
            /// 其他费用
            /// </summary>
            public decimal Other_fee { get; set; }
        }


        /// <summary>
        /// 添加业主信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOwners()
        {
            return View();
        }


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {

            var data = OwnersApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 添加业主
        /// </summary>
        /// <param name="userEntity">业主信息</param>
        /// <param name="keyValue">F_Id</param>
        /// <param name="ImgUrl">图片路径</param>
        /// <param name="ChargesID">计费Id</param>
        /// <param name="ChargesTiem">到期时间</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(OwnersEntity userEntity, string keyValue, string ImgUrl, string ChargesID, DateTime? ChargesTiem)
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            userEntity.OperTime = DateTime.Now;
            userEntity.Operator = LoginInfo.UserName.ToString();
            userEntity.CompanyCode = LoginInfo.CompanyId;
            userEntity.Dalance = 0;
            userEntity.Utility_Bill = 0;
            userEntity.Property_fee = 0;
            userEntity.Parking_fee = 0;
            userEntity.Other_fee = 0;
            OwnersApp.SubmitForm(userEntity, keyValue);
            if (!string.IsNullOrEmpty(keyValue))
                return Success("操作成功。");

            //添加充值记录
            RechargeEntity model = new RechargeEntity();
            model.UnitCode = userEntity.UnitCode;
            model.FloorCode = userEntity.FloorCode;
            model.CommunityCode = userEntity.CommunityCode;
            model.CompanyCode = LoginInfo.CompanyId;
            model.C_OwnersNo = userEntity.F_Id;
            model.C_Name = "用户充值";
            model.C_Type = 1;
            model.C_HouseNo = userEntity.O_HouseNo;
            model.C_Date = DateTime.Now;
            model.Operator = LoginInfo.UserName.ToString();
            model.OperTime = DateTime.Now;
            model.money = 0;
            rechargeApp.SubmitForm(model, keyValue);


            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {

            OwnersApp.DeleteForm(keyValue);

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

        /// <summary>
        /// 充值页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TopUp()
        {

            return View();
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTopUp(string keyValue, RechargeEntity model)
        {

            var data = OwnersApp.GetForm(keyValue);
            if (model.C_Name == "物业费")
                data.Property_fee = data.Property_fee + model.money;
            if (model.C_Name == "水电费")
                data.Utility_Bill = data.Utility_Bill + model.money;
            if (model.C_Name == "停车费")
                data.Parking_fee = data.Parking_fee + model.money;
            if (model.C_Name == "其他")
                data.Other_fee = data.Other_fee + model.money;

            data.Dalance = data.Dalance + model.money;
            OwnersApp.SubmitForm(data, keyValue);

            var LoginInfo = OperatorProvider.Provider.GetCurrent();

            //添加充值记录
            // RechargeEntity model = new RechargeEntity();
            model.UnitCode = data.UnitCode;
            model.FloorCode = data.FloorCode;
            model.CommunityCode = data.CommunityCode;
            model.CompanyCode = LoginInfo.CompanyId;
            model.C_OwnersNo = data.F_Id;
            model.C_Name = model.C_Name;

            if (model.C_Name == "物业费")
                model.C_Type = 1;
            if (model.C_Name == "水电费")
                model.C_Type = 2;
            if (model.C_Name == "停车费")
                model.C_Type = 3;
            if (model.C_Name == "其他")
                model.C_Type = 4;

            model.C_HouseNo = data.O_HouseNo;
            model.C_Date = DateTime.Now;
            model.Operator = LoginInfo.UserName.ToString();
            model.OperTime = DateTime.Now;
            model.money = model.money;
            rechargeApp.SubmitForm(model, null);


            return Success("充值成功");
        }

        /// <summary>
        /// 获取公司所有欠费用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetoweList(string keyword)
        {

            //查询业主集合
            var data = OwnersApp.GetList(keyword);


            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            List<OwnersEntity> data1 = data.Where(z => z.CompanyCode == LoginInfo.CompanyId && z.Dalance < 0).ToList();


            List<Owners> list = new List<Owners>();
            foreach (var item in data1)
            {
                Owners listmodel = new Owners();

                listmodel.F_Id = item.F_Id;
                listmodel.O_No = item.O_No;
                listmodel.O_IdNumber = item.O_IdNumber;
                listmodel.O_PropertyNo = item.O_PropertyNo;
                listmodel.O_Name = item.O_Name;
                listmodel.O_HouseNo = item.O_HouseNo;
                listmodel.O_Properties = item.O_Properties;
                listmodel.O_Type = item.O_Type;
                listmodel.O_BuildingArea = item.O_BuildingArea;
                listmodel.O_UseArea = item.O_UseArea;
                listmodel.O_FreeStatand = item.O_FreeStatand;
                listmodel.O_Peoples = item.O_Peoples;
                listmodel.O_Tel = item.O_Tel;
                listmodel.O_Phone = item.O_Phone;
                listmodel.O_StayTime = item.O_StayTime;
                listmodel.Dalance = item.Dalance;
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
        /// 查询计费标准
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBilling()
        {

            return View();
        }


        /// <summary>
        /// 添加房主和收费记录关联
        /// </summary>
        /// <param name="ChargesID">计费ID</param>
        /// <param name="O_HouseNo">房号</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult OwnerAndBilling(string ChargesID, string O_HouseNo)
        {

            var Charges = ChargesID.Split(',');

            foreach (var item in Charges)
            {
                var freeStandard = freeStandardApp.GetForm(item);//查询一条计费公告

                var dt = DateTime.Now;


                DateTime time = dt.AddDays(Convert.ToInt32(freeStandard.F_Cycle));
                //添加关联
                OwnerAndBillingEntity model1 = new OwnerAndBillingEntity();
                model1.ChargesID = item;
                model1.ChargesTiem = time;// ChargesTiem;
                model1.HouseNo = O_HouseNo;
                model1.F_Id = Common.GuId();
                model1.State = true;
                ownerapp.SubmitForm(model1);

            }

            return Success("操作成功");
        }
    }
}
