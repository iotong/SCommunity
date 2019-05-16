/*******************************************************************************
 * Copyright © 2016
 * 
 * Description: MVC快速开发平台  FROM http://xmwxgn.com
 *
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using ZFine.Domain.Entity.SystemManage;
using ZFine.Domain.IRepository.SystemManage;
using ZFine.Repository.SystemManage;

namespace ZFine.Application.SystemManage
{
    public class OwnerAndBillingApp
    {
        private IOwnerAndBillingRepository service = new OwnerAndBillingRepository();

        public List<OwnerAndBillingEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        public OwnerAndBillingEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            
                service.Delete(t => t.F_Id == keyValue);
            
        }
        public void SubmitForm(OwnerAndBillingEntity areaEntity)
        {
           
               // areaEntity.Create();
                service.Insert(areaEntity);
            
        }
    }
}
