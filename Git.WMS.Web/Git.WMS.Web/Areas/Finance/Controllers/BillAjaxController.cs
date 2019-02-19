using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Finance;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Finance.Controllers
{
    public class BillAjaxController : AjaxPage
    {
        /// <summary>
        /// 新增编辑应收
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult AddRec()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string BillNum = WebUtil.GetFormValue<string>("BillNum");
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string CateName = WebUtil.GetFormValue<string>("CateName");
            int BillType = (int)EFinanceType.BillReceivable;
            string FromNum = WebUtil.GetFormValue<string>("FromNum");
            string FromName = WebUtil.GetFormValue<string>("FromName");
            string ToNum = WebUtil.GetFormValue<string>("ToNum");
            string ToName = WebUtil.GetFormValue<string>("ToName");
            double Amount = WebUtil.GetFormValue<double>("Amount");
            int PrePayCount = WebUtil.GetFormValue<int>("PrePayCount");
            string PrePayRate = WebUtil.GetFormValue<string>("PrePayRate");
            int RealPayCount = WebUtil.GetFormValue<int>("RealPayCount");
            DateTime LastTime = WebUtil.GetFormValue<DateTime>("LastTime");
            string Title = WebUtil.GetFormValue<string>("Title");
            string ContractSn = WebUtil.GetFormValue<string>("ContractSn");
            string ContractNum = WebUtil.GetFormValue<string>("ContractNum");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            
            string CompanyID = this.CompanyID;

            FinanceBillEntity entity = new FinanceBillEntity();
            entity.SnNum = SnNum;
            entity.BillNum = BillNum;
            entity.CateNum = CateNum;
            entity.CateName = CateName;
            entity.BillType = BillType;
            entity.FromNum = FromNum;
            entity.FromName = FromName;
            entity.ToNum = ToNum;
            entity.ToName = ToName;
            entity.Amount = Amount;
            entity.PrePayCount = PrePayCount;
            entity.PrePayRate = PrePayRate;
            entity.RealPayCount = RealPayCount;
            entity.RealPayAmount = 0;
            entity.LastTime = LastTime;
            entity.Title = Title;
            entity.ContractNum = ContractNum;
            entity.ContractSn = ContractSn;
            entity.CreateUser = this.LoginUser.UserNum; ;
            entity.Remark = Remark;
            entity.CompanyID = this.CompanyID;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Entity", JsonConvert.SerializeObject(entity));
            dic.Add("CompanyID", CompanyID);

            ITopClient client = new TopClientDefault();
            string ApiName = FinanceBillApiName.FinanceBillApiName_Add;
            if (!SnNum.IsEmpty())
            {
                ApiName = FinanceBillApiName.FinanceBillApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增编辑实收
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult AddPay()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string BillNum = WebUtil.GetFormValue<string>("BillNum");
            string BillSnNum = WebUtil.GetFormValue<string>("BillSnNum");
            int PayType = WebUtil.GetFormValue<int>("PayType");
            string BankName = WebUtil.GetFormValue<string>("BankName");
            double Amount = WebUtil.GetFormValue<double>("Amount");
            DateTime PayTime = WebUtil.GetFormValue<DateTime>("PayTime",DateTime.Now);

            FinancePayEntity entity = new FinancePayEntity();
            entity.SnNum = SnNum;
            entity.BillSnNum = BillSnNum;
            entity.PayType = PayType;
            entity.BankName = BankName;
            entity.Amount = Amount;
            entity.PayTime = PayTime;
            entity.CompanyID = this.CompanyID;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", this.CompanyID);
            dic.Add("Entity", JsonConvert.SerializeObject(entity));

            ITopClient client = new TopClientDefault();
            string ApiName = FinancePayApiName.FinancePayApiName_Add;
            if (!SnNum.IsEmpty())
            {
                ApiName = FinancePayApiName.FinancePayApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }
    }
}
