/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-04-06 14:02:11
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-04-06 14:02:11       情缘
*********************************************************************************/

using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Order
{
    public class OrderAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Order";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Order_default",
                "Order/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
