using Git.Framework.Controller;
using Git.Storage.Common;
using Git.Storage.Entity.Check;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Storage.Common.Enum;
using Git.WMS.Sdk.ApiName;
using Newtonsoft.Json;
using Git.WMS.Sdk;

namespace Git.WMS.Web.Areas.Order.Controllers
{
    public class CheckAjaxController : AjaxPage
    {
        /// <summary>
        /// 新增或编辑盘点单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            InventoryOrderEntity entity = WebUtil.GetFormObject<InventoryOrderEntity>("Entity");
            List<InventoryDetailEntity> listDetail = Session[SessionKey.SESSION_CHECK_DETAIL] as List<InventoryDetailEntity>;
            string CompanyID = this.CompanyID;

            if (listDetail.IsNullOrEmpty())
            {
                DataResult<string> dataResult = new DataResult<string>() { Code = (int)EResponseCode.Exception, Message = "请选择要入库的产品" };
                return Content(JsonHelper.SerializeObject(dataResult));
            }
            string ApiName = CheckApiName.CheckApiName_Add;
            if (!entity.SnNum.IsEmpty())
            {
                ApiName = CheckApiName.CheckApiName_Edit;
            }

            entity.CreateUser = this.LoginUser.UserNum;
            entity.CreateTime = DateTime.Now;
            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.Status = (int)EAudite.Wait;
            entity.StorageNum = this.DefaultStorageNum;
            entity.Type = (int)ECheckType.Product;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("Entity", JsonConvert.SerializeObject(entity));
            dic.Add("List", JsonConvert.SerializeObject(listDetail));

            ITopClient client = new TopClientDefault();
            string result = client.Execute(ApiName, dic);

            return Content(result);
        }

        /// <summary>
        /// 加载盘点单详细
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult LoadDetail()
        {
            List<InventoryDetailEntity> listDetail = Session[SessionKey.SESSION_CHECK_DETAIL] as List<InventoryDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<InventoryDetailEntity>() : listDetail;

            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 5);
            PageInfo pageInfo = new PageInfo() { PageIndex = PageIndex, PageSize = PageSize };
            List<InventoryDetailEntity> listResult = listDetail.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
            pageInfo.RowCount = listDetail.Count;
            DataListResult<InventoryDetailEntity> dataResult = new DataListResult<InventoryDetailEntity>()
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
            List<InventoryDetailEntity> listDetail = Session[SessionKey.SESSION_CHECK_DETAIL] as List<InventoryDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<InventoryDetailEntity>() : listDetail;

            List<InventoryDetailEntity> list = WebUtil.GetFormObject<List<InventoryDetailEntity>>("List");
            if (!list.IsNullOrEmpty())
            {
                foreach (InventoryDetailEntity item in list)
                {
                    item.SnNum = item.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : item.SnNum;
                    if (!listDetail.Exists(a => a.TargetNum == item.TargetNum))
                    {
                        item.StorageNum = this.DefaultStorageNum;
                        item.CreateTime = DateTime.Now;
                        item.CompanyID = this.CompanyID;
                        listDetail.Add(item);
                    }
                }
            }
            Session[SessionKey.SESSION_CHECK_DETAIL] = listDetail;
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
            List<InventoryDetailEntity> listDetail = Session[SessionKey.SESSION_CHECK_DETAIL] as List<InventoryDetailEntity>;
            listDetail = listDetail.IsNull() ? new List<InventoryDetailEntity>() : listDetail;

            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            if (!SnNum.IsEmpty())
            {
                listDetail.Remove(a => a.SnNum == SnNum);
            }
            Session[SessionKey.SESSION_CHECK_DETAIL] = listDetail;
            DataResult dataResult = new DataResult()
            {
                Code = (int)EResponseCode.Success,
                Message = "响应成功"
            };
            return Content(JsonHelper.SerializeObject(dataResult));
        }

        /// <summary>
        /// 保存盘点差异单
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult SaveDif()
        {
            InventoryDifEntity entity = WebUtil.GetFormObject<InventoryDifEntity>("Entity");
            string CompanyID = this.CompanyID;

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("Entity", JsonConvert.SerializeObject(entity));

            ITopClient client = new TopClientDefault();
            string result = client.Execute(CheckApiName.CheckApiName_SaveDif, dic);

            return Content(result);
        }

        /// <summary>
        /// 新增盘差数据
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult AddDif()
        {
            InventoryDifEntity entity = WebUtil.GetFormObject<InventoryDifEntity>("Entity");
            string CompanyID = this.CompanyID;
            entity.FirstUser = this.LoginUser.UserNum;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("Entity", JsonConvert.SerializeObject(entity));

            ITopClient client = new TopClientDefault();
            string result = client.Execute(CheckApiName.CheckApiName_AddDif, dic);

            return Content(result);
        }

        /// <summary>
        /// 完成盘点作业
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter(true,false)]
        public ActionResult Complete()
        {
            string SnNum = WebUtil.GetFormValue<string>("SnNum");
            string CompanyID = this.CompanyID;
            
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("SnNum", SnNum);

            ITopClient client = new TopClientDefault();
            string result = client.Execute(CheckApiName.CheckApiName_Complete, dic);

            return Content(result);
        }
    }
}
