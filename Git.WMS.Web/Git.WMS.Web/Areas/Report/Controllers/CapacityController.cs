using Git.Storage.Common.Enum;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Report.Controllers
{
    public class CapacityController : MasterPage
    {
        /// <summary>
        /// 库存容量列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            return View();
        }
    }
}
