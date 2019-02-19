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
using Git.Storage.Common.Enum;
using System.Text;

namespace Git.WMS.Web.Areas.Client.Controllers
{
    public class CustomerAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询客户分页
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            string CompanyID = this.CompanyID;

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string CusNum = WebUtil.GetFormValue<string>("CusNum", string.Empty);
            string CusName = WebUtil.GetFormValue<string>("CusName", string.Empty);
            string Phone = WebUtil.GetFormValue<string>("Phone", string.Empty);
            int CusType = WebUtil.GetFormValue<int>("CusType", -1);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("CusNum", CusNum);
            dic.Add("CusName", CusName);
            dic.Add("Phone", Phone);
            dic.Add("CusType", CusType.ToString());

            string result = client.Execute(CustomerApiName.CustomerApiName_GetAddressPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增或修改客户 
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            ITopClient client = new TopClientDefault();
            string CompanyID = this.CompanyID;
            CustomerEntity entity = WebUtil.GetFormObject<CustomerEntity>("Entity");
            entity.CreateTime = DateTime.Now;
            entity.CreateUser = this.LoginUserNum;
            entity.CompanyID = this.CompanyID;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("Entity",JsonHelper.SerializeObject(entity));

            List<CusAddressEntity> listAddress = Session[SessionKey.SESSION_CUSTOMER_ADDRESS] as List<CusAddressEntity>;
            listAddress = listAddress.IsNull() ? new List<CusAddressEntity>() : listAddress;
            dic.Add("List", JsonHelper.SerializeObject(listAddress));

            string ApiName = CustomerApiName.CustomerApiName_Add;
            if (!entity.SnNum.IsEmpty())
            {
                ApiName = CustomerApiName.CustomerApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            DataResult dataResult = JsonHelper.DeserializeObject<DataResult>(result);
            if (dataResult.Code == (int)EResponseCode.Success)
            {
                Session[SessionKey.SESSION_CUSTOMER_ADDRESS] = null;
            }
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
            string result = client.Execute(CustomerApiName.CustomerApiName_Delete, dic);
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
            string CusNum = WebUtil.GetFormValue<string>("CusNum", string.Empty);
            string CusName = WebUtil.GetFormValue<string>("CusName", string.Empty);
            string Phone = WebUtil.GetFormValue<string>("Phone", string.Empty);
            int CusType = WebUtil.GetFormValue<int>("CusType", -1);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("CusNum", CusNum);
            dic.Add("CusName", CusName);
            dic.Add("Phone", Phone);
            dic.Add("CusType", CusType.ToString());

            string result = client.Execute(CustomerApiName.CustomerApiName_GetAddressPage, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<CusAddressEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<CusAddressEntity>>(result);
                List<CusAddressEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("客户编号"));
                    dt.Columns.Add(new DataColumn("客户名称"));
                    dt.Columns.Add(new DataColumn("地址"));
                    dt.Columns.Add(new DataColumn("联系人"));
                    dt.Columns.Add(new DataColumn("电话"));
                    dt.Columns.Add(new DataColumn("备注"));
                    dt.Columns.Add(new DataColumn("邮箱"));
                    dt.Columns.Add(new DataColumn("传真"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    foreach (CusAddressEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.CusNum;
                        row[1] = t.CusName;
                        row[2] = t.Address;
                        row[3] = t.Contact;
                        row[4] = t.Phone;
                        row[5] = t.Remark;
                        row[6] = t.Email;
                        row[7] = t.Fax;
                        row[8] = t.CreateTime.To("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("客户管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("客户管理", "客户", System.IO.Path.Combine(filePath, filename));
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
        /// 获取客户地址详细
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult GetAddList()
        {
            List<CusAddressEntity> listAddress = Session[SessionKey.SESSION_CUSTOMER_ADDRESS] as List<CusAddressEntity>;
            listAddress = listAddress.IsNull() ? new List<CusAddressEntity>() : listAddress;

            DataListResult<CusAddressEntity> dataResult = new DataListResult<CusAddressEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Result = listAddress;
            string result = JsonConvert.SerializeObject(dataResult);
            return Content(result);
        }

        /// <summary>
        /// 删除客户地址
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult DelAdd()
        {
            List<CusAddressEntity> listAddress = Session[SessionKey.SESSION_CUSTOMER_ADDRESS] as List<CusAddressEntity>;
            listAddress = listAddress.IsNull() ? new List<CusAddressEntity>() : listAddress;

            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            listAddress.Remove(a => a.SnNum == SnNum);
            Session[SessionKey.SESSION_CUSTOMER_ADDRESS] = listAddress;
            DataResult dataResult = new DataResult();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "操作成功";
            string result = JsonConvert.SerializeObject(dataResult);
            return Content(result);
        }

        /// <summary>
        /// 新增客户地址
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult AddAddress()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CustomerSN = WebUtil.GetFormValue<string>("CustomerSN");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string Address = WebUtil.GetFormValue<string>("Address");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            SnNum = SnNum.IsEmpty() ? ConvertHelper.NewGuid() : SnNum;

            List<CusAddressEntity> listAddress = Session[SessionKey.SESSION_CUSTOMER_ADDRESS] as List<CusAddressEntity>;
            listAddress = listAddress.IsNull() ? new List<CusAddressEntity>() : listAddress;

            if (!SnNum.IsEmpty() && listAddress.Exists(a => a.SnNum == SnNum))
            {
                CusAddressEntity entity = listAddress.First(a => a.SnNum == SnNum);
                entity.CustomerSN = CustomerSN;
                entity.Contact = Contact;
                entity.Phone = Phone;
                entity.Address = Address;
                entity.Remark = Remark;
            }
            else
            {
                CusAddressEntity entity = new CusAddressEntity();
                entity.SnNum = SnNum;
                entity.CustomerSN = CustomerSN;
                entity.Contact = Contact;
                entity.Phone = Phone;
                entity.Address = Address;
                entity.IsDelete = (int)EIsDelete.NotDelete;
                entity.CreateTime = DateTime.Now;
                entity.CreateUser = this.LoginUser.UserNum;
                entity.Remark = Remark;
                entity.CompanyID = this.CompanyID;
                listAddress.Add(entity);
            }
            Session[SessionKey.SESSION_CUSTOMER_ADDRESS] = listAddress;
            DataResult dataResult = new DataResult();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "操作成功";
            string result = JsonConvert.SerializeObject(dataResult);
            return Content(result);
        }

        /// <summary>
        /// 客户地址分页
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult GetAddressList()
        {
            string CompanyID = this.CompanyID;
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string Address = WebUtil.GetFormValue<string>("Address", string.Empty);
            string Phone = WebUtil.GetFormValue<string>("Phone", string.Empty);
            string CusNum = WebUtil.GetFormValue<string>("CusNum", string.Empty);
            string CusName = WebUtil.GetFormValue<string>("CusName", string.Empty);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("Address", Address);
            dic.Add("Phone", Phone);
            dic.Add("CusNum", CusNum);
            dic.Add("CusName", CusName);

            string result = client.Execute(CustomerApiName.CustomerApiName_GetAddressPage, dic);
            return Content(result);
        }


        /// <summary>
        /// 搜索客户信息
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult AutoCustomer()
        {
            string KeyWord = WebUtil.GetQueryStringValue<string>("KeyWord");
            int TopSize = WebUtil.GetFormValue<int>("TopSize", 10);
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("TopSize", TopSize.ToString());
            dic.Add("KeyWord", KeyWord);

            string result = client.Execute(CustomerApiName.CustomerApiName_SearchCustomer, dic);
            DataListResult<CusAddressEntity> dataResult = JsonHelper.DeserializeObject<DataListResult<CusAddressEntity>>(result);
            List<CusAddressEntity> listResult = dataResult.Result;
            listResult = listResult.IsNull() ? new List<CusAddressEntity>() : listResult;

            StringBuilder sb = new StringBuilder();
            foreach (CusAddressEntity item in listResult)
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
