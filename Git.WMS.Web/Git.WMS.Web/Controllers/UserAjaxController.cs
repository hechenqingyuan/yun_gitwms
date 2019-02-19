using Git.Framework.Controller;
using Git.Storage.Entity.Sys;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common.Enum;
using Newtonsoft.Json;
using Git.WMS.Web.Lib;
using Git.Storage.Common;
using Git.WMS.Web.Lib.Filter;
using System.Data;
using Git.Storage.Common.Excel;
using Git.Storage.Entity.Storage;

namespace Git.WMS.Web.Controllers
{
    public class UserAjaxController : AjaxPage
    {

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            string CompanyNum = WebUtil.GetFormValue<string>("CompanyNum",string.Empty);
            string UserName = WebUtil.GetFormValue<string>("UserName", string.Empty);
            string PassWord = WebUtil.GetFormValue<string>("PassWord", string.Empty);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            ITopClient client = new TopClientDefault();

            dic.Add("CompanyNum", CompanyNum);
            string result = client.Execute(CompanyApiName.CompanyApiName_GetSingle,dic);
            if (result.IsEmpty())
            {
                DataResult dataResult = new DataResult() { Code=(int)EResponseCode.Exception,Message="公司不存在或公司编号错误" };
                return Content(JsonHelper.SerializeObject(dataResult));
            }

            DataResult<CompanyEntity> CompanyResult = JsonConvert.DeserializeObject<DataResult<CompanyEntity>>(result);
            CompanyEntity CompanyEntity = CompanyResult.Result;
            if (CompanyEntity.IsNull())
            {
                DataResult dataResult = new DataResult() { Code = (int)EResponseCode.Exception, Message = "公司不存在或公司编号错误" };
                return Content(JsonHelper.SerializeObject(dataResult));
            }

            string CompanyID = CompanyEntity.CompanyID;
            dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("UserName", UserName);
            dic.Add("PassWord", PassWord);

            result = client.Execute(UserApiName.UserApiName_Login, dic);
            DataResult<AdminEntity> adminResult = JsonConvert.DeserializeObject<DataResult<AdminEntity>>(result);
            AdminEntity entity = adminResult.Result;
            if (entity == null)
            {
                DataResult dataResult = new DataResult() { Code = (int)EResponseCode.Exception, Message = "用户名或密码错误" };
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            else
            {
                this.LoginUser = entity;
                this.Company = CompanyEntity;


                //查询仓库
                dic = new Dictionary<string, string>();
                dic.Add("CompanyID", CompanyID);
                dic.Add("PageIndex", "1");
                dic.Add("PageSize", "100");
                result = client.Execute(StorageApiName.StorageApiName_GetPage, dic);
                if (result.IsNotEmpty())
                {
                    DataListResult<StorageEntity> dataStorageResult = JsonConvert.DeserializeObject<DataListResult<StorageEntity>>(result);
                    List<StorageEntity> listStorage = dataStorageResult.Result;
                    if (!listStorage.IsNullOrEmpty())
                    {
                        Session[SessionKey.SESSION_STORAGE_LIST] = listStorage;
                        this.DefaultStorage=listStorage[0];


                        //查询默认库位  当只有默认仓库 默认仓库的时候使用这段代码
                        //dic = new Dictionary<string, string>();

                        //int PageIndex = 1;
                        //int PageSize = Int32.MaxValue;

                        //dic.Add("CompanyID", CompanyID);
                        //dic.Add("PageIndex", PageIndex.ToString());
                        //dic.Add("PageSize", PageSize.ToString());
                        //dic.Add("StorageNum", this.DefaultStorageNum);
                        //result = client.Execute(LocationApiName.LocationApiName_GetPage, dic);

                        //string returnValue = string.Empty;
                        //if (!result.IsEmpty())
                        //{
                        //    DataListResult<LocationEntity> localResult = JsonConvert.DeserializeObject<DataListResult<LocationEntity>>(result);
                        //    List<LocationEntity> listResult = localResult.Result;
                        //    if (!listResult.IsNullOrEmpty())
                        //    {
                        //        this.DefaultLocation = listResult[0];
                        //    }
                        //}
                    }
                }

                DataResult dataResult = new DataResult() { Code = (int)EResponseCode.Success, Message = "登录成功" };
                return Content(JsonHelper.SerializeObject(dataResult));
            }
        }

        /// <summary>
        /// 查询用户分页列表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            int pageIndex = WebUtil.GetFormValue<int>("pageIndex", 1);
            int pageSize = WebUtil.GetFormValue<int>("pageSize", 15);
            string UserCode = WebUtil.GetFormValue<string>("UserCode", string.Empty);
            string UserName = WebUtil.GetFormValue<string>("UserName", string.Empty);
            string RoleNum = WebUtil.GetFormValue<string>("RoleNum", string.Empty);
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum", string.Empty);
            string CompanyID = this.CompanyID;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            ITopClient client = new TopClientDefault();
            dic.Add("CompanyID", CompanyID);
            dic.Add("pageIndex", pageIndex.ToString());
            dic.Add("pageSize", pageSize.ToString());
            dic.Add("UserCode", UserCode);
            dic.Add("UserName", UserName);
            dic.Add("RoleNum", RoleNum);
            dic.Add("DepartNum", DepartNum);

            string result = client.Execute(UserApiName.UserApiName_GetList_Page, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Delete()
        {
            ITopClient client = new TopClientDefault();
            string list = WebUtil.GetFormValue<string>("list");
            string CompanyID = this.CompanyID;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("List", list);
            string result = client.Execute(UserApiName.UserApiName_Delete, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            string UserNum = WebUtil.GetFormValue<string>("UserNum", string.Empty);
            string UserName = WebUtil.GetFormValue<string>("UserName", string.Empty);
            string PassWord = WebUtil.GetFormValue<string>("PassWord", string.Empty);
            string ConfirmPass = WebUtil.GetFormValue<string>("ConfirmPass", string.Empty);
            string UserCode = WebUtil.GetFormValue<string>("UserCode", string.Empty);
            string RealName = WebUtil.GetFormValue<string>("RealName", string.Empty);
            string Email = WebUtil.GetFormValue<string>("Email", string.Empty);
            string Mobile = WebUtil.GetFormValue<string>("Mobile", string.Empty);
            string Phone = WebUtil.GetFormValue<string>("Phone", string.Empty);
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum", string.Empty);
            string RoleNum = WebUtil.GetFormValue<string>("RoleNum", string.Empty);
            string Remark = WebUtil.GetFormValue<string>("Remark", string.Empty);
            string CompanyID = this.CompanyID;

            AdminEntity entity = new AdminEntity();
            entity.UserNum = UserNum;
            entity.UserName = UserName;
            entity.PassWord = PassWord;
            entity.UserCode = UserCode;
            entity.RealName = RealName;
            entity.Email = Email;
            entity.Mobile = Mobile;
            entity.Phone = Phone;
            entity.DepartNum = DepartNum;
            entity.RoleNum = RoleNum;
            entity.Remark = Remark;
            entity.CompanyID = CompanyID;
            entity.CreateTime = DateTime.Now;
            entity.LoginCount = 0;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.Status = 0;
            entity.CreateUser = this.LoginUserNum;
            entity.CreateIp = this.IP;
            entity.Picture = string.Empty;

            string ApiName = UserApiName.UserApiName_Add;
            if (!UserNum.IsEmpty())
            {
                ApiName = UserApiName.UserApiName_Edit;
            }

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Entity",JsonConvert.SerializeObject(entity));
            dic.Add("CompanyID", CompanyID);

            string result = client.Execute(ApiName, dic);
            return Content(result);
        }

        /// <summary>
        /// 管理员修改密码
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult AdminEditPass()
        {
            string UserNum = WebUtil.GetFormValue<string>("UserNum", string.Empty);
            string NewPass = WebUtil.GetFormValue<string>("NewPass", string.Empty);
            string ConfirmPass = WebUtil.GetFormValue<string>("ConfirmPass", string.Empty);

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

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", this.CompanyID);
            dic.Add("UserNum", UserNum);
            dic.Add("NewPass", NewPass);
            dic.Add("ConfirmPass", ConfirmPass);

            string result = client.Execute(UserApiName.UserApiName_AdminEditPass, dic);
            return Content(result);
        }

        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult ChangePass()
        {
            string UserNum = WebUtil.GetFormValue<string>("UserNum", string.Empty);
            string CurrentPass = WebUtil.GetFormValue<string>("CurrentPass", string.Empty);
            string NewPass = WebUtil.GetFormValue<string>("NewPass", string.Empty);
            string ConfirmPass = WebUtil.GetFormValue<string>("ConfirmPass", string.Empty);
            string CompanyID = this.CompanyID;

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

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            
            dic.Add("CompanyID", this.CompanyID);
            dic.Add("UserNum", this.LoginUser.UserNum);
            dic.Add("OldPass", CurrentPass);
            dic.Add("NewPass", ConfirmPass);

            string result = client.Execute(UserApiName.UserApiName_UpdatePass, dic);
            return Content(result);
        }

        /// <summary>
        /// 导出用户列表Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToExcel()
        {
            string UserCode = WebUtil.GetFormValue<string>("UserCode", string.Empty);
            string UserName = WebUtil.GetFormValue<string>("UserName", string.Empty);
            string RoleNum = WebUtil.GetFormValue<string>("RoleNum", string.Empty);
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum", string.Empty);
            string CompanyID = this.CompanyID;

            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("CompanyID", CompanyID);
            dic.Add("pageIndex", "1");
            dic.Add("pageSize", Int32.MaxValue.ToString());
            dic.Add("UserCode", UserCode);
            dic.Add("UserName", UserName);
            dic.Add("RoleNum", RoleNum);
            dic.Add("DepartNum", DepartNum);

            string result = client.Execute(UserApiName.UserApiName_GetList_Page, dic);
            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<AdminEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<AdminEntity>>(result);
                List<AdminEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("用户名"));
                    dt.Columns.Add(new DataColumn("工号"));
                    dt.Columns.Add(new DataColumn("真名"));
                    dt.Columns.Add(new DataColumn("邮箱"));
                    dt.Columns.Add(new DataColumn("手机"));
                    dt.Columns.Add(new DataColumn("固定电话"));
                    dt.Columns.Add(new DataColumn("创建时间"));
                    dt.Columns.Add(new DataColumn("登录次数"));
                    dt.Columns.Add(new DataColumn("部门"));
                    dt.Columns.Add(new DataColumn("角色"));
                    dt.Columns.Add(new DataColumn("备注"));
                    foreach (AdminEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.UserName;
                        row[1] = t.UserCode;
                        row[2] = t.RealName;
                        row[3] = t.Email;
                        row[4] = t.Mobile;
                        row[5] = t.Phone;
                        row[6] = t.CreateTime.To("yyyy-MM-dd");
                        row[7] = t.LoginCount;
                        row[8] = t.DepartName;
                        row[9] = t.RoleName;
                        row[10] = t.Remark;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("用户管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("用户管理", "用户管理", System.IO.Path.Combine(filePath, filename));
                    excel.ToExcel(dt);
                    returnValue = ("/UploadFile/" + filename).Escape();
                }
            }
            DataResult returnResult = null;
            if (!returnValue.IsEmpty())
            {
                returnResult = new DataResult() { Code = 1000, Message = returnValue };
            }
            else
            {
                returnResult = new DataResult() { Code = 1001, Message = "没有任何数据导出" };
            }
            return Content(JsonHelper.SerializeObject(returnResult));
        }

    }
}
