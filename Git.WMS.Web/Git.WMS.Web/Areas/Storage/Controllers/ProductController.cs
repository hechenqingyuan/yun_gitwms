using Git.Framework.Controller;
using Git.Storage.Entity.Storage;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.WMS.Sdk;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.WMS.Sdk.ApiName;
using Git.Storage.Common;
using Newtonsoft.Json;

namespace Git.WMS.Web.Areas.Storage.Controllers
{
    public class ProductController : MasterPage
    {
        /// <summary>
        /// 产品管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.Cate = DropDownHelper.GetCate(string.Empty, this.CompanyID);
            return View();
        }

        /// <summary>
        /// 新增修改产品
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);
            ProductEntity entity = null;
            ITopClient client = new TopClientDefault();

            if (!SnNum.IsEmpty())
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(ProductApiName.ProductApiName_Single, dic);
                DataResult<ProductEntity> dataResult = JsonConvert.DeserializeObject<DataResult<ProductEntity>>(result);
                entity = dataResult.Result;
            }

            entity = entity.IsNull() ? new ProductEntity() : entity;
            ViewBag.Entity = entity;

            ViewBag.Unit = DropDownHelper.GetUnit(entity.UnitNum,this.CompanyID);
            ViewBag.Cate = DropDownHelper.GetCate(entity.CateNum, this.CompanyID);
            ViewBag.Storage = DropDownHelper.GetStorage(entity.StorageNum, this.CompanyID);
            return View();
        }

        /// <summary>
        /// 选择产品对话框
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Dialog()
        {
            return View();
        }


        /// <summary>
        /// 选择产品对话框 带有数量 库存数
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Product()
        {
            return View();
        }

    }
}
