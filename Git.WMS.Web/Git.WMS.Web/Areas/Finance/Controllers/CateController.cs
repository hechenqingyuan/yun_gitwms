using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
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
    public class CateController : MasterPage
    {
        /// <summary>
        /// 新增财务类别
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);
            string CompanyID = this.CompanyID;

            FinanceCateEntity entity = null;
            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);
                string result = client.Execute(FinanceCateApiName.FinanceCateApiName_Single, dic);

                if (!result.IsEmpty())
                {
                    DataResult<FinanceCateEntity> dataResult = JsonConvert.DeserializeObject<DataResult<FinanceCateEntity>>(result);
                    entity = dataResult.Result;
                }
            }
            entity = entity.IsNull() ? new FinanceCateEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }

        /// <summary>
        /// 财务类别列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            return View();
        }
    }
}
