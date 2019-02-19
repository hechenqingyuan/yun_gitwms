/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-17 9:02:55
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-17 9:02:55       情缘
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
    public partial class CustomerProvider : DataFactory
    {
        public CustomerProvider(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 分页查询客户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<CustomerEntity> GetCustomerList(CustomerEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID);
            if (!entity.CusNum.IsEmpty())
            {
                entity.And("CusNum", ECondition.Like, "%" + entity.CusNum + "%");
            }
            if (!entity.CusName.IsEmpty())
            {
                entity.And("CusName", ECondition.Like, "%" + entity.CusName + "%");
            }
            if (!entity.Phone.IsEmpty())
            {
                entity.And("Phone", ECondition.Like, "%" + entity.Phone + "%");
            }
            if (entity.CusType > 0)
            {
                entity.And(a=>a.CusType==entity.CusType);
            }
            int rowCount = 0;
            List<CustomerEntity> listResult = this.Customer.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 添加客户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddCustomer(CustomerEntity entity, List<CusAddressEntity> list)
        {
            string Key = string.Format(CacheKey.JOOSHOW_CUSADDRESS_CACHE,this.CompanyID);
            entity.IncludeAll();
            int line = this.Customer.Add(entity);
            if (!list.IsNullOrEmpty())
            {
                list.ForEach(a =>
                {
                    a.CustomerSN = entity.SnNum;
                    a.IncludeAll();
                });
                line += this.CusAddress.Add(list);
            }
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="cusNum"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<string> list)
        {
            string Key = string.Format(CacheKey.JOOSHOW_CUSADDRESS_CACHE, this.CompanyID);
            CustomerEntity entity = new CustomerEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where("SnNum",ECondition.In,list.ToArray());
            int line = this.Customer.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 根据客户编号获得客户信息
        /// </summary>
        /// <param name="cusNum"></param>
        /// <returns></returns>
        public CustomerEntity GetSingleCustomer(string SnNum)
        {
            CustomerEntity entity = new CustomerEntity();
            entity.IncludeAll();
            entity.Where(a => a.SnNum == SnNum);
            entity = this.Customer.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Update(CustomerEntity entity, List<CusAddressEntity> list)
        {
            string Key = string.Format(CacheKey.JOOSHOW_CUSADDRESS_CACHE, this.CompanyID);

            entity.IncludeCusName(true)
                .IncludeEmail(true)
                .IncludeFax(true)
                .IncludePhone(true)
                .IncludeRemark(true)
                .IncludeCusType(true)
                .IncludeCusNum(true)
                ;
            entity.Where(a => a.SnNum == entity.SnNum);
            int line = this.Customer.Update(entity);

            if (!list.IsNullOrEmpty())
            {
                CusAddressEntity addSource = new CusAddressEntity();
                addSource.IncludeAll();
                addSource.Where(a => a.CustomerSN == entity.SnNum)
                    .And(a=>a.IsDelete==(int)EIsDelete.NotDelete)
                    ;
                List<CusAddressEntity> listSource = this.CusAddress.GetList(addSource);
                listSource = listSource.IsNull() ? new List<CusAddressEntity>() : listSource;

                //处理未删除的
                foreach (CusAddressEntity item in list.Where(a=>a.IsDelete==(int)EIsDelete.NotDelete))
                {
                    if (listSource.Exists(a => a.SnNum == item.SnNum))
                    {
                        item.Include(a => new { a.Contact,a.Phone,a.Address,a.Remark});
                        item.Where(a => a.SnNum == item.SnNum);
                        line += this.CusAddress.Update(item);
                    }
                    else
                    {
                        item.IncludeAll();
                        item.CustomerSN = entity.SnNum;
                        item.CreateTime = DateTime.Now;
                        item.IsDelete = (int)EIsDelete.NotDelete;
                        item.CompanyID = this.CompanyID;
                        line += this.CusAddress.Add(item);
                    }
                }

                //处理删除的
                foreach (CusAddressEntity item in listSource)
                {
                    if (!list.Exists(a => a.SnNum == item.SnNum))
                    {
                        item.IsDelete = (int)EIsDelete.Deleted;
                        item.IncludeIsDelete(true);
                        item.Where(a => a.SnNum == item.SnNum);
                        line += this.CusAddress.Update(item);
                    }
                }
            }
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 获得所有的地址
        /// </summary>
        /// <param name="cusNum"></param>
        /// <returns></returns>
        public List<CusAddressEntity> GetAddressList(string CustomerSN)
        {
            CusAddressEntity entity = new CusAddressEntity();
            entity.IncludeAll();
            entity.Where<CusAddressEntity>(a => a.CustomerSN == CustomerSN)
                .And<CusAddressEntity>(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID);
            List<CusAddressEntity> listResult = this.CusAddress.GetList(entity);
            return listResult;
        }

        /// <summary>
        /// 根据收货单位编号获得地址信息
        /// </summary>
        /// <param name="snNum"></param>
        /// <returns></returns>
        public CusAddressEntity GetSingleAddress(string SnNum)
        {
            CusAddressEntity entity = new CusAddressEntity();
            entity.IncludeAll();
            entity.Where(a => a.SnNum == SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            entity = this.CusAddress.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="SnNum"></param>
        /// <param name="CustomerSN"></param>
        /// <returns></returns>
        public int DeleteAddress(string SnNum, string CustomerSN)
        {
            CusAddressEntity entity = new CusAddressEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(a=>a.SnNum==SnNum)
                .And(a=>a.CustomerSN==CustomerSN);
            int line = this.CusAddress.Update(entity);
            return line;
        }

        /// <summary>
        /// 查询客户地址分页列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<CusAddressEntity> GetList(CusAddressEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.Where(a => a.CompanyID == this.CompanyID)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete);

            CustomerEntity customer = new CustomerEntity();
            customer.Include(a => new { CusNum = a.CusNum, CusName = a.CusName, CusType = a.CusType, Fax = a.Fax, Email=a.Email });

            entity.Left<CustomerEntity>(customer, new Params<string, string>() { Item1 = "CustomerSN", Item2 = "SnNum" });
            customer.And(a => a.IsDelete == (int)EIsDelete.NotDelete);
            if (entity.CusType > 0)
            {
                customer.And(item=>item.CusType==entity.CusType);
            }
            if (!entity.Address.IsEmpty())
            {
                entity.And("Address", ECondition.Like, "%" + entity.Address + "%");
            }
            if (!entity.Phone.IsEmpty())
            {
                entity.And("Phone", ECondition.Like, "%" + entity.Phone + "%");
            }
            if (!entity.CusNum.IsEmpty())
            {
                customer.And("CusNum", ECondition.Like, "%" + entity.CusNum + "%");
            }
            if (!entity.CusName.IsEmpty())
            {
                customer.And("CusName", ECondition.Like, "%" + entity.CusName + "%");
            }

            int rowCount = 0;
            List<CusAddressEntity> listResult = this.CusAddress.GetList(entity,pageInfo.PageSize,pageInfo.PageIndex,out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 根据关键字搜索客户信息
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <param name="TopSize"></param>
        /// <returns></returns>
        public List<CusAddressEntity> SearchCustomer(string KeyWord,int TopSize)
        {
            CusAddressEntity entity = new CusAddressEntity();
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.Where(a => a.CompanyID == this.CompanyID);

            CustomerEntity customer = new CustomerEntity();
            customer.Include(a => new { CusNum = a.CusNum, CusName = a.CusName, CusType = a.CusType, Fax = a.Fax, Email = a.Email });

            entity.Left<CustomerEntity>(customer, new Params<string, string>() { Item1 = "CustomerSN", Item2 = "SnNum" });
            customer.And(a => a.IsDelete == (int)EIsDelete.NotDelete);

            PageInfo pageInfo = new PageInfo() { PageIndex = 1, PageSize = 10 };
            customer.AndBegin<CustomerEntity>()
                .Or<CustomerEntity>("CusNum", ECondition.Like, "%"+KeyWord+"%")
                .Or<CustomerEntity>("CusName", ECondition.Like, "%" + KeyWord + "%")
                .Or<CustomerEntity>("Phone", ECondition.Like, "%" + KeyWord + "%")
                .End();

            int rowCount = 0;
            List<CusAddressEntity> listResult = this.CusAddress.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }
    }
}
