using Git.Framework.Controller;
using Git.Storage.Provider.Report;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Entity.Report;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Git.WMS.API.Areas.Report.Controllers
{
    public class BalanceBookController : Controller
    {
        /// <summary>
        /// 查询期初期末数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum");
            string Day = WebUtil.GetFormValue<string>("Day");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");

            BalanceBookEntity entity = new BalanceBookEntity();
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.BatchNum = BatchNum;
            entity.Day = Day;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.StorageNum = StorageNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            BalanceBookProvider provider = new BalanceBookProvider(CompanyID);
            List<BalanceBookEntity> listResult = provider.GetList(entity,ref pageInfo);
            DataListResult<BalanceBookEntity> dataResult = new DataListResult<BalanceBookEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = listResult;
            dataResult.PageInfo = pageInfo;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询期初期末
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSingle()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            BalanceBookProvider provider = new BalanceBookProvider(CompanyID);
            BalanceBookEntity result = provider.GetSingle(SnNum);

            DataResult<BalanceBookEntity> dataResult = new DataResult<BalanceBookEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = result;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

    }
}
