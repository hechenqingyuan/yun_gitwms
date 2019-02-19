/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-11 10:51:01
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-11 10:51:01       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class AllocateApiName
    {
        /// <summary>
        /// 创建调拨单
        /// </summary>
        public static string AllocateApiName_Add = "/Api/Order/Allocate/Create";

        /// <summary>
        /// 编辑调拨单
        /// </summary>
        public static string AllocateApiName_Edit = "/Api/Order/Allocate/Edit";

        /// <summary>
        /// 编辑调拨单主体
        /// </summary>
        public static string AllocateApiName_EditOrder = "/Api/Order/Allocate/EditOrder";

        /// <summary>
        /// 编辑调拨单详细
        /// </summary>
        public static string AllocateApiName_EditDetail = "/Api/Order/Allocate/EditDetail";

        /// <summary>
        /// 查询调拨单表头
        /// </summary>
        public static string AllocateApiName_GetOrder = "/Api/Order/Allocate/GetOrder";

        /// <summary>
        /// 查询调拨单详细
        /// </summary>
        public static string AllocateApiName_GetDetail = "/Api/Order/Allocate/GetDetail";

        /// <summary>
        /// 查询调拨单分页列表
        /// </summary>
        public static string AllocateApiName_GetOrderList = "/Api/Order/Allocate/GetOrderList";

        /// <summary>
        /// 删除调拨单
        /// </summary>
        public static string AllocateApiName_Delete = "/Api/Order/Allocate/Delete";

        /// <summary>
        /// 根据调拨单唯一编号删除调拨单
        /// </summary>
        public static string AllocateApiName_DeleteSingle = "/Api/Order/Allocate/DeleteSingle";

        /// <summary>
        /// 取消调拨单
        /// </summary>
        public static string AllocateApiName_Cancel = "/Api/Order/Allocate/Cancel";

        /// <summary>
        /// 审核调拨单
        /// </summary>
        public static string AllocateApiName_Audite = "/Api/Order/Allocate/Audite";

        /// <summary>
        /// 查询调拨单详细分页
        /// </summary>
        public static string AllocateApiName_GetDetailPage = "/Api/Order/Allocate/GetDetailPage";

        /// <summary>
        /// 根据条件查询调拨单数据行
        /// </summary>
        public static string AllocateApiName_GetCount = "/Api/Order/Allocate/GetCount";

        /// <summary>
        /// 打印设置
        /// </summary>
        public static string AllocateApiName_Print = "/Api/Order/Allocate/Print";

        /// <summary>
        /// 获得打印的数据源
        /// </summary>
        public static string AllocateApiName_GetPrint = "/Api/Order/Allocate/GetPrint";

        /// <summary>
        /// 根据调拨单号查询调拨单
        /// </summary>
        public static string AllocateApiName_GetOrderByNum = "/Api/Order/Allocate/GetOrderByNum";
    }
}
