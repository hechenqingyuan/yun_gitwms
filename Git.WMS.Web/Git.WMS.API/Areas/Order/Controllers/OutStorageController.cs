using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.OutStorage;
using Git.Storage.Provider;
using Git.Storage.Provider.OutStorage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Order.Controllers
{
    public class OutStorageController : Controller
    {
        /// <summary>
        /// 新增出库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            OutStorageEntity entity = WebUtil.GetFormObject<OutStorageEntity>("Entity");
            List<OutStoDetailEntity> list = WebUtil.GetFormObject<List<OutStoDetailEntity>>("List");

            entity.SnNum = ConvertHelper.NewGuid();
            entity.ProductType = (int)EProductType.Goods;
            entity.Status = (int)EAudite.Wait;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = entity.CompanyID.IsEmpty() ? CompanyID : entity.CompanyID;
            entity.EquipmentNum = "";
            entity.EquipmentCode = "";

            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            string returnValue = bill.Create(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "出库单创建成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "出库单创建失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑出库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            OutStorageEntity entity = WebUtil.GetFormObject<OutStorageEntity>("Entity");
            List<OutStoDetailEntity> list = WebUtil.GetFormObject<List<OutStoDetailEntity>>("List");

            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            string returnValue = bill.EditOrder(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "出库单编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "出库单编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑出库单主体信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditOrder()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            OutStorageEntity entity = WebUtil.GetFormObject<OutStorageEntity>("Entity");
            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            string returnValue = bill.EditOrder(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "出库单主体编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "出库单主体编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑出库单详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditDetail()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            OutStoDetailEntity entity = WebUtil.GetFormObject<OutStoDetailEntity>("Entity");
            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            string returnValue = bill.EditDetail(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "出库单详细项编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "出库单详细项编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据出库单唯一编号查询出库单主体信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrder()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            OutStorageEntity entity = new OutStorageEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            OutStorageEntity result = bill.GetOrder(entity);

            DataResult<OutStorageEntity> dataResult = new DataResult<OutStorageEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据出库单唯一编号查询出库单详细列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            OutStoDetailEntity entity = new OutStoDetailEntity();
            entity.OrderSnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            List<OutStoDetailEntity> list = bill.GetOrderDetail(entity);

            DataResult<List<OutStoDetailEntity>> dataResult = new DataResult<List<OutStoDetailEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询出库单主体分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int OutType = WebUtil.GetFormValue<int>("OutType", 0);
            string CusName = WebUtil.GetFormValue<string>("CusName");
            string CusNum = WebUtil.GetFormValue<string>("CusNum");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string CarrierNum = WebUtil.GetFormValue<string>("CarrierNum");
            string CarrierName = WebUtil.GetFormValue<string>("CarrierName");
            string LogisticsNo = WebUtil.GetFormValue<string>("LogisticsNo");

            OutStorageEntity entity = new OutStorageEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.OutType = OutType;
            entity.CusName = CusName;
            entity.CusNum = CusNum;
            entity.Phone = Phone;
            entity.ContractOrder = ContractOrder;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.StorageNum = StorageNum;
            entity.CarrierNum = CarrierNum;
            entity.CarrierName = CarrierName;
            entity.LogisticsNo = LogisticsNo;

            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            
            List<OutStorageEntity> listResult = bill.GetList(entity, ref pageInfo);
            DataListResult<OutStorageEntity> dataResult = new DataListResult<OutStorageEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 批量删除出库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            string returnValue = bill.Delete(list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "出库单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "出库单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据唯一编号删除出库单
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSingle()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            OutStorageEntity entity = new OutStorageEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            string returnValue = bill.Delete(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "出库单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "出库单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 取消出库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            OutStorageEntity entity = new OutStorageEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            string returnValue = bill.Cancel(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "操作成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "操作失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 审核出库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Audite()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status", (int)EAudite.NotPass);
            string AuditUser = WebUtil.GetFormValue<string>("AuditUser", string.Empty);
            string Reason = WebUtil.GetFormValue<string>("Reason", string.Empty);
            int OperateType = WebUtil.GetFormValue<int>("OperateType", 0);
            string EquipmentNum = WebUtil.GetFormValue<string>("EquipmentNum");
            string EquipmentCode = WebUtil.GetFormValue<string>("EquipmentCode");
            string Remark = WebUtil.GetFormValue<string>("Remark");

            OutStorageEntity entity = new OutStorageEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.Status = Status;
            entity.AuditUser = AuditUser;
            entity.Reason = Reason;
            entity.OperateType = OperateType;
            entity.EquipmentNum = EquipmentNum;
            entity.EquipmentCode = EquipmentCode;
            entity.Remark = Remark;

            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            string returnValue = bill.Audite(entity);
            DataResult result = new DataResult();
            if ("1000" == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "操作成功";
            }
            else if ("1001" == returnValue)
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "出库单不存在";
            }
            else if ("1002" == returnValue)
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "出库单已经审核";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询出库单详细分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetailList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string CusNum = WebUtil.GetFormValue<string>("CusNum");
            string CusName = WebUtil.GetFormValue<string>("CusName");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            int Status = WebUtil.GetFormValue<int>("Status",0);
            int OutType = WebUtil.GetFormValue<int>("OutType",0);
            string CarrierNum = WebUtil.GetFormValue<string>("CarrierNum");
            string CarrierName = WebUtil.GetFormValue<string>("CarrierName");
            string LogisticsNo = WebUtil.GetFormValue<string>("LogisticsNo");

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            OutStoDetailEntity entity = new OutStoDetailEntity();
            entity.CompanyID = CompanyID;
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.StorageNum = StorageNum;
            entity.CusNum = CusNum;
            entity.CusName = CusName;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.Status = Status;
            entity.OutType = OutType;
            entity.OrderNum = OrderNum;
            entity.CarrierNum = CarrierNum;
            entity.CarrierName = CarrierName;
            entity.LogisticsNo = LogisticsNo;
            

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            List<OutStoDetailEntity> listResult = bill.GetDetailList(entity, ref pageInfo);
            DataListResult<OutStoDetailEntity> dataResult = new DataListResult<OutStoDetailEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 设置物流信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SetCarrier()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CarrierNum = WebUtil.GetFormValue<string>("CarrierNum");
            string LogisticsNo = WebUtil.GetFormValue<string>("LogisticsNo");

            OutOrderExt provider = new OutOrderExt(CompanyID);
            int line = provider.SetCarrier(SnNum,CarrierNum,LogisticsNo);

            DataResult dataResult = new DataResult();

            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "设置成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "设置失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 设置出库单打印信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Print()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string PrintUser = WebUtil.GetFormValue<string>("PrintUser");
            DateTime PrintTime = WebUtil.GetFormValue<DateTime>("PrintTime",DateTime.Now);
            OutStorageEntity entity = new OutStorageEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.PrintUser = PrintUser;
            entity.PrintTime = PrintTime;

            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);

            string returnValue = bill.Print(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "打印数据设置成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "打印数据设置失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询出库单的打印数据源
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPrintDataSource()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            DataSet ds = bill.GetPrint(SnNum);

            DataResult<DataSet> dataResult = new DataResult<DataSet>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = ds;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询统计的数据行
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCount()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int OutType = WebUtil.GetFormValue<int>("OutType", 0);
            string CusName = WebUtil.GetFormValue<string>("CusName");
            string CusNum = WebUtil.GetFormValue<string>("CusNum");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");

            OutStorageEntity entity = new OutStorageEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.OutType = OutType;
            entity.CusName = CusName;
            entity.CusNum = CusNum;
            entity.Phone = Phone;
            entity.ContractOrder = ContractOrder;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.StorageNum = StorageNum;

            Bill<OutStorageEntity, OutStoDetailEntity> bill = new OutStorageOrder(CompanyID);
            int Count = bill.GetCount(entity);

            DataResult<int> dataResult = new DataResult<int>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = Count;

            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
