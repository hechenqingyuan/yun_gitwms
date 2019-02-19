using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Allocate;
using Git.Storage.Entity.Storage;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.Web.Areas.Order.Controllers
{
    public class AllocateController : MasterPage
    {
        /// <summary>
        /// 调拨单分页列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult List()
        {
            ViewBag.Allocate = EnumHelper.GetOptions<EAllocateType>(0);
            return View();
        }

        /// <summary>
        /// 新增或编辑调拨单
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Add()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            AllocateOrderEntity entity = null;
            List<AllocateDetailEntity> list = null;

            ITopClient client = new TopClientDefault();
            
            if (!SnNum.IsEmpty())
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(AllocateApiName.AllocateApiName_GetOrder, dic);
                DataResult<AllocateOrderEntity> dataResult = JsonConvert.DeserializeObject<DataResult<AllocateOrderEntity>>(result);
                entity = dataResult.Result;

                result = client.Execute(AllocateApiName.AllocateApiName_GetDetail, dic);
                DataResult<List<AllocateDetailEntity>> dataList = JsonConvert.DeserializeObject<DataResult<List<AllocateDetailEntity>>>(result);
                list = dataList.Result;
            }
            if (entity.IsNull())
            {
                entity = new AllocateOrderEntity();
                entity.CreateUser = this.LoginUser.UserNum;
                entity.CreateUserName = this.LoginUser.UserName;
                entity.CreateTime = DateTime.Now;
            }
            ViewBag.Entity = entity;

            list = list.IsNull() ? new List<AllocateDetailEntity>() : list;
            Session[SessionKey.SESSION_ALLOCATE_DETAIL] = list;

            ViewBag.AllocateType = EnumHelper.GetOptions<EAllocateType>(entity.AllocateType);

            //加载仓库
            Dictionary<string, string>  dicStorage = new Dictionary<string, string>();
            dicStorage.Add("CompanyID", CompanyID);
            dicStorage.Add("PageIndex", "1");
            dicStorage.Add("PageSize", "100");
            string storageResult = client.Execute(StorageApiName.StorageApiName_GetPage, dicStorage);
            string StorageList = string.Empty;
            string defautStorageNum = list.Count > 0 ? list[0].ToStorageNum : "";
            if (storageResult.IsNotEmpty())
            {
                DataListResult<StorageEntity> dataStorage = JsonConvert.DeserializeObject<DataListResult<StorageEntity>>(storageResult);
                List<StorageEntity> listStorage = dataStorage.Result;
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value=''>请选择</option>");
                if (!listStorage.IsNullOrEmpty())
                {
                    foreach (StorageEntity item in listStorage)
                    {
                        sb.AppendFormat("<option value='{0}' {1}>{2}</option>", item.SnNum, item.SnNum == defautStorageNum ? "selected='selected'" : "", item.StorageName);
                    }
                }
                StorageList = sb.ToString();
            }
            ViewBag.Storage = StorageList;
            return View();
        }

        /// <summary>
        /// 调拨单详细
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Detail()
        {
            string SnNum = WebUtil.GetQueryStringValue<string>("SnNum");

            AllocateOrderEntity entity = null;

            if (!SnNum.IsEmpty())
            {
                ITopClient client = new TopClientDefault();
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("CompanyID", CompanyID);
                dic.Add("SnNum", SnNum);

                string result = client.Execute(AllocateApiName.AllocateApiName_GetOrder, dic);
                DataResult<AllocateOrderEntity> dataResult = JsonConvert.DeserializeObject<DataResult<AllocateOrderEntity>>(result);
                entity = dataResult.Result;

            }
            entity = entity.IsNull() ? new AllocateOrderEntity() : entity;
            ViewBag.Entity = entity;
            return View();
        }

        /// <summary>
        /// 新增产品对话框
        /// </summary>
        /// <returns></returns>
        [LoginFilter(true,false)]
        public ActionResult AddProduct()
        {
            return View();
        }

    }
}
