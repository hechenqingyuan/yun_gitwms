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
    public class PayController : Controller
    {
        /// <summary>
        /// 新增财务实收实付
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            FinancePayEntity entity = WebUtil.GetFormObject<FinancePayEntity>("Entity");
            FinancePayProvider provider = new FinancePayProvider(CompanyID);
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
        /// 编辑财务实收实付
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            FinancePayEntity entity = WebUtil.GetFormObject<FinancePayEntity>("Entity");
            FinancePayProvider provider = new FinancePayProvider(CompanyID);
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
        /// 删除财务实收实付
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormObject<string>("SnNum");
            FinancePayProvider provider = new FinancePayProvider(CompanyID);
            int line = provider.Delete(SnNum);

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
        /// 查询财务实收实付
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSingle()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            FinancePayProvider provider = new FinancePayProvider(CompanyID);
            FinancePayEntity entity = provider.GetSingle(SnNum);
            DataResult<FinancePayEntity> result = new DataResult<FinancePayEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = entity
            };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询实收实付详细
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string BillSnNum = WebUtil.GetFormValue<string>("BillSnNum");

            FinancePayProvider provider = new FinancePayProvider(CompanyID);
            List<FinancePayEntity> listResult = provider.GetList(BillSnNum);
            DataListResult<FinancePayEntity> result = new DataListResult<FinancePayEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult
            };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询财务实收实付分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string BillNum = WebUtil.GetFormValue<string>("BillNum");
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string ToName = WebUtil.GetFormValue<string>("ToName");
            string Title = WebUtil.GetFormValue<string>("Title");
            int PayType = WebUtil.GetFormValue<int>("PayType");
            string BankName = WebUtil.GetFormValue<string>("BankName");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string ContractNum = WebUtil.GetFormValue<string>("ContractNum");
            
            FinancePayEntity entity = new FinancePayEntity();
            entity.CompanyID = CompanyID;
            entity.PayType = PayType;
            entity.BankName = BankName;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.PayNum = BillNum;
            entity.CateNum = CateNum;
            entity.ToName = ToName;
            entity.Title = Title;
            entity.ContractNum = ContractNum;


            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            FinancePayProvider provider = new FinancePayProvider(CompanyID);
            List<FinancePayEntity> listResult = provider.GetList(entity, ref pageInfo);
            DataListResult<FinancePayEntity> result = new DataListResult<FinancePayEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                PageInfo = pageInfo,
                Result = listResult
            };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询流水记录
        /// </summary>
        /// <returns></returns>
        public ActionResult GeAgencyBilltList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string BillNum = WebUtil.GetFormValue<string>("BillNum");
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string ToName = WebUtil.GetFormValue<string>("ToName");
            string Title = WebUtil.GetFormValue<string>("Title");
            int PayType = WebUtil.GetFormValue<int>("PayType");
            string BankName = WebUtil.GetFormValue<string>("BankName");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            string ContractNum = WebUtil.GetFormValue<string>("ContractNum");
            string AgencyNum = WebUtil.GetFormValue<string>("AgencyNum");

            FinancePayEntity entity = new FinancePayEntity();
            entity.CompanyID = CompanyID;
            entity.PayType = PayType;
            entity.BankName = BankName;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;
            entity.PayNum = BillNum;
            entity.CateNum = CateNum;
            entity.ToName = ToName;
            entity.Title = Title;
            entity.ContractNum = ContractNum;

            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            FinancePayProvider provider = new FinancePayProvider(CompanyID);
            List<FinancePayEntity> listResult = provider.GeAgencyBilltList(entity, ref pageInfo);
            DataListResult<FinancePayEntity> result = new DataListResult<FinancePayEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                PageInfo = pageInfo,
                Result = listResult
            };
            return Content(JsonHelper.SerializeObject(result));
        }
    }
}
