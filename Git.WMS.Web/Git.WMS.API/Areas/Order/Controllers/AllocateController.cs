using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.ORM;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Allocate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Provider.Allocate;
using Git.Storage.Common;
using Git.Storage.Provider;
using System.Data;

namespace Git.WMS.API.Areas.Order.Controllers
{
    public class AllocateController : Controller
    {
        /// <summary>
        /// 新增调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            AllocateOrderEntity entity = WebUtil.GetFormObject<AllocateOrderEntity>("Entity");
            List<AllocateDetailEntity> list = WebUtil.GetFormObject<List<AllocateDetailEntity>>("List");

            entity.SnNum = ConvertHelper.NewGuid();
            entity.ProductType = (int)EProductType.Goods;
            entity.Status = (int)EAudite.Wait;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = entity.CompanyID.IsEmpty() ? CompanyID : entity.CompanyID;
            entity.EquipmentNum = "";
            entity.EquipmentCode = "";

            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            string returnValue = bill.Create(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "调拨单创建成功";
            }
            else if (returnValue == "1001")
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "调拨仓库未初始化";
            }
            else if (returnValue == "1002")
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "调拨仓库待入库未初始化";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "调拨单创建失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            AllocateOrderEntity entity = WebUtil.GetFormObject<AllocateOrderEntity>("Entity");
            List<AllocateDetailEntity> list = WebUtil.GetFormObject<List<AllocateDetailEntity>>("List");

            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            string returnValue = bill.EditOrder(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "调拨单编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "调拨单编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑调拨单主体信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditOrder()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            AllocateOrderEntity entity = WebUtil.GetFormObject<AllocateOrderEntity>("Entity");

            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            string returnValue = bill.EditOrder(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "调拨单主体编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "调拨单主体编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑调拨单详细
        /// </summary>
        /// <returns></returns>
        public ActionResult EditDetail()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            AllocateDetailEntity entity = WebUtil.GetFormObject<AllocateDetailEntity>("Entity");

            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            string returnValue = bill.EditDetail(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "调拨单详细编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "调拨单详细编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrder()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            AllocateOrderEntity entity = new AllocateOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            AllocateOrderEntity result = bill.GetOrder(entity);

            DataResult<AllocateOrderEntity> dataResult = new DataResult<AllocateOrderEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询调拨单详细
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            AllocateDetailEntity entity = new AllocateDetailEntity();
            entity.OrderSnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            List<AllocateDetailEntity> list = bill.GetOrderDetail(entity);

            DataResult<List<AllocateDetailEntity>> dataResult = new DataResult<List<AllocateDetailEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询调拨单分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int AllocateType = WebUtil.GetFormValue<int>("AllocateType", 0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");

            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            AllocateOrderEntity entity = new AllocateOrderEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.AllocateType = AllocateType;
            entity.ContractOrder = ContractOrder;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.StorageNum = StorageNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            
            List<AllocateOrderEntity> listResult = bill.GetList(entity, ref pageInfo);
            DataListResult<AllocateOrderEntity> dataResult = new DataListResult<AllocateOrderEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            string returnValue = bill.Delete(list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "调拨单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "调拨单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据调拨单唯一编号删除调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSingle()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);

            AllocateOrderEntity entity = new AllocateOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;

            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            string returnValue = bill.Delete(entity);

            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "调拨单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "调拨单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 取消调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            AllocateOrderEntity entity = new AllocateOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
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
        /// 审核调拨单
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

            AllocateOrderEntity entity = new AllocateOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.Status = Status;
            entity.AuditUser = AuditUser;
            entity.Reason = Reason;
            entity.OperateType = OperateType;
            entity.EquipmentNum = EquipmentNum;
            entity.EquipmentCode = EquipmentCode;
            entity.Remark = Remark;

            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
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
                result.Message = "调拨单不存在";
            }
            else if ("1002" == returnValue)
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "调拨单已经审核";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询调拨单详细分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetailPage()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int AllocateType = WebUtil.GetFormValue<int>("AllocateType", 0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");

            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            AllocateDetailEntity entity = new AllocateDetailEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.AllocateType = AllocateType;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.StorageNum = StorageNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };

            List<AllocateDetailEntity> listResult = bill.GetDetailList(entity, ref pageInfo);
            DataListResult<AllocateDetailEntity> dataResult = new DataListResult<AllocateDetailEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据条件查询统计调拨单统计行
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCount()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int AllocateType = WebUtil.GetFormValue<int>("AllocateType", 0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");

            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            AllocateOrderEntity entity = new AllocateOrderEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.AllocateType = AllocateType;
            entity.ContractOrder = ContractOrder;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.StorageNum = StorageNum;

            int Count = bill.GetCount(entity);

            DataResult<int> dataResult = new DataResult<int>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = Count;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 设置打印数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Print()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string PrintUser = WebUtil.GetFormValue<string>("PrintUser");
            DateTime PrintTime = WebUtil.GetFormValue<DateTime>("PrintTime");

            AllocateOrderEntity entity = new AllocateOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.PrintUser = PrintUser;
            entity.PrintTime = PrintTime;

            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);

            string returnValue = bill.Print(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "设置成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "设置失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 获得打印的数据源
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPrint()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            Bill<AllocateOrderEntity, AllocateDetailEntity> bill = new AllocateOrder(CompanyID);
            DataSet ds = bill.GetPrint(SnNum);
            DataResult<DataSet> dataResult = new DataResult<DataSet>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = ds;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据调拨单号查询调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderByNum()
        {
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);

            AllocateOrderExt bill = new AllocateOrderExt(CompanyID);
            AllocateOrderEntity result = bill.GetOrderByNum(OrderNum);

            DataResult<AllocateOrderEntity> dataResult = new DataResult<AllocateOrderEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
