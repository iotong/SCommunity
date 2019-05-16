/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;

namespace ZFine.Domain.Entity.SystemManage
{
    public class UnitEntity : IEntity<UnitEntity>, ICreationAudited, IModificationAudited
    {


        /// <summary>
        ///单元名称
        /// </summary>
        public string U_Name { get; set; }
        /// <summary>
        /// 单元位置
        /// </summary>
        public string U_Addr { get; set; }
        /// <summary>
        /// 单元人口数
        /// </summary>
        public int U_Rks { get; set; }
        /// <summary>
        /// 单元总户数
        /// </summary>
        public int U_Fs { get; set; }
        /// <summary>
        /// 空置户数
        /// </summary>
        public int U_Kzfs { get; set; }
        /// <summary>
        /// 住宅户数
        /// </summary>
        public int U_Zzfs { get; set; }
        /// <summary>
        /// 商用户数
        /// </summary>
        public int U_Syfs { get; set; }
        /// <summary>
        /// 单元管理员
        /// </summary>
        public string U_Mange { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 是否上传
        /// </summary>
        public bool IsUpload { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string U_Tel { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }
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
