using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Common;
using Newtonsoft.Json;
using Git.Framework.Controller;
using Git.Storage.Entity.Storage;
using System.Data;
using Git.Storage.Common.Excel;
using Git.Storage.Entity.InStorage;
using Git.Storage.Entity.OutStorage;
using Git.Storage.Common.Enum;

namespace Git.WMS.Web.Areas.Report.Controllers
{
    public class StoreAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询库存清单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetStoreBillist()
        {
            string CompanyID = this.CompanyID;
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("BatchNum", BatchNum);
            dic.Add("StorageNum", StorageNum);

            string result = client.Execute(LocalProductApiName.LocalProductApiName_GetList, dic);

            return Content(result);
        }

        /// <summary>
        /// 库存清单导出Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToStoreBillExcel()
        {
            string CompanyID = this.CompanyID;
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);

            int PageIndex =1;
            int PageSize = Int32.MaxValue;

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("BatchNum", BatchNum);
            dic.Add("StorageNum", StorageNum);

            string result = client.Execute(LocalProductApiName.LocalProductApiName_GetList, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<LocalProductEntity> dataResult = JsonHelper.DeserializeObject<DataListResult<LocalProductEntity>>(result);
                List<LocalProductEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("产品规格"));
                    dt.Columns.Add(new DataColumn("产品批次"));
                    dt.Columns.Add(new DataColumn("类别"));
                    dt.Columns.Add(new DataColumn("仓库"));
                    dt.Columns.Add(new DataColumn("库位"));
                    dt.Columns.Add(new DataColumn("库存"));
                    dt.Columns.Add(new DataColumn("单位"));
                    foreach (LocalProductEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.ProductName;
                        row[1] = t.BarCode;
                        row[2] = t.Size;
                        row[3] = t.BatchNum;
                        row[4] = t.CateName;
                        row[5] = t.StorageName;
                        row[6] = t.LocalName;
                        row[7] = t.Num;
                        row[8] = t.UnitName;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("库存清单{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("库存清单", "库存清单", System.IO.Path.Combine(filePath, filename));
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
        /// 查询产品可销售的库存-与仓库无关
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetProductStoreList()
        {
            string CompanyID = this.CompanyID;
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);

            string result = client.Execute(LocalProductApiName.LocalProductApiName_GetLocalProduct, dic);

            return Content(result);
        }

        /// <summary>
        /// 导出产品可销售的库存Excel-与仓库无关
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToProductStoreExcel()
        {
            string CompanyID = this.CompanyID;
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);

            int PageIndex = 1;
            int PageSize = Int32.MaxValue;

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);

            string result = client.Execute(LocalProductApiName.LocalProductApiName_GetLocalProduct, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<V_LocalProductEntity> dataResult = JsonHelper.DeserializeObject<DataListResult<V_LocalProductEntity>>(result);
                List<V_LocalProductEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("产品规格"));
                    dt.Columns.Add(new DataColumn("类别"));
                    dt.Columns.Add(new DataColumn("库存"));
                    dt.Columns.Add(new DataColumn("单位"));
                    foreach (V_LocalProductEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.ProductName;
                        row[1] = t.BarCode;
                        row[2] = t.Size;
                        row[3] = t.CateName;
                        row[4] = t.Num;
                        row[5] = t.UnitName;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("可出库存{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("可出库存", "可出库存", System.IO.Path.Combine(filePath, filename));
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
        /// 入库详细报表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult InReportList()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string StorageNum = this.DefaultStorageNum;
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
            dic.Add("StorageNum", StorageNum);
            dic.Add("SupNum", SupNum);
            dic.Add("SupName", SupName);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());

            ITopClient client = new TopClientDefault();

            string result = client.Execute(InStorageApiName.InStorageApiName_GetDetailList, dic);
            return Content(result);
        }

        /// <summary>
        /// 入库详细报表-导出Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToInReportListExcel()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string StorageNum = this.DefaultStorageNum;
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
            dic.Add("StorageNum", StorageNum);
            dic.Add("SupNum", SupNum);
            dic.Add("SupName", SupName);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());

            ITopClient client = new TopClientDefault();

            string result = client.Execute(InStorageApiName.InStorageApiName_GetDetailList, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<InStorDetailEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<InStorDetailEntity>>(result);
                List<InStorDetailEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("产品规格"));
                    dt.Columns.Add(new DataColumn("产品批次"));
                    dt.Columns.Add(new DataColumn("入库单号"));
                    dt.Columns.Add(new DataColumn("数量"));
                    dt.Columns.Add(new DataColumn("单价"));
                    dt.Columns.Add(new DataColumn("总价"));
                    dt.Columns.Add(new DataColumn("供应商"));
                    dt.Columns.Add(new DataColumn("入库时间"));
                    foreach (InStorDetailEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.ProductName;
                        row[1] = t.BarCode;
                        row[2] = t.Size;
                        row[3] = t.BatchNum;
                        row[4] = t.OrderNum;
                        row[5] = t.Num.ToString("0.00");
                        row[6] = t.InPrice.ToString("0.00");
                        row[7] = t.Amount.ToString("0.00");
                        row[8] = t.SupName;
                        row[9] = t.CreateTime.To("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("入库单详细报表{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("入库单详细报表", "入库单详细报表", System.IO.Path.Combine(filePath, filename));
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
        /// 出库详细报表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult OutReportList()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string StorageNum = this.DefaultStorageNum;
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
            dic.Add("StorageNum", StorageNum);
            dic.Add("CusNum", CusNum);
            dic.Add("CusName", CusName);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());

            ITopClient client = new TopClientDefault();

            string result = client.Execute(OutStorageApiName.OutStorageApiName_GetDetailList, dic);
            return Content(result);
        }

        /// <summary>
        /// 出库详细报表-导出Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToOutReportListExcel()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string StorageNum = this.DefaultStorageNum;
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
            dic.Add("StorageNum", StorageNum);
            dic.Add("CusNum", CusNum);
            dic.Add("CusName", CusName);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());

            ITopClient client = new TopClientDefault();

            string result = client.Execute(OutStorageApiName.OutStorageApiName_GetDetailList, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<OutStoDetailEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<OutStoDetailEntity>>(result);
                List<OutStoDetailEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("产品规格"));
                    dt.Columns.Add(new DataColumn("产品批次"));
                    dt.Columns.Add(new DataColumn("入库单号"));
                    dt.Columns.Add(new DataColumn("数量"));
                    dt.Columns.Add(new DataColumn("单价"));
                    dt.Columns.Add(new DataColumn("总价"));
                    dt.Columns.Add(new DataColumn("客户"));
                    dt.Columns.Add(new DataColumn("出库时间"));
                    foreach (OutStoDetailEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.ProductName;
                        row[1] = t.BarCode;
                        row[2] = t.Size;
                        row[3] = t.BatchNum;
                        row[4] = t.OrderNum;
                        row[5] = t.Num.ToString("0.00");
                        row[6] = t.OutPrice.ToString("0.00");
                        row[7] = t.Amount.ToString("0.00");
                        row[8] = t.CusName;
                        row[9] = t.CreateTime.To("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("出库单详细报表{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("出库单详细报表", "出库单详细报表", System.IO.Path.Combine(filePath, filename));
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
