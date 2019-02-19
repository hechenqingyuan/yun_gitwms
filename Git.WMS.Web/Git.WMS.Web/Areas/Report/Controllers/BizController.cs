using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Report.Controllers
{
    public class BizController : MasterPage
    {
        /// <summary>
        /// 销售报表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult SaleReport()
        {
            return View();
        }

        /// <summary>
        /// 采购报表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult PurchaseReport()
        {
            return View();
        }

    }
}
