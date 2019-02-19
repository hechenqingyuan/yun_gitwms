/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-05-12 20:08:41
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-05-12 20:08:41       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class FinanceCateApiName
    {
        /// <summary>
        /// 新增财务类别
        /// </summary>
        public static string FinanceCateApiName_Add = "/Api/Finance/Cate/Add";

        /// <summary>
        /// 编辑财务类别
        /// </summary>
        public static string FinanceCateApiName_Edit = "/Api/Finance/Cate/Edit";

        /// <summary>
        /// 删除财务类别
        /// </summary>
        public static string FinanceCateApiName_Delete = "/Api/Finance/Cate/Delete";

        /// <summary>
        /// 查询财务类别
        /// </summary>
        public static string FinanceCateApiName_Single = "/Api/Finance/Cate/Single";

        /// <summary>
        /// 财务类别列表
        /// </summary>
        public static string FinanceCateApiName_GetList = "/Api/Finance/Cate/GetList";

        /// <summary>
        /// 财务类别分页
        /// </summary>
        public static string FinanceCateApiName_GetPage = "/Api/Finance/Cate/GetPage";
    }
}
