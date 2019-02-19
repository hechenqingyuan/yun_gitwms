using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Biz;
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

namespace Git.WMS.Web.Areas.Biz.Controllers
{
    public class PurchaseReturnController : MasterPage
    {
        /// <summary>
        /// 采购退货单列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 采购退货单详细
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Detail()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            PurchaseReturnEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(PurchaseReturnApiName.PurchaseReturnApiName_GetOrder, dic);
                DataResult<PurchaseReturnEntity> dataResult = JsonConvert.DeserializeObject<DataResult<PurchaseReturnEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new PurchaseReturnEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }

    }
}
