using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Storage.Controllers
{
    public class StockAjaxController : AjaxPage
    {
        /// <summary>
        /// 获得所有库存的产品
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult GetList()
        {
            string CompanyID = this.CompanyID;
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum", string.Empty);
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            int LocalType = WebUtil.GetFormValue<int>("LocalType", 0);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("LocalType", LocalType.ToString());
            dic.Add("LocalNum", LocalNum);
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("BatchNum", BatchNum);
            dic.Add("StorageNum", this.DefaultStorageNum);


            string result = client.Execute(LocalProductApiName.LocalProductApiName_GetList, dic);
            return Content(result);
        }

        /// <summary>
        /// 可出库产品库存列表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult GetOutAbleList()
        {
            string CompanyID = this.CompanyID;
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum", string.Empty);
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            int LocalType = WebUtil.GetFormValue<int>("LocalType", 0);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("LocalType", LocalType.ToString());
            dic.Add("LocalNum", LocalNum);
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("BatchNum", BatchNum);
            dic.Add("StorageNum", this.DefaultStorageNum);


            string result = client.Execute(LocalProductApiName.LocalProductApiName_GetOutAbleList, dic);
            return Content(result);
        }

        /// <summary>
        /// 可报损的仓库库存列表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult GetBadAbleList()
        {
            string CompanyID = this.CompanyID;
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum", string.Empty);
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            int LocalType = WebUtil.GetFormValue<int>("LocalType", 0);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("LocalType", LocalType.ToString());
            dic.Add("LocalNum", LocalNum);
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("BatchNum", BatchNum);
            dic.Add("StorageNum", this.DefaultStorageNum);


            string result = client.Execute(LocalProductApiName.LocalProductApiName_GetBadAbleList, dic);
            return Content(result);
        }

        /// <summary>
        /// 查询所有正式库位产品总和
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult GetLocalProduct()
        {
            string CompanyID = this.CompanyID;
            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            int LocalType = WebUtil.GetFormValue<int>("LocalType", 0);

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("BatchNum", BatchNum);


            string result = client.Execute(LocalProductApiName.LocalProductApiName_GetLocalProduct, dic);
            return Content(result);
        }
    }
}
