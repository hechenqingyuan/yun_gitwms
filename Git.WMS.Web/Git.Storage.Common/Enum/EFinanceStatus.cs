/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-13 11:38:42
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-13 11:38:42       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Git.Storage.Common.Enum
{
    public enum EFinanceStatus
    {
        [Description("待审核")]
        Wait=1,

        [Description("审核通过")]
        Pass=2,

        [Description("审核失败")]
        NotPass=3,

        [Description("取消")]
        Cancel=4,

        [Description("等待付款")]
        InProgress = 5,

        [Description("部分付款")]
        PayPart = 6,

        [Description("全部付款")]
        PayFull = 7,
    }
}
