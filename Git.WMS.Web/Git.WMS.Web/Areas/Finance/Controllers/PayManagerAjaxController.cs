using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Common.Excel;
using Git.Storage.Entity.Finance;
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

namespace Git.WMS.Web.Areas.Finance.Controllers
{
    public class PayManagerAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询应收分页列表
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

            string PayNum = WebUtil.GetFormValue<string>("PayNum", string.Empty);
            string CateNum = WebUtil.GetFormValue<string>("CateNum", string.Empty);
            int BillType = (int)EFinanceType.Payable;
            string FromName = WebUtil.GetFormValue<string>("FromName", string.Empty);
            string ToName = WebUtil.GetFormValue<string>("ToName", string.Empty);
            string Title = WebUtil.GetFormValue<string>("Title", string.Empty);
            string ContractNum = WebUtil.GetFormValue<string>("ContractNum", string.Empty);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime", string.Empty);
            string EndTime = WebUtil.GetFormValue<string>("EndTime", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("BillNum", PayNum);
            dic.Add("CateNum", CateNum);
            dic.Add("BillType", BillType.ToString());
            dic.Add("Status", Status.ToString());
            dic.Add("FromName", FromName);
            dic.Add("ToName", ToName);
            dic.Add("Title", Title);
            dic.Add("ContractNum", ContractNum);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);

            string result = client.Execute(FinanceBillApiName.FinanceBillApiName_GetPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除应收
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
            string result = client.Execute(FinanceBillApiName.FinanceBillApiName_Delete, dic);
            return Content(result);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Audite()
        {
            string CompanyID = this.CompanyID;
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            int Status = WebUtil.GetFormValue<int>("Status");

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);
            dic.Add("Status", Status.ToString());

            string result = client.Execute(FinanceBillApiName.FinanceBillApiName_Audite, dic);
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

            string BillNum = WebUtil.GetFormValue<string>("BillNum", string.Empty);
            string CateNum = WebUtil.GetFormValue<string>("CateNum", string.Empty);
            int BillType = (int)EFinanceType.Payable;
            string FromName = WebUtil.GetFormValue<string>("FromName", string.Empty);
            string ToName = WebUtil.GetFormValue<string>("ToName", string.Empty);
            string Title = WebUtil.GetFormValue<string>("Title", string.Empty);
            string ContractNum = WebUtil.GetFormValue<string>("ContractNum", string.Empty);
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime", string.Empty);
            string EndTime = WebUtil.GetFormValue<string>("EndTime", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status", 0);
            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("BillNum", BillNum);
            dic.Add("CateNum", CateNum);
            dic.Add("BillType", BillType.ToString());
            dic.Add("Status", Status.ToString());
            dic.Add("FromName", FromName);
            dic.Add("ToName", ToName);
            dic.Add("Title", Title);
            dic.Add("ContractNum", ContractNum);
            dic.Add("BeginTime", BeginTime);
            dic.Add("EndTime", EndTime);

            string result = client.Execute(FinanceBillApiName.FinanceBillApiName_GetPage, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<FinanceBillEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<FinanceBillEntity>>(result);
                List<FinanceBillEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("流水号"));
                    dt.Columns.Add(new DataColumn("名称"));
                    dt.Columns.Add(new DataColumn("分类"));
                    dt.Columns.Add(new DataColumn("收款方"));
                    dt.Columns.Add(new DataColumn("应付金额"));
                    dt.Columns.Add(new DataColumn("实付金额"));
                    dt.Columns.Add(new DataColumn("剩余金额"));
                    dt.Columns.Add(new DataColumn("状态"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    foreach (FinanceBillEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.BillNum;
                        row[1] = t.Title;
                        row[2] = t.CateName;
                        row[3] = t.ToName;
                        row[4] = t.Amount.ToString("0.00");
                        row[5] = t.RealPayAmount.ToString("0.00");
                        row[6] = t.LeavAmount.ToString("0.00");
                        row[7] = EnumHelper.GetEnumDesc<EFinanceStatus>(t.Status);
                        row[8] = t.CreateTime.To("yyyy-MM-dd");
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("应付管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("应付管理", "应付管理", System.IO.Path.Combine(filePath, filename));
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
