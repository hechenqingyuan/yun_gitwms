using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Bad;
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
    public class BadController : MasterPage
    {
        /// <summary>
        /// 报损单分页列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.BadType = EnumHelper.GetOptions<EBadType>(0);
            return View();
        }

        /// <summary>
        /// 新增或编辑报损单
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            BadReportEntity entity = null;
            List<BadReportDetailEntity> list = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(BadApiName.BadApiName_GetOrder, dic);
                DataResult<BadReportEntity> dataResult = JsonConvert.DeserializeObject<DataResult<BadReportEntity>>(result);
                entity = dataResult.Result;

                result = client.Execute(BadApiName.BadApiName_GetDetail, dic);
                DataResult<List<BadReportDetailEntity>> dataList = JsonConvert.DeserializeObject<DataResult<List<BadReportDetailEntity>>>(result);
                list = dataList.Result;
            }
            if (entity.IsNull())
            {
                entity = new BadReportEntity();
                entity.CreateUser = this.LoginUser.UserNum;
                entity.CreateUserName = this.LoginUser.UserName;
                entity.CreateTime = DateTime.Now;
            }
            ViewBag.Entity = entity;

            list = list.IsNull() ? new List<BadReportDetailEntity>() : list;
            Session[SessionKey.SESSION_BAD_DETAIL] = list;

            ViewBag.BadType = EnumHelper.GetOptions<EBadType>(entity.BadType);
            return View();
        }

        /// <summary>
        /// 报损单详细
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Detail()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            BadReportEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(BadApiName.BadApiName_GetOrder, dic);
                DataResult<BadReportEntity> dataResult = JsonConvert.DeserializeObject<DataResult<BadReportEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new BadReportEntity() : entity;
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
            ViewBag.Location = DropDownHelper.GetLocation(this.DefaultStorageNum, string.Empty, this.CompanyID, new List<int>() { (int)ELocalType.Normal, (int)ELocalType.WaitIn,(int)ELocalType.WaitCheck,(int)ELocalType.WaitOut });
            return View();
        }
    }
}
