using Git.Framework.Controller;
using Git.Storage.Entity.Sys;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Newtonsoft.Json;
using System.Data;
using Git.Storage.Common.Excel;
using Git.Storage.Common.Enum;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;

namespace Git.WMS.Web.Areas.Storage.Controllers
{
    public class CarrierAjaxController : AjaxPage
    {
        /// <summary>
        /// 新增保存
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CarrierNum = WebUtil.GetFormValue<string>("CarrierNum");
            string CarrierName = WebUtil.GetFormValue<string>("CarrierName");
            string Remark = WebUtil.GetFormValue<string>("Remark");

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("SnNum", SnNum);
            dic.Add("CarrierNum", CarrierNum);
            dic.Add("CarrierName", CarrierName);
            dic.Add("Remark", Remark);
            dic.Add("CompanyID", this.CompanyID);

            string ApiName = CarrierApiName.CarrierApiName_Add;
            if (SnNum.IsNotEmpty())
            {
                ApiName = CarrierApiName.CarrierApiName_Edit;
            }

            string content = client.Execute(ApiName, dic);

            return Content(content);
        }
        /// <summary>
        /// 查询车辆
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string CarrierNum = WebUtil.GetFormValue<string>("CarrierNum");
            string CarrierName = WebUtil.GetFormValue<string>("CarrierName");

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", this.CompanyID);
            dic.Add("CarrierNum", CarrierNum);
            dic.Add("CarrierName", CarrierName);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());

            string content = client.Execute(CarrierApiName.CarrierApiName_GetList, dic);
            return Content(content);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            if (list.IsNullOrEmpty())
            {
                DataResult dataResult = new DataResult();
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "请选择要删除的项";
                return Content(JsonHelper.SerializeObject(dataResult));
            }

            string content = string.Empty;
            foreach (string SnNum in list)
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", this.CompanyID);
                dic.Add("SnNum", SnNum);
                content = client.Execute(CarrierApiName.CarrierApiName_Delete, dic);
            }
            return Content(content);
        }

    }
}
