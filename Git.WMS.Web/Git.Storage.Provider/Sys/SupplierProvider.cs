/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-16 22:06:50
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-16 22:06:50       情缘
*********************************************************************************/

using Git.Framework.Cache;
using Git.Framework.Log;
using Git.Framework.ORM;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider.Sys
{
    public partial class SupplierProvider : DataFactory
    {
        public SupplierProvider(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 获得所有供应商信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<SupplierEntity> GetList()
        {
            string key = string.Format(CacheKey.JOOSHOW_SUPPLIER_CACHE,this.CompanyID);
            List<SupplierEntity> listResult = CacheHelper.Get(key) as List<SupplierEntity>;
            if (!listResult.IsNullOrEmpty())
            {
                return listResult;
            }
            SupplierEntity entity = new SupplierEntity();
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID);
            listResult = this.Supplier.GetList(entity);
            if (!listResult.IsNullOrEmpty())
            {
                CacheHelper.Insert(key, listResult);
            }
            return listResult;

        }

        /// <summary>
        /// 获得所有供应商信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<SupplierEntity> GetList(SupplierEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID);
            if (!entity.SupName.IsEmpty())
            {
                entity.And("SupName", ECondition.Like, "%"+entity.SupName+"%");
            }
            if (!entity.SupNum.IsEmpty())
            {
                entity.And("SupNum", ECondition.Like, "%" + entity.SupNum + "%");
            }
            if (entity.SupType > 0)
            {
                entity.And(a => a.SupType == entity.SupType);
            }
            if (!entity.Phone.IsEmpty())
            {
                entity.And("Phone",ECondition.Like,"%"+entity.Phone+"%");
            }
            int rowCount = 0;
            List<SupplierEntity> listResult = this.Supplier.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            
            return listResult;

        }

        /// <summary>
        /// 添加供应商
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(SupplierEntity entity)
        {
            string key = string.Format(CacheKey.JOOSHOW_SUPPLIER_CACHE, this.CompanyID);

            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.SupNum = entity.SupNum.IsEmpty() ? new TNumProvider(this.CompanyID).GetSwiftNum(typeof(SupplierEntity),5) : entity.SupNum;
            entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.SnNum;

            entity.IncludeAll();
            int line = this.Supplier.Add(entity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<string> list)
        {
            string key = string.Format(CacheKey.JOOSHOW_SUPPLIER_CACHE, this.CompanyID);
            SupplierEntity entity = new SupplierEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where("SnNum",ECondition.In,list.ToArray());
            entity.And(a=>a.CompanyID==this.CompanyID);
            int line = this.Supplier.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }

        /// <summary>
        /// 根据供应商编号获得供应商信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public SupplierEntity GetSupplier(string SnNum)
        {
            List<SupplierEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return null;
            }
            SupplierEntity entity = listSource.SingleOrDefault(item => item.SnNum == SnNum);
            return entity;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(SupplierEntity entity)
        {
            string key = string.Format(CacheKey.JOOSHOW_SUPPLIER_CACHE, this.CompanyID);
            entity.Include(a => new { a.SupNum, a.SupName, a.SupType, a.Email, a.Phone, a.Fax, a.ContactName, a.Address, a.Description });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==entity.CompanyID);
            int line = this.Supplier.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }

        /// <summary>
        /// 供应商搜索关键字
        /// </summary>
        /// <param name="Keyword"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<SupplierEntity> SearchSupplier(string Keyword,int PageSize)
        {
            List<SupplierEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return null;
            }
            List<SupplierEntity> listResult = listSource.Where(item => item.SupNum.Contains(Keyword) || item.SupName.Contains(Keyword) || item.Phone.Contains(Keyword)).Skip(0).Take(PageSize).ToList();

            return listResult;
        }

    }
}
