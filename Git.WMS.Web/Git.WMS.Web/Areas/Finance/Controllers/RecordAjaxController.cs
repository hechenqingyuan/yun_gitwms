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
    public class RecordAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询财务记账分页列表
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

            string result = client.Execute(FinancePayApiName.FinancePayApiName_GetPage, dic);
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

            string result = client.Execute(FinancePayApiName.FinancePayApiName_GetPage, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<FinancePayEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<FinancePayEntity>>(result);
                List<FinancePayEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("流水号"));
                    dt.Columns.Add(new DataColumn("名称"));
                    dt.Columns.Add(new DataColumn("分类"));
                    dt.Columns.Add(new DataColumn("收款方/付款方"));
                    dt.Columns.Add(new DataColumn("类型"));
                    dt.Columns.Add(new DataColumn("付款金额"));
                    dt.Columns.Add(new DataColumn("付款时间"));
                    dt.Columns.Add(new DataColumn("付款方式"));
                    dt.Columns.Add(new DataColumn("付款机构"));
                    foreach (FinancePayEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.PayNum;
                        row[1] = t.Title;
                        row[2] = t.CateName;
                        row[3] = t.SourceObject;
                        row[4] = EnumHelper.GetEnumDesc<EFinanceType>(t.BillType);
                        row[5] = t.Amount.ToString("0.00");
                        row[6] = t.PayTime.ToString("yyyy-MM-dd HH:mm:ss");
                        row[7] = EnumHelper.GetEnumDesc<EPayType>(t.PayType);
                        row[8] = t.BankName;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("财务记录{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("财务记录", "财务记录", System.IO.Path.Combine(filePath, filename));
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
