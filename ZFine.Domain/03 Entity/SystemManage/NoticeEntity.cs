using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ZFine.Domain._03_Entity.SystemManage
{
    public class NoticeEntity : IEntity<NoticeEntity>, ICreationAudited, IDeleteAudited, IModificationAudited

    {
        public string F_Id { get; set; }
        public bool? F_DeleteMark { get; set; }
        public string F_ParentId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }

      
        public string UnitCode { get; set; }
        public string FloorCode { get; set; }
        public string CommunityCode { get; set; }
        public string CompanyCode { get; set; }
        public int N_Type { get; set; }

        public string N_Type_Id { get; set; }
        public string N_Title { get; set; }
        public string N_Content { get; set; }
        public string N_Way { get; set; }
        public int N_StopTime { get; set; }
        public string N_ReleaseTime { get; set; }
        public string EquipNo { get; set; }
        public string N_ContgentAddr { get; set; }
        public bool IsDel { get; set; }
        public bool IsUpload { get; set; }
        public string Operator { get; set; }
        public DateTime? OperTime { get; set; }
        public int contentType { get; set; }

    }
}
