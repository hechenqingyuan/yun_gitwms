/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/3/15 20:24:10
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/3/15 20:24:10       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Git.Storage.Common.Enum
{
    public enum ESaleReturnStatus
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
        /// 已入库
        /// </summary>
        [Description("已入库")]
        InStorage = 4,
    }
}
