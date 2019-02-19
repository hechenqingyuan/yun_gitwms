/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-07 9:16:07
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-07 9:16:07       情缘
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk
{
    public interface ITopRequest<T> where T : class
    {
        /// <summary>
        /// 获取TOP的API名称。
        /// </summary>
        /// <returns>API名称</returns>
        string GetApiName();

        /// <summary>
        /// 获取所有的Key-Value形式的文本请求参数字典。其中：
        /// Key: 请求参数名
        /// Value: 请求参数文本值
        /// </summary>
        /// <returns>文本请求参数字典</returns>
        IDictionary<string, string> GetParameters();

        /// <summary>
        /// 提前验证参数。
        /// </summary>
        void Validate();
    }
}
