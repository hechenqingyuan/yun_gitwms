
/**
*
*仓库管理功能
*
**/

var StorageManager = {
    Server: function () {
        var config = (function () {
            var URL_GetList = "/Storage/StorageAjax/GetList";
            var URL_Delete = "/Storage/StorageAjax/Delete";
            var URL_Add = "/Storage/StorageAjax/Add";
            var URL_ToExcel = "/Storage/StorageAjax/ToExcel";
            var URL_SetDefault = "/Storage/StorageAjax/SetDefault";
            var URL_SetForbid = "/Storage/StorageAjax/SetForbid";

            return {
                URL_GetList: URL_GetList,
                URL_Delete: URL_Delete,
                URL_Add: URL_Add,
                URL_ToExcel: URL_ToExcel,
                URL_SetDefault: URL_SetDefault,
                URL_SetForbid: URL_SetForbid,
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

            var SetDefault=function(data,callback){
                $.gitAjax({
                    url: config.URL_SetDefault,
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

            var SetForbid=function(data,callback){
                $.gitAjax({
                    url: config.URL_SetForbid,
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
                ToExcel: ToExcel,
                SetDefault: SetDefault,
                SetForbid: SetForbid,
            }

        })($, config);
        return dataServer;
    },
    Dialog:function(SN,Command){
        var submit=function(v, h, f){
            if(v){
                var SnNum=h.find('input[name="SnNum"]').val();
                var StorageNum=h.find('input[name="StorageNum"]').val();
                var StorageName=h.find('input[name="StorageName"]').val();
                var StorageType=h.find('select[name="StorageType"]').val();
                var DepartNum = h.find('select[name="DepartNum"]').val();
                var ShortName = h.find('input[name="ShortName"]').val();
                var Action=h.find('input[name="Action"]').val();
                var LeaseTime=h.find('input[name="LeaseTime"]').val();
                var Area=h.find('input[name="Area"]').val();
                var Address=h.find('input[name="Address"]').val();
                var Contact=h.find('input[name="Contact"]').val();
                var Phone=h.find('input[name="Phone"]').val();
                var Remark=h.find('input[name="Remark"]').val();

                if(git.IsEmpty(StorageName)){
                    $.jBox.tip("请输入仓库名","warn");
                    return false;
                }
                if (git.IsEmpty(DepartNum)) {
                    $.jBox.tip("请选择仓库所属部门", "warn");
                    return false;
                }

                if(git.IsEmpty(LeaseTime)){
                    $.jBox.tip("请选择租赁时间", "warn");
                    return false;
                }
                
                var param={};
                param["SnNum"]=SnNum;
                param["StorageNum"]=StorageNum;
                param["StorageName"]=StorageName;
                param["StorageType"]=StorageType;
                param["Action"]=Action;
                param["Remark"]=Remark;
                param["DepartNum"] = DepartNum;
                param["ShortName"] = ShortName;
                param["LeaseTime"] = LeaseTime;
                param["Area"] = Area;
                param["Address"] = Address;
                param["Contact"] = Contact;
                param["Phone"] = Phone;

                var Server=StorageManager.Server();
                Server.Add(param,function(result){
                    var pageSize=$("#mypager").pager("GetPageSize");
                    var pageIndex=$("#mypager").pager("GetCurrent");
                    if(Command=="Add"){
                        StorageManager.PageClick(1,pageSize);
                    }else if(Command=="Edit"){
                        StorageManager.Refresh();
                    }
                    $.jBox.tip(result.Message,"success");
                });
            }
        }

        if(Command=="Add"){
            $.jBox.open("get:/Storage/Storage/Add", "新增仓库", 650, 350, { buttons: { "确定": true, "关闭": false }, submit: submit });
        }else if(Command=="Edit"){
            $.jBox.open("get:/Storage/Storage/Add?SnNum=" + SN, "编辑仓库", 650, 350, { buttons: { "确定": true, "关闭": false }, submit: submit });
        }
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=StorageManager.Server();
        var search=StorageManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            StorageManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=StorageManager.Server();
        var search=StorageManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            StorageManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable:function(result){
        $("#tabList").DataTable({
            destroy: true,
            data:result.Result,
            paging:false,
            searching:false,
            scrollX: false,
            bAutoWidth:true,
            bInfo:false,
            ordering:false,
            columns: [
                { data: 'SnNum' ,render:function(data, type, full, meta){
                    return "<input type='checkbox' name='storage_item' value='"+data+"'/>";
                }},
                { data: 'StorageNum' },
                { data: 'StorageName',render:function(data,type,full,meta) {
                    var html="";
                    html+='<a href="/Storage/Location/List?SnNum='+full.SnNum+'">'+full.StorageName+'</a>';
                    return html;
                }},
                { data: 'LeaseTime',render:function(data,type,full,meta){
                    return git.JsonToDateTime(data); 
                }},
                { data: 'StorageType',render:function(data,type,full,meta){
                    return git.GetEnumDesc(EStorageType,data);
                }},
                { data: 'DepartName' },
                { data: 'IsForbid',render:function(data,type,full,meta){
                    var html='';
                    if(data==EBoolJson.Yes){
                        html+='<input type="checkbox" name="IsForbid_item" checked="checked">';
                    }else{
                        html+='<input type="checkbox" name="IsForbid_item">';
                    }
                    
                    return html;
                }},
                { data: 'IsDefault',render:function(data,type,full,meta){
                    var html='';
                    if(data==EBoolJson.Yes){
                        html+='<input type="checkbox" name="IsDefault_item" checked="checked">';
                    }else{
                        html+='<input type="checkbox" name="IsDefault_item">';
                    }
                    
                    return html;
                }},
                { data: 'Address'},
                { data: 'Area'},
                { data: 'Contact'},
                { data: 'Phone'},
                {data:"ID",render:function(data,type,full,meta){
                    var html="";
                    html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;&nbsp;';
                    html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                    return html;
                }}
            ],
            aoColumnDefs:[
                { "sWidth": "15px",  "aTargets": [0] }
            ],
            oLanguage:{
                sEmptyTable:"没有查询到任何数据"
            }
        });

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: StorageManager.PageClick });
        }

        //绑定编辑 删除事件
        StorageManager.BindEvent();
    },
    BindEvent:function(){
        $("#tabList").find("a.edit").click(function(){
            var SN=$(this).parent().parent().find("input[name='storage_item']").val();
            StorageManager.Dialog(SN,"Edit")
        });

        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='storage_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var list=[];
                    list.push(SN);
                    var param={};
                    param["list"]=JSON.stringify(list);
                    var Server=StorageManager.Server();
                    Server.Delete(param,function(result){
                        $.jBox.tip(result.Message,"success");
                        var pageSize=$("#mypager").pager("GetPageSize");
                        StorageManager.PageClick(1,pageSize);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });

        $("#tabList").find("input[name='IsForbid_item']").click(function(){
            var SnNum=$(this).parent().parent().find("input[name='storage_item']").val();
            var IsForbid=EBoolJson.No;
            if($(this).attr("checked")){
                IsForbid=EBoolJson.Yes;
            }
            var param={};
            param["SnNum"]=SnNum;
            param["IsForbid"]=IsForbid;
            var Server=StorageManager.Server();
            Server.SetForbid(param,function(result){
                StorageManager.Refresh();
            });
        });

        $("#tabList").find("input[name='IsDefault_item']").click(function(){
            var SnNum=$(this).parent().parent().find("input[name='storage_item']").val();
            var IsDefault=EBoolJson.No;
            if($(this).attr("checked")){
                IsDefault=EBoolJson.Yes;
            }
            var param={};
            param["SnNum"]=SnNum;
            param["IsDefault"]=IsDefault;
            var Server=StorageManager.Server();
            Server.SetDefault(param,function(result){
                StorageManager.Refresh();
            });
        });
    },
    SelectAll:function(item){
        var flag=$(item).attr("checked");
        if(flag){
            $("#tabList").find("input[name='storage_item']").attr("checked",true);
        }else{
            $("#tabList").find("input[name='storage_item']").attr("checked",false);
        }
    },
    GetSelect:function(){
        var list=[];
        $("#tabList").find("input[name='storage_item']").each(function(i,item){
            var flag=$(item).attr("checked");
            if(flag){
                var value=$(item).val();
                list.push(value);
            }
        });
        return list;
    },
    GetSearch:function(){
        var searchBar=$("div[data-condition='search']");
        var StorageNum=searchBar.find("input[name='StorageNum']").val();
        var StorageName=searchBar.find("input[name='StorageName']").val();
        var DepartNum=searchBar.find("select[name='DepartNum']").val();
        var StorageType=searchBar.find("select[name='StorageType']").val();
        var search={};
        search["StorageNum"]=StorageNum;
        search["StorageName"]=StorageName;
        search["DepartNum"]=DepartNum;
        search["StorageType"]=StorageType;
        
        return search;
    },
    ToolBar:function(){

        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                StorageManager.Dialog(undefined,command);
            }else if(command=="Edit"){
                var list=StorageManager.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var SN=list[0];
                StorageManager.Dialog(SN,command);
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=StorageManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=StorageManager.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            StorageManager.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=StorageManager.Server();
                var search=StorageManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                StorageManager.Refresh();
            }

        });

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            StorageManager.PageClick(1,10);
        });
        
        //加载默认数据
        StorageManager.PageClick(1,10);
    }
}