/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-07 8:46:14
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-07 8:46:14       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class UserApiName
    {
        /// <summary>
        /// 用户查询分页
        /// </summary>
        public static string UserApiName_GetList_Page = "/Api/Sys/User/GetList";

        /// <summary>
        /// 新增用户
        /// </summary>
        public static string UserApiName_Add = "/Api/Sys/User/Add";

        /// <summary>
        /// 编辑用户
        /// </summary>
        public static string UserApiName_Edit = "/Api/Sys/User/Edit";

        /// <summary>
        /// 删除用户
        /// </summary>
        public static string UserApiName_Delete = "/Api/Sys/User/Delete";

        /// <summary>
        /// 查询单个用户
        /// </summary>
        public static string UserApiName_Single = "/Api/Sys/User/Single";

        /// <summary>
        /// 修改密码
        /// </summary>
        public static string UserApiName_UpdatePass = "/Api/Sys/User/UpdatePass";

        /// <summary>
        /// 管理员修改用户密码
        /// </summary>
        public static string UserApiName_AdminEditPass = "/Api/Sys/User/AdminEditPass";

        /// <summary>
        /// 用户登录
        /// </summary>
        public static string UserApiName_Login = "/Api/Sys/User/Login";

        /// <summary>
        /// 获得某个角色分配的权限
        /// </summary>
        public static string UserApiName_GetPower = "/Api/Sys/User/GetPower";

        /// <summary>
        /// 保存权限分配
        /// </summary>
        public static string UserApiName_SavePower = "/Api/Sys/User/SavePower";

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        public static string UserApiName_HasPower = "/Api/Sys/User/HasPower";
    }
}
