/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;

namespace ZFine.Domain.Entity.SystemManage
{
    public class OwnersItemsEntity : IEntity<OwnersItemsEntity>, ICreationAudited, IModificationAudited
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
        /// 业主编号
        /// </summary>
        public string OI_OwnersNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string OI_Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string OI_Sex { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string OI_IdNumber { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string OI_Notional { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string OI_Tel { get; set; }
        /// <summary>
        /// 联系手机号
        /// </summary>
        public string OI_Phone { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? OI_Birthday { get; set; }
        /// <summary>
        /// 发证机关
        /// </summary>
        public string OI_Idcompany { get; set; }
        /// <summary>
        /// 身份证有效期
        /// </summary>
        public DateTime? OI_Validity { get; set; }
        /// <summary>
        /// 与业主关系
        /// </summary>
        public string OI_Rela { get; set; }
        /// <summary>
        /// 卡号1
        /// </summary>
        public string OI_Card1 { get; set; }
        /// <summary>
        /// 卡号2
        /// </summary>
        public string OI_Card2 { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool OI_Enable { get; set; }
        public bool IsDel { get; set; }
        public bool IsUpload { get; set; }
        public string Operator { get; set; }
        public DateTime? OperTime { get; set; }
        /// <summary>
        /// 指纹1文件名
        /// </summary>
        public string OI_Finger1FileName { get; set; }
        /// <summary>
        /// 指纹2文件名
        /// </summary>
        public string OI_Finger2FileName { get; set; }
        /// <summary>
        /// 人脸图片文件名
        /// </summary>
        public string OI_FaceImgFileName { get; set; }
        /// <summary>
        /// 指纹1
        /// </summary>
        public string OI_Finger1 { get; set; }
        /// <summary>
        /// 指纹1
        /// </summary>
        public string OI_Finger2 { get; set; }
        /// <summary>
        /// 人脸图片
        /// </summary>
        public string OI_FaceImgP { get; set; }

        public string F_Id { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string EquipNO { get; set; }
        /// <summary>
        /// 房号
        /// </summary>
        public string O_HouseNo { get; set; }
        
    }
}
