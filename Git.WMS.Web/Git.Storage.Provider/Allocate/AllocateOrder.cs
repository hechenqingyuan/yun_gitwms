/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-11 10:23:41
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-11 10:23:41       情缘
*********************************************************************************/

using Git.Storage.Entity.Allocate;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Git.Storage.Provider.Base;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Sys;
using Git.Storage.Provider.Sys;
using Git.Storage.Entity.Storage;
using System.Data;

namespace Git.Storage.Provider.Allocate
{
    public partial class AllocateOrder : Bill<AllocateOrderEntity, AllocateDetailEntity>
    {
        public AllocateOrder(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }


        /// <summary>
        /// 创建单据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Create(AllocateOrderEntity entity, List<AllocateDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.SnNum = ConvertHelper.NewGuid();
                entity.OrderNum = entity.OrderNum.IsEmpty() ? new SequenceProvider(this.CompanyID).GetSequence(typeof(AllocateOrderEntity)) : entity.OrderNum;
                entity.CreateTime = DateTime.Now;
                entity.IncludeAll();

                if (!list.IsNullOrEmpty())
                {
                    string StorageNum = list[0].ToStorageNum;
                    LocationProvider locationProvider = new LocationProvider(this.CompanyID);
                    List<LocationEntity> listLocation = locationProvider.GetList(StorageNum);
                    if (listLocation.IsNullOrEmpty())
                    {
                        return "1001";
                    }
                    LocationEntity location = listLocation.FirstOrDefault(a => a.LocalType == (int)ELocalType.WaitIn);
                    if (location.IsNull())
                    {
                        return "1002";
                    }

                    list.ForEach(a =>
                    {
                        a.OrderNum = entity.OrderNum;
                        a.OrderSnNum = entity.SnNum;
                        a.CompanyID = this.CompanyID;
                        a.CreateTime = DateTime.Now;
                        a.ToLocalNum = location.LocalNum;
                        a.IncludeAll();
                    });

                    entity.Num = list.Sum(q => q.Num);
                    entity.Amount = list.Sum(a => a.Amount);

                    line = this.AllocateOrder.Add(entity);
                    line += this.AllocateDetail.Add(list);
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
        public override string Cancel(AllocateOrderEntity entity)
        {
            //只有待审核状态的单据才能取消，已经成功的订单不能取消
            AllocateOrderEntity checkOrder = new AllocateOrderEntity();
            entity.Where(a => a.Status == (int)EAudite.Wait)
                .And(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            if (this.AllocateOrder.GetCount(checkOrder) > 0)
            {
                return EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Pass); //已经审核或者取消的订单不能审核
            }
            entity.Status = (int)EAudite.NotPass;
            entity.IncludeStatus(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.AllocateOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Delete(AllocateOrderEntity entity)
        {
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.AllocateOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Audite(AllocateOrderEntity entity)
        {
            if (entity.Status == (int)EAudite.NotPass)
            {
                entity.IncludeStatus(true).IncludeReason(true).IncludeAuditUser(true);
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                int line = this.AllocateOrder.Update(entity);
                return line > 0 ? "1000" : string.Empty;
            }
            else if (entity.Status == (int)EAudite.Pass)
            {
                Proc_AuditeAllocateEntity auditeEntity = new Proc_AuditeAllocateEntity();
                auditeEntity.OrderNum = entity.SnNum;
                auditeEntity.Status = entity.Status;
                auditeEntity.AuditUser = entity.AuditUser;
                auditeEntity.Reason = entity.Reason;
                auditeEntity.OperateType = entity.OperateType;
                auditeEntity.EquipmentNum = entity.EquipmentNum;
                auditeEntity.EquipmentCode = entity.EquipmentCode;
                auditeEntity.CompanyID = this.CompanyID;
                int line = this.Proc_AuditeAllocate.ExecuteNonQuery(auditeEntity);
                return auditeEntity.ReturnValue;
            }
            return string.Empty;
        }

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Print(AllocateOrderEntity entity)
        {
            entity.IncludePrintUser(true).IncludePrintTime(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.AllocateOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 查询单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override AllocateOrderEntity GetOrder(AllocateOrderEntity entity)
        {
            entity.IncludeAll();

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeAdmin = new AdminEntity();
            auditeAdmin.Include(a => new { AuditUserName = a.UserName });
            entity.Left<AdminEntity>(auditeAdmin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;

            entity = this.AllocateOrder.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 获得单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override List<AllocateDetailEntity> GetOrderDetail(AllocateDetailEntity entity)
        {
            AllocateDetailEntity detail = new AllocateDetailEntity();
            detail.IncludeAll();
            detail.Where(a => a.OrderSnNum == entity.OrderSnNum)
                .And(a => a.CompanyID == this.CompanyID);

            ProductEntity product = new ProductEntity();
            product.Include(a => new { Size = a.Size });
            detail.Left<ProductEntity>(product, new Params<string, string>() { Item1 = "ProductNum", Item2 = "SnNum" });

            List<AllocateDetailEntity> list = this.AllocateDetail.GetList(detail);
            if (!list.IsNullOrEmpty())
            {
                List<LocationEntity> listLocation = new LocationProvider(this.CompanyID).GetList();
                listLocation = listLocation == null ? new List<LocationEntity>() : listLocation;
                foreach (AllocateDetailEntity item in list)
                {
                    LocationEntity location = listLocation.FirstOrDefault(a => a.LocalNum == item.FromLocalNum);
                    item.FromLocalName = location == null ? "" : location.LocalName;
                    item.FromStorageName = location == null ? "" : location.StorageName;

                    location = listLocation.FirstOrDefault(a => a.LocalNum == item.ToLocalNum);
                    item.ToLocalName = location == null ? "" : location.LocalName;
                    item.ToStorageName = location == null ? "" : location.StorageName;
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
        public override List<AllocateOrderEntity> GetList(AllocateOrderEntity entity, ref Framework.DataTypes.PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            entity.OrderBy(a => a.ID, EOrderBy.DESC);

            if (!entity.OrderNum.IsEmpty())
            {
                entity.Where("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }

            if (entity.AllocateType > 0)
            {
                entity.And(a => a.AllocateType == entity.AllocateType);
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
                DateTime Begin = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.Date.AddMonths(-1));
                entity.And(a => a.CreateTime >= Begin);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime End = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now.Date).AddDays(1).Date;
                entity.And(a => a.CreateTime <= End);
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(a => a.StorageNum == entity.StorageNum);
            }

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            int rowCount = 0;
            List<AllocateOrderEntity> listResult = this.AllocateOrder.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 查询单据详细数据分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<AllocateDetailEntity> GetDetailList(AllocateDetailEntity entity, ref Framework.DataTypes.PageInfo pageInfo)
        {
            AllocateDetailEntity detail = new AllocateDetailEntity();
            detail
                .And(a => a.CompanyID == this.CompanyID);

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

            AllocateOrderEntity allocateOrder = new AllocateOrderEntity();
            allocateOrder.Include(item => new { Status = item.Status, AllocateType = item.AllocateType, AuditeTime = item.AuditeTime });
            detail.Left<AllocateOrderEntity>(allocateOrder, new Params<string, string>() { Item1 = "OrderSnNum", Item2 = "SnNum" });
            allocateOrder.And(item => item.IsDelete == (int)EIsDelete.NotDelete);
            if (entity.OrderNum.IsNotEmpty())
            {
                allocateOrder.And("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.Status > 0)
            {
                allocateOrder.And(item => item.Status == entity.Status);
            }
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime begin = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.AddDays(-30)).Date;
                allocateOrder.And(item => item.CreateTime >= begin);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime end = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).AddDays(1).Date;
                allocateOrder.And(item => item.CreateTime < end);
            }
            if (entity.AllocateType > 0)
            {
                allocateOrder.And(item => item.AllocateType == entity.AllocateType);
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                allocateOrder.And(item => item.StorageNum == entity.StorageNum);
            }

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            allocateOrder.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeAdmin = new AdminEntity();
            auditeAdmin.Include(a => new { AuditUserName = a.UserName });
            allocateOrder.Left<AdminEntity>(auditeAdmin, new Params<string, string>() { Item1 = "AuditUser", Item2 = "UserNum" });


            int rowCount = 0;
            List<AllocateDetailEntity> listResult = this.AllocateDetail.GetList(detail, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            if (!listResult.IsNullOrEmpty())
            {
                List<LocationEntity> listLocation = new LocationProvider(this.CompanyID).GetList();
                listLocation = listLocation == null ? new List<LocationEntity>() : listLocation;
                foreach (AllocateDetailEntity item in listResult)
                {
                    LocationEntity location = listLocation.FirstOrDefault(a => a.LocalNum == item.FromLocalNum);
                    item.FromLocalName = location == null ? "" : location.LocalName;
                    item.FromStorageName = location == null ? "" : location.StorageName;

                    location = listLocation.FirstOrDefault(a => a.LocalNum == item.ToLocalNum);
                    item.ToLocalName = location == null ? "" : location.LocalName;
                    item.ToStorageName = location == null ? "" : location.StorageName;
                }
            }
            return listResult;
        }

        /// <summary>
        /// 编辑单据信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditOrder(AllocateOrderEntity entity)
        {
            entity.Include(a => new
            {
                a.AllocateType,
                a.ProductType,
                a.StorageNum,
                a.ContractOrder,
                a.Remark,
                a.Num,
                a.Amount,
                a.Weight
            });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.AllocateOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 查询单据详细信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public override AllocateDetailEntity GetDetail(string SnNum)
        {
            AllocateDetailEntity entity = new AllocateDetailEntity();
            entity.IncludeAll();
            entity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;

            entity = this.AllocateDetail.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 编辑单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditDetail(AllocateDetailEntity entity)
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
                a.FromStorageNum,
                a.FromLocalNum,
                a.ToStorageNum,
                a.ToLocalNum
            });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.AllocateDetail.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 获得订单数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int GetCount(AllocateOrderEntity entity)
        {
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            if (!entity.OrderNum.IsEmpty())
            {
                entity.Where("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.AllocateType > 0)
            {
                entity.And(a => a.AllocateType == entity.AllocateType);
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
                DateTime Begin = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.Date.AddMonths(-1));
                entity.And(a => a.CreateTime >= Begin);
            }
            if (entity.EndTime.IsNotEmpty())
            {
                DateTime End = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now.Date).AddDays(1).Date;
                entity.And(a => a.CreateTime <= End);
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(a => a.StorageNum == entity.StorageNum);
            }
            return this.AllocateOrder.GetCount(entity);
        }

        /// <summary>
        /// 编辑报损单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string EditOrder(AllocateOrderEntity entity, List<AllocateDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.Include(a => new
                {
                    a.AllocateType,
                    a.ProductType,
                    a.StorageNum,
                    a.ContractOrder,
                    a.Remark,
                    a.Num,
                    a.Amount,
                    a.Weight
                });
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;

                AllocateDetailEntity detail = new AllocateDetailEntity();
                detail.Where(a => a.OrderSnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                this.AllocateDetail.Delete(detail);

                if (!list.IsNullOrEmpty())
                {
                    string StorageNum = list[0].ToStorageNum;
                    LocationProvider locationProvider = new LocationProvider(this.CompanyID);
                    List<LocationEntity> listLocation = locationProvider.GetList(StorageNum);
                    if (listLocation.IsNullOrEmpty())
                    {
                        return "1001";
                    }
                    LocationEntity location = listLocation.FirstOrDefault(a => a.LocalType == (int)ELocalType.WaitIn);
                    if (location.IsNull())
                    {
                        return "1002";
                    }

                    foreach (AllocateDetailEntity item in list)
                    {
                        item.OrderNum = entity.OrderNum;
                        item.OrderSnNum = entity.SnNum;
                        item.CompanyID = this.CompanyID;
                        item.ToLocalNum = location.LocalNum;
                        item.CreateTime = DateTime.Now;
                        item.IncludeAll();
                    }
                }
                
                entity.Num = list.Sum(a => a.Num);
                entity.Amount = list.Sum(a => a.Amount);

                line = this.AllocateOrder.Update(entity);
                this.AllocateDetail.Add(list);

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
            AllocateOrderEntity entity = new AllocateOrderEntity();
            entity.SnNum = argOrderNum;
            entity = GetOrder(entity);
            if (entity != null)
            {
                List<AllocateOrderEntity> list = new List<AllocateOrderEntity>();
                list.Add(entity);
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                AllocateDetailEntity detail = new AllocateDetailEntity();
                detail.OrderSnNum = argOrderNum;
                List<AllocateDetailEntity> listDetail = GetOrderDetail(detail);
                if (!listDetail.IsNullOrEmpty())
                {
                    DataTable tableDetail = listDetail.ToDataTable();
                    ds.Tables.Add(tableDetail);
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
            AllocateOrderEntity entity = new AllocateOrderEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where<AllocateOrderEntity>("SnNum", ECondition.In, list.ToArray())
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.AllocateOrder.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }
    }
}
