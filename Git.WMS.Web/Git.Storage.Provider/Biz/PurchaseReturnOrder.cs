/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/3/16 11:25:44
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/3/16 11:25:44       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
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
    public partial class PurchaseReturnOrder : Bill<PurchaseReturnEntity, PurchaseReturnDetailEntity>
    {
        public PurchaseReturnOrder(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }


        /// <summary>
        /// 创建采购退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Create(PurchaseReturnEntity entity, List<PurchaseReturnDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.OrderNum = entity.OrderNum.IsEmpty() ? new TNumProvider(this.CompanyID).GetSwiftNum(typeof(PurchaseReturnEntity), 5) : entity.OrderNum;
                entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.SnNum;
                entity.Status = (int)EPurchaseReturnStatus.CreateOrder;
                entity.IsDelete = (int)EIsDelete.NotDelete;
                entity.CreateTime = DateTime.Now;
                entity.CompanyID = this.CompanyID;
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
                        a.Status = (int)EPurchaseReturnStatus.CreateOrder;
                        a.ReturnTime = entity.ReturnTime;
                        a.IncludeAll();
                    });
                    entity.Num = list.Sum(q => q.Num);
                    entity.Amount = list.Sum(a => a.Amount);

                    line = this.PurchaseReturn.Add(entity);
                    line += this.PurchaseReturnDetail.Add(list);
                }
                ts.Complete();
                return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
            }
        }

        /// <summary>
        /// 取消采购退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Cancel(PurchaseReturnEntity entity)
        {
            entity.Status = (int)EPurchaseReturnStatus.OrderCancel;
            entity.IncludeStatus(true);
            entity.Where(item => item.SnNum == entity.SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            int line = this.PurchaseReturn.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 删除采购退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Delete(PurchaseReturnEntity entity)
        {
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.PurchaseReturn.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 批量删除采购退货单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Delete(IEnumerable<string> list)
        {
            PurchaseReturnEntity entity = new PurchaseReturnEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where<PurchaseReturnEntity>("SnNum", ECondition.In, list.ToArray())
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.PurchaseReturn.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 审核采购退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Audite(PurchaseReturnEntity entity)
        {
            entity.IncludeStatus(true)
                    .IncludeReason(true)
                    .Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
            int line = this.PurchaseReturn.Update(entity);
            return line > 0 ? "1000" : string.Empty;
        }

        /// <summary>
        /// 打印采购退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Print(PurchaseReturnEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询采购退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override PurchaseReturnEntity GetOrder(PurchaseReturnEntity entity)
        {
            entity.IncludeAll();
            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                .And(a => a.SnNum == entity.SnNum);

            entity = this.PurchaseReturn.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 获得采购退货单详细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override List<PurchaseReturnDetailEntity> GetOrderDetail(PurchaseReturnDetailEntity entity)
        {
            PurchaseReturnDetailEntity detail = new PurchaseReturnDetailEntity();
            detail.IncludeAll();
            detail.Where(a => a.OrderSnNum == entity.OrderSnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            List<PurchaseReturnDetailEntity> list = this.PurchaseReturnDetail.GetList(detail);

            if (!list.IsNullOrEmpty())
            {
                List<ProductEntity> listProducts = new ProductProvider(this.CompanyID).GetList();
                listProducts = listProducts.IsNull() ? new List<ProductEntity>() : listProducts;

                foreach (PurchaseReturnDetailEntity item in list)
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
        /// 查询采购退货单分页列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<PurchaseReturnEntity> GetList(PurchaseReturnEntity entity, ref Framework.DataTypes.PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID);
            entity.OrderBy(a => a.ID, EOrderBy.DESC);

            if (entity.OrderNum.IsNotEmpty())
            {
                entity.And("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.SupNum.IsNotEmpty())
            {
                entity.And("SupNum", ECondition.Like, "%" + entity.SupNum + "%");
            }
            if (entity.SupName.IsNotEmpty())
            {
                entity.And("SupName", ECondition.Like, "%" + entity.SupName + "%");
            }
            if (entity.Contact.IsNotEmpty())
            {
                entity.And("Contact", ECondition.Like, "%" + entity.Contact + "%");
            }
            if (entity.Phone.IsNotEmpty())
            {
                entity.And("Phone", ECondition.Like, "%" + entity.Phone + "%");
            }
            if (entity.PurchaseSnNum.IsNotEmpty())
            {
                entity.And(item => item.PurchaseSnNum == entity.PurchaseSnNum);
            }
            if (entity.PurchaseOrderNum.IsNotEmpty())
            {
                entity.And("PurchaseOrderNum", ECondition.Like, "%" + entity.PurchaseOrderNum + "%");
            }
            if (entity.Status > 0)
            {
                entity.And(item => item.Status == entity.Status);
            }
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime begin = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.AddDays(-30)).Date;
                entity.And(item => item.CreateTime >= begin);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime end = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).AddDays(1).Date;
                entity.And(item => item.CreateTime < end);
            }

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            int rowCount = 0;
            List<PurchaseReturnEntity> listResult = this.PurchaseReturn.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 采购退货单明细分页列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<PurchaseReturnDetailEntity> GetDetailList(PurchaseReturnDetailEntity entity, ref Framework.DataTypes.PageInfo pageInfo)
        {
            PurchaseReturnDetailEntity detail = new PurchaseReturnDetailEntity();
            detail.And(a => a.CompanyID == this.CompanyID)
                ;
            if (!entity.OrderSnNum.IsEmpty())
            {
                detail.And(a => a.OrderSnNum == entity.OrderSnNum);
            }
            if (!entity.BarCode.IsEmpty())
            {
                detail.And("BarCode", ECondition.Like, "%" + entity.BarCode + "%");
            }
            if (!entity.ProductName.IsEmpty())
            {
                detail.And("ProductName", ECondition.Like, "%" + entity.ProductName + "%");
            }

            PurchaseReturnEntity returnOrder = new PurchaseReturnEntity();
            detail.Left<PurchaseReturnEntity>(returnOrder, new Params<string, string>() { Item1 = "OrderSnNum", Item2 = "SnNum" });
            returnOrder.Include(a => new { SupNum = a.SupNum, SupName = a.SupName, Phone = a.Phone, Contact = a.Contact, PurchaseSnNum = a.PurchaseSnNum, PurchaseOrderNum = a.PurchaseOrderNum, Status = a.Status });
            returnOrder.And(a => a.IsDelete == (int)EIsDelete.NotDelete);

            if (!entity.SupNum.IsEmpty())
            {
                returnOrder.AndBegin<PurchaseReturnEntity>()
                    .And<PurchaseReturnEntity>("SupNum", ECondition.Like, "%" + entity.SupNum + "%")
                    .Or<PurchaseReturnEntity>("SupName", ECondition.Like, "%" + entity.SupNum + "%")
                    .End<PurchaseReturnEntity>()
                    ;
            }
            if (!entity.SupName.IsEmpty())
            {
                returnOrder.AndBegin<PurchaseReturnEntity>()
                    .And<PurchaseReturnEntity>("SupNum", ECondition.Like, "%" + entity.SupName + "%")
                    .Or<PurchaseReturnEntity>("SupName", ECondition.Like, "%" + entity.SupName + "%")
                    .End<PurchaseReturnEntity>()
                    ;
            }
            if (!entity.Phone.IsEmpty())
            {
                returnOrder.And("Phone", ECondition.Like, "%" + entity.Phone + "%");
            }
            if (!entity.PurchaseSnNum.IsEmpty())
            {
                returnOrder.And(item => item.PurchaseSnNum == entity.PurchaseSnNum);
            }
            if (!entity.PurchaseOrderNum.IsEmpty())
            {
                returnOrder.And("PurchaseOrderNum", ECondition.Like, "%" + entity.PurchaseOrderNum + "%");
            }
            if (!entity.BeginTime.IsEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.Date.AddDays(-1));
                returnOrder.And(a => a.CreateTime >= time);
            }
            if (!entity.EndTime.IsEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now.Date.AddDays(1));
                returnOrder.And(a => a.CreateTime < time);
            }
            if (entity.Status > 0)
            {
                returnOrder.And(item => item.Status == entity.Status);
            }

            detail.IncludeAll();
            detail.Exclude(item => new { item.Status });
            detail.OrderBy(a => a.ID, EOrderBy.DESC);
            int rowCount = 0;
            List<PurchaseReturnDetailEntity> listResult = this.PurchaseReturnDetail.GetList(detail, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            if (!listResult.IsNullOrEmpty())
            {
                ProductProvider productProvider = new ProductProvider(this.CompanyID);
                foreach (PurchaseReturnDetailEntity item in listResult)
                {
                    ProductEntity product = productProvider.GetProduct(item.ProductNum);
                    item.UnitName = product != null ? product.UnitName : string.Empty;
                    item.Size = product != null ? product.Size : string.Empty;
                }
            }
            return listResult;
        }

        /// <summary>
        /// 编辑采购退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditOrder(PurchaseReturnEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询单据详细信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public override PurchaseReturnDetailEntity GetDetail(string SnNum)
        {
            PurchaseReturnDetailEntity entity = new PurchaseReturnDetailEntity();
            entity.IncludeAll();
            entity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;

            entity = this.PurchaseReturnDetail.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 编辑采购退货单单详细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditDetail(PurchaseReturnDetailEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 编辑采购退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string EditOrder(PurchaseReturnEntity entity, List<PurchaseReturnDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.Include(a => new { a.SupSnNum, a.SupNum, a.SupName, a.Contact, a.Phone, a.Num, a.Amount, a.Weight, a.ReturnTime, a.Remark });

                if (!list.IsNullOrEmpty())
                {
                    entity.Num = list.Sum(q => q.Num);
                    entity.Amount = list.Sum(a => a.Amount);
                }
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID);
                line += this.PurchaseReturn.Update(entity);

                PurchaseReturnDetailEntity detail = new PurchaseReturnDetailEntity();
                detail.IncludeAll();
                detail.Where(a => a.OrderSnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                List<PurchaseReturnDetailEntity> listSource = this.PurchaseReturnDetail.GetList(detail);
                listSource = listSource.IsNull() ? new List<PurchaseReturnDetailEntity>() : listSource;

                //如果在原有的数据中存在修改，如果不存在新增
                list = list.IsNull() ? new List<PurchaseReturnDetailEntity>() : list;
                foreach (PurchaseReturnDetailEntity item in list)
                {
                    if (listSource.Exists(a => a.SnNum == item.SnNum))
                    {
                        item.Include(a => new { a.BarCode, a.ProductName, a.ProductNum, a.Num, a.ReturnNum, a.UnitNum, a.Price, a.Amount, a.Remark });
                        item.Where(a => a.SnNum == item.SnNum);
                        line += this.PurchaseReturnDetail.Update(item);
                    }
                    else
                    {
                        item.IncludeAll();
                        item.OrderSnNum = entity.SnNum;
                        item.OrderNum = entity.OrderNum;
                        item.CreateTime = DateTime.Now;
                        line += this.PurchaseReturnDetail.Add(item);
                    }
                }

                //如果数据库中的不存在了则删除
                foreach (PurchaseReturnDetailEntity item in listSource)
                {
                    if (!list.Exists(a => a.SnNum == item.SnNum))
                    {
                        item.Where(a => a.SnNum == item.SnNum)
                            .And(a => a.CompanyID == this.CompanyID)
                            ;
                        line += this.PurchaseReturnDetail.Delete(item);
                    }
                }
                ts.Complete();
                return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
            }
        }

        /// <summary>
        /// 查询采购退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int GetCount(PurchaseReturnEntity entity)
        {
            entity.Where(a => a.CompanyID == this.CompanyID)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                ;
            return this.PurchaseReturn.GetCount(entity);
        }

        /// <summary>
        /// 查询采购退货单打印数据
        /// </summary>
        /// <param name="argOrderNum"></param>
        /// <returns></returns>
        public override System.Data.DataSet GetPrint(string argOrderNum)
        {
            DataSet ds = new DataSet();
            PurchaseReturnEntity entity = new PurchaseReturnEntity();
            entity.SnNum = argOrderNum;
            entity = GetOrder(entity);
            if (entity != null)
            {
                List<PurchaseReturnEntity> list = new List<PurchaseReturnEntity>();
                list.Add(entity);
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                PurchaseReturnDetailEntity detail = new PurchaseReturnDetailEntity();
                detail.OrderSnNum = argOrderNum;
                List<PurchaseReturnDetailEntity> listDetail = GetOrderDetail(detail);
                listDetail = listDetail.IsNull() ? new List<PurchaseReturnDetailEntity>() : listDetail;
                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            else
            {
                List<PurchaseReturnEntity> list = new List<PurchaseReturnEntity>();
                List<PurchaseReturnDetailEntity> listDetail = new List<PurchaseReturnDetailEntity>();
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            return ds;
        }
    }
}
