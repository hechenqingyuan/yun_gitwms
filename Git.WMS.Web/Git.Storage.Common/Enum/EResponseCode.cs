/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-07 8:49:38
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-07 8:49:38       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Git.Storage.Common.Enum
{
    public enum EResponseCode
    {
        [Description("成功")]
        Success=1,

        [Description("没有权限")]
        NoPermission=2,

        [Description("异常")]
        Exception=3,

        [Description("未登录")]
        NotLogin=4
    }
}
