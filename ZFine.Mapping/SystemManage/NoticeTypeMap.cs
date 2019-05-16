using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFine.Domain._03_Entity.SystemManage;

namespace ZFine.Mapping.SystemManage
{
   
    public class NoticeTypeMap : EntityTypeConfiguration<NoticeTypeEntity>
    {
        public NoticeTypeMap()
        {
            this.ToTable("SC_NoticeType");
            this.HasKey(t => t.F_Id);
        }
    }
}
