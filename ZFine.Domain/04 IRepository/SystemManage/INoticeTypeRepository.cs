using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFine.Data;
using ZFine.Domain._03_Entity.SystemManage;
using ZFine.Domain.Entity.SystemManage;

namespace ZFine.Domain._04_IRepository.SystemManage
{
    public interface INoticeTypeRepository : IRepositoryBase<NoticeTypeEntity>
    {
      
        void SubmitForm(NoticeTypeEntity userEntity, string keyValue);
    }

}
