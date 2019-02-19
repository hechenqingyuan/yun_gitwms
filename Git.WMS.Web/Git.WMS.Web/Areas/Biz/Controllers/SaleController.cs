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
    public class SaleController : MasterPage
    {
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.OrderType = EnumHelper.GetOptions<EOrderType>(0);
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

            SaleOrderEntity entity = null;
            List<SaleDetailEntity> list = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(SaleApiName.SaleApiName_GetOrder, dic);
                DataResult<SaleOrderEntity> dataResult = JsonConvert.DeserializeObject<DataResult<SaleOrderEntity>>(result);
                entity = dataResult.Result;

                result = client.Execute(SaleApiName.SaleApiName_GetDetail, dic);
                DataResult<List<SaleDetailEntity>> dataList = JsonConvert.DeserializeObject<DataResult<List<SaleDetailEntity>>>(result);
                list = dataList.Result;
            }
            if (entity == null)
            {
                entity = new SaleOrderEntity();
                entity.CreateUser = this.LoginUser.UserNum;
                entity.CreateUserName = this.LoginUser.UserName;
                entity.OrderTime = DateTime.Now;
            }
            ViewBag.Entity = entity;

            list = list.IsNull() ? new List<SaleDetailEntity>() : list;
            Session[SessionKey.SESSION_SALEORDER_DETAIL] = list;

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

            SaleOrderEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(SaleApiName.SaleApiName_GetOrder, dic);
                DataResult<SaleOrderEntity> dataResult = JsonConvert.DeserializeObject<DataResult<SaleOrderEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new SaleOrderEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }

        /// <summary>
        /// 销售出库
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true, false)]
        public ActionResult OutStorage()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            SaleOrderEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(SaleApiName.SaleApiName_GetOrder, dic);
                DataResult<SaleOrderEntity> dataResult = JsonConvert.DeserializeObject<DataResult<SaleOrderEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new SaleOrderEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }

        /// <summary>
        /// 销售退货
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult ToReturn()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            SaleOrderEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(SaleApiName.SaleApiName_GetOrder, dic);
                DataResult<SaleOrderEntity> dataResult = JsonConvert.DeserializeObject<DataResult<SaleOrderEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new SaleOrderEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }
    }
}
