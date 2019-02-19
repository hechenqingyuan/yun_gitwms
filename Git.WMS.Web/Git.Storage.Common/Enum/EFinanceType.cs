/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-13 13:55:24
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-13 13:55:24       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Git.Storage.Common.Enum
{
    public enum EFinanceType
    {
        [Description("应收")]
        BillReceivable=1,

        [Description("应付")]
        Payable=2,

        [Description("应收(退款)")]
        BillReceivableBack = 3,

        [Description("应付(退款)")]
        PayableBack = 4,
    }
}
