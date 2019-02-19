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
using Git.Storage.Entity.Report;

namespace Git.WMS.Web.Areas.Report.Controllers
{
    public class BalanceBookAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询期初期初期末分页
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string Day = WebUtil.GetFormValue<string>("Day");

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
            dic.Add("Day", Day);

            string result = client.Execute(BalanceBookApiName.BalanceBookApiName_GetList, dic);

            return Content(result);
        }

        /// <summary>
        /// 导出库存期初期末Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToExcel()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string Day = WebUtil.GetFormValue<string>("Day");

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
            dic.Add("Day", Day);

            string result = client.Execute(BalanceBookApiName.BalanceBookApiName_GetList, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<BalanceBookEntity> dataResult = JsonHelper.DeserializeObject<DataListResult<BalanceBookEntity>>(result);
                List<BalanceBookEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("日期"));
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("产品规格"));
                    dt.Columns.Add(new DataColumn("产品批次"));
                    dt.Columns.Add(new DataColumn("单位"));
                    dt.Columns.Add(new DataColumn("仓库"));
                    dt.Columns.Add(new DataColumn("期初"));
                    dt.Columns.Add(new DataColumn("入库"));
                    dt.Columns.Add(new DataColumn("出库"));
                    dt.Columns.Add(new DataColumn("期末"));
                    foreach (BalanceBookEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.Day;
                        row[1] = t.ProductName;
                        row[2] = t.BarCode;
                        row[3] = t.Size;
                        row[4] = t.BatchNum;
                        row[5] = t.UnitName;
                        row[6] = t.StorageName;
                        row[7] = t.BeginNum;
                        row[8] = t.InNum;
                        row[9] = t.OutNum;
                        row[10] = t.EndNum;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("期初期末{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("期初期末", "期初期末", System.IO.Path.Combine(filePath, filename));
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
