/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-07 9:18:33
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-07 9:18:33       情缘
*********************************************************************************/

using Git.Framework.Resource;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Git.WMS.Sdk
{
    public partial class TopClientDefault:ITopClient
    {
        /// <summary>
        /// 根据API名称以及传入的参数执行 POST 请求
        /// </summary>
        /// <param name="ApiName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Execute(string ApiName, IDictionary<string, string> parameters)
        {
            string result = string.Empty;
            try
            {
                SDKUtils sdk = new SDKUtils();
                string ApiUrl = string.Format("{0}{1}", ResourceManager.GetSettingEntity("API_URL").Value, ApiName);
                result = sdk.DoPost(ApiUrl, parameters);
            }
            catch (Exception e)
            {
                DataResult dataResult = new DataResult() { Code=(int)EResponseCode.Exception,Message=e.Message };
                result = JsonConvert.SerializeObject(dataResult);
            }
            return result;
        }

        /// <summary>
        /// 根据API 的URL以及传入的参数执行 POST请求
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string ExecuteUrl(string ApiUrl, IDictionary<string, string> parameters)
        {
            string result = string.Empty;
            try
            {
                SDKUtils sdk = new SDKUtils();
                result = sdk.DoPost(ApiUrl, parameters);
            }
            catch (Exception e)
            {
                DataResult dataResult = new DataResult() { Code = (int)EResponseCode.Exception, Message = e.Message };
                result = JsonConvert.SerializeObject(dataResult);
            }
            return result;
        }

        /// <summary>
        /// JSON格式提交POST请求
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string ExecuteUrl(string ApiUrl, JObject parameters)
        {
            string result = string.Empty;
            try
            {
                HttpContent httpContent = new StringContent(parameters.ToString(), Encoding.UTF8, "text/json");
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient httpClient = new HttpClient();

                string responseJson = httpClient.PostAsync(ApiUrl, httpContent).Result.Content.ReadAsStringAsync().Result;
                return responseJson;
            }
            catch (Exception e)
            {
                DataResult dataResult = new DataResult() { Code = (int)EResponseCode.Exception, Message = e.Message };
                result = JsonHelper.SerializeObject(dataResult);
            }
            return result;
        }

        /// <summary>
        /// 执行PSOT请求
        /// </summary>
        /// <param name="ApiName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Execute(string ApiName, JObject parameters)
        {
            string result = string.Empty;
            try
            {
                string BaseUrl = ResourceManager.GetSettingEntity("API_URL").Value;
                string ApiUrl = string.Format("{0}{1}", BaseUrl, ApiName);
                HttpContent httpContent = new StringContent(parameters.ToString(), Encoding.UTF8, "text/json");
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient httpClient = new HttpClient();

                string responseJson = httpClient.PostAsync(ApiUrl, httpContent).Result.Content.ReadAsStringAsync().Result;
                return responseJson;
            }
            catch (Exception e)
            {
                DataResult dataResult = new DataResult() { Code = (int)EResponseCode.Exception, Message = e.Message };
                result = JsonHelper.SerializeObject(dataResult);
            }
            return result;
        }

        /// <summary>
        /// 执行POST请求
        /// </summary>
        /// <param name="ApiName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Execute(string ApiName, JArray parameters)
        {
            string result = string.Empty;
            try
            {
                string BaseUrl = ResourceManager.GetSettingEntity("API_URL").Value;
                string ApiUrl = string.Format("{0}{1}", BaseUrl, ApiName);
                HttpContent httpContent = new StringContent(parameters.ToString(), Encoding.UTF8, "text/json");
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient httpClient = new HttpClient();

                string responseJson = httpClient.PostAsync(ApiUrl, httpContent).Result.Content.ReadAsStringAsync().Result;
                return responseJson;
            }
            catch (Exception e)
            {
                DataResult dataResult = new DataResult() { Code = (int)EResponseCode.Exception, Message = e.Message };
                result = JsonHelper.SerializeObject(dataResult);
            }
            return result;
        }

        /// <summary>
        /// 根据API名称以及传入的参数执行
        /// </summary>
        /// <param name="ApiName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Get(string ApiName, IDictionary<string, string> parameters)
        {
            string result = string.Empty;
            try
            {
                SDKUtils sdk = new SDKUtils();
                string ApiUrl = string.Format("{0}{1}", ResourceManager.GetSettingEntity("API_URL").Value, ApiName);
                result = sdk.DoGet(ApiUrl, parameters);
            }
            catch (Exception e)
            {
                DataResult dataResult = new DataResult() { Code = (int)EResponseCode.Exception, Message = e.Message };
                result = JsonConvert.SerializeObject(dataResult);
            }
            return result;
        }

        /// <summary>
        /// 根据API 的URL以及传入的参数执行
        /// </summary>
        /// <param name="ApiUrl"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string GetUrl(string ApiUrl, IDictionary<string, string> parameters)
        {
            string result = string.Empty;
            try
            {
                SDKUtils sdk = new SDKUtils();
                result = sdk.DoGet(ApiUrl, parameters);
            }
            catch (Exception e)
            {
                DataResult dataResult = new DataResult() { Code = (int)EResponseCode.Exception, Message = e.Message };
                result = JsonConvert.SerializeObject(dataResult);
            }
            return result;
        }

        public string Execute<T>(ITopRequest<T> request) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
