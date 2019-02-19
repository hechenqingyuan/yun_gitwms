using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Common
{
    public partial class DataResult
    {
        /// <summary>
        /// 相应状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 相应状态消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 业务状态码
        /// </summary>
        public int SubCode { get; set; }

        /// <summary>
        /// 业务相应消息
        /// </summary>
        public string SubMessage { get; set; }
    }
    public partial class DataResult<T> : DataResult
    {
        public T Result { get; set; }
    }

}
