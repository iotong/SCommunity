/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;

namespace ZFine.Domain.Entity.SystemManage
{
    public class FreeStandardEntity : IEntity<FreeStandardEntity>, ICreationAudited, IModificationAudited
    {

        /// <summary>
        /// 单元代码
        /// </summary>
        public string UnitCode { get; set; }
        /// <summary>
        /// 楼栋代码
        /// </summary>
        public string FloorCode { get; set; }
        /// <summary>
        /// 小区代码
        /// </summary>
        public string CommunityCode { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 费用名称
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 计费类型
        /// </summary>
        public string F_Type { get; set; }
        /// <summary>
        /// 计费标准
        /// </summary>
        public string F_Standard { get; set; }
        /// <summary>
        /// 计费周期
        /// </summary>
        public string F_Cycle { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal F_Money { get; set; }
        /// <summary>
        /// 费用说明
        /// </summary>
        public string F_Memo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsUpload { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperTime { get; set; }

     
        public string F_Id { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }

    }
}
