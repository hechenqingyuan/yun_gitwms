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
    public class DepartController : Controller
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string DepartName = WebUtil.GetFormValue<string>("DepartName");
            string ParentNum = WebUtil.GetFormValue<string>("ParentNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            SysDepartEntity entity = new SysDepartEntity();
            entity.SnNum = SnNum;
            entity.DepartName = DepartName;
            entity.ChildCount = 0;
            entity.ParentNum = ParentNum;
            entity.Depth = 0;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = CompanyID;

            DepartProvider provider = new DepartProvider(CompanyID);
            int line = provider.Add(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "部门新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "部门新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string DepartName = WebUtil.GetFormValue<string>("DepartName");
            string ParentNum = WebUtil.GetFormValue<string>("ParentNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            SysDepartEntity entity = new SysDepartEntity();
            entity.DepartNum = DepartNum;
            entity.DepartName = DepartName;
            entity.ChildCount = 0;
            entity.ParentNum = ParentNum;
            entity.Depth = 0;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = CompanyID;
            entity.SnNum = SnNum;

            DepartProvider provider = new DepartProvider(CompanyID);
            int line = provider.Update(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "部门编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "部门编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            DepartProvider provider = new DepartProvider(CompanyID);
            int line = provider.Delete(list);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("部门删除成功,受影响行数{0}行", line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "部门删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询部门信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum");
            DepartProvider provider = new DepartProvider(CompanyID);
            SysDepartEntity entity = provider.GetSingle(DepartNum);
            DataResult<SysDepartEntity> result = new DataResult<SysDepartEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            DepartProvider provider = new DepartProvider(CompanyID);
            List<SysDepartEntity> list = provider.GetList();
            DataListResult<SysDepartEntity> result = new DataListResult<SysDepartEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
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
            string DepartName = WebUtil.GetFormValue<string>("DepartName", string.Empty);
            string ParentNum = WebUtil.GetFormValue<string>("ParentNum",string.Empty);

            SysDepartEntity entity = new SysDepartEntity();
            entity.DepartName = DepartName;
            entity.ParentNum = ParentNum;

            PageInfo pageInfo = new PageInfo();
            pageInfo.PageIndex = PageIndex;
            pageInfo.PageSize = PageSize;

            DepartProvider provider = new DepartProvider(CompanyID);
            List<SysDepartEntity> list = provider.GetList(entity, ref pageInfo);
            DataListResult<SysDepartEntity> result = new DataListResult<SysDepartEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list, PageInfo = pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询部门的子类集合
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChildList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            DepartProvider provider = new DepartProvider(CompanyID);
            List<SysDepartEntity> list = provider.GetChildList(SnNum);

            DataResult<List<SysDepartEntity>> dataResult = new DataResult<List<SysDepartEntity>>();
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
            DepartProvider provider = new DepartProvider(CompanyID);
            List<SysDepartEntity> list = provider.GetParentList(SnNum);

            DataResult<List<SysDepartEntity>> dataResult = new DataResult<List<SysDepartEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = list;

            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
