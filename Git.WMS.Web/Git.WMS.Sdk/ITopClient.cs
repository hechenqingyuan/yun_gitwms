/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-07 9:13:22
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-07 9:13:22       情缘
*********************************************************************************/

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.WMS.Sdk
{
    public partial interface ITopClient
    {
        /// <summary>
        /// 根据API名称以及传入的参数执行 POST 请求
        /// </summary>
        /// <param name="ApiName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string Execute(string ApiName,IDictionary<string, string> parameters);

        /// <summary>
        /// 根据API 的URL以及传入的参数执行 POST 请求
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string ExecuteUrl(string ApiUrl, IDictionary<string, string> parameters);

        /// <summary>
        /// JSON格式提交POST请求
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string ExecuteUrl(string ApiUrl, JObject parameters);

        /// <summary>
        /// 执行POST请求
        /// </summary>
        /// <param name="ApiName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string Execute(string ApiName, JObject parameters);

        /// <summary>
        /// 执行POST请求
        /// </summary>
        /// <param name="ApiName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string Execute(string ApiName, JArray parameters);

        /// <summary>
        /// 根据API名称以及传入的参数执行
        /// </summary>
        /// <param name="ApiName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string Get(string ApiName, IDictionary<string, string> parameters);

        /// <summary>
        /// 根据API 的URL以及传入的参数执行
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GetUrl(string ApiUrl, IDictionary<string, string> parameters);

        /// <summary>
        /// 根据请求对象执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        string Execute<T>(ITopRequest<T> request) where T : class;
    }
}
