/*******************************************************************************
 * Copyright (C) Git Corporation. All rights reserved.
 *
 * Author: 情缘
 * Create Date: 2013-09-01 12:09:24
 *
 * Description: Git.Framework
 * http://www.cnblogs.com/qingyuan/
 * Revision History:
 * Date         Author               Description
 * 2013-09-01 12:09:24       情缘
*********************************************************************************/

using Git.Storage.Entity.Base;
using Git.Framework.DataTypes;
using Git.Framework.DataTypes.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Git.Framework.Resource;
using Git.Storage.Entity.Sys;
using Git.Storage.Entity.Storage;

namespace Git.WMS.Web.Lib
{
    public partial class MainPage : Git.Framework.Controller.Mvc.ControllerBase
    {
        /// <summary>
        /// 重新初始化方法
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            //设置用户信息
            SetUserInfo();
        }

        /// <summary>
        /// 登录用户
        /// </summary>
        public AdminEntity LoginUser
        {
            get
            {
                AdminEntity admin = Session[SessionKey.SESSION_LOGIN_ADMIN] as AdminEntity;
                return admin;
            }
            set
            {
                if (value != null)
                {
                    Session[SessionKey.SESSION_LOGIN_ADMIN] = value;
                }
            }
        }

        /// <summary>
        /// 当前登录用户所在公司
        /// </summary>
        public CompanyEntity Company
        {
            get
            {
                CompanyEntity company = Session[SessionKey.SESSION_LOGIN_COMPANY] as CompanyEntity;
                return company;
            }
            set
            {
                if (value != null)
                {
                    Session[SessionKey.SESSION_LOGIN_COMPANY] = value;
                }
            }
        }

        /// <summary>
        /// 当前登录用户唯一编码
        /// </summary>
        public string LoginUserNum 
        {
            get
            {
                AdminEntity admin = this.LoginUser;
                return admin != null ? admin.UserNum : string.Empty;
            }
        }

        /// <summary>
        /// 当前用户登录名
        /// </summary>
        public string LoginUserName
        {
            get
            {
                AdminEntity admin = this.LoginUser;
                return admin != null ? admin.UserName : string.Empty;
            }
        }

        /// <summary>
        /// 当前登录用户工号
        /// </summary>
        public string LoginUserCode
        {
            get
            {
                AdminEntity admin = this.LoginUser;
                return admin != null ? admin.UserCode : string.Empty;
            }
        }

        /// <summary>
        /// 当前登录用户公司唯一码
        /// </summary>
        public string CompanyID
        {
            get
            {
                AdminEntity admin = this.LoginUser;
                return admin != null ? admin.CompanyID : string.Empty;
            }
        }

        /// <summary>
        /// 默认仓库
        /// </summary>
        public StorageEntity DefaultStorage
        {
            get
            {
                StorageEntity entity = Session[SessionKey.SESSION_DEFAULT_STORAGE] as StorageEntity;
                return entity;
            }
            set
            {
                if (value != null)
                {
                    Session[SessionKey.SESSION_DEFAULT_STORAGE] = value;
                }
            }
        }

        /// <summary>
        /// 默认仓库编号
        /// </summary>
        public string DefaultStorageNum
        {
            get
            {
                StorageEntity entity = this.DefaultStorage;
                if (entity != null)
                {
                    return entity.SnNum;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 默认库位
        /// </summary>
        public LocationEntity DefaultLocation
        {
            get
            {
                LocationEntity entity = Session[SessionKey.SESSION_DEFAULT_LOCATION] as LocationEntity;
                return entity;
            }
            set
            {
                if (value != null)
                {
                    Session[SessionKey.SESSION_DEFAULT_LOCATION] = value;
                }
            }
        }

        /// <summary>
        /// 默认的库位编号
        /// </summary>
        public string DefaultLocalNum
        {
            get
            {
                LocationEntity entity = this.DefaultLocation;
                if (entity != null)
                {
                    return entity.LocalNum;
                }
                return string.Empty;
            }
        }


        /// <summary>
        /// 判断用户是否登录 登录返回true 未登录返回false
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            return this.LoginUser.IsNotNull();
        }

        /// <summary>
        /// 设置用户信息
        /// </summary>
        private void SetUserInfo()
        {
            if (IsLogin())
            {
                ViewBag.LoginUser = this.LoginUser;
                ViewBag.LoginUserNum = this.LoginUserNum;
                ViewBag.LoginUserName = this.LoginUserName;
                ViewBag.LoginUserCode = this.LoginUserCode;
                ViewBag.CompanyID = this.CompanyID;

                //设置仓库信息
                this.DefaultStorage = this.DefaultStorage.IsNull() ? new StorageEntity() : this.DefaultStorage;
                ViewBag.DefaultStorage = this.DefaultStorage;

                //仓库列表
                List<StorageEntity> listStorage = Session[SessionKey.SESSION_STORAGE_LIST] as List<StorageEntity>;
                listStorage = listStorage.IsNull() ? new List<StorageEntity>() : listStorage;
                ViewBag.StorageList = listStorage;
            }
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        protected void RemoveLogin()
        {
            Session.Remove(SessionKey.SESSION_LOGIN_ADMIN);
        }

    }
}