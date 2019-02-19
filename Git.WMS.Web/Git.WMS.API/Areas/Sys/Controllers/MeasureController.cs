using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Storage.Entity.Sys;
using Git.Storage.Provider;
using Git.Storage.Provider.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Sys.Controllers
{
    public class MeasureController : Controller
    {
        /// <summary>
        /// 新增单位
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string MeasureName = WebUtil.GetFormValue<string>("MeasureName");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            MeasureEntity entity = new MeasureEntity();
            entity.MeasureName = MeasureName;
            entity.CompanyID = CompanyID;
            entity.SN = ConvertHelper.NewGuid();
            entity.MeasureNum = new TNumProvider(CompanyID).GetSwiftNum(typeof(MeasureEntity), 5);
            MeasureProvider provider = new MeasureProvider(CompanyID);
            int line = provider.AddMeasure(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "单位新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "单位新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑单位
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string MeasureName = WebUtil.GetFormValue<string>("MeasureName");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SN = WebUtil.GetFormValue<string>("SN");
            string MeasureNum = WebUtil.GetFormValue<string>("MeasureNum");

            MeasureEntity entity = new MeasureEntity();
            entity.MeasureName = MeasureName;
            entity.CompanyID = CompanyID;
            entity.SN = SN;
            entity.MeasureNum = MeasureNum;

            MeasureProvider provider = new MeasureProvider(CompanyID);
            int line = provider.EditMeasure(entity);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "单位修改成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "单位修改失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            MeasureProvider provider = new MeasureProvider(CompanyID);
            int line = provider.DeleteMeasure(list, CompanyID);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("单位删除成功,受影响行数{0}行", line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "单位删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SN = WebUtil.GetFormValue<string>("SN");
            MeasureProvider provider = new MeasureProvider(CompanyID);
            MeasureEntity entity = provider.GetMeasure(SN);
            DataResult<MeasureEntity> result = new DataResult<MeasureEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询单位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            MeasureProvider provider = new MeasureProvider(CompanyID);
            List<MeasureEntity> list = provider.GetList();
            DataListResult<MeasureEntity> result = new DataListResult<MeasureEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
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
            string MeasureName = WebUtil.GetFormValue<string>("MeasureName");
            string MeasureNum = WebUtil.GetFormValue<string>("MeasureNum");
            MeasureEntity entity = new MeasureEntity();
            entity.MeasureName = MeasureName;
            entity.MeasureNum = MeasureNum;
            entity.CompanyID = CompanyID;
            PageInfo pageInfo = new PageInfo();
            pageInfo.PageIndex = PageIndex;
            pageInfo.PageSize = PageSize;
            MeasureProvider provider = new MeasureProvider(CompanyID);
            List<MeasureEntity> list = provider.GetList(entity, ref pageInfo);
            DataListResult<MeasureEntity> result = new DataListResult<MeasureEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list, PageInfo = pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }


        /// <summary>
        /// 查看产品包装情况
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPackageList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string ProductNum = WebUtil.GetFormValue<string>("ProductNum");

            MeasureProvider provider = new MeasureProvider(CompanyID);
            List<MeasureRelEntity> listResult = provider.GetMeasureRel(ProductNum);

            DataResult<List<MeasureRelEntity>> dataResult = new DataResult<List<MeasureRelEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = listResult;

            return Json(dataResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增产品包装结构
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPackage()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string MeasureSource = WebUtil.GetFormValue<string>("MeasureSource");
            string MeasureTarget = WebUtil.GetFormValue<string>("MeasureTarget");
            double Rate = WebUtil.GetFormValue<double>("Rate", 1);
            string ProductNum = WebUtil.GetFormValue<string>("ProductNum");

            MeasureRelEntity entity = new MeasureRelEntity();
            entity.CompanyID = CompanyID;
            entity.MeasureSource = MeasureSource;
            entity.MeasureTarget = MeasureTarget;
            entity.Rate = Rate;
            entity.ProductNum = ProductNum;
            entity.SN = ConvertHelper.NewGuid();

            MeasureProvider provider = new MeasureProvider(CompanyID);
            int line = provider.AddMeasureRel(entity);

            DataResult dataResult = new DataResult();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "新增成功";

            return Json(dataResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除产品包装结构
        /// </summary>
        /// <returns></returns>
        public ActionResult DeletePacage()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");

            MeasureProvider provider = new MeasureProvider(CompanyID);
            int line = provider.DeleteMeasureRel(SnNum);

            DataResult dataResult = new DataResult();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "删除成功";

            return Json(dataResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询产品单位
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductUnit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string ProductNum = WebUtil.GetFormValue<string>("ProductNum");

            MeasureProvider provider = new MeasureProvider(CompanyID);
            List<MeasureEntity> listResult = provider.GetList(ProductNum);

            DataResult<List<MeasureEntity>> dataResult = new DataResult<List<MeasureEntity>>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = listResult;

            return Json(dataResult, JsonRequestBehavior.AllowGet);
        } 
    }
}
