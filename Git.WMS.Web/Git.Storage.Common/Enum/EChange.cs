/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2014-07-22 14:48:53
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2014-07-22 14:48:53       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Git.Storage.Common.Enum
{
    public enum EChange
    {
        [Description("入库")]
        In = 1,

        [Description("出库")]
        Out = 2,

        [Description("移库-入")]
        MoveOut = 3,

        [Description("移库-出")]
        MoveIn = -3,

        [Description("报损-入")]
        BadOut = 4,

        [Description("报损-出")]
        BadIn = -4,

        [Description("盘盈")]
        InventoryIncome = 5,

        [Description("盘亏")]
        InventoryLoss = -5,

        [Description("调拨-入")]
        AllocateIn = 7,

        [Description("调拨-出")]
        AllocateOut = -7,
    }
}
