/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-09-19 20:27:45
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-09-19 20:27:45       情缘
*********************************************************************************/

using Git.Storage.Entity.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;

namespace Git.Storage.Provider.Sys
{
    public partial class InventoryBookProvider:DataFactory
    {
        public InventoryBookProvider(string _companyID) 
        {
            this.CompanyID = _companyID;
        }

        /// <summary>
        /// 查询台账记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<InventoryBookEntity> GetList(InventoryBookEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.Where(a => a.CompanyID == this.CompanyID);

            if (entity.BarCode.IsNotEmpty()) 
            {
                entity.And("BarCode", ECondition.Like, "%" + entity.BarCode + "%");
            }
            if (entity.ProductName.IsNotEmpty())
            {
                entity.And("ProductName", ECondition.Like, "%" + entity.ProductName + "%");
            }
            if (entity.BatchNum.IsNotEmpty())
            {
                entity.And("BatchNum", ECondition.Like, "%" + entity.BatchNum + "%");
            }
            if (entity.Type > 0)
            {
                entity.And(a => a.Type == entity.Type);
            }
            if (entity.OrderNum.IsNotEmpty())
            {
                entity.And("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.ContactOrder.IsNotEmpty())
            {
                entity.And(item=>item.ContactOrder==entity.ContactOrder);
            }
            if (entity.FromStorageNum.IsNotEmpty())
            {
                entity.And(item => item.FromStorageNum == entity.FromStorageNum);
            }
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime begin = ConvertHelper.ToType<DateTime>(entity.BeginTime,DateTime.Now.AddDays(-30)).Date;
                entity.And(a => a.CreateTime >= begin);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime end = ConvertHelper.ToType<DateTime>(entity.EndTime,DateTime.Now).AddDays(1).Date;
                entity.And(a => a.CreateTime < end);
            }
            int rowCount = 0;
            List<InventoryBookEntity> listResult = this.InventoryBook.GetList(entity,pageInfo.PageSize,pageInfo.PageIndex,out rowCount);
            pageInfo.RowCount = rowCount;

            if (!listResult.IsNullOrEmpty())
            {
                List<LocationEntity> listLocation = new LocationProvider(this.CompanyID).GetList();
                listLocation = listLocation == null ? new List<LocationEntity>() : listLocation;

                ProductProvider provider = new ProductProvider(this.CompanyID);
                foreach (InventoryBookEntity item in listResult)
                {
                    if (item.FromLocalNum.IsNotEmpty())
                    {
                        LocationEntity location = listLocation.FirstOrDefault(a => a.LocalNum == item.FromLocalNum);
                        item.FromLocalName = location != null ? location.LocalName : string.Empty;
                        item.FromStorageName = location != null ? location.StorageName : string.Empty;
                    }

                    if (item.ToLocalNum.IsNotEmpty())
                    {
                        LocationEntity location = listLocation.FirstOrDefault(a => a.LocalNum == item.ToLocalNum);
                        item.ToLocalName = location != null ? location.LocalName : string.Empty;
                        item.ToStorageName = location != null ? location.StorageName : string.Empty;
                    }

                    ProductEntity product = provider.GetProduct(item.ProductNum);
                    if (product != null)
                    {
                        item.Size = product.Size;
                        item.UnitName = product.UnitName;
                    }
                }
            }
            return listResult;
        }
    }
}
