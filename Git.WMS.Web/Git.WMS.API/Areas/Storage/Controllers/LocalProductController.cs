using Git.Framework.Controller;
using Git.Storage.Entity.Storage;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Provider.Sys;
using Git.Storage.Common;
using Git.Storage.Common.Enum;

namespace Git.WMS.API.Areas.Storage.Controllers
{
    public class LocalProductController : Controller
    {
        /// <summary>
        /// 查询所有的库存数据信息
        /// 包含所有仓库,库位,产品,批次的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);

            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);
            int LocalType = WebUtil.GetFormValue<int>("LocalType", 0);
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum", string.Empty);

            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            LocalProductEntity entity = new LocalProductEntity();
            entity.CompanyID = CompanyID;
            entity.StorageNum = StorageNum;
            entity.LocalType = LocalType;
            entity.LocalNum = LocalNum;
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.BatchNum = BatchNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            LocalProductProvider provider = new LocalProductProvider(CompanyID);
            List<LocalProductEntity> listResult = provider.GetList(entity, ref pageInfo);

            DataListResult<LocalProductEntity> dataResult = new DataListResult<LocalProductEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 可出库的产品库存分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOutAbleList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);

            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum", string.Empty);
            int LocalType = WebUtil.GetFormValue<int>("LocalType", 0);

            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            LocalProductEntity entity = new LocalProductEntity();
            entity.CompanyID = CompanyID;
            entity.StorageNum = StorageNum;
            entity.LocalNum = LocalNum;
            entity.LocalType = LocalType;
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.BatchNum = BatchNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            LocalProductProvider provider = new LocalProductProvider(CompanyID);
            List<LocalProductEntity> listResult = provider.GetOutAbleList(entity, ref pageInfo);

            DataListResult<LocalProductEntity> dataResult = new DataListResult<LocalProductEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询可用于报损的仓库库存数据分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBadAbleList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);

            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum", string.Empty);
            int LocalType = WebUtil.GetFormValue<int>("LocalType", 0);

            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            LocalProductEntity entity = new LocalProductEntity();
            entity.CompanyID = CompanyID;
            entity.StorageNum = StorageNum;
            entity.LocalNum = LocalNum;
            entity.LocalType = LocalType;
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.BatchNum = BatchNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            LocalProductProvider provider = new LocalProductProvider(CompanyID);
            List<LocalProductEntity> listResult = provider.GetBadAbleList(entity, ref pageInfo);
            DataListResult<LocalProductEntity> dataResult = new DataListResult<LocalProductEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询所有的仓库库存数据总和
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLocalProduct()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);

            string BarCode = WebUtil.GetFormValue<string>("BarCode", string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName", string.Empty);
            string Size = WebUtil.GetFormValue<string>("Size", string.Empty);
            string CateNum = WebUtil.GetFormValue<string>("CateNum", string.Empty);

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            V_LocalProductEntity entity = new V_LocalProductEntity();
            entity.CompanyID = CompanyID;
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.Size = Size;
            entity.CateNum = CateNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            LocalProductProvider provider = new LocalProductProvider(CompanyID);
            List<V_LocalProductEntity> listResult = provider.GetList(entity, ref pageInfo);

            DataListResult<V_LocalProductEntity> dataResult = new DataListResult<V_LocalProductEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
