/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-12 19:55:17
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-12 19:55:17       情缘
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Finance;
using Git.Storage.Entity.Sys;
using Git.Storage.Provider.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider.Finance
{
    public partial class FinanceBillProvider : DataFactory
    {
        public FinanceBillProvider(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 新增应收应付
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(FinanceBillEntity entity)
        {
            entity.SnNum = ConvertHelper.NewGuid();
            entity.BillNum = entity.BillNum.IsEmpty() ? DateTime.Now.ToString("yyyyMMdd") + new TNumProvider(this.CompanyID).GetSwiftNumByDay(typeof(FinanceBillEntity), 4) : entity.BillNum;
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.Status = (int)EFinanceStatus.Wait;
            entity.CompanyID = this.CompanyID;
            if (entity.CateNum.IsNotEmpty())
            {
                List<FinanceCateEntity> listCate = new FinanceCateProvider(this.CompanyID).GetList();
                if (!listCate.IsNullOrEmpty())
                {
                    FinanceCateEntity cate = listCate.FirstOrDefault(a => a.SnNum == entity.CateNum);
                    entity.CateName = cate != null ? cate.CateName : string.Empty;
                }
            }
            entity.IncludeAll();
            int line = this.FinanceBill.Add(entity);
            return line;
        }

        /// <summary>
        /// 编辑财务应收应付
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Edit(FinanceBillEntity entity)
        {
            if (entity.CateNum.IsNotEmpty())
            {
                List<FinanceCateEntity> listCate = new FinanceCateProvider(this.CompanyID).GetList();
                if (!listCate.IsNullOrEmpty())
                {
                    FinanceCateEntity cate = listCate.FirstOrDefault(a => a.SnNum == entity.CateNum);
                    entity.CateName = cate != null ? cate.CateName : string.Empty;
                }
            }
            entity.Include(a => new { a.CateNum, a.CateName, a.BillType, a.FromNum, a.FromName, a.ToNum, a.ToName, a.Amount, a.PrePayCount, a.PrePayRate, a.RealPayCount, a.LastTime, a.Title, a.ContractSn, a.ContractNum, a.Remark });
            entity.Where(a => a.SnNum == entity.SnNum).And(a => a.CompanyID == this.CompanyID);
            int line = this.FinanceBill.Update(entity);
            return line;
        }

        /// <summary>
        /// 删除应收应付
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public int Delete(string SnNum)
        {
            FinanceBillEntity entity = new FinanceBillEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(a => a.SnNum == SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.FinanceBill.Update(entity);
            return line;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<string> list)
        {
            FinanceBillEntity entity = new FinanceBillEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where<FinanceBillEntity>("SnNum", ECondition.In, list.ToArray())
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.FinanceBill.Update(entity);
            return line;
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="SnNum"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int Audite(string SnNum, int Status)
        {
            FinanceBillEntity entity = new FinanceBillEntity();
            entity.Status = Status;
            entity.IncludeStatus(true);
            entity.Where<FinanceBillEntity>(a => a.SnNum == SnNum)
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.FinanceBill.Update(entity);
            return line;
        }

        /// <summary>
        /// 查询财务应收应付
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public FinanceBillEntity GetSingle(string SnNum)
        {
            FinanceBillEntity entity = new FinanceBillEntity();
            entity.IncludeAll();
            entity.Where(a => a.SnNum == SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                ;

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            entity = this.FinanceBill.GetSingle(entity);
            entity.LeavAmount = entity.Amount - entity.RealPayAmount;
            return entity;
        }

        /// <summary>
        /// 查询财务应收应付分页列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<FinanceBillEntity> GetList(FinanceBillEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            if (entity.BillNum.IsNotEmpty())
            {
                entity.Where("BillNum", ECondition.Like, "%" + entity.BillNum + "%");
            }
            if (entity.BillType > 0)
            {
                entity.And(a => a.BillType == entity.BillType);
            }
            if (entity.CateNum.IsNotEmpty())
            {
                entity.And(a => a.CateNum == entity.CateNum);
            }
            if (entity.Title.IsNotEmpty())
            {
                entity.And("Title", ECondition.Like, "%" + entity.Title + "%");
            }
            if (entity.ContractNum.IsNotEmpty())
            {
                entity.And("ContractNum", ECondition.Like, "%" + entity.ContractNum + "%");
            }
            if (entity.Status > 0)
            {
                entity.And(a => a.Status == entity.Status);
            }
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime dateTime = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.AddDays(-1));
                entity.And(a => a.CreateTime >= dateTime);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime dateTime = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).AddDays(1).Date;
                entity.And(a => a.CreateTime < dateTime);
            }
            if (entity.FromNum.IsNotEmpty())
            {
                entity.And(item => item.FromNum == entity.FromNum);
            }
            if (entity.FromName.IsNotEmpty())
            {
                entity.And("FromName", ECondition.Like, "%" + entity.FromName + "%");
            }
            if (entity.ToNum.IsNotEmpty())
            {
                entity.And(item => item.ToNum == entity.ToNum);
            }
            if (entity.ToName.IsNotEmpty())
            {
                entity.And("ToName", ECondition.Like, "%" + entity.ToName + "%");
            }
            int rowCount = 0;
            List<FinanceBillEntity> listResult = this.FinanceBill.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            if (!listResult.IsNullOrEmpty())
            {
                listResult.ForEach(a =>
                {
                    a.LeavAmount = a.Amount - a.RealPayAmount;
                });
            }
            return listResult;
        }
    }
}
