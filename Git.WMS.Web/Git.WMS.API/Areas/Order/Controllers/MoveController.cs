using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Move;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Common;
using Git.Storage.Provider.Move;
using Git.Storage.Provider;
using System.Data;

namespace Git.WMS.API.Areas.Order.Controllers
{
    public class MoveController : Controller
    {
        /// <summary>
        /// 新增移库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            MoveOrderEntity entity = WebUtil.GetFormObject<MoveOrderEntity>("Entity");
            List<MoveOrderDetailEntity> list = WebUtil.GetFormObject<List<MoveOrderDetailEntity>>("List");

            entity.SnNum = ConvertHelper.NewGuid();
            entity.ProductType = (int)EProductType.Goods;
            entity.Status = (int)EAudite.Wait;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = entity.CompanyID.IsEmpty() ? CompanyID : entity.CompanyID;
            entity.EquipmentNum = "";
            entity.EquipmentCode = "";

            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
            string returnValue = bill.Create(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "移库单创建成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "移库单创建失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑移库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            MoveOrderEntity entity = WebUtil.GetFormObject<MoveOrderEntity>("Entity");
            List<MoveOrderDetailEntity> list = WebUtil.GetFormObject<List<MoveOrderDetailEntity>>("List");

            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
            string returnValue = bill.EditOrder(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "移库单编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "移库单编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑移库单主体信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditOrder()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            MoveOrderEntity entity = WebUtil.GetFormObject<MoveOrderEntity>("Entity");
            List<MoveOrderDetailEntity> list = WebUtil.GetFormObject<List<MoveOrderDetailEntity>>("List");

            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
            string returnValue = bill.EditOrder(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "移库单主体编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "移库单主体编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑移库单详细
        /// </summary>
        /// <returns></returns>
        public ActionResult EditDetail()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            MoveOrderDetailEntity entity = WebUtil.GetFormObject<MoveOrderDetailEntity>("Entity");
            List<MoveOrderDetailEntity> list = WebUtil.GetFormObject<List<MoveOrderDetailEntity>>("List");

            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
            string returnValue = bill.EditDetail(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "移库单详细编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "移库单详细编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询移库单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrder()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            MoveOrderEntity entity = new MoveOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
            MoveOrderEntity result = bill.GetOrder(entity);

            DataResult<MoveOrderEntity> dataResult = new DataResult<MoveOrderEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询移库单详细
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            MoveOrderDetailEntity entity = new MoveOrderDetailEntity();
            entity.OrderSnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
            List<MoveOrderDetailEntity> list = bill.GetOrderDetail(entity);

            DataResult<List<MoveOrderDetailEntity>> dataResult = new DataResult<List<MoveOrderDetailEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询移库单分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int MoveType = WebUtil.GetFormValue<int>("MoveType", 0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);

            MoveOrderEntity entity = new MoveOrderEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.MoveType = MoveType;
            entity.ContractOrder = ContractOrder;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.StorageNum = StorageNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            
            List<MoveOrderEntity> listResult = bill.GetList(entity, ref pageInfo);
            DataListResult<MoveOrderEntity> dataResult = new DataListResult<MoveOrderEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除移库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
            string returnValue = bill.Delete(list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "移库单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "移库单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据移库单唯一编号删除移库单
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSingle()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string SnNum = WebUtil.GetFormValue<string>("SnNum", string.Empty);

            MoveOrderEntity entity = new MoveOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;

            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
            string returnValue = bill.Delete(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "移库单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "移库单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 取消移库单
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            MoveOrderEntity entity = new MoveOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
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
        /// 审核移库单
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

            MoveOrderEntity entity = new MoveOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.Status = Status;
            entity.AuditUser = AuditUser;
            entity.Reason = Reason;
            entity.OperateType = OperateType;
            entity.EquipmentNum = EquipmentNum;
            entity.EquipmentCode = EquipmentCode;
            entity.Remark = Remark;

            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
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
                result.Message = "移库单不存在";
            }
            else if ("1002" == returnValue)
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "移库单已经审核";
            }
            return Content(JsonHelper.SerializeObject(result));
        }


        /// <summary>
        /// 查询移库单详细分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetailPage()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int MoveType = WebUtil.GetFormValue<int>("MoveType", 0);
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
            MoveOrderDetailEntity entity = new MoveOrderDetailEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.MoveType = MoveType;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.StorageNum = StorageNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };

            List<MoveOrderDetailEntity> listResult = bill.GetDetailList(entity, ref pageInfo);

            DataListResult<MoveOrderDetailEntity> dataResult = new DataListResult<MoveOrderDetailEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据条件统计移库单的数据行数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCount()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int MoveType = WebUtil.GetFormValue<int>("MoveType", 0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);

            MoveOrderEntity entity = new MoveOrderEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.MoveType = MoveType;
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
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string PrintUser = WebUtil.GetFormValue<string>("PrintUser");
            DateTime PrintTime = WebUtil.GetFormValue<DateTime>("PrintTime",DateTime.Now);

            MoveOrderEntity entity = new MoveOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.PrintUser = PrintUser;
            entity.PrintTime = PrintTime;

            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);
            string returnValue = bill.Print(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "打印设置成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "打印设置失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询移库单打印数据源
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPrint()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            Bill<MoveOrderEntity, MoveOrderDetailEntity> bill = new MoveOrder(CompanyID);

            DataSet ds = bill.GetPrint(SnNum);

            DataResult<DataSet> dataResult = new DataResult<DataSet>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = ds;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据移库单号查询移库单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderByNum()
        {
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            MoveOrderExt bill = new MoveOrderExt(CompanyID);

            MoveOrderEntity result = bill.GetOrderByNum(OrderNum);

            DataResult<MoveOrderEntity> dataResult = new DataResult<MoveOrderEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
