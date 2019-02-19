/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/3/16 11:19:55
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/3/16 11:19:55       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public class SaleReturnApiName
    {
        /// <summary>
        /// 创建销售退货订单
        /// </summary>
        public static string SaleReturnApiName_Add = "/Api/Biz/SaleReturn/Create";

        /// <summary>
        /// 编辑销售退货订单
        /// </summary>
        public static string SaleReturnApiName_Edit = "/Api/Biz/SaleReturn/Edit";

        /// <summary>
        /// 查询销售退货订单表头
        /// </summary>
        public static string SaleReturnApiName_GetOrder = "/Api/Biz/SaleReturn/GetOrder";

        /// <summary>
        /// 查询销售退货订单详细
        /// </summary>
        public static string SaleReturnApiName_GetDetail = "/Api/Biz/SaleReturn/GetDetail";

        /// <summary>
        /// 查询销售退货订单分页列表
        /// </summary>
        public static string SaleReturnApiName_GetOrderList = "/Api/Biz/SaleReturn/GetOrderList";

        /// <summary>
        /// 删除销售退货订单
        /// </summary>
        public static string SaleReturnApiName_Delete = "/Api/Biz/SaleReturn/Delete";

        /// <summary>
        /// 取消销售退货订单
        /// </summary>
        public static string SaleReturnApiName_Cancel = "/Api/Biz/SaleReturn/Cancel";

        /// <summary>
        /// 审核销售退货订单
        /// </summary>
        public static string SaleReturnApiName_Audite = "/Api/Biz/SaleReturn/Audite";

        /// <summary>
        /// 销售退货订单详细查询分页
        /// </summary>
        public static string SaleReturnApiName_GetDetailList = "/Api/Biz/SaleReturn/GetDetailList";
    }
}
