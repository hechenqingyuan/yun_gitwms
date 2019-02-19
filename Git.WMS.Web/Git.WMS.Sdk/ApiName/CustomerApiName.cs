/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-21 20:43:36
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-21 20:43:36       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public class CustomerApiName
    {
        /// <summary>
        /// 新增角色
        /// </summary>
        public static string CustomerApiName_Add = "/Api/Sys/Customer/Add";

        /// <summary>
        /// 编辑角色
        /// </summary>
        public static string CustomerApiName_Edit = "/Api/Sys/Customer/Edit";

        /// <summary>
        /// 删除角色
        /// </summary>
        public static string CustomerApiName_Delete = "/Api/Sys/Customer/Delete";

        /// <summary>
        /// 查询角色
        /// </summary>
        public static string CustomerApiName_Single = "/Api/Sys/Customer/Single";

        /// <summary>
        /// 角色分页
        /// </summary>
        public static string CustomerApiName_GetPage = "/Api/Sys/Customer/GetPage";

        /// <summary>
        /// 查询客户地址
        /// </summary>
        public static string CustomerApiName_GetAddressList = "/Api/Sys/Customer/GetAddressList";

        /// <summary>
        /// 查询客户单个地址
        /// </summary>
        public static string CustomerApiName_GetAddress = "/Api/Sys/Customer/GetAddress";

        /// <summary>
        /// 删除客户地址
        /// </summary>
        public static string CustomerApiName_DelAddress = "/Api/Sys/Customer/DelAddress";

        /// <summary>
        /// 客户地址分页列表
        /// </summary>
        public static string CustomerApiName_GetAddressPage = "/Api/Sys/Customer/GetAddressListPage";

        /// <summary>
        /// 模糊查找客户信息
        /// </summary>
        public static string CustomerApiName_SearchCustomer = "/Api/Sys/Customer/SearchCustomer";
    }
}
