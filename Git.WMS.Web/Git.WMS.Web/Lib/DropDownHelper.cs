/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-11 22:38:27
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-11 22:38:27       情缘
*********************************************************************************/

using Git.WMS.Sdk;
using Git.WMS.Sdk.ApiName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Storage.Common;
using Git.Storage.Entity.Sys;
using Newtonsoft.Json;
using System.Text;
using Git.Storage.Entity.Storage;
using Git.Storage.Entity.Finance;
using Git.Storage.Common.Enum;

namespace Git.WMS.Web.Lib
{
    public class DropDownHelper
    {
        /// <summary>
        /// 获取部门下拉框
        /// </summary>
        /// <param name="DepartNum"></param>
        /// <returns></returns>
        public static string GetDepart(string DepartNum,string CompanyID)
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("DepartNum", DepartNum);
            string result = client.Execute(DepartApiName.DepartApiName_GetList, dic);

            string returnResult = string.Empty;

            if (!result.IsEmpty())
            {
                DataListResult<SysDepartEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<SysDepartEntity>>(result);
                List<SysDepartEntity> listResult = dataResult.Result;
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value=''>请选择</option>");

                if (!listResult.IsNullOrEmpty())
                {
                    foreach (SysDepartEntity item in listResult.Where(a=>a.Depth==2))
                    {
                        sb.AppendFormat("<option value='{0}' {1}>{2}</option>", item.SnNum, item.SnNum == DepartNum ? "selected='selected'" : "", (item.DepartName=="Root") ? "":item.DepartName);
                        if (listResult.Exists(b => b.ParentNum == item.SnNum))
                        {
                            GetDepartChild(listResult,item.SnNum,DepartNum,ref sb);
                        }
                    }
                }
                returnResult = sb.ToString();
            }
            return returnResult;
        }

        /// <summary>
        /// 获取部门子类
        /// </summary>
        /// <param name="listSource"></param>
        /// <param name="ParentNum"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        private static void GetDepartChild(List<SysDepartEntity> listSource, string ParentNum, string DepartNum,ref StringBuilder sb)
        {
            foreach (SysDepartEntity item in listSource.Where(item => item.ParentNum == ParentNum))
            {
                string space = "";
                for (int i = 3; i <= item.Depth; i++)
                {
                    space += "&nbsp;&nbsp;";
                }
                sb.AppendFormat("<option value='{0}' {1}>"+ space + "{2}</option>", item.SnNum, item.SnNum == DepartNum ? "selected='selected'" : "", (item.DepartName == "Root") ? "" : item.DepartName);
                if (listSource.Exists(a => a.ParentNum == item.SnNum))
                {
                    GetDepartChild(listSource,item.SnNum,DepartNum,ref sb);
                }
            }
        }
        
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="RoleNum"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public static string GetRole(string RoleNum, string CompanyID)
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("RoleNum", RoleNum);
            string result = client.Execute(RoleApiName.RoleApiName_GetList, dic);

            string returnResult = string.Empty;

            if (!result.IsEmpty())
            {
                DataListResult<SysRoleEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<SysRoleEntity>>(result);
                List<SysRoleEntity> listResult = dataResult.Result;
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value=''>请选择</option>");

                if (!listResult.IsNullOrEmpty())
                {
                    foreach (SysRoleEntity item in listResult)
                    {
                        sb.AppendFormat("<option value='{0}' {1}>{2}</option>", item.SnNum, item.SnNum == RoleNum ? "selected='selected'" : "", item.RoleName);
                    }
                }
                returnResult = sb.ToString();
            }
            return returnResult;
        }

        /// <summary>
        /// 查询仓库下拉
        /// </summary>
        /// <param name="LocalNum"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public static string GetStorage(string StorageNum, string CompanyID)
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            dic.Add("StorageNum", StorageNum);
            string result = client.Execute(StorageApiName.StorageApiName_GetList, dic);

            string returnResult = string.Empty;

            if (!result.IsEmpty())
            {
                DataListResult<StorageEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<StorageEntity>>(result);
                List<StorageEntity> listResult = dataResult.Result;
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value=''>请选择</option>");

                if (!listResult.IsNullOrEmpty())
                {
                    foreach (StorageEntity item in listResult)
                    {
                        sb.AppendFormat("<option value='{0}' {1}>{2}</option>", item.SnNum, item.SnNum == StorageNum ? "selected='selected'" : "", item.StorageName);
                    }
                }
                returnResult = sb.ToString();
            }
            return returnResult;
        }

        /// <summary>
        /// 获得库位下拉选项
        /// </summary>
        /// <param name="StorageNum"></param>
        /// <param name="LocalNum"></param>
        /// <param name="CompanyID"></param>
        /// <param name="ListLocalType">显示的库位类型</param>
        /// <returns></returns>
        public static string GetLocation(string StorageNum, string LocalNum, string CompanyID, List<int> ListLocalType = null)
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            ListLocalType = ListLocalType.IsNull() ? new List<int>() : ListLocalType;

            int PageIndex = 1;
            int PageSize = Int32.MaxValue;

            dic.Add("CompanyID", CompanyID);
            dic.Add("PageIndex", PageIndex.ToString());
            dic.Add("PageSize", PageSize.ToString());
            dic.Add("StorageNum", StorageNum);
            dic.Add("ListLocalType", JsonHelper.SerializeObject(ListLocalType));
            string result = client.Execute(LocationApiName.LocationApiName_GetPage, dic);

            string returnValue = string.Empty;
            if (!result.IsEmpty())
            {
                DataListResult<LocationEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<LocationEntity>>(result);
                List<LocationEntity> listResult = dataResult.Result;

                StringBuilder sb = new StringBuilder();
                sb.Append("<option value=''>请选择</option>");

                if (!listResult.IsNullOrEmpty())
                {
                    if (!ListLocalType.IsNullOrEmpty())
                    {
                        foreach (LocationEntity item in listResult.Where(item => ListLocalType.Contains(item.LocalType)))
                        {
                            sb.AppendFormat("<option value='{0}' {1}>{2}</option>", item.LocalNum, item.LocalNum == LocalNum ? "selected='selected'" : "", item.StorageName + "." + item.LocalName);
                        }
                    }
                    else
                    {
                        foreach (LocationEntity item in listResult)
                        {
                            sb.AppendFormat("<option value='{0}' {1}>{2}</option>", item.LocalNum, item.LocalNum == LocalNum ? "selected='selected'" : "", item.StorageName + "." + item.LocalName);
                        }
                    }
                }
                returnValue = sb.ToString();
            }

            return returnValue;
        }

        /// <summary>
        /// 获得单位下拉列表
        /// </summary>
        /// <param name="UnitNum"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public static string GetUnit(string UnitNum, string CompanyID)
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            string result = client.Execute(MeasureApiName.MeasureApiName_GetList, dic);

            string returnResult = string.Empty;

            if (!result.IsEmpty())
            {
                DataListResult<MeasureEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<MeasureEntity>>(result);
                List<MeasureEntity> listResult = dataResult.Result;
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value=''>请选择</option>");

                if (!listResult.IsNullOrEmpty())
                {
                    foreach (MeasureEntity item in listResult)
                    {
                        sb.AppendFormat("<option value='{0}' {1}>{2}</option>", item.SN, item.SN == UnitNum ? "selected='selected'" : "", item.MeasureName);
                    }
                }
                returnResult = sb.ToString();
            }
            return returnResult;
        }

        /// <summary>
        /// 获得产品类别下拉
        /// </summary>
        /// <param name="CateNum"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public static string GetCate(string CateNum, string CompanyID)
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            string result = client.Execute(ProductCategoryApiName.ProductCategoryApiName_GetList, dic);

            string returnResult = string.Empty;

            if (!result.IsEmpty())
            {
                DataListResult<ProductCategoryEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<ProductCategoryEntity>>(result);
                List<ProductCategoryEntity> listResult = dataResult.Result;
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value=''>请选择</option>");

                if (!listResult.IsNullOrEmpty())
                {
                    foreach (ProductCategoryEntity item in listResult.Where(item=>item.Depth==2))
                    {
                        sb.AppendFormat("<option value='{0}' {1}>{2}</option>", item.SnNum, item.SnNum == CateNum ? "selected='selected'" : "", item.CateName);
                        if (listResult.Exists(b => b.ParentNum == item.SnNum))
                        {
                            GetCateChild(listResult, item.SnNum, CateNum, ref sb);
                        }
                    }
                }
                returnResult = sb.ToString();
            }
            return returnResult;
        }

        /// <summary>
        /// 获取产品类别子类别
        /// </summary>
        /// <param name="listSource"></param>
        /// <param name="ParentNum"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        private static void GetCateChild(List<ProductCategoryEntity> listSource, string ParentNum, string CateNum, ref StringBuilder sb)
        {
            foreach (ProductCategoryEntity item in listSource.Where(item => item.ParentNum == ParentNum))
            {
                string space = "";
                for (int i = 3; i <= item.Depth; i++)
                {
                    space += "&nbsp;&nbsp;";
                }
                sb.AppendFormat("<option value='{0}' {1}>" + space + "{2}</option>", item.SnNum, item.SnNum == CateNum ? "selected='selected'" : "", (item.CateName == "Root") ? "" : item.CateName);
                if (listSource.Exists(a => a.ParentNum == item.SnNum))
                {
                    GetCateChild(listSource, item.SnNum, CateNum, ref sb);
                }
            }
        }


        /// <summary>
        /// 获取产品类别子类别
        /// </summary>
        /// <param name="listSource"></param>
        /// <param name="ParentNum"></param>
        /// <param name="sb"></param>
        /// <returns></returns>
        private static void GetDepartChild(List<ProductCategoryEntity> listSource, string ParentNum, string[] CateNums, ref StringBuilder sb)
        {
            foreach (ProductCategoryEntity item in listSource.Where(item => item.ParentNum == ParentNum))
            {
                string space = "";
                for (int i = 3; i <= item.Depth; i++)
                {
                    space += "&nbsp;&nbsp;";
                }
                sb.AppendFormat("<option value='{0}' {1}>" + space + "{2}</option>", item.SnNum, CateNums.Contains(item.SnNum) ? "selected='selected'" : "", (item.CateName == "Root") ? "" : item.CateName);
                if (listSource.Exists(a => a.ParentNum == item.SnNum))
                {
                    GetDepartChild(listSource, item.SnNum, CateNums, ref sb);
                }
            }
        }

        /// <summary>
        /// 获得数据类型下拉列表
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static string GetDataType(string dbType)
        {
            List<string> list = new List<string>() 
            { 
                "varchar","nvarchar","text","datetime","int"
            };
            StringBuilder sb = new StringBuilder();
            string menuTemplate = "<option value='{0}' {1}>{2}</option>";
            sb.AppendFormat(menuTemplate, "", "", "请选择");
            if (!list.IsNullOrEmpty())
            {
                foreach (string item in list)
                {
                    sb.AppendFormat(menuTemplate, item, item == dbType ? "selected='selected'" : string.Empty, item);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取财务分类类别
        /// </summary>
        /// <param name="CateNum"></param>
        /// <returns></returns>
        public static string GetFinanceCate(string CateNum, string CompanyID)
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
            string result = client.Execute(FinanceCateApiName.FinanceCateApiName_GetList, dic);

            string returnResult = string.Empty;

            if (!result.IsEmpty())
            {
                DataListResult<FinanceCateEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<FinanceCateEntity>>(result);
                List<FinanceCateEntity> listResult = dataResult.Result;
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value=''>请选择</option>");

                if (!listResult.IsNullOrEmpty())
                {
                    foreach (FinanceCateEntity item in listResult)
                    {
                        sb.AppendFormat("<option value='{0}' {1}>{2}</option>", item.SnNum, item.SnNum == CateNum ? "selected='selected'" : "", item.CateName);
                    }
                }
                returnResult = sb.ToString();
            }
            return returnResult;
        }

        /// <summary>
        /// 物流承运商
        /// </summary>
        /// <param name="CarrierNum"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public static string GetCarrier(string CarrierNum, string CompanyID)
        {
            ITopClient client = new TopClientDefault();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CompanyID", CompanyID);
           
            string result = client.Execute(CarrierApiName.CarrierApiName_GetList, dic);

            string returnResult = string.Empty;

            if (!result.IsEmpty())
            {
                DataListResult<CarrierEntity> dataResult = JsonConvert.DeserializeObject<DataListResult<CarrierEntity>>(result);
                List<CarrierEntity> listResult = dataResult.Result;
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value=''>请选择</option>");

                if (!listResult.IsNullOrEmpty())
                {
                    foreach (var item in listResult)
                    {
                        sb.AppendFormat("<option value='{0}' {1}>{2}</option>", item.SnNum, item.SnNum == CarrierNum ? "selected='selected'" : "", item.CarrierName);
                    }
                }
                returnResult = sb.ToString();
            }
            return returnResult;
        }
    }
}