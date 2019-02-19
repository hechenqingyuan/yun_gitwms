using Git.Framework.Controller;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Entity.Storage;
using Newtonsoft.Json;
using Git.Storage.Common;
using System.Data;
using Git.Storage.Common.Excel;
using Git.Storage.Common.Enum;

namespace Git.WMS.Web.Areas.Client.Controllers
{
    public class SupplierController : MasterPage
    {
        /// <summary>
        /// 供应商管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.SupplierType = EnumHelper.GetOptions<ESupType>(0);

            return View();
        }

        /// <summary>
        /// 新增或编辑供应商
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum",string.Empty);
            SupplierEntity entity = null;
            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(SupplierApiName.SupplierApiName_Single, dic);
                DataResult<SupplierEntity> dataResult = JsonConvert.DeserializeObject<DataResult<SupplierEntity>>(result);
                entity = dataResult.Result;
            }
            
            entity = entity.IsNull() ? new SupplierEntity() : entity;
            ViewBag.Entity = entity;
            entity.SupType = entity.SupType == 0 ? (int)ESupType.Cooperation : entity.SupType;
            ViewBag.SupplierType = EnumHelper.GetOptions<ESupType>(entity.SupType);
            return View();
        }

        /// <summary>
        /// 选择供应商
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Dialog()
        {
            return View();
        }
    }
}
