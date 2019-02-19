/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-21 18:14:54
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-21 18:14:54       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Git.Storage.Common.Enum
{
    public enum EPurchaseType
    {
        /// <summary>
        /// 实际订单
        /// </summary>
        [Description("实际订单")]
        Really = 1,

        /// <summary>
        /// 虚拟订单
        /// </summary>
        [Description("虚拟订单")]
        Invented = 2
    }
}
