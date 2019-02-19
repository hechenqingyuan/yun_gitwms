/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-06-17 9:19:06
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-06-17 9:19:06       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class CheckApiName
    {
        /// <summary>
        /// 创建盘点单
        /// </summary>
        public static string CheckApiName_Add = "/Api/Order/Check/Create";

        /// <summary>
        /// 编辑盘点单
        /// </summary>
        public static string CheckApiName_Edit = "/Api/Order/Check/Edit";

        /// <summary>
        /// 根据唯一编号查询盘点单主体信息
        /// </summary>
        public static string CheckApiName_GetOrder = "/Api/Order/Check/GetOrder";

        /// <summary>
        /// 根据盘点单唯一编号查询盘点单详细信息
        /// </summary>
        public static string CheckApiName_GetDetail = "/Api/Order/Check/GetDetail";

        /// <summary>
        /// 查询盘点单分页列表
        /// </summary>
        public static string CheckApiName_GetOrderList = "/Api/Order/Check/GetOrderList";

        /// <summary>
        /// 批量删除盘点单
        /// </summary>
        public static string CheckApiName_Delete = "/Api/Order/Check/Delete";

        /// <summary>
        /// 根据盘点单唯一编号删除盘点单
        /// </summary>
        public static string CheckApiName_DeleteSingle = "/Api/Order/Check/DeleteSingle";

        /// <summary>
        /// 取消盘点单
        /// </summary>
        public static string CheckApiName_Cancel = "/Api/Order/Check/Cancel";

        /// <summary>
        /// 审核盘点单
        /// </summary>
        public static string CheckApiName_Audite = "/Api/Order/Check/Audite";

        /// <summary>
        /// 查询盘点差异单所有数据
        /// </summary>
        public static string CheckApiName_GetDif = "/Api/Order/Check/GetDif";

        /// <summary>
        /// 查询盘差表分页
        /// </summary>
        public static string CheckApiName_GetDifPage = "/Api/Order/Check/GetDifPage";

        /// <summary>
        /// 保存盘点差异单
        /// </summary>
        public static string CheckApiName_SaveDif = "/Api/Order/Check/SaveDif";

        /// <summary>
        /// 新增盘差数据
        /// </summary>
        public static string CheckApiName_AddDif = "/Api/Order/Check/AddDif";

        /// <summary>
        /// 删除盘差数据
        /// </summary>
        public static string CheckApiName_DeleteDif = "/Api/Order/Check/DeleteDif";

        /// <summary>
        /// 完成盘点作业
        /// </summary>
        public static string CheckApiName_Complete = "/Api/Order/Check/Complete";

        /// <summary>
        /// 根据盘点单号查询盘点单信息
        /// </summary>
        public static string CheckApiName_GetOrderByNum = "/Api/Order/Check/GetOrderByNum";
    }
}
