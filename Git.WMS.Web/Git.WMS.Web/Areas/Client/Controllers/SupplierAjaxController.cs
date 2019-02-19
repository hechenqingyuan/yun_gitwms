using Git.Framework.Controller;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Entity.Storage;
using Newtonsoft.Json;
using Git.Storage.Common;
using System.Data;
using Git.Storage.Common.Excel;
using System.Text;
using Git.Storage.Common.Enum;

namespace Git.WMS.Web.Areas.Client.Controllers
{
    public class SupplierAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询供应商分页
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            string CompanyID = this.CompanyID;

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string SupName = WebUtil.GetFormValue<string>("SupName", string.Empty);
            string SupNum = WebUtil.GetFormValue<string>("SupNum", string.Empty);
            string Phone = WebUtil.GetFormValue<string>("Phone",string.Empty);
            int SupType = WebUtil.GetFormValue<int>("SupType", -1);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("SupName", SupName);
            dic.Add("SupNum", SupNum);
            dic.Add("Phone", Phone);
            dic.Add("SupType", SupType.ToString());

            string result = client.Execute(SupplierApiName.SupplierApiName_GetPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增或修改供应商 
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            ITopClient client = new TopClientDefault();
            string CompanyID = this.CompanyID;
            SupplierEntity entity = WebUtil.GetFormObject<SupplierEntity>("Entity");
            entity.CreateTime = DateTime.Now;
            entity.CreateUser = this.LoginUserNum;
            entity.CompanyID = this.CompanyID;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("Entity",JsonConvert.SerializeObject(entity));

            string ApiName = SupplierApiName.SupplierApiName_Add;
            if (!entity.SnNum.IsEmpty())
            {
                ApiName = SupplierApiName.SupplierApiName_Edit;
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
            string result = client.Execute(SupplierApiName.SupplierApiName_Delete, dic);
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
            int PageIndex = 1;
            int PageSize = Int32.MaxValue;
            string SupName = WebUtil.GetFormValue<string>("SupName", string.Empty);
            string SupNum = WebUtil.GetFormValue<string>("SupNum", string.Empty);
            int SupType = WebUtil.GetFormValue<int>("SupType", -1);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("SupName", SupName);
            dic.Add("SupNum", SupNum);
            dic.Add("SupType", SupType.ToString());

            string result = client.Execute(SupplierApiName.SupplierApiName_GetPage, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<SupplierEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<SupplierEntity>>(result);
                List<SupplierEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("供应商编号"));
                    dt.Columns.Add(new DataColumn("供应商名称"));
                    dt.Columns.Add(new DataColumn("类型"));
                    dt.Columns.Add(new DataColumn("电话"));
                    dt.Columns.Add(new DataColumn("传真"));
                    dt.Columns.Add(new DataColumn("Email"));
                    dt.Columns.Add(new DataColumn("联系人"));
                    dt.Columns.Add(new DataColumn("地址"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    dt.Columns.Add(new DataColumn("描述"));
                    foreach (SupplierEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.SupNum;
                        row[1] = t.SupName;
                        row[2] = EnumHelper.GetEnumDesc<ESupType>(t.SupType);
                        row[3] = t.Phone;
                        row[4] = t.Fax;
                        row[5] = t.Email;
                        row[6] = t.ContactName;
                        row[7] = t.Address;
                        row[8] = t.CreateTime.To("yyyy-MM-dd");
                        row[9] = t.Description;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("供应商管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("供应商管理", "供应商", System.IO.Path.Combine(filePath, filename));
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
        /// 搜索供应商信息
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult AutoSupplier()
        {
            string KeyWord = WebUtil.GetQueryStringValue<string>("KeyWord");
            int TopSize = WebUtil.GetFormValue<int>("TopSize", 10);
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("TopSize", TopSize.ToString());
            dic.Add("KeyWord", KeyWord);

            string result = client.Execute(SupplierApiName.SupplierApiName_SearchSupplier, dic);
            DataListResult<SupplierEntity> dataResult = JsonHelper.DeserializeObject<DataListResult<SupplierEntity>>(result);
            List<SupplierEntity> listResult = dataResult.Result;
            listResult = listResult.IsNull() ? new List<SupplierEntity>() : listResult;

            StringBuilder sb = new StringBuilder();
            foreach (SupplierEntity item in listResult)
            {
                sb.Append(JsonHelper.SerializeObject(item) + "\n");
            }

            if (sb.Length == 0)
            {
                sb.Append("\n");
            }
            return Content(sb.ToString());
        }

    }
}
