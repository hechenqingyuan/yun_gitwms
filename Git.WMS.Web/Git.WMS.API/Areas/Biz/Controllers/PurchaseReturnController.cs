using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Biz;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Provider;
using Git.Storage.Provider.Biz;
using Git.Storage.Common;

namespace Git.WMS.API.Areas.Biz.Controllers
{
    public class PurchaseReturnController : Controller
    {
        /// <summary>
        /// 创建采购退货单
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PurchaseReturnEntity entity = WebUtil.GetFormObject<PurchaseReturnEntity>("Entity");
            List<PurchaseReturnDetailEntity> list = WebUtil.GetFormObject<List<PurchaseReturnDetailEntity>>("List");

            entity.CompanyID = entity.CompanyID.IsEmpty() ? CompanyID : entity.CompanyID;

            Bill<PurchaseReturnEntity, PurchaseReturnDetailEntity> bill = new PurchaseReturnOrder(CompanyID);
            string returnValue = bill.Create(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "采购退货单创建成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "采购退货单创建失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑采购退货单
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PurchaseReturnEntity entity = WebUtil.GetFormObject<PurchaseReturnEntity>("Entity");
            List<PurchaseReturnDetailEntity> list = WebUtil.GetFormObject<List<PurchaseReturnDetailEntity>>("List");

            Bill<PurchaseReturnEntity, PurchaseReturnDetailEntity> bill = new PurchaseReturnOrder(CompanyID);
            string returnValue = bill.EditOrder(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "采购退货单编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "采购退货单编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询采购退货单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrder()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PurchaseReturnEntity entity = new PurchaseReturnEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<PurchaseReturnEntity, PurchaseReturnDetailEntity> bill = new PurchaseReturnOrder(CompanyID);
            PurchaseReturnEntity result = bill.GetOrder(entity);

            DataResult<PurchaseReturnEntity> dataResult = new DataResult<PurchaseReturnEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询采购退货单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PurchaseReturnDetailEntity entity = new PurchaseReturnDetailEntity();
            entity.OrderSnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<PurchaseReturnEntity, PurchaseReturnDetailEntity> bill = new PurchaseReturnOrder(CompanyID);
            List<PurchaseReturnDetailEntity> list = bill.GetOrderDetail(entity);

            DataResult<List<PurchaseReturnDetailEntity>> dataResult = new DataResult<List<PurchaseReturnDetailEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 销售退货单分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");

            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string SupName = WebUtil.GetFormValue<string>("SupName");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string PurchaseSnNum = WebUtil.GetFormValue<string>("PurchaseSnNum");
            string PurchaseOrderNum = WebUtil.GetFormValue<string>("PurchaseOrderNum");

            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");

            Bill<PurchaseReturnEntity, PurchaseReturnDetailEntity> bill = new PurchaseReturnOrder(CompanyID);
            PurchaseReturnEntity entity = new PurchaseReturnEntity();
            entity.OrderNum = OrderNum;
            entity.SupNum = SupNum;
            entity.SupName = SupName;
            entity.Contact = Contact;
            entity.Phone = Phone;
            entity.PurchaseSnNum = PurchaseSnNum;
            entity.PurchaseOrderNum = PurchaseOrderNum;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };

            List<PurchaseReturnEntity> listResult = bill.GetList(entity, ref pageInfo);
            DataListResult<PurchaseReturnEntity> dataResult = new DataListResult<PurchaseReturnEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除销售退货单
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<PurchaseReturnEntity, PurchaseReturnDetailEntity> bill = new PurchaseReturnOrder(CompanyID);
            string returnValue = bill.Delete(list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "销售退货单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "销售退货单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 取消采购退货单
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PurchaseReturnEntity entity = new PurchaseReturnEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<PurchaseReturnEntity, PurchaseReturnDetailEntity> bill = new PurchaseReturnOrder(CompanyID);
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
        /// 审核采购退货单
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

            PurchaseReturnEntity entity = new PurchaseReturnEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.Status = Status;
            entity.Reason = Reason;
            entity.Remark = Remark;

            Bill<PurchaseReturnEntity, PurchaseReturnDetailEntity> bill = new PurchaseReturnOrder(CompanyID);
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
                result.Message = "采购退货单不存在";
            }
            else if ("1002" == returnValue)
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "采购退货单已经审核";
            }
            return Content(JsonHelper.SerializeObject(result));
        }


        /// <summary>
        /// 查询销售单详细分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetailList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");

            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string SupName = WebUtil.GetFormValue<string>("SupName");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string PurchaseSnNum = WebUtil.GetFormValue<string>("PurchaseSnNum");
            string PurchaseOrderNum = WebUtil.GetFormValue<string>("PurchaseOrderNum");
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");

            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");

            PurchaseReturnDetailEntity entity = new PurchaseReturnDetailEntity();
            entity.OrderNum = OrderNum;
            entity.SupNum = SupNum;
            entity.SupName = SupName;
            entity.Contact = Contact;
            entity.Phone = Phone;
            entity.PurchaseOrderNum = PurchaseOrderNum;
            entity.PurchaseSnNum = PurchaseSnNum;
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };

            Bill<PurchaseReturnEntity, PurchaseReturnDetailEntity> bill = new PurchaseReturnOrder(CompanyID);
            List<PurchaseReturnDetailEntity> listResult = bill.GetDetailList(entity, ref pageInfo);
            DataListResult<PurchaseReturnDetailEntity> dataResult = new DataListResult<PurchaseReturnDetailEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
