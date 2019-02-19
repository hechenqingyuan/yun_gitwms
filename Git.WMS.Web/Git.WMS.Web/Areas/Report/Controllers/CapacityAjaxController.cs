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
    public class CapacityAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询库存容量
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, true)]
        public ActionResult GetList()
        {
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);

            string LocalName = WebUtil.GetFormValue<string>("LocalName", string.Empty);
            int LocalType = WebUtil.GetFormValue<int>("LocalType", 0);
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            StorageNum = this.DefaultStorage.SnNum;
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("StorageNum", StorageNum);
            dic.Add("LocalName", LocalName);
            dic.Add("LocalType", LocalType.ToString());

            string result = client.Execute(CapacityApiName.CapacityApiName_GetList, dic);

            return Content(result);
        }

    }
}
