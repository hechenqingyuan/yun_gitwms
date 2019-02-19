/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-15 21:57:30
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-15 21:57:30       情缘
*********************************************************************************/

using Git.Framework.Cache;
using Git.Framework.Log;
using Git.Storage.Entity.Storage;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Framework.ORM;
using Git.Storage.Common.Enum;

namespace Git.Storage.Provider.Sys
{
    public partial class EquipmentProvider : DataFactory
    {
        private readonly Log log = Log.Instance(typeof(EquipmentProvider));

        public EquipmentProvider() { }

        public EquipmentProvider(string _CopanyID)
        {
            this.CompanyID = _CopanyID;
        }

        /// <summary>
        ///  获得所有设备信息
        /// </summary>
        /// <returns></returns>
        public List<EquipmentEntity> GetList()
        {
            string key = string.Format(CacheKey.JOOSHOW_EQUIPMENT_CACHE, this.CompanyID);
            List<EquipmentEntity> listResult = CacheHelper.Get(key) as List<EquipmentEntity>;
            if (!listResult.IsNullOrEmpty())
            {
                return listResult;
            }
            EquipmentEntity entity = new EquipmentEntity();
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID);
            listResult = this.Equipment.GetList(entity);
            if (!listResult.IsNullOrEmpty())
            {
                CacheHelper.Insert(key, listResult);
            }
            return listResult;
        }

        /// <summary>
        /// 分页查询设备信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<EquipmentEntity> GetList(EquipmentEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID);
            if (!entity.EquipmentName.IsEmpty())
            {
                entity.And("EquipmentName",ECondition.Like,"%"+entity.EquipmentName+"%");
            }
            if (!entity.EquipmentNum.IsEmpty())
            {
                entity.And("EquipmentNum", ECondition.Like, "%" + entity.EquipmentNum + "%");
            }
            if (!entity.Remark.IsEmpty())
            {
                entity.And("Remark", ECondition.Like, "%" + entity.Remark + "%");
            }
            if (entity.Status > 0)
            {
                entity.And(a => a.Status == entity.Status);
            }
            if (entity.CreateUser.IsNotEmpty())
            {
                entity.And(item=>item.CreateUser==entity.CreateUser);
            }
            int rowCount = 0;
            List<EquipmentEntity> listResult = this.Equipment.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }


        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddEquipment(EquipmentEntity entity)
        {
            string key = string.Format(CacheKey.JOOSHOW_EQUIPMENT_CACHE, this.CompanyID);
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.IncludeAll();
            int line = this.Equipment.Add(entity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }

        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="snNum"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<string> list)
        {
            string key = string.Format(CacheKey.JOOSHOW_EQUIPMENT_CACHE, this.CompanyID);

            EquipmentEntity entity = new EquipmentEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where("SnNum", ECondition.In, list.ToArray());
            entity.And(a => a.CompanyID == this.CompanyID);
            int line = this.Equipment.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }

        /// <summary>
        /// 根据设备编号获得设备信息
        /// </summary>
        /// <param name="snNum"></param>
        /// <returns></returns>
        public EquipmentEntity GetEquipment(string snNum)
        {
            List<EquipmentEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return null;
            }
            EquipmentEntity entity = listSource.SingleOrDefault(item => item.SnNum == snNum);
            return entity;
        }

        /// <summary>
        /// 是否授权
        /// </summary>
        /// <param name="SnNum"></param>
        /// <param name="IsImpower"></param>
        /// <returns></returns>
        public int Authorize(string SnNum, int IsImpower,string Flag)
        {
            string key = string.Format(CacheKey.JOOSHOW_EQUIPMENT_CACHE, this.CompanyID);
            EquipmentEntity entity = new EquipmentEntity();
            entity.IsImpower=IsImpower;
            entity.Flag = Flag;
            entity.IncludeIsImpower(true);
            entity.IncludeFlag(true);
            entity.Where(a => a.SnNum == SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.Equipment.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }

        /// <summary>
        /// 修改设备信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Edit(EquipmentEntity entity )
        {
            string key = string.Format(CacheKey.JOOSHOW_EQUIPMENT_CACHE, this.CompanyID);
            entity.Include(a => new { a.EquipmentName, a.Status, a.Remark,a.Flag,a.IsImpower });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.Equipment.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }
    }
}
