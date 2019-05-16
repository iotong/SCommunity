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
    public class OwnersItemsApp1
    {
        private IOwnersItemsRepository service = new OwnersItemsRepository();

        public List<OwnersItemsEntity> GetList(string keyword="")
        {
            var expression = ExtLinq.True<OwnersItemsEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.OI_Name.Contains(keyword));
                expression = expression.Or(t => t.EquipNO.Contains(keyword));
                expression = expression.Or(t => t.OI_Phone.Contains(keyword));
                expression = expression.Or(t => t.O_HouseNo.Contains(keyword));
            }
      
            return service.IQueryable(expression).ToList();
            
        }
        public OwnersItemsEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {

            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(OwnersItemsEntity areaEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                areaEntity.Modify(keyValue);
                service.Update(areaEntity);
            }
            else
            {
                areaEntity.Create();
                service.Insert(areaEntity);
            }
        }
    }
}
