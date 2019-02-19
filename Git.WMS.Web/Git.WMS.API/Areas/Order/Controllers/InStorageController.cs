using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.InStorage;
using Git.Storage.Provider;
using Git.Storage.Provider.InStorage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Order.Controllers
{
    public class InStorageController : Controller
    {
        /// <summary>
        /// 新增入库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InStorageEntity entity = WebUtil.GetFormObject<InStorageEntity>("Entity");
            List<InStorDetailEntity> list = WebUtil.GetFormObject<List<InStorDetailEntity>>("List");

            entity.SnNum = ConvertHelper.NewGuid();
            entity.ProductType = (int)EProductType.Goods;
            entity.Status = (int)EAudite.Wait;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = entity.CompanyID.IsEmpty() ? CompanyID : entity.CompanyID;
            entity.EquipmentNum = "";
            entity.EquipmentCode = "";

            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            string returnValue = bill.Create(entity,list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "入库单创建成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "入库单创建失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑入库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InStorageEntity entity = WebUtil.GetFormObject<InStorageEntity>("Entity");
            List<InStorDetailEntity> list = WebUtil.GetFormObject<List<InStorDetailEntity>>("List");

            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            string returnValue = bill.EditOrder(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "入库单编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "入库单编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑入库单的主体
        /// </summary>
        /// <returns></returns>
        public ActionResult EditOrder()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InStorageEntity entity = WebUtil.GetFormObject<InStorageEntity>("Entity");

            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            string returnValue = bill.EditOrder(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "入库单主体编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "入库单主体编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑入库单的详细项
        /// </summary>
        /// <returns></returns>
        public ActionResult EditDetail()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InStorDetailEntity entity = WebUtil.GetFormObject<InStorDetailEntity>("Detail");
            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            string returnValue = bill.EditDetail(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "入库单详细项编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "入库单详细项编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据入库单唯一编号查询入库单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrder()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InStorageEntity entity = new InStorageEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            InStorageEntity result = bill.GetOrder(entity);

            DataResult<InStorageEntity> dataResult = new DataResult<InStorageEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据订单唯一编号查询入库单详细
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InStorDetailEntity entity = new InStorDetailEntity();
            entity.OrderSnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            List<InStorDetailEntity> list = bill.GetOrderDetail(entity);

            DataResult<List<InStorDetailEntity>> dataResult = new DataResult<List<InStorDetailEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据唯一编号查询入库单明细数据行
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetailInfo()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            InStorDetailEntity entity = bill.GetDetail(SnNum);

            DataResult<InStorDetailEntity> dataResult = new DataResult<InStorDetailEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = entity;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询入库单分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int InType = WebUtil.GetFormValue<int>("InType",0);
            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string SupName = WebUtil.GetFormValue<string>("SupName");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");

            InStorageEntity entity = new InStorageEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.InType = InType;
            entity.SupNum = SupNum;
            entity.SupName = SupName;
            entity.Phone = Phone;
            entity.StorageNum = StorageNum;
            entity.ContractOrder = ContractOrder;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;

            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            PageInfo pageInfo = new PageInfo() { PageIndex=PageIndex,PageSize=PageSize };
            List<InStorageEntity> listResult = bill.GetList(entity, ref pageInfo);
            DataListResult<InStorageEntity> dataResult = new DataListResult<InStorageEntity>() 
            { 
                Code=(int)EResponseCode.Success,
                Message="响应成功",
                Result=listResult,
                PageInfo=pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除入库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            string returnValue = bill.Delete(list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "入库单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "入库单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据唯一编号删除入库单
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSingle()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string SnNum = WebUtil.GetFormValue<string>("SnNum",string.Empty);

            InStorageEntity entity = new InStorageEntity();
            entity.CompanyID = CompanyID;
            entity.SnNum = SnNum;

            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            string returnValue = bill.Delete(entity);

            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "入库单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "入库单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 取消入库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InStorageEntity entity = new InStorageEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
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
        /// 审核入库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Audite()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status",(int)EAudite.NotPass);
            string AuditUser = WebUtil.GetFormValue<string>("AuditUser",string.Empty);
            string Reason = WebUtil.GetFormValue<string>("Reason",string.Empty);
            int OperateType = WebUtil.GetFormValue<int>("OperateType",0);
            string EquipmentNum = WebUtil.GetFormValue<string>("EquipmentNum");
            string EquipmentCode = WebUtil.GetFormValue<string>("EquipmentCode");
            string Remark = WebUtil.GetFormValue<string>("Remark");

            InStorageEntity entity = new InStorageEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.Status = Status;
            entity.AuditUser = AuditUser;
            entity.Reason = Reason;
            entity.OperateType = OperateType;
            entity.EquipmentNum = EquipmentNum;
            entity.EquipmentCode = EquipmentCode;
            entity.Remark = Remark;

            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
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
                result.Message = "入库单不存在";
            }
            else if ("1002" == returnValue)
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "入库单已经审核";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询入库当详细分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetailList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string SupName = WebUtil.GetFormValue<string>("SupName");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status",0);
            int InType = WebUtil.GetFormValue<int>("InType", 0);
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            InStorDetailEntity entity = new InStorDetailEntity();
            entity.CompanyID = CompanyID;
            entity.BarCode = BarCode;
            entity.OrderNum = OrderNum;
            entity.ProductName = ProductName;
            entity.StorageNum = StorageNum;
            entity.SupNum = SupNum;
            entity.SupName = SupName;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.Status = Status;
            entity.InType = InType;
            entity.ContractOrder = ContractOrder;

            PageInfo pageInfo = new PageInfo() { PageIndex=PageIndex,PageSize=PageSize };
            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            List<InStorDetailEntity> listResult = bill.GetDetailList(entity, ref pageInfo);
            DataListResult<InStorDetailEntity> dataResult = new DataListResult<InStorDetailEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 入库单打印的数据写入
        /// </summary>
        /// <returns></returns>
        public ActionResult Print()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string PrintUser = WebUtil.GetFormValue<string>("PrintUser");
            DateTime PrintTime = WebUtil.GetFormValue<DateTime>("PrintTime");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            InStorageEntity entity = new InStorageEntity();
            entity.SnNum = SnNum;
            entity.PrintUser = PrintUser;
            entity.PrintTime = PrintTime;
            entity.CompanyID = CompanyID;

            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
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
        /// 查询打印的数据集合
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPrintSource()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            DataSet ds = bill.GetPrint(SnNum);

            DataResult<DataSet> dataResult = new DataResult<DataSet>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = ds;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 统计入库单数据行
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCount()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int InType = WebUtil.GetFormValue<int>("InType", 0);
            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string SupName = WebUtil.GetFormValue<string>("SupName");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");

            InStorageEntity entity = new InStorageEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.InType = InType;
            entity.SupNum = SupNum;
            entity.SupName = SupName;
            entity.Phone = Phone;
            entity.StorageNum = StorageNum;
            entity.ContractOrder = ContractOrder;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;

            Bill<InStorageEntity, InStorDetailEntity> bill = new InStorageOrder(CompanyID);
            int Count = bill.GetCount(entity);

            DataResult<int> dataResult = new DataResult<int>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = Count;

            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
