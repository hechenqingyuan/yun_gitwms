using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Excel;
using Git.Storage.Entity.Finance;
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

namespace Git.WMS.Web.Areas.Finance.Controllers
{
    public class CateAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询所有财务分类列表
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
            string CateName = WebUtil.GetFormValue<string>("CateName", string.Empty);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("CateName", CateName);

            string result = client.Execute(FinanceCateApiName.FinanceCateApiName_GetPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增财务分类,编辑财务分类
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            ITopClient client = new TopClientDefault();
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string CateName = WebUtil.GetFormValue<string>("CateName");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = this.CompanyID;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);
            dic.Add("CateNum", CateNum);
            dic.Add("CateName", CateName);

            string ApiName = FinanceCateApiName.FinanceCateApiName_Add;
            if (!SnNum.IsEmpty())
            {
                ApiName = FinanceCateApiName.FinanceCateApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除财务分类
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
            string result = client.Execute(FinanceCateApiName.FinanceCateApiName_Delete, dic);
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
            string CateName = WebUtil.GetFormValue<string>("CateName", string.Empty);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("CateName", CateName);

            string result = client.Execute(FinanceCateApiName.FinanceCateApiName_GetPage, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<FinanceCateEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<FinanceCateEntity>>(result);
                List<FinanceCateEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("类别编号"));
                    dt.Columns.Add(new DataColumn("类别名称"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    foreach (FinanceCateEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.CateNum;
                        row[1] = t.CateName;
                        row[2] = t.CreateTime.To("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("财务分类管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("财务分类管理", "财务分类", System.IO.Path.Combine(filePath, filename));
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
