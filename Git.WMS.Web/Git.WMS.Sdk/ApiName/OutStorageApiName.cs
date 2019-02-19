/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-04-27 16:27:46
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-04-27 16:27:46       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class OutStorageApiName
    {
        /// <summary>
        /// 创建出库单
        /// </summary>
        public static string OutStorageApiName_Add = "/Api/Order/OutStorage/Create";

        /// <summary>
        /// 编辑出库单
        /// </summary>
        public static string OutStorageApiName_Edit = "/Api/Order/OutStorage/Edit";

        /// <summary>
        /// 编辑出库单主体信息
        /// </summary>
        public static string OutStorageApiName_EditOrder = "/Api/Order/OutStorage/EditOrder";

        /// <summary>
        /// 编辑出库单详细信息
        /// </summary>
        public static string OutStorageApiName_EditDetail = "/Api/Order/OutStorage/EditDetail";

        /// <summary>
        /// 查询出库单表头
        /// </summary>
        public static string OutStorageApiName_GetOrder = "/Api/Order/OutStorage/GetOrder";

        /// <summary>
        /// 查询出库单详细
        /// </summary>
        public static string OutStorageApiName_GetDetail = "/Api/Order/OutStorage/GetDetail";

        /// <summary>
        /// 查询出库单分页列表
        /// </summary>
        public static string OutStorageApiName_GetOrderList = "/Api/Order/OutStorage/GetOrderList";

        /// <summary>
        /// 批量删除出库单
        /// </summary>
        public static string OutStorageApiName_Delete = "/Api/Order/OutStorage/Delete";

        /// <summary>
        /// 根据出库单唯一编号删除出库单
        /// </summary>
        public static string OutStorageApiName_DeleteSingle = "/Api/Order/OutStorage/DeleteSingle";

        /// <summary>
        /// 取消出库单
        /// </summary>
        public static string OutStorageApiName_Cancel = "/Api/Order/OutStorage/Cancel";

        /// <summary>
        /// 审核出库单
        /// </summary>
        public static string OutStorageApiName_Audite = "/Api/Order/OutStorage/Audite";

        /// <summary>
        /// 出库单详细分页列表
        /// </summary>
        public static string OutStorageApiName_GetDetailList = "/Api/Order/OutStorage/GetDetailList";

        /// <summary>
        /// 设置物流信息
        /// </summary>
        public static string OutStorageApiName_SetCarrier = "/Api/Order/OutStorage/SetCarrier";

        /// <summary>
        /// 设置打印参数数据
        /// </summary>
        public static string OutStorageApiName_Print = "/Api/Order/OutStorage/Print";

        /// <summary>
        /// 获得打印数据源
        /// </summary>
        public static string OutStorageApiName_GetPrintDataSource = "/Api/Order/OutStorage/GetPrintDataSource";

        /// <summary>
        /// 统计查询出库单行数
        /// </summary>
        public static string OutStorageApiName_GetCount = "/Api/Order/OutStorage/GetCount";

        /// <summary>
        /// 根据出库单编号查询出库单
        /// </summary>
        public static string OutStorageApiName_GetOrderByNum = "/Api/Order/OutStorage/GetOrderByNum";

    }
}
