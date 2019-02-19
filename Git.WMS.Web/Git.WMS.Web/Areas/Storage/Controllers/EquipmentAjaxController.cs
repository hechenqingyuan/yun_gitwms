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
    public class EquipmentAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询所有设备列表
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

            string EquipmentName = WebUtil.GetFormValue<string>("EquipmentName");
            string Remark = WebUtil.GetFormValue<string>("Remark", string.Empty);
            string EquipmentNum = WebUtil.GetFormValue<string>("EquipmentNum", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status", 0);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("EquipmentName", EquipmentName);
            dic.Add("Remark", Remark);
            dic.Add("EquipmentNum", EquipmentNum);
            dic.Add("Status", Status.ToString());

            string result = client.Execute(EquipmentApiName.EquipmentApiName_GetPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增设备,编辑设备
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            ITopClient client = new TopClientDefault();

            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string EquipmentName = WebUtil.GetFormValue<string>("EquipmentName");
            string EquipmentNum = WebUtil.GetFormValue<string>("EquipmentNum");
            int IsImpower = WebUtil.GetFormValue<int>("IsImpower");
            string Flag = WebUtil.GetFormValue<string>("Flag");
            int Status = WebUtil.GetFormValue<int>("Status");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string CompanyID = this.CompanyID;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);
            dic.Add("EquipmentName", EquipmentName);
            dic.Add("EquipmentNum", EquipmentNum);
            dic.Add("IsImpower", IsImpower.ToString());
            dic.Add("Flag", Flag);
            dic.Add("Status", Status.ToString());
            dic.Add("Remark", Remark);
            dic.Add("CreateUser", this.LoginUser.UserNum);

            string ApiName = EquipmentApiName.EquipmentApiName_Add;
            if (!SnNum.IsEmpty())
            {
                ApiName = EquipmentApiName.EquipmentApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除设备
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
            string result = client.Execute(EquipmentApiName.EquipmentApiName_Delete, dic);
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
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", Int32.MaxValue);

            string EquipmentName = WebUtil.GetFormValue<string>("EquipmentName");
            string Remark = WebUtil.GetFormValue<string>("Remark", string.Empty);
            string EquipmentNum = WebUtil.GetFormValue<string>("EquipmentNum", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status", 0);

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("EquipmentName", EquipmentName);
            dic.Add("Remark", Remark);
            dic.Add("EquipmentNum", EquipmentNum);
            dic.Add("Status", Status.ToString());

            string result = client.Execute(EquipmentApiName.EquipmentApiName_GetPage, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<EquipmentEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<EquipmentEntity>>(result);
                List<EquipmentEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("编号"));
                    dt.Columns.Add(new DataColumn("名称"));
                    dt.Columns.Add(new DataColumn("是否授权"));
                    dt.Columns.Add(new DataColumn("授权标识符"));
                    dt.Columns.Add(new DataColumn("状态"));
                    dt.Columns.Add(new DataColumn("备注"));
                    foreach (EquipmentEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.EquipmentNum;
                        row[1] = t.EquipmentName;
                        row[2] = EnumHelper.GetEnumDesc<EBool>(t.IsImpower);
                        row[3] = t.Flag;
                        row[4] = EnumHelper.GetEnumDesc<EEquipmentStatus>(t.Status);
                        row[5] = t.Remark;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("设备管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("设备管理", "设备管理", System.IO.Path.Combine(filePath, filename));
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
        /// 生成标识符
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult CreateFlag()
        {
            string Flag = ConvertHelper.NewGuid();
            DataResult<string> dataResult = new DataResult<string>() { Code = (int)EResponseCode.Success, Result = Flag };
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
