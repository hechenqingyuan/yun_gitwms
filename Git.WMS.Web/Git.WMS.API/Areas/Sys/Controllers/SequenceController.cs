using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Sys;
using Git.Storage.Provider.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Sys.Controllers
{
    public class SequenceController : Controller
    {
        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            int pageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int pageSize = WebUtil.GetFormValue<int>("PageSize", 15);
            string TabName = WebUtil.GetFormValue<string>("TabName", string.Empty);
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            PageInfo pageInfo = new PageInfo() { PageIndex = pageIndex, PageSize = pageSize };
            SequenceProvider provider = new SequenceProvider(CompanyID);
            SequenceEntity entity = new SequenceEntity();
            entity.TabName = TabName;
            entity.CompanyID = CompanyID;
            List<SequenceEntity> listResult = provider.GetList(entity, ref pageInfo);
            DataListResult<SequenceEntity> result = new DataListResult<SequenceEntity>()
            {
                Code = (int)EResponseCode.Success,
                PageInfo = pageInfo,
                Result = listResult,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑标识符规则
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            SequenceEntity entity = WebUtil.GetFormObject<SequenceEntity>("Entity");
            SequenceProvider provider = new SequenceProvider(CompanyID);
            int line = provider.Update(entity);

            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "保存成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "保存失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
