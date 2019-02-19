using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.ORM;
using Git.Storage.Entity.Sys;
using Git.Storage.Provider.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;

namespace Git.WMS.API.Areas.Sys.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// 查询用户分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            int pageIndex = WebUtil.GetFormValue<int>("pageIndex", 1);
            int pageSize = WebUtil.GetFormValue<int>("pageSize", 10);
            string UserCode = WebUtil.GetFormValue<string>("UserCode", string.Empty);
            string UserName = WebUtil.GetFormValue<string>("UserName", string.Empty);
            string RoleNum = WebUtil.GetFormValue<string>("RoleNum", string.Empty);
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum", string.Empty);
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);

            PageInfo pageInfo = new PageInfo() { PageIndex = pageIndex, PageSize = pageSize };
            AdminProvider provider = new AdminProvider(CompanyID);

            AdminEntity entity = new AdminEntity();
            entity.UserCode = UserCode;
            entity.UserName = UserName;
            entity.RoleNum = RoleNum;
            entity.DepartNum = DepartNum;
            entity.CompanyID = CompanyID;

            List<AdminEntity> listResult = provider.GetList(entity, ref pageInfo);
            DataListResult<AdminEntity> result = new DataListResult<AdminEntity>() 
            { 
                Code=(int)EResponseCode.Success, PageInfo=pageInfo,Result=listResult,Message="响应成功"
            };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            DataResult result = new DataResult();

            AdminEntity entity = WebUtil.GetFormObject<AdminEntity>("Entity");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID",string.Empty);

            if (entity.IsNull())
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "新增用户信息为空";
                return Content(JsonHelper.SerializeObject(result));
            }

            entity.CompanyID = CompanyID;
            AdminProvider provider = new AdminProvider(CompanyID);
            int line = provider.AddAdmin(entity);

            
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "用户新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "用户新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            DataResult result = new DataResult();

            AdminEntity entity = WebUtil.GetFormObject<AdminEntity>("Entity");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            entity.CompanyID = CompanyID;

            if (entity.IsNull())
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "新增用户信息为空";
                return Content(JsonHelper.SerializeObject(result));
            }

            AdminProvider provider = new AdminProvider(CompanyID);
            int line = provider.Update(entity);
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "用户编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "用户编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            AdminProvider provider = new AdminProvider(CompanyID);
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
        /// 根据用户编号查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string UserNum = WebUtil.GetFormValue<string>("UserNum");
            AdminProvider provider = new AdminProvider(CompanyID);
            AdminEntity entity = provider.GetAdmin(UserNum);
            DataResult<AdminEntity> result = new DataResult<AdminEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 管理员修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminEditPass()
        {
            string UserNum = WebUtil.GetFormValue<string>("UserNum", string.Empty);
            string NewPass = WebUtil.GetFormValue<string>("NewPass", string.Empty);
            string ConfirmPass = WebUtil.GetFormValue<string>("ConfirmPass", string.Empty);
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            DataResult dataResult = new DataResult();
            if (UserNum.IsEmpty())
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "该非法操作已经被禁止";
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            if (NewPass.IsEmpty())
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "新密码不能为空";
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            if (NewPass != ConfirmPass)
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "密码和确认密码不一致";
                return Content(JsonHelper.SerializeObject(dataResult));
            }

            AdminProvider provider = new AdminProvider(CompanyID);
            int line = provider.AdminEditPass(UserNum,NewPass);

            
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "修改成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "修改失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePass()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string UserNum = WebUtil.GetFormValue<string>("UserNum");
            string OldPass = WebUtil.GetFormValue<string>("OldPass");
            string NewPass = WebUtil.GetFormValue<string>("NewPass");
            AdminProvider provider = new AdminProvider(CompanyID);

            DataResult result = provider.UpdatePwd(UserNum,OldPass,NewPass);
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            string UserName = WebUtil.GetFormValue<string>("UserName", string.Empty);
            string PassWord = WebUtil.GetFormValue<string>("PassWord", string.Empty);
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);

            AdminProvider provider = new AdminProvider(CompanyID);
            AdminEntity entity = provider.Login(UserName, PassWord);
            DataResult<AdminEntity> dataResult = new DataResult<AdminEntity>();
            if (entity == null)
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "用户名或密码错误";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "响应成功";
                dataResult.Result = entity;
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 根据工号扫描
        /// </summary>
        /// <returns></returns>
        public ActionResult Scan()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string UserCode = WebUtil.GetFormValue<string>("UserCode");
            AdminProvider provider = new AdminProvider(CompanyID);
            AdminEntity entity = provider.Scan(UserCode);
            DataResult<AdminEntity> result = new DataResult<AdminEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 获得某个角色分配的权限
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPower()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string RoleNum = WebUtil.GetFormValue<string>("RoleNum", string.Empty);
            PowerProvider provider = new PowerProvider(CompanyID);
            List<SysResourceEntity> listResult = provider.GetRoleResource(RoleNum);
            DataResult<List<SysResourceEntity>> dataResult = new DataResult<List<SysResourceEntity>>() 
            { 
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 保存权限分配
        /// </summary>
        /// <returns></returns>
        public ActionResult SavePower()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string RoleNum = WebUtil.GetFormValue<string>("RoleNum", string.Empty);
            List<string> listItems = WebUtil.GetFormObject<List<string>>("List");
            PowerProvider provider = new PowerProvider(CompanyID);
            int line=provider.AllotPower(RoleNum,listItems);
            DataResult dataResult = new DataResult();
            if (line>0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "权限分配成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "权限分配失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <returns></returns>
        public ActionResult HasPower()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string RoleNum = WebUtil.GetFormValue<string>("RoleNum", string.Empty);
            string ResNum = WebUtil.GetFormValue<string>("ResNum", string.Empty);
            PowerProvider provider = new PowerProvider(CompanyID);
            bool hasPower = provider.HasPower(ResNum, RoleNum);
            DataResult dataResult = new DataResult();
            if (hasPower)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "验证有权限";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "验证无权限";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
