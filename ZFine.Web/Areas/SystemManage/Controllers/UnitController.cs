using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ZFine.Application.SystemManage;
using ZFine.Code;
using ZFine.Domain.Entity.SystemManage;
using ZFine.Web.App_Start;

namespace ZFine.Web.Areas.SystemManage.Controllers
{
    public class UnitController : ControllerBase
    {
        private UnitApp unitApp = new UnitApp();
        private FloorApp floor = new FloorApp();


        /// <summary>
        /// 单元页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UnitIndex()
        {

            return View();
        }




        /// <summary>
        /// 查询楼栋信息绑定下拉列表
        /// </summary>
        /// <returns></returns>
        public string Equipmentselect()
        {
            var data = new
            {
                rows = floor.GetList()
            };
            var LoginInfo = OperatorProvider.Provider.GetCurrent();

            List<FloorEntity> typeList = data.rows;
            StringBuilder sb = new StringBuilder();
            typeList.Where(x => x.CompanyCode == LoginInfo.CompanyId).ToList();
            foreach (var item in typeList)
            {
                sb.Append(string.Format(@"<option selected='selected' value='{0}'>{1}</option>", item.F_Id, item.F_Name));
            }
            ViewBag.SSS = sb;

            return JsonConvert.SerializeObject(sb);
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUnit()
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
        public ActionResult SubmitForm(UnitEntity userEntity, string keyValue, string ImgUrl)
        {
            var LoginInfo = OperatorProvider.Provider.GetCurrent();

            userEntity.CompanyCode = LoginInfo.CompanyId;
            userEntity.F_Id = Common.GuId();
            userEntity.OperTime = DateTime.Now;
            userEntity.Operator = LoginInfo.UserName;
            unitApp.SubmitForm(userEntity, keyValue);


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

         

            unitApp.DeleteForm(keyValue);
          
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

            var data = unitApp.GetForm(keyValue);
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

            var data = unitApp.GetList();

            //查询此公司设备集合
            var rows = unitApp.GetList();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            //List<EquipmentEntity> data1 = rows.Where(z => z.CompanyCode == LoginInfo.CompanyId).ToList();
            //var data2 = data1.Select(x => x.EquipSn).ToList();
            //var date = data.Where(x => data2.Contains(x.EquipNO)).ToList();
            return Content(data.ToJson());
        }

    }
}
