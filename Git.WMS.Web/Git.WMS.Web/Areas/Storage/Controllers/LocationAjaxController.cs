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
    public class LocationAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询所有库位列表
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
            string LocalBarCode = WebUtil.GetFormValue<string>("LocalBarCode");
            string LocalName = WebUtil.GetFormValue<string>("LocalName", string.Empty);
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);
            int StorageType = WebUtil.GetFormValue<int>("StorageType", 0);
            List<int> listLocalType = WebUtil.GetFormObject<List<int>>("ListLocalType", null);
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid", -1);
            int IsDefault = WebUtil.GetFormValue<int>("IsDefault", -1);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("LocalBarCode", LocalBarCode);
            dic.Add("LocalName", LocalName);
            dic.Add("StorageNum", StorageNum);
            dic.Add("StorageType", StorageType.ToString());
            dic.Add("ListLocalType", JsonHelper.SerializeObject(listLocalType));
            dic.Add("IsForbid", IsForbid.ToString());
            dic.Add("IsDefault", IsDefault.ToString());

            string result = client.Execute(LocationApiName.LocationApiName_GetPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增库位,编辑库位
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            ITopClient client = new TopClientDefault();

            string LocalNum = WebUtil.GetFormValue<string>("LocalNum");
            string LocalBarCode = WebUtil.GetFormValue<string>("LocalBarCode");
            string LocalName = WebUtil.GetFormValue<string>("LocalName");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            int StorageType = WebUtil.GetFormValue<int>("StorageType");
            int LocalType = WebUtil.GetFormValue<int>("LocalType");
            string Rack = WebUtil.GetFormValue<string>("Rack");
            double Length = WebUtil.GetFormValue<double>("Length");
            double Width = WebUtil.GetFormValue<double>("Width");
            double Height = WebUtil.GetFormValue<double>("Height");
            double X = WebUtil.GetFormValue<double>("X");
            double Y = WebUtil.GetFormValue<double>("Y");
            double Z = WebUtil.GetFormValue<double>("Z");
            string UnitNum = WebUtil.GetFormValue<string>("UnitNum");
            string UnitName = WebUtil.GetFormValue<string>("UnitName");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid",(int)EBool.No);
            int IsDefault = WebUtil.GetFormValue<int>("IsDefault", (int)EBool.No);
            int IsDelete = (int)EIsDelete.NotDelete;
            DateTime CreateTime = DateTime.Now;
            string CompanyID = this.CompanyID;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("LocalNum", LocalNum);
            dic.Add("LocalBarCode", LocalBarCode);
            dic.Add("LocalName", LocalName);
            dic.Add("StorageNum", StorageNum);
            dic.Add("StorageType", StorageType.ToString());
            dic.Add("LocalType", LocalType.ToString());
            dic.Add("Rack", Rack);
            dic.Add("Length", Length.ToString());
            dic.Add("Width", Width.ToString());
            dic.Add("Height", Height.ToString());
            dic.Add("X", X.ToString());
            dic.Add("Y", Y.ToString());
            dic.Add("Z", Z.ToString());
            dic.Add("UnitNum", UnitNum);
            dic.Add("UnitName", UnitName);
            dic.Add("Remark", Remark);
            dic.Add("IsForbid", IsForbid.ToString());
            dic.Add("IsDefault", IsDefault.ToString());
            dic.Add("IsDelete", IsDelete.ToString());
            dic.Add("CreateTime", CreateTime.To("yyyy-MM-dd"));
            dic.Add("CompanyID", CompanyID);

            string ApiName = LocationApiName.LocationApiName_Add;
            if (!LocalNum.IsEmpty())
            {
                ApiName = LocationApiName.LocationApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除库位
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
            string result = client.Execute(LocationApiName.LocationApiName_Delete, dic);
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
            string LocalBarCode = WebUtil.GetFormValue<string>("LocalBarCode");
            string LocalName = WebUtil.GetFormValue<string>("LocalName", string.Empty);
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);
            int StorageType = WebUtil.GetFormValue<int>("StorageType", 0);
            List<int> listLocalType = WebUtil.GetFormObject<List<int>>("ListLocalType", null);
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid", -1);
            int IsDefault = WebUtil.GetFormValue<int>("IsDefault", -1);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("LocalBarCode", LocalBarCode);
            dic.Add("LocalName", LocalName);
            dic.Add("StorageNum", StorageNum);
            dic.Add("StorageType", StorageType.ToString());
            dic.Add("ListLocalType", JsonHelper.SerializeObject(listLocalType));
            dic.Add("IsForbid", IsForbid.ToString());
            dic.Add("IsDefault", IsDefault.ToString());

            string result = client.Execute(LocationApiName.LocationApiName_GetPage, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<LocationEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<LocationEntity>>(result);
                List<LocationEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("库位编号"));
                    dt.Columns.Add(new DataColumn("库位名称"));
                    dt.Columns.Add(new DataColumn("库位类型"));
                    dt.Columns.Add(new DataColumn("仓库"));
                    dt.Columns.Add(new DataColumn("是否禁用"));
                    dt.Columns.Add(new DataColumn("是否默认"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    dt.Columns.Add(new DataColumn("备注"));
                    foreach (LocationEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.LocalBarCode;
                        row[1] = t.LocalName;
                        row[2] = EnumHelper.GetEnumDesc<ELocalType>(t.LocalType);
                        row[3] = t.StorageName;
                        row[4] = EnumHelper.GetEnumDesc<EBool>(t.IsForbid);
                        row[5] = EnumHelper.GetEnumDesc<EBool>(t.IsDefault);
                        row[6] = t.CreateTime.To("yyyy-MM-dd");
                        row[7] = t.Remark;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("库位管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("库位管理", "库位管理", System.IO.Path.Combine(filePath, filename));
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
        /// 设置默认库位
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult SetDefault()
        {
            ITopClient client = new TopClientDefault();
            string CompanyID = this.CompanyID;
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum", string.Empty);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("LocalNum", LocalNum);
            string result = client.Execute(LocationApiName.LocationApiName_SetDefault, dic);
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
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum", string.Empty);
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid", (int)EBool.No);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("LocalNum", LocalNum);
            dic.Add("IsForbid", IsForbid.ToString());
            string result = client.Execute(LocationApiName.LocationApiName_SetForbid, dic);
            return Content(result);
        }
    }
}
