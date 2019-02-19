/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/3/21 15:47:45
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/3/21 15:47:45       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Entity.Report;
using Git.Storage.Entity.Storage;
using Git.Storage.Provider.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider.Report
{
    public partial class BalanceBookProvider:DataFactory
    {
        public BalanceBookProvider(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 查询期初期末数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<BalanceBookEntity> GetList(BalanceBookEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(item => item.CompanyID == this.CompanyID);
            entity.OrderBy(item => item.ID, EOrderBy.DESC);

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
            if (entity.Day.IsNotEmpty())
            {
                entity.And("Day", ECondition.Like, "%" + entity.Day + "%");
            }

            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime begin = ConvertHelper.ToType<DateTime>(entity.BeginTime,DateTime.Now.AddDays(-30)).Date;
                entity.And(item=>item.CreateTime>=begin);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime end = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).AddDays(1).Date;
                entity.And(item => item.CreateTime<end);
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(item=>item.StorageNum==entity.StorageNum);
            }
            int rowCount = 0;
            List<BalanceBookEntity> listResult = this.BalanceBook.GetList(entity,pageInfo.PageSize,pageInfo.PageIndex,out rowCount);
            pageInfo.RowCount = rowCount;

            if (!listResult.IsNullOrEmpty())
            {
                ProductProvider provider = new ProductProvider(this.CompanyID);
                StorageProvider storageProvider = new StorageProvider(this.CompanyID);
                foreach (BalanceBookEntity item in listResult)
                {
                    ProductEntity product = provider.GetProduct(item.ProductNum);
                    if (product != null)
                    {
                        item.Size = product.Size;
                        item.UnitName = product.UnitName;
                    }

                    StorageEntity storage = storageProvider.GetSingleByNum(item.StorageNum);
                    if (storage != null)
                    {
                        item.StorageName = storage.StorageName;
                    }
                }
            }
            return listResult;
        }

        /// <summary>
        /// 查询库存期初期末数据
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public BalanceBookEntity GetSingle(string SnNum)
        {
            BalanceBookEntity entity = new BalanceBookEntity();
            entity.IncludeAll();
            entity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            BalanceBookEntity result = this.BalanceBook.GetSingle(entity);

            return result;
        }
    }
}
