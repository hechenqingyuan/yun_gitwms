using Git.Framework.Controller;
using Git.Storage.Entity.Sys;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Newtonsoft.Json;
using System.Data;
using Git.Storage.Common.Excel;
using Git.Storage.Common.Enum;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Git.Storage.Entity.Storage;

namespace Git.WMS.Web.Areas.Storage.Controllers
{
    public class MeasureAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询所有单位列表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string CompanyID = this.CompanyID;
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string MeasureNum = WebUtil.GetFormValue<string>("MeasureNum", string.Empty);
            string MeasureName = WebUtil.GetFormValue<string>("MeasureName", string.Empty);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("MeasureNum", MeasureNum);
            dic.Add("MeasureName", MeasureName);

            string result = client.Execute(MeasureApiName.MeasureApiName_GetPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增单位,编辑单位
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            ITopClient client = new TopClientDefault();
            string SN = WebUtil.GetFormValue<string>("SN");
            string MeasureNum = WebUtil.GetFormValue<string>("MeasureNum");
            string MeasureName = WebUtil.GetFormValue<string>("MeasureName");
            string CompanyID = this.CompanyID;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("SN", SN);
            dic.Add("MeasureNum", MeasureNum);
            dic.Add("MeasureName", MeasureName);

            string ApiName = MeasureApiName.MeasureApiName_Add;
            if (!SN.IsEmpty())
            {
                ApiName = MeasureApiName.MeasureApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Delete()
        {
            ITopClient client = new TopClientDefault();
            string list = WebUtil.GetFormValue<string>("list");
            string CompanyID = this.CompanyID;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("List", list);
            string result = client.Execute(MeasureApiName.MeasureApiName_Delete, dic);
            return Content(result);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToExcel()
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string CompanyID = this.CompanyID;
            dic.Add("CompanyID", CompanyID);
            string result = client.Execute(MeasureApiName.MeasureApiName_GetPage, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<MeasureEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<MeasureEntity>>(result);
                List<MeasureEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("编号"));
                    dt.Columns.Add(new DataColumn("名称"));
                    foreach (MeasureEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.MeasureNum;
                        row[1] = t.MeasureName;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("单位管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("单位管理", "单位", System.IO.Path.Combine(filePath, filename));
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
