/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using ZFine.Domain.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace ZFine.Mapping.SystemManage
{
    public class FloorMap : EntityTypeConfiguration<FloorEntity>
    {
        public FloorMap()
        {
            this.ToTable("SC_Floor");
            this.HasKey(t => t.F_Id);
        }
    }
}
