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
    public class PayController : MasterPage
    {
        /// <summary>
        /// 应付列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            string CateList = DropDownHelper.GetFinanceCate(string.Empty, this.CompanyID);
            ViewBag.CateList = CateList;
            return View();
        }

        /// <summary>
        /// 新增应付
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult AddRec()
        {
            string CompanyID = this.CompanyID;
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);

            FinanceBillEntity entity = null;
            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);
                string result = client.Execute(FinanceBillApiName.FinanceBillApiName_Single, dic);

                if (!result.IsEmpty())
                {
                    DataResult<FinanceBillEntity> dataResult = JsonConvert.DeserializeObject<DataResult<FinanceBillEntity>>(result);
                    entity = dataResult.Result;
                }
            }

            if (entity.IsNull())
            {
                entity = new FinanceBillEntity();
                entity.CreateTime = DateTime.Now;
                entity.CreateUser = this.LoginUser.UserNum;
                entity.CreateUserName = this.LoginUser.UserName;
                entity.LastTime = DateTime.Now.AddMonths(2);
            }
            entity.FromName = entity.FromName.IsEmpty() ? "公司" : entity.FromName;
            ViewBag.Entity = entity;

            string CateList = DropDownHelper.GetFinanceCate(entity.CateNum, this.CompanyID);
            ViewBag.CateList = CateList;

            return View();
        }

        /// <summary>
        /// 新增实付
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult AddPay()
        {
            string CompanyID = this.CompanyID;
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);
            string BillSnNum = WebUtil.GetQueryStringValue<string>("BillSnNum", string.Empty);

            FinancePayEntity entity = null;
            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);
                string result = client.Execute(FinancePayApiName.FinancePayApiName_Single, dic);

                if (!result.IsEmpty())
                {
                    DataResult<FinancePayEntity> dataResult = JsonConvert.DeserializeObject<DataResult<FinancePayEntity>>(result);
                    entity = dataResult.Result;
                }
            }

            if (entity.IsNull())
            {
                entity = new FinancePayEntity();
                entity.CreateTime = DateTime.Now;
                entity.CreateUser = this.LoginUser.UserNum;
                entity.CreateUserName = this.LoginUser.UserName;
                entity.PayTime = DateTime.Now;
            }
            ViewBag.PayTypeList = EnumHelper.GetOptions<EPayType>(entity.PayType);
            entity.BillSnNum = BillSnNum;
            ViewBag.Entity = entity;


            //加载应收数据项
            FinanceBillEntity billEnity = null;
            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", BillSnNum);
                string result = client.Execute(FinanceBillApiName.FinanceBillApiName_Single, dic);

                if (!result.IsEmpty())
                {
                    DataResult<FinanceBillEntity> dataResult = JsonConvert.DeserializeObject<DataResult<FinanceBillEntity>>(result);
                    billEnity = dataResult.Result;
                }
            }
            if (billEnity.IsNull())
            {
                billEnity = new FinanceBillEntity();
                billEnity.CreateTime = DateTime.Now;
                billEnity.CreateUser = this.LoginUser.UserNum;
                billEnity.CreateUserName = this.LoginUser.UserName;
                billEnity.LastTime = DateTime.Now.AddMonths(2);
            }
            ViewBag.BillEntity = billEnity;

            return View();
        }

        /// <summary>
        /// 流水账记录
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Record()
        {
            string CateList = DropDownHelper.GetFinanceCate(string.Empty, this.CompanyID);
            ViewBag.CateList = CateList;
            return View();
        }
    }
}
