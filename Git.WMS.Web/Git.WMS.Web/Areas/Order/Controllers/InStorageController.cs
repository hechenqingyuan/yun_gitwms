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
using Newtonsoft.Json;
using Git.Storage.Common;
using System.Data;
using Git.Storage.Common.Excel;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.InStorage;

namespace Git.WMS.Web.Areas.Order.Controllers
{
    public class InStorageController : MasterPage
    {
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.InType = EnumHelper.GetOptions<EInType>(0);
            return View();
        }

        /// <summary>
        /// 新增或编辑入库单
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            InStorageEntity entity = null;
            List<InStorDetailEntity> list = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(InStorageApiName.InStorageApiName_GetOrder, dic);
                DataResult<InStorageEntity> dataResult = JsonConvert.DeserializeObject<DataResult<InStorageEntity>>(result);
                entity = dataResult.Result;

                result = client.Execute(InStorageApiName.InStorageApiName_GetDetail, dic);
                DataResult<List<InStorDetailEntity>> dataList = JsonConvert.DeserializeObject<DataResult<List<InStorDetailEntity>>>(result);
                list = dataList.Result;
            }
            if (entity == null)
            {
                entity = new InStorageEntity();
                entity.CreateUser = this.LoginUser.UserNum;
                entity.CreateUserName = this.LoginUser.UserName;
                entity.OrderTime = DateTime.Now;
            }
            ViewBag.Entity = entity;

            list = list.IsNull() ? new List<InStorDetailEntity>() : list;
            Session[SessionKey.SESSION_INSTORAGE_DETAIL] = list;

            ViewBag.InType = EnumHelper.GetOptions<EInType>(entity.InType);
            return View();
        }

        /// <summary>
        /// 入库单详细
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Detail()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            InStorageEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(InStorageApiName.InStorageApiName_GetOrder, dic);
                DataResult<InStorageEntity> dataResult = JsonConvert.DeserializeObject<DataResult<InStorageEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new InStorageEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }

        /// <summary>
        /// 新增产品对话框
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult AddProduct()
        {
            return View();
        }
    }
}
