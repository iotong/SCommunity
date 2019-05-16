/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using ZFine.Domain.Entity.SystemManage;
using ZFine.Domain.IRepository.SystemManage;
using ZFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZFine.Application.SystemManage
{
    /// <summary>
    /// 设备报修
    /// </summary>
    public class RepairApp
    {
        private IRepairRepository service = new RepairRepository();

        public List<RepairEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }
        public RepairEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
           
                service.Delete(t => t.F_Id == keyValue);
            
        }
        public void SubmitForm(RepairEntity organizeEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                service.Update(organizeEntity);
            }
            else
            {
                organizeEntity.Create();
                service.Insert(organizeEntity);
            }
        }
    }
}
