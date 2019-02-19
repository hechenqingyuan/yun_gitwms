using Git.Framework.Controller;
using Git.Storage.Provider.Report;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Entity.Report;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Git.WMS.API.Areas.Report.Controllers
{
    public class ManagerController : Controller
    {
        /// <summary>
        /// 查询报表分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string ReportNum = WebUtil.GetFormValue<string>("ReportNum");
            string ReportName = WebUtil.GetFormValue<string>("ReportName");
            int ReportType = WebUtil.GetFormValue<int>("ReportType",0);
            int DsType = WebUtil.GetFormValue<int>("DsType",0);
            int Status = WebUtil.GetFormValue<int>("Status",0);
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            ReportsEntity entity = new ReportsEntity();
            PageInfo pageInfo = new PageInfo() { PageIndex=PageIndex,PageSize=PageSize };
            ReportProvider provider = new ReportProvider(CompanyID);
            if (ReportNum.IsNotEmpty())
            {
                entity.And("ReportNum", ECondition.Like, "%" + ReportNum + "%");
            }
            if (ReportName.IsNotEmpty())
            {
                entity.And("ReportName", ECondition.Like, "%" + ReportName + "%");
            }
            if (ReportType>0)
            {
                entity.And(a => a.ReportType == ReportType);
            }
            if (DsType > 0)
            {
                entity.And(a => a.DsType == DsType);
            }
            if (Status > 0)
            {
                entity.And(a => a.Status == Status);
            }
            List<ReportsEntity> listResult = provider.GetList(entity,ref pageInfo);
            DataListResult<ReportsEntity> dataResult = new DataListResult<ReportsEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 新增自定义报表
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            ReportsEntity entity = WebUtil.GetFormObject<ReportsEntity>("Entity");
            List<ReportParamsEntity> list = WebUtil.GetFormObject<List<ReportParamsEntity>>("List");
            ReportProvider provider = new ReportProvider(CompanyID);
            int line=provider.Create(entity,list);
            DataResult result = new DataResult();
            if (line>0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "自定义报表新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "自定义报表新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑自定义报表
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            ReportsEntity entity = WebUtil.GetFormObject<ReportsEntity>("Entity");
            List<ReportParamsEntity> list = WebUtil.GetFormObject<List<ReportParamsEntity>>("List");
            ReportProvider provider = new ReportProvider(CompanyID);
            int line = provider.Update(entity, list);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "自定义报表编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "自定义报表编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除报表
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("list");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            ReportProvider provider = new ReportProvider(CompanyID);
            int returnValue = provider.Delete(list);
            DataResult result = new DataResult();
            if (returnValue>0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "自定义报表删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "自定义报表删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 获得报表主体
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSingle()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string SnNum = WebUtil.GetFormValue<string>("SnNum",string.Empty);
            ReportProvider provider = new ReportProvider(CompanyID);
            ReportsEntity entity = provider.GetReport(SnNum);
            DataResult<ReportsEntity> result = new DataResult<ReportsEntity>();
            result.Code = (int)EResponseCode.Success;
            result.Message = "响应成功";
            result.Result = entity;
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 获得报表参数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetParameter()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string SnNum = WebUtil.GetFormValue<string>("SnNum", string.Empty);
            ReportProvider provider = new ReportProvider(CompanyID);
            List<ReportParamsEntity> listResult = provider.GetParams(SnNum);
            DataResult<List<ReportParamsEntity>> result = new DataResult<List<ReportParamsEntity>>();
            result.Code = (int)EResponseCode.Success;
            result.Message = "响应成功";
            result.Result = listResult;
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 获得存储过程参数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProcParameter()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            string argProceName = WebUtil.GetFormValue<string>("ProceName", string.Empty);
            ReportProvider provider = new ReportProvider(CompanyID);
            List<ReportParamsEntity> listResult = provider.GetProceMetadata(argProceName);
            DataResult<List<ReportParamsEntity>> result = new DataResult<List<ReportParamsEntity>>();
            result.Code = (int)EResponseCode.Success;
            result.Message = "响应成功";
            result.Result = listResult;
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDataSource()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID", string.Empty);
            ReportsEntity entity = WebUtil.GetFormObject<ReportsEntity>("Entity");
            List<ReportParamsEntity> list = WebUtil.GetFormObject<List<ReportParamsEntity>>("List");
            int orderType = WebUtil.GetFormValue<int>("OrderType",(int)EReportType.Report);
            string orderNum = WebUtil.GetFormValue<string>("OrderNum",string.Empty);

            ReportProvider provider = new ReportProvider(CompanyID);
            DataSet dataSet = provider.GetDataSource(entity, list, orderType, orderNum);

            DataResult<DataSet> dataResult = new DataResult<DataSet>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Message = "响应成功";
            dataResult.Result = dataSet;

            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
