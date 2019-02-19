
/**
*
*调拨管理
*
**/

var AllocateManager = {
    TabGrid:undefined,
    Server: function () {
        var config = (function () {
            var URL_Add = "/Order/AllocateAjax/Add";
            var URL_LoadDetail = "/Order/AllocateAjax/LoadDetail";
            var URL_AddProduct = "/Order/AllocateAjax/AddProduct";
            var URL_DelProduct = "/Order/AllocateAjax/DelProduct";

            var URL_GetList = "/Order/AllocateManagerAjax/GetList";
            var URL_GetDetail = "/Order/AllocateManagerAjax/GetDetail";
            var URL_Delete = "/Order/AllocateManagerAjax/Delete";
            var URL_Cancel = "/Order/AllocateManagerAjax/Cancel";
            var URL_Audite = "/Order/AllocateManagerAjax/Audite";
            var URL_ToExcel = "/Order/AllocateManagerAjax/ToExcel";

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
            var Add = function (data, callback) {
                $.gitAjax({
                    url: config.URL_Add,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if (callback != undefined && typeof callback == "function") {
                            callback(result);
                        }
                    }
                });
            }

            var LoadDetail = function (data, callback) {
                $.gitAjax({
                    url: config.URL_LoadDetail,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if (callback != undefined && typeof callback == "function") {
                            callback(result);
                        }
                    }
                });
            }

            var AddProduct = function (data, callback) {
                $.gitAjax({
                    url: config.URL_AddProduct,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if (callback != undefined && typeof callback == "function") {
                            callback(result);
                        }
                    }
                });
            }

            var DelProduct = function (data, callback) {
                $.gitAjax({
                    url: config.URL_DelProduct,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if (callback != undefined && typeof callback == "function") {
                            callback(result);
                        }
                    }
                });
            }

            var GetList = function (data, callback) {
                $.gitAjax({
                    url: config.URL_GetList,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if (callback != undefined && typeof callback == "function") {
                            callback(result);
                        }
                    }
                });
            }

            var GetDetail = function (data, callback) {
                $.gitAjax({
                    url: config.URL_GetDetail,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if (callback != undefined && typeof callback == "function") {
                            callback(result);
                        }
                    }
                });
            }

            var Delete = function (data, callback) {
                $.gitAjax({
                    url: config.URL_Delete,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if (callback != undefined && typeof callback == "function") {
                            callback(result);
                        }
                    }
                });
            }

            var Cancel = function (data, callback) {
                $.gitAjax({
                    url: config.URL_Cancel,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if (callback != undefined && typeof callback == "function") {
                            callback(result);
                        }
                    }
                });
            }

            var Audite = function (data, callback) {
                $.gitAjax({
                    url: config.URL_Audite,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if (callback != undefined && typeof callback == "function") {
                            callback(result);
                        }
                    }
                });
            }

            var ToExcel = function (data, callback) {
                $.gitAjax({
                    url: config.URL_ToExcel,
                    data: data,
                    type: "post",
                    dataType: "json",
                    success: function (result) {
                        if (callback != undefined && typeof callback == "function") {
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
    PageClick: function (PageIndex, PageSize) {
        $.jBox.tip("正在努力加载数据...", "loading");
        var Server = AllocateManager.Server();
        var search = AllocateManager.GetSearch();
        search["PageIndex"] = PageIndex;
        search["PageSize"] = PageSize;
        Server.GetList(search, function (result) {
            AllocateManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh: function () {
        var PageSize = $("#mypager").pager("GetPageSize");
        var PageIndex = $("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...", "loading");
        var Server = AllocateManager.Server();
        var search = AllocateManager.GetSearch();
        search["PageIndex"] = PageIndex;
        search["PageSize"] = PageSize;
        Server.GetList(search, function (result) {
            AllocateManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable: function (result) {

        var cols=[
            {title:'调拨单号', name:'OrderNum', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
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
            {title:'原仓库', name:'FromStorageName', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'原库位', name:'FromLocalName', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'调拨仓库', name:'ToStorageName', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            /*{title:'调拨类型', name:'AllocateType', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.GetEnumDesc(EAllocateType,data);
            }},*/
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
            {title:'审核人', name:'AuditUserName', width: 75, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'审核时间', name:'AuditeTime', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.JsonToDateTime(data);
            }},
            {title:'操作', name:'OrderNum', width: 150, align: 'left',lockWidth:false,  renderer: function(data,item,rowIndex){
                var html = "";
                if (item.Status == EAuditeJson.Wait || item.Status == EAuditeJson.NotPass) {
                    html += '<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;';
                }
                if (item.Status == EAuditeJson.Wait || item.Status == EAuditeJson.NotPass) {
                    html += '<a class="audite" href="javascript:void(0)">审核</a>&nbsp;';
                }
                html += '<a class="view" href="javascript:void(0)">查看</a>&nbsp;';
                html += '<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                return html;
            }},
        ];

        if(this.TabGrid==undefined){
            this.TabGrid=$("#tabList").mmGrid({
                cols:cols,
                items:result.Result,
                checkCol:true,
                nowrap:true,
                height:380
            });
            //绑定编辑 删除事件
            AllocateManager.BindEvent();
        }else{
            this.TabGrid.load(result.Result);
        }

        var pageInfo = result.PageInfo;
        if (pageInfo != undefined) {
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: AllocateManager.PageClick });
        }
    },
    BindEvent: function () {

        this.TabGrid.off("cellSelected").on("cellSelected",function(e, item, rowIndex, colIndex){
            
            if($(e.target).is("a.edit")){
                var SN=item.OrderSnNum;
                window.location.href = "/Order/Allocate/Add?SnNum=" + SN;
            }else if($(e.target).is("a.audite")){
                var SN=item.OrderSnNum;
                AllocateManager.Detail(SN, "Audite");
            }else if($(e.target).is("a.view")){
                var SN=item.OrderSnNum;
                AllocateManager.Detail(SN, "View");
            }else if($(e.target).is("a.delete")){
                var SN=item.OrderSnNum;
                var submit = function (v, h, f) {
                    if (v == "ok") {
                        var list = [];
                        list.push(SN);
                        var param = {};
                        param["list"] = JSON.stringify(list);
                        var Server = AllocateManager.Server();
                        Server.Delete(param, function (result) {
                            $.jBox.tip(result.Message, "success");
                            var pageSize = $("#mypager").pager("GetPageSize");
                            AllocateManager.PageClick(1, pageSize);
                        });
                    }
                }
                $.jBox.confirm("该操作将删除该订单所有数据,确定要删除吗？", "提示", submit);
            }
        });
    },
    GetSelect: function () {
        var list=[];
        if(this.TabGrid!=undefined){
            var rows=this.TabGrid.selectedRows();
            if(rows!=undefined && rows.length>0){
                for(var i=0;i<rows.length;i++){
                    list.push(rows[i].OrderSnNum);
                }
            }
        }
        return list;
    },
    GetRows: function () {
        var list=[];
        if(this.TabGrid!=undefined){
            var rows=this.TabGrid.selectedRows();
            if(rows!=undefined && rows.length>0){
                for(var i=0;i<rows.length;i++){
                    list.push(rows[i]);
                }
            }
        }
        return list;
    },
    GetSearch: function () {
        var searchBar = $("div[data-condition='search']");

        var OrderNum = searchBar.find("input[name='OrderNum']").val();
        var AllocateType = searchBar.find("select[name='AllocateType']").val();
        var ContractOrder = searchBar.find("input[name='ContractOrder']").val();
        var Status = $("div[data-group='Status']").find("button.disabled").attr("value");
        var BeginTime = searchBar.find("input[name='BeginTime']").val();
        var EndTime = searchBar.find("input[name='EndTime']").val();
        var search = {};
        search["OrderNum"] = OrderNum;
        search["AllocateType"] = AllocateType;
        search["ContractOrder"] = ContractOrder;
        search["Status"] = Status;
        search["BeginTime"] = BeginTime;
        search["EndTime"] = EndTime;

        return search;
    },
    Detail: function (SnNum, Command) {
        var submit = function (v, h, f) {
            if (v == "1") {
                var param = {};
                param["SnNum"] = SnNum;
                param["Status"] = 2; //审核通过
                var Server = AllocateManager.Server();
                Server.Audite(param, function (result) {
                    $.jBox.tip(result.Message, "success");
                    var pageSize = $("#mypager").pager("GetPageSize");
                    AllocateManager.PageClick(1, pageSize);
                });
            } else if (v == "2") {
                var param = {};
                param["SnNum"] = SnNum;
                param["Status"] = 3; //审核不通过
                var Server = AllocateManager.Server();
                Server.Audite(param, function (result) {
                    $.jBox.tip(result.Message, "success");
                    var pageSize = $("#mypager").pager("GetPageSize");
                    AllocateManager.PageClick(1, pageSize);
                });
            } else if (v == "3") {
                //打印
            }
        }
        var load = function (h) {
            var param = {};
            param["SnNum"] = SnNum;
            var Server = AllocateManager.Server();
            Server.GetDetail(param, function (result) {
                h.find('#tabDetail').DataTable({
                    destroy: true,
                    data: result.Result,
                    paging: false,
                    searching: false,
                    scrollX: false,
                    scrollY: "165px",
                    bAutoWidth: false,
                    bInfo: false,
                    ordering: false,
                    columns: [
                        { data: 'ProductName' },
                        { data: 'BarCode' },
                        { data: 'BatchNum' },
                        {
                            data: 'Num', render: function (data, type, full, meta) {
                                return git.ToDecimal(data, 2);
                            }
                        },
                        { data: 'FromLocalName' ,render:function(data,type,full,meta){
                            return full.FromStorageName+"."+data;
                        }},
                        { data: 'ToStorageName' },
                    ],
                    oLanguage: {
                        sEmptyTable: "没有查询到任何数据"
                    }
                });
            });
        }

        if (Command == "View") {
            $.jBox.open("get:/Order/Allocate/Detail?SnNum=" + SnNum, "调拨单详细", 850, 450, {
                buttons: { "关闭": 4 }, submit: submit, loaded: function (h) {
                    load(h);
                }
            });
        } else if (Command == "Audite") {
            $.jBox.open("get:/Order/Allocate/Detail?SnNum=" + SnNum, "调拨单详细", 850, 450, {
                buttons: { "审核通过": 1, "审核不通过": 2, "打印": 3, "关闭": 4 }, submit: submit, loaded: function (h) {
                    load(h);
                }
            });
        }
    },
    ToolBar: function () {
        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function () {
            var command = $(this).attr("data-command");

            if (command == "Add") {
                window.location.href = "/Order/Allocate/Add";
            } else if (command == "Edit") {
                var list = AllocateManager.GetRows();
                if (list.length == 0) {
                    $.jBox.tip("请选择要编辑的项", "warn");
                    return false;
                }
                var item=list[0];
                if (item.Status == EAuditeJson.Wait || item.Status == EAuditeJson.NotPass) {
                    var SN = item.OrderSnNum;
                    window.location.href = "/Order/Allocate/Add?SnNum=" + SN;
                }else{
                    $.jBox.tip("该项不能编辑","warn");
                }
            } else if (command == "Delete") {
                var submit = function (v, h, f) {
                    if (v == "ok") {
                        var list = AllocateManager.GetSelect();
                        if (list.length == 0) {
                            $.jBox.tip("请选择要删除的项", "warn");
                            return false;
                        }
                        var param = {};
                        param["list"] = JSON.stringify(list);
                        var Server = AllocateManager.Server();
                        Server.Delete(param, function (result) {
                            $.jBox.tip(result.Message, "success");
                            var pageSize = $("#mypager").pager("GetPageSize");
                            AllocateManager.PageClick(1, pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            } else if (command == "Excel") {
                var Server = AllocateManager.Server();
                var search = AllocateManager.GetSearch();
                Server.ToExcel(search, function (result) {

                    if (result.Code == 1000) {
                        var path = unescape(result.Message);
                        window.location.href = path;
                    } else {
                        $.jBox.info(result.Message, "提示");
                    }
                });
            } else if (command == "Refresh") {
                AllocateManager.Refresh();
            }

        });

        //搜索 高级搜索按钮
        var searchBar = $("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function () {
            AllocateManager.PageClick(1, 10);
        });
        searchBar.find("button[data-command='Advanced']").click(function () {
            var display = $("div.formsearch").css("display");
            if (display == "block") {
                $("div.formsearch").hide(300);
            } else {
                $("div.formsearch").show(300);
            }
        });
        searchBar.find("button[data-command='Clear']").click(function () {
            //清空搜索条件
            searchBar.find("input[name='OrderNum']").val("");
            searchBar.find("input[name='ContractOrder']").val("");
            searchBar.find("input[name='BeginTime']").val("");
            searchBar.find("input[name='EndTime']").val("");
            searchBar.find("select[name='AllocateType']").val("");
        });


        //状态按钮处理
        $("div[data-group='Status']").find("button").click(function () {
            $("div[data-group='Status']").find("button").removeClass("disabled");
            $(this).addClass("disabled");
            AllocateManager.PageClick(1, 10);
        });

        //加载默认数据
        AllocateManager.PageClick(1, 10);
    }
}



var AllocateOrder = {
    Init: function () {

        //初始化绑定事件
        $("div.formbtn").find("button.btn").click(function (event) {
            var command = $(this).attr("data-command");
            if (command == "Save") {
                var param = AllocateOrder.GetModel();
                
                if(!param){
                    return false;
                }
                var Server = AllocateManager.Server();
                Server.Add(param, function (result) {
                    if (result.Code == 1) {
                        window.location.href = "/Order/Allocate/List";
                    } else {
                        $.jBox.tip(result.Message);
                    }
                });
            } else if (command == "Cancel") {
                window.location.href = "/Order/Allocate/List";
            }
        });
        
        //初始化选择库存产品事件
        AllocateOrder.AddProduct();

        AllocateOrder.PageClick(1, 5);
    },
    PageClick: function (PageIndex, PageSize) {
        $.jBox.tip("正在努力加载数据...","loading");
        var Server = AllocateManager.Server();
        var search = AllocateManager.GetSearch();
        search["PageIndex"] = PageIndex;
        search["PageSize"] = PageSize;
        Server.LoadDetail(search, function (result) {
            AllocateOrder.SetTable(result);
            $.jBox.closeTip();
        });
    },
    SetTable: function (result) {
        $("#tabList").DataTable({
            destroy: true,
            data: result.Result,
            paging: false,
            searching: false,
            scrollX: false,
            bAutoWidth: false,
            bInfo: false,
            ordering: false,
            columns: [
                {
                    data: 'SnNum', render: function (data, type, full, meta) {
                        return "<input type='checkbox' name='Allocate_item' value='" + data + "' data-full='" + JSON.stringify(full) + "'/>";
                    }
                },
                { data: 'ProductName' },
                { data: 'Size' },
                { data: 'BarCode' },
                { data: 'BatchNum' },
                {
                    data: 'Num', render: function (data, type, full, meta) {
                        return git.ToDecimal(data, 2);
                    }
                },
                { data: 'FromLocalName',render:function(data,type,full,meta){
                    return full.FromStorageName+"."+full.FromLocalName;
                } },
                {
                    data: "ID", render: function (data, type, full, meta) {
                        var html = "";
                        html += '<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                        return html;
                    }
                }
            ],
            aoColumnDefs: [
                { "sWidth": "15px", "aTargets": [0] }
            ],
            oLanguage: {
                sEmptyTable: "没有查询到任何数据"
            }
        });

        var pageInfo = result.PageInfo;
        if (pageInfo != undefined) {
            $("#mypager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: AllocateOrder.PageClick });
        }
        AllocateOrder.BindTableEvent();
    },
    BindTableEvent: function () {
        $("#tabList").find("a.delete").click(function () {
            var SN = $(this).parent().parent().find("input[name='Allocate_item']").val();
            var submit = function (v, h, f) {
                if (v == "ok") {
                    var param = {};
                    param["SnNum"] = SN;
                    var Server = AllocateManager.Server();
                    Server.DelProduct(param, function (result) {
                        $.jBox.tip(result.Message, "success");
                        var pageSize = $("#mypager").pager("GetPageSize");
                        AllocateOrder.PageClick(1, pageSize);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });
    },
    AddProduct: function (SnNum) {
        $("div.formbtn").find("button.btn[data-command='Add']").LocalProduct({Mult:true,title:"请选择调拨产品",callBack:function(result){
            
            if(result!=undefined && result.length>0){
                var list=[];
                for(var i=0;i<result.length;i++){
                    var item={};
                    item["ProductName"]=result[i].ProductName;
                    item["BarCode"]=result[i].BarCode;
                    item["ProductNum"]=result[i].ProductNum;
                    item["BatchNum"]=result[i].BatchNum;
                    item["FromStorageNum"]=result[i].StorageNum;
                    item["FromStorageName"]=result[i].StorageName;
                    item["FromLocalNum"]=result[i].LocalNum;
                    item["FromLocalName"]=result[i].LocalName;
                    item["Num"]=result[i].Qty;
                    item["Size"]=result[i].Size;
                    
                    list.push(item)
                }
                var param={};
                param["List"]=JSON.stringify(list);
                var Server = AllocateManager.Server();
                Server.AddProduct(param,function(msg){
                    AllocateOrder.PageClick(1, 5);
                });
            }
        }});
    },
    GetModel: function () {
        var SnNum = $("input[name='SnNum']").val();
        var OrderNum = $("input[name='OrderNum']").val();
        var ContractOrder = $("input[name='ContractOrder']").val();
        var CreateTime = $("input[name='CreateTime']").val();
        var CreateUser = $("input[name='CreateUser']").val();
        var Remark = $("input[name='Remark']").val();
        var StorageNum=$("select[name='StorageNum']").val();
        
        //校验
        if(git.IsEmpty(StorageNum)){
            $.jBox.tip("请选择调拨仓库","warn");
            return false;
        }

        var param = {};
        param["SnNum"] = SnNum;
        param["OrderNum"] = OrderNum;
        param["ContractOrder"] = ContractOrder;
        param["CreateUser"] = CreateUser;
        param["Remark"] = Remark;

        var entity = {};
        entity["Entity"] = JSON.stringify(param);
        entity["CreateTime"] = CreateTime;
        entity["StorageNum"] = StorageNum;
        return entity;
    }
}