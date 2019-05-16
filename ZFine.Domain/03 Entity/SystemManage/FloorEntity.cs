/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;

namespace ZFine.Domain.Entity.SystemManage
{
    public class FloorEntity : IEntity<FloorEntity>, ICreationAudited, IModificationAudited
    {

        /// <summary>
        /// 
        /// </summary>
        public string FloorCode { get; set; }
        /// <summary>
        /// 楼栋名称
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 楼栋地址
        /// </summary>
        public string F_Addr { get; set; }
        /// <summary>
        /// 单元数量
        /// </summary>
        public string F_Unit { get; set; }
        /// <summary>
        /// 楼层信息
        /// </summary>
        public string F_Info { get; set; }
        /// <summary>
        /// 总人口数
        /// </summary>
        public int F_Rkzs { get; set; }
        /// <summary>
        /// 总户数
        /// </summary>
        public int F_Fs { get; set; }
        /// <summary>
        /// 入住户数
        /// </summary>
        public int F_Rzfs { get; set; }
        /// <summary>
        /// 空置户数
        /// 
        /// </summary>
        public int F_Kzfs { get; set; }
        /// <summary>
        /// 住宅户数
        /// 
        /// </summary>
        public int F_Zzfs { get; set; }
        /// <summary>
        /// 商用户数
        /// </summary>
        public int  F_Syfs { get; set; }
        /// <summary>
        /// 管理楼长
        /// </summary>
        public string F_Mange { get; set; }
        /// <summary>
        /// 小区代码
        /// </summary>
        public string CommunityCode { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 是否上传
        /// </summary>
        public bool IsUpload { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OperTime { get; set; }

        public string F_Id { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }

    }
}
