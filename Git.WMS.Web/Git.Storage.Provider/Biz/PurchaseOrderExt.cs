using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Biz;
using Git.Storage.Entity.Finance;
using Git.Storage.Entity.InStorage;
using Git.Storage.Provider.Finance;
using Git.Storage.Provider.InStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Git.Storage.Provider.Biz
{
    public partial class PurchaseOrderExt : PurchaseOrder
    {
        public PurchaseOrderExt(string _CompanyID)
            : base(_CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 根据采购订单转化财务账目记录
        /// 1000: 账务记录生成成功
        /// 1001：采购订单不存在
        /// 1002：已经生成账务记录,不要重复操作
        /// 1003：账务记录生成异常
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public string ToFiance(string SnNum)
        {
            PurchaseEntity entity = new PurchaseEntity();
            entity.SnNum = SnNum;
            entity = this.GetOrder(entity);
            if (entity == null)
            {
                return "1001";
            }
            if (entity.AuditeStatus == (int)EBool.Yes)
            {
                return "1002";
            }
            FinanceBillEntity bill = new FinanceBillEntity();
            bill.SnNum = ConvertHelper.NewGuid();
            bill.CateNum = "";
            bill.CateName = "";
            bill.BillType = (int)EFinanceType.Payable;
            bill.FromNum = "";
            bill.FromName = "公司";
            bill.ToName = entity.SupName;
            bill.ToNum = entity.SupSnNum;
            bill.Amount = entity.Amount;
            bill.Title = string.Format("[采购][{0}]{1}", entity.CreateTime.ToString("yyyy-MM-dd"), entity.SupName);
            bill.ContractSn = entity.SnNum;
            bill.ContractNum = entity.OrderNum;
            bill.Status = (int)EAudite.Wait;
            bill.IsDelete = (int)EIsDelete.NotDelete;
            bill.CreateTime = DateTime.Now;
            bill.CreateUser = entity.CreateUser;
            bill.Remark = string.Empty;
            bill.CompanyID = this.CompanyID;

            FinanceBillProvider provider = new FinanceBillProvider(this.CompanyID);
            int line = provider.Add(bill);
            if (line > 0)
            {
                entity.AuditeStatus = (int)EBool.Yes;
                entity.IncludeAuditeStatus(true);
                entity.Where(a => a.SnNum == entity.SnNum);
                line += this.Purchase.Update(entity);
            }
            return line > 0 ? "1000" : "1003";
        }


        /// <summary>
        /// 根据采购订单转化为入库单
        /// 1: 采购订单生成入库单成功
        /// 1001：采购订单不存在
        /// 1002：采购订单已经全部入库,请勿重新入库
        /// 1003: 订单已经被取消,不能入库
        /// 1004: 采购订单中不存在入库物品
        /// 1005: 存在不在采购单内的货品
        /// </summary>
        /// <param name="SnNum">采购订单唯一编号</param>
        /// <param name="list">要入库的内容</param>
        /// <param name="StorageNum">仓库编号</param>
        /// <param name="CreateUser">操作人编号</param>
        /// <returns></returns>
        public DataResult ToInStorage(string SnNum, List<PurchaseDetailEntity> list, string StorageNum, string CreateUser)
        {
            PurchaseEntity entity = new PurchaseEntity();
            entity.SnNum = SnNum;
            entity = this.GetOrder(entity);
            DataResult result = new DataResult();
            if (entity == null)
            {
                result.Code = 1001;
                result.Message = "采购订单不存在";
                return result;
            }

            if (entity.Status < (int)EPurchaseStatus.InTheStock)
            {
                result.Code = 1001;
                result.Message = "采购订单审核流程不正确,不能生成入库单";
                return result;
            }

            if (entity.Status == (int)EPurchaseStatus.AllIn)
            {
                result.Code = 1002;
                result.Message = "采购订单已经全部入库,请勿重新入库";
                return result;
            }

            if (entity.Status == (int)EPurchaseStatus.OrderCancel)
            {
                result.Code = 1003;
                result.Message = "订单已经被取消,不能入库";
                return result;
            }

            PurchaseDetailEntity detailEntity = new PurchaseDetailEntity();
            detailEntity.OrderSnNum = SnNum;
            List<PurchaseDetailEntity> listSource = this.GetOrderDetail(detailEntity);
            if (listSource.IsNullOrEmpty())
            {
                result.Code = 1004;
                result.Message = "采购订单中不存在入库物品";
                return result;
            }


            //检查入库产品是否在采购单内
            List<PurchaseDetailEntity> listExists = new List<PurchaseDetailEntity>();
            foreach (PurchaseDetailEntity item in list)
            {
                if (!listSource.Exists(a => a.SnNum == item.SnNum))
                {
                    //不存在添加到集合
                    listExists.Add(item);
                }
            }
            if (!listExists.IsNullOrEmpty())
            {
                string content = listExists.Select(a => string.Format("[{0}]{1}", a.BarCode, a.ProductName)).ToArray().ToString();
                result.Code = 1005;
                result.Message = content + " 不在采购订单[" + entity.OrderNum + "]中,请重新确认再入库";
                return result;
            }

            InStorageEntity InEntity = new InStorageEntity();
            InEntity.SnNum = ConvertHelper.NewGuid();
            InEntity.InType = (int)EInType.Purchase;
            InEntity.ProductType = (int)EProductType.Goods;
            InEntity.StorageNum = StorageNum;
            InEntity.SupNum = entity.SupNum;
            InEntity.SupSnNum = entity.SupSnNum;
            InEntity.SupName = entity.SupName;
            InEntity.ContactName = entity.Contact;
            InEntity.Phone = entity.Phone;
            InEntity.Address = entity.Address;
            InEntity.ContractOrder = entity.SnNum;
            //0 无关联 1 采购 2 出库 3退货
            InEntity.ContractType = 1;
            InEntity.Status = (int)EAudite.Wait;
            InEntity.IsDelete = (int)EIsDelete.NotDelete;
            InEntity.CreateTime = DateTime.Now;
            InEntity.OrderTime = DateTime.Now;
            InEntity.CreateUser = CreateUser;
            InEntity.CompanyID = this.CompanyID;

            List<InStorDetailEntity> listDetail = new List<InStorDetailEntity>();
            foreach (PurchaseDetailEntity item in list)
            {
                InStorDetailEntity InDetail = new InStorDetailEntity();
                InDetail.SnNum = ConvertHelper.NewGuid();
                InDetail.OrderSnNum = InEntity.SnNum;
                InDetail.ProductNum = item.ProductNum;
                InDetail.ProductName = item.ProductName;
                InDetail.BarCode = item.BarCode;
                InDetail.BatchNum = item.BatchNum;
                InDetail.Num = item.Qty;
                InDetail.InPrice = item.Price;
                InDetail.Amount = item.Qty * item.Price;
                InDetail.ContractOrder = item.SnNum;
                InDetail.CreateTime = DateTime.Now;
                InDetail.LocalNum = item.LocalNum;
                InDetail.StorageNum = StorageNum;
                InDetail.CreateUser = CreateUser;
                InDetail.CompanyID = this.CompanyID;

                listDetail.Add(InDetail);
            }
            string returnValue = string.Empty;
            using (TransactionScope ts = new TransactionScope())
            {
                Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(this.CompanyID);
                returnValue = bill.Create(InEntity, listDetail);

                if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
                {

                    foreach (PurchaseDetailEntity item in list)
                    {
                        if (item.RealNum + item.Qty >= item.Num)
                        {
                            item.Status = (int)EPurchaseStatus.AllIn;
                            item.RealNum += item.Qty;
                        }
                        else if ((item.RealNum + item.Qty) < item.Num && (item.RealNum + item.Qty) > 0)
                        {
                            item.Status = (int)EPurchaseStatus.PartialIn;
                            item.RealNum += item.Qty;
                        }
                        item.Include(a => new { a.RealNum, a.Status });
                        item.Where(a => a.SnNum == item.SnNum);
                        this.PurchaseDetail.Update(item);
                    }

                    //再次查询校验状态
                    detailEntity = new PurchaseDetailEntity();
                    detailEntity.OrderSnNum = SnNum;
                    listSource = this.GetOrderDetail(detailEntity);
                    if (!listSource.IsNullOrEmpty())
                    {
                        if (listSource.Count(a => a.Status == (int)EPurchaseStatus.PartialIn) > 0
                            || (listSource.Count(a => a.Status == (int)EPurchaseStatus.AllIn) < listSource.Count() && listSource.Count(a => a.Status == (int)EPurchaseStatus.AllIn) > 0)
                            )
                        {
                            entity.Status = (int)EPurchaseStatus.PartialIn;
                        }
                        else if (listSource.Count(a => a.Status == (int)EPurchaseStatus.AllIn) == listSource.Count())
                        {
                            entity.Status = (int)EPurchaseStatus.AllIn;
                        }
                        entity.IncludeStatus(true);
                        entity.Where(a => a.SnNum == entity.SnNum);
                        this.Purchase.Update(entity);
                    }
                    result.Code = (int)EResponseCode.Success;
                    result.Message = "入库单创建成功";
                }
                else
                {
                    result.Code = (int)EResponseCode.Exception;
                    result.Message = "入库单创建失败";
                }

                ts.Complete();
            }

            return result;
        }



        /// <summary>
        /// 采购退货单
        /// </summary>
        /// <param name="SnNum"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataResult ToReturn(string SnNum, List<PurchaseDetailEntity> list)
        {
            PurchaseEntity entity = new PurchaseEntity();
            entity.SnNum = SnNum;
            entity = this.GetOrder(entity);
            DataResult result = new DataResult();
            if (entity == null)
            {
                result.Code = 1001;
                result.Message = "采购订单不存在";
                return result;
            }
            if (entity.Status == (int)EPurchaseStatus.AllIn || entity.Status == (int)EPurchaseStatus.PartialIn)
            {
                result.Code = 1002;
                result.Message = "该采购单已经入库,不能退货操作";
                return result;
            }
            PurchaseDetailEntity detailEntity = new PurchaseDetailEntity();
            detailEntity.OrderSnNum = SnNum;
            List<PurchaseDetailEntity> listSource = this.GetOrderDetail(detailEntity);
            if (listSource.IsNullOrEmpty())
            {
                result.Code = 1003;
                result.Message = "采购订单中不存在出库货品";
                return result;
            }
            using (TransactionScope ts = new TransactionScope())
            {
                foreach (PurchaseDetailEntity item in list)
                {
                    if (item.Qty > 0)
                    {
                        PurchaseReturnDetailEntity returnDetail = new PurchaseReturnDetailEntity();
                        returnDetail.IncludeAll();
                        returnDetail
                            .Where(a => a.CompanyID == this.CompanyID)
                            .And(a => a.PurchaseDetailSn == item.SnNum)
                            ;
                        PurchaseReturnEntity returnItem = new PurchaseReturnEntity();
                        returnItem.And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                            ;
                        returnDetail.Left<PurchaseReturnEntity>(returnItem, new Params<string, string>() { Item1 = "OrderSnNum", Item2 = "SnNum" });

                        List<PurchaseReturnDetailEntity> listDetail = this.PurchaseReturnDetail.GetList(returnDetail);

                        if (listDetail != null && listDetail.Where(a => a.PurchaseDetailSn == item.SnNum).Sum(a => a.ReturnNum) >= item.Num)
                        {
                            result.Code = 1006;
                            result.Message = string.Format("货品[{0}]已经全部退货,请勿重复申请退货", item.ProductName);
                            return result;
                        }
                    }
                }

                string returnValue = string.Empty;

                Bill<PurchaseReturnEntity, PurchaseReturnDetailEntity> bill = new PurchaseReturnOrder(this.CompanyID);
                PurchaseReturnEntity PurchaseReturnEntity = new PurchaseReturnEntity();
                PurchaseReturnEntity.SupSnNum = entity.SupSnNum;
                PurchaseReturnEntity.SupNum = entity.SupNum;
                PurchaseReturnEntity.SupName = entity.SupName;
                PurchaseReturnEntity.Contact = entity.Contact;
                PurchaseReturnEntity.Phone = entity.Phone;
                PurchaseReturnEntity.PurchaseSnNum = entity.SnNum;
                PurchaseReturnEntity.PurchaseOrderNum = entity.OrderNum;
                PurchaseReturnEntity.CompanyID = this.CompanyID;

                List<PurchaseReturnDetailEntity> listReturnDetail = new List<PurchaseReturnDetailEntity>();
                foreach (PurchaseDetailEntity item in list)
                {
                    if (item.Qty > 0)
                    {
                        PurchaseReturnDetailEntity DetailEntity = new PurchaseReturnDetailEntity();
                        DetailEntity.SnNum = ConvertHelper.NewGuid();
                        DetailEntity.ProductName = item.ProductName;
                        DetailEntity.BarCode = item.BarCode;
                        DetailEntity.ProductNum = item.ProductNum;
                        DetailEntity.Num = item.Num;
                        DetailEntity.ReturnNum = item.Qty;
                        DetailEntity.UnitNum = item.UnitNum;
                        DetailEntity.Price = item.Price;
                        DetailEntity.Amount = item.Price * item.Qty;
                        DetailEntity.ReturnTime = DateTime.Now;
                        DetailEntity.PurchaseDetailSn = item.SnNum;
                        DetailEntity.CompanyID = this.CompanyID;
                        DetailEntity.CreateTime = DateTime.Now;

                        listReturnDetail.Add(DetailEntity);
                    }
                }

                returnValue = bill.Create(PurchaseReturnEntity, listReturnDetail);

                if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
                {
                    Task.Factory.StartNew(() => 
                    {
                        foreach (PurchaseDetailEntity DetailItem in listSource)
                        {
                            PurchaseReturnDetailEntity returnDetail = new PurchaseReturnDetailEntity();
                            returnDetail.IncludeAll();
                            returnDetail
                                .Where(a => a.CompanyID == this.CompanyID)
                                .And(a => a.PurchaseDetailSn == DetailItem.SnNum)
                                ;
                            PurchaseReturnEntity returnItem = new PurchaseReturnEntity();
                            returnItem.And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                                ;
                            returnDetail.Left<PurchaseReturnEntity>(returnItem, new Params<string, string>() { Item1 = "OrderSnNum", Item2 = "SnNum" });

                            List<PurchaseReturnDetailEntity> listDetail = this.PurchaseReturnDetail.GetList(returnDetail);
                            if (!listDetail.IsNullOrEmpty())
                            {
                                double ReturnNum = listDetail.Sum(a => a.ReturnNum);
                                DetailItem.IncludeReturnNum(true);
                                DetailItem.ReturnNum = ReturnNum;
                                DetailItem.Where(a => a.SnNum == DetailItem.SnNum).And(a => a.CompanyID == this.CompanyID);
                                this.PurchaseDetail.Update(DetailItem);
                            }
                        }
                        PurchaseEntity PurchaseItem = new PurchaseEntity();
                        PurchaseItem.HasReturn = (int)EBool.Yes;
                        PurchaseItem.IncludeHasReturn(true);
                        PurchaseItem.Where(a => a.SnNum == SnNum).And(a => a.CompanyID == this.CompanyID);
                        this.Purchase.Update(PurchaseItem);
                    });
                    result.Code = (int)EResponseCode.Success;
                    result.Message = "采购退货单创建成功";
                }
                else
                {
                    result.Code = (int)EResponseCode.Exception;
                    result.Message = "采购退货单创建失败";
                }
                ts.Complete();
            }
            return result;
        }
    }
}
