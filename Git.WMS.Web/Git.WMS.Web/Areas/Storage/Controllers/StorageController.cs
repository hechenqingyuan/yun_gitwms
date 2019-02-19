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
    public class StorageController : MasterPage
    {
        /// <summary>
        /// 仓库管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.DepartNum = DropDownHelper.GetDepart(string.Empty, this.CompanyID);
            ViewBag.StorageType = EnumHelper.GetOptions<EStorageType>(0);
            return View();
        }

        /// <summary>
        /// 新增或编辑仓库
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);
            StorageEntity entity = null;
            ITopClient client = new TopClientDefault();

            if (!SnNum.IsEmpty())
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(StorageApiName.StorageApiName_Single, dic);
                DataResult<StorageEntity> dataResult = JsonHelper.DeserializeObject<DataResult<StorageEntity>>(result);
                entity = dataResult.Result;
            }

            entity = entity.IsNull() ? new StorageEntity() : entity;
            ViewBag.Entity = entity;

            ViewBag.DepartNum = DropDownHelper.GetDepart(entity.DepartNum, this.CompanyID);
            ViewBag.StorageType = EnumHelper.GetOptions<EStorageType>(entity.StorageType);
            return View();
        }
    }
}
