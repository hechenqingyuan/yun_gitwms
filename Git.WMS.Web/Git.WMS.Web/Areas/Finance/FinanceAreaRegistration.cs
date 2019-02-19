/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-25 9:04:04
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-25 9:04:04       情缘
*********************************************************************************/

using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Finance
{
    public class FinanceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Finance";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Finance_default",
                "Finance/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
