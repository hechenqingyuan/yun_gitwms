/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-11 22:15:16
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-11 22:15:16       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class DepartApiName
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        public static string DepartApiName_Add = "/Api/Sys/Depart/Add";

        /// <summary>
        /// 编辑部门
        /// </summary>
        public static string DepartApiName_Edit = "/Api/Sys/Depart/Edit";

        /// <summary>
        /// 删除部门
        /// </summary>
        public static string DepartApiName_Delete = "/Api/Sys/Depart/Delete";

        /// <summary>
        /// 查询部门
        /// </summary>
        public static string DepartApiName_Single = "/Api/Sys/Depart/Single";

        /// <summary>
        /// 部门列表
        /// </summary>
        public static string DepartApiName_GetList = "/Api/Sys/Depart/GetList";

        /// <summary>
        /// 部门分页
        /// </summary>
        public static string DepartApiName_GetPage = "/Api/Sys/Depart/GetPage";

        /// <summary>
        /// 查询部门的子类集合
        /// </summary>
        public static string DepartApiName_GetChildList = "/Api/Sys/Depart/GetChildList";

        /// <summary>
        /// 查询族谱路径
        /// </summary>
        public static string DepartApiName_GetParentList = "/Api/Sys/Depart/GetParentList";
    }
}
