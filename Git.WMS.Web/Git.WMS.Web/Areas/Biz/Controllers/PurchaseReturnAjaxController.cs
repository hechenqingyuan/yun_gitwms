using Git.WMS.Web.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Biz.Controllers
{
    public class PurchaseReturnAjaxController : AjaxPage
    {
        //
        // GET: /Biz/PurchaseReturnAjax/

        public ActionResult Index()
        {
            return View();
        }

    }
}
