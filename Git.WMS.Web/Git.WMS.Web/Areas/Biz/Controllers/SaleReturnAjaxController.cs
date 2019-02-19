using Git.WMS.Web.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Biz.Controllers
{
    public class SaleReturnAjaxController : AjaxPage
    {
        //
        // GET: /Biz/SaleReturnAjax/

        public ActionResult Index()
        {
            return View();
        }

    }
}
