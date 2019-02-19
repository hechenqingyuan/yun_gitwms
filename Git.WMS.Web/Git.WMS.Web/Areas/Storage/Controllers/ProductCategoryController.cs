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

namespace Git.WMS.Web.Areas.Storage.Controllers
{
    public class ProductCategoryController : MasterPage
    {
        /// <summary>
        /// 类别管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 新增编辑类别
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);
            ProductCategoryEntity entity = null;
            ITopClient client = new TopClientDefault();

            if (!SnNum.IsEmpty())
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(ProductCategoryApiName.ProductCategoryApiName_Single, dic);
                DataResult<ProductCategoryEntity> dataResult = JsonConvert.DeserializeObject<DataResult<ProductCategoryEntity>>(result);
                entity = dataResult.Result;
            }

            entity = entity.IsNull() ? new ProductCategoryEntity() : entity;
            ViewBag.Entity = entity;
            ViewBag.ParentNum = DropDownHelper.GetCate(string.Empty,this.CompanyID);
            return View();
        }
    }
}
