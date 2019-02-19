using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Common.Excel;
using Git.Storage.Entity.Bad;
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
    public class BadManagerAjaxController : AjaxPage
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
            int BadType = WebUtil.GetFormValue<int>("BadType", 0);
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder", string.Empty);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime", string.Empty);
            string EndTime = WebUtil.GetFormValue<string>("EndTime", string.Empty);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("OrderNum", OrderNum);
            dic.Add("BadType", BadType.ToString());
            dic.Add("Status", Status.ToString());
            dic.Add("ContractOrder", ContractOrder);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);
            dic.Add("StorageNum", this.DefaultStorageNum);

            string result = client.Execute(BadApiName.BadApiName_GetDetailPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除报损单
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

            string result = client.Execute(BadApiName.BadApiName_Delete, dic);
            return Content(result);
        }

        /// <summary>
        /// 取消报损单
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

            string result = client.Execute(BadApiName.BadApiName_Cancel, dic);
            return Content(result);
        }

        /// <summary>
        /// 审核报损单
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

            string result = client.Execute(BadApiName.BadApiName_Audite, dic);
            return Content(result);
        }

        /// <summary>
        /// 获得报损单详细
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

            string result = client.Execute(BadApiName.BadApiName_GetDetail, dic);

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
            int BadType = WebUtil.GetFormValue<int>("BadType", 0);
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            string ContractOrder = WebUtil.GetFormValue<string>("ContractOrder", string.Empty);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime", string.Empty);
            string EndTime = WebUtil.GetFormValue<string>("EndTime", string.Empty);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("OrderNum", OrderNum);
            dic.Add("BadType", BadType.ToString());
            dic.Add("Status", Status.ToString());
            dic.Add("ContractOrder", ContractOrder);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);
            dic.Add("StorageNum", this.DefaultStorageNum);

            string result = client.Execute(BadApiName.BadApiName_GetDetailPage, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<BadReportDetailEntity> dataResult = JsonHelper.DeserializeObject<DataListResult<BadReportDetailEntity>>(result);
                List<BadReportDetailEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("报损单号"));
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("批次"));
                    dt.Columns.Add(new DataColumn("规格"));
                    dt.Columns.Add(new DataColumn("数量"));
                    dt.Columns.Add(new DataColumn("仓库"));
                    dt.Columns.Add(new DataColumn("库位"));
                    dt.Columns.Add(new DataColumn("报损库位"));
                    dt.Columns.Add(new DataColumn("报损类型"));
                    dt.Columns.Add(new DataColumn("制单人"));
                    dt.Columns.Add(new DataColumn("制单时间"));
                    dt.Columns.Add(new DataColumn("状态"));
                    dt.Columns.Add(new DataColumn("审核人"));
                    dt.Columns.Add(new DataColumn("审核时间"));

                    foreach (BadReportDetailEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.OrderNum;
                        row[1] = t.ProductName;
                        row[2] = t.BarCode ;
                        row[3] = t.BatchNum;
                        row[4] = t.Size;
                        row[5] = t.Num;
                        row[6] = t.StorageName;
                        row[7] = t.FromLocalName;
                        row[8] = t.ToLocalName;
                        row[9] = EnumHelper.GetEnumDesc<EBadType>(t.BadType);
                        row[10] = t.CreateUserName;
                        row[11] = t.CreateTime.To("yyyy-MM-dd");
                        row[12] = EnumHelper.GetEnumDesc<EAudite>(t.Status);
                        row[13] = t.AuditUserName;
                        row[14] = t.AuditeTime.To("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("报损管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("报损管理", "报损单", System.IO.Path.Combine(filePath, filename));
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
