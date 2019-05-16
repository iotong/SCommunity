using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFine.Code;
using ZFine.Data;
using ZFine.Domain._03_Entity.SystemManage;
using ZFine.Domain._04_IRepository.SystemManage;
using ZFine.Domain.Entity.SystemManage;

namespace ZFine.Repository.SystemManage
{
    public class NoticeRepository : RepositoryBase<NoticeEntity>, INoticeRepository
    {
     
        public void SubmitForm(NoticeEntity userEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(userEntity);
                }
                else
                {
                   
                    db.Insert(userEntity);
                   
                }
                db.Commit();

            }
        }
    }
    }
