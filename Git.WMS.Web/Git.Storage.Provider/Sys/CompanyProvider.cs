/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-04 10:26:43
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-04 10:26:43       情缘
*********************************************************************************/

using Git.Storage.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common.Enum;
using System.Transactions;
using Git.Storage.Provider.Base;

namespace Git.Storage.Provider.Sys
{
    public partial class CompanyProvider:DataFactory
    {
        public CompanyProvider() { }

        /// <summary>
        /// 新增公司
        /// </summary>
        /// <returns></returns>
        public int Add(CompanyEntity entity)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.CompanyID = entity.CompanyID.IsEmpty() ? ConvertHelper.NewGuid() : entity.CompanyID;
                entity.CompanyNum = entity.CompanyNum.IsEmpty() ? new TNumProvider(this.CompanyID).GetSwiftNum(typeof(CompanyEntity), 5) : entity.CompanyNum;
                entity.IncludeAll();
                line = this.Company.Add(entity);
                if (line > 0)
                {
                    SequenceProvider provider = new SequenceProvider(entity.CompanyID);
                    provider.Init();
                }
                ts.Complete();
                return line;
            }
        }

        /// <summary>
        /// 修改公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(CompanyEntity entity)
        {
            int line = 0;
            entity.Include(a => new {a .CompanyName });
            entity.Where(a => a.CompanyID == entity.CompanyID);
            line = this.Company.Update(entity);
            return line;
        }

        /// <summary>
        /// 查询公司
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public CompanyEntity GetSingle(string CompanyID)
        {
            CompanyEntity entity = new CompanyEntity();
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .AndBegin<CompanyEntity>()
                .And(a => a.CompanyID == CompanyID)
                .Or(a => a.CompanyNum == CompanyID)
                .End();

            entity = this.Company.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public int Delete(string CompanyID)
        {
            CompanyEntity entity = new CompanyEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.Where(a => a.CompanyID == CompanyID);
            int line = this.Company.Update(entity);
            return line;
        }

        /// <summary>
        /// 查询公司信息分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<CompanyEntity> GetList(CompanyEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete);
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            int rowCount = 0;
            List<CompanyEntity> listResult = this.Company.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }
    }
}
