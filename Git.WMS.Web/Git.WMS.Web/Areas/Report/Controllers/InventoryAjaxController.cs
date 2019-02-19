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
using Git.Storage.Entity.Storage;
using Git.Storage.Common.Enum;

namespace Git.WMS.Web.Areas.Report.Controllers
{
    public class InventoryAjaxController : AjaxPage
    {
        /// <summary>
        /// 台账记录查询分页列表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);
            int Type = WebUtil.GetFormValue<int>("Type", 0);
            string FromStorageNum = WebUtil.GetFormValue<string>("FromStorageNum");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string ContactOrder = WebUtil.GetFormValue<string>("ContactOrder");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            
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
            dic.Add("Type", Type.ToString());
            dic.Add("FromStorageNum", FromStorageNum);
            dic.Add("OrderNum", OrderNum);
            dic.Add("ContactOrder", ContactOrder);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);

            string result = client.Execute(InventoryApiName.InventoryApiName_GetList, dic);

            return Content(result);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToExcel()
        {
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);
            int Type = WebUtil.GetFormValue<int>("Type", 0);
            string FromStorageNum = WebUtil.GetFormValue<string>("FromStorageNum");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string ContactOrder = WebUtil.GetFormValue<string>("ContactOrder");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");

            int PageIndex = 1;
            int PageSize = Int32.MaxValue;

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("BatchNum", BatchNum);
            dic.Add("Type", Type.ToString());
            dic.Add("FromStorageNum", FromStorageNum);
            dic.Add("OrderNum", OrderNum);
            dic.Add("ContactOrder", ContactOrder);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);

            string result = client.Execute(InventoryApiName.InventoryApiName_GetList, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<InventoryBookEntity> dataResult = JsonHelper.DeserializeObject<DataListResult<InventoryBookEntity>>(result);
                List<InventoryBookEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("产品规格"));
                    dt.Columns.Add(new DataColumn("产品批次"));
                    dt.Columns.Add(new DataColumn("类型"));
                    dt.Columns.Add(new DataColumn("数量"));
                    dt.Columns.Add(new DataColumn("单位"));
                    dt.Columns.Add(new DataColumn("原仓库"));
                    dt.Columns.Add(new DataColumn("原库位"));
                    dt.Columns.Add(new DataColumn("目标仓库"));
                    dt.Columns.Add(new DataColumn("目标库位"));
                    dt.Columns.Add(new DataColumn("日期"));
                    foreach (InventoryBookEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.ProductName;
                        row[1] = t.BarCode;
                        row[2] = t.Size;
                        row[3] = t.BatchNum;
                        row[4] = EnumHelper.GetEnumDesc<EChange>(t.Type);
                        row[5] = t.Num;
                        row[6] = t.UnitName;
                        row[7] = t.FromStorageName;
                        row[8] = t.FromLocalName;
                        row[9] = t.ToStorageName;
                        row[10] = t.ToLocalName;
                        row[11] = t.CreateTime.To("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("库存台账{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("库存台账", "库存台账", System.IO.Path.Combine(filePath, filename));
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
