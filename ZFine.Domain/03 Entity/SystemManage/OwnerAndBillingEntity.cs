/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;

namespace ZFine.Domain.Entity.SystemManage
{
    public class OwnerAndBillingEntity : IEntity<OwnerAndBillingEntity>
    {
        public string F_Id { get; set; }
        public string UnitCode { get; set; }
        public string CommunityCode { get; set; }
        public string CompanyCode { get; set; }
        public string HouseNo { get; set; }
        public string ChargesID { get; set; }
        public DateTime? ChargesTiem { get; set; }
        public bool? State { get; set; }

    }
}
