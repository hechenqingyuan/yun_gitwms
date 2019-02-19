/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-10 10:37:46
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-10 10:37:46       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class RoleApiName
    {
        /// <summary>
        /// 新增角色
        /// </summary>
        public static string RoleApiName_Add = "/Api/Sys/Role/Add";

        /// <summary>
        /// 编辑角色
        /// </summary>
        public static string RoleApiName_Edit = "/Api/Sys/Role/Edit";

        /// <summary>
        /// 删除角色
        /// </summary>
        public static string RoleApiName_Delete = "/Api/Sys/Role/Delete";

        /// <summary>
        /// 查询角色
        /// </summary>
        public static string RoleApiName_Single = "/Api/Sys/Role/Single";

        /// <summary>
        /// 角色列表
        /// </summary>
        public static string RoleApiName_GetList = "/Api/Sys/Role/GetList";

        /// <summary>
        /// 角色分页
        /// </summary>
        public static string RoleApiName_GetPage = "/Api/Sys/Role/GetPage";
    }
}
