
var ReportCustomer={
	Server:function(){

		var config={
			URL_GetList:"/Report/ManagerAjax/GetList",
			URL_Delete:"/Report/ManagerAjax/Delete",
            URL_ToExcel:"/Report/ManagerAjax/ToExcel",

            URL_AddParam:"/Report/SingleAjax/AddParam",
            URL_DeleteParam:"/Report/SingleAjax/Delete",
            URL_LoadParam:"/Report/SingleAjax/LoadParam",
            URL_LoadProcParam:"/Report/SingleAjax/LoadProcParam",
            URL_Save:"/Report/SingleAjax/Save",
		}

		return {
			//查询报表分页
			GetList:function(data,callback){
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
			},
            ToExcel:function(data,callback){
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
            },
			Delete:function(data,callback){
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
			},
            AddParam:function(data,callback){
                $.gitAjax({
                    url: config.URL_AddParam,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            },
            DeleteParam:function(data,callback){
                $.gitAjax({
                    url: config.URL_DeleteParam,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            },
            LoadParam:function(data,callback){
                $.gitAjax({
                    url: config.URL_LoadParam,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            },
            LoadProcParam:function(data,callback){
                $.gitAjax({
                    url: config.URL_LoadProcParam,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            },
            Save:function(data,callback){
                $.gitAjax({
                    url: config.URL_Save,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if(callback!=undefined && typeof callback=="function"){
                            callback(result);
                        }
                    }
                });
            },
		}
	},
	PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=ReportCustomer.Server();
        var search=ReportCustomer.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            ReportCustomer.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=ReportCustomer.Server();
        var search=ReportCustomer.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            ReportCustomer.SetTable(result);
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
            bAutoWidth:false,
            bInfo:false,
            ordering:false,
            columns: [
                { data: 'SnNum' ,render:function(data, type, full, meta){
                    return "<input type='checkbox' name='in_report' value='"+data+"' data-full='"+JSON.stringify(full)+"'/>";
                }},
                { data: 'ReportNum',render:function(data,type,full,meta){
                    return git.GetStrSub(data,7);
                },createdCell:function(td, cellData, rowData, row, col){
                    if(!git.IsEmpty(cellData) && cellData.length>10){
                        $(td).popover({container:"body",content:cellData,trigger:"hover",placement:"bottom",delay:{show:500}});
                    } 
                }},
                { data: 'ReportName',render:function(data,type,full,meta){
                    return data;
                }},
                { data: 'ReportType',render:function(data,type,full,meta){
                	return git.GetEnumDesc(EReportType,data);
                }},
                { data: 'Status',render:function(data,type,full,meta){
                	return git.GetEnumDesc(EBool,data);
                }},
                { data: 'Remark'},
                { data: 'DsType',render:function(data,type,full,meta){
                	return git.GetEnumDesc(EDataSourceType,data);
                }},
                { data: 'DataSource',render:function(data,type,full,meta){
                    return git.GetStrSub(data,15);
                },createdCell:function(td, cellData, rowData, row, col){
                    if(!git.IsEmpty(cellData) && cellData.length>10){
                        $(td).popover({container:"body",content:cellData,trigger:"hover",placement:"bottom",delay:{show:500}});
                    } 
                }},
                { data: 'FileName'},
                {data:"ID",render:function(data,type,full,meta){
                    var html="";
                    
                    html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;';
                    html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                    html+='<a class="view" href="javascript:void(0)">查看</a>&nbsp;';
                    html+='<a class="design" href="javascript:void(0)">设计</a>&nbsp;';

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
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: ReportCustomer.PageClick });
        }

        //绑定编辑 删除事件
        ReportCustomer.BindEvent();
    },
    BindEvent:function(){

        $("#tabList").find("tbody td").popover({
            container:"body",
        });

        //编辑
        $("#tabList").find("a.edit").click(function(){
            var SN=$(this).parent().parent().find("input[name='in_report']").val();
            window.location.href="/Report/Manager/Add?SnNum="+SN;
        });

        //设计
        $("#tabList").find("a.design").click(function(){
            var SN=$(this).parent().parent().find("input[name='in_report']").val();
            window.location.href="/Report/Manager/Designer?SnNum="+SN;
        });

        //查看
        $("#tabList").find("a.view").click(function(){
            var SN=$(this).parent().parent().find("input[name='in_report']").val();
            window.location.href="/Report/Manager/Show?SnNum="+SN;
        });

        //删除
        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='in_report']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var list=[];
                    list.push(SN);
                    var param={};
                    param["list"]=JSON.stringify(list);
                    var Server=ReportCustomer.Server();
                    Server.Delete(param,function(result){
                        $.jBox.tip(result.Message,"success");
                        var pageSize=$("#mypager").pager("GetPageSize");
                        ReportCustomer.PageClick(1,pageSize);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });
    },
    SelectAll:function(item){
        var flag=$(item).attr("checked");
        if(flag){
            $("#tabList").find("input[name='in_report']").attr("checked",true);
        }else{
            $("#tabList").find("input[name='in_report']").attr("checked",false);
        }
    },
    GetSelect:function(){
        var list=[];
        $("#tabList").find("input[name='in_report']").each(function(i,item){
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

        var ReportNum=searchBar.find("input[name='ReportNum']").val();
        var ReportType=searchBar.find("select[name='ReportType']").val();
        var search={};
        search["ReportNum"]=ReportNum;
        search["ReportType"]=ReportType;

        return search;
    },
    ToolBar:function(){
        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                window.location.href="/Report/Manager/Add";
            }else if(command=="Edit"){
                var list=ReportCustomer.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var SN=list[0];
                window.location.href="/Report/Manager/Add?SnNum="+SN;
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=ReportCustomer.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=ReportCustomer.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            ReportCustomer.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=ReportCustomer.Server();
                var search=ReportCustomer.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                ReportCustomer.Refresh();
            }

        });

        //搜索 高级搜索按钮
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            ReportCustomer.PageClick(1,10);
        });
        searchBar.find("button[data-command='Advanced']").click(function(){
            var display=$("div.formsearch").css("display");
            if(display=="block"){
                $("div.formsearch").hide(300);
            }else{
                $("div.formsearch").show(300);
            }
        });
        searchBar.find("button[data-command='Clear']").click(function(){
            //清空搜索条件
            searchBar.find("input[name='OrderNum']").val("");
            searchBar.find("input[name='SupNum']").val("");
            searchBar.find("input[name='SupName']").val("");
            searchBar.find("input[name='ContractOrder']").val("");
            searchBar.find("input[name='BeginTime']").val("");
            searchBar.find("input[name='EndTime']").val("");
            searchBar.find("select[name='InType']").val("");
        });


        //状态按钮处理
        $("div[data-group='Status']").find("button").click(function(){
            $("div[data-group='Status']").find("button").removeClass("disabled");
            $(this).addClass("disabled");
            ReportCustomer.PageClick(1,10);
        });
        
        //加载默认数据
        ReportCustomer.PageClick(1,10);
    }
}


var ReportEdit={
	AddParam:function(SnNum){
        var submit=function(v, h, f){
            if(v){
                var SnNum=h.find("input[name='SnNum']").val();
                var ParamName=h.find("input[name='ParamName']").val();
                var ShowName=h.find("input[name='ShowName']").val();
                var ParamType=h.find("select[name='ParamType']").val();
                var ParamData=h.find("input[name='ParamData']").val();
                var DefaultValue=h.find("input[name='DefaultValue']").val();
                var ParamElement=h.find("select[name='ParamElement']").val();
                var Remark=h.find("input[name='Remark']").val();

                var param={};
                param["SnNum"]=SnNum;
                param["ParamName"]=ParamName;
                param["ShowName"]=ShowName;
                param["ParamType"]=ParamType;
                param["ParamData"]=ParamData;
                param["DefaultValue"]=DefaultValue;
                param["ParamElement"]=ParamElement;
                param["Remark"]=Remark;

                var Server=ReportCustomer.Server();
                Server.AddParam(param,function(result){
                    ReportEdit.PageClick(1,10);
                });
            }
        }

		if(git.IsEmpty(SnNum)){
			$.jBox.open("get:/Report/Manager/AddParam", "新增参数", 420, 450, { buttons: { "确定": true, "关闭": false } ,submit:submit});
		}else{
			$.jBox.open("get:/Report/Manager/AddParam?SnNum="+SnNum, "编辑参数", 420, 450, { buttons: { "确定": true, "关闭": false } ,submit:submit});
		}
	},
    Init:function(){

        //按钮的绑定事件
        $("div.formbtn").find("button.btn").click(function(event){
            var command=$(this).attr("data-command");
            if(command=="AddParam"){
                ReportEdit.AddParam();
            }else if(command=="Save"){
                var entity=ReportEdit.GetModel();
                var Server=ReportCustomer.Server();
                Server.Save(entity,function(result){
                    if(result.Code=="1"){
                        window.location.href="/Report/Manager/List";
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });

            }else if(command=="Cancel"){
                window.location.href="/Report/Manager/List";
            }
        });

        //回车加载存储过程参数
        $("input[name='DataSource']").keydown(function(e){
            if(e.keyCode==13){
                var DataSource=$("input[name='DataSource']").val();
                var DsType=$("select[name='DsType']").val();
                if(DsType==EDataSourceTypeJson.Procedure && !git.IsEmpty(DataSource)){
                    var param={};
                    param["ProcName"]=DataSource;
                    var Server=ReportCustomer.Server();
                    Server.LoadProcParam(param,function(result){
                        ReportEdit.PageClick(1,10);
                    });
                }
            }
        });

        //初始化上传
        ReportEdit.UploadCheck();

        //初始化参数
        var SnNum=$("input[name='SnNum']").val();
        if(!git.IsEmpty(SnNum)){
            ReportEdit.PageClick(1,10);
        }
    },
    PageClick:function(PageIndex,PageSize){
        var Server=ReportCustomer.Server();
        var search={};
        Server.LoadParam(search,function(result){
            ReportEdit.SetTable(result);
        });
    },
    SetTable:function(result){

        var DsType=$("select[name='DsType']").val();

        $("#tabList").DataTable({
            destroy: true,
            data:result.Result,
            paging:false,
            searching:false,
            scrollX: false,
            bAutoWidth:false,
            bInfo:false,
            ordering:false,
            columns: [
                { data: 'SnNum' ,render:function(data, type, full, meta){
                    return "<input type='checkbox' name='report_item' value='"+data+"' data-full='"+JSON.stringify(full)+"'/>";
                }},
                { data: 'ParamName'},
                { data: 'ShowName'},
                { data: 'ParamType'},
                { data: 'ParamData'},
                { data: 'DefaultValue'},
                { data: 'ParamElement',render:function(data,type,full,meta){
                    return git.GetEnumDesc(EElementType,data);
                }},
                { data: 'Remark'},
                {data:"ID",render:function(data,type,full,meta){
                    var html="";
                    if(DsType==EDataSourceTypeJson.SQL){
                        html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                    }
                    html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;';
                    return html;
                }}
            ],
            aoColumnDefs:[
                { "sWidth": "20px",  "aTargets": [0] }
            ],
            oLanguage:{
                sEmptyTable:"没有查询到任何数据"
            }
        });

        ReportEdit.BindTableEvent();
    },
    BindTableEvent:function(){

        //删除参数
        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='report_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var param={};
                    param["SnNum"]=SN;
                    var Server=ReportCustomer.Server();
                    Server.DeleteParam(param,function(result){
                        ReportEdit.PageClick(1,10);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });

        //编辑
        $("#tabList").find("a.edit").click(function(){
            var SN=$(this).parent().parent().find("input[name='report_item']").val();
            ReportEdit.AddParam(SN);
        });
    },
    UploadCheck: function () {
        var juploader = null;
        $.jUploader.setDefaults({
            cancelable: true, // 可取消上传
            allowedExtensions: ["frx"], // 
            messages: {
                upload: '导入分类',
                cancel: '取消',
                emptyFile: "{file} 为空，请选择一个文件.",
                invalidExtension: "只允许上传 {extensions} 文件.",
                onLeave: "文件正在上传，如果你现在离开，上传将会被取消。"
            }
        });
        if (juploader == null) {
            juploader = $.jUploader({
                button: 'btnUpload',
                action: '/Common/Upload',
                onUpload: function (fileName) {
                    jBox.tip('正在上传 ' + fileName + ' ...', 'loading');
                },
                onComplete: function (fileName, response) {
                    console.log(fileName);
                    console.log(response);
                    $.jBox.closeTip();
                    $("input[name='FileName']").val(response.Result);
                },
                showMessage: function (message) {
                    jBox.tip(message, 'error');
                },
                onCancel: function (fileName) {
                    jBox.tip(fileName + ' 上传取消。', 'info');
                }
            });
        }
    },
    GetModel:function(){

        var SnNum=$("input[name='SnNum']").val();
        var ReportNum=$("input[name='ReportNum']").val();
        var ReportName=$("input[name='ReportName']").val();
        var ReportType=$("select[name='ReportType']").val();
        var Remark=$("input[name='Remark']").val();
        var DsType=$("select[name='DsType']").val();
        var DataSource=$("input[name='DataSource']").val();
        var FileName=$("input[name='FileName']").val();

        var param={};
        param["SnNum"]=SnNum;
        param["ReportNum"]=ReportNum;
        param["ReportName"]=ReportName;
        param["ReportType"]=ReportType;
        param["Remark"]=Remark;
        param["DsType"]=DsType;
        param["DataSource"]=DataSource;
        param["FileName"]=FileName;

        return param;
    }
}


var ReportShow={
    Init:function(){

        //重新修改表格的宽度
        $(".frtoolbar").parent().css("width","100%");

        //初始化控件的事件
        $("div[data-condition='search']").find("a[data-command='search']").click(function(){
            ReportShow.Search();
        });
    },
    Search:function(){
        var list = [];
        $("div[data-condition='search']").find("input.arg").each(function(index, item) {
            var ReportParams = {};
            ReportParams["ParamName"] = $(item).attr("name");
            ReportParams["DefaultValue"] = $(item).val();
            list.push(ReportParams);
        });

        var ReportNum = $("#txtReportNum").val();
        var json = JSON.stringify(list);
        json = escape(json);
        window.location.href = "/Report/Manager/Show?SnNum=" + ReportNum + "&SearchValues=" + json;
    }
}