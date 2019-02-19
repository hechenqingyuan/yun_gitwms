/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-21 21:03:53
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-21 21:03:53       情缘
*********************************************************************************/

using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Storage
{
    public class StorageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Storage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Storage_default",
                "Storage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
