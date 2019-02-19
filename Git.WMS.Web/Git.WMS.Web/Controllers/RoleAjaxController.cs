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
    public class RoleAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询所有角色列表
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
            string RoleName = WebUtil.GetFormValue<string>("RoleName", string.Empty);
            string Remark = WebUtil.GetFormValue<string>("Remark", string.Empty);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("RoleName", RoleName);
            dic.Add("Remark", Remark);

            string result = client.Execute(RoleApiName.RoleApiName_GetPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增角色,编辑角色
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            ITopClient client = new TopClientDefault();
            string RoleNum = WebUtil.GetFormValue<string>("RoleNum");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string RoleName = WebUtil.GetFormValue<string>("RoleName");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string CompanyID = this.CompanyID;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);
            dic.Add("RoleNum", RoleNum);
            dic.Add("RoleName", RoleName);
            dic.Add("Remark", Remark);

            string ApiName = RoleApiName.RoleApiName_Add;
            if (!RoleNum.IsEmpty())
            {
                ApiName = RoleApiName.RoleApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除角色
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
            string result = client.Execute(RoleApiName.RoleApiName_Delete, dic);
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
            string result = client.Execute(RoleApiName.RoleApiName_GetList, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<SysRoleEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<SysRoleEntity>>(result);
                List<SysRoleEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("角色名"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    dt.Columns.Add(new DataColumn("备注"));
                    dt.Columns.Add(new DataColumn("角色编号"));
                    foreach (SysRoleEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.RoleName;
                        row[1] = t.CreateTime.To("yyyy-MM-dd");
                        row[2] = t.Remark;
                        row[3] = t.RoleNum;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("角色管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("角色管理", "角色", System.IO.Path.Combine(filePath, filename));
                    excel.ToExcel(dt);
                    returnValue = ("/UploadFile/" + filename).Escape();
                }
            }
            DataResult returnResult = null;
            if (!returnValue.IsEmpty())
            {
                returnResult = new DataResult() { Code = 1000, Message = returnValue };
            }
            else{
                returnResult = new DataResult() { Code = 1001, Message = "没有任何数据导出" };
            }
            return Content(JsonHelper.SerializeObject(returnResult));
        }

    }
}
