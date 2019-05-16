/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;

namespace ZFine.Domain.Entity.SystemManage
{
    public class CommunityInfoEntity : IEntity<CommunityInfoEntity>, ICreationAudited, IModificationAudited
    {


        /// <summary>
        /// 小区名字
        /// </summary>
        public string C_Name { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string C_Head { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string C_HeadTel { get; set; }
        /// <summary>
        /// 服务电话
        /// </summary>
        public string C_Tel { get; set; }
        /// <summary>
        /// 小区地址
        /// </summary>
        public string C_Addr { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal C_Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal C_Latitude { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string ComanyCode { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public string IsDel { get; set; }
        /// <summary>
        /// 是否上传
        /// </summary>
        public string IsUpload { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Opertror { get; set; }
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
