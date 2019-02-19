using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Sku;
using Git.Storage.Entity.Storage;
using Git.Storage.Provider;
using Git.Storage.Provider.Sku;
using Git.Storage.Provider.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Sys.Controllers
{
    public class SkuController : Controller
    {
        /// <summary>
        /// 新增产品以及产品SKU
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            ProductEntity entity = WebUtil.GetFormObject<ProductEntity>("Entity");
            List<ProductSkuEntity> listSku = WebUtil.GetFormObject<List<ProductSkuEntity>>("ListSku");

            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);
            int line = provider.Add(entity,listSku);

            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "新增成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "新增失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 编辑产品以及产品SKU
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            ProductEntity entity = WebUtil.GetFormObject<ProductEntity>("Entity");
            List<ProductSkuEntity> listSku = WebUtil.GetFormObject<List<ProductSkuEntity>>("ListSku");

            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);
            int line = provider.Edit(entity, listSku);

            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "编辑成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "编辑失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 编辑产品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProduct()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            ProductEntity entity = WebUtil.GetFormObject<ProductEntity>("Entity");

            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);
            int line = provider.EditProduct(entity);

            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "编辑成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "编辑失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 编辑产品SKU
        /// </summary>
        /// <returns></returns>
        public ActionResult EditSku()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            ProductSkuEntity entity = WebUtil.GetFormObject<ProductSkuEntity>("Entity");

            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);
            int line = provider.EditSku(entity);

            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "编辑成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "编辑失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据产品唯一编号删除产品
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSingle()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);
            int line = provider.DeleteProduct(SnNum);

            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "删除成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "删除失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据产品的唯一编号批量删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);
            int line = provider.DeleteProduct(list);

            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "删除成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "删除失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据SKU的唯一编号删除SKU
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSkuSingle()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);
            int line = provider.DeleteSku(SnNum);

            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "删除成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "删除失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据SKU唯一编号批量删除SKU信息
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSku()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);
            int line = provider.DeleteSku(list);

            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "删除成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "删除失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据产品SKU唯一编号查询SKU
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSku()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);

            ProductSkuEntity entity = provider.GetSku(SnNum);

            DataResult<ProductSkuEntity> dataResult = new DataResult<ProductSkuEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = entity;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据产品的唯一编号查询SKU信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSkuList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string ProductNum = WebUtil.GetFormValue<string>("ProductNum");
            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);

            List<ProductSkuEntity> listResult = provider.GetSkuList(ProductNum);

            DataResult<List<ProductSkuEntity>> dataResult = new DataResult<List<ProductSkuEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = listResult;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据SKU唯一编号查询产品SKU全部信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductSku()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);
            ProductEntity entity = provider.GetProductSku(SnNum);

            DataResult<ProductEntity> dataResult = new DataResult<ProductEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = entity;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据产品SKU的编码查询产品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSkuBarCode()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);
            ProductEntity entity = provider.GetSkuBarCode(BarCode);

            DataResult<ProductEntity> dataResult = new DataResult<ProductEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = entity;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询产品SKU信息分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string FactoryNum = WebUtil.GetFormValue<string>("FactoryNum");
            string InCode = WebUtil.GetFormValue<string>("InCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string UnitNum = WebUtil.GetFormValue<string>("UnitNum");
            string Color = WebUtil.GetFormValue<string>("Color");
            string Size = WebUtil.GetFormValue<string>("Size");
            string CusNum = WebUtil.GetFormValue<string>("CusNum");
            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string CusName = WebUtil.GetFormValue<string>("CusName");
            string SupName = WebUtil.GetFormValue<string>("SupName");

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex",1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize",10);

            ProductEntity entity = new ProductEntity();
            entity.CompanyID = CompanyID;
            entity.BarCode = BarCode;
            entity.FactoryNum = FactoryNum;
            entity.InCode = InCode;
            entity.ProductName = ProductName;
            entity.CateNum = CateNum;
            entity.UnitNum = UnitNum;
            entity.Color = Color;
            entity.Size = Size;
            entity.CusNum = CusNum;
            entity.SupNum = SupNum;
            entity.CusName = CusName;
            entity.SupName = SupName;

            PageInfo pageInfo = new PageInfo() { PageIndex=PageIndex,PageSize=PageSize };
            ProductSkuProvider provider = new ProductSkuProvider(CompanyID);
            List<ProductEntity> listResult = provider.GetSkuList(entity, ref pageInfo);

            DataListResult<ProductEntity> dataResult = new DataListResult<ProductEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = listResult;
            dataResult.PageInfo = pageInfo;

            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
