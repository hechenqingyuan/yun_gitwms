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
using Git.Storage.Entity.OutStorage;

namespace Git.WMS.Web.Areas.Order.Controllers
{
    public class OutStorageController : MasterPage
    {
        /// <summary>
        /// 出库单分页列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.OutType = EnumHelper.GetOptions<EOutType>(0);
            return View();
        }

        /// <summary>
        /// 新增或编辑出库单
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            OutStorageEntity entity = null;
            List<OutStoDetailEntity> list = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(OutStorageApiName.OutStorageApiName_GetOrder, dic);
                DataResult<OutStorageEntity> dataResult = JsonConvert.DeserializeObject<DataResult<OutStorageEntity>>(result);
                entity = dataResult.Result;

                result = client.Execute(OutStorageApiName.OutStorageApiName_GetDetail, dic);
                DataResult<List<OutStoDetailEntity>> dataList = JsonConvert.DeserializeObject<DataResult<List<OutStoDetailEntity>>>(result);
                list = dataList.Result;
            }
            if (entity.IsNull())
            {
                entity = new OutStorageEntity();
                entity.CreateUser = this.LoginUser.UserNum;
                entity.CreateUserName = this.LoginUser.UserName;
                entity.CreateTime = DateTime.Now;
                entity.SendDate = DateTime.Now;
            }
            ViewBag.Entity = entity;

            list = list.IsNull() ? new List<OutStoDetailEntity>() : list;
            Session[SessionKey.SESSION_OUTSTORAGE_DETAIL] = list;

            ViewBag.OutType = EnumHelper.GetOptions<EOutType>(entity.OutType);
            return View();
        }

        /// <summary>
        /// 出库单详细
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Detail()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            OutStorageEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(OutStorageApiName.OutStorageApiName_GetOrder, dic);
                DataResult<OutStorageEntity> dataResult = JsonConvert.DeserializeObject<DataResult<OutStorageEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new OutStorageEntity() : entity;
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
            ViewBag.Location = DropDownHelper.GetLocation(this.DefaultStorageNum, string.Empty, this.CompanyID, new List<int>() { (int)ELocalType.Normal,(int)ELocalType.WaitIn});
            return View();
        }

        /// <summary>
        /// 设置物流
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true, false)]
        public ActionResult Carrier()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            OutStorageEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(OutStorageApiName.OutStorageApiName_GetOrder, dic);
                DataResult<OutStorageEntity> dataResult = JsonConvert.DeserializeObject<DataResult<OutStorageEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new OutStorageEntity() : entity;
            ViewBag.Entity = entity;
            ViewBag.Carrier = DropDownHelper.GetCarrier(entity.CarrierNum, this.CompanyID);
            return View();
        }

    }
}
