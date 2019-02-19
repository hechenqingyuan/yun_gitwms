using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Common.Excel;
using Git.Storage.Entity.InStorage;
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

namespace Git.WMS.Web.Areas.Order.Controllers
{
    public class InStorageManagerAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            string CompanyID = this.CompanyID;

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int InType = WebUtil.GetFormValue<int>("InType",0);
            string SupName = WebUtil.GetFormValue<string>("SupName",string.Empty);
            string SupNum = WebUtil.GetFormValue<string>("SupNum",string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status",0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder",string.Empty);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime", string.Empty);
            string EndTime = WebUtil.GetFormValue<string>("EndTime", string.Empty);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("OrderNum", OrderNum);
            dic.Add("StorageNum",this.DefaultStorageNum);
            dic.Add("InType", InType.ToString());
            dic.Add("SupName", SupName);
            dic.Add("SupNum", SupNum);
            dic.Add("Status", Status.ToString());
            dic.Add("ContractOrder", ContractOrder);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);

            string result = client.Execute(InStorageApiName.InStorageApiName_GetDetailList, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除入库单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Delete()
        {
            string CompanyID = this.CompanyID;
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("list", JsonConvert.SerializeObject(list));

            string result = client.Execute(InStorageApiName.InStorageApiName_Delete, dic);
            return Content(result);
        }

        /// <summary>
        /// 取消入库单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Cancel()
        {
            string CompanyID = this.CompanyID;
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);

            string result = client.Execute(InStorageApiName.InStorageApiName_Cancel, dic);
            return Content(result);
        }

        /// <summary>
        /// 审核入库单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Audite()
        {
            string CompanyID = this.CompanyID;
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            int Status = WebUtil.GetFormValue<int>("Status");
            string Reason = WebUtil.GetFormValue<string>("Reason");
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);
            dic.Add("Status", Status.ToString());
            dic.Add("Reason", Reason);
            dic.Add("AuditUser", this.LoginUser.UserNum);
            dic.Add("OperateType", ((int)EOpType.PC).ToString());
            dic.Add("EquipmentNum", "");
            dic.Add("EquipmentCode", "");

            string result = client.Execute(InStorageApiName.InStorageApiName_Audite, dic);
            return Content(result);
        }

        /// <summary>
        /// 获得入库单详细
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult GetDetail()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);

            string result = client.Execute(InStorageApiName.InStorageApiName_GetDetail, dic);

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
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            int InType = WebUtil.GetFormValue<int>("InType", 0);
            string SupName = WebUtil.GetFormValue<string>("SupName", string.Empty);
            string SupNum = WebUtil.GetFormValue<string>("SupNum", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder", string.Empty);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("OrderNum", OrderNum);
            dic.Add("InType", InType.ToString());
            dic.Add("SupName", SupName);
            dic.Add("SupNum", SupNum);
            dic.Add("Status", Status.ToString());
            dic.Add("ContractOrder", ContractOrder);

            string result = client.Execute(InStorageApiName.InStorageApiName_GetDetailList, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<InStorDetailEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<InStorDetailEntity>>(result);
                List<InStorDetailEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("订单号"));
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("批次"));
                    dt.Columns.Add(new DataColumn("规格"));
                    dt.Columns.Add(new DataColumn("数量"));
                    dt.Columns.Add(new DataColumn("仓库"));
                    dt.Columns.Add(new DataColumn("库位"));
                    dt.Columns.Add(new DataColumn("入库类型"));
                    dt.Columns.Add(new DataColumn("供应商"));
                    dt.Columns.Add(new DataColumn("制单人"));
                    dt.Columns.Add(new DataColumn("制单时间"));
                    dt.Columns.Add(new DataColumn("状态"));
                    dt.Columns.Add(new DataColumn("审核人"));
                    dt.Columns.Add(new DataColumn("审核时间"));

                    foreach (InStorDetailEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.OrderNum;
                        row[1] = t.ProductName;
                        row[2] = t.BarCode;
                        row[3] = t.BatchNum;
                        row[4] = t.Size;
                        row[5] = t.Num.ToString("0.00");
                        row[6] = t.StorageName;
                        row[7] = t.LocalName;
                        row[8] = EnumHelper.GetEnumDesc<EInType>(t.InType);
                        row[9] = t.SupName;
                        row[10] = t.CreateUserName;
                        row[11] = t.CreateTime.To("yyyy-MM-dd");
                        row[12] = EnumHelper.GetEnumDesc<EAudite>(t.Status);
                        row[13] = t.AuditeUserName;
                        row[14] = t.AuditeTime.To("yyyy-MM-dd");

                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("入库管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("入库管理", "入库单", System.IO.Path.Combine(filePath, filename));
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
