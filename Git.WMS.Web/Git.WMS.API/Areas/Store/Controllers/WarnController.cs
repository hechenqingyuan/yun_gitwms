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
    public class WarnController : Controller
    {
        /// <summary>
        /// 查询库存预警清单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);

            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);
            string SupNum = WebUtil.GetFormValue<string>("SupNum", string.Empty);

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            V_StorageProductEntity entity = new V_StorageProductEntity();
            entity.CompanyID = CompanyID;
            entity.StorageNum = StorageNum;
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            ProductWarnProvider provider = new ProductWarnProvider(CompanyID);
            List<V_StorageProductEntity> listResult = provider.GetList(entity,ref pageInfo);

            DataListResult<V_StorageProductEntity> dataResult = new DataListResult<V_StorageProductEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = listResult;
            dataResult.PageInfo = pageInfo;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

    }
}
