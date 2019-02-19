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
    public class RoleController : Controller
    {
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string RoleName = WebUtil.GetFormValue<string>("RoleName");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string Remark = WebUtil.GetFormValue<string>("Remark");

            SysRoleEntity entity = new SysRoleEntity();
            entity.RoleName = RoleName;
            entity.CompanyID = CompanyID;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.Remark = Remark;

            SysRoleProvider provider = new SysRoleProvider(CompanyID);
            int line = provider.AddRole(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "角色新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "角色新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string RoleName = WebUtil.GetFormValue<string>("RoleName");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string RoleNum = WebUtil.GetFormValue<string>("RoleNum");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            SysRoleEntity entity = new SysRoleEntity();
            entity.SnNum = SnNum;
            entity.RoleNum = RoleNum;
            entity.RoleName = RoleName;
            entity.CompanyID = CompanyID;
            entity.RoleNum = RoleNum;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.Remark = Remark;

            SysRoleProvider provider = new SysRoleProvider(CompanyID);
            int line = provider.UpdateRole(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "角色修改成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "角色修改失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            SysRoleProvider provider = new SysRoleProvider(CompanyID);
            int line = provider.DeleteRole(list);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("角色删除成功,受影响行数{0}行",line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "角色删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            SysRoleProvider provider = new SysRoleProvider(CompanyID);
            SysRoleEntity entity = provider.GetRoleEntity(SnNum);
            DataResult<SysRoleEntity> result = new DataResult<SysRoleEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            SysRoleProvider provider = new SysRoleProvider(CompanyID);
            List<SysRoleEntity> list = provider.GetList();
            DataListResult<SysRoleEntity> result = new DataListResult<SysRoleEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPage()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex",1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string RoleName = WebUtil.GetFormValue<string>("RoleName",string.Empty);
            string Remark = WebUtil.GetFormValue<string>("Remark",string.Empty);
            SysRoleEntity entity = new SysRoleEntity();
            entity.RoleName = RoleName;
            entity.Remark = Remark;
            PageInfo pageInfo=new PageInfo();
            pageInfo.PageIndex=PageIndex;
            pageInfo.PageSize=PageSize;
            SysRoleProvider provider = new SysRoleProvider(CompanyID);
            List<SysRoleEntity> list = provider.GetList(entity,ref pageInfo);
            DataListResult<SysRoleEntity> result = new DataListResult<SysRoleEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list,PageInfo=pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }
    }
}
