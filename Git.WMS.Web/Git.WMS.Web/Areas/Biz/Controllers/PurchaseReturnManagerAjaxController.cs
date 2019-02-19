using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Common.Excel;
using Git.Storage.Entity.Biz;
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

namespace Git.WMS.Web.Areas.Biz.Controllers
{
    public class PurchaseReturnManagerAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询采购退货单详细分页
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            string CompanyID = this.CompanyID;

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string PurchaseOrderNum = WebUtil.GetFormValue<string>("PurchaseOrderNum", string.Empty);
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);

            string CusNum = WebUtil.GetFormValue<string>("CusNum", string.Empty);
            string CusName = WebUtil.GetFormValue<string>("CusName", string.Empty);

            string BeginTime = WebUtil.GetFormValue<string>("BeginTime", string.Empty);
            string EndTime = WebUtil.GetFormValue<string>("EndTime", string.Empty);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);

            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());

            dic.Add("OrderNum", OrderNum);
            dic.Add("PurchaseOrderNum", PurchaseOrderNum);
            dic.Add("CusName", CusName);
            dic.Add("CusNum", CusNum);

            dic.Add("Status", Status.ToString());

            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);

            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);

            string result = client.Execute(PurchaseReturnApiName.PurchaseReturnApiName_GetDetailList, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除采购退货单
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

            string result = client.Execute(PurchaseReturnApiName.PurchaseReturnApiName_Delete, dic);
            return Content(result);
        }

        /// <summary>
        /// 取消采购退货单
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

            string result = client.Execute(PurchaseReturnApiName.PurchaseReturnApiName_Cancel, dic);
            return Content(result);
        }

        /// <summary>
        /// 审核采购退货单
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

            string result = client.Execute(PurchaseReturnApiName.PurchaseReturnApiName_Audite, dic);
            return Content(result);
        }

        /// <summary>
        /// 获得采购退货单详细
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);

            string result = client.Execute(PurchaseReturnApiName.PurchaseReturnApiName_GetDetail, dic);

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
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string PurchaseOrderNum = WebUtil.GetFormValue<string>("PurchaseOrderNum", string.Empty);
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);

            string CusNum = WebUtil.GetFormValue<string>("CusNum", string.Empty);
            string CusName = WebUtil.GetFormValue<string>("CusName", string.Empty);

            string BeginTime = WebUtil.GetFormValue<string>("BeginTime", string.Empty);
            string EndTime = WebUtil.GetFormValue<string>("EndTime", string.Empty);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);

            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());

            dic.Add("OrderNum", OrderNum);
            dic.Add("PurchaseOrderNum", PurchaseOrderNum);
            dic.Add("CusName", CusName);
            dic.Add("CusNum", CusNum);

            dic.Add("Status", Status.ToString());

            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);

            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);

            string result = client.Execute(PurchaseReturnApiName.PurchaseReturnApiName_GetDetailList, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<PurchaseReturnDetailEntity> dataResult = JsonHelper.DeserializeObject<DataListResult<PurchaseReturnDetailEntity>>(result);
                List<PurchaseReturnDetailEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("退货单号"));
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("产品规格"));
                    dt.Columns.Add(new DataColumn("单价"));
                    dt.Columns.Add(new DataColumn("退货数"));
                    dt.Columns.Add(new DataColumn("单位"));
                    dt.Columns.Add(new DataColumn("总额"));
                    dt.Columns.Add(new DataColumn("采购单号"));
                    dt.Columns.Add(new DataColumn("供应商名称"));
                    dt.Columns.Add(new DataColumn("状态"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    foreach (PurchaseReturnDetailEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.OrderNum;
                        row[1] = t.ProductName;
                        row[2] = t.BarCode;
                        row[3] = t.Size;
                        row[4] = t.Price;
                        row[5] = t.ReturnNum;
                        row[6] = t.UnitName;
                        row[7] = t.Amount;
                        row[8] = t.PurchaseOrderNum;
                        row[9] = t.SupName;
                        row[10] = EnumHelper.GetEnumDesc<EPurchaseReturnStatus>(t.Status);
                        row[11] = t.CreateTime.ToString("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("采购退货单{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("采购退货单", "采购退货单", System.IO.Path.Combine(filePath, filename));
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
