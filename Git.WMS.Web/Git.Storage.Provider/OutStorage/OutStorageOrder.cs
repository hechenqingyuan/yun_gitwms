/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-04-26 17:37:15
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-04-26 17:37:15       情缘
*********************************************************************************/

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
using Git.Storage.Entity.OutStorage;

namespace Git.Storage.Provider.OutStorage
{
    public partial class OutStorageOrder : Bill<OutStorageEntity, OutStoDetailEntity>
    {
        public OutStorageOrder(string _CompanyID) 
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 创建单据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Create(OutStorageEntity entity, List<OutStoDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                entity.SnNum =entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid():entity.SnNum;
                entity.OrderNum = entity.OrderNum.IsEmpty() ?
                    new SequenceProvider(this.CompanyID).GetSequence(typeof(OutStorageEntity))
                    : entity.OrderNum;

                list.ForEach(a =>
                {
                    a.SnNum = a.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : a.SnNum;
                    a.OrderSnNum = entity.SnNum;
                    a.OrderNum = entity.OrderNum;
                    a.Amount = a.Amount == 0 ? a.OutPrice * a.Num : a.Amount;
                    a.CreateTime = DateTime.Now;
                    a.CompanyID = this.CompanyID;
                    a.IncludeAll();
                });
                
                entity.Num = list.Sum(a => a.Num);
                entity.Amount = list.Sum(a => a.Amount);

                entity.IncludeAll();
                int line = this.OutStorage.Add(entity);
                line += this.OutStoDetail.Add(list);
                ts.Complete();
                return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
            }
        }

        /// <summary>
        /// 取消单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Cancel(OutStorageEntity entity)
        {
            OutStorageEntity checkOrder = new OutStorageEntity();
            entity.Where(a => a.Status == (int)EAudite.Wait)
                .And(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            if (this.OutStorage.GetCount(checkOrder) > 0)
            {
                return EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Pass); //已经审核或者取消的订单不能审核
            }
            entity.Status = (int)EAudite.NotPass;
            entity.IncludeStatus(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID)
                ;
            int line = this.OutStorage.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Delete(OutStorageEntity entity)
        {
            OutStorageEntity item = new OutStorageEntity();
            item.IsDelete = (int)EIsDelete.Deleted;
            item.IncludeIsDelete(true);
            item.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.OutStorage.Update(item);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Audite(OutStorageEntity entity)
        {
            if (entity.Status == (int)EAudite.NotPass)
            {
                entity.IncludeStatus(true).IncludeReason(true)
                    .Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                int line = this.OutStorage.Update(entity);
                return line > 0 ? "1000" : string.Empty;
            }
            else if (entity.Status == (int)EAudite.Pass)
            {
                Proc_AuditeOutStorageEntity auditeEntity = new Proc_AuditeOutStorageEntity();
                auditeEntity.OrderNum = entity.SnNum;
                auditeEntity.Status = entity.Status;
                auditeEntity.AuditUser = entity.AuditUser;
                auditeEntity.Reason = entity.Reason;
                auditeEntity.OperateType = entity.OperateType;
                auditeEntity.EquipmentNum = entity.EquipmentNum;
                auditeEntity.EquipmentCode = entity.EquipmentCode;
                auditeEntity.Remark = entity.Remark;
                auditeEntity.CompanyID = this.CompanyID;
                int line = this.Proc_AuditeOutStorage.ExecuteNonQuery(auditeEntity);

                /***
                 * 如果是销售订单则需要更改订单中的发货数量以及相应的状态
                 * 1. 查询出库单，判断出库单的类型
                 * 2. 查询出库单所有的内容详细，判断存在哪些订单
                 * 3. 统计相应的订单的出库总数量
                 * 4. 修改订单状态
                 * */
                //OutStorageEntity outEntity = new OutStorageEntity();
                //outEntity.IncludeAll();
                //outEntity.Where(a => a.OrderNum == entity.OrderNum);
                //outEntity = this.OutStorage.GetSingle(outEntity);
                //OutStoDetailEntity detail = new OutStoDetailEntity();
                //detail.Where(a => a.OrderNum == entity.OrderNum);
                //detail.IncludeAll();
                //List<OutStoDetailEntity> listDetail = this.OutStoDetail.GetList(detail);
                //if (outEntity != null && !listDetail.IsNullOrEmpty())
                //{
                //    if (outEntity.OutType == (int)EOutType.Sell)
                //    {
                //        OutStorageProvider outProvider = new OutStorageProvider();
                //        foreach (var item in listDetail.Where(a => !a.ContractOrder.IsEmpty() && !a.ContractSn.IsEmpty()).GroupBy(a => new { a.ContractOrder, a.ContractSn }))
                //        {
                //            OutStoDetailEntity tempOutDetail = new OutStoDetailEntity();
                //            tempOutDetail.Where(a => a.ContractOrder == item.Key.ContractOrder).And(a => a.ContractSn == item.Key.ContractSn);
                //            List<OutStoDetailEntity> list = outProvider.GetOrderDetail(tempOutDetail);
                //            double value = list.Sum(a => a.Num);

                //            OrderDetailEntity orderDetail = new OrderDetailEntity();
                //            orderDetail.RealNum = value;
                //            orderDetail.IncludeRealNum(true);
                //            orderDetail.Where(a => a.SnNum == item.Key.ContractSn).And(a => a.OrderNum == item.Key.ContractOrder);
                //            this.OrderDetail.Update(orderDetail);

                //            orderDetail = new OrderDetailEntity();
                //            orderDetail.IncludeAll();
                //            orderDetail.And(a => a.OrderNum == item.Key.ContractOrder);
                //            List<OrderDetailEntity> listOrderDetail = this.OrderDetail.GetList(orderDetail);
                //            bool flag = true;
                //            foreach (OrderDetailEntity detailItem in listOrderDetail)
                //            {
                //                if (detailItem.RealNum < detailItem.Num)
                //                {
                //                    flag = false;
                //                }
                //            }

                //            OrdersEntity order = new OrdersEntity();
                //            if (flag)
                //            {
                //                order.Status = (int)EOrderStatus.AllDelivery;
                //            }
                //            else
                //            {
                //                order.Status = (int)EOrderStatus.PartialDelivery;
                //            }
                //            order.IncludeStatus(true);
                //            order.Where(a => a.OrderNum == item.Key.ContractOrder);
                //            this.Orders.Update(order);
                //        }
                //    }
                //}
                //return auditeEntity.ReturnValue;

            }
            return string.Empty;
        }

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Print(OutStorageEntity entity)
        {
            entity.IncludePrintUser(true).IncludePrintTime(true)
                .Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID)
                ;
            int line = this.OutStorage.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 查询单据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override OutStorageEntity GetOrder(OutStorageEntity entity)
        {
            entity.IncludeAll();

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeAdmin = new AdminEntity();
            auditeAdmin.Include(item => new { AuditeUserName =item.UserName});
            entity.Left<AdminEntity>(auditeAdmin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                .And(a=>a.IsDelete==(int)EIsDelete.NotDelete)
                ;
            entity = this.OutStorage.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 获得单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override List<OutStoDetailEntity> GetOrderDetail(OutStoDetailEntity entity)
        {
            OutStoDetailEntity detail = new OutStoDetailEntity();
            detail.IncludeAll();
            detail.Where(a => a.OrderSnNum == entity.OrderSnNum)
                .And(a=>a.CompanyID==this.CompanyID)
                ;
            ProductEntity product = new ProductEntity();
            product.Include(a => new { a.Size });
            detail.Left<ProductEntity>(product, new Params<string, string>() { Item1 = "ProductNum", Item2 = "SnNum" });

            List<OutStoDetailEntity> list = this.OutStoDetail.GetList(detail);
            if (!list.IsNullOrEmpty())
            {
                List<LocationEntity> listLocation = new LocationProvider(this.CompanyID).GetList();
                listLocation = listLocation == null ? new List<LocationEntity>() : listLocation;
                foreach (OutStoDetailEntity item in list)
                {
                    LocationEntity location = listLocation.FirstOrDefault(a => a.LocalNum == item.LocalNum);
                    item.LocalName = location == null ? "" : location.LocalName;
                    item.StorageName = location == null ? "" : location.StorageName;
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
        public override List<OutStorageEntity> GetList(OutStorageEntity entity, ref Framework.DataTypes.PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a=>a.CompanyID==this.CompanyID)
                ;
            entity.OrderBy(a => a.ID, EOrderBy.DESC);

            if (!entity.OrderNum.IsEmpty())
            {
                entity.Where("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(a => a.StorageNum == entity.StorageNum);
            }
            if (entity.OutType > 0)
            {
                entity.And(a => a.OutType == entity.OutType);
            }
            if (!entity.CusNum.IsEmpty())
            {
                entity.And("CusNum", ECondition.Like, "%" + entity.CusNum + "%");
            }
            if (!entity.CusName.IsEmpty())
            {
                entity.And("CusName", ECondition.Like, "%" + entity.CusName + "%");
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
            if (!entity.CarrierNum.IsEmpty())
            {
                entity.And(item => item.CarrierNum == entity.CarrierNum);
            }
            if (!entity.CarrierName.IsEmpty())
            {
                entity.And("CarrierName", ECondition.Like, "%" + entity.CarrierName + "%");
            }
            if (!entity.LogisticsNo.IsEmpty())
            {
                entity.And("LogisticsNo", ECondition.Like, "%" + entity.LogisticsNo + "%");
            }

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeAdmin = new AdminEntity();
            auditeAdmin.Include(a => new { AuditeUserName = a.UserName });
            entity.Left<AdminEntity>(auditeAdmin, new Params<string, string>() { Item1 = "AuditUser", Item2 = "UserNum" });

            int rowCount = 0;
            List<OutStorageEntity> listResult = this.OutStorage.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 查询单据详细数据分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<OutStoDetailEntity> GetDetailList(OutStoDetailEntity entity, ref Framework.DataTypes.PageInfo pageInfo)
        {
            OutStoDetailEntity detail = new OutStoDetailEntity();
            detail.IncludeAll();
            detail.OrderBy(a => a.ID, EOrderBy.DESC);
            detail.And(a=>a.CompanyID==this.CompanyID);
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

            OutStorageEntity OutOrder = new OutStorageEntity();
            detail.Left<OutStorageEntity>(OutOrder, new Params<string, string>() { Item1 = "OrderSnNum", Item2 = "SnNum" });
            OutOrder.Include(a => new { CusNum = a.CusNum, CusName = a.CusName, SendDate = a.SendDate, Status = a.Status, OutType = a.OutType, AuditeTime = a.AuditeTime, CarrierName = a.CarrierName, LogisticsNo = a.LogisticsNo });
            OutOrder.And(a => a.IsDelete == (int)EIsDelete.NotDelete);
            if (!entity.CusNum.IsEmpty())
            {
                OutOrder.AndBegin<OutStorageEntity>()
                    .And<OutStorageEntity>("CusNum", ECondition.Like, "%" + entity.CusNum + "%")
                    .Or<OutStorageEntity>("CusName", ECondition.Like, "%" + entity.CusNum + "%")
                    .End<OutStorageEntity>()
                    ;
            }
            if (!entity.CusName.IsEmpty())
            {
                OutOrder.AndBegin<OutStorageEntity>()
                    .And<OutStorageEntity>("CusNum", ECondition.Like, "%" + entity.CusName + "%")
                    .Or<OutStorageEntity>("CusName", ECondition.Like, "%" + entity.CusName + "%")
                    .End<OutStorageEntity>()
                    ;
            }
            if (!entity.BeginTime.IsEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.BeginTime, DateTime.Now.Date.AddDays(30)).Date;
                OutOrder.And(a => a.CreateTime >= time);
            }
            if (!entity.EndTime.IsEmpty())
            {
                DateTime time = ConvertHelper.ToType<DateTime>(entity.EndTime, DateTime.Now).Date.AddDays(1);
                OutOrder.And(a => a.CreateTime < time);
            }
            if (entity.Status > 0)
            {
                OutOrder.And(item=>item.Status==entity.Status);
            }
            if (entity.OutType > 0)
            {
                OutOrder.And(item=>item.OutType==entity.OutType);
            }
            if(entity.OrderNum.IsNotEmpty())
            {
                OutOrder.And("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (!entity.CarrierNum.IsEmpty())
            {
                OutOrder.And(item => item.CarrierNum==entity.CarrierNum);
            }
            if (!entity.CarrierName.IsEmpty())
            {
                OutOrder.And("CarrierName", ECondition.Like, "%" + entity.CarrierName + "%");
            }
            if (!entity.LogisticsNo.IsEmpty())
            {
                OutOrder.And("LogisticsNo", ECondition.Like, "%" + entity.LogisticsNo + "%");
            }
            ProductEntity product = new ProductEntity();
            product.Include(item => new { Size=item.Size });
            detail.Left<ProductEntity>(product, new Params<string, string>() { Item1 = "ProductNum", Item2 = "SnNum" });

            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            OutOrder.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });

            AdminEntity auditeAdmin = new AdminEntity();
            auditeAdmin.Include(a => new { AuditeUserName = a.UserName });
            OutOrder.Left<AdminEntity>(auditeAdmin, new Params<string, string>() { Item1 = "AuditUser", Item2 = "UserNum" });

            int rowCount = 0;
            List<OutStoDetailEntity> listResult = this.OutStoDetail.GetList(detail, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            if (!listResult.IsNullOrEmpty())
            {
                List<LocationEntity> listLocation = new LocationProvider(this.CompanyID).GetList();
                listLocation = listLocation == null ? new List<LocationEntity>() : listLocation;
                foreach (OutStoDetailEntity item in listResult)
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
        public override string EditOrder(OutStorageEntity entity)
        {
            entity.Include(a => new { a.OutType, a.ProductType, a.StorageNum, a.CusSnNum, a.CusNum, a.CusName, a.Contact, a.Phone, a.Address, a.ContractOrder ,a.SendDate,a.Remark});
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID)
                ;
            int line = this.OutStorage.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 查询单据详细信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public override OutStoDetailEntity GetDetail(string SnNum)
        {
            OutStoDetailEntity entity = new OutStoDetailEntity();
            entity.IncludeAll();
            entity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;

            ProductEntity product = new ProductEntity();
            product.Include(item => new { Size = item.Size });
            entity.Left<ProductEntity>(product, new Params<string, string>() { Item1 = "ProductNum", Item2 = "SnNum" });

            entity = this.OutStoDetail.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 编辑单据详细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditDetail(OutStoDetailEntity entity)
        {
            entity.Include(a => new 
            {
                a.ProductName,
                a.BarCode,
                a.ProductNum,
                a.BatchNum,
                a.StorageNum,
                a.Num,
                a.LocalNum,
                a.IsPick,
                a.OutPrice,
                a.Amount,
                a.ContractOrder,
                a.ContractSn
            });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==this.CompanyID);
            int line = this.OutStoDetail.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 获得订单数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int GetCount(OutStorageEntity entity)
        {
            if (!entity.OrderNum.IsEmpty())
            {
                entity.Where("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(a => a.StorageNum == entity.StorageNum);
            }
            if (entity.OutType > 0)
            {
                entity.And(a => a.OutType == entity.OutType);
            }
            if (!entity.CusNum.IsEmpty())
            {
                entity.And("CusNum", ECondition.Like, "%" + entity.CusNum + "%");
            }
            if (!entity.CusName.IsEmpty())
            {
                entity.And("CusName", ECondition.Like, "%" + entity.CusName + "%");
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
            return this.OutStorage.GetCount(entity);
        }

        /// <summary>
        /// 编辑出库单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string EditOrder(OutStorageEntity entity, List<OutStoDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.Include(a => new { a.OutType, a.ProductType, a.StorageNum, a.CusSnNum, a.CusNum, a.CusName, a.Contact, a.Phone, a.Address, a.ContractOrder, a.Num, a.Amount, a.Weight, a.SendDate, a.Remark});
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a=>a.CompanyID==this.CompanyID);

                OutStoDetailEntity detail = new OutStoDetailEntity();
                detail.Where(a => a.OrderSnNum == entity.SnNum)
                    .And(a=>a.CompanyID==this.CompanyID);
                this.OutStoDetail.Delete(detail);
                foreach (OutStoDetailEntity item in list)
                {
                    item.SnNum = item.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : item.SnNum;
                    item.OrderNum = entity.OrderNum;
                    item.OrderSnNum = entity.SnNum;
                    item.CreateTime = DateTime.Now;
                    item.CompanyID = this.CompanyID;
                    item.Amount = item.OutPrice * item.Num;

                    item.IncludeAll();
                }
                entity.Num = list.Sum(a => a.Num);
                entity.Amount = list.Sum(a => a.Amount);

                line = this.OutStorage.Update(entity);
                this.OutStoDetail.Add(list);
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
            OutStorageEntity entity = new OutStorageEntity();
            entity.SnNum = argOrderNum;
            entity.CompanyID = this.CompanyID;
            entity = GetOrder(entity);
            if (entity != null)
            {
                List<OutStorageEntity> list = new List<OutStorageEntity>();
                list.Add(entity);
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                OutStoDetailEntity detail = new OutStoDetailEntity();
                detail.OrderSnNum = argOrderNum;
                detail.CompanyID = this.CompanyID;
                List<OutStoDetailEntity> listDetail = GetOrderDetail(detail);
                if (!listDetail.IsNullOrEmpty())
                {
                    DataTable tableDetail = listDetail.ToDataTable();
                    ds.Tables.Add(tableDetail);
                }
            }
            else
            {
                List<OutStorageEntity> list = new List<OutStorageEntity>();
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);
                List<OutStoDetailEntity> listDetail = new List<OutStoDetailEntity>();
                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
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
            OutStorageEntity entity = new OutStorageEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where<OutStorageEntity>("SnNum", ECondition.In, list.ToArray())
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.OutStorage.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }
    }
}
