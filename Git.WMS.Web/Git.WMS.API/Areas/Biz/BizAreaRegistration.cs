/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-02 11:18:37
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-02 11:18:37       情缘
*********************************************************************************/

using System.Web.Mvc;

namespace Git.WMS.API.Areas.Biz
{
    public class BizAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Biz";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Biz_default",
                "Api/Biz/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
