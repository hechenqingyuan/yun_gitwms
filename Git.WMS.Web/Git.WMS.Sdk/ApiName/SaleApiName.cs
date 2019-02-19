/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-06 9:16:46
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-06 9:16:46       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class SaleApiName
    {
        /// <summary>
        /// 创建销售订单
        /// </summary>
        public static string SaleApiName_Add = "/Api/Biz/Sale/Create";

        /// <summary>
        /// 编辑销售订单
        /// </summary>
        public static string SaleApiName_Edit = "/Api/Biz/Sale/Edit";

        /// <summary>
        /// 查询销售订单表头
        /// </summary>
        public static string SaleApiName_GetOrder = "/Api/Biz/Sale/GetOrder";

        /// <summary>
        /// 查询销售订单详细
        /// </summary>
        public static string SaleApiName_GetDetail = "/Api/Biz/Sale/GetDetail";

        /// <summary>
        /// 查询销售订单分页列表
        /// </summary>
        public static string SaleApiName_GetOrderList = "/Api/Biz/Sale/GetOrderList";

        /// <summary>
        /// 删除销售订单
        /// </summary>
        public static string SaleApiName_Delete = "/Api/Biz/Sale/Delete";

        /// <summary>
        /// 取消销售订单
        /// </summary>
        public static string SaleApiName_Cancel = "/Api/Biz/Sale/Cancel";

        /// <summary>
        /// 审核销售订单
        /// </summary>
        public static string SaleApiName_Audite = "/Api/Biz/Sale/Audite";

        /// <summary>
        /// 销售单详细查询分页
        /// </summary>
        public static string SaleApiName_GetDetailList = "/Api/Biz/Sale/GetDetailList";

        /// <summary>
        /// 销售订单财务入账
        /// </summary>
        public static string SaleApiName_ToFiance = "/Api/Biz/Sale/ToFiance";

        /// <summary>
        /// 销售订单出库
        /// </summary>
        public static string SaleApiName_ToOutStorage = "/Api/Biz/Sale/ToOutStorage";

        /// <summary>
        /// 销售退货单
        /// </summary>
        public static string SaleApiName_ToReturn = "/Api/Biz/Sale/ToReturn";
    }
}
