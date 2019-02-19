/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2014-06-24 21:20:16
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2014-06-24 21:20:16       情缘
*********************************************************************************/

using Git.Framework.Cache;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Framework.Log;
using Git.Storage.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Storage.Entity.Storage;
using Git.Storage.Provider.Sys;

namespace Git.Storage.Provider.Base
{
    public partial class MeasureProvider : DataFactory
    {
        public MeasureProvider() { }

        public MeasureProvider(string _CompanyID) 
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 添加计量单位
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddMeasure(MeasureEntity entity)
        {
            string Key = string.Format(CacheKey.JOOSHOW_MEASURE_CACHE,entity.CompanyID);
            entity.IncludeAll();
            int line = this.Measure.Add(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 获得所有的计量单位
        /// </summary>
        /// <returns></returns>
        public List<MeasureEntity> GetList()
        {
            string Key = string.Format(CacheKey.JOOSHOW_MEASURE_CACHE, this.CompanyID);
            List<MeasureEntity> listResult = CacheHelper.Get(Key) as List<MeasureEntity>;
            if (!listResult.IsNullOrEmpty())
            {
                return listResult;
            }
            MeasureEntity entity = new MeasureEntity();
            entity.IncludeAll();
            entity.Where(a => a.CompanyID == this.CompanyID); 
            listResult = this.Measure.GetList(entity);
            if (!listResult.IsNullOrEmpty())
            {
                CacheHelper.Insert(Key, listResult);
            }
            return listResult;
        }

        /// <summary>
        /// 根据计量单位删除
        /// </summary>
        /// <param name="measureNum"></param>
        /// <returns></returns>
        public int DeleteMeasure(IEnumerable<string> list,string CompanyID)
        {
            string Key = string.Format(CacheKey.JOOSHOW_MEASURE_CACHE, CompanyID);
            MeasureEntity entity = new MeasureEntity();
            entity.Where("SN", ECondition.In, list.ToArray());
            entity.And(a => a.CompanyID == CompanyID);
            int line = this.Measure.Delete(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 编辑计量单位
        /// </summary>
        /// <param name="measureNum"></param>
        /// <param name="measureName"></param>
        /// <returns></returns>
        public int EditMeasure(MeasureEntity entity)
        {
            string Key = string.Format(CacheKey.JOOSHOW_MEASURE_CACHE, CompanyID);
            entity.MeasureName = entity.MeasureName;
            entity.IncludeMeasureName(true);
            entity.Where(a => a.SN == entity.SN);
            int line = this.Measure.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 根据计量单位编号获得计量单位
        /// </summary>
        /// <param name="measureNum"></param>
        /// <returns></returns>
        public MeasureEntity GetMeasure(string SN)
        {
            List<MeasureEntity> listResult = GetList();
            if (!listResult.IsNullOrEmpty())
            {
                return listResult.FirstOrDefault(a => a.SN == SN);
            }
            return null;
        }

        /// <summary>
        /// 查询分页计量单位
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<MeasureEntity> GetList(MeasureEntity entity, ref PageInfo pageInfo)
        {
            List<MeasureEntity> listResult = GetList();
            if (listResult.IsNull())
            {
                return null;
            }

            int rowCount = 0;
            if (!entity.MeasureNum.IsEmpty())
            {
                listResult = listResult.Where(a => a.MeasureNum.Contains(entity.MeasureNum)).ToList();
            }
            if (!entity.MeasureName.IsEmpty())
            {
                listResult = listResult.Where(a => a.MeasureName.Contains(entity.MeasureName)).ToList();
            }
            rowCount = listResult.Count;
            pageInfo.RowCount = rowCount;
            return listResult.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
        }

        /// <summary>
        /// 查询某个产品的包装情况
        /// </summary>
        /// <param name="ProductNum"></param>
        /// <returns></returns>
        public List<MeasureRelEntity> GetMeasureRel(string ProductNum)
        {
            MeasureRelEntity entity = new MeasureRelEntity();
            entity.IncludeAll();
            entity.Where(item => item.CompanyID == this.CompanyID)
                .And(item => item.ProductNum == ProductNum);
            List<MeasureRelEntity> listResult = this.MeasureRel.GetList(entity);
            if (!listResult.IsNullOrEmpty())
            {
                foreach (MeasureRelEntity item in listResult)
                {
                    MeasureEntity measure = GetMeasure(item.MeasureSource);
                    item.SourceName = measure.IsNull() ? string.Empty : measure.MeasureName;

                    measure = GetMeasure(item.MeasureTarget);
                    item.TargetName = measure.IsNull() ? string.Empty : measure.MeasureName;
                }
            }
            return listResult;
        }

        /// <summary>
        /// 新增产品包装结构
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddMeasureRel(MeasureRelEntity entity)
        {
            int line = 0;

            MeasureRelEntity check = new MeasureRelEntity();
            check.IncludeAll();
            check.Where(item => item.CompanyID == this.CompanyID)
                .And(item => item.ProductNum == entity.ProductNum)
                .And(item => item.MeasureSource == entity.MeasureSource)
                .And(item => item.MeasureTarget == entity.MeasureTarget)
                ;

            check = this.MeasureRel.GetSingle(check);
            if (check != null)
            {
                check.Rate = entity.Rate;
                check.IncludeRate(true);
                check.Where(item => item.SN == check.SN)
                    .And(item => item.CompanyID == this.CompanyID)
                    ;
                line = this.MeasureRel.Update(check);
            }
            else
            {
                entity.SN = entity.SN.IsEmpty() ? ConvertHelper.NewGuid() : entity.SN;
                entity.CompanyID = this.CompanyID;
                entity.IncludeAll();
                line = this.MeasureRel.Add(entity);
            }
            return line;
        }

        /// <summary>
        /// 删除包装结构
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public int DeleteMeasureRel(string SnNum)
        {
            MeasureRelEntity entity = new MeasureRelEntity();
            entity.Where(async => async.SN == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            int line = this.MeasureRel.Delete(entity);
            return line;
        }

        /// <summary>
        /// 查询产品单位
        /// </summary>
        /// <param name="ProductNum"></param>
        /// <returns></returns>
        public List<MeasureEntity> GetList(string ProductNum)
        {
            List<MeasureEntity> listResult = new List<MeasureEntity>();

            ProductProvider proProvider = new ProductProvider(this.CompanyID);
            ProductEntity product = proProvider.GetProduct(ProductNum);
            if (product != null)
            {
                MeasureEntity entity = GetMeasure(product.UnitNum);
                if (entity != null)
                {
                    listResult.Add(entity);
                }
            }
            List<MeasureRelEntity> listSource = GetMeasureRel(ProductNum);
            if (!listSource.IsNullOrEmpty())
            {
                foreach (MeasureRelEntity item in listSource)
                {
                    if (!listResult.Exists(a => a.SN == item.MeasureTarget))
                    {
                        MeasureEntity entity = GetMeasure(item.MeasureTarget);
                        if (entity != null)
                        {
                            listResult.Add(entity);
                        }
                    }
                }
            }
            return listResult;
        }
    }
}
