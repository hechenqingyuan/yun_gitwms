using Git.Framework.Controller;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Report.Controllers
{
    public class WarnAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询库存预警清单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, true)]
        public ActionResult GetList()
        {
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);

            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);
            string SupNum = WebUtil.GetFormValue<string>("SupNum", string.Empty);

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            StorageNum = this.DefaultStorage.SnNum;

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("StorageNum", StorageNum);
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("BatchNum", BatchNum);
            dic.Add("SupNum", SupNum);

            string result = client.Execute(WarnApiName.WarnApiName_GetList, dic);

            return Content(result);
        }

    }
}
