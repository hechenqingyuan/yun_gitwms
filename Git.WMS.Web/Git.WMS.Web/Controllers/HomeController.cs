using Git.Framework.Controller;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Entity.Sys;
using Git.Storage.Common;
using Newtonsoft.Json;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Git.Storage.Common.Enum;

namespace Git.WMS.Web.Controllers
{
    public class HomeController : MasterPage
    {
        /// <summary>
        /// 首页登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string returnurl = WebUtil.GetQueryStringValue<string>("returnurl");
            ViewBag.ReturnUrl = returnurl;
            return View();
        }

        /// <summary>
        /// 桌面
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult Desktop()
        {
            return View();
        }

        /// <summary>
        /// 员工列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult UserList()
        {
            string CompanyID = this.CompanyID;
            ViewBag.DepartList = DropDownHelper.GetDepart(string.Empty, CompanyID);
            ViewBag.RoleList = DropDownHelper.GetRole(string.Empty, CompanyID);
            return View();
        }

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult AddUser()
        {
            string UserNum = WebUtil.GetQueryStringValue<string>("UserNum", string.Empty);
            string CompanyID = this.CompanyID;
            AdminEntity entity = null;
            if (!UserNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("UserNum", UserNum);
                string result = client.Execute(UserApiName.UserApiName_Single, dic);
                if (!result.IsEmpty())
                {
                    DataResult<AdminEntity> dataResult = JsonConvert.DeserializeObject<DataResult<AdminEntity>>(result);
                    entity = dataResult.Result;
                }
            }   
            entity = entity == null ? new AdminEntity() : entity;
            ViewBag.Entity = entity;

            ViewBag.DepartList = DropDownHelper.GetDepart(entity.DepartNum, CompanyID);
            ViewBag.RoleList = DropDownHelper.GetRole(entity.RoleNum, CompanyID);
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult EditPass()
        {
            ViewBag.UserNum = this.LoginUser.UserNum;
            return View();
        }

        /// <summary>
        /// 管理员修改用户密码
        /// </summary>
        /// <returns></returns>
        [DialogFilter(true,true)]
        public ActionResult AdminPass()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");
            ViewBag.UserNum = SnNum;
            return View();
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult RoleList()
        {
            return View();
        }

        /// <summary>
        /// 新增编辑角色
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult AddRole()
        {
            string RoleNum = WebUtil.GetQueryStringValue<string>("RoleNum", string.Empty);
            string CompanyID = this.CompanyID;

            SysRoleEntity entity = null;
            if (!RoleNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", RoleNum);
                string result = client.Execute(RoleApiName.RoleApiName_Single, dic);

                if (!result.IsEmpty())
                {
                    DataResult<SysRoleEntity> dataResult = JsonConvert.DeserializeObject<DataResult<SysRoleEntity>>(result);
                    entity = dataResult.Result;
                }
            }
            entity = entity.IsNull() ? new SysRoleEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult DepartList()
        {
            return View();
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult AddDepart()
        {
            string DepartNum = WebUtil.GetQueryStringValue<string>("DepartNum", string.Empty);
            string CompanyID = this.CompanyID;
            string ParentNum = string.Empty;

            SysDepartEntity entity = null;

            if (!DepartNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("DepartNum", DepartNum);
                string result = client.Execute(DepartApiName.DepartApiName_Single, dic);

                if (!result.IsEmpty())
                {
                    DataResult<SysDepartEntity> dataResult = JsonConvert.DeserializeObject<DataResult<SysDepartEntity>>(result);
                    entity = dataResult.Result;
                }
            }

            entity = entity.IsNull() ? new SysDepartEntity() : entity;
            ViewBag.Entity = entity;

            ParentNum = entity.ParentNum;
            ViewBag.DepartList = DropDownHelper.GetDepart(ParentNum, CompanyID);
            return View();
        }

        /// <summary>
        /// 标识符管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult SN()
        {
            string SequenceStr = EnumHelper.GetOptions<ESequence>(0);
            ViewBag.SequenceStr = SequenceStr;
            return View();
        }

        /// <summary>
        /// 标识符管理
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult SysRes()
        {
            return View();
        }
        /// <summary>
        /// 权限设置
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Power()
        {
            string RoleNum = WebUtil.GetQueryStringValue<string>("RoleNum");
            ViewBag.RoleNum = RoleNum;

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string CompanyID = this.CompanyID;

            dic.Add("CompanyID", CompanyID);
            dic.Add("RoleNum", RoleNum);
            string result = client.Execute(UserApiName.UserApiName_GetPower, dic);
            DataListResult<SysResourceEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<SysResourceEntity>>(result);
            List<SysResourceEntity> listResult = dataResult.Result;
            listResult = listResult.IsNull() ? new List<SysResourceEntity>() : listResult;
            ViewBag.ListResult = listResult;
            return View();
        }

        /// <summary>
        /// 错误提示页(没有权限)
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true, false)]
        public ActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            RemoveLogin();
            return Redirect("/Home/Index");
        }
    }
}
