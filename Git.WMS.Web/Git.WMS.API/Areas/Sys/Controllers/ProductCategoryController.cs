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
    public class ProductCategoryController : Controller
    {
        /// <summary>
        /// 新增产品类别
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string CateName = WebUtil.GetFormValue<string>("CateName");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string CreateUser = WebUtil.GetFormValue<string>("CreateUser");
            string ParentNum = WebUtil.GetFormValue<string>("ParentNum");
            ProductCategoryEntity entity = new ProductCategoryEntity();
            
            entity.CateName = CateName;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CreateUser = CreateUser;
            entity.Remark = Remark;
            entity.ParentNum = ParentNum;
            entity.CompanyID = CompanyID;
            ProductCategoryProvider provider = new ProductCategoryProvider(CompanyID);
            int line = provider.Add(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "产品类别新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "产品类别新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑产品类别
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string CateName = WebUtil.GetFormValue<string>("CateName");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string CreateUser = WebUtil.GetFormValue<string>("CreateUser");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            ProductCategoryEntity entity = new ProductCategoryEntity();
            entity.SnNum = SnNum;
            entity.CateNum = CateNum;
            entity.CateName = CateName;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CreateUser = CreateUser;
            entity.Remark = Remark;
            entity.CompanyID = CompanyID;

            ProductCategoryProvider provider = new ProductCategoryProvider(CompanyID);
            int line = provider.Update(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "产品类别修改成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "产品类别修改失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除产品类别
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            ProductCategoryProvider provider = new ProductCategoryProvider(CompanyID);
            int line = provider.Delete(list);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("产品类别删除成功,受影响行数{0}行", line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "产品类别删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询产品类别信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            ProductCategoryProvider provider = new ProductCategoryProvider(CompanyID);
            ProductCategoryEntity entity = provider.GetSingle(SnNum);
            DataResult<ProductCategoryEntity> result = new DataResult<ProductCategoryEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询产品类别列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            ProductCategoryProvider provider = new ProductCategoryProvider(CompanyID);
            List<ProductCategoryEntity> list = provider.GetList();
            DataListResult<ProductCategoryEntity> result = new DataListResult<ProductCategoryEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
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

            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string CateName = WebUtil.GetFormValue<string>("CateName");
            string Remark = WebUtil.GetFormValue<string>("Remark");

            ProductCategoryEntity entity = new ProductCategoryEntity();
            entity.CateNum = CateNum;
            entity.CateName = CateName;
            entity.Remark = Remark;

            PageInfo pageInfo = new PageInfo();
            pageInfo.PageIndex = PageIndex;
            pageInfo.PageSize = PageSize;

            ProductCategoryProvider provider = new ProductCategoryProvider(CompanyID);
            List<ProductCategoryEntity> list = provider.GetList(entity, ref pageInfo);

            DataListResult<ProductCategoryEntity> result = new DataListResult<ProductCategoryEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list, PageInfo = pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }
        /// <summary>
        /// 查询所有的产品子类集合
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChildList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            ProductCategoryProvider provider = new ProductCategoryProvider(CompanyID);
            List<ProductCategoryEntity> list = provider.GetChildList(SnNum);

            DataResult<List<ProductCategoryEntity>> dataResult = new DataResult<List<ProductCategoryEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询族谱路径
        /// </summary>
        /// <returns></returns>
        public ActionResult GetParentList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            ProductCategoryProvider provider = new ProductCategoryProvider(CompanyID);
            List<ProductCategoryEntity> list = provider.GetParentList(SnNum);

            DataResult<List<ProductCategoryEntity>> dataResult = new DataResult<List<ProductCategoryEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;

            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
