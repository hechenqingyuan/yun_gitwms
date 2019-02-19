/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-05 17:58:58
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-05 17:58:58       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class MoveApiName
    {
        /// <summary>
        /// 创建移库单
        /// </summary>
        public static string MoveApiName_Add = "/Api/Order/Move/Create";

        /// <summary>
        /// 编辑移库单
        /// </summary>
        public static string MoveApiName_Edit = "/Api/Order/Move/Edit";

        /// <summary>
        /// 编辑移库单主体
        /// </summary>
        public static string MoveApiName_EditOrder = "/Api/Order/Move/EditOrder";

        /// <summary>
        /// 编辑移库单详细
        /// </summary>
        public static string MoveApiName_EditDetail = "/Api/Order/Move/EditDetail";

        /// <summary>
        /// 查询移库单表头
        /// </summary>
        public static string MoveApiName_GetOrder = "/Api/Order/Move/GetOrder";

        /// <summary>
        /// 查询移库单详细
        /// </summary>
        public static string MoveApiName_GetDetail = "/Api/Order/Move/GetDetail";

        /// <summary>
        /// 查询移库单分页列表
        /// </summary>
        public static string MoveApiName_GetOrderList = "/Api/Order/Move/GetOrderList";

        /// <summary>
        /// 删除移库单
        /// </summary>
        public static string MoveApiName_Delete = "/Api/Order/Move/Delete";

        /// <summary>
        /// 根据唯一编号删除移库单
        /// </summary>
        public static string MoveApiName_DeleteSingle = "/Api/Order/Move/DeleteSingle";

        /// <summary>
        /// 取消移库单
        /// </summary>
        public static string MoveApiName_Cancel = "/Api/Order/Move/Cancel";

        /// <summary>
        /// 审核移库单
        /// </summary>
        public static string MoveApiName_Audite = "/Api/Order/Move/Audite";

        /// <summary>
        /// 移库单详细分页
        /// </summary>
        public static string MoveApiName_GetDetailPage = "/Api/Order/Move/GetDetailPage";

        /// <summary>
        /// 根据条件统计移库单的数据行
        /// </summary>
        public static string MoveApiName_GetCount = "/Api/Order/Move/GetCount";

        /// <summary>
        /// 设置打印数据
        /// </summary>
        public static string MoveApiName_Print = "/Api/Order/Move/Print";

        /// <summary>
        /// 获取打印的数据源
        /// </summary>
        public static string MoveApiName_GetPrint = "/Api/Order/Move/GetPrint";

        /// <summary>
        /// 根据移库单号查询移库单
        /// </summary>
        public static string MoveApiName_GetOrderByNum = "/Api/Order/Move/GetOrderByNum";
    }
}
