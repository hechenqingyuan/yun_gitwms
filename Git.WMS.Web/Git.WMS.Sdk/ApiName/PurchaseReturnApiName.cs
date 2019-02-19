/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/3/16 11:55:52
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/3/16 11:55:52       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public class PurchaseReturnApiName
    {
        /// <summary>
        /// 创建采购退货单
        /// </summary>
        public static string PurchaseReturnApiName_Add = "/Api/Biz/PurchaseReturn/Create";

        /// <summary>
        /// 编辑采购退货单
        /// </summary>
        public static string PurchaseReturnApiName_Edit = "/Api/Biz/PurchaseReturn/Edit";

        /// <summary>
        /// 查询采购退货单表头
        /// </summary>
        public static string PurchaseReturnApiName_GetOrder = "/Api/Biz/PurchaseReturn/GetOrder";

        /// <summary>
        /// 查询采购退货单详细
        /// </summary>
        public static string PurchaseReturnApiName_GetDetail = "/Api/Biz/PurchaseReturn/GetDetail";

        /// <summary>
        /// 查询采购退货单分页列表
        /// </summary>
        public static string PurchaseReturnApiName_GetOrderList = "/Api/Biz/PurchaseReturn/GetOrderList";

        /// <summary>
        /// 删除采购退货单
        /// </summary>
        public static string PurchaseReturnApiName_Delete = "/Api/Biz/PurchaseReturn/Delete";

        /// <summary>
        /// 取消采购退货单
        /// </summary>
        public static string PurchaseReturnApiName_Cancel = "/Api/Biz/PurchaseReturn/Cancel";

        /// <summary>
        /// 审核采购退货单
        /// </summary>
        public static string PurchaseReturnApiName_Audite = "/Api/Biz/PurchaseReturn/Audite";

        /// <summary>
        /// 采购退货单详细查询分页
        /// </summary>
        public static string PurchaseReturnApiName_GetDetailList = "/Api/Biz/PurchaseReturn/GetDetailList";
    }
}
