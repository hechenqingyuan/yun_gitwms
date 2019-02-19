/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-29 18:00:37
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-29 18:00:37       情缘
*********************************************************************************/

using Git.Framework.Cache;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Framework.ORM;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Storage.Provider.Base;

namespace Git.Storage.Provider.Sys
{
    public partial class ProductProvider : DataFactory
    {
        public ProductProvider(string CompanyID) 
        {
            this.CompanyID = CompanyID;
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(ProductEntity entity)
        {
            string Key = string.Format(CacheKey.JOOSHOW_PRODUCT_CACHE, this.CompanyID);
            entity.IncludeAll();
            entity.InPrice = entity.AvgPrice;
            entity.OutPrice = entity.AvgPrice;
            entity.SnNum = ConvertHelper.NewGuid();
            entity.BarCode = entity.BarCode.IsEmpty() ? new TNumProvider(CompanyID).GetSwiftNum(typeof(ProductEntity), 6) : entity.BarCode;

            //处理类别
            if (!entity.CateNum.IsEmpty())
            {
                ProductCategoryEntity category = new ProductCategoryProvider(this.CompanyID).GetSingle(entity.CateNum);
                entity.CateName = category != null ? category.CateName : "";
            }
            if (!entity.UnitNum.IsEmpty())
            {
                MeasureEntity meaure = new MeasureProvider(this.CompanyID).GetMeasure(entity.UnitNum);
                entity.UnitName = meaure != null ? meaure.MeasureName : "";
            }
            int line = this.Product.Add(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(ProductEntity entity)
        {
            string Key = string.Format(CacheKey.JOOSHOW_PRODUCT_CACHE, this.CompanyID);

            entity.InPrice = entity.AvgPrice;
            entity.OutPrice = entity.AvgPrice;

            //处理类别
            if (!entity.CateNum.IsEmpty())
            {
                ProductCategoryEntity category = new ProductCategoryProvider(this.CompanyID).GetSingle(entity.CateNum);
                entity.CateName = category != null ? category.CateName : "";
            }
            if (!entity.UnitNum.IsEmpty())
            {
                MeasureEntity meaure = new MeasureProvider(this.CompanyID).GetMeasure(entity.UnitNum);
                entity.UnitName = meaure != null ? meaure.MeasureName : "";
            }

            entity.Include(a => new { a.ProductName, a.BarCode,a.FactoryNum,a.InCode, a.MinNum, a.MaxNum, a.UnitNum, a.UnitName, a.CateNum, a.CateName, a.Size, a.InPrice, a.OutPrice, a.AvgPrice, a.GrossWeight, a.NetWeight, a.StorageNum, a.DefaultLocal, a.CusNum, a.CusName,a.SupNum,a.SupName, a.Description,a.Display,a.Remark });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            int line = this.Product.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 删除产品信息
        /// </summary>
        /// <param name="snNum"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<string> list)
        {
            string Key = string.Format(CacheKey.JOOSHOW_PRODUCT_CACHE, this.CompanyID);

            ProductEntity entity = new ProductEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where("SnNum",ECondition.In,list.ToArray());
            entity.And(a=>a.CompanyID==this.CompanyID);
            int line = this.Product.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 根据产品的唯一编号删除产品
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public int Delete(string SnNum)
        {
            string Key = string.Format(CacheKey.JOOSHOW_PRODUCT_CACHE, this.CompanyID);

            ProductEntity entity = new ProductEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(item=>item.SnNum==SnNum);
            entity.And(a => a.CompanyID == this.CompanyID);
            int line = this.Product.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 获得产品的缓存数据
        /// </summary>
        /// <returns></returns>
        public List<ProductEntity> GetList()
        {
            string Key = string.Format(CacheKey.JOOSHOW_PRODUCT_CACHE, this.CompanyID);
            List<ProductEntity> list = CacheHelper.Get(Key) as List<ProductEntity>;
            if (!list.IsNullOrEmpty())
            {
                return list;
            }
            ProductEntity entity = new ProductEntity();
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID);
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            list = this.Product.GetList(entity);

            if (!list.IsNullOrEmpty())
            {
                StorageProvider storageProvider = new StorageProvider(this.CompanyID);
                List<StorageEntity> listStorage = storageProvider.GetList();
                listStorage = listStorage.IsNull() ? new List<StorageEntity>() : listStorage;

                LocationProvider locationProvider = new LocationProvider(this.CompanyID);
                List<LocationEntity> listLocation = locationProvider.GetList();
                listLocation = listLocation.IsNull() ? new List<LocationEntity>() : listLocation;

                foreach (ProductEntity item in list)
                {
                    if (!item.StorageNum.IsEmpty())
                    {
                        StorageEntity storage = listStorage.FirstOrDefault(a=>a.SnNum==item.StorageNum);
                        item.StorageName = storage.IsNull() ? string.Empty : storage.StorageName;
                    }

                    if (!item.DefaultLocal.IsEmpty())
                    {
                        LocationEntity location = listLocation.FirstOrDefault(a => a.LocalNum == item.DefaultLocal);
                        item.LocalName = location.IsNull() ? string.Empty : location.LocalName;
                    }
                }
            }
            if (!list.IsNullOrEmpty())
            {
                CacheHelper.Insert(Key, list);
            }
            return list;
        }

        /// <summary>
        /// 查询产品列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<ProductEntity> GetList(ProductEntity entity, ref PageInfo pageInfo)
        {
            List<ProductEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return listSource;
            }
            int rowCount = 0;
            List<ProductEntity> listResult = listSource;
            if (!entity.BarCode.IsEmpty())
            {
                listResult = listResult.Where(a => a.BarCode.Contains(entity.BarCode)).ToList();
            }
            if (!entity.ProductName.IsEmpty())
            {
                listResult = listResult.Where(a => a.ProductName.Contains(entity.ProductName)).ToList();
            }
            if (entity.FactoryNum.IsNotEmpty())
            {
                listResult = listResult.Where(a => a.FactoryNum.Contains(entity.FactoryNum)).ToList();
            }
            if (entity.InCode.IsNotEmpty())
            {
                listResult = listResult.Where(a => a.InCode.Contains(entity.InCode)).ToList();
            }
            if (!entity.Size.IsEmpty())
            {
                listResult = listResult.Where(a => a.Size.Contains(entity.Size)).ToList();
            }
            if (!entity.CateNum.IsEmpty())
            {
                ProductCategoryProvider cateProvider = new ProductCategoryProvider(this.CompanyID);
                List<ProductCategoryEntity> listCate = cateProvider.GetChildList(entity.CateNum);
                listCate = listCate.IsNull() ? new List<ProductCategoryEntity>() : listCate;
                listResult = listResult.Where(a => listCate.Exists(item=>item.SnNum==a.CateNum) ).ToList();
            }
            if (entity.IsSingle > 0)
            {
                listResult = listResult.Where(a => a.IsSingle==entity.IsSingle).ToList();
            }
            rowCount = listResult.Count;
            pageInfo.RowCount = rowCount;
            return listResult.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
        }

        /// <summary>
        /// 查询产品信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public ProductEntity GetProduct(string SnNum)
        {
            List<ProductEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return null;
            }
            ProductEntity result = listSource.FirstOrDefault(a => a.SnNum == SnNum);
            return result;
        }

        /// <summary>
        /// 根据条码扫描获得产品
        /// </summary>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public ProductEntity Scan(string BarCode)
        {
            List<ProductEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return null;
            }
            ProductEntity result = listSource.FirstOrDefault(a => a.BarCode == BarCode);
            return result;
        }

        /// <summary>
        /// 使用产品关键字搜索
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public List<ProductEntity> GetSearch(string KeyWord)
        {
            List<ProductEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return null;
            }
            List<ProductEntity> listResult = listSource
                .Where(item => item.BarCode.Contains(KeyWord) || item.FactoryNum.Contains(KeyWord) || item.ProductName.Contains(KeyWord) || item.Size.Contains(KeyWord))
                .ToList()
                ;
            return listResult;
        }

        /// <summary>
        /// 根据产品的唯一编号批量查找产品信息
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public List<ProductEntity> GetProductListBySn(string[] items)
        {
            List<ProductEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return listSource;
            }
            if (!items.IsNullOrEmpty())
            {
                return listSource.Where(item => items.Contains(item.SnNum)).ToList();
            }
            return null;
        }

        /// <summary>
        /// 根据产品的条码编号批量查询产品信息
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public List<ProductEntity> GetProductListByBarCode(string[] items)
        {
            List<ProductEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return listSource;
            }
            if (!items.IsNullOrEmpty())
            {
                return listSource.Where(item => items.Contains(item.BarCode)).ToList();
            }
            return null;
        }
    }
}
