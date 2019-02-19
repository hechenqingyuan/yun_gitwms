/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-27 12:35:55
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-27 12:35:55       情缘
*********************************************************************************/

using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Framework.Cache;
using System.Transactions;
using Git.Storage.Provider.Base;
using Git.Storage.Entity.Sys;

namespace Git.Storage.Provider.Sys
{
    public partial class StorageProvider : DataFactory
    {
        public StorageProvider(string CompanyID) 
        {
            this.CompanyID = CompanyID;
        }

        /// <summary>
        /// 注册仓库
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(StorageEntity entity)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                string Key = string.Format(CacheKey.JOOSHOW_STORAGE_CACHE, this.CompanyID);

                entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.SnNum;
                entity.StorageNum = entity.StorageNum.IsEmpty() ? new SequenceProvider(this.CompanyID).GetSequence(typeof(StorageEntity)):entity.StorageNum;

                //设置默认仓库
                if (entity.IsDefault == (int)EBool.Yes)
                {
                    StorageEntity temp = new StorageEntity();
                    temp.Where(a => a.IsDefault == (int)EBool.Yes)
                        .And(a => a.CompanyID == this.CompanyID);

                    if (this.Storage.GetCount(temp) > 0)
                    {
                        temp = new StorageEntity();
                        temp.IsDefault = (int)EBool.No;
                        temp.IncludeIsDefault(true);
                        temp.Where(a => a.IsDefault == (int)EBool.Yes)
                            .And(a => a.CompanyID == this.CompanyID)
                            ;
                        line+=this.Storage.Update(temp);
                    }
                }

                entity.IncludeAll();
                line += this.Storage.Add(entity);
                if (line > 0)
                {
                    //初始化库位信息
                    System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        LocationProvider locationProvider = new LocationProvider(this.CompanyID);
                        locationProvider.Init(entity.SnNum, entity.CreateUser);
                    });
                    CacheHelper.Remove(Key);
                }
                ts.Complete();
                return line;
            }
        }

        /// <summary>
        /// 查询所有的仓库数据信息
        /// </summary>
        /// <returns></returns>
        public List<StorageEntity> GetList()
        {
            string Key = string.Format(CacheKey.JOOSHOW_STORAGE_CACHE, this.CompanyID);
            List<StorageEntity> listResult = CacheHelper.Get<List<StorageEntity>>(Key);
            if (!listResult.IsNullOrEmpty())
            {
                return listResult;
            }
            StorageEntity entity = new StorageEntity();
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);

            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID);

            SysDepartEntity depart = new SysDepartEntity();
            depart.Include(item => new { DepartName=item.DepartName });
            entity.Left<SysDepartEntity>(depart, new Params<string, string>() { Item1 = "DepartNum", Item2 = "SnNum" });

            listResult = this.Storage.GetList(entity);
            if (!listResult.IsNullOrEmpty())
            {
                CacheHelper.Add(Key,listResult);
            }
            return listResult;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<StorageEntity> GetList(StorageEntity entity, ref PageInfo pageInfo)
        {
            List<StorageEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return null;
            }
            int rowCount = 0;
            List<StorageEntity> listResult = listSource;

            if (!entity.StorageNum.IsEmpty())
            {
                listResult = listResult.Where(a => a.StorageNum.Contains(entity.StorageNum)).ToList();
            }
            if (!entity.StorageName.IsEmpty())
            {
                listResult = listResult.Where(a => a.StorageName.Contains(entity.StorageName)).ToList();
            }
            if (entity.StorageType > 0)
            {
                listResult = listResult.Where(item => item.StorageType == entity.StorageType).ToList();
            }
            if (entity.IsDefault>-1)
            {
                listResult = listResult.Where(a => a.IsDefault == entity.IsDefault).ToList();
            }
            if (entity.IsForbid>-1)
            {
                listResult = listResult.Where(a => a.IsForbid == entity.IsForbid).ToList();
            }
            if (entity.Status > 0)
            {
                listResult = listResult.Where(a => a.Status == entity.Status).ToList();
            }
            if (!entity.Remark.IsEmpty())
            {
                listResult = listResult.Where(a => a.Remark.Contains(entity.Remark)).ToList();
            }
            if (entity.DepartNum.IsNotEmpty())
            {
                DepartProvider departProvider = new DepartProvider(this.CompanyID);
                List<SysDepartEntity> listDepart = departProvider.GetChildList(entity.DepartNum);
                listDepart = listDepart.IsNull() ? new List<SysDepartEntity>() : listDepart;
                listResult = listResult.Where(item => listDepart.Exists(depart => depart.SnNum == item.DepartNum)).ToList();
            }
            if (entity.CreateUser.IsNotEmpty())
            {
                listResult = listResult.Where(item => item.CreateUser == entity.CreateUser).ToList();
            }
            if (entity.Contact.IsNotEmpty())
            {
                listResult = listResult.Where(item => item.Contact.Contains(entity.Contact)).ToList();
            }
            if (entity.Address.IsNotEmpty())
            {
                listResult = listResult.Where(item => item.Address.Contains(entity.Address)).ToList();
            }
            if (entity.Phone.IsNotEmpty())
            {
                listResult = listResult.Where(item => item.Phone.Contains(entity.Phone)).ToList();
            }
            rowCount = listResult.Count();
            pageInfo.RowCount = rowCount;
            return listResult.Skip((pageInfo.PageIndex-1)*pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
        }

        /// <summary>
        /// 根据仓库编码查询仓库信息
        /// </summary>
        /// <param name="storageNum"></param>
        /// <returns></returns>
        public StorageEntity GetSingleByNum(string SnNum)
        {
            List<StorageEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return null;
            }

            return listSource.FirstOrDefault(item => item.SnNum == SnNum);
        }

        /// <summary>
        /// 删除仓库信息
        /// </summary>
        /// <param name="storageNum"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<string> list)
        {
            string Key = string.Format(CacheKey.JOOSHOW_STORAGE_CACHE, this.CompanyID);

            StorageEntity entity = new StorageEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where("SnNum",ECondition.In,list.ToArray());
            entity.And(a=>a.CompanyID==this.CompanyID);

            int line = this.Storage.Update(entity);
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
        public int Update(StorageEntity entity)
        {
            string Key = string.Format(CacheKey.JOOSHOW_STORAGE_CACHE, this.CompanyID);

            if (entity.IsDefault == (int)EBool.Yes)
            {
                StorageEntity temp = new StorageEntity();
                temp.Where(a => a.IsDefault == (int)EBool.Yes)
                    .And(a=>a.CompanyID==this.CompanyID);
                if (this.Storage.GetCount(temp) > 0)
                {
                    temp = new StorageEntity();
                    temp.IsDefault = (int)EBool.No;
                    temp.IncludeIsDefault(true);
                    temp.Where(a => a.IsDefault == (int)EBool.Yes)
                        .And(a=>a.CompanyID==this.CompanyID);
                    this.Storage.Update(temp);
                }
            }

            entity.Include(a => new { a.StorageName, a.StorageType, a.Length, a.Width, a.Height, a.Action, a.Remark, a.Status, a.IsDefault, a.IsForbid, a.Area, a.Contact, a.Address, a.Phone, a.DepartNum,a.LeaseTime });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;

            int line = this.Storage.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 设置某个仓库为默认仓库值
        /// </summary>
        /// <param name="sotrageNum"></param>
        /// <returns></returns>
        public int SetDefault(string SnNum)
        {
            string Key = string.Format(CacheKey.JOOSHOW_STORAGE_CACHE, this.CompanyID);

            StorageEntity entity = new StorageEntity();
            entity.IsDefault = (int)EBool.No;
            entity.IncludeIsDefault(true);
            entity.Where(a => a.CompanyID == this.CompanyID);
            int line = this.Storage.Update(entity);

            entity = new StorageEntity();
            entity.IsDefault = (int)EBool.Yes;
            entity.IncludeIsDefault(true);
            entity.Where(a => a.SnNum == SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            line += this.Storage.Update(entity);

            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 设置仓库的禁用和启用状态
        /// </summary>
        /// <param name="storageNum"></param>
        /// <param name="isForbid"></param>
        /// <returns></returns>
        public int SetForbid(string SnNum, EBool isForbid)
        {
            string Key = string.Format(CacheKey.JOOSHOW_STORAGE_CACHE, this.CompanyID);

            StorageEntity entity = new StorageEntity();
            entity.IsForbid = (int)isForbid;
            entity.IncludeIsForbid(true);
            entity.Where(a => a.SnNum == SnNum)
                .And(a=>a.CompanyID==this.CompanyID);

            int line = this.Storage.Update(entity);

            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

    }
}
