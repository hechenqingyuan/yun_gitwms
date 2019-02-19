using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Biz;
using Git.Storage.Provider;
using Git.Storage.Provider.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Biz.Controllers
{
    public class PurchaseController : Controller
    {
        /// <summary>
        /// 新增采购订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PurchaseEntity entity = WebUtil.GetFormObject<PurchaseEntity>("Entity");
            List<PurchaseDetailEntity> list = WebUtil.GetFormObject<List<PurchaseDetailEntity>>("List");

            entity.SnNum = ConvertHelper.NewGuid();
            entity.AuditeStatus = (int)EAudite.Wait;
            entity.Status = (int)EPurchaseStatus.CreateOrder;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = entity.CompanyID.IsEmpty() ? CompanyID : entity.CompanyID;

            Bill<PurchaseEntity, PurchaseDetailEntity> bill = new PurchaseOrder(CompanyID);
            string returnValue = bill.Create(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "采购订单创建成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "采购订单创建失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑采购订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PurchaseEntity entity = WebUtil.GetFormObject<PurchaseEntity>("Entity");
            List<PurchaseDetailEntity> list = WebUtil.GetFormObject<List<PurchaseDetailEntity>>("List");

            Bill<PurchaseEntity, PurchaseDetailEntity> bill = new PurchaseOrder(CompanyID);
            string returnValue = bill.EditOrder(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "采购订单编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "采购订单编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询采购订单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrder()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PurchaseEntity entity = new PurchaseEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<PurchaseEntity, PurchaseDetailEntity> bill = new PurchaseOrder(CompanyID);
            PurchaseEntity result = bill.GetOrder(entity);

            DataResult<PurchaseEntity> dataResult = new DataResult<PurchaseEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询采购订单详细
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PurchaseDetailEntity entity = new PurchaseDetailEntity();
            entity.OrderSnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<PurchaseEntity, PurchaseDetailEntity> bill = new PurchaseOrder(CompanyID);
            List<PurchaseDetailEntity> list = bill.GetOrderDetail(entity);

            DataResult<List<PurchaseDetailEntity>> dataResult = new DataResult<List<PurchaseDetailEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询采购订单分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int OrderType = WebUtil.GetFormValue<int>("OrderType", 0);
            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string SupName = WebUtil.GetFormValue<string>("SupName");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int AuditeStatus = WebUtil.GetFormValue<int>("AuditeStatus", 0);
            int Status = WebUtil.GetFormValue<int>("Status", 0);

            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string BeginOrderTime = WebUtil.GetFormValue<string>("BeginOrderTime");
            string EndOrderTime = WebUtil.GetFormValue<string>("EndOrderTime");
            string BeginRevDate = WebUtil.GetFormValue<string>("BeginRevDate");
            string EndRevDate = WebUtil.GetFormValue<string>("EndRevDate");

            PurchaseEntity entity = new PurchaseEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.OrderType = OrderType;
            entity.SupNum = SupNum;
            entity.SupName = SupName;
            entity.Contact = Contact;
            entity.Phone = Phone;
            entity.ContractOrder = ContractOrder;
            entity.AuditeStatus = AuditeStatus;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.BeginOrderTime = BeginOrderTime;
            entity.EndOrderTime = EndOrderTime;
            entity.BeginRevDate = BeginRevDate;
            entity.EndRevDate = EndRevDate;

            Bill<PurchaseEntity, PurchaseDetailEntity> bill = new PurchaseOrder(CompanyID);
            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            
            List<PurchaseEntity> listResult = bill.GetList(entity, ref pageInfo);
            DataListResult<PurchaseEntity> dataResult = new DataListResult<PurchaseEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除采购订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<PurchaseEntity, PurchaseDetailEntity> bill = new PurchaseOrder(CompanyID);
            string returnValue = bill.Delete(list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "采购订单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "采购订单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 取消采购订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PurchaseEntity entity = new PurchaseEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<PurchaseEntity, PurchaseDetailEntity> bill = new PurchaseOrder(CompanyID);
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
        /// 审核采购订单
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

            PurchaseEntity entity = new PurchaseEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.Status = Status;
            entity.AuditeStatus = Status;
            entity.Reason = Reason;
            entity.Remark = Remark;

            Bill<PurchaseEntity, PurchaseDetailEntity> bill = new PurchaseOrder(CompanyID);
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
                result.Message = "采购订单不存在";
            }
            else if ("1002" == returnValue)
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "采购订单已经审核";
            }
            return Content(JsonHelper.SerializeObject(result));
        }


        /// <summary>
        /// 查询采购订单详细分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetailList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");

            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string OrderSnNum = WebUtil.GetFormValue<string>("OrderSnNum");
            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string SupName = WebUtil.GetFormValue<string>("SupName");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");

            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            string ContractSn = WebUtil.GetFormValue<string>("ContractSn");
            
            int Status = WebUtil.GetFormValue<int>("Status",0);
            int AuditeStatus = WebUtil.GetFormValue<int>("AuditeStatus",-1);
            int HasReturn = WebUtil.GetFormValue<int>("HasReturn",-1);

            string CreateUser = WebUtil.GetFormValue<string>("CreateUser");

            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            PurchaseDetailEntity entity = new PurchaseDetailEntity();
            entity.CompanyID = CompanyID;
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.OrderNum = OrderNum;
            entity.OrderSnNum = OrderSnNum;
            entity.SupNum = SupNum;
            entity.SupName = SupName;
            entity.Contact = Contact;
            entity.Phone = Phone;
            entity.ContractOrder = ContractOrder;
            entity.ContractSn = ContractSn;
            entity.Status = Status;
            entity.AuditeStatus = AuditeStatus;
            entity.HasReturn = HasReturn;
            entity.CreateUser = CreateUser;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            Bill<PurchaseEntity, PurchaseDetailEntity> bill = new PurchaseOrder(CompanyID);
            List<PurchaseDetailEntity> listResult = bill.GetDetailList(entity, ref pageInfo);
            DataListResult<PurchaseDetailEntity> dataResult = new DataListResult<PurchaseDetailEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 财务入账
        /// </summary>
        /// <returns></returns>
        public ActionResult ToFiance()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PurchaseOrderExt provider = new PurchaseOrderExt(CompanyID);
            string line = provider.ToFiance(SnNum);
            DataResult dataResult = new DataResult();
            if (line == "1000")
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "账务记录生成成功";
            }
            else if (line == "1001")
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "采购订单不存在";
            }
            else if (line == "1002")
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "已经生成账务记录,不要重复操作";
            }
            else if (line == "1003")
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "账务记录生成异常";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 采购入库
        /// </summary>
        /// <returns></returns>
        public ActionResult ToInStorage()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string CreateUser = WebUtil.GetFormValue<string>("CreateUser");
            List<PurchaseDetailEntity> list = WebUtil.GetFormObject<List<PurchaseDetailEntity>>("List");

            PurchaseOrderExt provider = new PurchaseOrderExt(CompanyID);
            DataResult dataResult = provider.ToInStorage(SnNum, list, StorageNum, CreateUser);
            return Content(JsonHelper.SerializeObject(dataResult));
        }


        /// <summary>
        /// 采购退货
        /// </summary>
        /// <returns></returns>
        public ActionResult ToReturn()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string CreateUser = WebUtil.GetFormValue<string>("CreateUser");
            List<PurchaseDetailEntity> list = WebUtil.GetFormObject<List<PurchaseDetailEntity>>("List");

            PurchaseOrderExt provider = new PurchaseOrderExt(CompanyID);
            DataResult dataResult = provider.ToReturn(SnNum, list);
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
