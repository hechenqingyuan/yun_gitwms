/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-09-19 20:55:26
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-09-19 20:55:26       情缘
*********************************************************************************/

using Git.Framework.Controller;
using Git.Framework.DataTypes;
using Git.Storage.Common;
using Git.Storage.Common.Enum;
using Git.Storage.Entity.Storage;
using Git.Storage.Provider.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Git.WMS.API.Areas.Sys.Controllers
{
    public class InventoryController : Controller
    {
        /// <summary>
        /// 查询台账记录分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList()
        {
            string CompanyID = WebUtil.GetFormValue<string>("CompanyID",string.Empty);
            string BarCode = WebUtil.GetFormValue<string>("BarCode",string.Empty);
            string ProductName = WebUtil.GetFormValue<string>("ProductName",string.Empty);
            string BatchNum = WebUtil.GetFormValue<string>("BatchNum", string.Empty);
            int Type = WebUtil.GetFormValue<int>("Type",0);
            string FromStorageNum = WebUtil.GetFormValue<string>("FromStorageNum");
            string OrderNum = WebUtil.GetFormValue<string>("OrderNum");
            string ContactOrder = WebUtil.GetFormValue<string>("ContactOrder");
            string BeginTime = WebUtil.GetFormValue<string>("BeginTime");
            string EndTime = WebUtil.GetFormValue<string>("EndTime");
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 0);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 0);

            InventoryBookEntity entity = new InventoryBookEntity();
            entity.CompanyID = CompanyID;
            entity.BarCode = BarCode;
            entity.ProductName = ProductName;
            entity.BatchNum = BatchNum;
            entity.Type = Type;
            entity.FromStorageNum = FromStorageNum;
            entity.OrderNum = OrderNum;
            entity.ContactOrder = ContactOrder;
            entity.BeginTime = BeginTime;
            entity.EndTime = EndTime;

            PageInfo pageInfo = new PageInfo() { PageIndex=PageIndex,PageSize=PageSize };
            InventoryBookProvider provider = new InventoryBookProvider(CompanyID);
            List<InventoryBookEntity> listResult = provider.GetList(entity, ref pageInfo);

            DataListResult<InventoryBookEntity> dataResult = new DataListResult<InventoryBookEntity>();
            dataResult.Code = (int)EResponseCode.Success;
            dataResult.Result = listResult;
            dataResult.Message = "响应成功";
            dataResult.PageInfo = pageInfo;

            return Content(JsonHelper.SerializeObject(dataResult));
        }
    }
}