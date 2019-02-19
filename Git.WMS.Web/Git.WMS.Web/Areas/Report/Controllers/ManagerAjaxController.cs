using Git.Framework.Controller;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Newtonsoft.Json;
using Git.Storage.Common;
using Git.Storage.Entity.Report;
using System.Data;
using Git.Storage.Common.Excel;
using Git.Storage.Common.Enum;

namespace Git.WMS.Web.Areas.Report.Controllers
{
    public class ManagerAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询自定义报表分页列表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            string CompanyID = this.CompanyID;
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string ReportNum = WebUtil.GetFormValue<string>("ReportNum");
            string ReportName = WebUtil.GetFormValue<string>("ReportName");
            int ReportType = WebUtil.GetFormValue<int>("ReportType",0);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("ReportNum", ReportNum);
            dic.Add("ReportName", ReportName);
            dic.Add("ReportType", ReportType.ToString());

            string result = client.Execute(ReportApiName.ReportApiName_GetList, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除自定义报表
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

            string result = client.Execute(ReportApiName.ReportApiName_Delete, dic);
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
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = Int32.MaxValue;
            string ReportNum = WebUtil.GetFormValue<string>("ReportNum");
            string ReportName = WebUtil.GetFormValue<string>("ReportName");
            int ReportType = WebUtil.GetFormValue<int>("ReportType", 0);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("ReportNum", ReportNum);
            dic.Add("ReportName", ReportName);
            dic.Add("ReportType", ReportType.ToString());

            string result = client.Execute(ReportApiName.ReportApiName_GetList, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<ReportsEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<ReportsEntity>>(result);
                List<ReportsEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("编号"));
                    dt.Columns.Add(new DataColumn("报表名称"));
                    dt.Columns.Add(new DataColumn("报表类型"));
                    dt.Columns.Add(new DataColumn("是否禁用"));
                    dt.Columns.Add(new DataColumn("备注"));
                    dt.Columns.Add(new DataColumn("数据源类型"));
                    dt.Columns.Add(new DataColumn("数据源"));
                    dt.Columns.Add(new DataColumn("文件路径"));
                    foreach (ReportsEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.ReportNum;
                        row[1] = t.ReportName;
                        row[2] = EnumHelper.GetEnumDesc<EReportType>(t.ReportType);
                        row[3] = EnumHelper.GetEnumDesc<EBool>(t.Status);
                        row[4] = t.Remark;
                        row[5] = EnumHelper.GetEnumDesc<EDataSourceType>(t.DsType);
                        row[6] = t.DataSource;
                        row[7] = t.FileName;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("自定义报表{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("自定义报表", "报表", System.IO.Path.Combine(filePath, filename));
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
