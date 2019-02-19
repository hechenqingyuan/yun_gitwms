/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-01 23:13:25
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-01 23:13:25       情缘
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
using Git.Storage.Entity.Storage;
using Git.Storage.Provider.Sys;

namespace Git.Storage.Provider.Biz
{
    public partial class SaleOrder : Bill<SaleOrderEntity, SaleDetailEntity>
    {
        public SaleOrder(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 创建单据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Create(SaleOrderEntity entity, List<SaleDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.OrderNum = entity.OrderNum.IsEmpty() ?
                    new TNumProvider(this.CompanyID).GetSwiftNum(typeof(SaleOrderEntity), 5)
                    : entity.OrderNum;
                entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.SnNum;
                entity.Status = (int)EOrderStatus.CreateOrder;
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
                        a.Status = (int)EOrderStatus.CreateOrder;
                        a.IncludeAll();
                    });
                    entity.Num = list.Sum(q => q.Num);
                    entity.Amount = list.Sum(a => a.Amount);

                    line = this.SaleOrder.Add(entity);
                    line += this.SaleDetail.Add(list);
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
        public override string Cancel(SaleOrderEntity entity)
        {
            //只有待审核状态的单据才能取消，已经成功的订单不能取消
            SaleOrderEntity checkOrder = new SaleOrderEntity();
            checkOrder.Where(a => a.Status != (int)EAudite.Wait)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID)
                ;
            if (this.SaleOrder.GetCount(checkOrder) > 0)
            {
                return EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Pass); //已经审核或者取消的订单不能审核
            }
            entity.Status = (int)EOrderStatus.OrderCancel;
            entity.IncludeStatus(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.SaleOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Delete(SaleOrderEntity entity)
        {
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.SaleOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 批量删除单据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Delete(IEnumerable<string> list)
        {
            SaleOrderEntity entity = new SaleOrderEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where<SaleOrderEntity>("SnNum", ECondition.In, list.ToArray())
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.SaleOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 审核单据
        /// 审核不通过 以及审核通过的处理方式
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Audite(SaleOrderEntity entity)
        {
            entity.IncludeStatus(true)
                    .IncludeReason(true)
                    .Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
            int line = this.SaleOrder.Update(entity);
            return line > 0 ? "1000" : string.Empty;
        }

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Print(SaleOrderEntity entity)
        {
            return string.Empty;
        }

        /// <summary>
        /// 查询单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override SaleOrderEntity GetOrder(SaleOrderEntity entity)
        {
            entity.IncludeAll();
            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                .And(a => a.SnNum == entity.SnNum);
            entity = this.SaleOrder.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 获得单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override List<SaleDetailEntity> GetOrderDetail(SaleDetailEntity entity)
        {
            SaleDetailEntity detail = new SaleDetailEntity();
            detail.IncludeAll();
            detail.Where(a => a.OrderSnNum == entity.OrderSnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;

            List<SaleDetailEntity> list = this.SaleDetail.GetList(detail);

            if (!list.IsNullOrEmpty())
            {
                List<ProductEntity> listProducts = new ProductProvider(this.CompanyID).GetList();
                listProducts = listProducts.IsNull() ? new List<ProductEntity>() : listProducts;
                foreach (SaleDetailEntity item in list)
                {
                    ProductEntity product = listProducts.FirstOrDefault(a => a.SnNum == item.ProductNum);
                    if (product != null)
                    {
                        item.UnitNum = product.UnitNum;
                        item.UnitName = product.UnitName;
                        item.Size = product.Size;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 查询单据分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<SaleOrderEntity> GetList(SaleOrderEntity entity, ref PageInfo pageInfo)
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
            if (entity.CusNum.IsNotEmpty())
            {
                entity.Where("CusNum", ECondition.Like, "%" + entity.CusNum + "%");
            }
            if (entity.CusName.IsNotEmpty())
            {
                entity.Where("CusName", ECondition.Like, "%" + entity.CusName + "%");
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
            if (!entity.ContractSn.IsEmpty())
            {
                entity.And(item => item.ContractSn==entity.ContractSn);
            }
            if (entity.Status > 0)
            {
                entity.And(a => a.Status == entity.Status);
            }
            if (entity.AuditeStatus > -1)
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
            if (entity.BeginSendTime.IsNotEmpty())
            {
                DateTime Begin = ConvertHelper.ToType<DateTime>(entity.BeginSendTime, DateTime.Now.AddDays(-30)).Date;
                entity.And(a => a.SendDate >= Begin);
            }
            if (entity.EndSendTime.IsNotEmpty())
            {
                DateTime End = ConvertHelper.ToType<DateTime>(entity.EndSendTime, DateTime.Now).AddDays(1).Date;
                entity.And(a => a.SendDate < End);
            }
            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            int rowCount = 0;
            List<SaleOrderEntity> listResult = this.SaleOrder.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 查询单据详细数据分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<SaleDetailEntity> GetDetailList(SaleDetailEntity entity, ref PageInfo pageInfo)
        {
            SaleDetailEntity detail = new SaleDetailEntity();
            detail.And(a => a.CompanyID == this.CompanyID)
                ;
            if (!entity.BarCode.IsEmpty())
            {
                detail.And("BarCode", ECondition.Like, "%" + entity.BarCode + "%");
            }
            if (!entity.ProductName.IsEmpty())
            {
                detail.And("ProductName", ECondition.Like, "%" + entity.ProductName + "%");
            }

            SaleOrderEntity SaleOrder = new SaleOrderEntity();
            detail.Left<SaleOrderEntity>(SaleOrder, new Params<string, string>() { Item1 = "OrderSnNum", Item2 = "SnNum" });
            SaleOrder.Include(a => new { CusNum = a.CusNum, CusName = a.CusName, Phone = a.Phone, CusOrderNum = a.CusOrderNum, Address = a.Address, SendDate = a.SendDate, OrderTime = a.OrderTime, AuditeStatus = a.AuditeStatus, OrderStatus = a.Status, OrderAmount = a.Amount, HasReturn=a.HasReturn });
            SaleOrder.And(a => a.IsDelete == (int)EIsDelete.NotDelete);

            if (!entity.CusNum.IsEmpty())
            {
                SaleOrder.AndBegin<SaleOrderEntity>()
                    .And<SaleOrderEntity>("CusNum", ECondition.Like, "%" + entity.CusNum + "%")
                    .Or<SaleOrderEntity>("CusName", ECondition.Like, "%" + entity.CusNum + "%")
                    .End<SaleOrderEntity>()
                    ;
            }
            if (!entity.CusName.IsEmpty())
            {
                SaleOrder.AndBegin<SaleOrderEntity>()
                    .And<SaleOrderEntity>("CusNum", ECondition.Like, "%" + entity.CusName + "%")
                    .Or<SaleOrderEntity>("CusName", ECondition.Like, "%" + entity.CusName + "%")
                    .End<SaleOrderEntity>()
                    ;
            }
            if (!entity.Phone.IsEmpty())
            {
                SaleOrder.And("Phone", ECondition.Like, "%" + entity.Phone + "%");
            }
            if (!entity.OrderNum.IsEmpty())
            {
                SaleOrder.And("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (!entity.OrderSnNum.IsEmpty())
            {
                SaleOrder.And(item => item.SnNum==entity.OrderSnNum);
            }
            if (entity.Status > 0)
            {
                SaleOrder.And(item => item.Status == entity.Status);
            }
            if (entity.AuditeStatus > -1)
            {
                SaleOrder.And(item => item.AuditeStatus == entity.AuditeStatus);
            }
            if (!entity.ContractOrder.IsEmpty())
            {
                SaleOrder.And(item => item.ContractOrder == entity.ContractOrder);
            }
            if (!entity.CusOrderNum.IsEmpty())
            {
                SaleOrder.And(item => item.CusOrderNum == entity.CusOrderNum);
            }
            if (!entity.BeginTime.IsEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now).Date;
                SaleOrder.And(a => a.CreateTime >= time);
            }
            if (!entity.EndTime.IsEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).Date.AddDays(1);
                SaleOrder.And(a => a.CreateTime < time);
            }
            detail.IncludeAll();
            detail.OrderBy(a => a.ID, EOrderBy.DESC);
            int rowCount = 0;
            List<SaleDetailEntity> listResult = this.SaleDetail.GetList(detail, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            if (!listResult.IsNullOrEmpty())
            {
                ProductProvider productProvider = new ProductProvider(this.CompanyID);
                foreach (SaleDetailEntity item in listResult)
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
        public override string EditOrder(SaleOrderEntity entity)
        {
            entity.Include(a => new 
            { 
                a.CusNum,
                a.CusName,
                a.CusSnNum,
                a.Contact,
                a.Phone,
                a.Address,
                a.ContractOrder,
                a.Num,
                a.Amount,
                a.Weight,
                a.OrderTime,
                a.SendDate,
                a.Remark,
            });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.SaleOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 查询单据详细信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public override SaleDetailEntity GetDetail(string SnNum)
        {
            SaleDetailEntity entity = new SaleDetailEntity();
            entity.IncludeAll();
            entity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;

            entity = this.SaleDetail.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 编辑单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditDetail(SaleDetailEntity entity)
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
            int line = this.SaleDetail.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 编辑入库单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string EditOrder(SaleOrderEntity entity, List<SaleDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.Include(a => new
                {
                    a.CusNum,
                    a.CusName,
                    a.CusSnNum,
                    a.Contact,
                    a.Phone,
                    a.Address,
                    a.ContractOrder,
                    a.Num,
                    a.Amount,
                    a.Weight,
                    a.OrderTime,
                    a.SendDate,
                    a.Remark,
                });

                if (!list.IsNullOrEmpty())
                {
                    entity.Num = list.Sum(q => q.Num);
                    entity.Amount = list.Sum(a => a.Amount);
                }
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID);
                line += this.SaleOrder.Update(entity);

                SaleDetailEntity detail = new SaleDetailEntity();
                detail.IncludeAll();
                detail.Where(a => a.OrderSnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                List<SaleDetailEntity> listSource = this.SaleDetail.GetList(detail);
                listSource = listSource.IsNull() ? new List<SaleDetailEntity>() : listSource;

                //如果在原有的数据中存在修改，如果不存在新增
                list = list.IsNull() ? new List<SaleDetailEntity>() : list;
                foreach (SaleDetailEntity item in list)
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
                        line += this.SaleDetail.Update(item);
                    }
                    else
                    {
                        item.IncludeAll();
                        item.OrderSnNum = entity.SnNum;
                        item.OrderNum = entity.OrderNum;
                        item.CreateTime = DateTime.Now;
                        line += this.SaleDetail.Add(item);
                    }
                }

                //如果数据库中的不存在了则删除
                foreach (SaleDetailEntity item in listSource)
                {
                    if (!list.Exists(a => a.SnNum == item.SnNum))
                    {
                        item.Where(a => a.SnNum == item.SnNum)
                            .And(a => a.CompanyID == this.CompanyID)
                            ;
                        line += this.SaleDetail.Delete(item);
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
        public override int GetCount(SaleOrderEntity entity)
        {
            entity.Where(a => a.CompanyID == this.CompanyID)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                ;
            return this.SaleOrder.GetCount(entity);
        }

        /// <summary>
        /// 获得打印单据的数据源
        /// </summary>
        /// <param name="argOrderNum"></param>
        /// <returns></returns>
        public override DataSet GetPrint(string argOrderNum)
        {
            DataSet ds = new DataSet();
            SaleOrderEntity entity = new SaleOrderEntity();
            entity.SnNum = argOrderNum;
            entity = GetOrder(entity);
            if (entity != null)
            {
                List<SaleOrderEntity> list = new List<SaleOrderEntity>();
                list.Add(entity);
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                SaleDetailEntity detail = new SaleDetailEntity();
                detail.OrderSnNum = argOrderNum;
                List<SaleDetailEntity> listDetail = GetOrderDetail(detail);
                listDetail = listDetail.IsNull() ? new List<SaleDetailEntity>() : listDetail;
                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            else
            {
                List<SaleOrderEntity> list = new List<SaleOrderEntity>();
                List<SaleDetailEntity> listDetail = new List<SaleDetailEntity>();
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            return ds;
        }
    }
}
