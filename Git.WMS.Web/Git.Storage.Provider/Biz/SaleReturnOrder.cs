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
    public partial class SaleReturnOrder : Bill<SaleReturnEntity, SaleReturnDetailEntity>
    {
        public SaleReturnOrder(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 创建销售退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Create(SaleReturnEntity entity, List<SaleReturnDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.OrderNum = entity.OrderNum.IsEmpty() ? new TNumProvider(this.CompanyID).GetSwiftNum(typeof(SaleReturnEntity), 5) : entity.OrderNum;
                entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.SnNum;
                entity.Status = (int)ESaleReturnStatus.CreateOrder;
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
                        a.Status = (int)ESaleReturnStatus.CreateOrder;
                        a.ReturnTime = entity.ReturnTime;
                        a.IncludeAll();
                    });
                    entity.Num = list.Sum(q => q.Num);
                    entity.Amount = list.Sum(a => a.Amount);

                    line = this.SaleReturn.Add(entity);
                    line += this.SaleReturnDetail.Add(list);
                }
                ts.Complete();
                return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
            }
        }

        /// <summary>
        /// 取消销售退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Cancel(SaleReturnEntity entity)
        {
            entity.Status = (int)ESaleReturnStatus.OrderCancel;
            entity.IncludeStatus(true);
            entity.Where(item => item.SnNum == entity.SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;
            int line = this.SaleReturn.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 删除销售退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Delete(SaleReturnEntity entity)
        {
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.SaleReturn.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 批量删除销售退货单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string Delete(IEnumerable<string> list)
        {
            SaleReturnEntity entity = new SaleReturnEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.IncludeIsDelete(true);
            entity.Where<SaleReturnEntity>("SnNum", ECondition.In, list.ToArray())
                .And(a => a.CompanyID == this.CompanyID);
            int line = this.SaleReturn.Update(entity);
            return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
        }

        /// <summary>
        /// 审核销售退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Audite(SaleReturnEntity entity)
        {
            entity.IncludeStatus(true)
                    .IncludeReason(true)
                    .Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
            int line = this.SaleReturn.Update(entity);
            return line > 0 ? "1000" : string.Empty;
        }

        /// <summary>
        /// 打印销售退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string Print(SaleReturnEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询销售退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override SaleReturnEntity GetOrder(SaleReturnEntity entity)
        {
            entity.IncludeAll();
            AdminEntity admin = new AdminEntity();
            admin.Include(a => new { CreateUserName = a.UserName });
            entity.Left<AdminEntity>(admin, new Params<string, string>() { Item1 = "CreateUser", Item2 = "UserNum" });
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID)
                .And(a => a.SnNum == entity.SnNum);

            entity = this.SaleReturn.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 获得销售退货单详细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override List<SaleReturnDetailEntity> GetOrderDetail(SaleReturnDetailEntity entity)
        {
            SaleReturnDetailEntity detail = new SaleReturnDetailEntity();
            detail.IncludeAll();
            detail.Where(a => a.OrderSnNum == entity.OrderSnNum)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            List<SaleReturnDetailEntity> list = this.SaleReturnDetail.GetList(detail);

            if (!list.IsNullOrEmpty())
            {
                List<ProductEntity> listProducts = new ProductProvider(this.CompanyID).GetList();
                listProducts = listProducts.IsNull() ? new List<ProductEntity>() : listProducts;

                foreach (SaleReturnDetailEntity item in list)
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
        /// 查询销售退货单分页列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<SaleReturnEntity> GetList(SaleReturnEntity entity, ref Framework.DataTypes.PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And(a => a.CompanyID == this.CompanyID);
            entity.OrderBy(a => a.ID, EOrderBy.DESC);

            if (entity.OrderNum.IsNotEmpty())
            {
                entity.And("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
            }
            if (entity.CusNum.IsNotEmpty())
            {
                entity.And("CusNum", ECondition.Like, "%" + entity.CusNum + "%");
            }
            if (entity.CusName.IsNotEmpty())
            {
                entity.And("CusName", ECondition.Like, "%" + entity.CusName + "%");
            }
            if (entity.Contact.IsNotEmpty())
            {
                entity.And("Contact", ECondition.Like, "%" + entity.Contact + "%");
            }
            if (entity.Phone.IsNotEmpty())
            {
                entity.And("Phone", ECondition.Like, "%" + entity.Phone + "%");
            }
            if (entity.SaleSnNum.IsNotEmpty())
            {
                entity.And(item=>item.SaleSnNum==entity.SaleSnNum);
            }
            if (entity.SaleOrderNum.IsNotEmpty())
            {
                entity.And("SaleOrderNum", ECondition.Like, "%" + entity.SaleOrderNum + "%");
            }
            if (entity.Status > 0)
            {
                entity.And(item=>item.Status==entity.Status);
            }
            if (entity.BeginTime.IsNotEmpty())
            {
                DateTime begin = ConvertHelper.ToType<DateTime>(entity.BeginTime,DateTime.Now.AddDays(-30)).Date;
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
            List<SaleReturnEntity> listResult = this.SaleReturn.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }

        /// <summary>
        /// 销售退货单明细分页列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override List<SaleReturnDetailEntity> GetDetailList(SaleReturnDetailEntity entity, ref Framework.DataTypes.PageInfo pageInfo)
        {
            SaleReturnDetailEntity detail = new SaleReturnDetailEntity();
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

            SaleReturnEntity returnOrder = new SaleReturnEntity();
            detail.Left<SaleReturnEntity>(returnOrder, new Params<string, string>() { Item1 = "OrderSnNum", Item2 = "SnNum" });
            returnOrder.Include(a => new { CusNum = a.CusNum, CusName = a.CusName, Phone = a.Phone, Contact = a.Contact, SaleSnNum = a.SaleSnNum, SaleOrderNum=a.SaleOrderNum,Status=a.Status });
            returnOrder.And(a => a.IsDelete == (int)EIsDelete.NotDelete);

            if (!entity.CusNum.IsEmpty())
            {
                returnOrder.AndBegin<SaleReturnEntity>()
                    .And<SaleReturnEntity>("CusNum", ECondition.Like, "%" + entity.CusNum + "%")
                    .Or<SaleReturnEntity>("CusName", ECondition.Like, "%" + entity.CusNum + "%")
                    .End<SaleReturnEntity>()
                    ;
            }
            if (!entity.CusName.IsEmpty())
            {
                returnOrder.AndBegin<SaleReturnEntity>()
                    .And<SaleReturnEntity>("CusNum", ECondition.Like, "%" + entity.CusName + "%")
                    .Or<SaleReturnEntity>("CusName", ECondition.Like, "%" + entity.CusName + "%")
                    .End<SaleReturnEntity>()
                    ;
            }
            if (!entity.Phone.IsEmpty())
            {
                returnOrder.And("Phone", ECondition.Like, "%" + entity.Phone + "%");
            }
            if (!entity.SaleSnNum.IsEmpty())
            {
                returnOrder.And(item=>item.SaleSnNum==entity.SaleSnNum);
            }
            if (!entity.SaleOrderNum.IsEmpty())
            {
                returnOrder.And("SaleOrderNum", ECondition.Like, "%" + entity.SaleOrderNum + "%");
            }
            if (!entity.OrderNum.IsEmpty())
            {
                returnOrder.And("OrderNum", ECondition.Like, "%" + entity.OrderNum + "%");
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
            List<SaleReturnDetailEntity> listResult = this.SaleReturnDetail.GetList(detail, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            if (!listResult.IsNullOrEmpty())
            {
                ProductProvider productProvider = new ProductProvider(this.CompanyID);
                foreach (SaleReturnDetailEntity item in listResult)
                {
                    ProductEntity product = productProvider.GetProduct(item.ProductNum);
                    item.UnitName = product != null ? product.UnitName : string.Empty;
                    item.Size = product != null ? product.Size : string.Empty;
                }
            }
            return listResult;
        }

        /// <summary>
        /// 编辑销售退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditOrder(SaleReturnEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 编辑销售退货单详细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string EditDetail(SaleReturnDetailEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询单据详细信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public override SaleReturnDetailEntity GetDetail(string SnNum)
        {
            SaleReturnDetailEntity entity = new SaleReturnDetailEntity();
            entity.IncludeAll();
            entity.Where(item => item.SnNum == SnNum)
                .And(item => item.CompanyID == this.CompanyID)
                ;

            entity = this.SaleReturnDetail.GetSingle(entity);
            return entity;
        }

        /// <summary>
        /// 编辑销售退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public override string EditOrder(SaleReturnEntity entity, List<SaleReturnDetailEntity> list)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                int line = 0;
                entity.Include(a => new { a.CusSnNum, a.CusNum, a.CusName, a.Contact, a.Phone, a.Num, a.Amount, a.Weight, a.ReturnTime, a.Remark });

                if (!list.IsNullOrEmpty())
                {
                    entity.Num = list.Sum(q => q.Num);
                    entity.Amount = list.Sum(a => a.Amount);
                }
                entity.Where(a => a.SnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID);
                line += this.SaleReturn.Update(entity);

                SaleReturnDetailEntity detail = new SaleReturnDetailEntity();
                detail.IncludeAll();
                detail.Where(a => a.OrderSnNum == entity.SnNum)
                    .And(a => a.CompanyID == this.CompanyID)
                    ;
                List<SaleReturnDetailEntity> listSource = this.SaleReturnDetail.GetList(detail);
                listSource = listSource.IsNull() ? new List<SaleReturnDetailEntity>() : listSource;

                //如果在原有的数据中存在修改，如果不存在新增
                list = list.IsNull() ? new List<SaleReturnDetailEntity>() : list;
                foreach (SaleReturnDetailEntity item in list)
                {
                    if (listSource.Exists(a => a.SnNum == item.SnNum))
                    {
                        item.Include(a => new { a.BarCode, a.ProductName, a.ProductNum, a.Num, a.ReturnNum, a.UnitNum, a.Price, a.Amount, a.Remark });
                        item.Where(a => a.SnNum == item.SnNum);
                        line += this.SaleReturnDetail.Update(item);
                    }
                    else
                    {
                        item.IncludeAll();
                        item.OrderSnNum = entity.SnNum;
                        item.OrderNum = entity.OrderNum;
                        item.CreateTime = DateTime.Now;
                        line += this.SaleReturnDetail.Add(item);
                    }
                }

                //如果数据库中的不存在了则删除
                foreach (SaleReturnDetailEntity item in listSource)
                {
                    if (!list.Exists(a => a.SnNum == item.SnNum))
                    {
                        item.Where(a => a.SnNum == item.SnNum)
                            .And(a => a.CompanyID == this.CompanyID)
                            ;
                        line += this.SaleReturnDetail.Delete(item);
                    }
                }
                ts.Complete();
                return line > 0 ? EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) : string.Empty;
            }
        }

        /// <summary>
        /// 查询销售退货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int GetCount(SaleReturnEntity entity)
        {
            entity.Where(a => a.CompanyID == this.CompanyID)
                .And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                ;
            return this.SaleReturn.GetCount(entity);
        }

        /// <summary>
        /// 查询销售退货单打印数据
        /// </summary>
        /// <param name="argOrderNum"></param>
        /// <returns></returns>
        public override System.Data.DataSet GetPrint(string argOrderNum)
        {
            DataSet ds = new DataSet();
            SaleReturnEntity entity = new SaleReturnEntity();
            entity.SnNum = argOrderNum;
            entity = GetOrder(entity);
            if (entity != null)
            {
                List<SaleReturnEntity> list = new List<SaleReturnEntity>();
                list.Add(entity);
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                SaleReturnDetailEntity detail = new SaleReturnDetailEntity();
                detail.OrderSnNum = argOrderNum;
                List<SaleReturnDetailEntity> listDetail = GetOrderDetail(detail);
                listDetail = listDetail.IsNull() ? new List<SaleReturnDetailEntity>() : listDetail;
                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            else
            {
                List<SaleReturnEntity> list = new List<SaleReturnEntity>();
                List<SaleReturnDetailEntity> listDetail = new List<SaleReturnDetailEntity>();
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);

                DataTable tableDetail = listDetail.ToDataTable();
                ds.Tables.Add(tableDetail);
            }
            return ds;
        }
    }
}
