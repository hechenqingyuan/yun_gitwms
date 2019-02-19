/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-27 9:35:25
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-27 9:35:25       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class ProductCategoryApiName
    {
        /// <summary>
        /// 新增产品类别
        /// </summary>
        public static string ProductCategoryApiName_Add = "/Api/Sys/ProductCategory/Add";

        /// <summary>
        /// 编辑产品类别
        /// </summary>
        public static string ProductCategoryApiName_Edit = "/Api/Sys/ProductCategory/Edit";

        /// <summary>
        /// 删除产品类别
        /// </summary>
        public static string ProductCategoryApiName_Delete = "/Api/Sys/ProductCategory/Delete";

        /// <summary>
        /// 查询产品类别
        /// </summary>
        public static string ProductCategoryApiName_Single = "/Api/Sys/ProductCategory/Single";

        /// <summary>
        /// 产品类别列表
        /// </summary>
        public static string ProductCategoryApiName_GetList = "/Api/Sys/ProductCategory/GetList";

        /// <summary>
        /// 产品类别分页
        /// </summary>
        public static string ProductCategoryApiName_GetPage = "/Api/Sys/ProductCategory/GetPage";
        /// <summary>
        /// 查询产品分类的所有子类
        /// </summary>
        public static string ProductCategoryApiName_GetChildList = "/Api/Sys/ProductCategory/GetChildList";

        /// <summary>
        /// 查询产品分类族谱路径
        /// </summary>
        public static string ProductCategoryApiName_GetParentList = "/Api/Sys/ProductCategory/GetParentList";
    }
}
