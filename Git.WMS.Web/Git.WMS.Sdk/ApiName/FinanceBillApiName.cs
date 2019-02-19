/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-14 8:20:52
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-14 8:20:52       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class FinanceBillApiName
    {
        /// <summary>
        /// 新增财务应收应付
        /// </summary>
        public static string FinanceBillApiName_Add = "/Api/Finance/Bill/Add";

        /// <summary>
        /// 编辑财务应收应付
        /// </summary>
        public static string FinanceBillApiName_Edit = "/Api/Finance/Bill/Edit";

        /// <summary>
        /// 删除财务应收应付
        /// </summary>
        public static string FinanceBillApiName_Delete = "/Api/Finance/Bill/Delete";

        /// <summary>
        /// 查询财务应收应付
        /// </summary>
        public static string FinanceBillApiName_Single = "/Api/Finance/Bill/GetSingle";

        /// <summary>
        /// 财务应收应付分页
        /// </summary>
        public static string FinanceBillApiName_GetPage = "/Api/Finance/Bill/GetList";

        /// <summary>
        /// 财务应收应付审核
        /// </summary>
        public static string FinanceBillApiName_Audite = "/Api/Finance/Bill/Audite";
    }
}
