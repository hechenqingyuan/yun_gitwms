
/**
*
*财务类别
*
**/

var CateManager = {
    Server: function () {
    	var config = (function () {
            var URL_GetList = "/CateAjax/GetList";
            var URL_Delete = "/CateAjax/Delete";
            var URL_Add = "/CateAjax/Add";
            var URL_ToExcel = "/CateAjax/ToExcel";

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
    Dialog:function(SnNum,Command){
    	var submit=function(v, h, f){
    		if(v){
    			var SnNum=h.find('input[name="SnNum"]').val();
    			var CateNum=h.find('input[name="CateNum"]').val();
    			var CateName=h.find('input[name="CateName"]').val();

    			if(git.IsEmpty(CateName)){
    				$.jBox.tip("请输入财务分类名","warn");
    				return false;
    			}
    			var param={};
    			param["SnNum"]=SnNum;
    			param["CateNum"]=CateNum;
    			param["CateName"]=CateName;

    			var Server=CateManager.Server();
    			Server.Add(param,function(result){
                    var pageSize=$("#mypager").pager("GetPageSize");
                    var pageIndex=$("#mypager").pager("GetCurrent");
                    if(Command=="Add"){
                        CateManager.PageClick(1,pageSize);
                    }else if(Command=="Edit"){
                        CateManager.Refresh();
                    }
    				$.jBox.tip(result.Message,"success");
    			});
    		}
    	}

    	if(Command=="Add"){
    		$.jBox.open("get:/Finance/Cate/Add", "新增财务分类", 350, 200, { buttons: { "确定": true, "关闭": false } ,submit:submit});
    	}else if(Command=="Edit"){
    		$.jBox.open("get:/Finance/Cate/Add?SnNum="+SnNum, "编辑财务分类", 350, 200, { buttons: { "确定": true, "关闭": false } ,submit:submit});
    	}
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
    	var Server=CateManager.Server();
        var search=CateManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            CateManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=CateManager.Server();
        var search=CateManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            CateManager.SetTable(result);
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
                	return "<input type='checkbox' name='Cate_item' value='"+data+"'/>";
                }},
                { data: 'CateNum'},
                { data: 'CateName'},
                { data: 'CreateTime' ,render:function(data,type,full,meta){
                	return git.JsonToDateTime(data);
                }},
                {data:"ID",render:function(data,type,full,meta){
                    var html="";
                    html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;&nbsp;';
                    html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';

                    return html;
                }}
            ],
            aoColumnDefs:[
                { "sWidth": "15px",  "aTargets": [0] },
                { "sWidth": "70px",  "aTargets": [4] },
            ],
            oLanguage:{
                sEmptyTable:"没有查询到任何数据"
            }
        });

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: CateManager.PageClick });
        }

        //绑定编辑 删除事件
        CateManager.BindEvent();
    },
    BindEvent:function(){
        $("#tabList").find("a.edit").click(function(){
            var SN=$(this).parent().parent().find("input[name='Cate_item']").val();
            CateManager.Dialog(SN,"Edit")
        });

        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='Cate_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var list=[];
                    list.push(SN);
                    var param={};
                    param["list"]=JSON.stringify(list);
                    var Server=CateManager.Server();
                    Server.Delete(param,function(result){
                        $.jBox.tip(result.Message,"success");
                        var pageSize=$("#mypager").pager("GetPageSize");
                        CateManager.PageClick(1,pageSize);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });
    },
    SelectAll:function(item){
    	var flag=$(item).attr("checked");
    	if(flag){
    		$("#tabList").find("input[name='Cate_item']").attr("checked",true);
    	}else{
    		$("#tabList").find("input[name='Cate_item']").attr("checked",false);
    	}
    },
    GetSelect:function(){
    	var list=[];
    	$("#tabList").find("input[name='Cate_item']").each(function(i,item){
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
        var CateName=searchBar.find("input[name='CateName']").val();
        var search={};
        search["CateName"]=CateName;
        return search;
    },
    ToolBar:function(){

    	//工具栏按钮点击事件
    	$("div.toolbar").find("a.btn").click(function(){
    		var command=$(this).attr("data-command");

    		if(command=="Add"){
    			CateManager.Dialog(undefined,command);
    		}else if(command=="Edit"){
    			var list=CateManager.GetSelect();
    			if(list.length==0){
    				$.jBox.tip("请选择要编辑的项","warn");
    				return false;
    			}
    			var CateNum=list[0];
    			CateManager.Dialog(CateNum,command);
    		}else if(command=="Delete"){
    			var submit=function(v,h,f){
    				if(v=="ok"){
    					var list=CateManager.GetSelect();
		    			if(list.length==0){
		    				$.jBox.tip("请选择要删除的项","warn");
		    				return false;
		    			}
		    			var param={};
		    			param["list"]=JSON.stringify(list);
		    			var Server=CateManager.Server();
		    			Server.Delete(param,function(result){
		    				$.jBox.tip(result.Message,"success");
		    				var pageSize=$("#mypager").pager("GetPageSize");
                            CateManager.PageClick(1,pageSize);
		    			});
    				}
    			}
    			$.jBox.confirm("确定要删除吗？", "提示", submit);

    		}else if(command=="Excel"){
    			var Server=CateManager.Server();
    			var search=CateManager.GetSearch();
                Server.ToExcel(search,function(result){
    				
    				if(result.Code==1000){
    					var path = unescape(result.Message);
                    	window.location.href = path;
    				}else{
    					$.jBox.info(result.Message, "提示");
    				}
    			});
    		}else if(command=="Refresh"){
                CateManager.Refresh();
            }

    	});

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            CateManager.PageClick(1,10);
        });
        
    	//加载默认数据
    	CateManager.PageClick(1,10);
    }
}