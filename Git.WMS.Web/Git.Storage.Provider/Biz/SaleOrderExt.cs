using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Biz;
using Git.Storage.Entity.Finance;
using Git.Storage.Entity.OutStorage;
using Git.Storage.Entity.Pick;
using Git.Storage.Entity.Storage;
using Git.Storage.Provider.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Git.Storage.Provider.Biz
{
    public partial class SaleOrderExt : SaleOrder
    {
        public SaleOrderExt(string _CompanyID)
            : base(_CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 根据销售订单转换为财务账务记录
        /// 1000: 账务记录生成成功
        /// 1001：销售订单不存在
        /// 1002：已经生成账务记录,不要重复操作
        /// 1003：账务记录生成异常
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public string ToFiance(string SnNum)
        {
            SaleOrderEntity entity = new SaleOrderEntity();
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
            bill.BillType = (int)EFinanceType.BillReceivable;
            bill.FromNum = entity.CusSnNum;
            bill.FromName = entity.CusName;
            bill.ToName = "";
            bill.ToNum = "公司";
            bill.Amount = entity.Amount;
            bill.Title = string.Format("[销售][{0}]{1}", entity.CreateTime.ToString("yyyy-MM-dd"), entity.CusName);
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
                line += this.SaleOrder.Update(entity);
            }
            return line > 0 ? "1000" : "1003";
        }


        /// <summary>
        /// 根据销售订单转换为出库单
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public DataResult ToOutStorage(string SnNum, List<SaleDetailEntity> list, string StorageNum)
        {
            SaleOrderEntity entity = new SaleOrderEntity();
            entity.SnNum = SnNum;
            entity = this.GetOrder(entity);
            DataResult result = new DataResult();
            if (entity == null)
            {
                result.Code = 1001;
                result.Message = "销售订单不存在";
                return result;
            }

            if (entity.Status == (int)EOrderStatus.CreateOrder)
            {
                result.Code = 1001;
                result.Message = "销售订单审核流程不正确,不能生成出库单";
                return result;
            }

            if (entity.Status == (int)EPurchaseStatus.AllIn)
            {
                result.Code = 1002;
                result.Message = "销售订单已经全部出库,请勿重新出库";
                return result;
            }

            if (entity.Status == (int)EPurchaseStatus.OrderCancel)
            {
                result.Code = 1003;
                result.Message = "订单已经被取消,不能出库";
                return result;
            }

            SaleDetailEntity detailEntity = new SaleDetailEntity();
            detailEntity.OrderSnNum = SnNum;
            List<SaleDetailEntity> listSource = this.GetOrderDetail(detailEntity);
            if (listSource.IsNullOrEmpty())
            {
                result.Code = 1004;
                result.Message = "销售订单中不存在出库货品";
                return result;
            }


            //检查入库产品是否在采购单内
            List<SaleDetailEntity> listExists = new List<SaleDetailEntity>();
            foreach (SaleDetailEntity item in list)
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
                result.Message = content + " 不在销售订单[" + entity.OrderNum + "]中,请重新确认再出库";
                return result;
            }

            List<LocalProductEntity> listResult = new List<LocalProductEntity>();
            //拣货
            foreach (SaleDetailEntity item in list)
            {
                Proc_PickProductEntity pickEntity = new Proc_PickProductEntity();
                pickEntity.InStorageNum = StorageNum;
                pickEntity.InProductNum = item.ProductNum;
                pickEntity.InCompanyID = this.CompanyID;
                pickEntity.InNum = item.Qty;

                List<LocalProductEntity> listPickResult = this.Proc_PickProduct.ExceuteEntityList<LocalProductEntity>(pickEntity);
                if (listPickResult.IsNullOrEmpty() || listPickResult.Sum(a => a.Num) < item.Qty)
                {
                    result.Code = 1006;
                    result.Message = string.Format("货品[{0}]拣货数 {1} 不满足要求数 {2},请确保仓库有足够的货品", item.ProductName, listPickResult.Sum(a => a.Num).ToString("0.00"),item.Qty.ToString("0.00"));
                    return result;
                }

                listResult.AddRange(listPickResult);
            }

            OutStorageEntity outEntity = new OutStorageEntity();
            outEntity.SnNum = ConvertHelper.NewGuid();
            outEntity.OutType = (int)EOutType.Sell;
            outEntity.ProductType = (int)EProductType.Goods;
            outEntity.StorageNum = StorageNum;
            outEntity.CusSnNum = entity.CusSnNum;
            outEntity.CusNum = entity.CusNum;
            outEntity.CusName = entity.CusName;
            outEntity.Contact = entity.Contact;
            outEntity.Phone = entity.Phone;
            outEntity.Address = entity.Address;
            outEntity.ContractOrder = entity.SnNum;
            outEntity.SendDate = entity.SendDate;
            outEntity.Status = (int)EAudite.Wait;
            outEntity.IsDelete = (int)EIsDelete.NotDelete;
            outEntity.CreateTime = DateTime.Now;
            outEntity.CreateUser = entity.CreateUser;
            outEntity.OperateType = (int)EOpType.PC;
            outEntity.EquipmentNum = "";
            outEntity.EquipmentCode = "";
            outEntity.CompanyID = this.CompanyID;

            List<OutStoDetailEntity> listDetails = new List<OutStoDetailEntity>();

            foreach (LocalProductEntity item in listResult)
            {
                OutStoDetailEntity detail = new OutStoDetailEntity();
                detail.SnNum = ConvertHelper.NewGuid();
                detail.OrderSnNum = outEntity.SnNum;
                detail.ProductName = item.ProductName;
                detail.ProductNum = item.ProductNum;
                detail.BarCode = item.BarCode;
                detail.BatchNum = item.BatchNum;
                detail.LocalNum = item.LocalNum;
                detail.StorageNum = item.StorageNum;
                detail.Num = item.Num;
                detail.IsPick = (int)EBool.No;
                detail.RealNum = 0;

                SaleDetailEntity saleEntity = list.FirstOrDefault(a => a.ProductNum == item.ProductNum);
                if (saleEntity != null)
                {
                    detail.OutPrice = saleEntity.Price;
                    detail.Amount = detail.OutPrice * detail.Num;
                    detail.ContractOrder = entity.OrderNum;
                    detail.ContractSn = saleEntity.SnNum;
                }
                detail.LocalSn = item.Sn;
                detail.CreateTime = DateTime.Now;
                detail.CompanyID = this.CompanyID;

                listDetails.Add(detail);
            }

            string returnValue = string.Empty;
            using (TransactionScope ts = new TransactionScope())
            {
                Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorage.OutStorageOrder(this.CompanyID);
                returnValue = bill.Create(outEntity, listDetails);

                if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
                {

                    foreach (SaleDetailEntity item in list)
                    {
                        if (item.RealNum + item.Qty >= item.Num)
                        {
                            item.Status = (int)EOrderStatus.AllDelivery;
                            item.RealNum += item.Qty;
                        }
                        else if ((item.RealNum + item.Qty) < item.Num && (item.RealNum + item.Qty) > 0)
                        {
                            item.Status = (int)EOrderStatus.PartialDelivery;
                            item.RealNum += item.Qty;
                        }
                        item.Include(a => new { a.RealNum, a.Status });
                        item.Where(a => a.SnNum == item.SnNum);
                        this.SaleDetail.Update(item);
                    }

                    //再次查询校验状态
                    detailEntity = new SaleDetailEntity();
                    detailEntity.OrderSnNum = SnNum;
                    listSource = this.GetOrderDetail(detailEntity);
                    if (!listSource.IsNullOrEmpty())
                    {
                        if (listSource.Count(a => a.Status == (int)EOrderStatus.PartialDelivery) > 0
                            || (listSource.Count(a => a.Status == (int)EOrderStatus.AllDelivery) < listSource.Count() && listSource.Count(a => a.Status == (int)EOrderStatus.AllDelivery) > 0)
                            )
                        {
                            entity.Status = (int)EOrderStatus.PartialDelivery;
                        }
                        else if (listSource.Count(a => a.Status == (int)EOrderStatus.AllDelivery) == listSource.Count())
                        {
                            entity.Status = (int)EOrderStatus.AllDelivery;
                        }
                        entity.IncludeStatus(true);
                        entity.Where(a => a.SnNum == entity.SnNum);
                        this.SaleOrder.Update(entity);
                    }
                    result.Code = (int)EResponseCode.Success;
                    result.Message = "出库单创建成功";
                }
                else
                {
                    result.Code = (int)EResponseCode.Exception;
                    result.Message = "出库单创建失败";
                }

                ts.Complete();
            }
            return result;
        }

        /// <summary>
        /// 销售退货单
        /// </summary>
        /// <param name="SnNum"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataResult ToReturn(string SnNum, List<SaleDetailEntity> list)
        {
            SaleOrderEntity entity = new SaleOrderEntity();
            entity.SnNum = SnNum;
            entity = this.GetOrder(entity);
            DataResult result = new DataResult();
            if (entity == null)
            {
                result.Code = 1001;
                result.Message = "销售订单不存在";
                return result;
            }
            if (!(entity.Status == (int)EOrderStatus.PartialDelivery || entity.Status == (int)EOrderStatus.AllDelivery))
            {
                result.Code = 1002;
                result.Message = "该销售订单未发货,不能申请退货";
                return result;
            }
            SaleDetailEntity detailEntity = new SaleDetailEntity();
            detailEntity.OrderSnNum = SnNum;
            List<SaleDetailEntity> listSource = this.GetOrderDetail(detailEntity);
            if (listSource.IsNullOrEmpty())
            {
                result.Code = 1003;
                result.Message = "销售订单中不存在出库货品";
                return result;
            }

            foreach (SaleDetailEntity item in list)
            {
                if (item.Qty > 0)
                {
                    SaleReturnDetailEntity returnDetail = new SaleReturnDetailEntity();
                    returnDetail.IncludeAll();
                    returnDetail
                        .Where(a => a.CompanyID == this.CompanyID)
                        .And(a => a.SaleDetailSn == item.SnNum)
                        ;

                    List<SaleReturnDetailEntity> listDetail = this.SaleReturnDetail.GetList(returnDetail);

                    if (listDetail!=null && listDetail.Where(a => a.SaleDetailSn == item.SnNum).Sum(a => a.ReturnNum) >= item.Num)
                    {
                        result.Code = 1006;
                        result.Message = string.Format("货品[{0}]已经全部退货,请勿重复申请退货", item.ProductName);
                        return result;
                    }
                }
            }

            string returnValue = string.Empty;
            using (TransactionScope ts = new TransactionScope())
            {
                Bill<SaleReturnEntity, SaleReturnDetailEntity> bill = new SaleReturnOrder(this.CompanyID);
                SaleReturnEntity SaleReturnEntity = new SaleReturnEntity();
                SaleReturnEntity.CusSnNum = entity.CusSnNum;
                SaleReturnEntity.CusNum = entity.CusNum;
                SaleReturnEntity.CusName = entity.CusName;
                SaleReturnEntity.Contact = entity.Contact;
                SaleReturnEntity.Phone = entity.Phone;
                SaleReturnEntity.SaleSnNum = entity.SnNum;
                SaleReturnEntity.SaleOrderNum = entity.OrderNum;
                SaleReturnEntity.CompanyID = this.CompanyID;

                List<SaleReturnDetailEntity> listReturnDetail = new List<SaleReturnDetailEntity>();
                foreach (SaleDetailEntity item in list)
                {
                    if (item.Qty > 0)
                    {
                        SaleReturnDetailEntity DetailEntity = new SaleReturnDetailEntity();
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
                        DetailEntity.SaleDetailSn = item.SnNum;
                        DetailEntity.CompanyID = this.CompanyID;
                        DetailEntity.CreateTime = DateTime.Now;

                        listReturnDetail.Add(DetailEntity);
                    }
                }

                returnValue = bill.Create(SaleReturnEntity, listReturnDetail);

                if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
                {
                    Task.Factory.StartNew(() =>
                    {
                        foreach (SaleDetailEntity DetailItem in listSource)
                        {
                            SaleReturnDetailEntity returnDetail = new SaleReturnDetailEntity();
                            returnDetail.IncludeAll();
                            returnDetail
                                .Where(a => a.CompanyID == this.CompanyID)
                                .And(a => a.SaleDetailSn == DetailItem.SnNum)
                                ;
                            SaleReturnEntity returnItem = new SaleReturnEntity();
                            returnItem.And(a => a.IsDelete == (int)EIsDelete.NotDelete)
                                ;
                            returnDetail.Left<SaleReturnEntity>(returnItem, new Params<string, string>() { Item1 = "OrderSnNum", Item2 = "SnNum" });

                            List<SaleReturnDetailEntity> listDetail = this.SaleReturnDetail.GetList(returnDetail);
                            if (!listDetail.IsNullOrEmpty())
                            {
                                double ReturnNum = listDetail.Sum(a => a.ReturnNum);
                                DetailItem.IncludeReturnNum(true);
                                DetailItem.ReturnNum = ReturnNum;
                                DetailItem.Where(a => a.SnNum == DetailItem.SnNum).And(a => a.CompanyID == this.CompanyID);
                                this.SaleDetail.Update(DetailItem);
                            }
                        }
                        SaleOrderEntity SaleItem = new SaleOrderEntity();
                        SaleItem.HasReturn = (int)EBool.Yes;
                        SaleItem.IncludeHasReturn(true);
                        SaleItem.Where(a => a.SnNum == SnNum).And(a => a.CompanyID == this.CompanyID);
                        this.SaleOrder.Update(SaleItem);
                    });

                    result.Code = (int)EResponseCode.Success;
                    result.Message = "销售退货单创建成功";
                }
                else
                {
                    result.Code = (int)EResponseCode.Exception;
                    result.Message = "销售退货单创建失败";
                }
                ts.Complete();
            }
            return result;
        }
    }
}
