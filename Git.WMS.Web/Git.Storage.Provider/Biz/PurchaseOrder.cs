/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-02 10:49:37
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-02 10:49:37       情缘
*********************************************************************************/

using Git.Storage.Entity.Biz;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Sys;
using System.Data;
using Git.Storage.Provider.Sys;
using Git.Storage.Entity.Storage;

namespace Git.Storage.Provider.Biz
{
    public partial class PurchaseOrder : Bill<PurchaseEntity, PurchaseDetailEntity>
    {
        public PurchaseOrder(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 创建单据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Create(PurchaseEntity entity, List<PurchaseDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.OrderNum = entity.OrderNum.IsEmpty() ?
                    new TNumProvider(this.CompanyID).GetSwiftNum(typeof(PurchaseEntity), 5)
                    : entity.OrderNum;
                entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.SnNum;
                entity.Status = (int)EPurchaseStatus.CreateOrder;
                entity.AuditeStatus = (int)EBool.No;//是否生成财务记录

                entity.IncludeAll();

                if (!list.IsNullOrEmpty())
                {
                    list.ForEach(a =>
                    {
                        a.SnNum = a.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : a.SnNum;
                        a.OrderSnNum = entity.SnNum;
                        a.OrderNum = entity.OrderNum;
                        a.CompanyID = this.CompanyID;
                        a.CreateTime = DateTime.Now;
                        a.IncludeAll();
                    });
                    entity.Num = list.Sum(q => q.Num);
                    entity.Amount = list.Sum(a => a.Amount);

                    line = this.Purchase.Add(entity);
                    line += this.PurchaseDetail.Add(list);
                }
                ts.Complete();
                return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
            }
        }

        /// <summary>
        /// 取消单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Cancel(PurchaseEntity entity)
        {
            //只有待审核状态的单据才能取消，已经成功的订单不能取消
            PurchaseEntity checkOrder = new PurchaseEntity();
            checkOrder.Where(a => a.Status != (int)EAudite.Wait)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            if (this.Purchase.GetCount(checkOrder) > 0)
            {
                return EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Pass); //已经审核或者取消的订单不能审核
            }
            entity.Status = (int)EPurchaseStatus.OrderCancel;
            entity.IncludeStatus(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.Purchase.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Delete(PurchaseEntity entity)
        {
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.Purchase.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 批量删除单据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Delete(IEnumerable<string> list)
        {
            PurchaseEntity entity = new PurchaseEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where<PurchaseEntity>("SnNum", ECondition.In, list.ToArray())
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.Purchase.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 审核单据
        /// 审核不通过 以及审核通过的处理方式
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Audite(PurchaseEntity entity)
        {
            entity.IncludeStatus(true)
                    .IncludeReason(true)
                    .Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
            int line = this.Purchase.Update(entity);
            return line > 0 ? "1000" : string.Empty;
        }

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Print(PurchaseEntity entity)
        {
            return string.Empty;
        }

        /// <summary>
        /// 查询单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override PurchaseEntity GetOrder(PurchaseEntity entity)
        {
            entity.IncludeAll();
            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                .And(a => a.SnNum == entity.SnNum);
            entity = this.Purchase.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 获得单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override List<PurchaseDetailEntity> GetOrderDetail(PurchaseDetailEntity entity)
        {
            PurchaseDetailEntity detail = new PurchaseDetailEntity();
            detail.IncludeAll();
            detail.Where(a => a.OrderSnNum == entity.OrderSnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            List<PurchaseDetailEntity> list = this.PurchaseDetail.GetList(detail);

            if (!list.IsNullOrEmpty())
            {
                List<ProductEntity> listProducts = new ProductProvider(this.CompanyID).GetList();
                listProducts = listProducts.IsNull() ? new List<ProductEntity>() : listProducts;

                foreach (PurchaseDetailEntity item in list)
                {
                    ProductEntity product = listProducts.First(a => a.SnNum == item.ProductNum);
                    item.UnitNum = product.UnitNum;
                    item.UnitName = product.UnitName;
                    item.Size = product.Size;
                }
            }
            return list;
        }

        /// <summary>
        /// 查询采购订单分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<PurchaseEntity> GetList(PurchaseEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID);
            entity.OrderBy(a => a.ID, EOrderBy.DESC);

            if (!entity.OrderNum.IsEmpty())
            {
                entity.Where("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.OrderType > 0)
            {
                entity.And(a => a.OrderType == entity.OrderType);
            }
            if (entity.SupNum.IsNotEmpty())
            {
                entity.Where("SupNum", ECondition.Like, "%" + entity.SupNum + "%");
            }
            if (entity.SupName.IsNotEmpty())
            {
                entity.Where("SupName", ECondition.Like, "%" + entity.SupName + "%");
            }
            if (entity.Contact.IsNotEmpty())
            {
                entity.Where("Contact", ECondition.Like, "%" + entity.Contact + "%");
            }
            if (entity.Phone.IsNotEmpty())
            {
                entity.Where("Phone", ECondition.Like, "%" + entity.Phone + "%");
            }
            if (!entity.ContractOrder.IsEmpty())
            {
                entity.And("ContractOrder", ECondition.Like, "%" + entity.ContractOrder + "%");
            }
            if (entity.Status > 0)
            {
                entity.And(a => a.Status == entity.Status);
            }
            if (entity.AuditeStatus > 0)
            {
                entity.And(a => a.AuditeStatus == entity.AuditeStatus);
            }
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime Begin = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.AddDays(-30)).Date;
                entity.And(a => a.CreateTime >= Begin);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime End = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).AddDays(1).Date;
                entity.And(a => a.CreateTime < End);
            }
            if (entity.BeginOrderTime.IsNotEmpty())
            {
                DateTime Begin = ConvertHelper.ToType<DateTime>(entity.BeginOrderTime, DateTime.Now.AddDays(-30)).Date;
                entity.And(a => a.OrderTime >= Begin);
            }
            if (entity.EndOrderTime.IsNotEmpty())
            {
                DateTime End = ConvertHelper.ToType<DateTime>(entity.EndOrderTime, DateTime.Now).AddDays(1).Date;
                entity.And(a => a.OrderTime < End);
            }
            if (entity.BeginRevDate.IsNotEmpty())
            {
                DateTime Begin = ConvertHelper.ToType<DateTime>(entity.BeginRevDate, DateTime.Now.AddDays(-30)).Date;
                entity.And(a => a.RevDate >= Begin);
            }
            if (entity.EndRevDate.IsNotEmpty())
            {
                DateTime End = ConvertHelper.ToType<DateTime>(entity.EndRevDate, DateTime.Now).AddDays(1).Date;
                entity.And(a => a.RevDate < End);
            }

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeAdmin = new AdminEntity();
            auditeAdmin.Include(a => new { AuidteUserName = a.UserName });
            entity.Left<AdminEntity>(auditeAdmin, new Params<string, string>() { Item1 = "AuidteUser", Item2 = "UserNum" });

            int rowCount = 0;
            List<PurchaseEntity> listResult = this.Purchase.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 查询单据详细数据分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<PurchaseDetailEntity> GetDetailList(PurchaseDetailEntity entity, ref PageInfo pageInfo)
        {
            PurchaseDetailEntity detail = new PurchaseDetailEntity();
            detail.And(a => a.CompanyID == this.CompanyID)
                ;
            detail.IncludeAll();
            detail.OrderBy(a => a.ID, EOrderBy.DESC);

            if (!entity.BarCode.IsEmpty())
            {
                detail.And("BarCode", ECondition.Like, "%" + entity.BarCode + "%");
            }
            if (!entity.ProductName.IsEmpty())
            {
                detail.And("ProductName", ECondition.Like, "%" + entity.ProductName + "%");
            }

            PurchaseEntity PurchaseOrder = new PurchaseEntity();
            detail.Left<PurchaseEntity>(PurchaseOrder, new Params<string, string>() { Item1 = "OrderSnNum", Item2 = "SnNum" });
            PurchaseOrder.Include(a => new { SupNum = a.SupNum, SupName = a.SupName, Phone = a.Phone, Contact = a.Contact, ContractOrder = a.ContractOrder, OrderTime = a.OrderTime, RevDate = a.RevDate, OrderAmount = a.Amount, OrderStatus = a.Status, AuditeStatus = a.AuditeStatus, HasReturn=a.HasReturn });
            PurchaseOrder.And(a => a.IsDelete == (int)EIsDelete.NotDelete);

            if (entity.OrderNum.IsNotEmpty())
            {
                PurchaseOrder.And("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.OrderSnNum.IsNotEmpty())
            {
                PurchaseOrder.And(item => item.SnNum==entity.OrderSnNum);
            }
            if (!entity.SupNum.IsEmpty())
            {
                PurchaseOrder.AndBegin<PurchaseEntity>()
                    .And<PurchaseEntity>("SupNum", ECondition.Like, "%" + entity.SupNum + "%")
                    .Or<PurchaseEntity>("SupName", ECondition.Like, "%" + entity.SupNum + "%")
                    .End<PurchaseEntity>()
                    ;
            }
            if (!entity.SupName.IsEmpty())
            {
                PurchaseOrder.AndBegin<PurchaseEntity>()
                    .And<PurchaseEntity>("SupNum", ECondition.Like, "%" + entity.SupName + "%")
                    .Or<PurchaseEntity>("SupName", ECondition.Like, "%" + entity.SupName + "%")
                    .End<PurchaseEntity>()
                    ;
            }
            if (entity.Contact.IsNotEmpty())
            {
                PurchaseOrder.And("Contact", ECondition.Like, "%" + entity.Contact + "%");
            }
            if (entity.Contact.IsNotEmpty())
            {
                PurchaseOrder.And("Phone", ECondition.Like, "%" + entity.Phone + "%");
            }
            if (entity.ContractOrder.IsNotEmpty())
            {
                PurchaseOrder.And("ContractOrder", ECondition.Like, "%" + entity.ContractOrder + "%");
            }
            if (entity.ContractSn.IsNotEmpty())
            {
                PurchaseOrder.And(item => item.ContractSn == entity.ContractSn);
            }
            if (entity.Status > 0)
            {
                PurchaseOrder.And(item=>item.Status==entity.Status);
            }
            if (entity.AuditeStatus > -1)
            {
                PurchaseOrder.And(item=>item.AuditeStatus==entity.AuditeStatus);
            }
            if (entity.HasReturn > -1)
            {
                PurchaseOrder.And(item => item.HasReturn == entity.HasReturn);
            }
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime Begin = ConvertHelper.ToType<DateTime>(entity.BeginTime,DateTime.Now.AddDays(-30)).Date;
                PurchaseOrder.And(item=>item.CreateTime>=Begin);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime End = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).Date.AddDays(1);
                PurchaseOrder.And(item => item.CreateTime < End);
            }

            int rowCount = 0;
            List<PurchaseDetailEntity> listResult = this.PurchaseDetail.GetList(detail, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;

            if (!listResult.IsNullOrEmpty())
            {
                ProductProvider productProvider = new ProductProvider(this.CompanyID);
                foreach(PurchaseDetailEntity item in listResult)
                {
                    ProductEntity product = productProvider.GetProduct(item.ProductNum);
                    item.UnitName = product != null ? product.UnitName : string.Empty;
                    item.Size = product != null ? product.Size : string.Empty;
                }
            }
            return listResult;
        }

        /// <summary>
        /// 编辑单据信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditOrder(PurchaseEntity entity)
        {
            entity.Include(a => new
            {
                a.SupSnNum,
                a.SupNum,
                a.SupName,
                a.Contact,
                a.Phone,
                a.Address,
                a.ContractOrder,
                a.Num,
                a.Amount,
                a.Weight,
                a.OrderTime,
                a.RevDate,
                a.Remark,
            });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.Purchase.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 查询单据详细信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public override PurchaseDetailEntity GetDetail(string SnNum)
        {
            PurchaseDetailEntity entity = new PurchaseDetailEntity();
            entity.IncludeAll();
            entity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;

            entity = this.PurchaseDetail.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 编辑单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditDetail(PurchaseDetailEntity entity)
        {
            entity.Include(a => new
            {
                a.ProductNum,
                a.BarCode,
                a.ProductName,
                a.Num,
                a.RealNum,
                a.UnitNum,
                a.Price,
                a.Amount,
                a.SendTime,
                a.ContractID,
                a.Remark,
            });
            entity.Where(a => a.SnNum == entity.SnNum);
            int line = this.PurchaseDetail.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 编辑入库单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string EditOrder(PurchaseEntity entity, List<PurchaseDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.Include(a => new
                {
                    a.SupSnNum,
                    a.SupNum,
                    a.SupName,
                    a.Contact,
                    a.Phone,
                    a.Address,
                    a.ContractOrder,
                    a.Num,
                    a.Amount,
                    a.Weight,
                    a.OrderTime,
                    a.RevDate,
                    a.Remark,
                });

                if (!list.IsNullOrEmpty())
                {
                    entity.Num = list.Sum(q => q.Num);
                    entity.Amount = list.Sum(a => a.Amount);
                }
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID);
                line += this.Purchase.Update(entity);

                PurchaseDetailEntity detail = new PurchaseDetailEntity();
                detail.IncludeAll();
                detail.Where(a => a.OrderSnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                List<PurchaseDetailEntity> listSource = this.PurchaseDetail.GetList(detail);
                listSource = listSource.IsNull() ? new List<PurchaseDetailEntity>() : listSource;

                //如果在原有的数据中存在修改，如果不存在新增
                list = list.IsNull() ? new List<PurchaseDetailEntity>() : list;
                foreach (PurchaseDetailEntity item in list)
                {
                    if (listSource.Exists(a => a.SnNum == item.SnNum))
                    {
                        item.Include(a => new
                        {
                            a.ProductNum,
                            a.BarCode,
                            a.ProductName,
                            a.Num,
                            a.RealNum,
                            a.UnitNum,
                            a.Price,
                            a.Amount,
                            a.SendTime,
                            a.ContractID,
                            a.Remark,
                        });
                        item.Where(a => a.SnNum == item.SnNum);
                        line += this.PurchaseDetail.Update(item);
                    }
                    else
                    {
                        item.IncludeAll();
                        item.OrderSnNum = entity.SnNum;
                        item.OrderNum = entity.OrderNum;
                        item.CreateTime = DateTime.Now;
                        line += this.PurchaseDetail.Add(item);
                    }
                }

                //如果数据库中的不存在了则删除
                foreach (PurchaseDetailEntity item in listSource)
                {
                    if (!list.Exists(a => a.SnNum == item.SnNum))
                    {
                        item.Where(a => a.SnNum == item.SnNum)
                            .And(a => a.CompanyID == this.CompanyID)
                            ;
                        line += this.PurchaseDetail.Delete(item);
                    }
                }
                ts.Complete();
                return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
            }
        }

        /// <summary>
        /// 获得订单数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int GetCount(PurchaseEntity entity)
        {
            entity.Where(a => a.CompanyID == this.CompanyID)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                ;
            return this.Purchase.GetCount(entity);
        }

        /// <summary>
        /// 获得打印单据的数据源
        /// </summary>
        /// <param name="argOrderNum"></param>
        /// <returns></returns>
        public override DataSet GetPrint(string argOrderNum)
        {
            DataSet ds = new DataSet();
            PurchaseEntity entity = new PurchaseEntity();
            entity.SnNum = argOrderNum;
            entity = GetOrder(entity);
            if (entity != null)
            {
                List<PurchaseEntity> list = new List<PurchaseEntity>();
                list.Add(entity);
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                PurchaseDetailEntity detail = new PurchaseDetailEntity();
                detail.OrderSnNum = argOrderNum;
                List<PurchaseDetailEntity> listDetail = GetOrderDetail(detail);
                listDetail = listDetail.IsNull() ? new List<PurchaseDetailEntity>() : listDetail;
                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            else
            {
                List<PurchaseEntity> list = new List<PurchaseEntity>();
                List<PurchaseDetailEntity> listDetail = new List<PurchaseDetailEntity>();
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            return ds;
        }
    }
}
