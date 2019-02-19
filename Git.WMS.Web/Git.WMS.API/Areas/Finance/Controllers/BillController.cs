using Git.Framework.Cache;
using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Finance;
using Git.Storage.Provider.Base;
using Git.Storage.Provider.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Finance.Controllers
{
    public class BillController : Controller
    {
        /// <summary>
        /// 新增财务应收应付
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            FinanceBillEntity entity = WebUtil.GetFormObject<FinanceBillEntity>("Entity");
            FinanceBillProvider provider = new FinanceBillProvider(CompanyID);
            int line = provider.Add(entity);

            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "新增成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "新增失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 编辑财务应收应付
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            FinanceBillEntity entity = WebUtil.GetFormObject<FinanceBillEntity>("Entity");
            FinanceBillProvider provider = new FinanceBillProvider(CompanyID);
            int line = provider.Edit(entity);

            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "编辑成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "编辑失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除财务应收应付
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            FinanceBillProvider provider = new FinanceBillProvider(CompanyID);
            int line = provider.Delete(list);

            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "删除成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>
        public ActionResult Audite()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            int Status = WebUtil.GetFormValue<int>("Status");
            FinanceBillProvider provider = new FinanceBillProvider(CompanyID);
            int line = provider.Audite(SnNum,Status);

            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = "审核成功";
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "审核失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询财务应收应付
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSingle()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            FinanceBillProvider provider = new FinanceBillProvider(CompanyID);
            FinanceBillEntity entity = provider.GetSingle(SnNum);
            DataResult<FinanceBillEntity> result = new DataResult<FinanceBillEntity>() 
            { 
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result=entity
            };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询财务应收应付分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string BillNum = WebUtil.GetFormValue<string>("BillNum");
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            int BillType = WebUtil.GetFormValue<int>("BillType",0);
            string FromNum = WebUtil.GetFormValue<string>("FromNum");
            string FromName = WebUtil.GetFormValue<string>("FromName");
            string ToNum = WebUtil.GetFormValue<string>("ToNum");
            string ToName = WebUtil.GetFormValue<string>("ToName");
            string ContractNum = WebUtil.GetFormValue<string>("ContractNum");
            int Status = WebUtil.GetFormValue<int>("Status");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string Title = WebUtil.GetFormValue<string>("Title");

            FinanceBillEntity entity = new FinanceBillEntity();
            entity.CompanyID = CompanyID;
            entity.BillNum = BillNum;
            entity.CateNum = CateNum;
            entity.BillType = BillType;
            entity.FromNum = FromNum;
            entity.FromName = FromName;
            entity.ToNum = ToNum;
            entity.ToName = ToName;
            entity.ContractNum = ContractNum;
            entity.Status = Status;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.Title = Title;

            PageInfo pageInfo = new PageInfo() { PageIndex=PageIndex,PageSize=PageSize };
            FinanceBillProvider provider = new FinanceBillProvider(CompanyID);
            List<FinanceBillEntity> listResult = provider.GetList(entity,ref pageInfo);
            DataListResult<FinanceBillEntity> result = new DataListResult<FinanceBillEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                PageInfo=pageInfo,
                Result = listResult
            };
            return Content(JsonHelper.SerializeObject(result));
        }
    }
}
