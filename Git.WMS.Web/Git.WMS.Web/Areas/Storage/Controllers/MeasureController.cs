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
    public class MeasureController : MasterPage
    {
        /// <summary>
        /// 单位列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 编辑或修改单位
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SN = WebUtil.GetQueryStringValue<string>("SN", string.Empty);
            MeasureEntity entity = null;
            ITopClient client = new TopClientDefault();

            if (!SN.IsEmpty())
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("SN", SN);

                string result = client.Execute(MeasureApiName.MeasureApiName_Single, dic);
                DataResult<MeasureEntity> dataResult = JsonConvert.DeserializeObject<DataResult<MeasureEntity>>(result);
                entity = dataResult.Result;
            }

            entity = entity.IsNull() ? new MeasureEntity() : entity;
            ViewBag.Entity = entity;

            return View();
        }

    }
}
