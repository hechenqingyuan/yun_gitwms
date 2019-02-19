/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-30 17:23:28
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-30 17:23:28       情缘
*********************************************************************************/

using Git.Storage.Entity.InStorage;
using Git.Storage.Provider.Base;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Sys;
using Git.Storage.Entity.Storage;
using Git.Storage.Provider.Sys;

namespace Git.Storage.Provider.InStorage
{
    public partial class InStorageOrder : Bill<InStorageEntity, InStorDetailEntity>
    {
        public InStorageOrder(string CompanyID) 
        {
            this.CompanyID = CompanyID;
        }

        /// <summary>
        /// 创建单据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Create(InStorageEntity entity, List<InStorDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.OrderNum = entity.OrderNum.IsEmpty() ?
                    new SequenceProvider(this.CompanyID).GetSequence(typeof(InStorageEntity)) 
                    : entity.OrderNum;
                entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.SnNum;

                entity.IncludeAll();

                if (!list.IsNullOrEmpty())
                {
                    list.ForEach(a =>
                    {
                        a.SnNum = a.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : a.SnNum;
                        a.OrderSnNum = entity.SnNum;
                        a.CreateTime = DateTime.Now;
                        a.Amount = a.Num * a.InPrice;
                        a.CompanyID = entity.CompanyID;
                        a.IncludeAll();
                    });

                    entity.Num = list.Sum(q => q.Num);
                    entity.Amount = list.Sum(a => a.Amount);

                    line = this.InStorage.Add(entity);
                    line += this.InStorDetail.Add(list);
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
        public override string Cancel(InStorageEntity entity)
        {
            //只有待审核状态的单据才能取消，已经成功的订单不能取消
            InStorageEntity checkOrder = new InStorageEntity();
            checkOrder.Where(a => a.Status != (int)EAudite.Wait)
                .And(a=>a.IsDelete==(int)EIsDelete.NotDelete)
                .And(a => a.SnNum == entity.SnNum);
            if (this.InStorage.GetCount(checkOrder) > 0)
            {
                return EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Pass); //已经审核或者取消的订单不能审核
            }
            entity.Status = (int)EAudite.NotPass;
            entity.IncludeStatus(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID)
                ;
            int line = this.InStorage.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Delete(InStorageEntity entity)
        {
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            int line = this.InStorage.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 批量删除单据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Delete(IEnumerable<string> list)
        {
            InStorageEntity entity = new InStorageEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where<InStorageEntity>("SnNum", ECondition.In, list.ToArray())
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.InStorage.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 审核单据
        /// 审核不通过 以及审核通过的处理方式
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Audite(InStorageEntity entity)
        {
            //审核不通过 修改状态，修改不通过的理由
            if (entity.Status == (int)EAudite.NotPass)
            {
                entity.IncludeStatus(true)
                    .IncludeReason(true)
                    .Where(a => a.SnNum == entity.SnNum)
                    .And(a=>a.CompanyID==this.CompanyID)
                    ;
                int line = this.InStorage.Update(entity);
                return line > 0 ? "1000" : string.Empty;
            }
            else if (entity.Status == (int)EAudite.Pass)
            {
                Proc_AuditeInStorageEntity auditeEntity = new Proc_AuditeInStorageEntity();
                auditeEntity.OrderNum = entity.SnNum;
                auditeEntity.Status = entity.Status;
                auditeEntity.AuditUser = entity.AuditUser;
                auditeEntity.Reason = entity.Reason;
                auditeEntity.OperateType = entity.OperateType;
                auditeEntity.EquipmentNum = entity.EquipmentNum;
                auditeEntity.EquipmentCode = entity.EquipmentCode;
                auditeEntity.Remark = entity.Remark;
                auditeEntity.CompanyID = this.CompanyID;
                int line = this.Proc_AuditeInStorage.ExecuteNonQuery(auditeEntity);
                return auditeEntity.ReturnValue;
            }
            return string.Empty;
        }

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Print(InStorageEntity entity)
        {
            entity.IncludePrintUser(true).IncludePrintTime(true)
                .Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            int line = this.InStorage.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 查询单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override InStorageEntity GetOrder(InStorageEntity entity)
        {
            entity.IncludeAll();

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID)
                .And(a => a.SnNum == entity.SnNum);

            entity = this.InStorage.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 获得单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override List<InStorDetailEntity> GetOrderDetail(InStorDetailEntity entity)
        {
            InStorDetailEntity detail = new InStorDetailEntity();
            detail.IncludeAll();
            detail.Where(a => a.OrderSnNum == entity.OrderSnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            List<InStorDetailEntity> list = this.InStorDetail.GetList(detail);
            if (!list.IsNullOrEmpty())
            {
                List<LocationEntity> listLocation = new LocationProvider(this.CompanyID).GetList();
                listLocation = listLocation == null ? new List<LocationEntity>() : listLocation;
                List<ProductEntity> listProduct = new ProductProvider(this.CompanyID).GetList();
                listProduct = listProduct == null ? new List<ProductEntity>() : listProduct;
                foreach (InStorDetailEntity item in list)
                {
                    LocationEntity location = listLocation.FirstOrDefault(a => a.LocalNum == item.LocalNum);
                    item.LocalName = location == null ? "" : location.LocalName;
                    item.StorageName = location == null ? "" : location.StorageName;

                    ProductEntity product = listProduct.FirstOrDefault(a => a.SnNum == item.ProductNum);
                    item.Size = product.IsNull() ? string.Empty : product.Size;
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
        public override List<InStorageEntity> GetList(InStorageEntity entity, ref PageInfo pageInfo)
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
            if (entity.InType > 0)
            {
                entity.And(a => a.InType == entity.InType);
            }
            if (!entity.SupNum.IsEmpty())
            {
                entity.And("SupNum", ECondition.Like, "%" + entity.SupNum + "%");
            }
            if (!entity.SupName.IsEmpty())
            {
                entity.And("SupName", ECondition.Like, "%" + entity.SupName + "%");
            }
            if (!entity.Phone.IsEmpty())
            {
                entity.And("Phone", ECondition.Like, "%" + entity.Phone + "%");
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
                DateTime End = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now.Date).AddDays(1);
                entity.And(a => a.CreateTime <= End);
            }

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeUser = new AdminEntity();
            auditeUser.Include(a => new { AuditeUserName = a.UserName });
            entity.Left<AdminEntity>(auditeUser, new Params<string, string>() { Item1 = "AuditUser", Item2 = "UserNum" });

            int rowCount = 0;
            List<InStorageEntity> listResult = this.InStorage.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 查询单据详细数据分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<InStorDetailEntity> GetDetailList(InStorDetailEntity entity, ref PageInfo pageInfo)
        {
            InStorDetailEntity detail = new InStorDetailEntity();
            detail.IncludeAll();
            detail.OrderBy(a => a.ID, EOrderBy.DESC);
            detail.Where(a => a.CompanyID == this.CompanyID);
            
            if (!entity.BarCode.IsEmpty())
            {
                detail.And("BarCode", ECondition.Like, "%" + entity.BarCode + "%");
            }
            if (!entity.ProductName.IsEmpty())
            {
                detail.And("ProductName", ECondition.Like, "%" + entity.ProductName + "%");
            }
            if (!entity.StorageNum.IsEmpty())
            {
                detail.And(a => a.StorageNum == entity.StorageNum);
            }

            ProductEntity product = new ProductEntity();
            product.Include(item => new { Size=item.Size });
            detail.Left<ProductEntity>(product, new Params<string, string>() { Item1="ProductNum",Item2="SnNum" });

            InStorageEntity InOrder = new InStorageEntity();
            InOrder.Include(a => new { OrderNum = a.OrderNum, SupNum = a.SupNum, SupName = a.SupName, OrderTime = a.OrderTime, Status = a.Status, InType = a.InType, AuditeTime = a.AuditeTime });
            detail.Left<InStorageEntity>(InOrder, new Params<string, string>() { Item1 = "OrderSnNum", Item2 = "SnNum" });
            InOrder.And(a => a.IsDelete == (int)EIsDelete.NotDelete);
            if (!entity.SupNum.IsEmpty())
            {
                InOrder.AndBegin<InStorageEntity>()
                    .And<InStorageEntity>("SupNum", ECondition.Like, "%" + entity.SupNum + "%")
                    .Or<InStorageEntity>("SupName", ECondition.Like, "%" + entity.SupNum + "%")
                    .End<InStorageEntity>()
                    ;
            }
            if (!entity.SupName.IsEmpty())
            {
                InOrder.AndBegin<InStorageEntity>()
                    .And<InStorageEntity>("SupNum", ECondition.Like, "%" + entity.SupName + "%")
                    .Or<InStorageEntity>("SupName", ECondition.Like, "%" + entity.SupName + "%")
                    .End<InStorageEntity>()
                    ;
            }
            if (!entity.BeginTime.IsEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.BeginTime,DateTime.Now.Date.AddDays(-1));
                InOrder.And(a => a.CreateTime >= time);
            }
            if (!entity.EndTime.IsEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now.Date.AddDays(1));
                InOrder.And(a => a.CreateTime < time);
            }
            if (entity.Status > 0)
            {
                InOrder.And(item => item.Status == entity.Status);
            }
            if (entity.CreateUser.IsNotEmpty())
            {
                InOrder.And(item=>item.CreateUser==entity.CreateUser);
            }
            if (entity.InType > 0)
            {
                InOrder.And(item=>item.InType==entity.InType);
            }
            if (entity.OrderNum.IsNotEmpty())
            {
                InOrder.And("OrderNum", ECondition.Like,"%"+entity.OrderNum+"%");
            }
            if (entity.ContractOrder.IsNotEmpty())
            {
                InOrder.And("ContractOrder", ECondition.Like, "%" + entity.ContractOrder + "%");
            }
            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            InOrder.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeUser = new AdminEntity();
            auditeUser.Include(a => new { AuditeUserName = a.UserName });
            InOrder.Left<AdminEntity>(auditeUser, new Params<string, string>() { Item1 = "AuditUser", Item2 = "UserNum" });

            int rowCount = 0;
            List<InStorDetailEntity> listResult = this.InStorDetail.GetList(detail, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            if (!listResult.IsNullOrEmpty())
            {
                List<LocationEntity> listLocation = new LocationProvider(this.CompanyID).GetList();
                listLocation = listLocation == null ? new List<LocationEntity>() : listLocation;
                foreach (InStorDetailEntity item in listResult)
                {
                    LocationEntity location = listLocation.FirstOrDefault(a => a.LocalNum == item.LocalNum);
                    item.LocalName = location == null ? "" : location.LocalName;
                    item.StorageName = location == null ? "" : location.StorageName;
                }
            }
            return listResult;
        }

        /// <summary>
        /// 编辑单据信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditOrder(InStorageEntity entity)
        {
            entity.Include(a => new { a.InType,a.ProductType,a.StorageNum,a.SupNum,a.SupName,a.ContactName,a.Phone,a.Address,a.ContractOrder,a.ContractType,a.OperateType,a.EquipmentNum,a.EquipmentCode,a.Remark});
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            int line = this.InStorage.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 查询单据详细信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public override InStorDetailEntity GetDetail(string SnNum)
        {
            InStorDetailEntity entity = new InStorDetailEntity();
            entity.IncludeAll();
            entity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;

            ProductEntity product = new ProductEntity();
            product.Include(item => new { Size = item.Size });
            entity.Left<ProductEntity>(product, new Params<string, string>() { Item1 = "ProductNum", Item2 = "SnNum" });

            entity = this.InStorDetail.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 编辑单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditDetail(InStorDetailEntity entity)
        {
            entity.Include(a => new { a.ProductName,a.ProductNum,a.BarCode,a.BatchNum,a.Num,a.InPrice,a.Amount,a.ContractOrder,a.LocalNum,a.StorageNum});
            entity.Where(a => a.SnNum == entity.SnNum);
            int line = this.InStorDetail.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 编辑入库单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string EditOrder(InStorageEntity entity, List<InStorDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.Include(a => new { a.InType, a.ProductType, a.StorageNum, a.SupNum, a.SupName, a.ContactName, a.Phone, a.Address, a.ContractOrder, a.ContractType, a.Num, a.Amount, a.NetWeight, a.GrossWeight, a.OperateType, a.EquipmentNum, a.EquipmentCode, a.Remark });
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID);
                line += this.InStorage.Update(entity);

                InStorDetailEntity detail = new InStorDetailEntity();
                detail.IncludeAll();
                detail.Where(a => a.OrderSnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                List<InStorDetailEntity> listSource = this.InStorDetail.GetList(detail);
                listSource = listSource.IsNull() ? new List<InStorDetailEntity>() : listSource;
                
                //如果在原有的数据中存在修改，如果不存在新增
                list = list.IsNull() ? new List<InStorDetailEntity>() : list;
                foreach (InStorDetailEntity item in list)
                {
                    if (listSource.Exists(a => a.SnNum == item.SnNum))
                    {
                        item.Include(a => new { a.ProductName, a.ProductNum, a.BarCode, a.BatchNum, a.Num, a.InPrice, a.Amount, a.ContractOrder, a.LocalNum, a.StorageNum });
                        item.Where(a => a.SnNum == item.SnNum);
                        line += this.InStorDetail.Update(item);
                    }
                    else
                    {
                        item.IncludeAll();
                        item.OrderSnNum = entity.SnNum;
                        item.CreateTime = DateTime.Now;
                        line += this.InStorDetail.Add(item);
                    }
                }

                //如果数据库中的不存在了则删除
                foreach (InStorDetailEntity item in listSource)
                {
                    if (!list.Exists(a => a.SnNum == item.SnNum))
                    {
                        item.Where(a => a.SnNum == item.SnNum)
                            .And(a => a.CompanyID == this.CompanyID)
                            ;
                        line += this.InStorDetail.Delete(item);
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
        public override int GetCount(InStorageEntity entity)
        {
            entity.Where(a => a.CompanyID == this.CompanyID)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                ;

            if (!entity.OrderNum.IsEmpty())
            {
                entity.Where("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(a => a.StorageNum == entity.StorageNum);
            }
            if (entity.InType > 0)
            {
                entity.And(a => a.InType == entity.InType);
            }
            if (!entity.SupNum.IsEmpty())
            {
                entity.And("SupNum", ECondition.Like, "%" + entity.SupNum + "%");
            }
            if (!entity.SupName.IsEmpty())
            {
                entity.And("SupName", ECondition.Like, "%" + entity.SupName + "%");
            }
            if (!entity.Phone.IsEmpty())
            {
                entity.And("Phone", ECondition.Like, "%" + entity.Phone + "%");
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
                DateTime End = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now.Date).AddDays(1);
                entity.And(a => a.CreateTime <= End);
            }

            return this.InStorage.GetCount(entity);
        }

        /// <summary>
        /// 获得打印单据的数据源
        /// </summary>
        /// <param name="argOrderNum"></param>
        /// <returns></returns>
        public override DataSet GetPrint(string argOrderNum)
        {
            DataSet ds = new DataSet();
            InStorageEntity entity = new InStorageEntity();
            entity.SnNum = argOrderNum;
            entity = GetOrder(entity);
            if (entity != null)
            {
                List<InStorageEntity> list = new List<InStorageEntity>();
                list.Add(entity);
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                InStorDetailEntity detail = new InStorDetailEntity();
                detail.OrderSnNum = argOrderNum;
                List<InStorDetailEntity> listDetail = GetOrderDetail(detail);
                listDetail = listDetail.IsNull() ? new List<InStorDetailEntity>() : listDetail;
                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            else
            {
                List<InStorageEntity> list = new List<InStorageEntity>();
                List<InStorDetailEntity> listDetail = new List<InStorDetailEntity>();
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            return ds;
        }
    }
}
