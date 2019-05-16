/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;

namespace ZFine.Domain.Entity.SystemManage
{
    public class RepairEntity : IEntity<RepairEntity>, ICreationAudited, IModificationAudited
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
        public int R_Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string R_People { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string R_Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string R_Company { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string R_Community { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string R_Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? R_Time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string R_PickPeople { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<System.DateTime> R_PickTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string R_MainPeople { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? R_MainTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool R_ManResults { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string R_PeopleMY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsUpload { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OperTime { get; set; }
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
        public string R_Image { get; set; }
    }
}
