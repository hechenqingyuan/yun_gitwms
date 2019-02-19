/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2013-10-28 22:53:45
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2013-10-28 22:53:45       情缘
*********************************************************************************/

using Git.Framework.Cache;
using Git.Framework.Log;
using Git.Framework.ORM;
using Git.Storage.Entity.Base;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Storage.Common;
using Git.Storage.Entity.Sys;
using Git.Storage.Common.Enum;
using Git.Framework.Resource;

namespace Git.Storage.Provider.Base
{
    public partial class SysRoleProvider : DataFactory
    {
        public SysRoleProvider(string _CompanyID)
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="entity"></param>
        public int AddRole(SysRoleEntity entity)
        {
            string key = string.Format(CacheKey.JOOSHOW_SYSROLE_CACHE,this.CompanyID);
            entity.SnNum = entity.SnNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.SnNum;
            entity.RoleNum = entity.RoleNum.IsEmpty()? new TNumProvider(this.CompanyID).GetSwiftNum(typeof(SysRoleEntity),5):entity.RoleNum;
            entity.IncludeAll();
            int line = this.SysRole.Add(entity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }


        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateRole(SysRoleEntity entity)
        {
            string key = string.Format(CacheKey.JOOSHOW_SYSROLE_CACHE, this.CompanyID);
            entity.IncludeRoleName(true).IncludeRemark(true);
            entity.Where(a => a.SnNum == entity.SnNum)
                .And(a=>a.CompanyID==entity.CompanyID);
            int line = this.SysRole.Update(entity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }

        /// <summary>
        /// 根据角色编号获得角色信息
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public SysRoleEntity GetRoleEntity(string SnNum)
        {
            List<SysRoleEntity> listSource = GetList();
            if (!listSource.IsNullOrEmpty())
            {
                return listSource.SingleOrDefault(item => item.SnNum == SnNum);
            }
            return null;
        }
        
        /// <summary>
        /// 获得所有角色信息
        /// </summary>
        /// <returns></returns>
        public List<SysRoleEntity> GetList()
        {
            string key = string.Format(CacheKey.JOOSHOW_SYSROLE_CACHE, this.CompanyID);
            string AdminRoleNum = ResourceManager.GetSettingEntity("Super_AdminRole").Value;
            List<SysRoleEntity> list = CacheHelper.Get(key) as List<SysRoleEntity>;
            if (!list.IsNullOrEmpty())
            {
                return list.Where(item => item.RoleNum != AdminRoleNum).ToList();
            }
            SysRoleEntity sysRole = new SysRoleEntity();
            sysRole.IncludeAll();
            sysRole.Where<SysRoleEntity>(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And<SysRoleEntity>(a => a.CompanyID == this.CompanyID);
            sysRole.OrderBy(a => a.ID, EOrderBy.DESC);
            list = this.SysRole.GetList(sysRole);
            if (!list.IsNullOrEmpty())
            {
                CacheHelper.Insert(key, list);
            }
            return list.Where(item => item.RoleNum != AdminRoleNum).ToList();
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<SysRoleEntity> GetList(SysRoleEntity entity, ref PageInfo pageInfo)
        {
            List<SysRoleEntity> listSource = GetList();
            if (listSource.IsNullOrEmpty())
            {
                return listSource;
            }
            int rowCount = 0;
            if (!entity.RoleName.IsEmpty())
            {
                listSource = listSource.Where(a => a.RoleName.Contains(entity.RoleName)).ToList();
            }
            if (!entity.Remark.IsEmpty())
            {
                listSource = listSource.Where(a => a.Remark.Contains(entity.Remark)).ToList();
            }
            rowCount = listSource.Count;
            pageInfo.RowCount = rowCount;
            string AdminRoleNum = ResourceManager.GetSettingEntity("Super_AdminRole").Value;
            return listSource.Where(item => item.RoleNum != AdminRoleNum).Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="SnNum"></param>
        /// <returns></returns>
        public int DeleteRole(string SnNum)
        {
            string key = string.Format(CacheKey.JOOSHOW_SYSROLE_CACHE, this.CompanyID);
            SysRoleEntity roleEntity = new SysRoleEntity();
            roleEntity.IsDelete = (int)EIsDelete.Deleted;
            roleEntity.IncludeIsDelete(true);
            roleEntity.Where(a => a.SnNum == SnNum);
            int line = this.SysRole.Update(roleEntity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public int DeleteRole(IEnumerable<string> list)
        {
            string key = string.Format(CacheKey.JOOSHOW_SYSROLE_CACHE, this.CompanyID);
            SysRoleEntity roleEntity = new SysRoleEntity();
            roleEntity.IsDelete = (int)EIsDelete.Deleted;
            roleEntity.IncludeIsDelete(true);
            roleEntity.Where("SnNum", ECondition.In, list.ToArray());
            roleEntity.And(a => a.CompanyID == CompanyID);
            int line = this.SysRole.Update(roleEntity);
            if (line > 0)
            {
                CacheHelper.Remove(key);
            }
            return line;
        }
    }
}
