/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-10 15:41:00
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-10 15:41:00       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class BadApiName
    {
        /// <summary>
        /// 创建报损单
        /// </summary>
        public static string BadApiName_Add = "/Api/Order/Bad/Create";

        /// <summary>
        /// 编辑报损单
        /// </summary>
        public static string BadApiName_Edit = "/Api/Order/Bad/Edit";

        /// <summary>
        /// 编辑报损单主体信息
        /// </summary>
        public static string BadApiName_EditOrder = "/Api/Order/Bad/EditOrder";

        /// <summary>
        /// 编辑报损单详细信息
        /// </summary>
        public static string BadApiName_EditDetail = "/Api/Order/Bad/EditDetail";

        /// <summary>
        /// 查询报损单表头
        /// </summary>
        public static string BadApiName_GetOrder = "/Api/Order/Bad/GetOrder";

        /// <summary>
        /// 查询报损单详细
        /// </summary>
        public static string BadApiName_GetDetail = "/Api/Order/Bad/GetDetail";

        /// <summary>
        /// 查询报损单分页列表
        /// </summary>
        public static string BadApiName_GetOrderList = "/Api/Order/Bad/GetOrderList";

        /// <summary>
        /// 批量删除报损单
        /// </summary>
        public static string BadApiName_Delete = "/Api/Order/Bad/Delete";

        /// <summary>
        /// 根据唯一编号删除报损单
        /// </summary>
        public static string BadApiName_DeleteSingle = "/Api/Order/Bad/DeleteSingle";

        /// <summary>
        /// 取消报损单
        /// </summary>
        public static string BadApiName_Cancel = "/Api/Order/Bad/Cancel";

        /// <summary>
        /// 审核报损单
        /// </summary>
        public static string BadApiName_Audite = "/Api/Order/Bad/Audite";

        /// <summary>
        /// 查询报损单详细分页
        /// </summary>
        public static string BadApiName_GetDetailPage = "/Api/Order/Bad/GetDetailPage";

        /// <summary>
        /// 报损单打印设置
        /// </summary>
        public static string BadApiName_Print = "/Api/Order/Bad/Print";

        /// <summary>
        /// 获得打印的数据源
        /// </summary>
        public static string BadApiName_GetPrint = "/Api/Order/Bad/GetPrint";

        /// <summary>
        /// 根据条件获得报损单的数据行
        /// </summary>
        public static string BadApiName_GetCount = "/Api/Order/Bad/GetCount";

        /// <summary>
        /// 根据报损单号查询报损单信息
        /// </summary>
        public static string BadApiName_GetOrderByNum = "/Api/Order/Bad/GetOrderByNum";
    }
}
