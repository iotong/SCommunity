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
using ZFine.Code;

namespace ZFine.Application.SystemManage
{
    public class OwnersApp
    {
        private IOwnersRepository service = new OwnersRepository();

        public List<OwnersEntity> GetList( string keyword)
        {
            var expression = ExtLinq.True<OwnersEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.O_HouseNo.Contains(keyword));
                expression = expression.Or(t => t.O_Name.Contains(keyword));
            }
          
            return service.IQueryable(expression).ToList();

        }
        public OwnersEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
           
                service.Delete(t => t.F_Id == keyValue);
            
        }
        public void SubmitForm(OwnersEntity organizeEntity, string keyValue)
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
