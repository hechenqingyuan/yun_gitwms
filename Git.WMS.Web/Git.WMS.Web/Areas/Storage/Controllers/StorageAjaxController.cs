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
    public class StorageAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询所有仓库列表
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

            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string StorageName = WebUtil.GetFormValue<string>("StorageName", string.Empty);
            string StorageType = WebUtil.GetFormValue<string>("StorageType", string.Empty);
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid", -1);
            int IsDefault = WebUtil.GetFormValue<int>("IsDefault", -1);
            double Area = WebUtil.GetFormValue<double>("Area");
            string Address = WebUtil.GetFormValue<string>("Address");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("StorageNum", StorageNum);
            dic.Add("StorageName", StorageName);
            dic.Add("StorageType", StorageType);
            dic.Add("DepartNum", DepartNum);
            dic.Add("Status", Status.ToString());
            dic.Add("IsForbid", IsForbid.ToString());
            dic.Add("IsDefault", IsDefault.ToString());
            

            string result = client.Execute(StorageApiName.StorageApiName_GetPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增仓库,编辑仓库
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            ITopClient client = new TopClientDefault();

            string CompanyID = this.CompanyID;
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string StorageName = WebUtil.GetFormValue<string>("StorageName");
            int StorageType = WebUtil.GetFormValue<int>("StorageType");
            double Length = WebUtil.GetFormValue<double>("Length");
            double Width = WebUtil.GetFormValue<double>("Width");
            double Height = WebUtil.GetFormValue<double>("Height");
            string Action = WebUtil.GetFormValue<string>("Action");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum");
            string ShortName = WebUtil.GetFormValue<string>("ShortName");
            string LeaseTime = WebUtil.GetFormValue<string>("LeaseTime");
            double Area = WebUtil.GetFormValue<double>("Area");
            string Address = WebUtil.GetFormValue<string>("Address");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);
            dic.Add("StorageNum", StorageNum);
            dic.Add("StorageName", StorageName);
            dic.Add("StorageType", StorageType.ToString());
            dic.Add("Length", Length.ToString());
            dic.Add("Width", Width.ToString());
            dic.Add("Height", Height.ToString());
            dic.Add("Action", Action);
            dic.Add("Remark", Remark);
            dic.Add("DepartNum", DepartNum);
            dic.Add("LeaseTime", LeaseTime);
            dic.Add("Address", Address);
            dic.Add("Contact", Contact);
            dic.Add("Phone", Phone);
            dic.Add("Area", Area.ToString());
            dic.Add("CreateUser", this.LoginUser.UserNum);

            string ApiName = StorageApiName.StorageApiName_Add;
            if (!SnNum.IsEmpty())
            {
                ApiName = StorageApiName.StorageApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除仓库
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
            string result = client.Execute(StorageApiName.StorageApiName_Delete, dic);
            return Content(result);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToExcel()
        {
            DataResult returnResult = null;
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string CompanyID = this.CompanyID;
            int PageIndex = 1;
            int PageSize = Int32.MaxValue;

            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string StorageName = WebUtil.GetFormValue<string>("StorageName", string.Empty);
            string StorageType = WebUtil.GetFormValue<string>("StorageType", string.Empty);
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid", -1);
            int IsDefault = WebUtil.GetFormValue<int>("IsDefault", -1);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("StorageNum", StorageNum);
            dic.Add("StorageName", StorageName);
            dic.Add("StorageType", StorageType);
            dic.Add("DepartNum", DepartNum);
            dic.Add("Status", Status.ToString());
            dic.Add("IsForbid", IsForbid.ToString());
            dic.Add("IsDefault", IsDefault.ToString());

            string result = client.Execute(StorageApiName.StorageApiName_GetPage, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<StorageEntity> dataResult = JsonHelper.DeserializeObject<DataListResult<StorageEntity>>(result);
                List<StorageEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("仓库编号"));
                    dt.Columns.Add(new DataColumn("仓库名称"));
                    dt.Columns.Add(new DataColumn("租赁时间"));
                    dt.Columns.Add(new DataColumn("所属部门"));
                    dt.Columns.Add(new DataColumn("仓库类型"));
                    dt.Columns.Add(new DataColumn("是否禁用"));
                    dt.Columns.Add(new DataColumn("是否默认"));
                    dt.Columns.Add(new DataColumn("地址"));
                    dt.Columns.Add(new DataColumn("面积"));
                    dt.Columns.Add(new DataColumn("联系人"));
                    dt.Columns.Add(new DataColumn("电话"));
                    foreach (StorageEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.StorageNum;
                        row[1] = t.StorageName;
                        row[2] = t.LeaseTime.To("yyyy-MM-dd");
                        row[3] = t.DepartName;
                        row[4] = EnumHelper.GetEnumDesc<EStorageType>(t.StorageType);
                        row[5] = EnumHelper.GetEnumDesc<EBool>(t.IsForbid);
                        row[6] = EnumHelper.GetEnumDesc<EBool>(t.IsDefault);
                        row[7] = t.Address;
                        row[8] = t.Area.ToString("0.00");
                        row[9] = t.Contact;
                        row[10] = t.Phone;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("仓库管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("仓库管理", "仓库", System.IO.Path.Combine(filePath, filename));
                    excel.ToExcel(dt);
                    returnValue = ("/UploadFile/" + filename).Escape();
                }
            }

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
        /// 设置默认仓库
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult SetDefault()
        {
            ITopClient client = new TopClientDefault();
            string CompanyID = this.CompanyID;
            string SnNum = WebUtil.GetFormValue<string>("SnNum", string.Empty);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);
            string result = client.Execute(StorageApiName.StorageApiName_SetDefault, dic);
            return Content(result);
        }

        /// <summary>
        /// 设置禁用或者启用
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult SetForbid()
        {
            ITopClient client = new TopClientDefault();
            string CompanyID = this.CompanyID;
            string SnNum = WebUtil.GetFormValue<string>("SnNum",string.Empty);
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid",(int)EBool.No);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);
            dic.Add("IsForbid", IsForbid.ToString());
            string result = client.Execute(StorageApiName.StorageApiName_SetForbid, dic);
            return Content(result);
        }
    }
}
