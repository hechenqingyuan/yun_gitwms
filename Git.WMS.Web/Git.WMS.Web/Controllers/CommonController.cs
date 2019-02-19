using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Controllers
{
    public class CommonController : AjaxPage
    {
        /// <summary>
        /// 输出枚举转化JS
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 30 * 1000)]
        public ActionResult Js()
        {
            string js = EnumToJsonHelper.GetJs();
            js += EnumToJsonHelper.GetMenuObject();
            return Content(js);
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult Upload()
        {
            DataResult<string> dataResult = new DataResult<string>();
            bool success = false;
            if (Request.Files.Count > 0)
            {
                string ext = System.IO.Path.GetExtension(Request.Files[0].FileName).ToLower();
                if (Request.Files[0].ContentLength <= 5 * 1024 * 1024)
                {
                    success = true;
                    string fileName = ConvertHelper.NewGuid() + ext;
                    string dir = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(dir))
                    {
                        System.IO.Directory.CreateDirectory(dir);
                    }
                    Request.Files[0].SaveAs(System.IO.Path.Combine(dir, fileName));
                    dataResult.Code = (int)EResponseCode.Success;
                    dataResult.Message = "上传成功";
                    dataResult.Result = "/UploadFile/" + fileName;
                    return Content(JsonHelper.SerializeObject(dataResult));
                }
            }
            
            if (!success)
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "上传失败";
                dataResult.Result = "";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 选择仓库
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult ChangeStorage()
        {
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            List<StorageEntity> listStorage = Session[SessionKey.SESSION_STORAGE_LIST] as List<StorageEntity>;
            listStorage = listStorage.IsNull() ? new List<StorageEntity>() : listStorage;
            StorageEntity entity = listStorage.FirstOrDefault(a => a.SnNum == StorageNum);
            this.DefaultStorage = entity;
            DataResult<string> dataResult = new DataResult<string>() 
            { 
                Code=(int)EResponseCode.Success,
                Message="响应成功"
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 设置菜单显示的状态
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true, false)]
        public ActionResult SetMenuStatus()
        {
            string status = WebUtil.GetFormValue<string>("MenuStatus", "open");
            Session[SessionKey.SESSION_MENU_STATUS] = status;
            DataResult<string> dataResult = new DataResult<string>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
