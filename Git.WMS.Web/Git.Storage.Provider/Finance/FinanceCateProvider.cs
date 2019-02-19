/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-12 19:55:03
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-12 19:55:03       情缘
*********************************************************************************/

using Git.Framework.Cache;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Finance;
using Git.Storage.Provider.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider.Finance
{
    public partial class FinanceCateProvider : DataFactory
    {
        public FinanceCateProvider(string _CompanyID) 
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 添加财务类别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(FinanceCateEntity entity)
        {
            string Key = string.Format(CacheKey.JOOSHOW_FINANCECATE_CACHE, this.CompanyID);
            entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid():entity.SnNum;
            entity.CateNum = entity.CateNum.IsEmpty() ?
                new SequenceProvider(this.CompanyID).GetSequence(typeof(FinanceCateEntity)) : entity.CateNum;
            entity.IncludeAll();
            int line = this.FinanceCate.Add(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 根据财务类别删除删除
        /// </summary>
        /// <param name="cateNum"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<string> list)
        {
            string Key = string.Format(CacheKey.JOOSHOW_FINANCECATE_CACHE, this.CompanyID);
            FinanceCateEntity entity = new FinanceCateEntity();
            entity.Where("SnNum", ECondition.In, list.ToArray());
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            int line = this.FinanceCate.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 修改财务类别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(FinanceCateEntity entity)
        {
            string Key = string.Format(CacheKey.JOOSHOW_FINANCECATE_CACHE, this.CompanyID);
            entity.IncludeCateName(true)
                .Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID)
                ;
            int line = this.FinanceCate.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(Key);
            }
            return line;
        }

        /// <summary>
        /// 根据财务类别编号查询
        /// </summary>
        /// <param name="cateNum"></param>
        /// <returns></returns>
        public FinanceCateEntity GetSingle(string SnNum)
        {
            List<FinanceCateEntity> list = GetList();
            if (!list.IsNullOrEmpty())
            {
                return list.FirstOrDefault(a => a.SnNum == SnNum);
            }
            return null;
        }

        /// <summary>
        /// 查询所有的财务类别
        /// </summary>
        /// <returns></returns>
        public List<FinanceCateEntity> GetList()
        {
            string Key = string.Format(CacheKey.JOOSHOW_FINANCECATE_CACHE, this.CompanyID);

            List<FinanceCateEntity> list = CacheHelper.Get(Key) as List<FinanceCateEntity>;
            if (!list.IsNullOrEmpty())
            {
                return list;
            }
            FinanceCateEntity entity = new FinanceCateEntity();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID);
            list = this.FinanceCate.GetList(entity);
            if (!list.IsNullOrEmpty())
            {
                CacheHelper.Insert(Key, list);
            }
            return list;
        }

        /// <summary>
        /// 查询财务类别分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<FinanceCateEntity> GetList(FinanceCateEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID);
            if (!entity.CateNum.IsEmpty())
            {
                entity.And("CateNum", ECondition.Like, "%" + entity.CateNum + "%");
            }
            if (!entity.CateName.IsEmpty())
            {
                entity.And("CateName", ECondition.Like, "%" + entity.CateName + "%");
            }
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            int rowCount = 0;
            List<FinanceCateEntity> list = this.FinanceCate.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return list;
        }
    }
}
