using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Entity.Base;
using Git.Storage.Entity.Sys;
using Git.WMS.Sdk;
using Git.Storage.Common;
using Newtonsoft.Json;
using Git.WMS.Sdk.ApiName;
using Git.Storage.Common.Enum;

namespace Git.WMS.Web.Lib.Filter
{
    public class DialogFilter : BaseAuthorizeAttribute
    {
        private bool ValidateLogin = true;

        private bool ValidateRequest = true;

        public DialogFilter()
            : base()
        {

        }

        public DialogFilter(bool _validateLogin)
            : base()
        {
            this.ValidateLogin = _validateLogin;
        }

        public DialogFilter(bool _validateLogin, bool _validateRequest)
            : base()
        {
            this.ValidateLogin = _validateLogin;
            this.ValidateRequest = _validateRequest;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            AdminEntity LoginUser = filterContext.HttpContext.Session[SessionKey.SESSION_LOGIN_ADMIN] as AdminEntity;
            
            if (this.ValidateLogin)
            {
                string path = filterContext.HttpContext.Request.Path;
                if (LoginUser.IsNull())
                {
                    string url = "/Home/Index";
                    if (!path.IsEmpty())
                    {
                        path = filterContext.HttpContext.Server.UrlEncode(path);
                        url = "/Home/Index?returnurl=" + path;
                    }
                    filterContext.Result = new RedirectResult(url);
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
                            string url = "/Home/Error";
                            filterContext.Result = new RedirectResult(url);
                        }
                    }
                }
            }
        }
    }
}