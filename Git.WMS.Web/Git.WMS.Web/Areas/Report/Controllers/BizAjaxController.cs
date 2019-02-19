using Git.Framework.Controller;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.DataTypes;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Git.Storage.Common;
using Git.Storage.Entity.Biz;
using System.Data;
using Git.Storage.Common.Excel;

namespace Git.WMS.Web.Areas.Report.Controllers
{
    public class BizAjaxController : AjaxPage
    {
        /// <summary>
        /// 销售订单报表查询
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult SaleReportList()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string CusNum = WebUtil.GetFormValue<string>("CusNum");
            string CusName = WebUtil.GetFormValue<string>("CusName");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("CusNum", CusNum);
            dic.Add("CusName", CusName);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());

            ITopClient client = new TopClientDefault();

            string result = client.Execute(SaleApiName.SaleApiName_GetDetailList, dic);
            return Content(result);
        }

        /// <summary>
        /// 售订单报表-导出Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToSaleReportListExcel()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string CusNum = WebUtil.GetFormValue<string>("CusNum");
            string CusName = WebUtil.GetFormValue<string>("CusName");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            int PageIndex = 1;
            int PageSize = Int32.MaxValue;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("CusNum", CusNum);
            dic.Add("CusName", CusName);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());

            ITopClient client = new TopClientDefault();

            string result = client.Execute(SaleApiName.SaleApiName_GetDetailList, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<SaleDetailEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<SaleDetailEntity>>(result);
                List<SaleDetailEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("产品规格"));
                    dt.Columns.Add(new DataColumn("销售单号"));
                    dt.Columns.Add(new DataColumn("数量"));
                    dt.Columns.Add(new DataColumn("单价"));
                    dt.Columns.Add(new DataColumn("总价"));
                    dt.Columns.Add(new DataColumn("客户"));
                    dt.Columns.Add(new DataColumn("销售时间"));
                    foreach (SaleDetailEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.ProductName;
                        row[1] = t.BarCode;
                        row[2] = t.Size;
                        row[3] = t.OrderNum;
                        row[4] = t.Num.ToString("0.00");
                        row[5] = t.Price.ToString("0.00");
                        row[6] = t.Amount.ToString("0.00");
                        row[7] = t.CusName;
                        row[8] = t.CreateTime.To("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("销售详细报表{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("销售详细报表", "销售详细报表", System.IO.Path.Combine(filePath, filename));
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



        /// <summary>
        /// 采购单报表查询
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult PurchaseReportList()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string SupName = WebUtil.GetFormValue<string>("SupName");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("SupNum", SupNum);
            dic.Add("SupName", SupName);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());

            ITopClient client = new TopClientDefault();

            string result = client.Execute(PurchaseApiName.PurchaseApiName_GetDetailList, dic);
            return Content(result);
        }

        /// <summary>
        /// 采购单报表-导出Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToPurchaseReportListExcel()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string SupName = WebUtil.GetFormValue<string>("SupName");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            int PageIndex = 1;
            int PageSize = Int32.MaxValue;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("SupNum", SupNum);
            dic.Add("SupName", SupName);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());

            ITopClient client = new TopClientDefault();

            string result = client.Execute(PurchaseApiName.PurchaseApiName_GetDetailList, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<PurchaseDetailEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<PurchaseDetailEntity>>(result);
                List<PurchaseDetailEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("产品规格"));
                    dt.Columns.Add(new DataColumn("采购单号"));
                    dt.Columns.Add(new DataColumn("数量"));
                    dt.Columns.Add(new DataColumn("单价"));
                    dt.Columns.Add(new DataColumn("总价"));
                    dt.Columns.Add(new DataColumn("供应商"));
                    dt.Columns.Add(new DataColumn("采购时间"));
                    foreach (PurchaseDetailEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.ProductName;
                        row[1] = t.BarCode;
                        row[2] = t.Size;
                        row[3] = t.OrderNum;
                        row[4] = t.Num.ToString("0.00");
                        row[5] = t.Price.ToString("0.00");
                        row[6] = t.Amount.ToString("0.00");
                        row[7] = t.SupName;
                        row[8] = t.CreateTime.To("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("采购详细报表{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("采购详细报表", "采购详细报表", System.IO.Path.Combine(filePath, filename));
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
