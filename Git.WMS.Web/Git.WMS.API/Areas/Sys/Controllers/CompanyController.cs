using Git.Framework.Controller;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Sys;
using Git.Storage.Provider.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;

namespace Git.WMS.API.Areas.Sys.Controllers
{
    public class CompanyController : Controller
    {
        /// <summary>
        /// 查询公司信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSingle()
        {
            DataResult<CompanyEntity> result = null;
            string CompanyNum = WebUtil.GetFormValue<string>("CompanyNum",string.Empty);
            if (CompanyNum.IsEmpty())
            {
                result = new DataResult<CompanyEntity>() { Code = (int)EResponseCode.Exception, Message = "公司编号为空" };
                return Content(JsonHelper.SerializeObject(result));
            }

            CompanyProvider provider = new CompanyProvider();
            CompanyEntity entity = provider.GetSingle(CompanyNum);
            result = new DataResult<CompanyEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑公司信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string CompanyName = WebUtil.GetFormValue<string>("CompanyName");

            CompanyProvider provider = new CompanyProvider();
            CompanyEntity entity = new CompanyEntity();
            entity.CompanyID = CompanyID;
            entity.CompanyName = CompanyName;

            int line = provider.Update(entity);

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
        /// 新增公司信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string CompanyNum = WebUtil.GetFormValue<string>("CompanyNum");
            string CompanyName = WebUtil.GetFormValue<string>("CopanyName");
            string CreateUser = WebUtil.GetFormValue<string>("CreateUser");

            CompanyEntity entity = new CompanyEntity();
            entity.CompanyNum = CompanyNum;
            entity.CompanyName = CompanyName;
            entity.CreateUser = CreateUser;

            CompanyProvider provider = new CompanyProvider();
            int line = provider.Add(entity);
            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "新增成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "新增失败";
            }

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            CompanyProvider provider = new CompanyProvider();
            int line = provider.Delete(CompanyID);
            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "删除成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "删除失败";
            }

            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 查询公司分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex");
            int PageSize = WebUtil.GetFormValue<int>("PageSize");

            CompanyProvider provider = new CompanyProvider();
            PageInfo pageInfo = new PageInfo() { PageIndex=PageIndex,PageSize=PageSize };
            CompanyEntity entity = new CompanyEntity();
            List<CompanyEntity> listResult = provider.GetList(entity,ref pageInfo);
            DataListResult<CompanyEntity> dataResult = new DataListResult<CompanyEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = listResult;
            dataResult.PageInfo = pageInfo;

            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
