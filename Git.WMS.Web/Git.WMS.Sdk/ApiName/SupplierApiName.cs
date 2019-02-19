/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-17 7:22:03
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-17 7:22:03       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public class SupplierApiName
    {
        /// <summary>
        /// 新增供应商
        /// </summary>
        public static string SupplierApiName_Add = "/Api/Sys/Supplier/Add";

        /// <summary>
        /// 编辑供应商
        /// </summary>
        public static string SupplierApiName_Edit = "/Api/Sys/Supplier/Edit";

        /// <summary>
        /// 删除供应商
        /// </summary>
        public static string SupplierApiName_Delete = "/Api/Sys/Supplier/Delete";

        /// <summary>
        /// 查询供应商
        /// </summary>
        public static string SupplierApiName_Single = "/Api/Sys/Supplier/Single";

        /// <summary>
        /// 供应商列表
        /// </summary>
        public static string SupplierApiName_GetList = "/Api/Sys/Supplier/GetList";

        /// <summary>
        /// 供应商分页
        /// </summary>
        public static string SupplierApiName_GetPage = "/Api/Sys/Supplier/GetPage";

        /// <summary>
        /// 搜索供应商信息
        /// </summary>
        public static string SupplierApiName_SearchSupplier = "/Api/Sys/Supplier/SearchSupplier";
    }
}
