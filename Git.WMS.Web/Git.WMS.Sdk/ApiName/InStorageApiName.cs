/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-04-06 17:41:57
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-04-06 17:41:57       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class InStorageApiName
    {
        /// <summary>
        /// 创建入库单
        /// </summary>
        public static string InStorageApiName_Add = "/Api/Order/InStorage/Create";

        /// <summary>
        /// 编辑入库单
        /// </summary>
        public static string InStorageApiName_Edit = "/Api/Order/InStorage/Edit";

        /// <summary>
        /// 编辑入库单主体
        /// </summary>
        public static string InStorageApiName_EditOrder = "/Api/Order/InStorage/EditOrder";

        /// <summary>
        /// 编辑入库单的详细项
        /// </summary>
        public static string InStorageApiName_EditDetail = "/Api/Order/InStorage/EditDetail";

        /// <summary>
        /// 查询入库单表头
        /// </summary>
        public static string InStorageApiName_GetOrder= "/Api/Order/InStorage/GetOrder";

        /// <summary>
        /// 查询入库单详细
        /// </summary>
        public static string InStorageApiName_GetDetail = "/Api/Order/InStorage/GetDetail";

        /// <summary>
        /// 根据唯一编号查询入库单明细数据行
        /// </summary>
        public static string InStorageApiName_GetDetailInfol = "/Api/Order/InStorage/GetDetailInfo";

        /// <summary>
        /// 查询入库单分页列表
        /// </summary>
        public static string InStorageApiName_GetOrderList = "/Api/Order/InStorage/GetOrderList";

        /// <summary>
        /// 删除入库单
        /// </summary>
        public static string InStorageApiName_Delete = "/Api/Order/InStorage/Delete";

        /// <summary>
        /// 根据唯一编号删除入库单
        /// </summary>
        public static string InStorageApiName_DeleteSingle = "/Api/Order/InStorage/DeleteSingle";

        /// <summary>
        /// 取消入库单
        /// </summary>
        public static string InStorageApiName_Cancel = "/Api/Order/InStorage/Cancel";

        /// <summary>
        /// 审核入库单
        /// </summary>
        public static string InStorageApiName_Audite = "/Api/Order/InStorage/Audite";

        /// <summary>
        /// 入库单详细分页列表
        /// </summary>
        public static string InStorageApiName_GetDetailList = "/Api/Order/InStorage/GetDetailList";

        /// <summary>
        /// 统计入库单的行数
        /// </summary>
        public static string InStorageApiName_GetCount = "/Api/Order/InStorage/GetCount";

        /// <summary>
        /// 设置打印的数据信息
        /// </summary>
        public static string InStorageApiName_Print = "/Api/Order/InStorage/Print";

        /// <summary>
        /// 查询打印的数据集合
        /// </summary>
        public static string InStorageApiName_GetPrintSource = "/Api/Order/InStorage/GetPrintSource";

        /// <summary>
        /// 根据入库单编号查询入库单信息
        /// </summary>
        public static string InStorageApiName_GetOrderByNum = "/Api/Order/InStorage/GetOrderByNum";
    }
}
