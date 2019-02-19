/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2013-09-01 15:10:06
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2013-09-01 15:10:06       情缘
*********************************************************************************/

using Git.Framework.DataTypes;
using Git.Storage.Entity.Base;
using Git.Framework.DataTypes.ExtensionMethods;
using Git.Framework.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Storage.Common;
using Git.Framework.Log;
using System.Transactions;
using Git.Storage.Entity.Sys;
using Git.Storage.Common.Enum;
using Git.Framework.Resource;
using System.Threading.Tasks;
using System.Data;

namespace Git.Storage.Provider.Base
{
    public partial class AdminProvider : DataFactory
    {
        public AdminProvider(string _CompanyID) 
        {
            this.CompanyID = _CompanyID;
        }

        /// <summary>
        /// 查询用户管理员分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public List<AdminEntity> GetList(AdminEntity entity, ref PageInfo pageInfo)
        {
            entity.IncludeAll();
            entity.Exclude(a => a.PassWord);
            entity.OrderBy(a => a.ID, EOrderBy.DESC);
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete);
            entity.And(a => a.CompanyID == entity.CompanyID);
            entity.And(item => item.UserCode != "DA_0000");

            if (!entity.UserName.IsEmpty())
            {
                entity.And("UserName", ECondition.Like, "%" + entity.UserName + "%");
            }
            if (!entity.UserCode.IsEmpty())
            {
                entity.And("UserCode", ECondition.Like, "%" + entity.UserCode + "%");
            }

            if (!entity.RoleNum.IsEmpty())
            {
                entity.And(a=>a.RoleNum==entity.RoleNum);
            }

            if (!entity.DepartNum.IsEmpty())
            {
                DepartProvider provider = new DepartProvider(this.CompanyID);
                List<SysDepartEntity> listDepart = provider.GetChildList(entity.DepartNum);
                string[] items = null;
                if (!listDepart.IsNullOrEmpty())
                {
                    items = listDepart.Select(item => item.SnNum).ToArray();
                    entity.And("DepartNum", ECondition.In, items);
                }
            }

            int rowCount = 0;
            List<AdminEntity> listResult = this.Admin.GetList(entity, pageInfo.PageSize, pageInfo.PageIndex, out rowCount);
            pageInfo.RowCount = rowCount;

            if (!listResult.IsNullOrEmpty())
            {
                SysRoleProvider RoleProvider = new SysRoleProvider(this.CompanyID);
                DepartProvider DepartProvider = new DepartProvider(this.CompanyID);
                foreach (AdminEntity item in listResult)
                {
                    if (item.RoleNum.IsNotEmpty())
                    {
                        SysRoleEntity RoleEntity = RoleProvider.GetRoleEntity(item.RoleNum);
                        item.RoleName = RoleEntity != null ? RoleEntity.RoleName : string.Empty;
                    }
                    if (item.DepartNum.IsNotEmpty())
                    {
                        SysDepartEntity DepartEntity = DepartProvider.GetSingle(item.DepartNum);
                        item.DepartName = DepartEntity != null ? DepartEntity.DepartName : string.Empty;
                    }
                }
            }
            return listResult;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddAdmin(AdminEntity entity)
        {
            entity.UserNum = entity.UserNum.IsEmpty() ? ConvertHelper.NewGuid() : entity.UserNum;
            entity.UserCode = entity.UserCode.IsEmpty() ? new TNumProvider(this.CompanyID).GetSwiftNum(typeof(AdminEntity), 5) : entity.UserCode;

            entity.IsDelete = (int)EIsDelete.NotDelete;
            entity.Status = 0;
            entity.LoginCount = 0;
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            entity.IncludeAll();
            int line = this.Admin.Add(entity);
            return line;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Delete(IEnumerable<string> list)
        {
            AdminEntity entity = new AdminEntity();
            entity.IsDelete = (int)EIsDelete.Deleted;
            entity.UpdateTime = DateTime.Now;
            entity.IncludeIsDelete(true)
                .IncludeUpdateTime(true);

            entity.Where(a => a.CompanyID == CompanyID)
                .And("UserNum",ECondition.In,list.ToArray());

            int line = this.Admin.Update(entity);
            return line;
        }

        /// <summary>
        /// 根据用户唯一编号查询用户信息
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public AdminEntity GetAdmin(string UserNum)
        {
            AdminEntity entity = new AdminEntity();
            entity.IncludeAll();
            entity.Exclude(a => a.PassWord);
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And<AdminEntity>(a=>a.CompanyID==CompanyID)
                .And(item=>item.UserNum==UserNum)
                ;
            entity = this.Admin.GetSingle(entity);
            if (entity != null)
            {
                SysRoleProvider RoleProvider = new SysRoleProvider(this.CompanyID);
                DepartProvider DepartProvider = new DepartProvider(this.CompanyID);

                if (entity.RoleNum.IsNotEmpty())
                {
                    SysRoleEntity RoleEntity = RoleProvider.GetRoleEntity(entity.RoleNum);
                    entity.RoleName = RoleEntity != null ? RoleEntity.RoleName : string.Empty;
                }
                if (entity.DepartNum.IsNotEmpty())
                {
                    SysDepartEntity DepartEntity = DepartProvider.GetSingle(entity.DepartNum);
                    entity.DepartName = DepartEntity != null ? DepartEntity.DepartName : string.Empty;
                }
            }
            return entity;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(AdminEntity entity)
        {
            entity.Include(a => new 
            {
                a.RealName,
                a.Email,
                a.Mobile,
                a.Phone,
                a.DepartNum,
                a.ParentNum,
                a.RoleNum,
                a.Picture,
                a.Remark
            });
            entity.Where(a => a.UserNum == entity.UserNum);
            int line = this.Admin.Update(entity);
            return line;
        }

        /// <summary>
        /// 管理员修改密码
        /// </summary>
        /// <param name="UserNum"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public int AdminEditPass(string UserNum,string Password)
        {
            AdminEntity entity = new AdminEntity();
            entity.PassWord = Password;
            entity.Where(item => item.CompanyID == this.CompanyID)
                .And(item=>item.UserNum==UserNum);
            entity.IncludePassWord(true);

            int line = this.Admin.Update(entity);
            return line;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserNum"></param>
        /// <param name="OldPass"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public DataResult UpdatePwd(string UserNum, string OldPass, string NewPass)
        {
            AdminEntity entity = new AdminEntity();
            entity.Where(a => a.UserNum == UserNum)
                .And(a => a.PassWord == OldPass)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int count = this.Admin.GetCount(entity);
            DataResult dataResult = new DataResult();
            if (count == 0)
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "旧密码不正确";
                return dataResult;
            }
            entity = new AdminEntity();
            entity.PassWord = NewPass;
            entity.IncludePassWord(true);
            entity.Where(a => a.PassWord == OldPass)
                .And(a => a.CompanyID == this.CompanyID)
                ;
            int line = this.Admin.Update(entity);
            if (line > 0)
            {
                dataResult.Code = (int)EResponseCode.Success;
                dataResult.Message = "密码修改成功";
            }
            else
            {
                dataResult.Code = (int)EResponseCode.Exception;
                dataResult.Message = "密码修改失败";
            }

            return dataResult;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public AdminEntity Login(string userName, string passWord)
        {
            AdminEntity entity = new AdminEntity();
            entity.IncludeAll();
            entity.Exclude(a => a.ParentName);
            entity.Where(a => a.UserName == userName)
                .And(a => a.PassWord == passWord)
                .And<AdminEntity>(a=>a.CompanyID==CompanyID)
                .And(a=>a.IsDelete==(int)EIsDelete.NotDelete)
                ;
            
            entity = this.Admin.GetSingle(entity);
            if (entity != null)
            {
                SysRoleProvider RoleProvider = new SysRoleProvider(this.CompanyID);
                DepartProvider DepartProvider = new DepartProvider(this.CompanyID);

                if (entity.RoleNum.IsNotEmpty())
                {
                    SysRoleEntity RoleEntity = RoleProvider.GetRoleEntity(entity.RoleNum);
                    entity.RoleName = RoleEntity != null ? RoleEntity.RoleName : string.Empty;
                }
                if (entity.DepartNum.IsNotEmpty())
                {
                    SysDepartEntity DepartEntity = DepartProvider.GetSingle(entity.DepartNum);
                    entity.DepartName = DepartEntity != null ? DepartEntity.DepartName : string.Empty;
                }
                Task.Factory.StartNew(() => 
                {
                    AdminEntity admin = new AdminEntity();
                    admin.LoginCount = entity.LoginCount + 1;
                    admin.IncludeLoginCount(true);
                    admin.Where(a => a.UserNum == entity.UserNum).And(item=>item.CompanyID==this.CompanyID);
                    this.Admin.Update(admin);
                });
            }
            return entity;
        }

        /// <summary>
        /// 使用工号扫描
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public AdminEntity Scan(string UserCode)
        {
            AdminEntity entity = new AdminEntity();
            entity.IncludeAll();
            entity.Exclude(a => a.PassWord);
            entity.Where(a => a.IsDelete == (int)EIsDelete.NotDelete)
                .And<AdminEntity>(a => a.CompanyID == CompanyID)
                .And(item => item.UserCode == UserCode)
                ;
            entity = this.Admin.GetSingle(entity);
            if (entity != null)
            {
                SysRoleProvider RoleProvider = new SysRoleProvider(this.CompanyID);
                DepartProvider DepartProvider = new DepartProvider(this.CompanyID);

                if (entity.RoleNum.IsNotEmpty())
                {
                    SysRoleEntity RoleEntity = RoleProvider.GetRoleEntity(entity.RoleNum);
                    entity.RoleName = RoleEntity != null ? RoleEntity.RoleName : string.Empty;
                }
                if (entity.DepartNum.IsNotEmpty())
                {
                    SysDepartEntity DepartEntity = DepartProvider.GetSingle(entity.DepartNum);
                    entity.DepartName = DepartEntity != null ? DepartEntity.DepartName : string.Empty;
                }
            }
            return entity;
        }

        /// <summary>
        /// 获得打印数据源
        /// </summary>
        /// <param name="argOrderNum"></param>
        /// <returns></returns>
        public DataSet GetPrint(string UserNum)
        {
            DataSet ds = new DataSet();
            AdminEntity entity = GetAdmin(UserNum);
            if (entity != null)
            {
                List<AdminEntity> list = new List<AdminEntity>();
                list.Add(entity);
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);
            }
            else
            {
                List<AdminEntity> list = new List<AdminEntity>();
                DataTable tableOrder = list.ToDataTable();
                ds.Tables.Add(tableOrder);
            }
            return ds;
        }
    }
}
