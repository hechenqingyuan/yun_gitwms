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
    public class SaleController : Controller
    {
        /// <summary>
        /// 新增销售订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            SaleOrderEntity entity = WebUtil.GetFormObject<SaleOrderEntity>("Entity");
            List<SaleDetailEntity> list = WebUtil.GetFormObject<List<SaleDetailEntity>>("List");

            entity.SnNum = ConvertHelper.NewGuid();
            entity.AuditeStatus = (int)EAudite.Wait;
            entity.Status = (int)EOrderStatus.CreateOrder;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = entity.CompanyID.IsEmpty() ? CompanyID : entity.CompanyID;

            Bill<SaleOrderEntity, SaleDetailEntity> bill = new SaleOrder(CompanyID);
            string returnValue = bill.Create(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "销售订单创建成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "销售订单创建失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑销售订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            SaleOrderEntity entity = WebUtil.GetFormObject<SaleOrderEntity>("Entity");
            List<SaleDetailEntity> list = WebUtil.GetFormObject<List<SaleDetailEntity>>("List");

            Bill<SaleOrderEntity, SaleDetailEntity> bill = new SaleOrder(CompanyID);
            string returnValue = bill.EditOrder(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "销售订单编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "销售订单编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询销售订单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrder()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            SaleOrderEntity entity = new SaleOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<SaleOrderEntity, SaleDetailEntity> bill = new SaleOrder(CompanyID);
            SaleOrderEntity result = bill.GetOrder(entity);

            DataResult<SaleOrderEntity> dataResult = new DataResult<SaleOrderEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询销售订单详细
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            SaleDetailEntity entity = new SaleDetailEntity();
            entity.OrderSnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<SaleOrderEntity, SaleDetailEntity> bill = new SaleOrder(CompanyID);
            List<SaleDetailEntity> list = bill.GetOrderDetail(entity);

            DataResult<List<SaleDetailEntity>> dataResult = new DataResult<List<SaleDetailEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询销售订单分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int OrderType = WebUtil.GetFormValue<int>("OrderType", 0);

            string CusNum = WebUtil.GetFormValue<string>("CusNum");
            string CusName = WebUtil.GetFormValue<string>("CusName");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");

            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            string ContractSn = WebUtil.GetFormValue<string>("ContractSn");

            int AuditeStatus = WebUtil.GetFormValue<int>("AuditeStatus", -1);
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");

            string BeginOrderTime = WebUtil.GetFormValue<string>("BeginOrderTime");
            string EndOrderTime = WebUtil.GetFormValue<string>("EndOrderTime");

            string BeginSendTime = WebUtil.GetFormValue<string>("BeginSendTime");
            string EndSendTime = WebUtil.GetFormValue<string>("EndSendTime");

            Bill<SaleOrderEntity, SaleDetailEntity> bill = new SaleOrder(CompanyID);
            SaleOrderEntity entity = new SaleOrderEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.OrderType = OrderType;
            entity.CusNum = CusNum;
            entity.CusName = CusName;
            entity.Contact = Contact;
            entity.Phone = Phone;
            entity.ContractOrder = ContractOrder;
            entity.ContractSn = ContractSn;
            entity.AuditeStatus = AuditeStatus;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.BeginOrderTime = BeginOrderTime;
            entity.EndOrderTime = EndOrderTime;
            entity.BeginSendTime = BeginSendTime;
            entity.EndSendTime = EndSendTime;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            
            List<SaleOrderEntity> listResult = bill.GetList(entity, ref pageInfo);
            DataListResult<SaleOrderEntity> dataResult = new DataListResult<SaleOrderEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除销售订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<SaleOrderEntity, SaleDetailEntity> bill = new SaleOrder(CompanyID);
            string returnValue = bill.Delete(list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "销售订单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "销售订单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 取消销售订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            SaleOrderEntity entity = new SaleOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<SaleOrderEntity, SaleDetailEntity> bill = new SaleOrder(CompanyID);
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
        /// 审核销售订单
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

            SaleOrderEntity entity = new SaleOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.Status = Status;
            entity.AuditeStatus = Status;
            entity.Reason = Reason;
            entity.Remark = Remark;

            Bill<SaleOrderEntity, SaleDetailEntity> bill = new SaleOrder(CompanyID);
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
                result.Message = "销售订单不存在";
            }
            else if ("1002" == returnValue)
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "销售订单已经审核";
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

            string OrderSnNum = WebUtil.GetFormValue<string>("OrderSnNum");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int OrderType = WebUtil.GetFormValue<int>("OrderType", 0);
            string CusName = WebUtil.GetFormValue<string>("CusName", string.Empty);
            string CusNum = WebUtil.GetFormValue<string>("CusNum", string.Empty);
            string Phone = WebUtil.GetFormValue<string>("Phone", string.Empty);

            int Status = WebUtil.GetFormValue<int>("Status", 0);
            int AuditeStatus = WebUtil.GetFormValue<int>("AuditeStatus", 0);

            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder", string.Empty);
            string CusOrderNum = WebUtil.GetFormValue<string>("CusOrderNum", string.Empty);

            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);

            string BeginTime = WebUtil.GetFormValue<string>("BeginTime", string.Empty);
            string EndTime = WebUtil.GetFormValue<string>("EndTime", string.Empty);

            SaleDetailEntity entity = new SaleDetailEntity();
            entity.CompanyID = CompanyID;
            entity.OrderSnNum = OrderSnNum;
            entity.OrderNum = OrderNum;
            entity.CusName = CusName;
            entity.CusNum = CusNum;
            entity.Phone = Phone;
            entity.Status = Status;
            entity.AuditeStatus = AuditeStatus;

            entity.ContractOrder = ContractOrder;
            entity.CusOrderNum = CusOrderNum;
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            Bill<SaleOrderEntity, SaleDetailEntity> bill = new SaleOrder(CompanyID);
            List<SaleDetailEntity> listResult = bill.GetDetailList(entity, ref pageInfo);
            DataListResult<SaleDetailEntity> dataResult = new DataListResult<SaleDetailEntity>()
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
            SaleOrderExt provider = new SaleOrderExt(CompanyID);
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
                dataResult.Message = "销售订单不存在";
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
        /// 销售出库
        /// </summary>
        /// <returns></returns>
        public ActionResult ToOutStorage()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            List<SaleDetailEntity> list = WebUtil.GetFormObject<List<SaleDetailEntity>>("List");

            SaleOrderExt provider = new SaleOrderExt(CompanyID);
            DataResult dataResult = provider.ToOutStorage(SnNum, list, StorageNum);
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 销售退货单创建成功
        /// </summary>
        /// <returns></returns>
        public ActionResult ToReturn()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            List<SaleDetailEntity> list = WebUtil.GetFormObject<List<SaleDetailEntity>>("List");

            SaleOrderExt provider = new SaleOrderExt(CompanyID);
            DataResult dataResult = provider.ToReturn(SnNum, list);
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
