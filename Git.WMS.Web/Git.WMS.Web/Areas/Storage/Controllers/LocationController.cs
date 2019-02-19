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
    public class LocationController : MasterPage
    {
        /// <summary>
        ///  库位管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");
            ViewBag.Storage = DropDownHelper.GetStorage(SnNum, this.CompanyID);
            
            return View();
        }

        /// <summary>
        /// 新增或编辑库位
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string StorageNum = WebUtil.GetQueryStringValue<string>("StorageNum");
            string LocalNum = WebUtil.GetQueryStringValue<string>("SnNum", string.Empty);
            LocationEntity entity = null;
            ITopClient client = new TopClientDefault();

            if (!LocalNum.IsEmpty())
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("LocalNum", LocalNum);

                string result = client.Execute(LocationApiName.LocationApiName_Single, dic);
                DataResult<LocationEntity> dataResult = JsonConvert.DeserializeObject<DataResult<LocationEntity>>(result);
                entity = dataResult.Result;
            }

            entity = entity.IsNull() ? new LocationEntity() : entity;
            ViewBag.Entity = entity;
            entity.StorageNum = entity.StorageNum.IsEmpty() ? StorageNum : entity.StorageNum;
            ViewBag.LocalType = EnumHelper.GetOptions<ELocalType>(entity.LocalType);
            ViewBag.Storage = DropDownHelper.GetStorage(entity.StorageNum, this.CompanyID);
            return View();
        }

        /// <summary>
        /// 选择库位对话框
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Dialog()
        {
            ViewBag.LocalType = EnumHelper.GetOptions<ELocalType>(0);
            ViewBag.Storage = DropDownHelper.GetStorage(this.DefaultStorageNum, this.CompanyID);
            return View();
        }
    }
}
