﻿/*******************************************************************************
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
    public class RechargeApp
    {
        private IRechargeRepository service = new RechargeRepository();

        public List<RechargeEntity> GetList(string keyword = "")
        {
            return service.IQueryable().Where(x=>x.CompanyCode!=null).ToList();
        }
        public RechargeEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
           
                service.Delete(t => t.F_Id == keyValue);
            
        }
        public void SubmitForm(RechargeEntity organizeEntity, string keyValue)
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
