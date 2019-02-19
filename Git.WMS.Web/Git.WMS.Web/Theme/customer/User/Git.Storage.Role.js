
/**
*
*角色管理功能
*
**/

var RoleManager = {
    Server: function () {
    	var config = (function () {
            var URL_GetList = "/RoleAjax/GetList";
            var URL_Delete = "/RoleAjax/Delete";
            var URL_Add = "/RoleAjax/Add";
            var URL_ToExcel = "/RoleAjax/ToExcel";

            return {
                URL_GetList: URL_GetList,
                URL_Delete: URL_Delete,
                URL_Add: URL_Add,
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
                ToExcel: ToExcel
            }

        })($, config);
        return dataServer;
    },
    Dialog:function(RoleNum,Command){
    	var submit=function(v, h, f){
    		if(v){
    			var RoleNum=h.find('input[name="RoleNum"]').val();
    			var RoleName=h.find('input[name="RoleName"]').val();
    			var Remark=h.find('input[name="Remark"]').val();
                	var SnNum=h.find('input[name="SnNum"]').val();
    			if(git.IsEmpty(RoleName)){
    				$.jBox.tip("请输入角色名","warn");
    				return false;
    			}
    			var param={};
    			param["RoleNum"]=RoleNum;
    			param["RoleName"]=RoleName;
    			param["Remark"]=Remark;
                	param["SnNum"]=SnNum;

    			var Server=RoleManager.Server();
    			Server.Add(param,function(result){
                    var pageSize=$("#mypager").pager("GetPageSize");
                    var pageIndex=$("#mypager").pager("GetCurrent");
                    if(Command=="Add"){
                        RoleManager.PageClick(1,pageSize);
                    }else if(Command=="Edit"){
                        RoleManager.Refresh();
                    }
    				$.jBox.tip(result.Message,"success");
    			});
    		}
    	}

    	if(Command=="Add"){
    		$.jBox.open("get:/Home/AddRole", "新增角色", 350, 200, { buttons: { "确定": true, "关闭": false } ,submit:submit});
    	}else if(Command=="Edit"){
    		$.jBox.open("get:/Home/AddRole?RoleNum="+RoleNum, "编辑角色", 350, 200, { buttons: { "确定": true, "关闭": false } ,submit:submit});
    	}
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
    	var Server=RoleManager.Server();
        var search=RoleManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            RoleManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=RoleManager.Server();
        var search=RoleManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            RoleManager.SetTable(result);
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
                	return "<input type='checkbox' name='role_item' value='"+data+"'/>";
                }},
                { data: 'RoleName'},
                { data: 'CreateTime' ,render:function(data,type,full,meta){
                	return git.JsonToDateTime(data);
                }},
                { data: 'Remark'},
                {data:"ID",render:function(data,type,full,meta){
                    var html="";
                    html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;&nbsp;';
                    html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                    html+='<a href="/Home/Power?RoleNum='+full.SnNum+'">分配权限</a>&nbsp;';
                    return html;
                }}
            ],
            aoColumnDefs:[
                { "sWidth": "15px",  "aTargets": [0] },
                { "sWidth": "140px",  "aTargets": [4] },
            ],
            oLanguage:{
                sEmptyTable:"没有查询到任何数据"
            }
        });

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: RoleManager.PageClick });
        }

        //绑定编辑 删除事件
        RoleManager.BindEvent();
    },
    BindEvent:function(){
        $("#tabList").find("a.edit").click(function(){
            var SN=$(this).parent().parent().find("input[name='role_item']").val();
            console.log(SN);
            RoleManager.Dialog(SN,"Edit")
        });

        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='role_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var list=[];
                    list.push(SN);
                    var param={};
                    param["list"]=JSON.stringify(list);
                    var Server=RoleManager.Server();
                    Server.Delete(param,function(result){
                        $.jBox.tip(result.Message,"success");
                        var pageSize=$("#mypager").pager("GetPageSize");
                        RoleManager.PageClick(1,pageSize);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });
    },
    SelectAll:function(item){
    	var flag=$(item).attr("checked");
    	if(flag){
    		$("#tabList").find("input[name='role_item']").attr("checked",true);
    	}else{
    		$("#tabList").find("input[name='role_item']").attr("checked",false);
    	}
    },
    GetSelect:function(){
    	var list=[];
    	$("#tabList").find("input[name='role_item']").each(function(i,item){
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
        var RoleName=searchBar.find("input[name='RoleName']").val();
        var Remark=searchBar.find("input[name='Remark']").val();
        var search={};
        search["RoleName"]=RoleName;
        search["Remark"]=Remark;

        search["BaseInfo"]={
            Name:"hechen",
            Sex:34,
            Birthday:"1988-08-08"
        };

        search["List"]=[
            {Name:"AAA",Sex:"M",List:[3,4,5,6]},
            {Name:"BBB",Sex:"W"},
            {Name:"CCC",Sex:"M"},
        ];

        return search;
    },
    ToolBar:function(){

    	//工具栏按钮点击事件
    	$("div.toolbar").find("a.btn").click(function(){
    		var command=$(this).attr("data-command");

    		if(command=="Add"){
    			RoleManager.Dialog(undefined,command);
    		}else if(command=="Edit"){
    			var list=RoleManager.GetSelect();
    			if(list.length==0){
    				$.jBox.tip("请选择要编辑的项","warn");
    				return false;
    			}
    			var RoleNum=list[0];
    			RoleManager.Dialog(RoleNum,command);
    		}else if(command=="Delete"){
    			var submit=function(v,h,f){
    				if(v=="ok"){
    					var list=RoleManager.GetSelect();
		    			if(list.length==0){
		    				$.jBox.tip("请选择要删除的项","warn");
		    				return false;
		    			}
		    			var param={};
		    			param["list"]=JSON.stringify(list);
		    			var Server=RoleManager.Server();
		    			Server.Delete(param,function(result){
		    				$.jBox.tip(result.Message,"success");
		    				var pageSize=$("#mypager").pager("GetPageSize");
                            RoleManager.PageClick(1,pageSize);
		    			});
    				}
    			}
    			$.jBox.confirm("确定要删除吗？", "提示", submit);

    		}else if(command=="Excel"){
    			var Server=RoleManager.Server();
    			var search=RoleManager.GetSearch();
                Server.ToExcel(search,function(result){
    				
    				if(result.Code==1000){
    					var path = unescape(result.Message);
                    	window.location.href = path;
    				}else{
    					$.jBox.info(result.Message, "提示");
    				}
    			});
    		}else if(command=="Refresh"){
                RoleManager.Refresh();
            }

    	});

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            RoleManager.PageClick(1,10);
        });
        
    	//加载默认数据
    	RoleManager.PageClick(1,10);
    }
}