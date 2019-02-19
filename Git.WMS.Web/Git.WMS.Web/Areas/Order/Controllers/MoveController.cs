using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Move;
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
    public class MoveController : MasterPage
    {
        /// <summary>
        /// 移库单分页列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.MoveType = EnumHelper.GetOptions<EMoveType>(0);
            return View();
        }

        /// <summary>
        /// 新增或编辑移库单
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            MoveOrderEntity entity = null;
            List<MoveOrderDetailEntity> list = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(MoveApiName.MoveApiName_GetOrder, dic);
                DataResult<MoveOrderEntity> dataResult = JsonConvert.DeserializeObject<DataResult<MoveOrderEntity>>(result);
                entity = dataResult.Result;

                result = client.Execute(MoveApiName.MoveApiName_GetDetail, dic);
                DataResult<List<MoveOrderDetailEntity>> dataList = JsonConvert.DeserializeObject<DataResult<List<MoveOrderDetailEntity>>>(result);
                list = dataList.Result;
            }
            if (entity.IsNull())
            {
                entity = new MoveOrderEntity();
                entity.CreateUser = this.LoginUser.UserNum;
                entity.CreateUserName = this.LoginUser.UserName;
                entity.CreateTime = DateTime.Now;
            }
            ViewBag.Entity = entity;

            list = list.IsNull() ? new List<MoveOrderDetailEntity>() : list;
            Session[SessionKey.SESSION_MOVE_DETAIL] = list;

            ViewBag.MoveType = EnumHelper.GetOptions<EMoveType>(entity.MoveType);
            return View();
        }

        /// <summary>
        /// 移库单详细
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Detail()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            MoveOrderEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(MoveApiName.MoveApiName_GetOrder, dic);
                DataResult<MoveOrderEntity> dataResult = JsonConvert.DeserializeObject<DataResult<MoveOrderEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new MoveOrderEntity() : entity;
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
            ViewBag.Location = DropDownHelper.GetLocation(this.DefaultStorageNum, string.Empty, this.CompanyID);
            return View();
        }
    }
}
