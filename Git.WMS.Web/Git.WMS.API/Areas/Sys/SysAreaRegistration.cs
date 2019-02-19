/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-04 10:32:52
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-04 10:32:52       情缘
*********************************************************************************/

using System.Web.Mvc;

namespace Git.WMS.API.Areas.Sys
{
    public class SysAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sys";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Sys_default",
                "Api/Sys/{controller}/{action}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
