using Git.Framework.Resource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Storage.Provider.Sys
{
    public partial class SmsProvider:DataFactory
    {
        public SmsProvider()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CompanyID"></param>
        public SmsProvider(string CompanyID)
        {
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="Phone">手机号码</param>
        /// <param name="param">短信参数</param>
        /// <param name="sign">短信模板签名</param>
        /// <param name="template">短信模板</param>
        public void Send(string Phone, Dictionary<string, string> param, string sign,string template)
        {
            Task.Factory.StartNew(() => 
            {
                string url = ResourceManager.GetSettingEntity("Alidayu_URL").Value;
                string appkey = ResourceManager.GetSettingEntity("Alidayu_AppKey").Value;
                string secret = ResourceManager.GetSettingEntity("Alidayu_AppSecret").Value;
                string SignName = ResourceManager.GetSettingEntity("Alidayu_SignName").Value;

                Top.Api.ITopClient client = new Top.Api.DefaultTopClient(url, appkey, secret);
                Top.Api.Request.AlibabaAliqinFcSmsNumSendRequest req = new Top.Api.Request.AlibabaAliqinFcSmsNumSendRequest();
                req.Extend = "";
                req.SmsType = "normal";
                req.SmsFreeSignName = SignName;
                req.SmsParam = JsonConvert.SerializeObject(param);
                req.RecNum = Phone;
                req.SmsTemplateCode = template;
                Top.Api.Response.AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
            });
        }
    }
}
