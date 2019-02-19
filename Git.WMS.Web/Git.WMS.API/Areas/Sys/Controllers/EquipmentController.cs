using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Storage.Provider;
using Git.Storage.Provider.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Sys.Controllers
{
    public class EquipmentController : Controller
    {
        /// <summary>
        /// 新增设备
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string EquipmentName = WebUtil.GetFormValue<string>("EquipmentName");
            string EquipmentNum = WebUtil.GetFormValue<string>("EquipmentNum");
            int IsImpower = WebUtil.GetFormValue<int>("IsImpower",(int)EBool.No);
            string Flag = WebUtil.GetFormValue<string>("Flag",string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status",(int)EEquipmentStatus.Unused);
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string CreateUser = WebUtil.GetFormValue<string>("CreateUser",string.Empty);
            DateTime CreateTime=WebUtil.GetFormValue<DateTime>("CreateTime",DateTime.Now);

            EquipmentEntity entity = new EquipmentEntity();
            entity.SnNum = ConvertHelper.NewGuid();
            entity.EquipmentName = EquipmentName;
            entity.EquipmentNum = new TNumProvider(CompanyID).GetSwiftNum(typeof(EquipmentEntity), 5);
            entity.IsImpower = IsImpower;
            entity.Flag = Flag;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.Status = Status;
            entity.CreateUser = CreateUser;
            entity.CreateTime = CreateTime;
            entity.Remark = Remark;
            entity.CompanyID = CompanyID;

            EquipmentProvider provider = new EquipmentProvider(CompanyID);
            int line = provider.AddEquipment(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "设备新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "设备新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑设备
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string EquipmentName = WebUtil.GetFormValue<string>("EquipmentName");
            string EquipmentNum = WebUtil.GetFormValue<string>("EquipmentNum");
            int IsImpower = WebUtil.GetFormValue<int>("IsImpower", (int)EBool.No);
            string Flag = WebUtil.GetFormValue<string>("Flag", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status", (int)EEquipmentStatus.Unused);
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string CreateUser = WebUtil.GetFormValue<string>("CreateUser", string.Empty);
            DateTime CreateTime = WebUtil.GetFormValue<DateTime>("CreateTime", DateTime.Now);

            EquipmentEntity entity = new EquipmentEntity();
            entity.SnNum = SnNum;
            entity.EquipmentName = EquipmentName;
            entity.EquipmentNum = new TNumProvider(CompanyID).GetSwiftNum(typeof(EquipmentEntity), 5);
            entity.IsImpower = IsImpower;
            entity.Flag = Flag;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.Status = Status;
            entity.CreateUser = CreateUser;
            entity.CreateTime = CreateTime;
            entity.Remark = Remark;
            entity.CompanyID = CompanyID;

            EquipmentProvider provider = new EquipmentProvider(CompanyID);
            int line = provider.Edit(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "设备修改成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "设备修改失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            EquipmentProvider provider = new EquipmentProvider(CompanyID);
            int line = provider.Delete(list);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("设备删除成功,受影响行数{0}行", line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "设备删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询设备信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            EquipmentProvider provider = new EquipmentProvider(CompanyID);
            EquipmentEntity entity = provider.GetEquipment(SnNum);
            DataResult<EquipmentEntity> result = new DataResult<EquipmentEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询设备列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            
            EquipmentProvider provider = new EquipmentProvider(CompanyID);
            List<EquipmentEntity> list = provider.GetList();
            DataListResult<EquipmentEntity> result = new DataListResult<EquipmentEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
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
            string EquipmentName = WebUtil.GetFormValue<string>("EquipmentName");
            string Remark = WebUtil.GetFormValue<string>("Remark", string.Empty);
            string EquipmentNum = WebUtil.GetFormValue<string>("EquipmentNum", string.Empty);
            int Status = WebUtil.GetFormValue<int>("Status",0);
            string CreateUser = WebUtil.GetFormValue<string>("CreateUser");

            EquipmentEntity entity = new EquipmentEntity();
            entity.EquipmentName = EquipmentName;
            entity.Remark = Remark;
            entity.EquipmentNum = EquipmentNum;
            entity.Status = Status;
            entity.CreateUser = CreateUser;

            PageInfo pageInfo = new PageInfo();
            pageInfo.PageIndex = PageIndex;
            pageInfo.PageSize = PageSize;
            EquipmentProvider provider = new EquipmentProvider(CompanyID);
            List<EquipmentEntity> list = provider.GetList(entity, ref pageInfo);
            DataListResult<EquipmentEntity> result = new DataListResult<EquipmentEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list, PageInfo = pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 设备授权
        /// </summary>
        /// <returns></returns>
        public ActionResult Authorize()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            int IsImpower = WebUtil.GetFormValue<int>("IsImpower", (int)EBool.No);
            string Flag = WebUtil.GetFormValue<string>("Flag", string.Empty);
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            EquipmentProvider provider = new EquipmentProvider(CompanyID);
            int line = provider.Authorize(SnNum, IsImpower, Flag);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "操作成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "操作失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }
    }
}
