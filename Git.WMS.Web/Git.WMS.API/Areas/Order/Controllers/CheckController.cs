using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Check;
using Git.Storage.Provider;
using Git.Storage.Provider.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Order.Controllers
{
    public class CheckController : Controller
    {
        /// <summary>
        /// 新增盘点单
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InventoryOrderEntity entity = WebUtil.GetFormObject<InventoryOrderEntity>("Entity");
            List<InventoryDetailEntity> list = WebUtil.GetFormObject<List<InventoryDetailEntity>>("List");

            entity.SnNum = ConvertHelper.NewGuid();
            entity.ProductType = (int)EProductType.Goods;
            entity.Status = (int)EAudite.Wait;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = entity.CompanyID.IsEmpty() ? CompanyID : entity.CompanyID;
            entity.EquipmentNum = "";
            entity.EquipmentCode = "";

            Bill<InventoryOrderEntity, InventoryDetailEntity> bill = new CheckOrder(CompanyID);
            string returnValue = bill.Create(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "盘点单创建成功";
            }
            else if (returnValue == "1001")
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "该仓库存在一个正在作业的盘点任务";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "盘点单创建失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑盘点单
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InventoryOrderEntity entity = WebUtil.GetFormObject<InventoryOrderEntity>("Entity");
            List<InventoryDetailEntity> list = WebUtil.GetFormObject<List<InventoryDetailEntity>>("List");

            Bill<InventoryOrderEntity, InventoryDetailEntity> bill = new CheckOrder(CompanyID);
            string returnValue = bill.EditOrder(entity, list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "盘点单编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "盘点单编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据唯一编号查询盘点单主体信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrder()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InventoryOrderEntity entity = new InventoryOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<InventoryOrderEntity, InventoryDetailEntity> bill = new CheckOrder(CompanyID);
            InventoryOrderEntity result = bill.GetOrder(entity);

            DataResult<InventoryOrderEntity> dataResult = new DataResult<InventoryOrderEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据盘点单唯一编号查询盘点单详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InventoryDetailEntity entity = new InventoryDetailEntity();
            entity.OrderSnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<InventoryOrderEntity, InventoryDetailEntity> bill = new CheckOrder(CompanyID);
            List<InventoryDetailEntity> list = bill.GetOrderDetail(entity);

            DataResult<List<InventoryDetailEntity>> dataResult = new DataResult<List<InventoryDetailEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询盘点单分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");

            InventoryOrderEntity entity = new InventoryOrderEntity();
            entity.CompanyID = CompanyID;
            entity.OrderNum = OrderNum;
            entity.StorageNum = StorageNum;
            entity.ContractOrder = ContractOrder;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;

            Bill<InventoryOrderEntity, InventoryDetailEntity> bill = new CheckOrder(CompanyID);
            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            
            List<InventoryOrderEntity> listResult = bill.GetList(entity, ref pageInfo);

            DataListResult<InventoryOrderEntity> dataResult = new DataListResult<InventoryOrderEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询盘点差异单所有数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDif()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string OrderSnNum = WebUtil.GetFormValue<string>("OrderSnNum");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum");
            string LocalName = WebUtil.GetFormValue<string>("LocalName");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string ProductNum = WebUtil.GetFormValue<string>("ProductNum");
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum");

            CheckOrderExt provider = new CheckOrderExt(CompanyID);
            InventoryDifEntity entity = new InventoryDifEntity() 
            { 
                OrderSnNum=OrderSnNum,
                CompanyID=CompanyID,
                OrderNum=OrderNum,
                LocalNum=LocalNum,
                LocalName=LocalName,
                StorageNum=StorageNum,
                ProductNum=ProductNum,
                BarCode=BarCode,
                ProductName=ProductName,
                BatchNum=BatchNum
            };
            List<InventoryDifEntity> listResult = provider.GetList(entity);
            DataListResult<InventoryDifEntity> dataResult = new DataListResult<InventoryDifEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDifPage()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string OrderSnNum = WebUtil.GetFormValue<string>("OrderSnNum");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum");
            string LocalName = WebUtil.GetFormValue<string>("LocalName");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string ProductNum = WebUtil.GetFormValue<string>("ProductNum");
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum");

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 5);

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            CheckOrderExt provider = new CheckOrderExt(CompanyID);
            InventoryDifEntity entity = new InventoryDifEntity()
            {
                OrderSnNum = OrderSnNum,
                CompanyID = CompanyID,
                OrderNum = OrderNum,
                LocalNum = LocalNum,
                LocalName = LocalName,
                StorageNum = StorageNum,
                ProductNum = ProductNum,
                BarCode = BarCode,
                ProductName = ProductName,
                BatchNum = BatchNum
            };
            List<InventoryDifEntity> listResult = provider.GetList(entity,ref pageInfo);
            DataListResult<InventoryDifEntity> dataResult = new DataListResult<InventoryDifEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo=pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 保存盘点差异单
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveDif()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            InventoryDifEntity entity = WebUtil.GetFormObject<InventoryDifEntity>("Entity");

            CheckOrderExt provider = new CheckOrderExt(CompanyID);

            int line = provider.SaveDif(entity);
            DataResult dataResult = new DataResult()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 新增盘差数据
        /// </summary>
        /// <returns></returns>
        public ActionResult AddDif()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            InventoryDifEntity entity = WebUtil.GetFormObject<InventoryDifEntity>("Entity");
            CheckOrderExt provider = new CheckOrderExt(CompanyID);

            InventoryDetailEntity check = new InventoryDetailEntity();
            check.OrderSnNum = entity.OrderSnNum;
            check.TargetNum = entity.ProductNum;
            check.CompanyID = CompanyID;
            DataResult dataResult = null;
            int count = provider.GetCount(check);
            if (count == 0)
            {
                dataResult=new DataResult()
                {
                    Code = (int)EResponseCode.Exception,
                    Message = "该产品不在盘点任务内"
                };

                return Content(JsonHelper.SerializeObject(dataResult));
            }
            int line = provider.AddDif(entity);
            dataResult = new DataResult()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除盘点差异单
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteDif()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string SnNum = WebUtil.GetFormValue<string>("SnNum", string.Empty);

            CheckOrderExt provider = new CheckOrderExt(CompanyID);
            int line = provider.DeleteDif(SnNum);

            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "删除成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "删除失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 完成盘点作业
        /// </summary>
        /// <returns></returns>
        public ActionResult Complete()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            InventoryOrderEntity entity = new InventoryOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            CheckOrderExt provider = new CheckOrderExt(CompanyID);
            int line = provider.Complete(entity);
            DataResult dataResult = new DataResult()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 批量删除盘点单
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            Bill<InventoryOrderEntity, InventoryDetailEntity> bill = new CheckOrder(CompanyID);
            string returnValue = bill.Delete(list);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "盘点单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "盘点单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据盘点单唯一编号删除盘点单
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSingle()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string SnNum = WebUtil.GetFormValue<string>("SnNum", string.Empty);
            InventoryOrderEntity entity = new InventoryOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;

            Bill<InventoryOrderEntity, InventoryDetailEntity> bill = new CheckOrder(CompanyID);
            string returnValue = bill.Delete(entity);
            DataResult result = new DataResult();
            if (EnumHelper.GetEnumDesc<EReturnStatus>(EReturnStatus.Success) == returnValue)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "盘点单删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "盘点单删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 取消盘点单
        /// </summary>
        /// <returns></returns>
        public ActionResult Cancel()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            InventoryOrderEntity entity = new InventoryOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            Bill<InventoryOrderEntity, InventoryDetailEntity> bill = new CheckOrder(CompanyID);
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
        /// 审核盘点单
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

            InventoryOrderEntity entity = new InventoryOrderEntity();
            entity.SnNum = SnNum;
            entity.CompanyID = CompanyID;
            entity.Status = Status;
            entity.AuditUser = AuditUser;
            entity.Reason = Reason;
            entity.OperateType = OperateType;
            entity.EquipmentNum = EquipmentNum;
            entity.EquipmentCode = EquipmentCode;
            entity.Remark = Remark;

            Bill<InventoryOrderEntity, InventoryDetailEntity> bill = new CheckOrder(CompanyID);
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
                result.Message = "盘点单不存在";
            }
            else if ("1002" == returnValue)
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "盘点单已经审核";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据盘点单号查询盘点单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderByNum()
        {
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);

            CheckOrderExt bill = new CheckOrderExt(CompanyID);
            InventoryOrderEntity result = bill.GetOrderByNum(OrderNum);

            DataResult<InventoryOrderEntity> dataResult = new DataResult<InventoryOrderEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
