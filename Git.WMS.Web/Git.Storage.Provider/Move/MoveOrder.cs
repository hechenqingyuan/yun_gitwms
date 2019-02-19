/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-05 17:05:00
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-05 17:05:00       情缘
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Move;
using Git.Storage.Entity.Storage;
using Git.Storage.Entity.Sys;
using Git.Storage.Provider.Base;
using Git.Storage.Provider.Sys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Git.Storage.Provider.Move
{
    public partial class MoveOrder : Bill<MoveOrderEntity, MoveOrderDetailEntity>
    {
        public MoveOrder(string _CompanyID) 
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 创建单据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Create(MoveOrderEntity entity, List<MoveOrderDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.SnNum;
                entity.OrderNum = entity.OrderNum.IsEmpty() ? new SequenceProvider(this.CompanyID).GetSequence(typeof(MoveOrderEntity)) : entity.OrderNum;
                entity.Status = (int)EAudite.Wait;
                entity.IsDelete = (int)EIsDelete.NotDelete;
                entity.CreateTime = DateTime.Now;
                entity.IncludeAll();

                if (!list.IsNullOrEmpty())
                {
                    list.ForEach(a =>
                    {
                        a.IncludeAll();
                        a.OrderNum = entity.OrderNum;
                        a.OrderSnNum = entity.SnNum;
                        a.CreateTime = DateTime.Now;
                    });

                    entity.Num = list.Sum(q => q.Num);
                    entity.Amount = list.Sum(a => a.Amount);

                    line = this.MoveOrder.Add(entity);
                    line += this.MoveOrderDetail.Add(list);
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
        public override string Cancel(MoveOrderEntity entity)
        {
            //只有待审核状态的单据才能取消，已经成功的订单不能取消
            MoveOrderEntity checkOrder = new MoveOrderEntity();
            entity.Where(a => a.Status == (int)EAudite.Wait).And(a => a.SnNum == entity.SnNum).And(a=>a.CompanyID==this.CompanyID);
            if (this.MoveOrder.GetCount(checkOrder) > 0)
            {
                return EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Pass); //已经审核或者取消的订单不能审核
            }
            entity.Status = (int)EAudite.NotPass;
            entity.IncludeStatus(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            int line = this.MoveOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Delete(MoveOrderEntity entity)
        {
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            int line = this.MoveOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Audite(MoveOrderEntity entity)
        {
            if (entity.Status == (int)EAudite.NotPass)
            {
                entity.IncludeStatus(true).IncludeReason(true)
                    .Where(a => a.SnNum == entity.SnNum)
                    .And(a=>a.CompanyID==this.CompanyID);
                int line = this.MoveOrder.Update(entity);
                return line > 0 ? "1000" : string.Empty;
            }
            else if (entity.Status == (int)EAudite.Pass)
            {
                Proc_AuditeMoveEntity auditeEntity = new Proc_AuditeMoveEntity();
                auditeEntity.OrderNum = entity.SnNum;
                auditeEntity.Status = entity.Status;
                auditeEntity.AuditUser = entity.AuditUser;
                auditeEntity.Reason = entity.Reason;
                auditeEntity.OperateType = entity.OperateType;
                auditeEntity.EquipmentNum = entity.EquipmentNum;
                auditeEntity.EquipmentCode = entity.EquipmentCode;
                auditeEntity.Remark = entity.Remark;
                auditeEntity.CompanyID = this.CompanyID;
                int line = this.Proc_AuditeMove.ExecuteNonQuery(auditeEntity);
                return auditeEntity.ReturnValue;
            }
            return string.Empty;
        }

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Print(MoveOrderEntity entity)
        {
            entity.IncludePrintUser(true).IncludePrintTime(true)
                .Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            int line = this.MoveOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 查询单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override MoveOrderEntity GetOrder(MoveOrderEntity entity)
        {
            entity.IncludeAll();
            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            entity = this.MoveOrder.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 获得单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override List<MoveOrderDetailEntity> GetOrderDetail(MoveOrderDetailEntity entity)
        {
            MoveOrderDetailEntity detail = new MoveOrderDetailEntity();
            detail.IncludeAll();
            detail.Where(a => a.OrderSnNum == entity.OrderSnNum)
                .And(a=>a.CompanyID==this.CompanyID);

            ProductEntity product = new ProductEntity();
            product.Include(a => new { Size = a.Size });
            detail.Left<ProductEntity>(product, new Params<string, string>() { Item1 = "ProductNum", Item2 = "SnNum" });
            List<MoveOrderDetailEntity> list = this.MoveOrderDetail.GetList(detail);
            if (!list.IsNullOrEmpty())
            {
                List<LocationEntity> listLocation = new LocationProvider(this.CompanyID).GetList();
                listLocation = listLocation == null ? new List<LocationEntity>() : listLocation;
                foreach (MoveOrderDetailEntity item in list)
                {
                    LocationEntity location = listLocation.FirstOrDefault(a => a.LocalNum == item.FromLocalNum);
                    item.FromLocalName = location == null ? "" : location.LocalName;
                    item.StorageName = location == null ? "" : location.StorageName;

                    location = listLocation.FirstOrDefault(a => a.LocalNum == item.ToLocalNum);
                    item.ToLocalName = location == null ? "" : location.LocalName;
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
        public override List<MoveOrderEntity> GetList(MoveOrderEntity entity, ref Framework.DataTypes.PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID);
            entity.OrderBy(a => a.ID, EOrderBy.DESC);

            if (!entity.OrderNum.IsEmpty())
            {
                entity.Where("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(a => a.StorageNum == entity.StorageNum);
            }
            if (entity.MoveType > 0)
            {
                entity.And(a => a.MoveType == entity.MoveType);
            }
            if (!entity.ContractOrder.IsEmpty())
            {
                entity.And("ContractOrder", ECondition.Like, "%" + entity.ContractOrder + "%");
            }
            if (entity.Status > 0)
            {
                entity.And(a => a.Status == entity.Status);
            }
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime Begin = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.Date.AddDays(-10)).Date;
                entity.And(a => a.CreateTime >= Begin);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime End = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).AddDays(1).Date;
                entity.And(a => a.CreateTime < End);
            }

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });
            int rowCount = 0;
            List<MoveOrderEntity> listResult = this.MoveOrder.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 查询单据详细数据分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<MoveOrderDetailEntity> GetDetailList(MoveOrderDetailEntity entity, ref Framework.DataTypes.PageInfo pageInfo)
        {
            MoveOrderDetailEntity detail = new MoveOrderDetailEntity();
            detail
                .And(a=>a.CompanyID==this.CompanyID);

            if (entity.OrderSnNum.IsNotEmpty())
            {
                detail.And(item => item.OrderSnNum == entity.OrderSnNum);
            }
            if (entity.ProductName.IsNotEmpty())
            {
                detail.And("ProductName", ECondition.Like, "%" + entity.ProductName + "%");
            }
            if (entity.BarCode.IsNotEmpty())
            {
                detail.And("BarCode", ECondition.Like, "%" + entity.BarCode + "%");
            }
            detail.IncludeAll();
            detail.OrderBy(a => a.ID, EOrderBy.DESC);

            MoveOrderEntity moveOrder = new MoveOrderEntity();
            moveOrder.Include(item => new { Status = item.Status, MoveType = item.MoveType, AuditeTime = item.AuditeTime });
            detail.Left<MoveOrderEntity>(moveOrder, new Params<string, string>() { Item1 = "OrderSnNum", Item2 = "SnNum" });
            moveOrder.And(item=>item.IsDelete==(int)EIsDelete.NotDelete);
            if (entity.OrderNum.IsNotEmpty())
            {
                moveOrder.And("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.Status > 0)
            {
                moveOrder.And(item => item.Status == entity.Status);
            }
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime begin = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.AddDays(-10)).Date;
                moveOrder.And(item => item.CreateTime >= begin);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime end = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).AddDays(1).Date;
                moveOrder.And(item => item.CreateTime < end);
            }
            if (entity.MoveType > 0)
            {
                moveOrder.And(item => item.MoveType == entity.MoveType);
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                moveOrder.And(item=>item.StorageNum==entity.StorageNum);
            }

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            moveOrder.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeAdmin = new AdminEntity();
            auditeAdmin.Include(a => new { AuditUserName = a.UserName });
            moveOrder.Left<AdminEntity>(auditeAdmin, new Params<string, string>() { Item1 = "AuditUser", Item2 = "UserNum" });

            int rowCount = 0;
            List<MoveOrderDetailEntity> listResult = this.MoveOrderDetail.GetList(detail, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            if (!listResult.IsNullOrEmpty())
            {
                List<LocationEntity> listLocation = new LocationProvider(this.CompanyID).GetList();
                listLocation = listLocation == null ? new List<LocationEntity>() : listLocation;

                ProductProvider productProvider = new ProductProvider(this.CompanyID);
                foreach (MoveOrderDetailEntity item in listResult)
                {
                    LocationEntity location = listLocation.FirstOrDefault(a => a.LocalNum == item.FromLocalNum);
                    item.FromLocalName = location == null ? "" : location.LocalName;
                    location = listLocation.FirstOrDefault(a => a.LocalNum == item.ToLocalNum);
                    item.ToLocalName = location == null ? "" : location.LocalName;
                    item.StorageName = location == null ? "" : location.StorageName;

                    ProductEntity productEntity = productProvider.GetProduct(item.ProductNum);
                    item.UnitName = productEntity != null ? productEntity.ProductName : string.Empty;
                    item.Size = productEntity != null ? productEntity.Size : string.Empty;
                }
            }
            return listResult;
        }

        /// <summary>
        /// 编辑单据信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditOrder(MoveOrderEntity entity)
        {
            entity.Include(a => new { a.MoveType, a.ProductType, a.ContractOrder, a.Remark});
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            int line = this.MoveOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 查询单据详细信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public override MoveOrderDetailEntity GetDetail(string SnNum)
        {
            MoveOrderDetailEntity entity = new MoveOrderDetailEntity();
            entity.IncludeAll();
            entity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;

            entity = this.MoveOrderDetail.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 编辑单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditDetail(MoveOrderDetailEntity entity)
        {
            entity.Include(a => new 
            { 
                a.ProductName,
                a.BarCode,
                a.ProductNum,
                a.BatchNum,
                a.Num,
                a.InPrice,
                a.Amount,
                a.RealNum,
                a.StorageNum,
                a.FromLocalNum,
                a.ToLocalNum
            });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            int line = this.MoveOrderDetail.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 获得订单数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int GetCount(MoveOrderEntity entity)
        {
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID);
            
            if (!entity.OrderNum.IsEmpty())
            {
                entity.Where("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(a => a.StorageNum == entity.StorageNum);
            }
            if (entity.MoveType > 0)
            {
                entity.And(a => a.MoveType == entity.MoveType);
            }
            if (!entity.ContractOrder.IsEmpty())
            {
                entity.And("ContractOrder", ECondition.Like, "%" + entity.ContractOrder + "%");
            }
            if (entity.Status > 0)
            {
                entity.And(a => a.Status == entity.Status);
            }
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime Begin = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.Date.AddDays(-10)).Date;
                entity.And(a => a.CreateTime >= Begin);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime End = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).AddDays(1).Date;
                entity.And(a => a.CreateTime < End);
            }

            return this.MoveOrder.GetCount(entity);
        }

        /// <summary>
        /// 编辑移库单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string EditOrder(MoveOrderEntity entity, List<MoveOrderDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.Include(a => new { a.MoveType, a.ProductType, a.ContractOrder, a.Remark, a.Amount, a.Num, a.Weight });
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a=>a.CompanyID==this.CompanyID);
                MoveOrderDetailEntity detail = new MoveOrderDetailEntity();
                detail.Where(a => a.OrderSnNum == entity.SnNum);
                this.MoveOrderDetail.Delete(detail);
                foreach (MoveOrderDetailEntity item in list)
                {
                    item.OrderNum = entity.OrderNum;
                    item.OrderSnNum = entity.SnNum;
                    item.CreateTime = DateTime.Now;
                    item.CompanyID = this.CompanyID;
                    item.IncludeAll();
                }
                entity.Num = list.Sum(a => a.Num);
                entity.Amount = list.Sum(a => a.Amount);
                line = this.MoveOrder.Update(entity);
                this.MoveOrderDetail.Add(list);
                ts.Complete();
                return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
            }
        }


        /// <summary>
        /// 获得打印单据的数据源
        /// </summary>
        /// <param name="argOrderNum"></param>
        /// <returns></returns>
        public override DataSet GetPrint(string argOrderNum)
        {
            DataSet ds = new DataSet();
            MoveOrderEntity entity = new MoveOrderEntity();
            entity.OrderNum = argOrderNum;
            entity = GetOrder(entity);
            if (entity != null)
            {
                List<MoveOrderEntity> list = new List<MoveOrderEntity>();
                list.Add(entity);
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                MoveOrderDetailEntity detail = new MoveOrderDetailEntity();
                detail.OrderNum = argOrderNum;
                List<MoveOrderDetailEntity> listDetail = GetOrderDetail(detail);
                if (!listDetail.IsNullOrEmpty())
                {
                    DataTable tableDetail = listDetail.ToDataTable();
                    ds.Tables.Add(tableDetail);
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataTable table in ds.Tables)
                    {
                        if (table.Rows.Count == 0)
                        {
                            DataRow row = table.NewRow();
                            table.Rows.Add(row);
                        }
                    }
                }
            }

            return ds;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Delete(IEnumerable<string> list)
        {
            MoveOrderEntity entity = new MoveOrderEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where<MoveOrderEntity>("SnNum", ECondition.In, list.ToArray())
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.MoveOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }
    }
}
