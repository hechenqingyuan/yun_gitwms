/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-20 13:10:28
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-20 13:10:28       情缘
*********************************************************************************/

using Git.Storage.Entity.Check;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Storage.Entity.Storage;
using Git.Storage.Common.Enum;
using Git.Storage.Provider.Sys;
using Git.Storage.Entity.Sys;

namespace Git.Storage.Provider.Check
{
    public partial class CheckOrderExt:CheckOrder
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckOrderExt(string CompanyID)
            : base(CompanyID)
        {
            this.CompanyID = CompanyID;
        }

        /// <summary>
        /// 查询盘点差异单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<InventoryDifEntity> GetList(InventoryDifEntity entity)
        {
            entity.IncludeAll();
            entity.Where(item => item.CompanyID == entity.CompanyID);
            if (entity.OrderSnNum.IsNotEmpty())
            {
                entity.And(item => item.OrderSnNum == entity.OrderSnNum);
            }
            if (entity.OrderNum.IsNotEmpty())
            {
                entity.And("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.LocalNum.IsNotEmpty())
            {
                entity.And(item => item.LocalNum == entity.LocalNum);
            }
            if (entity.LocalName.IsNotEmpty())
            {
                entity.And("LocalName", ECondition.Like, "%" + entity.LocalName + "%");
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(item => item.StorageNum == entity.StorageNum);
            }
            if (entity.ProductNum.IsNotEmpty())
            {
                entity.And(item => item.ProductNum == entity.ProductNum);
            }
            if (entity.BarCode.IsNotEmpty())
            {
                entity.And("BarCode", ECondition.Like, "%" + entity.BarCode + "%");
            }
            if (entity.ProductName.IsNotEmpty())
            {
                entity.And("ProductName", ECondition.Like, "%" + entity.ProductName + "%");
            }
            if (entity.BatchNum.IsNotEmpty())
            {
                entity.And("BatchNum", ECondition.Like, "%" + entity.BatchNum + "%");
            }
            ProductEntity product = new ProductEntity();
            product.Include(a => new { Size = a.Size, CateName = a.CateName, UnitName=a.UnitName });
            entity.Left<ProductEntity>(product, new Params<string, string>() { Item1 = "ProductNum", Item2 = "SnNum" });

            StorageEntity Storage = new StorageEntity();
            Storage.Include(a => new { StorageName = a.StorageName });
            entity.Left<StorageEntity>(Storage, new Params<string, string>() { Item1 = "StorageNum", Item2 = "SnNum" });

            List<InventoryDifEntity> listResult = this.InventoryDif.GetList(entity);
            return listResult;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<InventoryDifEntity> GetList(InventoryDifEntity entity,ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.ASC);

            entity.Where(item => item.CompanyID == entity.CompanyID);
            if (entity.OrderSnNum.IsNotEmpty())
            {
                entity.And(item => item.OrderSnNum == entity.OrderSnNum);
            }
            if (entity.OrderNum.IsNotEmpty())
            {
                entity.And("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.LocalNum.IsNotEmpty())
            {
                entity.And(item => item.LocalNum == entity.LocalNum);
            }
            if (entity.LocalName.IsNotEmpty())
            {
                entity.And("LocalName", ECondition.Like, "%" + entity.LocalName + "%");
            }
            if (entity.StorageNum.IsNotEmpty())
            {
                entity.And(item => item.StorageNum == entity.StorageNum);
            }
            if (entity.ProductNum.IsNotEmpty())
            {
                entity.And(item => item.ProductNum == entity.ProductNum);
            }
            if (entity.BarCode.IsNotEmpty())
            {
                entity.And("BarCode", ECondition.Like, "%" + entity.BarCode + "%");
            }
            if (entity.ProductName.IsNotEmpty())
            {
                entity.And("ProductName", ECondition.Like, "%" + entity.ProductName + "%");
            }
            if (entity.BatchNum.IsNotEmpty())
            {
                entity.And("BatchNum", ECondition.Like, "%" + entity.BatchNum + "%");
            }

            ProductEntity product = new ProductEntity();
            product.Include(a => new { Size = a.Size, CateName = a.CateName, UnitName = a.UnitName });
            entity.Left<ProductEntity>(product, new Params<string, string>() { Item1 = "ProductNum", Item2 = "SnNum" });

            StorageEntity Storage = new StorageEntity();
            Storage.Include(a => new { StorageName = a.StorageName });
            entity.Left<StorageEntity>(Storage, new Params<string, string>() { Item1 = "StorageNum", Item2 = "SnNum" });

            int rowCount = 0;
            List<InventoryDifEntity> listResult = this.InventoryDif.GetList(entity,pageInfo.PageSize,pageInfo.PageIndex,out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 保存盘点差异单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveDif(InventoryDifEntity entity)
        {
            entity.DifQty = entity.FirstQty - entity.LocalQty;
            LocationProvider provider = new LocationProvider(this.CompanyID);
            LocationEntity Location = provider.GetSingleByNum(entity.LocalNum);
            if (Location != null)
            {
                entity.LocalName = Location.LocalName;
            }
            entity.Include(a => new { a.BatchNum,a.LocalNum,a.LocalName,a.FirstQty,a.DifQty });
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.InventoryDif.Update(entity);
            return line;
        }

        /// <summary>
        /// 新增盘差数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddDif(InventoryDifEntity entity)
        {
            int line=0;

            //检查是否库位存在该产品
            InventoryDifEntity check = new InventoryDifEntity();
            check.Where(a => a.ProductNum == entity.ProductNum)
                .And(a => a.LocalNum == entity.LocalNum)
                .And(a=>a.BatchNum==entity.BatchNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            check.IncludeAll();
            check = this.InventoryDif.GetSingle(check);
            if (check.IsNotNull())
            {
                check.FirstQty = entity.FirstQty;
                check.IncludeFirstQty(true);
                check.Where(a => a.SnNum == check.SnNum);
                line = this.InventoryDif.Update(check);
            }
            else
            {
                LocationProvider provider = new LocationProvider(this.CompanyID);
                LocationEntity Location = provider.GetSingleByNum(entity.LocalNum);
                if (Location != null)
                {
                    entity.LocalName = Location.LocalName;
                }
                entity.DifQty = entity.FirstQty - entity.LocalQty;
                entity.SnNum = ConvertHelper.NewGuid();
                entity.CreateTime = DateTime.Now;
                entity.CompanyID = this.CompanyID;
                entity.IncludeAll();
                line = this.InventoryDif.Add(entity);
            }
            return line;
        }

        /// <summary>
        /// 根据唯一编号删除盘点差异单
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public int DeleteDif(string SnNum)
        {
            InventoryDifEntity entity = new InventoryDifEntity();
            entity.Where(item => item.SnNum == SnNum).And(item => item.CompanyID == this.CompanyID);
            int line = this.InventoryDif.Delete(entity);

            return line;
        }

        /// <summary>
        /// 完成盘点作业
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Complete(InventoryOrderEntity entity)
        {
            entity.IsComplete = (int)EBool.Yes;
            entity.IncludeIsComplete(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.InventoryOrder.Update(entity);
            return line;
        }

        /// <summary>
        /// 检查盘点产品是否满足要求
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetCount(InventoryDetailEntity entity)
        {
            entity.Where(a => a.TargetNum == entity.TargetNum)
                .And(a => a.OrderSnNum == entity.OrderSnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;

            int count = this.InventoryDetail.GetCount(entity);
            return count;
        }

        /// <summary>
        /// 根据盘点单号查询盘点单信息
        /// </summary>
        /// <param name="OrderNum"></param>
        /// <returns></returns>
        public InventoryOrderEntity GetOrderByNum(string OrderNum)
        {
            InventoryOrderEntity entity = new InventoryOrderEntity();
            entity.IncludeAll();
            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                .And(a => a.OrderNum == OrderNum);
            entity = this.InventoryOrder.GetSingle(entity);
            return entity;
        }
    }
}
