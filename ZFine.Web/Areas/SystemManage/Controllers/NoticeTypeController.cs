using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZFine.Application.SystemManage;
using ZFine.Code;
using ZFine.Domain._03_Entity.SystemManage;

namespace ZFine.Web.Areas.SystemManage.Controllers
{
    public class NoticeTypeController : ControllerBase
    { 

        private NoticeTypeApp noticeTypeApp = new NoticeTypeApp();
        //
        // GET: /SystemManage/NoticeType/

        /// <summary>
        /// 公告类型
        /// </summary>
        /// <returns></returns>
        public ActionResult NoticeType()
        {
            return View();
        }

        /// <summary>
        /// 查询公告类型
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = noticeTypeApp.GetList(),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        public ActionResult addNoticeType()
        {

            return View();

        }
        /// <summary>
        /// 添加公告类型
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateInput(false)]
        public ActionResult addNoticeType(string Name, string keyValue)
        {
            NoticeTypeEntity areaEntity = new NoticeTypeEntity();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();

            areaEntity.F_Id = Common.GuId();
            areaEntity.F_DeleteMark = true;
            areaEntity.F_ParentId = "1";
            areaEntity.F_CreatorTime = Convert.ToDateTime(DateTime.Now.ToString());
            areaEntity.F_CreatorUserId = LoginInfo.UserId.ToString();
            areaEntity.F_LastModifyTime = Convert.ToDateTime(DateTime.Now.ToString());
            areaEntity.F_LastModifyUserId = LoginInfo.UserId.ToString();
            areaEntity.F_DeleteTime = Convert.ToDateTime(DateTime.Now.ToString());
            areaEntity.F_DeleteUserId = LoginInfo.UserId.ToString();
            areaEntity.TypeName =Name;
            areaEntity.Datetime = Convert.ToDateTime(DateTime.Now.ToString());
            noticeTypeApp.SubmitForm(areaEntity, keyValue);

            return View();


        }

        /// <summary>
        /// 查询一条公告类型
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = noticeTypeApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 删除公告类型
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>

        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            noticeTypeApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }


    }
}
