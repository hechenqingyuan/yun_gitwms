using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Storage.Common.Enum;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Common;
using Git.Storage.Entity.Bad;
using Git.Storage.Provider;
using Git.Storage.Provider.Bad;
using System.Data;

namespace Git.WMS.API.Areas.Order.Controllers
{
    public class BadController : Controller
    {
        /// <summary>
        /// 新增报损单
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            BadReportEntity entity = WebUtil.GetFormObject<BadReportEntity>("Entity");
            List<BadReportDetailEntity> list = WebUtil.GetFormObject<List<BadReportDetailEntity>>("List");

            entity.SnNum = ConvertHelper.NewGuid();
            entity.ProductType = (int)EProductType.Goods;
            entity.Status = (int)EAudite.Wait;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = entity.CompanyID.IsEmpty() ? CompanyID : entity.CompanyID;
            entity.EquipmentNum = "";
            entity.EquipmentCode = "";

            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
            string returnValue = bill.Create(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "报损单创建成功";
            }
            else if (returnValue == "1001")
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "仓库未初始化";
            }
            else if (returnValue == "1002")
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "报损库位未初始化";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "报损单创建失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑报损单
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            BadReportEntity entity = WebUtil.GetFormObject<BadReportEntity>("Entity");
            List<BadReportDetailEntity> list = WebUtil.GetFormObject<List<BadReportDetailEntity>>("List");

            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
            string returnValue = bill.EditOrder(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "报损单编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "报损单编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑报损单主体信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditOrder()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            BadReportEntity entity = WebUtil.GetFormObject<BadReportEntity>("Entity");
            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
            string returnValue = bill.EditOrder(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "报损单主体编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "报损单主体编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑报损单详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditDetail()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            BadReportDetailEntity entity = WebUtil.GetFormObject<BadReportDetailEntity>("Entity");
            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
            string returnValue = bill.EditDetail(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "报损单详细编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "报损单详细编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询报损单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrder()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            BadReportEntity entity = new BadReportEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
            BadReportEntity result = bill.GetOrder(entity);

            DataResult<BadReportEntity> dataResult = new DataResult<BadReportEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询报损单详细
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            BadReportDetailEntity entity = new BadReportDetailEntity();
            entity.OrderSnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
            List<BadReportDetailEntity> list = bill.GetOrderDetail(entity);

            DataResult<List<BadReportDetailEntity>> dataResult = new DataResult<List<BadReportDetailEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询报损单分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int BadType = WebUtil.GetFormValue<int>("BadType", 0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");

            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
            BadReportEntity entity = new BadReportEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.BadType = BadType;
            entity.ContractOrder = ContractOrder;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.StorageNum = StorageNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            
            List<BadReportEntity> listResult = bill.GetList(entity, ref pageInfo);
            DataListResult<BadReportEntity> dataResult = new DataListResult<BadReportEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除报损单
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
            string returnValue = bill.Delete(list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "报损单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "报损单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据唯一编号删除报损单
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSingle()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);

            BadReportEntity entity = new BadReportEntity();
            entity.SnNum = SnNum;

            string returnValue = bill.Delete(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "报损单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "报损单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 取消报损单
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            BadReportEntity entity = new BadReportEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
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
        /// 审核报损单
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

            BadReportEntity entity = new BadReportEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.Status = Status;
            entity.AuditUser = AuditUser;
            entity.Reason = Reason;
            entity.OperateType = OperateType;
            entity.EquipmentNum = EquipmentNum;
            entity.EquipmentCode = EquipmentCode;
            entity.Remark = Remark;

            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
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
                result.Message = "报损单不存在";
            }
            else if ("1002" == returnValue)
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "报损单已经审核";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询报损单详细分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetailPage()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int BadType = WebUtil.GetFormValue<int>("BadType", 0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");

            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
            BadReportDetailEntity entity = new BadReportDetailEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.BadType = BadType;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.StorageNum = StorageNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };

            List<BadReportDetailEntity> listResult = bill.GetDetailList(entity, ref pageInfo);
            DataListResult<BadReportDetailEntity> dataResult = new DataListResult<BadReportDetailEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 打印报损单设置
        /// </summary>
        /// <returns></returns>
        public ActionResult Print()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string PrintUser = WebUtil.GetFormValue<string>("PrintUser");
            DateTime PrintTime = WebUtil.GetFormValue<DateTime>("PrintTime",DateTime.Now);

            BadReportEntity entity = new BadReportEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.PrintUser = PrintUser;
            entity.PrintTime = PrintTime;

            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
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
        /// 获得报损单打印的数据源
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPrint()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
            DataSet ds = bill.GetPrint(SnNum);

            DataResult<DataSet> dataResult = new DataResult<DataSet>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = ds;

            return Content(JsonHelper.SerializeObject(dataResult));
        }


        /// <summary>
        /// 根据条件统计报损单的行数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCount()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int BadType = WebUtil.GetFormValue<int>("BadType", 0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");

            Bill<BadReportEntity, BadReportDetailEntity> bill = new BadOrder(CompanyID);
            BadReportEntity entity = new BadReportEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.BadType = BadType;
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
        /// 根据报损单号查询报损单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderByNum()
        {
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);

            BadOrderExt bill = new BadOrderExt(CompanyID);
            BadReportEntity result = bill.GetOrderByNum(OrderNum);

            DataResult<BadReportEntity> dataResult = new DataResult<BadReportEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
