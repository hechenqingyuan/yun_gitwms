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
    public class SysResourceController : Controller
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            int pageIndex = WebUtil.GetFormValue<int>("pageIndex", 1);
            int pageSize = WebUtil.GetFormValue<int>("pageSize", 15);
            string ResNum = WebUtil.GetFormValue<string>("ResNum", string.Empty);
            string ResName = WebUtil.GetFormValue<string>("ResName", string.Empty);
            string Url = WebUtil.GetFormValue<string>("Url", string.Empty);
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);

            PageInfo pageInfo = new PageInfo() { PageIndex = pageIndex, PageSize = pageSize };
            SysResourceProvider provider = new SysResourceProvider(CompanyID);
            SysResourceEntity entity = new SysResourceEntity();
            entity.ResNum = ResNum;
            entity.ResName = ResName;
            entity.Url = Url;

            List<SysResourceEntity> listResult = provider.GetList(entity, ref pageInfo);
            DataListResult<SysResourceEntity> result = new DataListResult<SysResourceEntity>()
            {
                Code = (int)EResponseCode.Success,
                PageInfo = pageInfo,
                Result = listResult,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(result));
        }
    }
}
