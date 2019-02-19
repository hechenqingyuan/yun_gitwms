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
    public partial class LocationController : Controller
    {
        /// <summary>
        /// 新增库位
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum");
            string LocalBarCode = WebUtil.GetFormValue<string>("LocalBarCode");
            string LocalName = WebUtil.GetFormValue<string>("LocalName");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            int StorageType = WebUtil.GetFormValue<int>("StorageType");
            int LocalType = WebUtil.GetFormValue<int>("LocalType");
            string Rack = WebUtil.GetFormValue<string>("Rack");
            double Length = WebUtil.GetFormValue<double>("Length");
            double Width = WebUtil.GetFormValue<double>("Width");
            double Height = WebUtil.GetFormValue<double>("Height");
            double X = WebUtil.GetFormValue<double>("X");
            double Y = WebUtil.GetFormValue<double>("Y");
            double Z = WebUtil.GetFormValue<double>("Z");
            string UnitNum = WebUtil.GetFormValue<string>("UnitNum");
            string UnitName = WebUtil.GetFormValue<string>("UnitName");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid", (int)EBool.No);
            int IsDefault = WebUtil.GetFormValue<int>("IsDefault", (int)EBool.No);
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            LocationEntity entity = new LocationEntity();
            entity.LocalNum = ConvertHelper.NewGuid();
            entity.LocalBarCode = new TNumProvider(CompanyID).GetSwiftNum(typeof(LocationEntity), 5);
            entity.LocalName = LocalName;
            entity.StorageNum = StorageNum;
            entity.StorageType = StorageType;
            entity.LocalType = LocalType;
            entity.Rack = Rack;
            entity.Length = Length;
            entity.Width = Width;
            entity.Height = Height;
            entity.X = X;
            entity.Y = Y;
            entity.Z = Z;
            entity.UnitNum = UnitNum;
            entity.UnitName = UnitName;
            entity.Remark = Remark;
            entity.IsForbid = IsForbid;
            entity.IsDefault = IsDefault;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = CompanyID;

            LocationProvider provider = new LocationProvider(CompanyID);
            int line = provider.Add(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "库位新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "库位新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑库位
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum");
            string LocalBarCode = WebUtil.GetFormValue<string>("LocalBarCode");
            string LocalName = WebUtil.GetFormValue<string>("LocalName");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            int StorageType = WebUtil.GetFormValue<int>("StorageType");
            int LocalType = WebUtil.GetFormValue<int>("LocalType");
            string Rack = WebUtil.GetFormValue<string>("Rack");
            double Length = WebUtil.GetFormValue<double>("Length");
            double Width = WebUtil.GetFormValue<double>("Width");
            double Height = WebUtil.GetFormValue<double>("Height");
            double X = WebUtil.GetFormValue<double>("X");
            double Y = WebUtil.GetFormValue<double>("Y");
            double Z = WebUtil.GetFormValue<double>("Z");
            string UnitNum = WebUtil.GetFormValue<string>("UnitNum");
            string UnitName = WebUtil.GetFormValue<string>("UnitName");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid", (int)EBool.No);
            int IsDefault = WebUtil.GetFormValue<int>("IsDefault", (int)EBool.No);
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            LocationEntity entity = new LocationEntity();
            entity.LocalNum = LocalNum;
            entity.LocalBarCode = LocalBarCode;
            entity.LocalName = LocalName;
            entity.StorageNum = StorageNum;
            entity.StorageType = StorageType;
            entity.LocalType = LocalType;
            entity.Rack = Rack;
            entity.Length = Length;
            entity.Width = Width;
            entity.Height = Height;
            entity.X = X;
            entity.Y = Y;
            entity.Z = Z;
            entity.UnitNum = UnitNum;
            entity.UnitName = UnitName;
            entity.Remark = Remark;
            entity.IsForbid = IsForbid;
            entity.IsDefault = IsDefault;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.CreateTime = DateTime.Now;
            entity.CompanyID = CompanyID;

            LocationProvider provider = new LocationProvider(CompanyID);
            int line = provider.Update(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "库位修改成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "库位修改失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除库位
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            LocationProvider provider = new LocationProvider(CompanyID);
            int line = provider.Delete(list);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("库位删除成功,受影响行数{0}行", line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "库位删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询库位信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum");
            LocationProvider provider = new LocationProvider(CompanyID);
            LocationEntity entity = provider.GetSingleByNum(LocalNum);
            DataResult<LocationEntity> result = new DataResult<LocationEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询库位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            LocationProvider provider = new LocationProvider(CompanyID);
            List<LocationEntity> list = provider.GetList();
            DataListResult<LocationEntity> result = new DataListResult<LocationEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询库位分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPage()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string LocalBarCode = WebUtil.GetFormValue<string>("LocalBarCode");
            string LocalName = WebUtil.GetFormValue<string>("LocalName", string.Empty);
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum", string.Empty);
            int StorageType = WebUtil.GetFormValue<int>("StorageType", 0);
            List<int> listLocalType = WebUtil.GetFormObject<List<int>>("ListLocalType",null);
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid", -1);
            int IsDefault = WebUtil.GetFormValue<int>("IsDefault", -1);

            LocationEntity entity = new LocationEntity();
            entity.LocalBarCode = LocalBarCode;
            entity.LocalName = LocalName;
            entity.StorageNum = StorageNum;
            entity.StorageType = StorageType;
            entity.IsForbid = IsForbid;
            entity.IsDefault = IsDefault;
            entity.ListLocalType = listLocalType;
            entity.CompanyID = CompanyID;

            PageInfo pageInfo = new PageInfo();
            pageInfo.PageIndex = PageIndex;
            pageInfo.PageSize = PageSize;

            LocationProvider provider = new LocationProvider(CompanyID);
            List<LocationEntity> list = provider.GetList(entity, ref pageInfo);
            DataListResult<LocationEntity> result = new DataListResult<LocationEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list, PageInfo = pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 设为默认库位
        /// </summary>
        /// <returns></returns>
        public ActionResult SetDefault()
        {
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            LocationProvider provider = new LocationProvider(CompanyID);
            int line = provider.SetDefault(LocalNum);
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
        /// 启用或禁用库位
        /// </summary>
        /// <returns></returns>
        public ActionResult SetForbid()
        {
            string LocalNum = WebUtil.GetFormValue<string>("LocalNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int IsForbid = WebUtil.GetFormValue<int>("IsForbid");
            EBool EIsForbid = EnumHelper.GetModel<EBool>(IsForbid);
            LocationProvider provider = new LocationProvider(CompanyID);
            int line = provider.SetForbid(LocalNum, EIsForbid);
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
