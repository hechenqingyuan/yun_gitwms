
/**
*
*入库管理功能
*
**/

var CheckManager = {
    Server: function () {
        var config = (function () {
            var URL_Add = "/Order/CheckAjax/Add";
            var URL_LoadDetail = "/Order/CheckAjax/LoadDetail";

            var URL_AddProduct = "/Order/CheckAjax/AddProduct";
            var URL_DelProduct = "/Order/CheckAjax/DelProduct";
            var URL_SaveDif = "/Order/CheckAjax/SaveDif";
            var URL_AddDif = "/Order/CheckAjax/AddDif";
            var URL_Complete = "/Order/CheckAjax/Complete";

            var URL_GetList = "/Order/CheckManagerAjax/GetList";
            var URL_GetDetail = "/Order/CheckManagerAjax/GetDetail";
            var URL_GetDifPage = "/Order/CheckManagerAjax/GetDifPage";
            var URL_Delete = "/Order/CheckManagerAjax/Delete";
            var URL_Cancel = "/Order/CheckManagerAjax/Cancel";
            var URL_Audite = "/Order/CheckManagerAjax/Audite";
            var URL_ToExcel = "/Order/CheckManagerAjax/ToExcel";

            return {
                URL_GetList: URL_GetList,
                URL_GetDetail: URL_GetDetail,
                URL_LoadDetail: URL_LoadDetail,
                URL_GetDifPage: URL_GetDifPage,
                URL_AddProduct: URL_AddProduct,
                URL_DelProduct: URL_DelProduct,
                URL_SaveDif: URL_SaveDif,
                URL_AddDif: URL_AddDif,
                URL_Complete: URL_Complete,

                URL_Add: URL_Add,
                URL_Delete: URL_Delete,
                URL_Cancel: URL_Cancel,
                URL_Audite: URL_Audite,
                URL_ToExcel: URL_ToExcel,
            };
        })();

        //数据操作服务
        var dataServer = (function ($, config) {

            //查询分页列表
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

            var LoadDetail=function(data,callback){
                $.gitAjax({
                    url: config.URL_LoadDetail,
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

            var AddProduct=function(data,callback){
                $.gitAjax({
                    url: config.URL_AddProduct,
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

            var DelProduct=function(data,callback){
                $.gitAjax({
                    url: config.URL_DelProduct,
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

            var GetDetail=function(data,callback){
                $.gitAjax({
                    url: config.URL_GetDetail,
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

            var GetDifPage=function(data,callback){
                $.gitAjax({
                    url: config.URL_GetDifPage,
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

            var SaveDif=function(data,callback){
                $.gitAjax({
                    url: config.URL_SaveDif,
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

            var AddDif=function(data,callback){
                $.gitAjax({
                    url: config.URL_AddDif,
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

            var Complete=function(data,callback){
                $.gitAjax({
                    url: config.URL_Complete,
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

            var Cancel=function(data,callback){
                $.gitAjax({
                    url: config.URL_Cancel,
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

            var Audite=function(data,callback){
                $.gitAjax({
                    url: config.URL_Audite,
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
                Add: Add,
                LoadDetail: LoadDetail,
                AddProduct: AddProduct,
                DelProduct: DelProduct,
                GetList: GetList,
                GetDetail: GetDetail,
                GetDifPage: GetDifPage,
                SaveDif: SaveDif,
                Complete: Complete,
                AddDif: AddDif,
                Delete: Delete,
                Cancel: Cancel,
                Audite: Audite,
                ToExcel: ToExcel,
            }

        })($, config);
        return dataServer;
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=CheckManager.Server();
        var search=CheckManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            CheckManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=CheckManager.Server();
        var search=CheckManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            CheckManager.SetTable(result);
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
                    return "<input type='checkbox' name='in_item' value='"+data+"' data-full='"+JSON.stringify(full)+"'/>";
                }},
                { data: 'OrderNum',render:function(data,type,full,meta){
                    return git.GetStrSub(data,7);
                },createdCell:function(td, cellData, rowData, row, col){
                    if(!git.IsEmpty(cellData) && cellData.length>10){
                        $(td).popover({container:"body",content:cellData,trigger:"hover",placement:"bottom",delay:{show:500}});
                    } 
                }},
                { data: 'Type',render:function(data,type,full,meta){
                    return git.GetEnumDesc(ECheckType,data);
                }},
                { data: 'ContractOrder'},
                { data: 'Status',render:function(data,type,full,meta){
                    if(data==EAuditeJson.Pass){
                        return '<span class="label label-success">'+git.GetEnumDesc(EAudite,data)+'</span>';
                    }else if(data==EAuditeJson.NotPass){
                        return '<span class="label label-warning">'+git.GetEnumDesc(EAudite,data)+'</span>';
                    }else{
                        return git.GetEnumDesc(EAudite,data);
                    }
                }},
                { data: 'CreateUserName'},
                { data: 'CreateTime',render:function(data,type,full,meta){
                    return git.JsonToDateTime(data);
                }},
                
                {data:"ID",render:function(data,type,full,meta){
                    var html="";
                    
                    if(full.IsComplete!=EBoolJson.Yes){
                        html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;';
                    }

                    if(full.IsComplete!=EBoolJson.Yes){
                        html+='<a class="upload" href="javascript:void(0)">上传</a>&nbsp;';
                    }

                    if(full.IsComplete==EBoolJson.Yes && full.Status==EAuditeJson.Wait){
                        html+='<a class="audite" href="javascript:void(0)">审核</a>&nbsp;';
                    }

                    html+='<a class="view" href="javascript:void(0)">查看</a>&nbsp;';

                    if(full.IsComplete!=EBoolJson.Yes || full.Status==EAuditeJson.Wait){
                        html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                    }
                    
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
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: CheckManager.PageClick });
        }

        //绑定编辑 删除事件
        CheckManager.BindEvent();
    },
    BindEvent:function(){

        $("#tabList").find("tbody td").popover({
            container:"body",
        });

        //编辑
        $("#tabList").find("a.edit").click(function(){
            var SN=$(this).parent().parent().find("input[name='in_item']").val();
            window.location.href="/Order/Check/Add?SnNum="+SN;
        });

        //上传盘点数据
        $("#tabList").find("a.upload").click(function(){
            var SN=$(this).parent().parent().find("input[name='in_item']").val();
            window.location.href="/Order/Check/Upload?SnNum="+SN;
        });

        //审核
        $("#tabList").find("a.audite").click(function(){
            var SN=$(this).parent().parent().find("input[name='in_item']").val();
            CheckManager.Detail(SN,"Audite");
        });

        //查看
        $("#tabList").find("a.view").click(function(){
            var SN=$(this).parent().parent().find("input[name='in_item']").val();
            CheckManager.Detail(SN,"View");
        });

        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='in_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var list=[];
                    list.push(SN);
                    var param={};
                    param["list"]=JSON.stringify(list);
                    var Server=CheckManager.Server();
                    Server.Delete(param,function(result){
                        $.jBox.tip(result.Message,"success");
                        var pageSize=$("#mypager").pager("GetPageSize");
                        CheckManager.PageClick(1,pageSize);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });
    },
    SelectAll:function(item){
        var flag=$(item).attr("checked");
        if(flag){
            $("#tabList").find("input[name='in_item']").attr("checked",true);
        }else{
            $("#tabList").find("input[name='in_item']").attr("checked",false);
        }
    },
    GetSelect:function(){
        var list=[];
        $("#tabList").find("input[name='in_item']").each(function(i,item){
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

        var OrderNum=searchBar.find("input[name='OrderNum']").val();
        var InType=searchBar.find("select[name='InType']").val();
        var SupNum=searchBar.find("input[name='SupNum']").val();
        var SupName=searchBar.find("input[name='SupName']").val();
        var ContractOrder=searchBar.find("input[name='ContractOrder']").val();
        var Status=$("div[data-group='Status']").find("button.disabled").attr("value");
        var BeginTime=searchBar.find("input[name='BeginTime']").val();
        var EndTime=searchBar.find("input[name='EndTime']").val();
        var search={};
        search["OrderNum"]=OrderNum;
        search["InType"]=InType;
        search["SupNum"]=SupNum;
        search["SupName"]=SupName;
        search["ContractOrder"]=ContractOrder;
        search["Status"]=Status;
        search["BeginTime"]=BeginTime;
        search["EndTime"]=EndTime;

        return search;
    },
    Detail:function(SnNum,Command){
        var submit=function(v,h,f){
            if(v=="1"){
                var Reason=h.find('input[name="Reason"]').val();
                var param={};
                param["SnNum"]=SnNum;
                param["Status"]=2; //审核通过
                param["Reason"]=Reason;
                var Server=CheckManager.Server();
                Server.Audite(param,function(result){
                    $.jBox.tip(result.Message,"success");
                    var pageSize=$("#mypager").pager("GetPageSize");
                    CheckManager.PageClick(1,pageSize);
                }); 
            }else if(v=="2"){
                var Reason=h.find('input[name="Reason"]').val();
                var param={};
                param["SnNum"]=SnNum;
                param["Status"]=3; //审核不通过
                param["Reason"]=Reason;
                var Server=CheckManager.Server();
                Server.Audite(param,function(result){
                    $.jBox.tip(result.Message,"success");
                    var pageSize=$("#mypager").pager("GetPageSize");
                    CheckManager.PageClick(1,pageSize);
                }); 
            }else if(v=="3"){
                //打印
            }
        }
        var load=function(h){
            var OrderSnNum=h.find('input[name="SnNum"]').val();
            var Status=h.find('input[name="Status"]').val();
            var IsComplete=h.find('input[name="IsComplete"]').val();
            var loadDif=function(pageIndex,pageSize){
                var param={};
                param["OrderSnNum"]=OrderSnNum;
                param["PageIndex"]=pageIndex;
                param["PageSize"]=pageSize;
                var Server=CheckManager.Server();
                Server.GetDifPage(param,function(result){
                    h.find('#tabDetail').DataTable({
                        destroy: true,
                        data:result.Result,
                        paging:false,
                        searching:false,
                        scrollX: false,
                        scrollY: "195px",
                        bAutoWidth:false,
                        bInfo:false,
                        ordering:false,
                        columns: [
                            { data: 'ProductName'},
                            { data: 'BarCode'},
                            { data: 'Size'},
                            { data: 'BatchNum'},
                            { data: 'LocalName',render:function(data,type,full,meta){
                                if(git.IsEmpty(full.LocalNum)){
                                    return "";
                                }else{
                                    return full.StorageName+"."+data;
                                }
                            }},
                            { data: 'LocalQty',render:function(data,type,full,meta){
                                return git.ToDecimal(data,2);
                            }},
                            { data: 'FirstQty',render:function(data,type,full,meta){
                                if(IsComplete!=EBoolJson.Yes){
                                    return "X";
                                }else{
                                    return git.ToDecimal(data,2);
                                }
                            }},
                            { data: 'DifQty',render:function(data,type,full,meta){
                                if(IsComplete!=EBoolJson.Yes){
                                    return "X";
                                }else{
                                    return git.ToDecimal(data,2);
                                }
                            }}
                        ],
                        oLanguage:{
                            sEmptyTable:"没有查询到任何数据"
                        }
                    });

                    var pageInfo=result.PageInfo;
                    if(pageInfo!=undefined){
                        h.find("#mypager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: loadDif });
                    }

                });
            }
            
            loadDif(1,5);
        }

        if(Command=="View"){
            $.jBox.open("get:/Order/Check/Detail?SnNum="+SnNum, "盘点单详细", 850, 480, { buttons: {"关闭":4 } ,submit:submit,loaded:function(h){
                load(h);
            }}); 
        }else if(Command=="Audite"){
            $.jBox.open("get:/Order/Check/Detail?SnNum="+SnNum, "盘点单详细", 850, 480, { buttons: {"审核通过": 1, "审核不通过": 2,"打印":3,"关闭":4 } ,submit:submit,loaded:function(h){
                load(h);
            }}); 
        }
    },
    ToolBar:function(){
        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                window.location.href="/Order/Check/Add";
            }else if(command=="Edit"){
                var list=CheckManager.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var SN=list[0];
                window.location.href="/Order/Check/Add?SnNum="+SN;
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=CheckManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=CheckManager.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            CheckManager.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=CheckManager.Server();
                var search=CheckManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                CheckManager.Refresh();
            }

        });

        //搜索 高级搜索按钮
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            CheckManager.PageClick(1,10);
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
            CheckManager.PageClick(1,10);
        });
        
        //加载默认数据
        CheckManager.PageClick(1,10);
    }
}



var CheckOrder={
    Init:function(){

        //初始化绑定事件
        $("div.formbtn").find("button.btn").click(function(event){
            var command=$(this).attr("data-command");
            if(command=="Add"){
                
            }else if(command=="Save"){

                var param=CheckOrder.GetModel();
                if(!param){
                    return false;
                }
                var Server=CheckManager.Server();
                Server.Add(param,function(result){
                    if(result.Code==1){
                        window.location.href="/Order/Check/List";
                    }else{
                        $.jBox.tip(result.Message);
                    }
                });

            }else if(command=="Cancel"){
                window.location.href="/Order/Check/List";
            }
        });

        CheckOrder.PageClick(1,5);

        //选择产品
        CheckOrder.AddProduct();
    },
    PageClick:function(PageIndex,PageSize){
        var Server=CheckManager.Server();
        var search=CheckManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.LoadDetail(search,function(result){
            CheckOrder.SetTable(result);
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
                    return "<input type='checkbox' name='in_item' value='"+data+"' data-full='"+JSON.stringify(full)+"'/>";
                }},
                { data: 'ProductName'},
                { data: 'BarCode'},
                { data: 'Size'},
                { data: 'CateName'},
                { data: 'UnitName'},
                {data:"ID",render:function(data,type,full,meta){
                    var html="";
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
            $("#mypager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: CheckOrder.PageClick });
        }

        CheckOrder.BindTableEvent();
    },
    BindTableEvent:function(){
        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='in_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var param={};
                    param["SnNum"]=SN;
                    var Server=CheckManager.Server();
                    Server.DelProduct(param,function(result){
                        $.jBox.tip(result.Message,"success");
                        var pageSize=$("#mypager").pager("GetPageSize");
                        CheckOrder.PageClick(1,pageSize);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });
    },
    AddProduct:function(SnNum){
        $("div.formbtn").find("button.btn[data-command='Add']").ProductDialog({Mult:true,EventName:"click",callBack:function(result){
            console.log(result);

            if(result!=undefined && result.length>0){
                var list=[];
                for(var i=0;i<result.length;i++){
                    var entity={};
                    entity["ProductName"]=result[i].ProductName;
                    entity["BarCode"]=result[i].BarCode;
                    entity["TargetNum"]=result[i].SnNum;

                    entity["Size"]=result[i].Size;
                    entity["CateName"]=result[i].CateName;
                    entity["UnitName"]=result[i].UnitName;

                    list.push(entity);
                }

                var Server=CheckManager.Server();
                var param={};
                param["List"]=JSON.stringify(list);
                Server.AddProduct(param,function(result){
                    $.jBox.tip(result.Message,"success");
                    var pageSize=$("#mypager").pager("GetPageSize");
                    CheckOrder.PageClick(1,pageSize);
                });
            }

        }});
    },
    GetModel:function(){
        var SnNum=$("input[name='SnNum']").val();
        var OrderNum=$("input[name='OrderNum']").val();
        var ContractOrder=$("input[name='ContractOrder']").val();
        var CreateUser=$("input[name='CreateUser']").val();
        var Remark=$("input[name='Remark']").val();

        var param={};
        param["SnNum"]=SnNum;
        param["OrderNum"]=OrderNum;
        param["ContractOrder"]=ContractOrder;
        param["CreateUser"]=SnNum;
        param["Remark"]=Remark;

        var entity={};

        entity["Entity"]=JSON.stringify(param);

        return entity;
    }
}


//上传盘点数据
var CheckUpload={
    Init:function(){

        //初始化绑定事件
        $("div.formbtn").find("button.btn").click(function(event){
            var command=$(this).attr("data-command");
            if(command=="Add"){
                var OrderNum=$("input[name='OrderNum']").val();
                var OrderSnNum=$("input[name='SnNum']").val();
                //新增盘点产品
                CheckUpload.AddProduct(OrderSnNum,OrderNum);
            }else if(command=="Save"){

                var param=CheckUpload.GetModel();
                if(!param){
                    return false;
                }
                var Server=CheckManager.Server();
                Server.Complete(param,function(result){
                    if(result.Code==1){
                        window.location.href="/Order/Check/List";
                    }else{
                        $.jBox.tip(result.Message);
                    }
                });

            }else if(command=="Cancel"){
                window.location.href="/Order/Check/List";
            }
        });

        CheckUpload.PageClick(1,5);
    },
    PageClick:function(PageIndex,PageSize){

        var OrderSnNum=$("input[name='SnNum']").val();
        var Server=CheckManager.Server();
        var search={};
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        search["OrderSnNum"]=OrderSnNum;
        Server.GetDifPage(search,function(result){
            CheckUpload.SetTable(result);
        });
    },
    SetTable:function(result){
        var Status=$("input[name='Status']").val();
        var LocationStr=$("input[name='Location']").val();
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
                { data: 'ProductName',createdCell:function(td, cellData, rowData, row, col){
                    var tr=$(td).parent();
                    $(tr).attr("data-value",JSON.stringify(rowData));
                }},
                { data: 'BarCode'},
                { data: 'Size'},
                { data: 'BatchNum',render:function(data,type,full,meta){
                    return '<input type="text" name="BatchNum" class="m-wrap input-small" value="'+data+'">';
                }},
                { data: 'LocalName',render:function(data,type,full,meta){
                    var html="";
                    html+='<select name="LocalName" class="input-medium" data-value="'+full.LocalNum+'">';
                    html+=LocationStr;
                    html+='</select>';
                    return html;
                }},
                { data: 'LocalQty',render:function(data,type,full,meta){
                    return git.ToDecimal(data,2);
                }},
                { data: 'FirstQty',render:function(data,type,full,meta){
                    return '<input type="text" name="FirstQty" class="m-wrap input-small" value="'+data+'">';
                }},
                { data: 'ID',render:function(data,type,full,meta){
                    var html="";
                    html+='<a class="save" href="javascript:void(0)">保存</a>&nbsp;';
                    return html;
                }},
            ],
            aoColumnDefs:[
                
            ],
            oLanguage:{
                sEmptyTable:"没有查询到任何数据"
            }
        });

        //初始化
        $("#tabList").find("select[name='LocalName']").each(function(i,item){
            var LocalNum=$(item).attr("data-value");
            $(item).find("option").each(function(index, el) {
                if($(el).val()==LocalNum){
                    $(el).attr("selected",true);
                }
            });
        });

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: CheckUpload.PageClick });
        }

        CheckUpload.BindTableEvent();
    },
    BindTableEvent:function(){
        $("#tabList").find("a.save").click(function(){
            var tr=$(this).parent().parent();
            var param=JSON.parse($(tr).attr("data-value"));

            var BatchNum=$(tr).find('input[name="BatchNum"]').val();
            var LocalNum=$(tr).find('select[name="LocalName"]').val();
            var LocalName=$(tr).find('select[name="LocalName"]').find("option:selected").text();
            var FirstQty=$(tr).find('input[name="FirstQty"]').val();

            param["BatchNum"]=BatchNum;
            if(git.IsEmpty(LocalNum)){
                param["LocalNum"]="";
                param["LocalName"]="";
            }else{
                param["LocalNum"]=LocalNum;
                param["LocalName"]=LocalName;
            }
            param["FirstQty"]=FirstQty;

            var Server=CheckManager.Server();
            Server.SaveDif({Entity:JSON.stringify(param)},function(result){
                $.jBox.tip(result.Message,"success");
                var pageSize=$("#mypager").pager("GetPageSize");
                CheckUpload.PageClick(1,pageSize);
            });
        });
    },
    AddProduct:function(OrderSnNum,OrderNum){
        var submit=function(v, h, f){
            if(v){

                var ProductNum=h.find("input[name='ProductNum']").val();
                var BarCode=h.find("input[name='BarCode']").val();
                var ProductName=h.find("input[name='ProductName']").val();
                var Size=h.find("input[name='Size']").val();
                var InPrice=h.find("input[name='InPrice']").val();
                var BatchNum=h.find("input[name='BatchNum']").val();
                var LocalName=h.find("input[name='LocalName']").val();
                var LocalNum=h.find("input[name='LocalNum']").val();
                var Num=h.find("input[name='Num']").val();
                var StorageNum=h.find("input[name='StorageNum']").val();

                if(git.IsEmpty(LocalNum) || git.IsEmpty(LocalName)){
                    $.jBox.tip("请选择盘点库位","warn");
                    return false;
                }

                if(git.IsEmpty(Num)){
                    $.jBox.tip("请输入盘点数量","warn");
                    return false;
                }

                if(isNaN(Num)){
                    $.jBox.tip("数量必须为数字","warn");
                    return false;
                }

                var param={};
                param["OrderSnNum"]=OrderSnNum;
                param["OrderNum"]=OrderNum;
                param["LocalNum"]=LocalNum;
                param["LocalName"]=LocalName;
                param["StorageNum"]=StorageNum;
                param["ProductNum"]=ProductNum;
                param["BarCode"]=BarCode;
                param["ProductName"]=ProductName;
                param["BatchNum"]=BatchNum;
                param["LocalQty"]=0;
                param["FirstQty"]=Num;
                param["SecondQty"]=0;
                param["DifQty"]=0;

                var entity={};
                entity["Entity"]=JSON.stringify(param);

                var Server=CheckManager.Server();
                Server.AddDif(entity,function(result){
                    if(result.Code==1){
                        CheckUpload.PageClick(1,5);
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                    
                });
            }
        }

        //对话框加载之后回调事件
        var  load=function(h){
            h.find("input[name='BarCode']").ProductDialog({Mult:false,callBack:function(result){
                h.find("input[name='BarCode']").val(result.BarCode);
                h.find("input[name='ProductNum']").val(result.SnNum);
                h.find("input[name='ProductName']").val(result.ProductName);
                h.find("input[name='Size']").val(result.Size);
                h.find("input[name='InPrice']").val(result.AvgPrice);
            }});

            h.find("input[name='LocalName']").LocalDialog({Mult:false,callBack:function(result){
                console.log(JSON.stringify(result));
                h.find("input[name='LocalName']").val(result.LocalName);
                h.find("input[name='LocalNum']").val(result.LocalNum);
                h.find("input[name='StorageNum']").val(result.StorageNum);
            }});
        }
        $.jBox.open("get:/Order/InStorage/AddProduct", "新增盘点产品", 420, 450, { buttons: { "确定": true, "关闭": false } ,submit:submit,loaded:load});
    },
    GetModel:function(){
        var SnNum=$("input[name='SnNum']").val();
        var param={};
        param["SnNum"]=SnNum;
        return param;
    }
}