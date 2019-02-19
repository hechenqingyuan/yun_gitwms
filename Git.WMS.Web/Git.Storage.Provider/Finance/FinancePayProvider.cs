/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-12 19:55:32
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-12 19:55:32       情缘
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
using System.Transactions;

namespace Git.Storage.Provider.Finance
{
    public partial class FinancePayProvider : DataFactory
    {
        public FinancePayProvider(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 新增财务实收实付
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(FinancePayEntity entity)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                entity.SnNum = ConvertHelper.NewGuid();
                entity.PayNum = new SequenceProvider(this.CompanyID).GetSequence(typeof(FinancePayEntity));
                entity.CreateTime = DateTime.Now;
                entity.CompanyID = this.CompanyID;
                entity.IncludeAll();
                int line = this.FinancePay.Add(entity);


                FinancePayEntity pay = new FinancePayEntity();
                pay.IncludeAll();
                pay.Where(a => a.BillSnNum == entity.BillSnNum)
                    .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                List<FinancePayEntity> listResult = this.FinancePay.GetList(pay);
                if (!listResult.IsNullOrEmpty())
                {
                    int RealPayCount = listResult.Count();
                    double TotalAmount = listResult.Sum(a => a.Amount);

                    FinanceBillEntity bill = new FinanceBillEntity();
                    bill.IncludeAll();
                    bill.Where(a => a.SnNum == entity.BillSnNum)
                        .And(a => a.CompanyID == this.CompanyID)
                        ;
                    bill = this.FinanceBill.GetSingle(bill);

                    if (bill != null)
                    {
                        FinanceBillEntity finance = new FinanceBillEntity();
                        finance.RealPayCount = RealPayCount;
                        finance.RealPayAmount = TotalAmount;
                        if (TotalAmount == 0)
                        {
                            finance.Status = (int)EFinanceStatus.InProgress;
                        }
                        else if (TotalAmount > 0 && TotalAmount < bill.Amount)
                        {
                            finance.Status = (int)EFinanceStatus.PayPart;
                        }
                        else if (TotalAmount >= bill.Amount)
                        {
                            finance.Status = (int)EFinanceStatus.PayFull;
                        }
                        finance.Where(a => a.SnNum == bill.SnNum)
                            .And(a => a.CompanyID == this.CompanyID)
                            ;
                        finance.Include(a => new { a.RealPayAmount, a.RealPayCount, a.Status });
                        line += this.FinanceBill.Update(finance);
                    }
                }
                ts.Complete();

                return line;
            }
        }

        /// <summary>
        /// 编辑财务实收实付
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Edit(FinancePayEntity entity)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                entity.Include(a => new
                {
                    a.PayType,
                    a.BankName,
                    a.Amount,
                });
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                int line = this.FinancePay.Update(entity);

                FinancePayEntity pay = new FinancePayEntity();
                pay.IncludeAll();
                pay.Where(a => a.BillSnNum == entity.BillSnNum)
                    .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                List<FinancePayEntity> listResult = this.FinancePay.GetList(pay);
                if (!listResult.IsNullOrEmpty())
                {
                    int RealPayCount = listResult.Count();
                    double TotalAmount = listResult.Sum(a => a.Amount);

                    FinanceBillEntity bill = new FinanceBillEntity();
                    bill.IncludeAll();
                    bill.Where(a => a.SnNum == entity.BillSnNum)
                        .And(a => a.CompanyID == this.CompanyID)
                        ;
                    bill = this.FinanceBill.GetSingle(bill);

                    if (bill != null)
                    {
                        FinanceBillEntity finance = new FinanceBillEntity();
                        finance.RealPayCount = RealPayCount;

                        if (TotalAmount == 0)
                        {
                            finance.Status = (int)EFinanceStatus.InProgress;
                        }
                        else if (TotalAmount > 0 && TotalAmount < bill.Amount)
                        {
                            finance.Status = (int)EFinanceStatus.PayPart;
                        }
                        else if (TotalAmount >= bill.Amount)
                        {
                            finance.Status = (int)EFinanceStatus.PayFull;
                        }
                        finance.Where(a => a.SnNum == bill.SnNum)
                            .And(a => a.CompanyID == this.CompanyID)
                            ;
                    }
                }

                ts.Complete();

                return line;
            }
        }

        /// <summary>
        /// 删除实收实付
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public int Delete(string SnNum)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                FinancePayEntity entity = new FinancePayEntity();
                entity.SnNum = SnNum;
                entity.IncludeAll();
                entity.Where(a => a.SnNum == SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                entity = this.FinancePay.GetSingle(entity);

                FinancePayEntity finEntity = new FinancePayEntity();
                finEntity.IsDelete = (int)EIsDelete.Deleted;
                finEntity.IncludeIsDelete(true);
                finEntity.Where(a => a.SnNum == SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                int line = this.FinancePay.Update(finEntity);

                FinancePayEntity pay = new FinancePayEntity();
                pay.IncludeAll();
                pay.Where(a => a.BillSnNum == entity.BillSnNum)
                    .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                List<FinancePayEntity> listResult = this.FinancePay.GetList(pay);
                if (!listResult.IsNullOrEmpty())
                {
                    int RealPayCount = listResult.Count();
                    double TotalAmount = listResult.Sum(a => a.Amount);

                    FinanceBillEntity bill = new FinanceBillEntity();
                    bill.IncludeAll();
                    bill.Where(a => a.SnNum == entity.BillSnNum)
                        .And(a => a.CompanyID == this.CompanyID)
                        ;
                    bill = this.FinanceBill.GetSingle(bill);

                    if (bill != null)
                    {
                        FinanceBillEntity finance = new FinanceBillEntity();
                        finance.RealPayCount = RealPayCount;

                        if (TotalAmount == 0)
                        {
                            finance.Status = (int)EFinanceStatus.InProgress;
                        }
                        else if (TotalAmount > 0 && TotalAmount < bill.Amount)
                        {
                            finance.Status = (int)EFinanceStatus.PayPart;
                        }
                        else if (TotalAmount >= bill.Amount)
                        {
                            finance.Status = (int)EFinanceStatus.PayFull;
                        }
                        finance.Where(a => a.SnNum == bill.SnNum)
                            .And(a => a.CompanyID == this.CompanyID)
                            ;
                    }
                }

                ts.Complete();

                return line;
            }
        }

        /// <summary>
        /// 查询实收实付
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public FinancePayEntity GetSingle(string SnNum)
        {
            FinancePayEntity entity = new FinancePayEntity();
            entity.IncludeAll();
            entity.Where(a => a.SnNum == SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                ;
            entity = this.FinancePay.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 查询应收应付对应的实收实付
        /// </summary>
        /// <param name="BillSnNum"></param>
        /// <returns></returns>
        public List<FinancePayEntity> GetList(string BillSnNum)
        {
            FinancePayEntity entity = new FinancePayEntity();
            entity.IncludeAll();
            entity.Where(a => a.BillSnNum == BillSnNum)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            List<FinancePayEntity> listResult = this.FinancePay.GetList(entity);
            return listResult;
        }

        /// <summary>
        /// 查询分页 实收实付
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<FinancePayEntity> GetList(FinancePayEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                ;

            FinanceBillEntity bill = new FinanceBillEntity();
            bill.Include(a => new
            {
                Title = a.Title,
                FromName = a.FromName,
                ToName = a.ToName,
                TotalAmount = a.Amount,
                LastTime = a.LastTime,
                ContractSn = a.ContractSn,
                ContractNum = a.ContractNum,
                BillType = a.BillType,
                CateName = a.CateName,
                CateNum = a.CateNum,
            });

            entity.Left<FinanceBillEntity>(bill, new Params<string, string>() { Item1 = "BillSnNum", Item2 = "SnNum" });

            if (entity.PayType > 0)
            {
                entity.And(a => a.PayType == entity.PayType);
            }
            if (entity.BankName.IsNotEmpty())
            {
                entity.Where("BankName", ECondition.Like, "%" + entity.BankName + "%");
            }
            if (entity.PayNum.IsNotEmpty())
            {
                entity.Where("PayNum", ECondition.Like, "%" + entity.PayNum + "%");
            }
            if (entity.CateNum.IsNotEmpty())
            {
                bill.And(a => a.CateNum == entity.CateNum);
            }
            if (entity.Title.IsNotEmpty())
            {
                bill.And("Title", ECondition.Like, "%" + entity.Title + "%");
            }
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.AddDays(-7)).Date;
                entity.And(a => a.PayTime >= time);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).AddDays(1).Date;
                entity.And(a => a.PayTime < time);
            }
            int rowCount = 0;
            List<FinancePayEntity> listResult = this.FinancePay.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 查询门店的流水记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<FinancePayEntity> GeAgencyBilltList(FinancePayEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                ;

            FinanceBillEntity bill = new FinanceBillEntity();
            bill.Include(a => new
            {
                Title = a.Title,
                FromName = a.FromName,
                ToName = a.ToName,
                TotalAmount = a.Amount,
                LastTime = a.LastTime,
                ContractSn = a.ContractSn,
                ContractNum = a.ContractNum,
                BillType = a.BillType,
                CateName = a.CateName,
                CateNum = a.CateNum,
            });

            entity.Left<FinanceBillEntity>(bill, new Params<string, string>() { Item1 = "BillSnNum", Item2 = "SnNum" });

            
            if (entity.PayType > 0)
            {
                entity.And(a => a.PayType == entity.PayType);
            }
            if (entity.BankName.IsNotEmpty())
            {
                entity.Where("BankName", ECondition.Like, "%" + entity.BankName + "%");
            }
            if (entity.PayNum.IsNotEmpty())
            {
                entity.Where("PayNum", ECondition.Like, "%" + entity.PayNum + "%");
            }
            if (entity.CateNum.IsNotEmpty())
            {
                bill.And(a => a.CateNum == entity.CateNum);
            }
            if (entity.Title.IsNotEmpty())
            {
                bill.And("Title", ECondition.Like, "%" + entity.Title + "%");
            }
            
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.AddDays(-7)).Date;
                entity.And(a => a.PayTime >= time);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).AddDays(1).Date;
                entity.And(a => a.PayTime < time);
            }
            int rowCount = 0;
            List<FinancePayEntity> listResult = this.FinancePay.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }
    }
}
