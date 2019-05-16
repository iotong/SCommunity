/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;

namespace ZFine.Domain.Entity.SystemManage
{
    public class EquipmentEntity : IEntity<EquipmentEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public bool? F_DeleteMark { get; set; }

        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
        
        public string EquipName { get; set; }
        public string EquipType { get; set; }
        public string EquipPosition { get; set; }
        public decimal EquipLongitude { get; set; }
        public decimal EquipLatitude { get; set; }
        public string LoginName { get; set; }
        public string LoginPass { get; set; }
        public string EquipAddr { get; set; }
        public int? EquipPort { get; set; }
        public string EquipSn { get; set; }
        public string Suppliers { get; set; }
        public string EquipModel { get; set; }
        public string EquipCode { get; set; }
        public string FaceAlgorithmVer { get; set; }
        public string SdkVer { get; set; }
        public string Appid { get; set; }
        public string SdkKey { get; set; }
        public string SoftVer { get; set; }
        public string EquipMask { get; set; }
        public string ServerIp { get; set; }
        public string ServerPort { get; set; }
        public string ServerId { get; set; }
        public string ServerMask { get; set; }
        public bool AutoUpdate { get; set; }
        public string ShowTxt { get; set; }
        public int? AutoRemind { get; set; }
        public bool? AutoDelete { get; set; }
        public bool? UpLoadDelete { get; set; }
        public string RemindTxt { get; set; }
        public string FloorCode { get; set; }
        public string UnitCode { get; set; }
        public string CommunityCode { get; set; }
        public string CompanyCode { get; set; }
        public string ImagePath { get; set; }
        public bool? IsDel { get; set; }
        public bool? IsUpload { get; set; }
        public string Operator { get; set; }
        public DateTime? OperTime { get; set; }
    }
}
