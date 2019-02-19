/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-21 21:36:13
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-21 21:36:13       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Git.Storage.Common.Enum
{
    public enum EPurchaseStatus
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        [Description("创建订单")]
        CreateOrder = 1,

        /// <summary>
        /// 订单确认
        /// </summary>
        [Description("订单确认")]
        OrderConfirm = 2,

        /// <summary>
        /// 订单取消
        /// </summary>
        [Description("订单取消")]
        OrderCancel = 3,

        /// <summary>
        /// 采购中
        /// </summary>
        [Description("采购中")]
        InTheStock = 4,

        /// <summary>
        /// 部分入库
        /// </summary>
        [Description("部分入库")]
        PartialIn = 5,

        /// <summary>
        /// 全部入库
        /// </summary>
        [Description("全部入库")]
        AllIn = 6,

        /// <summary>
        /// 入库失败
        /// </summary>
        [Description("入库失败")]
        InFailure = 7
    }
}
