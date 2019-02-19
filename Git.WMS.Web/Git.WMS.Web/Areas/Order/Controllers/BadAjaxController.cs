using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Bad;
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
    public class BadAjaxController : AjaxPage
    {
        /// <summary>
        /// 新增或编辑报损单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
             BadReportEntity entity = WebUtil.GetFormObject<BadReportEntity>("Entity");
            List<BadReportDetailEntity> listDetail = Session[SessionKey.SESSION_BAD_DETAIL] as List<BadReportDetailEntity>;
            string CompanyID = this.CompanyID;

            if (listDetail.IsNullOrEmpty())
            {
                DataResult dataResult = new DataResult() { Code=(int)EResponseCode.Exception,Message="请选择要报损的产品" };
                return Content(JsonHelper.SerializeObject(dataResult));
            }

            string ApiName = BadApiName.BadApiName_Add;
            if (!entity.SnNum.IsEmpty())
            {
                ApiName = BadApiName.BadApiName_Edit;
            }
            entity.CreateUser = this.LoginUser.UserNum;
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.Status = (int)EAudite.Wait;
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
        /// 加载报损单详细
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult LoadDetail()
        {
            List<BadReportDetailEntity> listDetail = Session[SessionKey.SESSION_BAD_DETAIL] as List<BadReportDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<BadReportDetailEntity>() : listDetail;

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 5);
            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            List<BadReportDetailEntity> listResult = listDetail.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
            pageInfo.RowCount = listDetail.Count;
            DataListResult<BadReportDetailEntity> dataResult = new DataListResult<BadReportDetailEntity>()
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
            List<BadReportDetailEntity> listDetail = Session[SessionKey.SESSION_BAD_DETAIL] as List<BadReportDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<BadReportDetailEntity>() : listDetail;

            List<BadReportDetailEntity> list = WebUtil.GetFormObject<List<BadReportDetailEntity>>("List");
            if (!list.IsNullOrEmpty())
            {
                list.ForEach(a =>
                {
                    a.SnNum = ConvertHelper.NewGuid();
                    a.Amount = a.InPrice * a.Num;
                    a.CompanyID = this.CompanyID;
                    listDetail.Add(a);
                });
            }
            Session[SessionKey.SESSION_BAD_DETAIL] = listDetail;
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
            List<BadReportDetailEntity> listDetail = Session[SessionKey.SESSION_BAD_DETAIL] as List<BadReportDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<BadReportDetailEntity>() : listDetail;

            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            if (!SnNum.IsEmpty())
            {
                listDetail.Remove(a => a.SnNum == SnNum);
            }
            Session[SessionKey.SESSION_BAD_DETAIL] = listDetail;
            DataResult dataResult = new DataResult()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
