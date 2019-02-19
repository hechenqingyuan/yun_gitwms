using Git.Framework.Controller;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Biz;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Common.Excel;

namespace Git.WMS.Web.Areas.Biz.Controllers
{
    public class PurchaseManagerAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            string CompanyID = this.CompanyID;

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int OrderType = WebUtil.GetFormValue<int>("OrderType", 0);
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string SupNum = WebUtil.GetFormValue<string>("SupNum", string.Empty);
            string SupName = WebUtil.GetFormValue<string>("SupName", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            int AuditeStatus = WebUtil.GetFormValue<int>("AuditeStatus", -1);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder", string.Empty);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime", string.Empty);
            string EndTime = WebUtil.GetFormValue<string>("EndTime", string.Empty);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("OrderNum", OrderNum);
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("OrderType", OrderType.ToString());
            dic.Add("SupNum", SupNum);
            dic.Add("SupName", SupName);
            dic.Add("Status", Status.ToString());
            dic.Add("AuditeStatus", AuditeStatus.ToString());
            dic.Add("ContractOrder", ContractOrder);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);

            string result = client.Execute(PurchaseApiName.PurchaseApiName_GetDetailList, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除采购订单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Delete()
        {
            string CompanyID = this.CompanyID;
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("list", JsonConvert.SerializeObject(list));

            string result = client.Execute(PurchaseApiName.PurchaseApiName_Delete, dic);
            return Content(result);
        }

        /// <summary>
        /// 取消采购订单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Cancel()
        {
            string CompanyID = this.CompanyID;
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);

            string result = client.Execute(PurchaseApiName.PurchaseApiName_Cancel, dic);
            return Content(result);
        }

        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Audite()
        {
            string CompanyID = this.CompanyID;
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            int Status = WebUtil.GetFormValue<int>("Status");
            string Reason = WebUtil.GetFormValue<string>("Reason");
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);
            dic.Add("Status", Status.ToString());
            dic.Add("Reason", Reason);
            dic.Add("AuditUser", this.LoginUser.UserNum);
            dic.Add("OperateType", ((int)EOpType.PC).ToString());
            dic.Add("EquipmentNum", "");
            dic.Add("EquipmentCode", "");

            string result = client.Execute(PurchaseApiName.PurchaseApiName_Audite, dic);
            return Content(result);
        }

        /// <summary>
        /// 获得采购订单详细
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);

            string result = client.Execute(PurchaseApiName.PurchaseApiName_GetDetail, dic);

            return Content(result);
        }

        /// <summary>
        /// 生成财务账目记录
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult ToFiance()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);

            string result = client.Execute(PurchaseApiName.PurchaseApiName_ToFiance, dic);

            return Content(result);
        }

        /// <summary>
        /// 采购入库
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult ToStorage()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            List<PurchaseDetailEntity> list = WebUtil.GetFormObject<List<PurchaseDetailEntity>>("List");
            if (list.IsNullOrEmpty())
            {
                DataResult dataResult = new DataResult() 
                { 
                    Code=(int)EResponseCode.Exception,
                    Message="请选择要入库的产品"
                };
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            string StorageNum = list[0].StorageNum;

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);
            dic.Add("StorageNum", StorageNum);
            dic.Add("List", JsonConvert.SerializeObject(list));

            string result = client.Execute(PurchaseApiName.PurchaseApiName_ToInStorage, dic);

            return Content(result);
        }

        /// <summary>
        /// 采购退货
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult ToReturn()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            List<PurchaseDetailEntity> list = WebUtil.GetFormObject<List<PurchaseDetailEntity>>("List");
            if (list.IsNullOrEmpty())
            {
                DataResult dataResult = new DataResult()
                {
                    Code = (int)EResponseCode.Exception,
                    Message = "请选择要退货的产品"
                };
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            string StorageNum = this.DefaultStorageNum;

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);
            dic.Add("StorageNum", StorageNum);
            dic.Add("List", JsonConvert.SerializeObject(list));

            string result = client.Execute(PurchaseApiName.PurchaseApiName_ToReturn, dic);

            return Content(result);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToExcel()
        {
            string CompanyID = this.CompanyID;

            int PageIndex = 1;
            int PageSize = Int32.MaxValue;
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int OrderType = WebUtil.GetFormValue<int>("OrderType", 0);
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string SupNum = WebUtil.GetFormValue<string>("SupNum", string.Empty);
            string SupName = WebUtil.GetFormValue<string>("SupName", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            int AuditeStatus = WebUtil.GetFormValue<int>("AuditeStatus", -1);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder", string.Empty);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime", string.Empty);
            string EndTime = WebUtil.GetFormValue<string>("EndTime", string.Empty);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("OrderNum", OrderNum);
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("OrderType", OrderType.ToString());
            dic.Add("SupNum", SupNum);
            dic.Add("SupName", SupName);
            dic.Add("Status", Status.ToString());
            dic.Add("AuditeStatus", AuditeStatus.ToString());
            dic.Add("ContractOrder", ContractOrder);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);

            string result = client.Execute(PurchaseApiName.PurchaseApiName_GetDetailList, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<PurchaseDetailEntity> dataResult = JsonHelper.DeserializeObject<DataListResult<PurchaseDetailEntity>>(result);
                List<PurchaseDetailEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("订单号"));
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("产品规格"));
                    dt.Columns.Add(new DataColumn("单价"));
                    dt.Columns.Add(new DataColumn("单位"));
                    dt.Columns.Add(new DataColumn("数量"));
                    dt.Columns.Add(new DataColumn("总额"));
                    dt.Columns.Add(new DataColumn("供应商编号"));
                    dt.Columns.Add(new DataColumn("供应商名称"));
                    dt.Columns.Add(new DataColumn("订单总额"));
                    dt.Columns.Add(new DataColumn("状态"));
                    dt.Columns.Add(new DataColumn("是否入账"));
                    dt.Columns.Add(new DataColumn("退货"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    foreach (PurchaseDetailEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.OrderNum;
                        row[1] = t.ProductName;
                        row[2] = t.BarCode;
                        row[3] = t.Size;
                        row[4] = t.Price;
                        row[5] = t.UnitName;
                        row[6] = t.Num;
                        row[7] = t.Amount;
                        row[8] = t.SupNum;
                        row[9] = t.SupName;
                        row[10] = t.OrderAmount;
                        row[11] = EnumHelper.GetEnumDesc<EPurchaseStatus>(t.OrderStatus);
                        row[12] = EnumHelper.GetEnumDesc<EBool>(t.AuditeStatus);
                        row[13] = EnumHelper.GetEnumDesc<EBool>(t.HasReturn);
                        row[14] = t.CreateTime.ToString("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("采购订单{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("采购订单", "采购订单", System.IO.Path.Combine(filePath, filename));
                    excel.ToExcel(dt);
                    returnValue = ("/UploadFile/" + filename).Escape();
                }
            }
            DataResult returnResult = null;
            if (!returnValue.IsEmpty())
            {
                returnResult = new DataResult() { Code = 1000, Message = returnValue };
            }
            else
            {
                returnResult = new DataResult() { Code = 1001, Message = "没有任何数据导出" };
            }
            return Content(JsonHelper.SerializeObject(returnResult));
        }
    }
}
