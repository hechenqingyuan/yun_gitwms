/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-14 9:10:40
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-14 9:10:40       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public class CompanyApiName
    {
        /// <summary>
        /// 获取公司信息
        /// </summary>
        public static string CompanyApiName_GetSingle = "/Api/Sys/Company/GetSingle";

        /// <summary>
        /// 编辑公司信息
        /// </summary>
        public static string CompanyApiName_Edit = "/Api/Sys/Company/Edit";

        /// <summary>
        /// 新增公司信息
        /// </summary>
        public static string CompanyApiName_Add = "/Api/Sys/Company/Add";

        /// <summary>
        /// 删除公司信息
        /// </summary>
        public static string CompanyApiName_Delete = "/Api/Sys/Company/Delete";

        /// <summary>
        /// 查询公司信息分页
        /// </summary>
        public static string CompanyApiName_GetList = "/Api/Sys/Company/GetList";
    }
}
