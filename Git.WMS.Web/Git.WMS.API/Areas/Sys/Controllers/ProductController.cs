using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Storage.Provider;
using Git.Storage.Provider.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Sys.Controllers
{
    public class ProductController : Controller
    {
        /// <summary>
        /// 新增产品
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            ProductEntity entity = WebUtil.GetFormObject<ProductEntity>("Entity");
            ProductProvider provider = new ProductProvider(CompanyID);
            
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            int line = provider.Add(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "产品新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "产品新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑产品
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            ProductEntity entity = WebUtil.GetFormObject<ProductEntity>("Entity");

            ProductProvider provider = new ProductProvider(CompanyID);
            int line = provider.Update(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "产品修改成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "产品修改失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据产品唯一编号批量删除产品
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            ProductProvider provider = new ProductProvider(CompanyID);
            int line = provider.Delete(list);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("产品删除成功,受影响行数{0}行", line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "产品删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据产品唯一编号删除产品
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSingle()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            ProductProvider provider = new ProductProvider(CompanyID);
            int line = provider.Delete(SnNum);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("产品删除成功,受影响行数{0}行", line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "产品删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询产品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            ProductProvider provider = new ProductProvider(CompanyID);
            ProductEntity entity = provider.GetProduct(SnNum);
            DataResult<ProductEntity> result = new DataResult<ProductEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询产品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            ProductProvider provider = new ProductProvider(CompanyID);
            List<ProductEntity> list = provider.GetList();
            DataListResult<ProductEntity> result = new DataListResult<ProductEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPage()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string FactoryNum = WebUtil.GetFormValue<string>("FactoryNum");
            string InCode = WebUtil.GetFormValue<string>("InCode");
            string UnitNum = WebUtil.GetFormValue<string>("UnitNum");
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string Size = WebUtil.GetFormValue<string>("Size");
            int IsSingle = WebUtil.GetFormValue<int>("IsSingle",0);
            

            ProductEntity entity = new ProductEntity();
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.FactoryNum = FactoryNum;
            entity.InCode = InCode;
            entity.UnitNum = UnitNum;
            entity.CateNum = CateNum;
            entity.Size = Size;
            entity.IsSingle = IsSingle;

            PageInfo pageInfo = new PageInfo();
            pageInfo.PageIndex = PageIndex;
            pageInfo.PageSize = PageSize;
            ProductProvider provider = new ProductProvider(CompanyID);
            List<ProductEntity> list = provider.GetList(entity, ref pageInfo);
            DataListResult<ProductEntity> result = new DataListResult<ProductEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list, PageInfo = pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }


        /// <summary>
        /// 条码扫描获得产品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Scan()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            ProductProvider provider = new ProductProvider(CompanyID);
            ProductEntity entity = provider.Scan(BarCode);
            DataResult<ProductEntity> result = new DataResult<ProductEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 根据关键字搜索产品集合
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSearch()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string KeyWord = WebUtil.GetFormValue<string>("KeyWord");
            ProductProvider provider = new ProductProvider(CompanyID);
            List<ProductEntity> listResult = provider.GetSearch(KeyWord);

            DataResult<List<ProductEntity>> dataResult = new DataResult<List<ProductEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = listResult;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据产品的唯一编号批量查找产品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductListBySn()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string[] items = WebUtil.GetFormObject<string[]>("Items");
            ProductProvider provider = new ProductProvider(CompanyID);
            List<ProductEntity> listResult = provider.GetProductListBySn(items);

            DataResult<List<ProductEntity>> dataResult = new DataResult<List<ProductEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = listResult;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据产品的条码编号批量查询产品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductListByBarCode()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string[] items = WebUtil.GetFormObject<string[]>("Items");
            ProductProvider provider = new ProductProvider(CompanyID);
            List<ProductEntity> listResult = provider.GetProductListByBarCode(items);

            DataResult<List<ProductEntity>> dataResult = new DataResult<List<ProductEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = listResult;

            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
