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
    public class RechargeController : ControllerBase
    {

        private RechargeApp areaApp = new RechargeApp();

        //小区
        private CommunityInfoApp communityInfoapp = new CommunityInfoApp();
        //单元
        private UnitApp unitApp = new UnitApp();
        //楼栋
        private FloorApp floorApp = new FloorApp();

        /// <summary>
        /// 查看账单页面
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult GetRecharge()
        {

            return View();

        }

        ///// <summary>
        ///// 查询账单
        ///// </summary>
        ///// <param name="pagination"></param>
        ///// <param name="keyword"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[HandlerAjaxOnly]
        //public ActionResult GetGridJson(Pagination pagination, string keyword)
        //{
        //    //查询费用清单
        //    var rows = areaApp.GetList();
           
        //    var LoginInfo = OperatorProvider.Provider.GetCurrent();
        //    var date = rows.Where(x => x.CompanyCode==LoginInfo.CompanyId).ToList();
        //    return Content(date.ToJson());
        //}

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
            var data = areaApp.GetList(keyword);
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            
           var date = data.Where(x => x.CompanyCode==LoginInfo.CompanyId).ToList();
            List<Owners> list = new List<Owners>();

            foreach (var item in date)
            {
                Owners listmodel = new Owners();

                listmodel.F_Id = item.F_Id;
                listmodel.C_Name = item.C_Name;
                listmodel.C_HouseNo = item.C_HouseNo;
                listmodel.money = item.money;
                listmodel.OperTime = item.OperTime;
                listmodel.C_Type = item.C_Type;
                
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
            public string C_Name { get; set; }
            public string C_HouseNo { get; set; }
            public decimal money { get; set; }
            public int C_Type { get; set; }
            public DateTime? OperTime { get; set; }
            public string UnitCode { get; set; }
            public string FloorCode { get; set; }
            public string CommunityCode { get; set; }
            public string CompanyCode { get; set; }
            
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

       


    }
}
