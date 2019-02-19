/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2015/9/5 20:58:16
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2015/9/5 20:58:16       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Git.Storage.Common.Enum
{
    /// <summary>
    /// 报表打印模板的类型
    /// 1-20  仓库作业单据
    /// 21-40 业务模块单据处理
    /// 41-60 基础资料模板
    /// </summary>
    public enum EReportType
    {
        [Description("入库单")]
        InBill=1,

        [Description("出库单")]
        OutBill = 2,

        [Description("盘点单")]
        CheckBill = 3,

        [Description("调拨")]
        AllocateBill = 4,

        [Description("报损")]
        BadBill = 5,




        [Description("工号")]
        User=41,

        [Description("产品")]
        Product = 42,

        [Description("仓库")]
        Storage = 43,

        [Description("库位")]
        Location = 44,



        [Description("报表")]
        Report=100
    }
}
