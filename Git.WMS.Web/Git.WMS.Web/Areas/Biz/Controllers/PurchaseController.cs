using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Biz;
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

namespace Git.WMS.Web.Areas.Biz.Controllers
{
    public class PurchaseController : MasterPage
    {
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.OrderType = EnumHelper.GetOptions<EPurchaseType>(0);
            return View();
        }

        /// <summary>
        /// 新增或编辑销售订单
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            PurchaseEntity entity = null;
            List<PurchaseDetailEntity> list = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(PurchaseApiName.PurchaseApiName_GetOrder, dic);
                DataResult<PurchaseEntity> dataResult = JsonConvert.DeserializeObject<DataResult<PurchaseEntity>>(result);
                entity = dataResult.Result;

                result = client.Execute(PurchaseApiName.PurchaseApiName_GetDetail, dic);
                DataResult<List<PurchaseDetailEntity>> dataList = JsonConvert.DeserializeObject<DataResult<List<PurchaseDetailEntity>>>(result);
                list = dataList.Result;
            }
            if (entity == null)
            {
                entity = new PurchaseEntity();
                entity.CreateUser = this.LoginUser.UserNum;
                entity.CreateUserName = this.LoginUser.UserName;
                entity.OrderTime = DateTime.Now;
            }
            ViewBag.Entity = entity;

            list = list.IsNull() ? new List<PurchaseDetailEntity>() : list;
            Session[SessionKey.SESSION_PURCHASE_DETAIL] = list;

            entity.OrderType = entity.OrderType == 0 ? (int)EOrderType.Really : entity.OrderType;
            ViewBag.OrderType = EnumHelper.GetOptions<EOrderType>(entity.OrderType);
            return View();
        }

        /// <summary>
        /// 销售订单详细
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Detail()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            PurchaseEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(PurchaseApiName.PurchaseApiName_GetOrder, dic);
                DataResult<PurchaseEntity> dataResult = JsonConvert.DeserializeObject<DataResult<PurchaseEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new PurchaseEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }

        /// <summary>
        /// 采购退货
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true, false)]
        public ActionResult ToReturn()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            PurchaseEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(PurchaseApiName.PurchaseApiName_GetOrder, dic);
                DataResult<PurchaseEntity> dataResult = JsonConvert.DeserializeObject<DataResult<PurchaseEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new PurchaseEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }

        /// <summary>
        /// 采购入库
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true, false)]
        public ActionResult InStorage()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            PurchaseEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(PurchaseApiName.PurchaseApiName_GetOrder, dic);
                DataResult<PurchaseEntity> dataResult = JsonConvert.DeserializeObject<DataResult<PurchaseEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new PurchaseEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }


        /// <summary>
        /// 选择产品
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true, false)]
        public ActionResult AddProduct()
        {
            return View();
        }
    }
}
