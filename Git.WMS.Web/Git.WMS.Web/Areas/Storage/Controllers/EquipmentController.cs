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
    public class EquipmentController : MasterPage
    {
        /// <summary>
        /// 设备管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 编辑或修改设备信息
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum",string.Empty);
            EquipmentEntity entity = null;
            ITopClient client = new TopClientDefault();

            if (!SnNum.IsEmpty())
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(EquipmentApiName.EquipmentApiName_Single, dic);
                DataResult<EquipmentEntity> dataResult = JsonConvert.DeserializeObject<DataResult<EquipmentEntity>>(result);
                entity = dataResult.Result;
            }

            entity = entity.IsNull() ? new EquipmentEntity() : entity;
            ViewBag.Entity = entity;

            ViewBag.EquipmentStatus = EnumHelper.GetOptions<EEquipmentStatus>(entity.Status);
            return View();
        }
    }
}
