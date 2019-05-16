/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;

namespace ZFine.Domain.Entity.SystemManage
{
    public class RechargeEntity : IEntity<RechargeEntity>, ICreationAudited, IModificationAudited
    {
       
        /// <summary>
        /// 
        /// </summary>
        public string UnitCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FloorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CommunityCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string C_OwnersNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string C_Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int C_Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string C_HouseNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime C_Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OperTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_CreatorUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? F_CreatorTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_LastModifyUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? F_LastModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal money { get; set; }
    }
}
