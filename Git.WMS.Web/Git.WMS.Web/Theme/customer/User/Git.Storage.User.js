
var UserManager = {
    UserGrid:undefined,
    Server: function () {
        var config = (function () {
            var URL_GetList = "/UserAjax/GetList";
            var URL_Delete = "/UserAjax/Delete";
            var URL_Add = "/UserAjax/Add";
            var URL_ChangePass = "/UserAjax/ChangePass";
            var URL_AdminEditPass = "/UserAjax/AdminEditPass";
            var URL_ToExcel = "/UserAjax/ToExcel";

            return {
                URL_GetList: URL_GetList,
                URL_Delete: URL_Delete,
                URL_Add: URL_Add,
                URL_ChangePass: URL_ChangePass,
                URL_AdminEditPass:URL_AdminEditPass,
                URL_ToExcel: URL_ToExcel
            };
        })();

        //数据操作服务
        var dataServer = (function ($, config) {

            //查询分页列表
            var GetList=function(data,callback){
                $.gitAjax({
                    url: config.URL_GetList,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            var Delete=function(data,callback){
                $.gitAjax({
                    url: config.URL_Delete,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            var Add=function(data,callback){
                $.gitAjax({
                    url: config.URL_Add,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            var ChangePass=function(data,callback){
                $.gitAjax({
                    url: config.URL_ChangePass,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            var AdminEditPass=function(data,callback){
                $.gitAjax({
                    url: config.URL_AdminEditPass,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            var ToExcel=function(data,callback){
                $.gitAjax({
                    url: config.URL_ToExcel,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            }

            return {
                GetList: GetList,
                Delete: Delete,
                Add: Add,
                ChangePass: ChangePass,
                AdminEditPass:AdminEditPass,
                ToExcel: ToExcel
            }

        })($, config);
        return dataServer;
    },
    Dialog:function(UserNum,Command){
        var submit=function(v, h, f){
            if(v){
                var UserNum=h.find('input[name="UserNum"]').val();
                var UserName=h.find('input[name="UserName"]').val();
                var UserCode=h.find('input[name="UserCode"]').val();
                var PassWord=h.find('input[name="PassWord"]').val();
                var ConfirmPass=h.find('input[name="ConfirmPass"]').val();
                var RealName=h.find('input[name="RealName"]').val();
                var Email=h.find('input[name="Email"]').val();
                var Phone=h.find('input[name="Phone"]').val();
                var Mobile=h.find('input[name="Mobile"]').val();
                var DepartNum=h.find('select[name="DepartNum"]').val();
                var RoleNum=h.find('select[name="RoleNum"]').val();
                var Remark=h.find('input[name="Remark"]').val();
                
                if(git.IsEmpty(UserName)){
                    $.jBox.tip("请输入用户名","warn");
                    return false;
                }
                if(git.IsEmpty(UserCode)){
                    $.jBox.tip("请输入工号","warn");
                    return false;
                }

                if(Command=="Add"){
                    if(git.IsEmpty(PassWord)){
                        $.jBox.tip("请输入密码","warn");
                        return false;
                    }
                    if(PassWord!=ConfirmPass){
                        $.jBox.tip("确认密码必须和密码一致","warn");
                        return false;
                    }
                }

                if(git.IsEmpty(DepartNum)){
                    $.jBox.tip("请选择部门","warn");
                    return false;
                }
                if(git.IsEmpty(RoleNum)){
                    $.jBox.tip("请选择角色","warn");
                    return false;
                }
                var param={};
                param["UserNum"]=UserNum;
                param["UserName"]=UserName;
                param["UserCode"]=UserCode;
                param["PassWord"]=PassWord;
                param["ConfirmPass"]=ConfirmPass;
                param["RealName"]=RealName;
                param["Email"]=Email;
                param["Phone"]=Phone;
                param["Mobile"]=Mobile;
                param["DepartNum"]=DepartNum;
                param["RoleNum"]=RoleNum;
                param["Remark"]=Remark;
                
                var Server=UserManager.Server();
                Server.Add(param,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        var pageIndex=$("#mypager").pager("GetCurrent");
                        if(Command=="Add"){
                            UserManager.PageClick(1,pageSize);
                        }else if(Command=="Edit"){
                            UserManager.Refresh();
                        }
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });
            }
        }

        //窗体加载完成回调事件
        var loaded=function(h){
            h.find('input[name="UserName"]').attr("disabled",true);
            h.find('input[name="UserCode"]').attr("disabled",true);
            h.find('div[data-edit="pass"]').hide();
        }
        if(Command=="Add"){
            $.jBox.open("get:/Home/AddUser", "新增用户", 650, 400, { buttons: { "确定": true, "关闭": false } ,submit:submit});
        }else if(Command=="Edit"){
            $.jBox.open("get:/Home/AddUser?UserNum="+UserNum, "编辑用户", 650, 400, { buttons: { "确定": true, "关闭": false } ,submit:submit,loaded:loaded});
        }
    },
    AdminPass:function(UserNum){
        var submit=function(v, h, f){
            if(v){
                var UserNum=h.find('input[name="UserNum"]').val();
                var NewPass=h.find('input[name="NewPass"]').val();
                var ConfirmPass=h.find('input[name="ConfirmPass"]').val();
                
                if(git.IsEmpty(NewPass)){
                    $.jBox.tip("请输入密码","warn");
                    return false;
                }
                if(NewPass!=ConfirmPass){
                    $.jBox.tip("确认密码必须和密码一致","warn");
                    return false;
                }

                var param={};
                param["UserNum"]=UserNum;
                param["NewPass"]=NewPass;
                param["ConfirmPass"]=ConfirmPass;
                
                var Server=UserManager.Server();
                Server.AdminEditPass(param,function(result){
                    if(result.Code==1){
                        $.jBox.tip(result.Message,"success");
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });
            }
        }
        $.jBox.open("get:/Home/AdminPass?SnNum="+UserNum, "重置密码", 350, 200, { buttons: { "确定": true, "关闭": false } ,submit:submit});
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=UserManager.Server();
        var search=UserManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        console.log(JSON.stringify(search));
        Server.GetList(search,function(result){
            UserManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=UserManager.Server();
        var search=UserManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            UserManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable:function(result){
        var cols=[
            {title:'用户名', name:'UserName', width: 90, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'工号', name:'UserCode', width: 70, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'真名', name:'RealName', width: 70, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'邮箱', name:'Email', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'手机', name:'Mobile', width: 90, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'固定电话', name:'Phone', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'创建时间', name:'CreateTime', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.JsonToDateTime(data);
            }},
            {title:'登录次数', name:'LoginCount', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'部门', name:'DepartName', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'角色', name:'RoleName', width: 70, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'备注', name:'Remark', width: 90, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'操作', name:'ID', width: 180, align: 'left',lockWidth:false,  renderer: function(data,item,rowIndex){
                var html="";
                html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;&nbsp;';
                html+='<a class="print" href="javascript:void(0)">打印</a>&nbsp;&nbsp;';
                html+='<a class="pass" href="javascript:void(0)">重置密码</a>&nbsp;&nbsp;';
                html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                return html;
            }},
        ];

        if(this.UserGrid==undefined){
            this.UserGrid=$("#tabList").mmGrid({
                cols:cols,
                items:result.Result,
                checkCol:true,
                nowrap:true,
                height:380
            });
            //绑定事件
            UserManager.BindEvent();
        }else{
            this.UserGrid.load(result.Result);
        }
        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: UserManager.PageClick });
        }
    },
    BindEvent:function(){
        //打印事件的绑定
        this.UserGrid.off("loadSuccess").on('loadSuccess', function(e, data){
            $('#tabList').find('a.print').each(function(i,item){
                $(item).CusReportDialog({
                    ReportType:41,
                    callBack:function(result){
                        if(result!=undefined){
                            var SN=data[i].UserNum;
                            var SnNum=result.SnNum;
                            var url="/Report/Manager/Show?SnNum="+SnNum+"&OrderNum="+SN;
                            window.location.href=url;
                        }
                    }
                });
            });
        });

        //编辑删除事件都绑定
        this.UserGrid.off("cellSelected").on("cellSelected",function(e, item, rowIndex, colIndex){
            if($(e.target).is("a.edit")){
                var UserNum=item.UserNum;
                UserManager.Dialog(UserNum,"Edit");
            }else if($(e.target).is("a.delete")){
                var UserNum=item.UserNum;
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=[];
                        list.push(UserNum);
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=UserManager.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            UserManager.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除该账号吗？", "提示", submit);
            }else if($(e.target).is("a.pass")){
                var UserNum=item.UserNum;
                UserManager.AdminPass(UserNum);
            }
        });
    },
    GetSelect:function(){
        var list=[];
        if(this.UserGrid!=undefined){
            var rows=this.UserGrid.selectedRows();
            if(rows!=undefined && rows.length>0){
                for(var i=0;i<rows.length;i++){
                    list.push(rows[i].UserNum);
                }
            }
        }
        return list;
    },
    GetSearch:function(){
        var searchBar=$("div[data-condition='search']");
        var UserName=searchBar.find("input[name='UserName']").val();
        var UserCode=searchBar.find("input[name='UserCode']").val();
        var DepartNum=searchBar.find("select[name='DepartNum']").val();
        var RoleNum=searchBar.find("select[name='RoleNum']").val();

        var search={};
        search["UserName"]=UserName;
        search["UserCode"]=UserCode;
        search["DepartNum"]=DepartNum;
        search["RoleNum"]=RoleNum;
        return search;
    },
    ToolBar:function(){

        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                UserManager.Dialog(undefined,command);
            }else if(command=="Edit"){
                var list=UserManager.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var UserNum=list[0];
                UserManager.Dialog(UserNum,command);
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=UserManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=UserManager.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            UserManager.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=UserManager.Server();
                var search = UserManager.GetSearch();
                Server.ToExcel(search,function(result){
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                UserManager.Refresh();
            }
        });

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            UserManager.PageClick(1,10);
        });

        //监听回车事件,用于扫描
        $("input[name='UserCode']").keydown(function(event){
            if (event.keyCode == 13) {    
                var value=$(this).val();
                if(!git.IsEmpty(value)){
                    UserManager.PageClick(1,10);
                    setTimeout(function(){
                        $("input[name='UserCode']").val("");
                        $("input[name='UserCode']").focus();
                    },300);
                }
            }    
        });

        $("input[name='UserCode']").focus();
        //加载默认数据
        UserManager.PageClick(1,10);
    }
};