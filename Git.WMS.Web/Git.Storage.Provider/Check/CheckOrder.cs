/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-13 15:58:29
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-13 15:58:29       情缘
*********************************************************************************/

using Git.Storage.Entity.Check;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Git.Storage.Common.Enum;
using System.Data;
using Git.Storage.Entity.Sys;
using Git.Storage.Entity.Storage;
using Git.Storage.Provider.Sys;

namespace Git.Storage.Provider.Check
{
    public partial class CheckOrder : Bill<InventoryOrderEntity, InventoryDetailEntity>
    {
        public CheckOrder(string CompanyID)
        {
            this.CompanyID = CompanyID;
        }

        /// <summary>
        /// 创建单据
        /// 1001：存在未完成的盘点作业
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Create(InventoryOrderEntity entity, List<InventoryDetailEntity> list)
        {
            //检查是否存在未完成的
            InventoryOrderEntity checkEntity = new InventoryOrderEntity();
            checkEntity.Where(a => a.StorageNum == entity.StorageNum)
                .And(a=>a.IsDelete==(int)EIsDelete.NotDelete)
                .And(a=>a.Status!=(int)EAudite.Pass)
                .And(a => a.CompanyID == this.CompanyID)
                ;

            int count = this.InventoryOrder.GetCount(checkEntity);
            if (count > 0)
            {
                return "1001";
            }

            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;

                entity.OrderNum = entity.OrderNum.IsEmpty() ?
                    new TNumProvider(this.CompanyID).GetSwiftNum(typeof(InventoryOrderEntity), 5)
                    : entity.OrderNum;
                entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.SnNum;

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

                    line = this.InventoryOrder.Add(entity);
                    line += this.InventoryDetail.Add(list);

                    //新建盘点单还要生成盘点差异单
                    Proc_CreateCheckEntity procEntity = new Proc_CreateCheckEntity();
                    procEntity.OrderNum = entity.SnNum;
                    procEntity.CreateUser = entity.CreateUser;
                    procEntity.CompanyID = this.CompanyID;
                    line += this.Proc_CreateCheck.ExecuteNonQuery(procEntity);
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
        public override string Cancel(InventoryOrderEntity entity)
        {
            //只有待审核状态的单据才能取消，已经成功的订单不能取消
            InventoryOrderEntity checkOrder = new InventoryOrderEntity();
            checkOrder.Where(a => a.Status != (int)EAudite.Wait)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.SnNum == entity.SnNum);
            if (this.InventoryOrder.GetCount(checkOrder) > 0)
            {
                return EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Pass); //已经审核或者取消的订单不能审核
            }
            entity.Status = (int)EAudite.NotPass;
            entity.IncludeStatus(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.InventoryOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Delete(InventoryOrderEntity entity)
        {
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.InventoryOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 批量删除单据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Delete(IEnumerable<string> list)
        {
            InventoryOrderEntity entity = new InventoryOrderEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where<InventoryOrderEntity>("SnNum", ECondition.In, list.ToArray())
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.InventoryOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 审核单据
        /// 审核不通过 以及审核通过的处理方式
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Audite(InventoryOrderEntity entity)
        {
            //审核不通过 修改状态，修改不通过的理由
            if (entity.Status == (int)EAudite.NotPass)
            {
                entity.IncludeStatus(true)
                    .IncludeReason(true)
                    .Where(a => a.OrderNum == entity.OrderNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                int line = this.InventoryOrder.Update(entity);
                return line > 0 ? "1000" : string.Empty;
            }
            else if (entity.Status == (int)EAudite.Pass)
            {
                Proc_AuditeCheckEntity auditeEntity = new Proc_AuditeCheckEntity();
                auditeEntity.OrderNum = entity.SnNum;
                auditeEntity.Status = entity.Status;
                auditeEntity.AuditUser = entity.AuditUser;
                auditeEntity.Reason = entity.Reason;
                auditeEntity.OperateType = entity.OperateType;
                auditeEntity.EquipmentNum = entity.EquipmentNum;
                auditeEntity.EquipmentCode = entity.EquipmentCode;
                auditeEntity.CompanyID = this.CompanyID;
                int line = this.Proc_AuditeCheck.ExecuteNonQuery(auditeEntity);
                return auditeEntity.ReturnValue;
            }
            return string.Empty;
        }

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Print(InventoryOrderEntity entity)
        {
            entity.IncludePrintUser(true).IncludePrintTime(true)
                .Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.InventoryOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 查询单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override InventoryOrderEntity GetOrder(InventoryOrderEntity entity)
        {
            entity.IncludeAll();
            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                .And(a => a.SnNum == entity.SnNum);
            entity = this.InventoryOrder.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 获得单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override List<InventoryDetailEntity> GetOrderDetail(InventoryDetailEntity entity)
        {
            InventoryDetailEntity detail = new InventoryDetailEntity();
            detail.IncludeAll();
            detail.Where(a => a.OrderSnNum == entity.OrderSnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;

            ProductEntity product = new ProductEntity();
            product.Include(a => new { a.ProductName,a.BarCode,a.UnitName,a.CateName,a.Size });

            detail.Left<ProductEntity>(product, new Params<string, string>() { Item1 = "TargetNum", Item2 = "SnNum" });

            List<InventoryDetailEntity> list = this.InventoryDetail.GetList(detail);
            return list;
        }

        /// <summary>
        /// 查询单据分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<InventoryOrderEntity> GetList(InventoryOrderEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID);
            entity.OrderBy(a => a.ID, EOrderBy.DESC);

            if (!entity.OrderNum.IsEmpty())
            {
                entity.Where("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(a => a.StorageNum == entity.StorageNum);
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
                DateTime Begin = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.AddMonths(-10)).Date;
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

            AdminEntity auditeUser = new AdminEntity();
            auditeUser.Include(a => new { AuditeUserName = a.UserName });
            entity.Left<AdminEntity>(auditeUser, new Params<string, string>() { Item1 = "AuditUser", Item2 = "UserNum" });

            int rowCount = 0;
            List<InventoryOrderEntity> listResult = this.InventoryOrder.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 查询单据详细数据分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<InventoryDetailEntity> GetDetailList(InventoryDetailEntity entity, ref PageInfo pageInfo)
        {
            InventoryDetailEntity detail = new InventoryDetailEntity();
            detail.Where(a => a.OrderSnNum == entity.OrderSnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            detail.IncludeAll();
            detail.OrderBy(a => a.ID, EOrderBy.DESC);
            int rowCount = 0;
            List<InventoryDetailEntity> listResult = this.InventoryDetail.GetList(detail, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            if (!listResult.IsNullOrEmpty())
            {
                List<LocationEntity> listLocation = new LocationProvider(this.CompanyID).GetList();
                listLocation = listLocation == null ? new List<LocationEntity>() : listLocation;
            }
            return listResult;
        }

        /// <summary>
        /// 编辑单据信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditOrder(InventoryOrderEntity entity)
        {
            entity.Include(a => new 
            {
                a.ContractOrder,a.Remark
            });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.InventoryOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;

        }

        /// <summary>
        /// 查询单据详细信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public override InventoryDetailEntity GetDetail(string SnNum)
        {
            InventoryDetailEntity entity = new InventoryDetailEntity();
            entity.IncludeAll();
            entity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;

            entity = this.InventoryDetail.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 编辑单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditDetail(InventoryDetailEntity entity)
        {
            entity.Include(a => a.TargetNum);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.InventoryDetail.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 编辑入库单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string EditOrder(InventoryOrderEntity entity, List<InventoryDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.Include(a => new
                {
                    a.ContractOrder,
                    a.Remark
                });
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID);
                line = this.InventoryOrder.Update(entity);

                InventoryDetailEntity detail = new InventoryDetailEntity();
                detail.Where(a => a.OrderSnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                line += this.InventoryDetail.Delete(detail);

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

                    line += this.InventoryDetail.Add(list);

                    //新建盘点单还要生成盘点差异单
                    Proc_CreateCheckEntity procEntity = new Proc_CreateCheckEntity();
                    procEntity.OrderNum = entity.SnNum;
                    procEntity.CreateUser = entity.CreateUser;
                    procEntity.CompanyID = this.CompanyID;
                    line += this.Proc_CreateCheck.ExecuteNonQuery(procEntity);
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
        public override int GetCount(InventoryOrderEntity entity)
        {
            entity.Where(a => a.CompanyID == this.CompanyID)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                ;
            return this.InventoryOrder.GetCount(entity);
        }

        /// <summary>
        /// 获得打印单据的数据源
        /// </summary>
        /// <param name="argOrderNum"></param>
        /// <returns></returns>
        public override DataSet GetPrint(string argOrderNum)
        {
            DataSet ds = new DataSet();
            InventoryOrderEntity entity = new InventoryOrderEntity();
            entity.SnNum = argOrderNum;
            entity = GetOrder(entity);
            if (entity != null)
            {
                List<InventoryOrderEntity> list = new List<InventoryOrderEntity>();
                list.Add(entity);
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                InventoryDetailEntity detail = new InventoryDetailEntity();
                detail.OrderSnNum = argOrderNum;
                List<InventoryDetailEntity> listDetail = GetOrderDetail(detail);
                listDetail = listDetail.IsNull() ? new List<InventoryDetailEntity>() : listDetail;
                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            else
            {
                List<InventoryOrderEntity> list = new List<InventoryOrderEntity>();
                List<InventoryDetailEntity> listDetail = new List<InventoryDetailEntity>();
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            return ds;
        }
    }
}
