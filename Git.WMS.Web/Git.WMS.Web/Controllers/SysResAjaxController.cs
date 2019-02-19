using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Common.Excel;
using Git.Storage.Entity.Sys;
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

namespace Git.WMS.Web.Controllers
{
    public class SysResAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询资源分页
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
            string ResNum = WebUtil.GetFormValue<string>("ResNum", string.Empty);
            string ResName = WebUtil.GetFormValue<string>("ResName", string.Empty);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("ResNum", ResNum);
            dic.Add("ResName", ResName);

            string result = client.Execute(SysResourceApiName.SysResourceApiName_GetList, dic);
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
            string ResNum = WebUtil.GetFormValue<string>("ResNum", string.Empty);
            string ResName = WebUtil.GetFormValue<string>("ResName", string.Empty);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("ResNum", ResNum);
            dic.Add("ResName", ResName);

            string result = client.Execute(SysResourceApiName.SysResourceApiName_GetList, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<SysResourceEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<SysResourceEntity>>(result);
                List<SysResourceEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("编号"));
                    dt.Columns.Add(new DataColumn("菜单名称"));
                    dt.Columns.Add(new DataColumn("父级菜单"));
                    dt.Columns.Add(new DataColumn("类型"));
                    dt.Columns.Add(new DataColumn("样式"));
                    dt.Columns.Add(new DataColumn("排序"));
                    dt.Columns.Add(new DataColumn("路径"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    foreach (SysResourceEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.ResNum;
                        row[1] = t.ResName;
                        row[2] = t.ParentName;
                        row[3] = EnumHelper.GetEnumDesc<EResourceType>(t.ResType);
                        row[4] = t.CssName;
                        row[5] = t.Sort;
                        row[6] = t.Url;
                        row[7] = t.CreateTime.To("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("资源管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("资源管理", "资源管理", System.IO.Path.Combine(filePath, filename));
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
        /// 保存权限
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Save()
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string CompanyID = this.CompanyID;
            string RoleNum = WebUtil.GetFormValue<string>("RoleNum", string.Empty);
            string List = WebUtil.GetFormValue<string>("List", string.Empty);

            dic.Add("CompanyID", CompanyID);
            dic.Add("RoleNum", RoleNum);
            dic.Add("List", List);

            string result = client.Execute(UserApiName.UserApiName_SavePower, dic);
            return Content(result);
        }
    }
}
