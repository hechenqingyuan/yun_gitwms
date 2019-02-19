/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-27 20:43:43
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-27 20:43:43       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class StorageApiName
    {
        /// <summary>
        /// 新增仓库
        /// </summary>
        public static string StorageApiName_Add = "/Api/Sys/Storage/Add";

        /// <summary>
        /// 编辑仓库
        /// </summary>
        public static string StorageApiName_Edit = "/Api/Sys/Storage/Edit";

        /// <summary>
        /// 删除仓库
        /// </summary>
        public static string StorageApiName_Delete = "/Api/Sys/Storage/Delete";

        /// <summary>
        /// 查询仓库
        /// </summary>
        public static string StorageApiName_Single = "/Api/Sys/Storage/Single";

        /// <summary>
        /// 仓库列表
        /// </summary>
        public static string StorageApiName_GetList = "/Api/Sys/Storage/GetList";

        /// <summary>
        /// 仓库分页
        /// </summary>
        public static string StorageApiName_GetPage = "/Api/Sys/Storage/GetPage";

        /// <summary>
        /// 设置默认
        /// </summary>
        public static string StorageApiName_SetDefault = "/Api/Sys/Storage/SetDefault";

        /// <summary>
        /// 禁用启用
        /// </summary>
        public static string StorageApiName_SetForbid = "/Api/Sys/Storage/SetForbid";
    }
}
