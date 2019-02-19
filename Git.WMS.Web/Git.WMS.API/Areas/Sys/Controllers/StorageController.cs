using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Storage.Entity.Sys;
using Git.Storage.Provider;
using Git.Storage.Provider.Base;
using Git.Storage.Provider.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Sys.Controllers
{
    public class StorageController : Controller
    {
        /// <summary>
        /// 新增仓库
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string StorageName = WebUtil.GetFormValue<string>("StorageName");
            int StorageType = WebUtil.GetFormValue<int>("StorageType");
            double Length = WebUtil.GetFormValue<double>("Length");
            double Width = WebUtil.GetFormValue<double>("Width");
            double Height = WebUtil.GetFormValue<double>("Height");
            string Action = WebUtil.GetFormValue<string>("Action");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string Address = WebUtil.GetFormValue<string>("Address");
            double Area = WebUtil.GetFormValue<double>("Area");
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum");
            string CreateUser = WebUtil.GetFormValue<string>("CreateUser");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            DateTime LeaseTime = WebUtil.GetFormValue<DateTime>("LeaseTime",DateTime.Now.AddYears(1));

            StorageEntity entity = new StorageEntity();
            entity.StorageName = StorageName;
            entity.StorageType = StorageType;
            entity.Length = Length;
            entity.Width = Width;
            entity.Height = Height;
            entity.Action = Action;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.Remark = Remark;
            entity.Contact = Contact;
            entity.Phone = Phone;
            entity.Address = Address;
            entity.Area = Area;
            entity.DepartNum = DepartNum;
            entity.LeaseTime = LeaseTime;
            entity.CreateUser = CreateUser;
            entity.CompanyID = CompanyID;

            StorageProvider provider = new StorageProvider(CompanyID);
            int line = provider.Add(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "仓库新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "仓库新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑仓库
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string StorageName = WebUtil.GetFormValue<string>("StorageName");
            int StorageType = WebUtil.GetFormValue<int>("StorageType");
            double Length = WebUtil.GetFormValue<double>("Length");
            double Width = WebUtil.GetFormValue<double>("Width");
            double Height = WebUtil.GetFormValue<double>("Height");
            string Action = WebUtil.GetFormValue<string>("Action");
            int Status = WebUtil.GetFormValue<int>("Status");
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid");
            int IsDefault = WebUtil.GetFormValue<int>("IsDefault");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string Address = WebUtil.GetFormValue<string>("Address");
            double Area = WebUtil.GetFormValue<double>("Area");
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum");
            string CreateUser = WebUtil.GetFormValue<string>("CreateUser");
            DateTime LeaseTime = WebUtil.GetFormValue<DateTime>("LeaseTime", DateTime.Now.AddYears(1));
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            StorageEntity entity = new StorageEntity();
            entity.SnNum = SnNum;
            entity.StorageNum = StorageNum;
            entity.StorageName = StorageName;
            entity.StorageType = StorageType;
            entity.Length = Length;
            entity.Width = Width;
            entity.Height = Height;
            entity.Action = Action;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.Status = Status;
            entity.IsForbid = IsForbid;
            entity.IsDefault = IsDefault;
            entity.CreateTime = DateTime.Now;
            entity.Remark = Remark;
            entity.Contact = Contact;
            entity.Phone = Phone;
            entity.Address = Address;
            entity.Area = Area;
            entity.DepartNum = DepartNum;
            entity.LeaseTime = LeaseTime;
            entity.CreateUser = CreateUser;
            entity.CompanyID = CompanyID;

            StorageProvider provider = new StorageProvider(CompanyID);
            int line = provider.Update(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "仓库修改成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "仓库修改失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            StorageProvider provider = new StorageProvider(CompanyID);
            int line = provider.Delete(list);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("仓库删除成功,受影响行数{0}行", line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "仓库删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询仓库信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            StorageProvider provider = new StorageProvider(CompanyID);
            StorageEntity entity = provider.GetSingleByNum(SnNum);
            DataResult<StorageEntity> result = new DataResult<StorageEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询仓库列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            StorageProvider provider = new StorageProvider(CompanyID);
            List<StorageEntity> list = provider.GetList();
            DataListResult<StorageEntity> result = new DataListResult<StorageEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询仓库分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPage()
        {
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string StorageName = WebUtil.GetFormValue<string>("StorageName");
            int StorageType = WebUtil.GetFormValue<int>("StorageType",0);
            int Status = WebUtil.GetFormValue<int>("Status",-1);
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid", -1);
            int IsDefault = WebUtil.GetFormValue<int>("IsDefault", -1);
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string DepartNum = WebUtil.GetFormValue<string>("DepartNum");
            string Contact = WebUtil.GetFormValue<string>("Contact");
            string Phone = WebUtil.GetFormValue<string>("Phone");
            string Address = WebUtil.GetFormValue<string>("Address");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            StorageEntity entity = new StorageEntity();
            entity.StorageNum = StorageNum;
            entity.StorageName = StorageName;
            entity.StorageType = StorageType;
            entity.Status = Status;
            entity.IsForbid = IsForbid;
            entity.IsDefault = IsDefault;
            entity.Remark = Remark;
            entity.DepartNum = DepartNum;
            entity.Contact = Contact;
            entity.Phone = Phone;
            entity.Address = Address;
            entity.CompanyID = CompanyID;

            PageInfo pageInfo = new PageInfo();
            pageInfo.PageIndex = PageIndex;
            pageInfo.PageSize = PageSize;

            StorageProvider provider = new StorageProvider(CompanyID);
            List<StorageEntity> list = provider.GetList(entity, ref pageInfo);
            DataListResult<StorageEntity> result = new DataListResult<StorageEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list, PageInfo = pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 设为默认仓库
        /// </summary>
        /// <returns></returns>
        public ActionResult SetDefault()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            
            StorageProvider provider = new StorageProvider(CompanyID);
            int line = provider.SetDefault(SnNum);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "设置成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "设置失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 启用或禁用仓库
        /// </summary>
        /// <returns></returns>
        public ActionResult SetForbid()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid");
            EBool EIsForbid=EnumHelper.GetModel<EBool>(IsForbid);
            StorageProvider provider = new StorageProvider(CompanyID);
            int line = provider.SetForbid(SnNum,EIsForbid);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "设置成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "设置失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }
    }
}
