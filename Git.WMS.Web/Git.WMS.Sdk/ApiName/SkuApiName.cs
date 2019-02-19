/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2017/5/14 10:21:11
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2017/5/14 10:21:11       情缘
 * 吉特仓储管理系统 开源地址 https://github.com/hechenqingyuan/gitwms
 * 项目地址:http://yun.gitwms.com/
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    /// <summary>
    /// 产品SKU相关的接口
    /// </summary>
    public partial class SkuApiName
    {
        /// <summary>
        /// 新增产品以及产品SKU
        /// </summary>
        public static string SkuApiName_Add = "/Api/Sys/Sku/Add";

        /// <summary>
        /// 编辑产品以及产品SKU
        /// </summary>
        public static string SkuApiName_Edit = "/Api/Sys/Sku/Edit";

        /// <summary>
        /// 编辑产品信息
        /// </summary>
        public static string SkuApiName_EditProduct = "/Api/Sys/Sku/EditProduct";

        /// <summary>
        /// 编辑产品SKU
        /// </summary>
        public static string SkuApiName_EditSku = "/Api/Sys/Sku/EditSku";

        /// <summary>
        /// 根据产品唯一编号删除产品
        /// </summary>
        public static string SkuApiName_DeleteSingle = "/Api/Sys/Sku/DeleteSingle";

        /// <summary>
        /// 根据产品的唯一编号批量删除
        /// </summary>
        public static string SkuApiName_Delete = "/Api/Sys/Sku/Delete";

        /// <summary>
        /// 根据SKU的唯一编号删除SKU
        /// </summary>
        public static string SkuApiName_DeleteSkuSingle = "/Api/Sys/Sku/DeleteSkuSingle";

        /// <summary>
        /// 根据SKU唯一编号批量删除SKU信息
        /// </summary>
        public static string SkuApiName_DeleteSku = "/Api/Sys/Sku/DeleteSku";

        /// <summary>
        /// 根据产品SKU唯一编号查询SKU
        /// </summary>
        public static string SkuApiName_GetSku = "/Api/Sys/Sku/GetSku";

        /// <summary>
        /// 根据产品的唯一编号查询SKU信息
        /// </summary>
        public static string SkuApiName_GetSkuList = "/Api/Sys/Sku/GetSkuList";

        /// <summary>
        /// 根据SKU唯一编号查询产品SKU全部信息
        /// </summary>
        public static string SkuApiName_GetProductSku = "/Api/Sys/Sku/GetProductSku";

        /// <summary>
        /// 根据产品SKU的编码查询产品信息
        /// </summary>
        public static string SkuApiName_GetSkuBarCode = "/Api/Sys/Sku/GetSkuBarCode";

        /// <summary>
        /// 查询产品SKU信息分页
        /// </summary>
        public static string SkuApiName_GetList = "/Api/Sys/Sku/GetList";
    }
}
