/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017-01-13 8:57:07
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017-01-13 8:57:07       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Git.Storage.Common.Enum
{
    public enum EProductPackage
    {
        [Description("单品")]
        Single=1,

        [Description("组合品")]
        Combination=2,

        [Description("包装品")]
        Pack=3
    }
}
