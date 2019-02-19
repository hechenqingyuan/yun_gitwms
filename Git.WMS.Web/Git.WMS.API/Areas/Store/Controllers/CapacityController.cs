using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Storage.Provider.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Store.Controllers
{
    public class CapacityController : Controller
    {
        /// <summary>
        /// 库存容量查询
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);

            string LocalName = WebUtil.GetFormValue<string>("LocalName", string.Empty);
            int LocalType = WebUtil.GetFormValue<int>("LocalType", 0);

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            V_LocalCapacityEntity entity = new V_LocalCapacityEntity();
            entity.CompanyID = CompanyID;
            entity.StorageNum = StorageNum;
            entity.LocalName = LocalName;
            entity.LocalType = LocalType;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            StoreCapacityProvider provider = new StoreCapacityProvider(CompanyID);
            List<V_LocalCapacityEntity> listResult = provider.GetList(entity, ref pageInfo);

            DataListResult<V_LocalCapacityEntity> dataResult = new DataListResult<V_LocalCapacityEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = listResult;
            dataResult.PageInfo = pageInfo;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

    }
}
