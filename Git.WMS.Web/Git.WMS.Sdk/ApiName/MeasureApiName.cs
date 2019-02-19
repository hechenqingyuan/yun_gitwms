/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-13 17:51:30
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-13 17:51:30       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk.ApiName
{
    public partial class MeasureApiName
    {
        /// <summary>
        /// 新增单位
        /// </summary>
        public static string MeasureApiName_Add = "/Api/Sys/Measure/Add";

        /// <summary>
        /// 编辑单位
        /// </summary>
        public static string MeasureApiName_Edit = "/Api/Sys/Measure/Edit";

        /// <summary>
        /// 删除单位
        /// </summary>
        public static string MeasureApiName_Delete = "/Api/Sys/Measure/Delete";

        /// <summary>
        /// 查询单位
        /// </summary>
        public static string MeasureApiName_Single = "/Api/Sys/Measure/Single";

        /// <summary>
        /// 单位列表
        /// </summary>
        public static string MeasureApiName_GetList = "/Api/Sys/Measure/GetList";

        /// <summary>
        /// 单位分页
        /// </summary>
        public static string MeasureApiName_GetPage = "/Api/Sys/Measure/GetPage";

        /// <summary>
        /// 查看产品包装情况
        /// </summary>
        public static string MeasureApiName_GetPackageList = "/Api/Sys/Measure/GetPackageList";

        /// <summary>
        /// 新增产品包装情况
        /// </summary>
        public static string MeasureApiName_AddPackage = "/Api/Sys/Measure/AddPackage";

        /// <summary>
        /// 删除产品包装结构
        /// </summary>
        public static string MeasureApiName_DeletePacage = "/Api/Sys/Measure/DeletePacage";

        /// <summary>
        /// 查询产品单位
        /// </summary>
        public static string MeasureApiName_GetProductUnit = "/Api/Sys/Measure/GetProductUnit";
    }
}
