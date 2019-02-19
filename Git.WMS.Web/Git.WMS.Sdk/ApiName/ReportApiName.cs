/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-04-25 20:43:15
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-04-25 20:43:15       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public class ReportApiName
    {
        /// <summary>
        /// 获取自定义报表分页列表
        /// </summary>
        public static string ReportApiName_GetList = "/Api/Report/Manager/GetList";

        /// <summary>
        /// 新增自定义报表
        /// </summary>
        public static string ReportApiName_Add = "/Api/Report/Manager/Add";

        /// <summary>
        /// 编辑自定义报表
        /// </summary>
        public static string ReportApiName_Edit = "/Api/Report/Manager/Edit";

        /// <summary>
        /// 查询自定义报表主体
        /// </summary>
        public static string ReportApiName_GetSingle = "/Api/Report/Manager/GetSingle";

        /// <summary>
        /// 查找自定义报表参数
        /// </summary>
        public static string ReportApiName_GetParameter = "/Api/Report/Manager/GetParameter";

        /// <summary>
        /// 删除自定义报表
        /// </summary>
        public static string ReportApiName_Delete = "/Api/Report/Manager/Delete";

        /// <summary>
        /// 查询存储过程参数
        /// </summary>
        public static string ReportApiName_GetProcParameter = "/Api/Report/Manager/GetProcParameter";

        /// <summary>
        /// 获得数据源
        /// </summary>
        public static string ReportApiName_GetDataSource = "/Api/Report/Manager/GetDataSource";
    }
}
