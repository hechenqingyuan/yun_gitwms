using Git.Framework.Controller;
using Git.Storage.Entity.Sys;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Common;
using System.Data;
using Git.Storage.Common.Enum;
using Git.Storage.Common.Excel;

namespace Git.WMS.Web.Controllers
{
    public class SequenceAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询标识符分页
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
            string TabName = WebUtil.GetFormValue<string>("TabName", string.Empty);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("TabName", TabName);

            string result = client.Execute(SequenceApiName.SequenceApiName_GetOrderList, dic);
            return Content(result);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Edit()
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string CompanyID = this.CompanyID;
            SequenceEntity entity = WebUtil.GetFormObject<SequenceEntity>("entity");
            dic.Add("CompanyID", CompanyID);
            dic.Add("Entity", JsonConvert.SerializeObject(entity));

            string result = client.Execute(SequenceApiName.SequenceApiName_Edit, dic);
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
            int PageIndex = 1;
            int PageSize = Int32.MaxValue;
            string TabName = WebUtil.GetFormValue<string>("TabName", string.Empty);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("TabName", TabName);

            string result = client.Execute(SequenceApiName.SequenceApiName_GetOrderList, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<SequenceEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<SequenceEntity>>(result);
                List<SequenceEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("表名"));
                    dt.Columns.Add(new DataColumn("类型1"));
                    dt.Columns.Add(new DataColumn("规则1"));
                    dt.Columns.Add(new DataColumn("长度1"));
                    dt.Columns.Add(new DataColumn("连接符"));
                    dt.Columns.Add(new DataColumn("类型2"));
                    dt.Columns.Add(new DataColumn("规则2"));
                    dt.Columns.Add(new DataColumn("长度2"));
                    foreach (SequenceEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.TabName;
                        row[1] = EnumHelper.GetEnumDesc<ESequence>(t.FirstType);
                        row[2] = t.FirstRule;
                        row[3] = t.FirstLength;
                        row[4] = t.JoinChar;
                        row[5] = EnumHelper.GetEnumDesc<ESequence>(t.SecondType);
                        row[6] = t.SecondRule;
                        row[7] = t.SecondLength;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("标识符管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("标识符管理", "标识符管理", System.IO.Path.Combine(filePath, filename));
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
