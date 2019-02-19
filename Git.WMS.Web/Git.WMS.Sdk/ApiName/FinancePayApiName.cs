/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-14 8:21:06
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-14 8:21:06       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class FinancePayApiName
    {
        /// <summary>
        /// 新增财务实收实付
        /// </summary>
        public static string FinancePayApiName_Add = "/Api/Finance/Pay/Add";

        /// <summary>
        /// 编辑财务实收实付
        /// </summary>
        public static string FinancePayApiName_Edit = "/Api/Finance/Pay/Edit";

        /// <summary>
        /// 删除财务实收实付
        /// </summary>
        public static string FinancePayApiName_Delete = "/Api/Finance/Pay/Delete";

        /// <summary>
        /// 查询财务实收实付
        /// </summary>
        public static string FinancePayApiName_Single = "/Api/Finance/Pay/GetSingle";

        /// <summary>
        /// 查询财务实收实付详细
        /// </summary>
        public static string FinancePayApiName_Detail = "/Api/Finance/Pay/GetDetail";

        /// <summary>
        /// 财务实收实付分页
        /// </summary>
        public static string FinancePayApiName_GetPage = "/Api/Finance/Pay/GetList";

        /// <summary>
        /// 查询流水记录
        /// </summary>
        public static string FinancePayApiName_GeAgencyBilltList = "/Api/Finance/Pay/GeAgencyBilltList";
    }
}
