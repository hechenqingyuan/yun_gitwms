
/**
*
*入库管理功能
*
**/

var InStorageManager = {
    InTable:undefined,
    Server: function () {
        var config = (function () {
            var URL_Add = "/Order/InStorageAjax/Add";
            var URL_LoadDetail = "/Order/InStorageAjax/LoadDetail";
            var URL_AddProduct = "/Order/InStorageAjax/AddProduct";
            var URL_DelProduct = "/Order/InStorageAjax/DelProduct";

            var URL_GetList = "/Order/InStorageManagerAjax/GetList";
            var URL_GetDetail = "/Order/InStorageManagerAjax/GetDetail";
            var URL_Delete = "/Order/InStorageManagerAjax/Delete";
            var URL_Cancel = "/Order/InStorageManagerAjax/Cancel";
            var URL_Audite = "/Order/InStorageManagerAjax/Audite";
            var URL_ToExcel = "/Order/InStorageManagerAjax/ToExcel";

            return {
                URL_GetList: URL_GetList,
                URL_GetDetail: URL_GetDetail,
                URL_LoadDetail: URL_LoadDetail,
                URL_AddProduct: URL_AddProduct,
                URL_DelProduct: URL_DelProduct,

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
        var Server=InStorageManager.Server();
        var search=InStorageManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            $.jBox.closeTip();
            if(result.Code==1){
                InStorageManager.SetTable(result);
            }else{
                $.jBox.tip(result.Message,"warn");
            }
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=InStorageManager.Server();
        var search=InStorageManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            $.jBox.closeTip();
            if(result.Code==1){
                InStorageManager.SetTable(result);
            }else{
                $.jBox.tip(result.Message,"warn");
            }
        });
    },
    SetTable:function(result){
        var cols=[
            {title:'订单号', name:'OrderNum', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'产品名称', name:'ProductName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'产品编号', name:'BarCode', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'批次', name:'BatchNum', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'规格', name:'Size', width: 85, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'数量', name:'Num', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'仓库', name:'StorageName', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'库位', name:'LocalName', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'入库类型', name:'InType', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.GetEnumDesc(EInType,data);
            }},
            {title:'供应商', name:'SupName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'制单人', name:'CreateUserName', width: 75, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'制单时间', name:'CreateTime', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.JsonToDateTime(data);
            }},
            {title:'状态', name:'Status', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                if(data==EAuditeJson.Pass){
                    return '<span class="label label-success">'+git.GetEnumDesc(EAudite,data)+'</span>';
                }else if(data==EAuditeJson.NotPass){
                    return '<span class="label label-warning">'+git.GetEnumDesc(EAudite,data)+'</span>';
                }else{
                    return git.GetEnumDesc(EAudite,data);
                }
            }},
            {title:'审核人', name:'AuditeUserName', width: 75, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'审核时间', name:'AuditeTime', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.JsonToDateTime(data);
            }},
            {title:'操作', name:'OrderNum', width: 150, align: 'left',lockWidth:false,  renderer: function(data,item,rowIndex){
                var html="";
                if(item.Status==EAuditeJson.Wait || item.Status==EAuditeJson.NotPass){
                    html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;';
                }
                if(item.Status==EAuditeJson.Wait || item.Status==EAuditeJson.NotPass){
                    html+='<a class="audite" href="javascript:void(0)">审核</a>&nbsp;';
                }
                html+='<a class="view" href="javascript:void(0)">查看</a>&nbsp;';
                html+='<a class="print" href="javascript:void(0)">打印</a>&nbsp;';
                html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                return html;
            }},
        ];

        if(this.InTable==undefined){
            this.InTable=$("#tabList").mmGrid({
                cols:cols,
                items:result.Result,
                checkCol:true,
                nowrap:true,
                height:380
            });
            //绑定编辑 删除事件
            InStorageManager.BindEvent();
        }else{
            this.InTable.load(result.Result);
        }


        var pageInfo = result.PageInfo;
        if (pageInfo != undefined) {
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: InStorageManager.PageClick });
        }
    },
    BindEvent:function(){

        this.InTable.off("cellSelected").on("cellSelected",function(e, item, rowIndex, colIndex){
            
            if($(e.target).is("a.edit")){
                var SN=item.OrderSnNum;
                window.location.href="/Order/InStorage/Add?SnNum="+SN;
            }else if($(e.target).is("a.audite")){
                var SN=item.OrderSnNum;
                InStorageManager.Detail(SN,"Audite");
            }else if($(e.target).is("a.view")){
                var SN=item.OrderSnNum;
                InStorageManager.Detail(SN,"View");
            }else if($(e.target).is("a.delete")){
                var SN=item.OrderSnNum;
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=[];
                        list.push(SN);
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=InStorageManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                InStorageManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"success");
                            }
                        });
                    }
                }
                $.jBox.confirm("该操作将删除该订单所有数据,确定要删除吗？", "提示", submit);
            }
        });

        this.InTable.on('loadSuccess', function(e, data){
            $('#tabList').find('a.print').each(function(i,item){
                $(item).CusReportDialog({
                    ReportType:1,
                    callBack:function(result){
                        if(result!=undefined){
                            var SN=data[i].OrderSnNum;
                            var SnNum=result.SnNum;
                            var url="/Report/Manager/Show?SnNum="+SnNum+"&OrderNum="+SN;
                            window.location.href=url;
                        }
                    }
                });
            });
        });

    },
    GetSelect:function(){
        var list=[];
        if(this.InTable!=undefined){
            var rows=this.InTable.selectedRows();
            if(rows!=undefined && rows.length>0){
                for(var i=0;i<rows.length;i++){
                    list.push(rows[i].OrderSnNum);
                }
            }
        }
        return list;
    },
    GetRows:function(){
        var list=[];
        if(this.InTable!=undefined){
            var rows=this.InTable.selectedRows();
            if(rows!=undefined && rows.length>0){
                for(var i=0;i<rows.length;i++){
                    list.push(rows[i]);
                }
            }
        }
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
                var Server=InStorageManager.Server();
                Server.Audite(param,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        InStorageManager.PageClick(1,pageSize);
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                }); 
            }else if(v=="2"){
                var Reason=h.find('input[name="Reason"]').val();
                var param={};
                param["SnNum"]=SnNum;
                param["Status"]=3; //审核不通过
                param["Reason"]=Reason;
                var Server=InStorageManager.Server();
                Server.Audite(param,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        InStorageManager.PageClick(1,pageSize);
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                }); 
            }else if(v=="3"){
                //打印
            }
        }
        var load=function(h){
            var param={};
            param["SnNum"]=SnNum;
            var Server=InStorageManager.Server();
            Server.GetDetail(param,function(result){
                h.find('#tabDetail').DataTable({
                    destroy: true,
                    data:result.Result,
                    paging:false,
                    searching:false,
                    scrollX: false,
                    scrollY: "165px",
                    bAutoWidth:false,
                    bInfo:false,
                    ordering:false,
                    columns: [
                        { data: 'ProductName'},
                        { data: 'BarCode'},
                        { data: 'Size'},
                        { data: 'BatchNum'},
                        { data: 'InPrice',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }},
                        { data: 'Num',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }},
                        { data: 'Amount',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }},
                        { data: 'LocalName',render:function(data,type,full,meta){
                            return full.StorageName+"."+data;
                        }}
                    ],
                    oLanguage:{
                        sEmptyTable:"没有查询到任何数据"
                    }
                });
            });
        }

        if(Command=="View"){
            $.jBox.open("get:/Order/InStorage/Detail?SnNum="+SnNum, "入库单详细", 850, 450, { buttons: {"关闭":4 } ,submit:submit,loaded:function(h){
                load(h);
            }}); 
        }else if(Command=="Audite"){
            $.jBox.open("get:/Order/InStorage/Detail?SnNum="+SnNum, "入库单详细", 850, 450, { buttons: {"审核通过": 1, "审核不通过": 2,"关闭":4 } ,submit:submit,loaded:function(h){
                load(h);
            }}); 
        }
    },
    ToolBar:function(){
        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                window.location.href="/Order/InStorage/Add";
            }else if(command=="Edit"){
                var list=InStorageManager.GetRows();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var item=list[0];
                if(item.Status==EAuditeJson.Wait || item.Status==EAuditeJson.NotPass){
                    var SN=item.OrderSnNum;
                    window.location.href="/Order/InStorage/Add?SnNum="+SN;
                }else{
                    $.jBox.tip("该订单不能编辑","warn");
                }
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=InStorageManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=InStorageManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                InStorageManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"warn");
                            }
                        });
                    }
                }
                $.jBox.confirm("该操作将删除该订单所有数据,确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=InStorageManager.Server();
                var search=InStorageManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                InStorageManager.Refresh();
            }

        });

        //搜索 高级搜索按钮
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            InStorageManager.PageClick(1,10);
        });
        searchBar.find("button[data-command='Advanced']").click(function(){
            var display=$("div.formsearch").css("display");
            if(display=="block"){
                $("div.formsearch").slideUp();
            }else{
                $("div.formsearch").slideDown();
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
            InStorageManager.PageClick(1,10);
        });
        
        //监听回车事件,用于扫描
        searchBar.find("input[name='OrderNum']").keydown(function(event){
            if (event.keyCode == 13) {    
                var value=$(this).val();
                if(!git.IsEmpty(value)){
                    InStorageManager.PageClick(1,10);
                    setTimeout(function(){
                        searchBar.find("input[name='OrderNum']").val("");
                        searchBar.find("input[name='OrderNum']").focus();
                    },300);
                }
            }    
        });

        //窗体加载获得焦点
        searchBar.find("input[name='OrderNum']").focus();
        //加载默认数据
        InStorageManager.PageClick(1,10);
    }
}



var InStorageOrder={
    Init:function(){

        //初始化绑定事件
        $("div.formbtn").find("button.btn").click(function(event){
            var command=$(this).attr("data-command");
            if(command=="Add"){
                InStorageOrder.AddProduct();
            }else if(command=="Save"){

                var param=InStorageOrder.GetModel();
                if(!param){
                    return false;
                }
                var Server=InStorageManager.Server();
                Server.Add(param,function(result){
                    if(result.Code==1){
                        window.location.href="/Order/InStorage/List";
                    }else{
                        $.jBox.tip(result.Message);
                    }
                });

            }else if(command=="Cancel"){
                window.location.href="/Order/InStorage/List";
            }
        });

        //选择供应商
        $("input[name='SupNum']").SupplierDialog({Mult:false,callBack:function(result){
            if(result!=undefined){
                $("input[name='SupSnNum']").val(result.SnNum);
                $("input[name='SupNum']").val(result.SupNum);
                $("input[name='SupName']").val(result.SupName);
                $("input[name='ContactName']").val(result.ContactName);
                $("input[name='Phone']").val(result.Phone);

                //如果值发生变化
                $("input[name='SupNum']").unbind("change").bind("change",function(){
                    var value=$(this).val();
                    if(value!=result.SupNum){
                        $("input[name='SupSnNum']").val("");
                        $("input[name='SupName']").val("");
                        $("input[name='ContactName']").val("");
                        $("input[name='Phone']").val("");
                    }
                });
            }
        }});

        //自动提示供应商搜索
        $("input[name='SupNum']").autocomplete({
            paramName: "KeyWord",
            url: '/Client/SupplierAjax/AutoSupplier',
            showResult: function (value, data) {
                var row = JSON.parse(value);
                return '<span>' + row.SupNum + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + row.SupName + '</span>';
            },
            onItemSelect: function (item) {

            },
            maxItemsToShow: 5,
            selectedCallback: function (selectItem) {
                $("input[name='SupSnNum']").val(selectItem.SnNum);
                $("input[name='SupNum']").val(selectItem.SupNum);
                $("input[name='SupName']").val(selectItem.SupName);
                $("input[name='ContactName']").val(selectItem.ContactName);
                $("input[name='Phone']").val(selectItem.Phone);
            }
        });


        InStorageOrder.PageClick(1,5);
    },
    PageClick:function(PageIndex,PageSize){
        var Server=InStorageManager.Server();
        var search=InStorageManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.LoadDetail(search,function(result){
            InStorageOrder.SetTable(result);
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
                { data: 'BatchNum'},
                { data: 'InPrice'},
                { data: 'Num',render:function(data,type,full,meta){
                    return git.ToDecimal(data,2);
                }},
                { data: 'Amount',render:function(data,type,full,meta){
                    return git.ToDecimal(data,2);
                }},
                { data: 'LocalName'},
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
            $("#mypager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: InStorageOrder.PageClick });
        }

        InStorageOrder.BindTableEvent();
    },
    BindTableEvent:function(){
        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='in_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var param={};
                    param["SnNum"]=SN;
                    var Server=InStorageManager.Server();
                    Server.DelProduct(param,function(result){
                        if(result.Code==1){
                            var pageSize=$("#mypager").pager("GetPageSize");
                            InStorageOrder.PageClick(1,pageSize);
                        }else{
                            $.jBox.tip(result.Message,"success");
                        }
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });
    },
    AddProduct:function(SnNum){
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
                    $.jBox.tip("请选择要入库库位","warn");
                    return false;
                }

                if(git.IsEmpty(Num)){
                    $.jBox.tip("请输入要入库的数量","warn");
                    return false;
                }

                if(isNaN(Num)){
                    $.jBox.tip("数量必须为数字","warn");
                    return false;
                }

                var param={};
                param["ProductNum"]=ProductNum;
                param["BarCode"]=BarCode;
                param["ProductName"]=ProductName;
                param["Size"]=Size;
                param["InPrice"]=InPrice;
                param["BatchNum"]=BatchNum;
                param["LocalName"]=LocalName;
                param["LocalNum"]=LocalNum;
                param["Num"]=Num;
                param["StorageNum"]=StorageNum;

                var entity={};
                entity["Entity"]=JSON.stringify(param);

                var Server=InStorageManager.Server();
                Server.AddProduct(entity,function(result){
                    if(result.Code==1){
                        InStorageOrder.PageClick(1,5);
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });
            }
        }

        //对话框加载之后回调事件
        var  load=function(h){
            h.find("input[name='BarCode']").ProductDialog({Mult:false,
                callBack:function(result){
                    h.find("input[name='BarCode']").val(result.BarCode);
                    h.find("input[name='ProductNum']").val(result.SnNum);
                    h.find("input[name='ProductName']").val(result.ProductName);
                    h.find("input[name='Size']").val(result.Size);
                    h.find("input[name='InPrice']").val(result.AvgPrice);
                }
            });

            h.find("input[name='LocalName']").LocalDialog({Mult:false,
                data:{ListLocalType:JSON.stringify([1,2,3])},
                callBack:function(result){
                    console.log(JSON.stringify(result));
                    h.find("input[name='LocalName']").val(result.LocalName);
                    h.find("input[name='LocalNum']").val(result.LocalNum);
                    h.find("input[name='StorageNum']").val(result.StorageNum);
                }
            });
        }
        $.jBox.open("get:/Order/InStorage/AddProduct", "新增产品", 420, 450, { buttons: { "确定": true, "关闭": false } ,submit:submit,loaded:load});
    },
    GetModel:function(){
        var SnNum=$("input[name='SnNum']").val();
        var OrderNum=$("input[name='OrderNum']").val();
        var InType=$("select[name='InType']").val();
        var ContractOrder=$("input[name='ContractOrder']").val();
        var CreateUser=$("input[name='CreateUser']").val();
        var SupNum=$("input[name='SupNum']").val();
        var SupName=$("input[name='SupName']").val();
        var SupSnNum=$("input[name='SupSnNum']").val();
        var ContactName=$("input[name='ContactName']").val();
        var Phone=$("input[name='Phone']").val();
        var OrderTime=$("input[name='OrderTime']").val();
        var Remark=$("input[name='Remark']").val();

        //校验
        if(git.IsEmpty(InType)){
            $.jBox.tip("请选择入库单类型","warn");
            return false;
        }

        if(git.IsEmpty(SupSnNum)){
            $.jBox.tip("请选择供应商","warn");
            return false;
        }

        var param={};
        param["SnNum"]=SnNum;
        param["OrderNum"]=OrderNum;
        param["InType"]=InType;
        param["ContractOrder"]=ContractOrder;
        param["CreateUser"]=SnNum;
        param["SupNum"]=SupNum;
        param["SupName"]=SupName;
        param["SupSnNum"]=SupSnNum;
        param["ContactName"]=ContactName;
        param["Phone"]=Phone;
        param["OrderTime"]=OrderTime;
        param["Remark"]=Remark;

        var entity={};

        entity["Entity"]=JSON.stringify(param);

        return entity;
    }
}