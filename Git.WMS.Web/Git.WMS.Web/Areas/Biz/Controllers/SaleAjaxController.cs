using Git.Framework.Controller;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Biz;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Git.WMS.Sdk;
using Git.Framework.DataTypes;

namespace Git.WMS.Web.Areas.Biz.Controllers
{
    public class SaleAjaxController : AjaxPage
    {
        /// <summary>
        /// 新增或编辑销售订单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            SaleOrderEntity entity = WebUtil.GetFormObject<SaleOrderEntity>("Entity");
            string OrderTime = WebUtil.GetFormValue<string>("OrderTime",string.Empty);
            string SendDate = WebUtil.GetFormValue<string>("SendDate", string.Empty);
            if (!OrderTime.IsEmpty())
            {
                entity.OrderTime = ConvertHelper.ToType<DateTime>(OrderTime);
            }
            if (!SendDate.IsEmpty())
            {
                entity.SendDate = ConvertHelper.ToType<DateTime>(SendDate);
            }
            List<SaleDetailEntity> listDetail = Session[SessionKey.SESSION_SALEORDER_DETAIL] as List<SaleDetailEntity>;
            string CompanyID = this.CompanyID;

            if (listDetail.IsNullOrEmpty())
            {
                DataResult<string> dataResult = new DataResult<string>() { Code = (int)EResponseCode.Exception, Message = "请选择要入库的产品" };
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            string ApiName = SaleApiName.SaleApiName_Add;
            if (!entity.SnNum.IsEmpty())
            {
                ApiName = SaleApiName.SaleApiName_Edit;
            }

            entity.CreateUser = this.LoginUser.UserNum;
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.Status = (int)EAudite.Wait;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("Entity", JsonConvert.SerializeObject(entity));
            dic.Add("List", JsonConvert.SerializeObject(listDetail));

            ITopClient client = new TopClientDefault();
            string result = client.Execute(ApiName, dic);

            return Content(result);
        }

        /// <summary>
        /// 加载销售订单详细
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult LoadDetail()
        {
            List<SaleDetailEntity> listDetail = Session[SessionKey.SESSION_SALEORDER_DETAIL] as List<SaleDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<SaleDetailEntity>() : listDetail;

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 5);
            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            List<SaleDetailEntity> listResult = listDetail.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
            pageInfo.RowCount = listDetail.Count;
            DataListResult<SaleDetailEntity> dataResult = new DataListResult<SaleDetailEntity>()
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
            List<SaleDetailEntity> listDetail = Session[SessionKey.SESSION_SALEORDER_DETAIL] as List<SaleDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<SaleDetailEntity>() : listDetail;

            List<SaleDetailEntity>  list = WebUtil.GetFormObject<List<SaleDetailEntity>>("List");
            if (!list.IsNullOrEmpty())
            {
                foreach (SaleDetailEntity item in list)
                {
                    if (listDetail.Exists(a => a.ProductNum == item.ProductNum))
                    {
                        SaleDetailEntity entity = listDetail.First(a => a.ProductNum == item.ProductNum);
                        entity.Num = entity.Num + item.Num;
                        entity.Amount = entity.Price * entity.Num;
                    }
                    else
                    {
                        SaleDetailEntity entity = new SaleDetailEntity();
                        entity.SnNum = ConvertHelper.NewGuid();
                        entity.ProductName = item.ProductName;
                        entity.BarCode = item.BarCode;
                        entity.ProductNum = item.ProductNum;
                        entity.Num = item.Num;
                        entity.UnitNum = item.UnitNum;
                        entity.UnitName = item.UnitName;
                        entity.Price = item.Price;
                        entity.Amount = entity.Price * entity.Num;
                        entity.Status = (int)EOrderStatus.CreateOrder;
                        entity.CreateTime = DateTime.Now;
                        entity.CompanyID = this.CompanyID;
                        entity.Size = item.Size;
                        listDetail.Add(entity);
                    }
                }
            }
            Session[SessionKey.SESSION_SALEORDER_DETAIL] = listDetail;
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
            List<SaleDetailEntity> listDetail = Session[SessionKey.SESSION_SALEORDER_DETAIL] as List<SaleDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<SaleDetailEntity>() : listDetail;

            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            if (!SnNum.IsEmpty())
            {
                listDetail.Remove(a => a.SnNum == SnNum);
            }
            Session[SessionKey.SESSION_SALEORDER_DETAIL] = listDetail;
            DataResult dataResult = new DataResult()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}
