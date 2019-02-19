/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/3/21 16:09:25
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/3/21 16:09:25       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public class BalanceBookApiName
    {
        /// <summary>
        /// 查询库存期初期末分页列表
        /// </summary>
        public static string BalanceBookApiName_GetList = "/Api/Report/BalanceBook/GetList";

        /// <summary>
        /// 查询单行期初期末数据
        /// </summary>
        public static string BalanceBookApiName_GetSingle = "/Api/Report/BalanceBook/GetSingle";
    }
}
