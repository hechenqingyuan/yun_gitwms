using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Common;
using Newtonsoft.Json;

namespace Git.WMS.Web.Areas.Report.Controllers
{
    public class StoreController : MasterPage
    {
        /// <summary>
        /// 库存清单查询
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Location()
        {
            ViewBag.StorageNum = DropDownHelper.GetStorage(string.Empty,this.CompanyID);
            return View();
        }

        /// <summary>
        /// 可出库存清单查询
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult SaleLocation()
        {
            ViewBag.StorageNum = DropDownHelper.GetStorage(string.Empty, this.CompanyID);
            return View();
        }

        /// <summary>
        /// 库存期初期末
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult BalanceBook()
        {
            ViewBag.StorageNum = DropDownHelper.GetStorage(string.Empty, this.CompanyID);
            return View();
        }

        /// <summary>
        /// 仓库台账
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult InventoryBook()
        {
            ViewBag.StorageNum = DropDownHelper.GetStorage(string.Empty, this.CompanyID);
            return View();
        }

        /// <summary>
        /// 入库报表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult InReport()
        {
            return View();
        }

        /// <summary>
        /// 出库报备
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult OutReport()
        {
            return View();
        }
    }
}
