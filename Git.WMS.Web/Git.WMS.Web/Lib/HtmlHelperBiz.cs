/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-07-13 22:20:39
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-07-13 22:20:39       情缘
*********************************************************************************/

using Git.Storage.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Lib
{
    public static class HtmlHelperBiz
    {
        public static MvcHtmlString RoleCheckBox(this HtmlHelper html, List<SysResourceEntity> listSource, string ResNum)
        {
            if (listSource.Exists(a => a.ResNum == ResNum))
            {
                return MvcHtmlString.Create(string.Format("<input type=\"checkbox\" class=\"checkboxes\" name=\"{0}\" value=\"{1}\" checked=\"checked\">", ResNum, ResNum));
            }
            else
            {
                return MvcHtmlString.Create(string.Format("<input type=\"checkbox\" class=\"checkboxes\" name=\"{0}\" value=\"{1}\">", ResNum, ResNum));
            }
        }
    }
}