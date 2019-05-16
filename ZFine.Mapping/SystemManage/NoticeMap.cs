using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFine.Domain._03_Entity.SystemManage;

namespace ZFine.Mapping.SystemManage
{
   
    public class NoticeMap : EntityTypeConfiguration<NoticeEntity>
    {
        public NoticeMap()
        {
            this.ToTable("SC_Notice");
            this.HasKey(t => t.F_Id);
        }
    }
}
