/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-05 22:26:52
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-05 22:26:52       情缘
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider.Sys
{
    public partial class LocalProductProvider : DataFactory
    {
        public LocalProductProvider(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 查询库存清单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<LocalProductEntity> GetList(LocalProductEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(item => item.CompanyID == this.CompanyID).And(item => item.Num > 0);
            entity.OrderBy(item => item.ID, EOrderBy.DESC);
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(item => item.StorageNum == entity.StorageNum);
            }
            if (entity.LocalType > 0)
            {
                entity.And(item => item.LocalType == entity.LocalType);
            }
            if (entity.LocalNum.IsNotEmpty())
            {
                entity.And(item => item.LocalNum == entity.LocalNum);
            }
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

            int rowCount = 0;
            List<LocalProductEntity> listResult = this.LocalProduct.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;

            if (!listResult.IsNullOrEmpty())
            {
                ProductProvider productProvider = new ProductProvider(this.CompanyID);
                foreach (LocalProductEntity item in listResult)
                {
                    ProductEntity product = productProvider.GetProduct(item.ProductNum);
                    if (product != null)
                    {
                        item.Size = product.Size;
                        item.CateNum = product.CateNum;
                        item.CateName = product.CateName;
                        item.AvgPrice = product.AvgPrice;
                        item.MinNum = product.MinNum;
                        item.MaxNum = product.MaxNum;
                        item.UnitNum = product.UnitNum;
                        item.UnitName = product.UnitName;
                    }
                }
            }
            return listResult;
        }

        /// <summary>
        /// 查询可出库操作的库存数据集合：
        /// 正式库位以及带入库位的产品是可以出库的
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<LocalProductEntity> GetOutAbleList(LocalProductEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(item => item.CompanyID == this.CompanyID).And(item => item.Num > 0);
            entity.OrderBy(item => item.ID, EOrderBy.DESC);

            entity.AndBegin<LocalProductEntity>()
                .Or<LocalProductEntity>(item => item.LocalType == (int)ELocalType.Normal)
                .Or<LocalProductEntity>(item => item.LocalType == (int)ELocalType.WaitIn)
                .End();


            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(item => item.StorageNum == entity.StorageNum);
            }
            if (entity.LocalType > 0)
            {
                entity.And(item => item.LocalType == entity.LocalType);
            }
            if (entity.LocalNum.IsNotEmpty())
            {
                entity.And(item => item.LocalNum == entity.LocalNum);
            }
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

            int rowCount = 0;
            List<LocalProductEntity> listResult = this.LocalProduct.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;

            if (!listResult.IsNullOrEmpty())
            {
                ProductProvider productProvider = new ProductProvider(this.CompanyID);
                foreach (LocalProductEntity item in listResult)
                {
                    ProductEntity product = productProvider.GetProduct(item.ProductNum);
                    if (product != null)
                    {
                        item.Size = product.Size;
                        item.CateNum = product.CateNum;
                        item.CateName = product.CateName;
                        item.AvgPrice = product.AvgPrice;
                        item.MinNum = product.MinNum;
                        item.MaxNum = product.MaxNum;
                    }
                }
            }
            return listResult;
        }

        /// <summary>
        /// 查询可用于报损的仓库数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<LocalProductEntity> GetBadAbleList(LocalProductEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(item => item.CompanyID == this.CompanyID).And(item => item.Num > 0);
            entity.OrderBy(item => item.ID, EOrderBy.DESC);

            entity.AndBegin<LocalProductEntity>()
                .Or<LocalProductEntity>(item => item.LocalType == (int)ELocalType.Normal)
                .Or<LocalProductEntity>(item => item.LocalType == (int)ELocalType.WaitIn)
                .Or<LocalProductEntity>(item => item.LocalType == (int)ELocalType.WaitCheck)
                .End();

            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(item => item.StorageNum == entity.StorageNum);
            }
            if (entity.LocalType > 0)
            {
                entity.And(item => item.LocalType == entity.LocalType);
            }
            if (entity.LocalNum.IsNotEmpty())
            {
                entity.And(item => item.LocalNum == entity.LocalNum);
            }
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

            int rowCount = 0;
            List<LocalProductEntity> listResult = this.LocalProduct.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;

            if (!listResult.IsNullOrEmpty())
            {
                ProductProvider productProvider = new ProductProvider(this.CompanyID);
                foreach (LocalProductEntity item in listResult)
                {
                    ProductEntity product = productProvider.GetProduct(item.ProductNum);
                    if (product != null)
                    {
                        item.Size = product.Size;
                        item.CateNum = product.CateNum;
                        item.CateName = product.CateName;
                        item.AvgPrice = product.AvgPrice;
                        item.MinNum = product.MinNum;
                        item.MaxNum = product.MaxNum;
                    }
                }
            }
            return listResult;
        }

        /// <summary>
        /// 所有仓库的正式库位和待入库位货品数量的总和
        /// 不区分仓库，不区分库位
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<V_LocalProductEntity> GetList(V_LocalProductEntity entity, ref PageInfo pageInfo)
        {
            V_LocalProductEntity Entity = new V_LocalProductEntity();
            Entity.IncludeAll();
            Entity.OrderBy(a => a.ID, EOrderBy.ASC);
            Entity.Where(a => a.CompanyID == this.CompanyID).And(item => item.Num > 0);

            if (entity.BarCode.IsNotEmpty())
            {
                Entity.Where("BarCode", ECondition.Like, "%" + entity.BarCode + "%");
            }
            if (entity.ProductName.IsNotEmpty())
            {
                Entity.Where("ProductName", ECondition.Like, "%" + entity.ProductName + "%");
            }
            if (entity.Size.IsNotEmpty())
            {
                Entity.Where("Size", ECondition.Like, "%" + entity.Size + "%");
            }
            if (entity.CateNum.IsNotEmpty())
            {
                ProductCategoryProvider provider = new ProductCategoryProvider(this.CompanyID);
                List<ProductCategoryEntity> listCate = provider.GetChildList(entity.CateNum);
                string[] items = null;
                if (!listCate.IsNullOrEmpty())
                {
                    items = listCate.Select(item => item.SnNum).ToArray();
                }
                else
                {
                    items = new string[] { "" };
                }
                Entity.And("CateNum", ECondition.In, items);
            }

            int rowCount = 0;
            List<V_LocalProductEntity> listResult = this.V_LocalProduct.GetList<V_LocalProductEntity>(Entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;

            return listResult;
        }
    }
}
