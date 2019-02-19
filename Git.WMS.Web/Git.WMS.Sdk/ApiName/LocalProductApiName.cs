/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-07 10:17:10
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-07 10:17:10       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class LocalProductApiName
    {
        /// <summary>
        /// 查询库存信息
        /// </summary>
        public static string LocalProductApiName_GetList = "/Api/Storage/LocalProduct/GetList";

        /// <summary>
        /// 可出库的产品库存列表
        /// </summary>
        public static string LocalProductApiName_GetOutAbleList = "/Api/Storage/LocalProduct/GetOutAbleList";

        /// <summary>
        /// 可报损的产品库存列表
        /// </summary>
        public static string LocalProductApiName_GetBadAbleList = "/Api/Storage/LocalProduct/GetBadAbleList";

        /// <summary>
        /// 查询库存所有正式库位的产品总数
        /// </summary>
        public static string LocalProductApiName_GetLocalProduct = "/Api/Storage/LocalProduct/GetLocalProduct";
    }
}
