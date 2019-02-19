using Git.Framework.Controller;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Git.Storage.Common;
using System.Data;
using Git.Storage.Common.Excel;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.OutStorage;

namespace Git.WMS.Web.Areas.Order.Controllers
{
    public class OutStorageAjaxController : AjaxPage
    {
        /// <summary>
        /// 新增或编辑出库单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            OutStorageEntity entity = WebUtil.GetFormObject<OutStorageEntity>("Entity");
            DateTime SendDate = WebUtil.GetFormValue<DateTime>("SendDate");
            List<OutStoDetailEntity> listDetail = Session[SessionKey.SESSION_OUTSTORAGE_DETAIL] as List<OutStoDetailEntity>;
            string CompanyID = this.CompanyID;

            if (listDetail.IsNullOrEmpty())
            {
                DataResult<string> dataResult = new DataResult<string>() { Code = (int)EResponseCode.Exception, Message = "请选择要出库的产品" };
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            string ApiName = OutStorageApiName.OutStorageApiName_Add;
            if (!entity.SnNum.IsEmpty())
            {
                ApiName = OutStorageApiName.OutStorageApiName_Edit;
            }

            entity.CreateUser = this.LoginUser.UserNum;
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.Status = (int)EAudite.Wait;
            entity.SendDate = SendDate;
            entity.CompanyID = CompanyID;
            entity.StorageNum = this.DefaultStorageNum;
            
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("Entity", JsonConvert.SerializeObject(entity));
            dic.Add("List", JsonConvert.SerializeObject(listDetail));

            ITopClient client = new TopClientDefault();
            string result = client.Execute(ApiName, dic);

            return Content(result);
        }

        /// <summary>
        /// 加载出库单详细
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult LoadDetail()
        {
            List<OutStoDetailEntity> listDetail = Session[SessionKey.SESSION_OUTSTORAGE_DETAIL] as List<OutStoDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<OutStoDetailEntity>() : listDetail;
            
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 5);
            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            List<OutStoDetailEntity> listResult = listDetail.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
            pageInfo.RowCount = listDetail.Count;
            DataListResult<OutStoDetailEntity> dataResult = new DataListResult<OutStoDetailEntity>()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功",
                Result = listResult,
                PageInfo = pageInfo
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
            List<OutStoDetailEntity> listDetail = Session[SessionKey.SESSION_OUTSTORAGE_DETAIL] as List<OutStoDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<OutStoDetailEntity>() : listDetail;

            List<OutStoDetailEntity> list = WebUtil.GetFormObject<List<OutStoDetailEntity>>("List");
            if (!list.IsNullOrEmpty())
            {
                list.ForEach(a => 
                {
                    a.SnNum = ConvertHelper.NewGuid();
                    a.Amount = a.OutPrice * a.Num;
                    a.CompanyID = this.CompanyID;
                    listDetail.Add(a);
                });
            }
            Session[SessionKey.SESSION_OUTSTORAGE_DETAIL] = listDetail;
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
            List<OutStoDetailEntity> listDetail = Session[SessionKey.SESSION_OUTSTORAGE_DETAIL] as List<OutStoDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<OutStoDetailEntity>() : listDetail;

            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            if (!SnNum.IsEmpty())
            {
                listDetail.Remove(a => a.SnNum == SnNum);
            }
            Session[SessionKey.SESSION_OUTSTORAGE_DETAIL] = listDetail;
            DataResult dataResult = new DataResult()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
