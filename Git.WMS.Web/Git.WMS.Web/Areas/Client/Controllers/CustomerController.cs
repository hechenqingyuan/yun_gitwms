using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Client.Controllers
{
    public class CustomerController : MasterPage
    {
        /// <summary>
        /// 客户管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.CustomerType = EnumHelper.GetOptions<ECusType>(0);
            return View();
        }

        /// <summary>
        /// 新增或编辑客户管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);
            CustomerEntity entity = null;
            ITopClient client = new TopClientDefault();

            if (!SnNum.IsEmpty())
            {
                
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SN", SnNum);

                string result = client.Execute(CustomerApiName.CustomerApiName_Single, dic);
                DataResult<CustomerEntity> dataResult = JsonConvert.DeserializeObject<DataResult<CustomerEntity>>(result);
                entity = dataResult.Result;
            }

            entity = entity.IsNull() ? new CustomerEntity() : entity;
            ViewBag.Entity = entity;

            //加载客户地址
            if (!entity.SnNum.IsEmpty())
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("CustomerSN", SnNum);
                string result = client.Execute(CustomerApiName.CustomerApiName_GetAddressList, dic);
                DataListResult<CusAddressEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<CusAddressEntity>>(result);
                List<CusAddressEntity> listAddress = dataResult.Result;
                listAddress = listAddress.IsNull() ? new List<CusAddressEntity>() : listAddress;
                Session[SessionKey.SESSION_CUSTOMER_ADDRESS] = listAddress;
            }
            else
            {
                Session[SessionKey.SESSION_CUSTOMER_ADDRESS] = null;
            }
            ViewBag.CustomerType = EnumHelper.GetOptions<ECusType>(entity.CusType);

            return View();
        }

        /// <summary>
        /// 添加或编辑地址
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Address()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");
            CusAddressEntity entity = null;
            ITopClient client = new TopClientDefault();

            if (!SnNum.IsEmpty())
            {
                List<CusAddressEntity> listAddress = Session[SessionKey.SESSION_CUSTOMER_ADDRESS] as List<CusAddressEntity>;
                listAddress = listAddress.IsNull() ? new List<CusAddressEntity>() : listAddress;
                entity = listAddress.FirstOrDefault(a => a.SnNum == SnNum);
            }

            entity = entity.IsNull() ? new CusAddressEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }

        /// <summary>
        /// 选择客户管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Dialog()
        {
            return View();
        }

    }
}
