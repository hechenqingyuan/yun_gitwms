/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-07 9:57:17
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-07 9:57:17       情缘
*********************************************************************************/

using System.Web.Mvc;

namespace Git.WMS.API.Areas.Storage
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
                "Api/Storage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
