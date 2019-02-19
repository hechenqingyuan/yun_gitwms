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
    public class SaleReturnController : Controller
    {
        /// <summary>
        /// 创建销售退货订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            SaleReturnEntity entity = WebUtil.GetFormObject<SaleReturnEntity>("Entity");
            List<SaleReturnDetailEntity> list = WebUtil.GetFormObject<List<SaleReturnDetailEntity>>("List");

            entity.CompanyID = entity.CompanyID.IsEmpty() ? CompanyID : entity.CompanyID;

            Bill<SaleReturnEntity, SaleReturnDetailEntity> bill = new SaleReturnOrder(CompanyID);
            string returnValue = bill.Create(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "销售退货订单创建成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "销售退货订单创建失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑销售退货订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            SaleReturnEntity entity = WebUtil.GetFormObject<SaleReturnEntity>("Entity");
            List<SaleReturnDetailEntity> list = WebUtil.GetFormObject<List<SaleReturnDetailEntity>>("List");

            Bill<SaleReturnEntity, SaleReturnDetailEntity> bill = new SaleReturnOrder(CompanyID);
            string returnValue = bill.EditOrder(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "销售退货订单编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "销售退货订单编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询销售退货订单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrder()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            SaleReturnEntity entity = new SaleReturnEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<SaleReturnEntity, SaleReturnDetailEntity> bill = new SaleReturnOrder(CompanyID);
            SaleReturnEntity result = bill.GetOrder(entity);

            DataResult<SaleReturnEntity> dataResult = new DataResult<SaleReturnEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询销售退货订单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            SaleReturnDetailEntity entity = new SaleReturnDetailEntity();
            entity.OrderSnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<SaleReturnEntity, SaleReturnDetailEntity> bill = new SaleReturnOrder(CompanyID);
            List<SaleReturnDetailEntity> list = bill.GetOrderDetail(entity);

            DataResult<List<SaleReturnDetailEntity>> dataResult = new DataResult<List<SaleReturnDetailEntity>>();
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
            
            string CusNum = WebUtil.GetFormValue<string>("CusNum");
            string CusName = WebUtil.GetFormValue<string>("CusName");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string SaleSnNum = WebUtil.GetFormValue<string>("SaleSnNum");
            string SaleOrderNum = WebUtil.GetFormValue<string>("SaleOrderNum");
            
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");

            Bill<SaleReturnEntity, SaleReturnDetailEntity> bill = new SaleReturnOrder(CompanyID);
            SaleReturnEntity entity = new SaleReturnEntity();
            entity.OrderNum = OrderNum;
            entity.CusNum = CusNum;
            entity.CusName = CusName;
            entity.Contact = Contact;
            entity.Phone = Phone;
            entity.SaleSnNum = SaleSnNum;
            entity.SaleOrderNum = SaleOrderNum;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };

            List<SaleReturnEntity> listResult = bill.GetList(entity, ref pageInfo);
            DataListResult<SaleReturnEntity> dataResult = new DataListResult<SaleReturnEntity>()
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
            Bill<SaleReturnEntity, SaleReturnDetailEntity> bill = new SaleReturnOrder(CompanyID);
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
        /// 取消销售退货订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            SaleReturnEntity entity = new SaleReturnEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<SaleReturnEntity, SaleReturnDetailEntity> bill = new SaleReturnOrder(CompanyID);
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
        /// 审核销售退货订单
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

            SaleReturnEntity entity = new SaleReturnEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.Status = Status;
            entity.Reason = Reason;
            entity.Remark = Remark;

            Bill<SaleReturnEntity, SaleReturnDetailEntity> bill = new SaleReturnOrder(CompanyID);
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
                result.Message = "销售退货订单不存在";
            }
            else if ("1002" == returnValue)
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "销售退货订单已经审核";
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

            string CusNum = WebUtil.GetFormValue<string>("CusNum");
            string CusName = WebUtil.GetFormValue<string>("CusName");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string SaleSnNum = WebUtil.GetFormValue<string>("SaleSnNum");
            string SaleOrderNum = WebUtil.GetFormValue<string>("SaleOrderNum");

            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");

            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");

            SaleReturnDetailEntity entity = new SaleReturnDetailEntity();
            entity.OrderNum = OrderNum;
            entity.CusNum = CusNum;
            entity.CusName = CusName;
            entity.Contact = Contact;
            entity.Phone = Phone;
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.SaleSnNum = SaleSnNum;
            entity.SaleOrderNum = SaleOrderNum;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };

            Bill<SaleReturnEntity, SaleReturnDetailEntity> bill = new SaleReturnOrder(CompanyID);
            List<SaleReturnDetailEntity> listResult = bill.GetDetailList(entity, ref pageInfo);
            DataListResult<SaleReturnDetailEntity> dataResult = new DataListResult<SaleReturnDetailEntity>()
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
