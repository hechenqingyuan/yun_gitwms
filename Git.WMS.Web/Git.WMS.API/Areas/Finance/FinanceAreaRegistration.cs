/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-12 20:11:00
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-12 20:11:00       情缘
*********************************************************************************/

using System.Web.Mvc;

namespace Git.WMS.API.Areas.Finance
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
                "Api/Finance/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
