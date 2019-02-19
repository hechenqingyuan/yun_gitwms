using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
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
    public class CustomerController : Controller
    {

        /// <summary>
        /// 新增客户
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            CustomerEntity entity = WebUtil.GetFormObject<CustomerEntity>("Entity");
            List<CusAddressEntity> list = WebUtil.GetFormObject<List<CusAddressEntity>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID",string.Empty);

            DataResult dataResult = new DataResult();
            if (entity.IsNull())
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "请填写客户信息";
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            if (list.IsNullOrEmpty())
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "请填写客户地址";
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            if (entity.CusName.IsEmpty())
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "请输入客户名";
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            entity.SnNum = ConvertHelper.NewGuid();
            entity.CusNum = new TNumProvider(CompanyID).GetSwiftNum(typeof(CustomerEntity), 5);

            list.ForEach(a => 
            {
                a.SnNum = a.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : a.SnNum;
                a.CustomerSN = entity.SnNum;
            });

            CustomerProvider provider = new CustomerProvider(entity.CompanyID);
            int line = provider.AddCustomer(entity,list);
            
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "客户新增成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "客户新增失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 编辑客户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            CustomerEntity entity = WebUtil.GetFormObject<CustomerEntity>("Entity");
            List<CusAddressEntity> list = WebUtil.GetFormObject<List<CusAddressEntity>>("List");

            DataResult dataResult = new DataResult();
            if (entity.IsNull())
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "请填写客户信息";
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            if (list.IsNullOrEmpty())
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "请填写客户地址";
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            if (entity.CusName.IsEmpty())
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "请输入客户名";
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            
            list.ForEach(a =>
            {
                a.SnNum = a.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : a.SnNum;
                a.CustomerSN = entity.SnNum;
            });
            CustomerProvider provider = new CustomerProvider(entity.CompanyID);
            int line = provider.Update(entity,list); ;

            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "客户编辑成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "客户编辑失败";
            }
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            List<string> list = WebUtil.GetFormObject<List<string>>("List");
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            CustomerProvider provider = new CustomerProvider(CompanyID);
            int line = provider.Delete(list);
            DataResult result = new DataResult();
            if (line > 0)
            {
                result.Code = (int)EResponseCode.Success;
                result.Message = string.Format("客户删除成功,受影响行数{0}行", line);
            }
            else
            {
                result.Code = (int)EResponseCode.Exception;
                result.Message = "客户删除失败";
            }
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Single()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SN = WebUtil.GetFormValue<string>("SN");
            CustomerProvider provider = new CustomerProvider(CompanyID);
            CustomerEntity entity = provider.GetSingleCustomer(SN);
            DataResult<CustomerEntity> result = new DataResult<CustomerEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询客户分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPage()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);
            string CusNum = WebUtil.GetFormValue<string>("CusNum", string.Empty);
            string CusName = WebUtil.GetFormValue<string>("CusName", string.Empty);
            string Phone = WebUtil.GetFormValue<string>("Phone", string.Empty);
            int CusType = WebUtil.GetFormValue<int>("CusType", 0);
            CustomerEntity entity = new CustomerEntity();
            entity.CusNum = CusNum;
            entity.CusName = CusName;
            entity.Phone = Phone;
            entity.CusType = CusType;
            PageInfo pageInfo = new PageInfo();
            pageInfo.PageIndex = PageIndex;
            pageInfo.PageSize = PageSize;
            CustomerProvider provider = new CustomerProvider(CompanyID);
            List<CustomerEntity> list = provider.GetCustomerList(entity, ref pageInfo);
            DataListResult<CustomerEntity> result = new DataListResult<CustomerEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list, PageInfo = pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询客户地址分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAddressListPage()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string Address = WebUtil.GetFormValue<string>("Address", string.Empty);
            string Phone = WebUtil.GetFormValue<string>("Phone", string.Empty);
            string CusNum = WebUtil.GetFormValue<string>("CusNum", string.Empty);
            string CusName = WebUtil.GetFormValue<string>("CusName", string.Empty);
            int CusType = WebUtil.GetFormValue<int>("CusType",0);

            CusAddressEntity entity = new CusAddressEntity();
            entity.Address = Address;
            entity.Phone = Phone;
            entity.CusNum = CusNum;
            entity.CusName = CusName;

            PageInfo pageInfo = new PageInfo();
            pageInfo.PageIndex = PageIndex;
            pageInfo.PageSize = PageSize;

            CustomerProvider provider = new CustomerProvider(CompanyID);
            List<CusAddressEntity> list = provider.GetList(entity, ref pageInfo);
            DataListResult<CusAddressEntity> result = new DataListResult<CusAddressEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list, PageInfo = pageInfo };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询客户地址
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAddressList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string CustomerSN = WebUtil.GetFormValue<string>("CustomerSN");
            CustomerProvider provider = new CustomerProvider(CompanyID);
            List<CusAddressEntity> list = provider.GetAddressList(CustomerSN);
            DataListResult<CusAddressEntity> result = new DataListResult<CusAddressEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list};
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 查询客户地址
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAddress()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            CustomerProvider provider = new CustomerProvider(CompanyID);
            CusAddressEntity entity = provider.GetSingleAddress(SnNum);
            DataResult<CusAddressEntity> result = new DataResult<CusAddressEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = entity };
            return Content(JsonHelper.SerializeObject(result));
        }

        /// <summary>
        /// 删除地址
        /// </summary>
        /// <returns></returns>
        public ActionResult DelAddress()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CustomerSN = WebUtil.GetFormValue<string>("CustomerSN");
            CustomerProvider provider = new CustomerProvider(CompanyID);
            int line = provider.DeleteAddress(SnNum,CustomerSN);
            DataResult result = null;
            if (line > 0)
            {
                result = new DataResult() { Code = (int)EResponseCode.Success, Message = "删除成功" };
            }
            else
            {
                result = new DataResult() { Code = (int)EResponseCode.Exception, Message = "删除失败" };
            }
            return Content(JsonHelper.SerializeObject(result));
        }


        /// <summary>
        /// 搜索客户资料信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchCustomer()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID");
            string Keyword = WebUtil.GetFormValue<string>("Keyword");
            int TopSize = WebUtil.GetFormValue<int>("TopSize",10);
            CustomerProvider provider = new CustomerProvider(CompanyID);
            List<CusAddressEntity> list = provider.SearchCustomer(Keyword,TopSize);
            DataListResult<CusAddressEntity> result = new DataListResult<CusAddressEntity>() { Code = (int)EResponseCode.Success, Message = "响应成功", Result = list };
            return Content(JsonHelper.SerializeObject(result));
        }
    }
}
