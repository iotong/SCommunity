/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;

namespace ZFine.Domain.Entity.SystemManage
{
    public class OwnersEntity : IEntity<OwnersEntity>, ICreationAudited, IModificationAudited
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
        public string O_No { get; set; }
        /// <summary>
        /// 不动产号
        /// </summary>
        public string O_PropertyNo { get; set; }
        /// <summary>
        /// 单元编号
        /// </summary>
        public string O_Name { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string O_IdNumber { get; set; }
        /// <summary>
        /// 房号
        /// </summary>
        public string O_HouseNo { get; set; }
        /// <summary>
        /// 房屋性质
        /// </summary>
        public string O_Properties { get; set; }
        /// <summary>
        /// 房屋类型
        /// </summary>
        public string O_Type { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public Decimal O_BuildingArea { get; set; }
        /// <summary>
        /// 使用面积
        /// </summary>
        public Decimal O_UseArea { get; set; }
        /// <summary>
        /// 物业费标准
        /// </summary>
        public Decimal O_FreeStatand { get; set; }
        /// <summary>
        /// 常住人员
        /// </summary>
        public string O_Peoples { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string O_Tel { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string O_Phone { get; set; }
        /// <summary>
        /// 入住时间
        /// </summary>
        public DateTime? O_StayTime { get; set; }
        /// <summary>
        /// 公众号
        /// </summary>
        public string O_WenXInNo { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string O_Pass { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string O_RealName { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public string O_LastTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 是否上传
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

        public string F_Id { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set;  }

        /// <summary>
        /// 设备号
        /// </summary>
        public string EquipNO { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Dalance { get; set; }
        /// <summary>
        /// 水电费
        /// </summary>
        public decimal Utility_Bill { get; set; }
        /// <summary>
        /// 物业费
        /// </summary>
        public decimal Property_fee { get; set; }
        /// <summary>
        /// 停车费
        /// </summary>
        public decimal Parking_fee { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal Other_fee { get; set; }
    }
}
