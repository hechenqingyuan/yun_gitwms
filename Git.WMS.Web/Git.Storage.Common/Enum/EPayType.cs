/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-31 22:52:23
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-31 22:52:23       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Git.Storage.Common.Enum
{

    public enum EPayType
    {
        [Description("支付宝")]
        AliPay=1,

        [Description("网银")]
        OnlineBank = 2,

        [Description("汇款")]
        HuiKuan = 3,
    }
}
