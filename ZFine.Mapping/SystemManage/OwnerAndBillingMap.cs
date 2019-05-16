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
    public class OwnerAndBillingMap : EntityTypeConfiguration<OwnerAndBillingEntity>
    {
        public OwnerAndBillingMap()
        {
            this.ToTable("OwnerAndBilling");
            this.HasKey(t => t.F_Id);
        }
    }
}
