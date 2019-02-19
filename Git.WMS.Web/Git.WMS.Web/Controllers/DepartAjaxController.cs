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

namespace Git.WMS.Web.Controllers
{
    public class DepartAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询所有部门列表
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
            string DepartName = WebUtil.GetFormValue<string>("DepartName", string.Empty);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("DepartName", DepartName);

            string result = client.Execute(DepartApiName.DepartApiName_GetPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增部门,编辑部门
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            ITopClient client = new TopClientDefault();
            string CompanyID = this.CompanyID;
            string DepartName = WebUtil.GetFormValue<string>("DepartName");
            string ParentNum = WebUtil.GetFormValue<string>("ParentNum");
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("DepartName", DepartName);
            dic.Add("ParentNum", ParentNum);
            dic.Add("DepartNum", DepartNum);
            dic.Add("SnNum", SnNum);

            string ApiName = DepartApiName.DepartApiName_Add;
            if (!DepartNum.IsEmpty())
            {
                ApiName = DepartApiName.DepartApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除部门
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
            string result = client.Execute(DepartApiName.DepartApiName_Delete, dic);
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
            string result = client.Execute(DepartApiName.DepartApiName_GetList, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<SysDepartEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<SysDepartEntity>>(result);
                List<SysDepartEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("部门名"));
                    dt.Columns.Add(new DataColumn("上级部门"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    dt.Columns.Add(new DataColumn("部门编号"));
                    foreach (SysDepartEntity t in listResult.Remove(item=>item.DepartName=="Root"))
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.DepartName;
                        row[1] = t.ParentName;
                        row[2] = t.CreateTime.To("yyyy-MM-dd");
                        row[3] = t.DepartNum;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("部门管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("部门管理", "部门管理", System.IO.Path.Combine(filePath, filename));
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
