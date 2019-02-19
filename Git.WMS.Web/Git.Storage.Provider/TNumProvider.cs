/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2016-03-05 8:18:20
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2016-03-05 8:18:20       情缘
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Framework.Log;
using Git.Framework.ORM;
using Git.Storage.Entity.Base;
using Git.Storage.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Storage.Provider
{                                            
    public partial class TNumProvider : DataFactory
    {
        private Log log = Log.Instance(typeof(TNumProvider));
        private static Random random = new Random(DateTime.Now.Millisecond);

        public TNumProvider(string CompanyID) 
        {
            this.CompanyID = CompanyID;
        }

        /// <summary>
        /// 生成时间格式唯一编号
        /// </summary>
        /// <returns></returns>
        public static string UniqueNum()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff") + random.Next(1000, 9999);
        }

        /// <summary>
        /// 获得GUID
        /// </summary>
        /// <returns></returns>
        public static string CreateGUID()
        {
            return ConvertHelper.NewGuid();
        }

        /// <summary>
        /// 获得不间断流水
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual string GetSwiftNum(Type type)
        {
            TableInfo tableInfo = EntityTypeCache.Get(type);
            Proc_SwiftNumEntity entity = new Proc_SwiftNumEntity();
            entity.Day = string.Empty;
            entity.TabName = tableInfo.Table.Name;
            entity.CompanyID = this.CompanyID;
            this.Proc_SwiftNum.ExecuteNonQuery(entity);
            if (entity != null)
            {
                return entity.Num.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获得日期的流水号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual string GetSwiftNumByDay(Type type)
        {
            TableInfo tableInfo = EntityTypeCache.Get(type);
            Proc_SwiftNumEntity entity = new Proc_SwiftNumEntity();
            entity.Day = DateTime.Now.ToString("yyyy-MM-dd");
            entity.TabName = tableInfo.Table.Name;
            entity.CompanyID = this.CompanyID;
            this.Proc_SwiftNum.ExecuteNonQuery(entity);
            if (entity != null)
            {
                return entity.Num.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获得流水号
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public virtual string GetSwiftNum(Type type, int length)
        {
            string num = GetSwiftNum(type);
            string returnValue = string.Empty;
            for (int i = 0; i < length - num.Length; i++)
            {
                returnValue += "0";
            }
            returnValue += num;
            return returnValue;
        }

        /// <summary>
        /// 根据字符串定义查询流水号
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual string GetSwiftNum(string name)
        {
            Proc_SwiftNumEntity entity = new Proc_SwiftNumEntity();
            entity.Day = string.Empty;
            entity.TabName = name;
            entity.CompanyID = this.CompanyID;
            this.Proc_SwiftNum.ExecuteNonQuery(entity);
            if (entity != null)
            {
                return entity.Num.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获得日期的流水号
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public virtual string GetSwiftNumByDay(Type type, int length)
        {
            string num = GetSwiftNumByDay(type);
            string returnValue = string.Empty;
            for (int i = 0; i < length - num.Length; i++)
            {
                returnValue += "0";
            }
            returnValue += num;
            return returnValue;
        }

        /// <summary>
        /// 根据字符串定义查询流水号
        /// </summary>
        /// <param name="name"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public virtual string GetSwiftNum(string name, int length)
        {
            string num = GetSwiftNum(name);
            string returnValue = string.Empty;
            for (int i = 0; i < length - num.Length; i++)
            {
                returnValue += "0";
            }
            returnValue += num;
            return returnValue;
        }

        /// <summary>
        /// 根据字符串定义查询流水号
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual string GetSwiftNumByDay(string name)
        {
            Proc_SwiftNumEntity entity = new Proc_SwiftNumEntity();
            entity.Day = DateTime.Now.ToString("yyyy-MM-dd");
            entity.TabName = name;
            entity.CompanyID = this.CompanyID;
            this.Proc_SwiftNum.ExecuteNonQuery(entity);
            if (entity != null)
            {
                return entity.Num.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 根据字符串定义查询流水号
        /// </summary>
        /// <param name="name"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public virtual string GetSwiftNumByDay(string name, int length)
        {
            string num = GetSwiftNumByDay(name);
            string returnValue = string.Empty;
            for (int i = 0; i < length - num.Length; i++)
            {
                returnValue += "0";
            }
            returnValue += num;
            return returnValue;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<TNumEntity> GetList(TNumEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            int rowCount = 0;
            List<TNumEntity> listResult = this.TNum.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;
            return listResult;
        }
    }
}
