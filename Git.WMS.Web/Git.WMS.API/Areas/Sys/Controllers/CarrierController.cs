using Git.Framework.Controller;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Sys;
using Git.Storage.Provider.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;

namespace Git.WMS.API.Areas.Sys.Controllers
{
    public class CarrierController : Controller
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CarrierName = WebUtil.GetFormValue<string>("CarrierName");
            string CarrierNum = WebUtil.GetFormValue<string>("CarrierNum");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            CarrierEntity entity = new CarrierEntity();
            entity.SnNum = ConvertHelper.NewGuid();
            entity.CarrierName = CarrierName;
            entity.CarrierNum = CarrierNum;
            entity.Remark = Remark;
            entity.CompanyID = CompanyID;

            CarrierProvider provider = new CarrierProvider(CompanyID);
            int line = provider.Add(entity);

            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "新增成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "新增失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        public ActionResult Edit()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CarrierName = WebUtil.GetFormValue<string>("CarrierName");
            string CarrierNum = WebUtil.GetFormValue<string>("CarrierNum");
            string Remark = WebUtil.GetFormValue<string>("Remark");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            CarrierEntity entity = new CarrierEntity();
            entity.SnNum = SnNum;
            entity.CarrierName = CarrierName;
            entity.CarrierNum = CarrierNum;
            entity.Remark = Remark;
            entity.CompanyID = CompanyID;


            CarrierProvider provider = new CarrierProvider(CompanyID);

            int line = provider.Edit(entity);
            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "编辑成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "编辑失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            CarrierProvider provider = new CarrierProvider(CompanyID);

            int line = provider.Delete(SnNum);
            DataResult dataResult = new DataResult();
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "删除成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "删除失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSingle()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");

            CarrierProvider provider = new CarrierProvider(CompanyID);
            CarrierEntity entity = provider.GetSingle(SnNum);

            DataResult<CarrierEntity> dataResult = new DataResult<CarrierEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Result = entity;
            dataResult.Message = "响应成功";

            return Content(JsonHelper.SerializeObject(dataResult));
        }


        /// <summary>
        /// 查询分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string CarrierNum = WebUtil.GetFormValue<string>("CarrierNum");
            string CarrierName = WebUtil.GetFormValue<string>("CarrierName");

            CarrierEntity entity = new CarrierEntity();
            entity.CompanyID = CompanyID;
            entity.CarrierNum = CarrierNum;
            entity.CarrierName = CarrierName;

            CarrierProvider provider = new CarrierProvider(CompanyID);
            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            List<CarrierEntity> listResult = provider.GetList(entity, ref pageInfo);
            DataListResult<CarrierEntity> dataResult = new DataListResult<CarrierEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Result = listResult;
            dataResult.PageInfo = pageInfo;
            dataResult.Message = "响应成功";

            return Content(JsonHelper.SerializeObject(dataResult));
        }

    }
}
