using Git.Framework.Resource;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Git.WMS.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ResourceManager.LoadCache();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            RegEnum();
        }

        /// <summary>
        /// 注册枚举值
        /// </summary>
        private void RegEnum()
        {
            EnumToJsonHelper.Reg(typeof(EAudite),
                    typeof(EBadType),
                    typeof(ECusType),
                    typeof(EInType),
                    typeof(EEquipmentStatus),
                    typeof(EIsDelete),
                    typeof(ELocalType),
                    typeof(EMoveType),
                    typeof(EOutType),
                    typeof(EProductType),
                    typeof(EReturnStatus),
                    typeof(EStorageType),
                    typeof(EOpType),
                    typeof(ECheckType),
                    typeof(EReturnType),
                    typeof(EChange),
                    typeof(EOrderStatus),
                    typeof(EOrderType),
                    typeof(ESupType),
                    typeof(EDataSourceType),
                    typeof(EReportType),
                    typeof(EElementType),
                    typeof(ESequence),
                    typeof(EFinanceStatus),
                    typeof(EPayType),
                    typeof(EFinanceType),
                    typeof(EPurchaseType),
                    typeof(EResourceType),
                    typeof(EPurchaseStatus),
                    typeof(EProductPackage),
                    typeof(EAllocateType),
                    typeof(ESaleReturnStatus),
                    typeof(EPurchaseReturnStatus),
                    typeof(EBool));
        }
    }
}