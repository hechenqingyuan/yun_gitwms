using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Storage.Controllers
{
    public class StockController : MasterPage
    {
        [LoginFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}
