/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-16 21:59:49
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-16 21:59:49       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class EquipmentApiName
    {
        /// <summary>
        /// 新增设备
        /// </summary>
        public static string EquipmentApiName_Add = "/Api/Sys/Equipment/Add";

        /// <summary>
        /// 编辑设备
        /// </summary>
        public static string EquipmentApiName_Edit = "/Api/Sys/Equipment/Edit";

        /// <summary>
        /// 删除设备
        /// </summary>
        public static string EquipmentApiName_Delete = "/Api/Sys/Equipment/Delete";

        /// <summary>
        /// 查询设备
        /// </summary>
        public static string EquipmentApiName_Single = "/Api/Sys/Equipment/Single";

        /// <summary>
        /// 设备列表
        /// </summary>
        public static string EquipmentApiName_GetList = "/Api/Sys/Equipment/GetList";

        /// <summary>
        /// 设备分页
        /// </summary>
        public static string EquipmentApiName_GetPage = "/Api/Sys/Equipment/GetPage";

        /// <summary>
        /// 设备授权
        /// </summary>
        public static string EquipmentApiName_Authorize = "/Api/Sys/Equipment/Authorize";
    }
}
