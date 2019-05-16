using ZFine.Domain.Entity.SystemManage;
using ZFine.Domain.IRepository.SystemManage;
using ZFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using ZFine.Domain._03_Entity.SystemManage;
using ZFine.Domain._04_IRepository.SystemManage;
namespace ZFine.Application.SystemManage
{
    public class NoticeApp
    {
        private INoticeRepository service = new NoticeRepository();


        public List<NoticeEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        public NoticeEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(NoticeEntity areaEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                areaEntity.Modify(keyValue);
               
            }
            else
            {
                areaEntity.Create();
                
            }
            service.SubmitForm(areaEntity, keyValue);
        }
    }
}
