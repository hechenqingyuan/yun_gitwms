using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.InStorage;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Order.Controllers
{
    public class InStorageAjaxController : AjaxPage
    {
        /// <summary>
        /// 新增或编辑入库单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            InStorageEntity entity = WebUtil.GetFormObject<InStorageEntity>("Entity");
            List<InStorDetailEntity> listDetail = Session[SessionKey.SESSION_INSTORAGE_DETAIL] as List<InStorDetailEntity>;
            string CompanyID = this.CompanyID;

            if (listDetail.IsNullOrEmpty())
            {
                DataResult<string> dataResult = new DataResult<string>() { Code=(int)EResponseCode.Exception,Message="请选择要入库的产品" };
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            string ApiName = InStorageApiName.InStorageApiName_Add;
            if (!entity.SnNum.IsEmpty())
            {
                ApiName = InStorageApiName.InStorageApiName_Edit;
            }

            entity.CreateUser = this.LoginUser.UserNum;
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.Status = (int)EAudite.Wait;
            entity.StorageNum = this.DefaultStorageNum;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("Entity",JsonConvert.SerializeObject(entity));
            dic.Add("List",JsonConvert.SerializeObject(listDetail));

            ITopClient client = new TopClientDefault();
            string result = client.Execute(ApiName, dic);

            return Content(result);
        }

        /// <summary>
        /// 加载入库单详细
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult LoadDetail()
        {
            List<InStorDetailEntity> listDetail = Session[SessionKey.SESSION_INSTORAGE_DETAIL] as List<InStorDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<InStorDetailEntity>() : listDetail;
            
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 5);
            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            List<InStorDetailEntity> listResult = listDetail.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
            pageInfo.RowCount = listDetail.Count;
            DataListResult<InStorDetailEntity> dataResult = new DataListResult<InStorDetailEntity>() 
            { 
                Code=(int)EResponseCode.Success,
                Message="响应成功",
                Result = listResult,
                PageInfo=pageInfo
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult AddProduct()
        {
            List<InStorDetailEntity> listDetail = Session[SessionKey.SESSION_INSTORAGE_DETAIL] as List<InStorDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<InStorDetailEntity>() : listDetail;

            InStorDetailEntity entity = WebUtil.GetFormObject<InStorDetailEntity>("Entity");
            if (entity != null)
            {
                entity.SnNum = ConvertHelper.NewGuid();
                entity.CompanyID = this.CompanyID;
                entity.Amount = entity.InPrice * entity.Num;
                listDetail.Add(entity);
            }
            DataResult dataResult = new DataResult()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult DelProduct()
        {
            List<InStorDetailEntity> listDetail = Session[SessionKey.SESSION_INSTORAGE_DETAIL] as List<InStorDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<InStorDetailEntity>() : listDetail;

            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            if (!SnNum.IsEmpty())
            {
                listDetail.Remove(a => a.SnNum == SnNum);
            }
            Session[SessionKey.SESSION_INSTORAGE_DETAIL] = listDetail;
            DataResult dataResult = new DataResult()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
