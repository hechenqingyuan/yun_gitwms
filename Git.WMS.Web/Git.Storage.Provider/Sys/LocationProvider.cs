/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-27 20:36:43
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-27 20:36:43       情缘
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
using System.Transactions;
using Git.Framework.Cache;
using Git.Storage.Provider.Base;

namespace Git.Storage.Provider.Sys
{
    public partial class LocationProvider : DataFactory
    {
        public LocationProvider(string CompanyID)
        {
            this.CompanyID = CompanyID;
        }

        /// <summary>
        /// 初始化某个仓库的库位
        /// </summary>
        /// <param name="StorageNum"></param>
        /// <param name="CreateUser"></param>
        /// <returns></returns>
        public int Init(string StorageNum,string CreateUser)
        {
            string Key = string.Format(CacheKey.JOOSHOW_LOCATION_CACHE, this.CompanyID);

            int line = 0;
            LocationEntity entity = new LocationEntity();
            entity.Where(item => item.StorageNum == StorageNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            if (this.Location.GetCount(entity) == 0)
            {
                StorageProvider storageProvider = new StorageProvider(this.CompanyID);
                StorageEntity storage = storageProvider.GetSingleByNum(StorageNum);
                if (storage != null)
                {
                    LocationEntity location = new LocationEntity();
                    location.LocalNum = ConvertHelper.NewGuid();
                    location.LocalBarCode = new SequenceProvider(this.CompanyID).GetSequence(typeof(LocationEntity));
                    location.LocalName = "默认待入库位";
                    location.LocalType = (int)ELocalType.WaitIn;
                    location.StorageNum = storage.SnNum;
                    location.StorageType = storage.StorageType;
                    location.CreateUser = CreateUser;
                    location.CreateTime = DateTime.Now;
                    location.IsDefault = (int)EBool.Yes;
                    location.IsDelete = (int)EIsDelete.NotDelete;
                    location.IsForbid = (int)EBool.No;
                    location.CompanyID = this.CompanyID;
                    location.IncludeAll();
                    line += this.Location.Add(location);


                    location = new LocationEntity();
                    location.LocalNum = ConvertHelper.NewGuid();
                    location.LocalBarCode = new SequenceProvider(this.CompanyID).GetSequence(typeof(LocationEntity));
                    location.LocalName = "默认正式库位";
                    location.LocalType = (int)ELocalType.Normal;
                    location.StorageNum = storage.SnNum;
                    location.StorageType = storage.StorageType;
                    location.CreateUser = CreateUser;
                    location.CreateTime = DateTime.Now;
                    location.IsDefault = (int)EBool.Yes;
                    location.IsDelete = (int)EIsDelete.NotDelete;
                    location.IsForbid = (int)EBool.No;
                    location.CompanyID = this.CompanyID;
                    location.IncludeAll();
                    line += this.Location.Add(location);

                    location = new LocationEntity();
                    location.LocalNum = ConvertHelper.NewGuid();
                    location.LocalBarCode = new SequenceProvider(this.CompanyID).GetSequence(typeof(LocationEntity));
                    location.LocalName = "默认待检库位";
                    location.LocalType = (int)ELocalType.WaitCheck;
                    location.StorageNum = storage.SnNum;
                    location.StorageType = storage.StorageType;
                    location.CreateUser = CreateUser;
                    location.CreateTime = DateTime.Now;
                    location.IsDefault = (int)EBool.Yes;
                    location.IsDelete = (int)EIsDelete.NotDelete;
                    location.IsForbid = (int)EBool.No;
                    location.CompanyID = this.CompanyID;
                    location.IncludeAll();
                    line += this.Location.Add(location);

                    location = new LocationEntity();
                    location.LocalNum = ConvertHelper.NewGuid();
                    location.LocalBarCode = new SequenceProvider(this.CompanyID).GetSequence(typeof(LocationEntity));
                    location.LocalName = "默认待出库位";
                    location.LocalType = (int)ELocalType.WaitOut;
                    location.StorageNum = storage.SnNum;
                    location.StorageType = storage.StorageType;
                    location.CreateUser = CreateUser;
                    location.CreateTime = DateTime.Now;
                    location.IsDefault = (int)EBool.Yes;
                    location.IsDelete = (int)EIsDelete.NotDelete;
                    location.IsForbid = (int)EBool.No;
                    location.CompanyID = this.CompanyID;
                    location.IncludeAll();
                    line += this.Location.Add(location);

                    location = new LocationEntity();
                    location.LocalNum = ConvertHelper.NewGuid();
                    location.LocalBarCode = new SequenceProvider(this.CompanyID).GetSequence(typeof(LocationEntity));
                    location.LocalName = "默认报损库位";
                    location.LocalType = (int)ELocalType.Bad;
                    location.StorageNum = storage.SnNum;
                    location.StorageType = storage.StorageType;
                    location.CreateUser = CreateUser;
                    location.CreateTime = DateTime.Now;
                    location.IsDefault = (int)EBool.Yes;
                    location.IsDelete = (int)EIsDelete.NotDelete;
                    location.IsForbid = (int)EBool.No;
                    location.CompanyID = this.CompanyID;
                    location.IncludeAll();
                    line += this.Location.Add(location);
                }
            }

            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }

            return line;
        }

        /// <summary>
        /// 新增库位
        /// 如果设置为默认库位，则其他的库位修改为非默认库位
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(LocationEntity entity)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                string Key = string.Format(CacheKey.JOOSHOW_LOCATION_CACHE, this.CompanyID);
                //设置默认值
                if (entity.IsDefault == (int)EBool.Yes)
                {
                    LocationEntity location = new LocationEntity();
                    location.IsDefault = (int)EBool.No;
                    location.IncludeIsDefault(true);
                    location.Where(a => a.LocalType == entity.LocalType)
                        .And(a => a.CompanyID == this.CompanyID)
                        ;
                    this.Location.Update(location);

                    LocationEntity temp = new LocationEntity();
                    temp.IsDefault = (int)EBool.No;
                    temp.IncludeIsDefault(true);
                    temp.Where(a => a.CompanyID == this.CompanyID);
                    this.Location.Update(temp);
                }

                //绑定仓库信息
                StorageProvider storageProvider = new StorageProvider(this.CompanyID);
                List<StorageEntity> listStorage = storageProvider.GetList();
                listStorage = listStorage.IsNull() ? new List<StorageEntity>() : listStorage;

                if (entity.StorageNum.IsEmpty())
                {
                    StorageEntity storage = listStorage.FirstOrDefault(a => a.IsDefault == (int)EBool.Yes);
                    if (storage != null)
                    {
                        entity.StorageNum = storage.StorageNum;
                        entity.StorageType = storage.StorageType;
                    }
                }
                else
                {
                    StorageEntity storage = listStorage.FirstOrDefault(a => a.StorageNum == entity.StorageNum);
                    if (storage != null)
                    {
                        entity.StorageType = storage.StorageType;
                    }
                }

                if (entity.UnitName.IsEmpty() && !entity.UnitNum.IsEmpty())
                {
                    MeasureProvider provider = new MeasureProvider(this.CompanyID);
                    List<MeasureEntity> listMeasure = provider.GetList();
                    listMeasure = listMeasure.IsNull() ? new List<MeasureEntity>() : listMeasure;
                    MeasureEntity measureEntity = listMeasure.FirstOrDefault(a => a.SN == entity.UnitNum);
                    entity.UnitName = measureEntity != null ? measureEntity.MeasureName : entity.UnitName;
                }
                entity.UnitNum = entity.UnitNum.IsEmpty() ? "" : entity.UnitNum;
                entity.UnitName = entity.UnitName.IsEmpty() ? "" : entity.UnitName;
                entity.IncludeAll();
                int line = this.Location.Add(entity);
                if (line > 0)
                {
                    CacheHelper.Remove(Key);
                }
                ts.Complete();
                return line;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(LocationEntity entity)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                string Key = string.Format(CacheKey.JOOSHOW_LOCATION_CACHE, this.CompanyID);

                if (entity.IsDefault == (int)EBool.Yes)
                {
                    LocationEntity location = new LocationEntity();
                    location.IsDefault = (int)EBool.No;
                    location.IncludeIsDefault(true);
                    location.Where(a => a.LocalType == entity.LocalType)
                        .And(a => a.CompanyID == this.CompanyID)
                        ;
                    this.Location.Update(location);

                    LocationEntity temp = new LocationEntity();
                    temp.IsDefault = (int)EBool.No;
                    temp.IncludeIsDefault(true);
                    temp.Where(a => a.CompanyID == this.CompanyID);
                    this.Location.Update(temp);
                }

                //绑定仓库信息
                StorageProvider storageProvider = new StorageProvider(this.CompanyID);
                List<StorageEntity> listStorage = storageProvider.GetList();
                listStorage = listStorage.IsNull() ? new List<StorageEntity>() : listStorage;
                if (entity.StorageNum.IsEmpty())
                {
                    StorageEntity storage = listStorage.FirstOrDefault(a => a.IsDefault == (int)EBool.Yes);
                    if (storage != null)
                    {
                        entity.StorageNum = storage.StorageNum;
                        entity.StorageType = storage.StorageType;
                    }
                }
                else
                {
                    StorageEntity storage = listStorage.FirstOrDefault(a => a.StorageNum == entity.StorageNum);
                    if (storage != null)
                    {
                        entity.StorageType = storage.StorageType;
                    }
                }
                if (entity.UnitName.IsEmpty() && !entity.UnitNum.IsEmpty())
                {
                    MeasureProvider provider = new MeasureProvider(this.CompanyID);
                    List<MeasureEntity> listMeasure = provider.GetList();
                    listMeasure = listMeasure.IsNull() ? new List<MeasureEntity>() : listMeasure;
                    MeasureEntity measureEntity = listMeasure.FirstOrDefault(a => a.SN == entity.UnitNum);
                    entity.UnitName = measureEntity != null ? measureEntity.MeasureName : entity.UnitName;
                }
                entity.UnitNum = entity.UnitNum.IsEmpty() ? "" : entity.UnitNum;
                entity.UnitName = entity.UnitName.IsEmpty() ? "" : entity.UnitName;

                entity.Include(a => new { a.LocalBarCode, a.LocalName, a.StorageNum, a.StorageType, a.LocalType, a.Rack, a.Length, a.Width, a.Height, a.X, a.Y, a.Z, a.UnitNum, a.UnitName, a.Remark, a.IsForbid, a.IsDefault });
                entity.Where(a => a.LocalNum == entity.LocalNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                int line = this.Location.Update(entity);
                if (line > 0)
                {
                    CacheHelper.Remove(Key);
                }
                ts.Complete();
                return line;
            }
        }

        /// <summary>
        /// 获得所有的库位
        /// </summary>
        /// <returns></returns>
        public List<LocationEntity> GetList()
        {
            string Key = string.Format(CacheKey.JOOSHOW_LOCATION_CACHE, this.CompanyID);
            List<LocationEntity> listResult = CacheHelper.Get<List<LocationEntity>>(Key);
            if (!listResult.IsNullOrEmpty())
            {
                return listResult;
            }
            LocationEntity entity = new LocationEntity();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID);
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            listResult = this.Location.GetList(entity);

            if (!listResult.IsNullOrEmpty())
            {
                StorageProvider storageProvider = new StorageProvider(this.CompanyID);
                List<StorageEntity> listStorage = storageProvider.GetList();
                listStorage = listStorage.IsNull() ? new List<StorageEntity>() : listStorage;
                foreach (LocationEntity item in listResult)
                {
                    StorageEntity storage = listStorage.FirstOrDefault(a => a.SnNum == item.StorageNum);
                    if (storage != null)
                    {
                        item.StorageName = storage.StorageName;
                    }
                }
                CacheHelper.Insert(Key, listResult);
            }
            return listResult;
        }

        /// <summary>
        /// 根据仓库获得所有的库位
        /// </summary>
        /// <returns></returns>
        public List<LocationEntity> GetList(string StorageNum)
        {
            List<LocationEntity> listResult = GetList();
            if (!listResult.IsNullOrEmpty())
            {
                return listResult.Where(a => a.StorageNum == StorageNum && a.CompanyID==this.CompanyID).ToList();
            }
            return null;
        }

        /// <summary>
        /// 根据库存编码查询仓库信息
        /// </summary>
        /// <param name="storageNum"></param>
        /// <returns></returns>
        public LocationEntity GetSingleByNum(string LocalNum)
        {
            LocationEntity entity = new LocationEntity();
            entity.IncludeAll();
            entity.Where(a => a.LocalNum == LocalNum)
                .And(a=>a.IsDelete==(int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID);
            entity = this.Location.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<LocationEntity> GetList(LocationEntity entity, ref PageInfo pageInfo)
        {
            List<LocationEntity> listSource = GetList();
            if (listSource.IsNull())
            {
                return null;
            }
            List<LocationEntity> listResult = listSource;
            
            if (!entity.LocalName.IsEmpty())
            {
                listResult = listResult.Where(a => a.LocalName.Contains(entity.LocalName) || a.LocalBarCode.Contains(entity.LocalName)).ToList();
            }
            if (!entity.LocalBarCode.IsEmpty())
            {
                listResult = listResult.Where(a => a.LocalName.Contains(entity.LocalBarCode) || a.LocalBarCode.Contains(entity.LocalBarCode)).ToList();
            }
            if (!entity.StorageNum.IsEmpty())
            {
                listResult = listResult.Where(a => a.StorageNum.Contains(entity.StorageNum)).ToList();
            }
            if (entity.StorageType > 0)
            {
                listResult = listResult.Where(a => a.StorageType==entity.StorageType).ToList();
            }
            if (entity.IsForbid>-1)
            {
                listResult = listResult.Where(a => a.IsForbid == entity.IsForbid).ToList();
            }
            if (entity.IsDefault>-1)
            {
                listResult = listResult.Where(a => a.IsDefault == entity.IsDefault).ToList();
            }
            if (!entity.ListLocalType.IsNullOrEmpty())
            {
                listResult = listResult.Where(a => entity.ListLocalType.Contains(a.LocalType)).ToList();
            }
            int rowCount = 0;
            rowCount = listResult.Count();
            pageInfo.RowCount = rowCount;
            return listResult.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
        }

        /// <summary>
        /// 删除库位信息
        /// </summary>
        /// <param name="storageNum"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<string> list)
        {
            string Key = string.Format(CacheKey.JOOSHOW_LOCATION_CACHE, this.CompanyID);

            LocationEntity entity = new LocationEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where("LocalNum", ECondition.In, list.ToArray());
            entity.And(a => a.CompanyID == this.CompanyID);
            int line = this.Location.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 设置为默认
        /// </summary>
        /// <param name="LocalNum"></param>
        /// <returns></returns>
        public int SetDefault(string LocalNum)
        {
            string Key = string.Format(CacheKey.JOOSHOW_LOCATION_CACHE, this.CompanyID);

            LocationEntity location = GetSingleByNum(LocalNum);
            location = new LocationEntity();
            location.IsDefault = (int)EBool.No;
            location.IncludeIsDefault(true);
            location.And(a => a.CompanyID == this.CompanyID);
            this.Location.Update(location);

            LocationEntity entity = new LocationEntity();
            entity.IsDefault = (int)EBool.Yes;
            entity.IncludeIsDefault(true);
            entity.Where(a => a.LocalNum == LocalNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.Location.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 设置禁用和启用
        /// </summary>
        /// <returns></returns>
        public int SetForbid(string LocalNum,EBool IsForbid)
        {
            string Key = string.Format(CacheKey.JOOSHOW_LOCATION_CACHE, this.CompanyID);

            LocationEntity entity = new LocationEntity();
            entity.IsForbid = (int)IsForbid;
            entity.IncludeIsForbid(true);
            entity.Where(a => a.LocalNum == LocalNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.Location.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

    }
}
