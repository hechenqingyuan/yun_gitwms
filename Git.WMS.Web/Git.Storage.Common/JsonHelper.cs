/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-11-14 22:12:23
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-11-14 22:12:23       情缘
*********************************************************************************/

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Common
{
    public static class JsonHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string SerializeObject(object value)
        {
            if (value != null)
            {
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
                timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
                return JsonConvert.SerializeObject(value, Formatting.None, timeConverter);
            }
            return string.Empty;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
                timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
                return JsonConvert.DeserializeObject<T>(value, timeConverter);
            }
            return default(T);
        }
    }
}
