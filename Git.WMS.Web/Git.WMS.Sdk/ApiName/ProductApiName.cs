/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-29 21:24:39
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-29 21:24:39       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class ProductApiName
    {
        /// <summary>
        /// 新增产品
        /// </summary>
        public static string ProductApiName_Add = "/Api/Sys/Product/Add";

        /// <summary>
        /// 编辑产品
        /// </summary>
        public static string ProductApiName_Edit = "/Api/Sys/Product/Edit";

        /// <summary>
        /// 根据产品唯一编号批量删除产品
        /// </summary>
        public static string ProductApiName_Delete = "/Api/Sys/Product/Delete";

        /// <summary>
        /// 根据产品唯一编号删除产品
        /// </summary>
        public static string ProductApiName_DeleteSingle = "/Api/Sys/Product/DeleteSingle";

        /// <summary>
        /// 查询产品
        /// </summary>
        public static string ProductApiName_Single = "/Api/Sys/Product/Single";

        /// <summary>
        /// 产品列表
        /// </summary>
        public static string ProductApiName_GetList = "/Api/Sys/Product/GetList";

        /// <summary>
        /// 产品分页
        /// </summary>
        public static string ProductApiName_GetPage = "/Api/Sys/Product/GetPage";

        /// <summary>
        /// 根据产品条码扫描获取产品信息
        /// </summary>
        public static string ProductApiName_Scan = "/Api/Sys/Product/Scan";

        /// <summary>
        /// 根据关键搜索扫描产品
        /// </summary>
        public static string ProductApiName_GetSearch = "/Api/Sys/Product/GetSearch";

        /// <summary>
        /// 根据产品的唯一编号批量查找产品信息
        /// </summary>
        public static string ProductApiName_GetProductListBySn = "/Api/Sys/Product/GetProductListBySn";

        /// <summary>
        /// 根据产品的条码编号批量查询产品信息
        /// </summary>
        public static string ProductApiName_GetProductListByBarCode = "/Api/Sys/Product/GetProductListByBarCode";
    }
}
