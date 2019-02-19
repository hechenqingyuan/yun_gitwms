/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-13 20:35:01
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-13 20:35:01       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Git.WMS.Web.Lib
{
    public class SessionKey
    {
        /// <summary>
        /// 登录用户
        /// </summary>
        public static string SESSION_LOGIN_ADMIN = "SESSION_LOGIN_ADMIN";

        /// <summary>
        /// 当前登录用户所在公司
        /// </summary>
        public static string SESSION_LOGIN_COMPANY = "SESSION_LOGIN_COMPANY";

        /// <summary>
        /// 默认选择的仓库
        /// </summary>
        public static string SESSION_DEFAULT_STORAGE = "SESSION_DEFAULT_STORAGE";

        /// <summary>
        /// 默认选择的库位
        /// </summary>
        public static string SESSION_DEFAULT_LOCATION = "SESSION_DEFAULT_LOCATION";

        /// <summary>
        /// 仓库列表
        /// </summary>
        public static string SESSION_STORAGE_LIST = "SESSION_STORAGE_LIST";

        /// <summary>
        /// 客户地址
        /// </summary>
        public static string SESSION_CUSTOMER_ADDRESS = "SESSION_CUSTOMER_ADDRESS";

        /// <summary>
        /// 入库单详细
        /// </summary>
        public static string SESSION_INSTORAGE_DETAIL = "SESSION_INSTORAGE_DETAIL";

        /// <summary>
        /// 出库单详细
        /// </summary>
        public static string SESSION_OUTSTORAGE_DETAIL = "SESSION_OUTSTORAGE_DETAIL";

        /// <summary>
        /// 移库单
        /// </summary>
        public static string SESSION_MOVE_DETAIL = "SESSION_MOVE_DETAIL";

        /// <summary>
        /// 报损单
        /// </summary>
        public static string SESSION_BAD_DETAIL = "SESSION_BAD_DETAIL";

        /// <summary>
        /// 调拨单
        /// </summary>
        public static string SESSION_ALLOCATE_DETAIL = "SESSION_ALLOCATE_DETAIL";

        /// <summary>
        /// 盘点单
        /// </summary>
        public static string SESSION_CHECK_DETAIL = "SESSION_CHECK_DETAIL";

        /// <summary>
        /// 报表详细参数
        /// </summary>
        public static string SESSION_REPORT_DETAIL = "SESSION_REPORT_DETAIL";

        /// <summary>
        /// 销售订单
        /// </summary>
        public static string SESSION_SALEORDER_DETAIL = "SESSION_SALEORDER_DETAIL";

        /// <summary>
        /// 采购订单
        /// </summary>
        public static string SESSION_PURCHASE_DETAIL = "SESSION_PURCHASE_DETAIL";

        /// <summary>
        /// 权限菜单
        /// </summary>
        public static string SESSION_MENU_RESOURCE = "SESSION_MENU_RESOURCE";

        /// <summary>
        /// 左侧菜单是否显示
        /// </summary>
        public static string SESSION_MENU_STATUS = "SESSION_MENU_STATUS";
    }
}