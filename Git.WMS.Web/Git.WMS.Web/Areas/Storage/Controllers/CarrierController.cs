using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Storage.Entity.Sys;
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

namespace Git.WMS.Web.Areas.Storage.Controllers
{
    public class CarrierController : MasterPage
    {
        /// <summary>
        /// 承运商管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 新增编辑承运商
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);
            CarrierEntity entity = null;
            ITopClient client = new TopClientDefault();

            if (!SnNum.IsEmpty())
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(CarrierApiName.CarrierApiName_GetSingle, dic);
                DataResult<CarrierEntity> dataResult = JsonHelper.DeserializeObject<DataResult<CarrierEntity>>(result);
                entity = dataResult.Result;
            }

            entity = entity.IsNull() ? new CarrierEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }

        /// <summary>
        /// 选择承运商对话框
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true, false)]
        public ActionResult Dialog()
        {
            return View();
        }
    }
}
