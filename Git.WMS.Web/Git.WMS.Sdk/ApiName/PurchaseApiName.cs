/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-06 23:06:51
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-06 23:06:51       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class PurchaseApiName
    {
        /// <summary>
        /// 创建采购订单
        /// </summary>
        public static string PurchaseApiName_Add = "/Api/Biz/Purchase/Create";

        /// <summary>
        /// 编辑采购订单
        /// </summary>
        public static string PurchaseApiName_Edit = "/Api/Biz/Purchase/Edit";

        /// <summary>
        /// 查询采购订单表头
        /// </summary>
        public static string PurchaseApiName_GetOrder = "/Api/Biz/Purchase/GetOrder";

        /// <summary>
        /// 查询采购订单详细
        /// </summary>
        public static string PurchaseApiName_GetDetail = "/Api/Biz/Purchase/GetDetail";

        /// <summary>
        /// 查询采购订单分页列表
        /// </summary>
        public static string PurchaseApiName_GetOrderList = "/Api/Biz/Purchase/GetOrderList";

        /// <summary>
        /// 删除采购订单
        /// </summary>
        public static string PurchaseApiName_Delete = "/Api/Biz/Purchase/Delete";

        /// <summary>
        /// 取消采购订单
        /// </summary>
        public static string PurchaseApiName_Cancel = "/Api/Biz/Purchase/Cancel";

        /// <summary>
        /// 审核采购订单
        /// </summary>
        public static string PurchaseApiName_Audite = "/Api/Biz/Purchase/Audite";

        /// <summary>
        /// 采购订单详细查询分页
        /// </summary>
        public static string PurchaseApiName_GetDetailList = "/Api/Biz/Purchase/GetDetailList";

        /// <summary>
        /// 采购订单生成财务记录
        /// </summary>
        public static string PurchaseApiName_ToFiance = "/Api/Biz/Purchase/ToFiance";

        /// <summary>
        /// 采购入库
        /// </summary>
        public static string PurchaseApiName_ToInStorage = "/Api/Biz/Purchase/ToInStorage";

        /// <summary>
        /// 采购退货
        /// </summary>
        public static string PurchaseApiName_ToReturn = "/Api/Biz/Purchase/ToReturn";
    }
}
