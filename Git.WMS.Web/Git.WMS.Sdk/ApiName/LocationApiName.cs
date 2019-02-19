/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-28 20:47:40
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-28 20:47:40       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class LocationApiName
    {
        /// <summary>
        /// 新增库位
        /// </summary>
        public static string LocationApiName_Add = "/Api/Sys/Location/Add";

        /// <summary>
        /// 编辑库位
        /// </summary>
        public static string LocationApiName_Edit = "/Api/Sys/Location/Edit";

        /// <summary>
        /// 删除库位
        /// </summary>
        public static string LocationApiName_Delete = "/Api/Sys/Location/Delete";

        /// <summary>
        /// 查询库位
        /// </summary>
        public static string LocationApiName_Single = "/Api/Sys/Location/Single";

        /// <summary>
        /// 库位列表
        /// </summary>
        public static string LocationApiName_GetList = "/Api/Sys/Location/GetList";

        /// <summary>
        /// 库位分页
        /// </summary>
        public static string LocationApiName_GetPage = "/Api/Sys/Location/GetPage";

        /// <summary>
        /// 设置默认
        /// </summary>
        public static string LocationApiName_SetDefault = "/Api/Sys/Location/SetDefault";

        /// <summary>
        /// 禁用启用
        /// </summary>
        public static string LocationApiName_SetForbid = "/Api/Sys/Location/SetForbid";
    }
}
