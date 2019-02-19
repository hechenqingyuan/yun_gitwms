using Git.Framework.Controller;
using Git.Storage.Entity.Sys;
using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Newtonsoft.Json;
using System.Data;
using Git.Storage.Common.Excel;
using Git.Storage.Common.Enum;
using Git.WMS.Web.Lib;
using Git.WMS.Web.Lib.Filter;
using Git.Storage.Entity.Storage;

namespace Git.WMS.Web.Areas.Storage.Controllers
{
    public class ProductAjaxController : AjaxPage
    {
        /// <summary>
        /// 查询所有产品列表
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult GetList()
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string CompanyID = this.CompanyID;
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string UnitNum = WebUtil.GetFormValue<string>("UnitNum");
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string Size = WebUtil.GetFormValue<string>("Size");
            string Color = WebUtil.GetFormValue<string>("Color");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string DefaultLocal = WebUtil.GetFormValue<string>("DefaultLocal");
            string CusNum = WebUtil.GetFormValue<string>("CusNum");
            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string Display = WebUtil.GetFormValue<string>("Display");
            string Remark = WebUtil.GetFormValue<string>("Remark");

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("UnitNum", UnitNum);
            dic.Add("CateNum", CateNum);
            dic.Add("Size", Size);
            dic.Add("Color", Color);
            dic.Add("StorageNum", StorageNum);
            dic.Add("DefaultLocal", DefaultLocal);
            dic.Add("CusNum", CusNum);
            dic.Add("SupNum", SupNum);
            dic.Add("Display", Display);
            dic.Add("Remark", Remark);

            string result = client.Execute(ProductApiName.ProductApiName_GetPage, dic);
            return Content(result);
        }

        /// <summary>
        /// 新增产品,编辑产品
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Add()
        {
            ITopClient client = new TopClientDefault();

            ProductEntity entity = WebUtil.GetFormObject<ProductEntity>("Entity");
            entity.CompanyID = this.CompanyID;
            entity.CreateUser = this.LoginUser.UserNum;
            entity.CreateTime = DateTime.Now;
            entity.IsSingle = (int)EProductPackage.Single;

            string CompanyID = this.CompanyID;
            
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Entity", JsonConvert.SerializeObject(entity));
            dic.Add("CompanyID", CompanyID);

            string ApiName = ProductApiName.ProductApiName_Add;
            if (!entity.SnNum.IsEmpty())
            {
                ApiName = ProductApiName.ProductApiName_Edit;
            }
            string result = client.Execute(ApiName, dic);
            return Content(result);
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult Delete()
        {
            ITopClient client = new TopClientDefault();
            string list = WebUtil.GetFormValue<string>("list");
            string CompanyID = this.CompanyID;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("List", list);
            string result = client.Execute(ProductApiName.ProductApiName_Delete, dic);
            return Content(result);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        [LoginAjaxFilter]
        public ActionResult ToExcel()
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string CompanyID = this.CompanyID;
            int PageIndex = WebUtil.GetFormValue<int>("PageIndex", 1);
            int PageSize = WebUtil.GetFormValue<int>("PageSize", 10);

            string BarCode = WebUtil.GetFormValue<string>("BarCode");
            string ProductName = WebUtil.GetFormValue<string>("ProductName");
            string UnitNum = WebUtil.GetFormValue<string>("UnitNum");
            string CateNum = WebUtil.GetFormValue<string>("CateNum");
            string Size = WebUtil.GetFormValue<string>("Size");
            string Color = WebUtil.GetFormValue<string>("Color");
            string StorageNum = WebUtil.GetFormValue<string>("StorageNum");
            string DefaultLocal = WebUtil.GetFormValue<string>("DefaultLocal");
            string CusNum = WebUtil.GetFormValue<string>("CusNum");
            string SupNum = WebUtil.GetFormValue<string>("SupNum");
            string Display = WebUtil.GetFormValue<string>("Display");
            string Remark = WebUtil.GetFormValue<string>("Remark");

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("BarCode", BarCode);
            dic.Add("ProductName", ProductName);
            dic.Add("UnitNum", UnitNum);
            dic.Add("CateNum", CateNum);
            dic.Add("Size", Size);
            dic.Add("Color", Color);
            dic.Add("StorageNum", StorageNum);
            dic.Add("DefaultLocal", DefaultLocal);
            dic.Add("CusNum", CusNum);
            dic.Add("SupNum", SupNum);
            dic.Add("Display", Display);
            dic.Add("Remark", Remark);

            string result = client.Execute(ProductApiName.ProductApiName_GetPage, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<ProductEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<ProductEntity>>(result);
                List<ProductEntity> listResult = dataResult.Result;
                if (!listResult.IsNullOrEmpty())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("产品编号"));
                    dt.Columns.Add(new DataColumn("产品名称"));
                    dt.Columns.Add(new DataColumn("厂商编码"));
                    dt.Columns.Add(new DataColumn("内部编码"));
                    dt.Columns.Add(new DataColumn("规格"));
                    dt.Columns.Add(new DataColumn("类别"));
                    dt.Columns.Add(new DataColumn("存储单位"));
                    dt.Columns.Add(new DataColumn("预警(下)"));
                    dt.Columns.Add(new DataColumn("预警(上)"));
                    dt.Columns.Add(new DataColumn("包装类型"));
                    dt.Columns.Add(new DataColumn("价格"));
                    dt.Columns.Add(new DataColumn("重量"));
                    dt.Columns.Add(new DataColumn("显示名"));
                    dt.Columns.Add(new DataColumn("默认供应商"));
                    dt.Columns.Add(new DataColumn("默认客户"));
                    dt.Columns.Add(new DataColumn("备注"));
                    foreach (ProductEntity t in listResult)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = t.BarCode;
                        row[1] = t.ProductName;
                        row[2] = t.FactoryNum;
                        row[3] = t.InCode;
                        row[4] = t.Size;
                        row[5] = t.CateName;
                        row[6] = t.UnitName;
                        row[7] = t.MinNum;
                        row[8] = t.MaxNum;
                        row[9] = EnumHelper.GetEnumDesc<EProductPackage>(t.IsSingle);
                        row[10] = t.AvgPrice;
                        row[11] = t.NetWeight;
                        row[12] = t.Display;
                        row[13] = t.SupName;
                        row[14] = t.CusName;
                        row[15] = t.Remark;
                        dt.Rows.Add(row);
                    }
                    string filePath = Server.MapPath("~/UploadFile/");
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string filename = string.Format("产品管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    NPOIExcel excel = new NPOIExcel("产品管理", "产品", System.IO.Path.Combine(filePath, filename));
                    excel.ToExcel(dt);
                    returnValue = ("/UploadFile/" + filename).Escape();
                }
            }
            DataResult returnResult = null;
            if (!returnValue.IsEmpty())
            {
                returnResult = new DataResult() { Code = 1000, Message = returnValue };
            }
            else
            {
                returnResult = new DataResult() { Code = 1001, Message = "没有任何数据导出" };
            }
            return Content(JsonHelper.SerializeObject(returnResult));
        }

    }
}
