using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Finance;
using Git.Storage.Provider.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Finance.Controllers
{
    public class CateController : Controller
    {
        /// <summary>
        /// 新增财务类别
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string CateName = WebUtil.GetFormValue<string>("CateName");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            FinanceCateEntity entity = new FinanceCateEntity();
            entity.SnNum = SnNum;
            entity.CateNum = CateNum;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CateName = CateName;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = CompanyID;

            FinanceCateProvider provider = new FinanceCateProvider(CompanyID);
            int line = provider.Add(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "财务类别新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "财务类别新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑财务类别
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string CateName = WebUtil.GetFormValue<string>("CateName");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            FinanceCateEntity entity = new FinanceCateEntity();
            entity.SnNum = SnNum;
            entity.CateNum = CateNum;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CateName = CateName;
            entity.CompanyID = CompanyID;

            FinanceCateProvider provider = new FinanceCateProvider(CompanyID);
            int line = provider.Update(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "财务类别修改成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "财务类别修改失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除财务类别
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            FinanceCateProvider provider = new FinanceCateProvider(CompanyID);
            int line = provider.Delete(list);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("财务类别删除成功,受影响行数{0}行", line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "财务类别删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询财务类别信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            FinanceCateProvider provider = new FinanceCateProvider(CompanyID);
            FinanceCateEntity entity = provider.GetSingle(SnNum);
            DataResult<FinanceCateEntity> result = new DataResult<FinanceCateEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询财务类别列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            FinanceCateProvider provider = new FinanceCateProvider(CompanyID);
            List<FinanceCateEntity> list = provider.GetList();
            DataListResult<FinanceCateEntity> result = new DataListResult<FinanceCateEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
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

            FinanceCateEntity entity = new FinanceCateEntity();
            entity.CateNum = CateNum;
            entity.CateName = CateName;

            PageInfo pageInfo = new PageInfo();
            pageInfo.PageIndex = PageIndex;
            pageInfo.PageSize = PageSize;

            FinanceCateProvider provider = new FinanceCateProvider(CompanyID);
            List<FinanceCateEntity> list = provider.GetList(entity, ref pageInfo);

            DataListResult<FinanceCateEntity> result = new DataListResult<FinanceCateEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list, PageInfo = pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }
    }
}
