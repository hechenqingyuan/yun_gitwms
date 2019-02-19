using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Storage.Entity.Sys;
using Git.Storage.Provider.Base;
using Git.Storage.Provider.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Sys.Controllers
{
    public class SupplierController : Controller
    {
        /// <summary>
        /// 新增供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            SupplierEntity entity = WebUtil.GetFormObject<SupplierEntity>("Entity");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            entity.CompanyID = CompanyID;
            
            SupplierProvider provider = new SupplierProvider(CompanyID);
            int line = provider.Add(entity);
            DataResult result = new DataResult();

            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "供应商新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "供应商新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            SupplierEntity entity = WebUtil.GetFormObject<SupplierEntity>("Entity");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            SupplierProvider provider = new SupplierProvider(CompanyID);
            int line = provider.Update(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "供应商修改成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "供应商修改失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            SupplierProvider provider = new SupplierProvider(CompanyID);
            int line = provider.Delete(list);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("供应商删除成功,受影响行数{0}行", line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "供应商删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询供应商信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            SupplierProvider provider = new SupplierProvider(CompanyID);
            SupplierEntity entity = provider.GetSupplier(SnNum);
            DataResult<SupplierEntity> result = new DataResult<SupplierEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询供应商列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            SupplierProvider provider = new SupplierProvider(CompanyID);
            List<SupplierEntity> list = provider.GetList();
            DataListResult<SupplierEntity> result = new DataListResult<SupplierEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
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
            string SupName = WebUtil.GetFormValue<string>("SupName", string.Empty);
            string SupNum = WebUtil.GetFormValue<string>("SupNum", string.Empty);
            string Phone = WebUtil.GetFormValue<string>("Phone", string.Empty);
            int SupType = WebUtil.GetFormValue<int>("SupType",-1);
            SupplierEntity entity = new SupplierEntity();
            entity.SupName = SupName;
            entity.SupNum = SupNum;
            entity.SupType = SupType;
            entity.Phone = Phone;
            PageInfo pageInfo = new PageInfo();
            pageInfo.PageIndex = PageIndex;
            pageInfo.PageSize = PageSize;
            SupplierProvider provider = new SupplierProvider(CompanyID);
            List<SupplierEntity> list = provider.GetList(entity, ref pageInfo);
            DataListResult<SupplierEntity> result = new DataListResult<SupplierEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list, PageInfo = pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 搜索供应商信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchSupplier()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string Keyword = WebUtil.GetFormValue<string>("Keyword");
            int TopSize = WebUtil.GetFormValue<int>("TopSize");
            SupplierProvider provider = new SupplierProvider(CompanyID);
            List<SupplierEntity> list = provider.SearchSupplier(Keyword, TopSize);
            DataListResult<SupplierEntity> result = new DataListResult<SupplierEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
            return Content(JsonHelper.SerializeObject(result));
        }
    }
}
