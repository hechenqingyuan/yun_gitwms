using FastReport;
using FastReport.Data;
using FastReport.Web;
using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.Io;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Report;
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
using System.Web.UI.WebControls;

namespace Git.WMS.Web.Areas.Report.Controllers
{
    public class ManagerController : MasterPage
    {
        /// <summary>
        /// 自定义报表管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.ReportType = EnumHelper.GetOptions<EReportType>(string.Empty);
            return View();
        }

        /// <summary>
        /// 新增自定义报表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            Session[SessionKey.SESSION_REPORT_DETAIL] = null;

            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", this.CompanyID);
            dic.Add("SnNum", SnNum);
            string result = client.Execute(ReportApiName.ReportApiName_GetSingle,dic);
            DataResult<ReportsEntity> dataResult = JsonConvert.DeserializeObject<DataResult<ReportsEntity>>(result);
            ReportsEntity entity = dataResult.Result;
            entity = entity.IsNull() ? new ReportsEntity() : entity;
            ViewBag.Entity = entity;

            ViewBag.ReportType = EnumHelper.GetOptions<EReportType>(entity.ReportType);
            ViewBag.DataSourceType = EnumHelper.GetOptions<EDataSourceType>(entity.DsType);

            result = client.Execute(ReportApiName.ReportApiName_GetParameter, dic);
            DataResult<List<ReportParamsEntity>> paramResult = JsonConvert.DeserializeObject<DataResult<List<ReportParamsEntity>>>(result);
            List<ReportParamsEntity> listParams = paramResult.Result;
            Session[SessionKey.SESSION_REPORT_DETAIL] = listParams;

            return View();
        }

        /// <summary>
        /// 新增报表参数
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult AddParam()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum",string.Empty);
            List<ReportParamsEntity> listSource = Session[SessionKey.SESSION_REPORT_DETAIL] as List<ReportParamsEntity>;
            ReportParamsEntity entity = null;
            if (!listSource.IsNullOrEmpty())
            {
                entity = listSource.FirstOrDefault(a => a.SnNum == SnNum);
            }
            if (entity == null)
            {
                entity = new ReportParamsEntity();
            }
            ViewBag.ParamType = DropDownHelper.GetDataType(entity.ParamType);
            entity.ParamElement = entity.ParamElement.IsEmpty() ? ((int)EElementType.TextBox).ToString() : entity.ParamElement;
            ViewBag.ParamElement = EnumHelper.GetOptions<EElementType>(entity.ParamElement);
            ViewBag.Entity = entity;
            return View();
        }

        /// <summary>
        /// 报表对话框
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true, false)]
        public ActionResult Dialog()
        {
            return View();
        }

        /// <summary>
        /// 报表设计
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Designer()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", this.CompanyID);
            dic.Add("SnNum", SnNum);

            if (SnNum.IsEmpty())
            {
                return Redirect("/Report/Manager/List");
            }
            string result = client.Execute(ReportApiName.ReportApiName_GetSingle, dic);
            DataResult<ReportsEntity> dataResult = JsonConvert.DeserializeObject<DataResult<ReportsEntity>>(result);
            ReportsEntity entity = dataResult.Result;
            if (entity.IsNull())
            {
                return Redirect("/Report/Manager/List");
            }

            result = client.Execute(ReportApiName.ReportApiName_GetParameter, dic);
            DataResult<List<ReportParamsEntity>> paramResult = JsonConvert.DeserializeObject<DataResult<List<ReportParamsEntity>>>(result);
            List<ReportParamsEntity> list = paramResult.Result;

            WebReport webReport = new WebReport();
            webReport.Width = Unit.Percentage(100);
            webReport.Height = 600;
            webReport.ToolbarIconsStyle = ToolbarIconsStyle.Black;
            webReport.ToolbarIconsStyle = ToolbarIconsStyle.Black;
            webReport.PrintInBrowser = true;
            webReport.PrintInPdf = true;
            webReport.ShowExports = true;
            webReport.ShowPrint = true;
            webReport.SinglePage = true;
            DataSet ds = null;
            int orderType = entity.ReportType;

            if (!list.IsNullOrEmpty())
            {
                foreach (ReportParamsEntity item in list)
                {
                    if (item.ParamType == "datetime" || item.ParamType == "date")
                    {
                        item.DefaultValue = DateTime.Now.To("yyyy-MM-dd");
                    }
                    else if (item.ParamType == "int")
                    {
                        item.DefaultValue = "0";
                    }
                    else
                    {
                        item.DefaultValue = "0";
                    }
                }
            }

            dic = new Dictionary<string, string>();

            dic.Add("CompanyID", this.CompanyID);
            dic.Add("Entity", JsonConvert.SerializeObject(entity));
            dic.Add("List", JsonConvert.SerializeObject(list));
            dic.Add("OrderType", orderType.ToString());
            dic.Add("OrderNum", "-1");
            result = client.Execute(ReportApiName.ReportApiName_GetDataSource,dic);
            DataResult<DataSet> dataSetResult = JsonConvert.DeserializeObject<DataResult<DataSet>>(result);
            ds = dataSetResult.Result;

            string path = Server.MapPath("~" + entity.FileName);
            if (!FileManager.FileExists(path))
            {
                string template = Server.MapPath("~/Theme/content/report/temp/Report.frx");
                System.IO.File.Copy(template, path, true);
            }
            webReport.Report.Load(path);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                webReport.Report.RegisterData(ds);
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    webReport.Report.GetDataSource(ds.Tables[i].TableName).Enabled = true;
                }
            }


            ////给DataBand(主表数据)绑定数据源 
            //DataBand masterBand = webReport.Report.FindObject("Data3") as DataBand;
            //masterBand.DataSource = webReport.Report.GetDataSource("Table"); //主表 

            ////给DataBand(明细数据)绑定数据源 
            //DataBand detailBand = webReport.Report.FindObject("Data2") as DataBand;
            //detailBand.DataSource = webReport.Report.GetDataSource("Table1"); //明细表 

            ////重要！！给明细表设置主外键关系！ 
            //detailBand.Relation = new Relation();
            //detailBand.Relation.ParentColumns = new string[] { "SnNum" };
            //detailBand.Relation.ParentDataSource = webReport.Report.GetDataSource("Table"); //主表 
            //detailBand.Relation.ChildColumns = new string[] { "OrderSnNum" };
            //detailBand.Relation.ChildDataSource = webReport.Report.GetDataSource("Table1"); //明细表 


            webReport.DesignerPath = "~/WebReportDesigner/index.html";
            webReport.DesignReport = true;
            webReport.DesignScriptCode = true;
            webReport.DesignerSavePath = "~/Theme/content/report/temp/";
            webReport.DesignerSaveCallBack = "~/Report/Manager/SaveDesignedReport";
            webReport.ID = SnNum;

            ViewBag.WebReport = webReport;
            return View();
        }

        /// <summary>
        /// 保存设计报表
        /// </summary>
        /// <param name="reportID"></param>
        /// <param name="reportUUID"></param>
        /// <returns></returns>
        public ActionResult SaveDesignedReport(string reportID, string reportUUID)
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", this.CompanyID);
            dic.Add("SnNum", reportID);

            if (reportID.IsEmpty())
            {
                return Redirect("/Report/Manager/List");
            }
            string result = client.Execute(ReportApiName.ReportApiName_GetSingle, dic);
            DataResult<ReportsEntity> dataResult = JsonConvert.DeserializeObject<DataResult<ReportsEntity>>(result);
            ReportsEntity entity = dataResult.Result;
            if (entity.IsNull())
            {
                return Redirect("/Report/Manager/List");
            }
            string FileRealPath = Server.MapPath("~" + entity.FileName);
            string FileTempPath = Server.MapPath("~/Theme/content/report/temp/" + reportUUID);
            FileManager.DeleteFile(FileRealPath);
            System.IO.File.Copy(FileTempPath, FileRealPath, true);
            return Content("");
        }

        /// <summary>
        /// 显示报表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Show()
        {
            //报表文件编号
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);
            //订单号
            string orderNum = WebUtil.GetQueryStringValue<string>("OrderNum", string.Empty);
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", this.CompanyID);
            dic.Add("SnNum", SnNum);

            if (SnNum.IsEmpty())
            {
                return Redirect("/Report/Manager/List");
            }
            string result = client.Execute(ReportApiName.ReportApiName_GetSingle, dic);
            DataResult<ReportsEntity> dataResult = JsonConvert.DeserializeObject<DataResult<ReportsEntity>>(result);
            ReportsEntity entity = dataResult.Result;
            if (entity.IsNull())
            {
                return Redirect("/Report/Manager/List");
            }

            result = client.Execute(ReportApiName.ReportApiName_GetParameter, dic);
            DataResult<List<ReportParamsEntity>> paramResult = JsonConvert.DeserializeObject<DataResult<List<ReportParamsEntity>>>(result);
            List<ReportParamsEntity> list = paramResult.Result;

            string SearchValues = WebUtil.GetQueryStringValue<string>("SearchValues");
            SearchValues = SearchValues.UnEscapge();
            List<ReportParamsEntity> listParams = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReportParamsEntity>>(SearchValues);
            if (!listParams.IsNullOrEmpty())
            {
                foreach (ReportParamsEntity item in listParams)
                {
                    item.ParamName = item.ParamName.Replace("arg_", "@");
                    if (list.Exists(a => a.ParamName == item.ParamName))
                    {
                        list.First(a => a.ParamName == item.ParamName).DefaultValue = item.DefaultValue;
                    }
                }
            }

            ViewBag.Entity = entity;
            ViewBag.ListParam = list;

            WebReport webReport = new WebReport();
            webReport.Width = Unit.Percentage(100);
            webReport.Height = 600;
            webReport.ToolbarIconsStyle = ToolbarIconsStyle.Black;
            webReport.ToolbarIconsStyle = ToolbarIconsStyle.Black;
            webReport.PrintInBrowser = true;
            webReport.PrintInPdf = true;
            webReport.ShowExports = true;
            webReport.ShowPrint = true;
            webReport.SinglePage = true;

            DataSet ds = null;
            int orderType = entity.ReportType;

            dic = new Dictionary<string, string>();

            dic.Add("CompanyID", this.CompanyID);
            dic.Add("Entity", JsonConvert.SerializeObject(entity));
            dic.Add("List", JsonConvert.SerializeObject(list));
            dic.Add("OrderType", orderType.ToString());
            dic.Add("OrderNum", orderNum);
            result = client.Execute(ReportApiName.ReportApiName_GetDataSource, dic);
            DataResult<DataSet> dataSetResult = JsonConvert.DeserializeObject<DataResult<DataSet>>(result);
            ds = dataSetResult.Result;

            string path = Server.MapPath("~" + entity.FileName);
            if (!FileManager.FileExists(path))
            {
                string template = Server.MapPath("~/Theme/content/report/temp/Report.frx");
                System.IO.File.Copy(template, path, true);
            }
            webReport.Report.Load(path);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                webReport.Report.RegisterData(ds);
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    webReport.Report.GetDataSource(ds.Tables[i].TableName).Enabled = true;
                }
            }
            webReport.ID = SnNum;
            ViewBag.WebReport = webReport;
            return View();
        }
    }
}
