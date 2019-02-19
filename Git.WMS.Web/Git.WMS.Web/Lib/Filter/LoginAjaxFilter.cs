using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Entity.Base;
using Git.Storage.Entity.Sys;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Newtonsoft.Json;

namespace Git.WMS.Web.Lib.Filter
{
    public class LoginAjaxFilter : BaseAuthorizeAttribute
    {

        private bool ValidateLogin = true;

        private bool ValidateRequest = true;

        public LoginAjaxFilter()
            : base()
        {

        }

        public LoginAjaxFilter(bool _validateLogin)
            : base()
        {
            this.ValidateLogin = _validateLogin;
        }

        public LoginAjaxFilter(bool _validateLogin, bool _validateRequest)
            : base()
        {
            this.ValidateLogin = _validateLogin;
            this.ValidateRequest = _validateRequest;
        }

        /// <summary>
        /// 1001:用户未登录
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.ValidateLogin)
            {
                AdminEntity LoginUser = filterContext.HttpContext.Session[SessionKey.SESSION_LOGIN_ADMIN] as AdminEntity;
                string path = filterContext.HttpContext.Request.Path;
                if (LoginUser.IsNull())
                {
                    filterContext.Result = new JsonResult() { Data = new DataResult() { Code=(int)EResponseCode.NotLogin,Message="未登录" },JsonRequestBehavior=JsonRequestBehavior.AllowGet };
                }
                else
                {
                    if (ValidateRequest && path != "/")
                    {
                        ITopClient client = new TopClientDefault();
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        string CompanyID = LoginUser.CompanyID;
                        dic.Add("CompanyID", CompanyID);
                        dic.Add("RoleNum", LoginUser.RoleNum);
                        dic.Add("ResNum", path);
                        string result = client.Execute(UserApiName.UserApiName_HasPower, dic);
                        DataResult dataResult = JsonConvert.DeserializeObject<DataResult>(result);

                        if (dataResult.Code != (int)EResponseCode.Success)
                        {
                            filterContext.Result = new JsonResult() { Data = new DataResult() { Code = (int)EResponseCode.NoPermission, Message = "未登录" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }

                    }
                }
            }
        }
    }
}