using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Check;
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

namespace Git.WMS.Web.Areas.Order.Controllers
{
    public class CheckController : MasterPage
    {
        [LoginFilter]
        public ActionResult List()
        {
            //ViewBag.CheckType = EnumHelper.GetOptions<ECheckType>(0);
            return View();
        }

        /// <summary>
        /// 新增或编辑盘点单
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,true)]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            InventoryOrderEntity entity = null;
            List<InventoryDetailEntity> list = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(CheckApiName.CheckApiName_GetOrder, dic);
                DataResult<InventoryOrderEntity> dataResult = JsonConvert.DeserializeObject<DataResult<InventoryOrderEntity>>(result);
                entity = dataResult.Result;

                result = client.Execute(CheckApiName.CheckApiName_GetDetail, dic);
                DataResult<List<InventoryDetailEntity>> dataList = JsonConvert.DeserializeObject<DataResult<List<InventoryDetailEntity>>>(result);
                list = dataList.Result;
            }
            if (entity == null)
            {
                entity = new InventoryOrderEntity();
                entity.CreateUser = this.LoginUser.UserNum;
                entity.CreateUserName = this.LoginUser.UserName;
            }
            ViewBag.Entity = entity;

            list = list.IsNull() ? new List<InventoryDetailEntity>() : list;
            Session[SessionKey.SESSION_CHECK_DETAIL] = list;

            return View();
        }

        /// <summary>
        /// 盘点单详细
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Detail()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            InventoryOrderEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(CheckApiName.CheckApiName_GetOrder, dic);
                DataResult<InventoryOrderEntity> dataResult = JsonConvert.DeserializeObject<DataResult<InventoryOrderEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new InventoryOrderEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }


        /// <summary>
        /// 盘差数据上传
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Upload()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            InventoryOrderEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(CheckApiName.CheckApiName_GetOrder, dic);
                DataResult<InventoryOrderEntity> dataResult = JsonConvert.DeserializeObject<DataResult<InventoryOrderEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new InventoryOrderEntity() : entity;
            ViewBag.Entity = entity;

            //加载默认的库位
            string LocationStr = DropDownHelper.GetLocation(this.DefaultStorageNum, "", this.CompanyID);
            ViewBag.LocationStr = LocationStr;
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
