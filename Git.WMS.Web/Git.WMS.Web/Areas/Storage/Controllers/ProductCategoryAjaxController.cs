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
    public class ProductCategoryAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询所有产品类别列表
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

            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string CateName = WebUtil.GetFormValue<string>("CateName");
            string Remark = WebUtil.GetFormValue<string>("Remark");

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("CateName", CateName);
            dic.Add("CateNum", CateNum);
            dic.Add("Remark", Remark);

            string result = client.Execute(ProductCategoryApiName.ProductCategoryApiName_GetPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增产品类别,编辑产品类别
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            ITopClient client = new TopClientDefault();

            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CateName = WebUtil.GetFormValue<string>("CateName");
            string ParentNum = WebUtil.GetFormValue<string>("ParentNum");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string CompanyID = this.CompanyID;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("CateNum", CateNum);
            dic.Add("SnNum", SnNum);
            dic.Add("CateName", CateName);
            dic.Add("ParentNum", ParentNum);
            dic.Add("Remark", Remark);

            string ApiName = ProductCategoryApiName.ProductCategoryApiName_Add;
            if (!SnNum.IsEmpty())
            {
                ApiName = ProductCategoryApiName.ProductCategoryApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除产品类别
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
            string result = client.Execute(ProductCategoryApiName.ProductCategoryApiName_Delete, dic);
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

            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string CateName = WebUtil.GetFormValue<string>("CateName");
            string Remark = WebUtil.GetFormValue<string>("Remark");

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("CateName", CateName);
            dic.Add("CateNum", CateNum);
            dic.Add("Remark", Remark);

            string result = client.Execute(ProductCategoryApiName.ProductCategoryApiName_GetPage, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<ProductCategoryEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<ProductCategoryEntity>>(result);
                List<ProductCategoryEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("类别编号"));
                    dt.Columns.Add(new DataColumn("类别名称"));
                    dt.Columns.Add(new DataColumn("父级名称"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    dt.Columns.Add(new DataColumn("备注"));
                    foreach (ProductCategoryEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.CateNum;
                        row[1] = t.CateName;
                        row[2] = t.ParentName;
                        row[3] = t.CreateTime.To("yyyy-MM-dd");
                        row[4] = t.Remark;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("产品类别管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("产品类别管理", "产品类别", System.IO.Path.Combine(filePath, filename));
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
