
/**
*
*销售订单管理功能
*
**/

var SaleManager = {
    TabGrid:undefined,
    Server: function () {
        var config = (function () {
            var URL_Add = "/Biz/SaleAjax/Add";
            var URL_LoadDetail = "/Biz/SaleAjax/LoadDetail";
            var URL_AddProduct = "/Biz/SaleAjax/AddProduct";
            var URL_DelProduct = "/Biz/SaleAjax/DelProduct";

            var URL_GetList = "/Biz/SaleManagerAjax/GetList";
            var URL_GetDetail = "/Biz/SaleManagerAjax/GetDetail";
            var URL_Delete = "/Biz/SaleManagerAjax/Delete";
            var URL_Cancel = "/Biz/SaleManagerAjax/Cancel";
            var URL_Audite = "/Biz/SaleManagerAjax/Audite";
            var URL_ToFiance = "/Biz/SaleManagerAjax/ToFiance";
            var URL_ToStorage = "/Biz/SaleManagerAjax/ToStorage";
            var URL_ToReturn = "/Biz/SaleManagerAjax/ToReturn";
            var URL_ToExcel = "/Biz/SaleManagerAjax/ToExcel";

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
                URL_ToFiance: URL_ToFiance,
                URL_ToStorage: URL_ToStorage,
                URL_ToReturn:URL_ToReturn,
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

            var ToFiance=function(data,callback){
                $.gitAjax({
                    url: config.URL_ToFiance,
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

            var ToStorage=function(data,callback){
                $.gitAjax({
                    url: config.URL_ToStorage,
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

            var ToReturn=function(data,callback){
                $.gitAjax({
                    url: config.URL_ToReturn,
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
                ToFiance: ToFiance,
                ToStorage: ToStorage,
                ToReturn: ToReturn,
                ToExcel: ToExcel,
            }

        })($, config);
        return dataServer;
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=SaleManager.Server();
        var search=SaleManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            $.jBox.closeTip();
            if(result.Code==1){
                SaleManager.SetTable(result);
            }else{
                $.jBox.tip(result.Message,"warn");
            }
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=SaleManager.Server();
        var search=SaleManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            $.jBox.closeTip();
            if(result.Code==1){
                SaleManager.SetTable(result);
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
            {title:'产品规格', name:'Size', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'单价', name:'Price', width: 55, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'单位', name:'UnitName', width: 55, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'数量', name:'Num', width: 55, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'出库数', name:'RealNum', width: 55, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'总额', name:'Amount', width: 55, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'客户单号', name:'CusOrderNum', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'客户编号', name:'CusNum', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'客户名', name:'CusName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'订单总额', name:'OrderAmount', width: 60, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'状态', name:'OrderStatus', width: 60, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                if(data==EOrderStatusJson.CreateOrder){
                    return '<span class="label label-important">'+git.GetEnumDesc(EOrderStatus,data)+'</span>';
                }else{
                    return git.GetEnumDesc(EOrderStatus,data);
                }
            }},
            {title:'是否入账', name:'AuditeStatus', width: 60, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                if(data==EBoolJson.Yes){
                    return '<span class="label label-success"><i class="icon-ok"></i>&nbsp;&nbsp;'+git.GetEnumDesc(EBool,data)+'</span>';
                }else{
                    return git.GetEnumDesc(EBool,data);
                }
            }},
            {title:'退货', name:'HasReturn', width: 45, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                if(data==EBoolJson.Yes){
                    return '<span class="label label-success">退</span>';
                }else{
                    return "";
                }
            }},
            {title:'退货数', name:'ReturnNum', width: 50, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'创建时间', name:'CreateTime', width: 75, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.JsonToDateTime(data);
            }},
            {title:'操作', name:'ID', width: 150, align: 'left',lockWidth:false,  renderer: function(data,item,rowIndex){
                var html="";
                if(item.OrderStatus==EOrderStatusJson.CreateOrder){
                    html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;';
                }
                if(item.OrderStatus==EOrderStatusJson.CreateOrder){
                    html+='<a class="audite" href="javascript:void(0)">确认</a>&nbsp;';
                    html+='<a class="cancel" href="javascript:void(0)">取消</a>&nbsp;';
                }

                if(item.OrderStatus==EOrderStatusJson.OrderConfirm || item.OrderStatus==EOrderStatusJson.PartialDelivery){
                    html+='<a class="out" href="javascript:void(0)">出库</a>&nbsp;';
                }

                if(item.AuditeStatus==EBoolJson.No){
                    html+='<a class="pay" href="javascript:void(0)">入账</a>&nbsp;';
                }
		        if(item.OrderStatus==EOrderStatusJson.PartialDelivery || item.OrderStatus==EOrderStatusJson.AllDelivery){
                    html+='<a class="return" href="javascript:void(0)">退货</a>&nbsp;';
                }
                html+='<a class="view" href="javascript:void(0)">查看</a>&nbsp;';
                html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                return html;
            }},
        ];

        if(this.TabGrid==undefined){
            this.TabGrid=$("#tabList").mmGrid({
                cols:cols,
                items:result.Result,
                checkCol:true,
                nowrap:true,
                height:450
            });
            //绑定事件
            SaleManager.BindEvent();
        }else{
            this.TabGrid.load(result.Result);
        }
        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: SaleManager.PageClick });
        }
    },
    BindEvent:function(){
        this.TabGrid.on("cellSelected",function(e, item, rowIndex, colIndex){
            if($(e.target).is("a.edit")){
                //--编辑
                var SN=item.OrderSnNum;
                window.location.href="/Biz/Sale/Add?SnNum="+SN;
            }else if($(e.target).is("a.audite")){
                //--确认
                var SN=item.OrderSnNum;
                var param={};
                param["SnNum"]=SN;
                param["Status"]=2;
                param["Reason"]="";
                var Server=SaleManager.Server();
                Server.Audite(param,function(result){
                    if(result.Code==1){
                        SaleManager.Refresh();
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });
            }else if($(e.target).is("a.cancel")){
                //--取消销售订单
                var SN=item.OrderSnNum;
                var param={};
                param["SnNum"]=SN;
                param["Status"]=3;
                param["Reason"]="";
                var Server=SaleManager.Server();
                Server.Audite(param,function(result){
                    if(result.Code==1){
                        SaleManager.Refresh();
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });
            }else if($(e.target).is("a.delete")){
                //--删除销售订单
                var SN=item.OrderSnNum;
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=[];
                        list.push(SN);
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=SaleManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                SaleManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"warn");
                            }
                        });
                    }
                }
                $.jBox.confirm("该操作将删除所有关联数据,确定要删除吗？", "提示", submit);
            }else if($(e.target).is("a.out")){
                //--销售单出库
                var SN=item.OrderSnNum;
                SaleManager.OutStorage(SN);
            }else if($(e.target).is("a.pay")){
                //--销售单生成财务账
                var SN=item.OrderSnNum;
                var Server=SaleManager.Server();
                var param={};
                param["SnNum"]=SN;
                Server.ToFiance(param,function(result){
                    if(result.Code==1){
                        SaleManager.Refresh();
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });
            }else if($(e.target).is("a.view")){
                //--采购单查看详细
                var SN=item.OrderSnNum;
                SaleManager.Detail(SN,"View");
            }else if($(e.target).is("a.return")){
                //--销售退货
                var SN=item.OrderSnNum;
                SaleManager.ToReturn(SN,"Return");
            }
        });
    },
    GetSelect:function(){
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
    GetRows:function(){
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
    GetSearch:function(){
        var searchBar=$("div[data-condition='search']");

        var OrderNum=searchBar.find("input[name='OrderNum']").val();
        var OrderType=searchBar.find("select[name='OrderType']").val();
        var CusName=searchBar.find("input[name='CusName']").val();
        var Phone=searchBar.find("input[name='Phone']").val();
        var Status=$("div[data-group='Status']").find("button.disabled").attr("value");
        var BarCode=searchBar.find("input[name='BarCode']").val();
        var ProductName=searchBar.find("input[name='ProductName']").val();
        var CusOrderNum=searchBar.find("input[name='CusOrderNum']").val();
        var BeginTime=searchBar.find("input[name='BeginTime']").val();
        var EndTime=searchBar.find("input[name='EndTime']").val();

        var search={};
        search["OrderNum"]=OrderNum;
        search["OrderType"]=OrderType;
        search["CusName"]=CusName;
        search["Phone"]=Phone;
        search["Status"]=Status;
        search["BarCode"]=BarCode;
        search["ProductName"]=ProductName;
        search["CusOrderNum"]=CusOrderNum;
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
                var Server=SaleManager.Server();
                Server.Audite(param,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        SaleManager.PageClick(1,pageSize);
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
                var Server=SaleManager.Server();
                Server.Audite(param,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        SaleManager.PageClick(1,pageSize);
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
            var Server=SaleManager.Server();
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
                        { data: 'Price',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }},
                        { data: 'UnitName'},
                        { data: 'Num',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }},
                        { data: 'Amount',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }}
                    ],
                    oLanguage:{
                        sEmptyTable:"没有查询到任何数据"
                    }
                });
            });
        }

        if(Command=="View"){
            $.jBox.open("get:/Biz/Sale/Detail?SnNum="+SnNum, "销售订单详细", 850, 450, { buttons: {"关闭":4 } ,submit:submit,loaded:function(h){
                load(h);
            }}); 
        }else if(Command=="Audite"){
            $.jBox.open("get:/Biz/Sale/Detail?SnNum="+SnNum, "销售订单详细", 850, 450, { buttons: {"审核通过": 1, "审核不通过": 2,"打印":3,"关闭":4 } ,submit:submit,loaded:function(h){
                load(h);
            }}); 
        }
    },
    OutStorage:function(SnNum){

        var submit=function(v,h,f){
            if(v=="1"){
                var list=[];
                h.find('input[name="out_item"]').each(function(index, el) {
                    var flag=$(el).attr("checked");
                    if(flag){
                        var tr=$(el).parent().parent();
                        var Qty=$(tr).find("input[name='Qty']").val();
                        var item=JSON.parse($(el).attr("data-full"));
                        item["Qty"]=Qty;
                        list.push(item);
                    }
                });

                var Server=SaleManager.Server();
                var param={};
                param["List"]=JSON.stringify(list);
                param["SnNum"]=SnNum;
                Server.ToStorage(param,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        SaleManager.PageClick(1,pageSize);
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                }); 
            }
        }

        var load=function(h){

            var loadLocal=function(){
                //如果已经入库的产品不需要再入库
                h.find('input[name="Qty"]').each(function(index, el) {
                    var Qty=$(el).val();
                    if(parseFloat(Qty)==0){
                        var parent=$(this).parent().parent();
                        $(parent).find("input").attr("disabled",true);
                    }
                });
            }

            var param={};
            param["SnNum"]=SnNum;
            var Server=SaleManager.Server();
            Server.GetDetail(param,function(result){
                h.find('#tabDetail').DataTable({
                    destroy: true,
                    data:result.Result,
                    paging:false,
                    searching:false,
                    scrollX: "100%",
                    scrollY: "165px",
                    bAutoWidth:false,
                    bInfo:false,
                    ordering:false,
                    columns: [
                        { data: 'SnNum' ,render:function(data, type, full, meta){
                            return "<input type='checkbox' name='out_item' value='"+data+"' data-full='"+JSON.stringify(full)+"'/>";
                        }},
                        { data: 'ProductName'},
                        { data: 'BarCode'},
                        { data: 'Num',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }},
                        { data: 'Qty',render:function(data,type,full,meta){
                            var Qty=parseFloat(full.Num)-parseFloat(full.RealNum);

                            return '<input type="text" name="Qty" class="m-wrap input-small" value="'+ git.ToDecimal(Qty,2) +'">';
                        }},
                        { data: 'Size'},
                        { data: 'Amount',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }},
                        { data: 'Price',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }},
                        { data: 'UnitName'}
                    ],
                    oLanguage:{
                        sEmptyTable:"没有查询到任何数据"
                    },
                    aoColumnDefs:[
                        { "sWidth": "27px",  "aTargets": [0] },
                        { "sWidth": "60px",  "aTargets": [4] }
                    ],
                });

                //加载库位选择
                loadLocal();
            });
        }

        $.jBox.open("get:/Biz/Sale/OutStorage?SnNum="+SnNum, "销售出库", 850, 450, { buttons: {"确定":1,"关闭":4 } ,submit:submit,loaded:function(h){
            load(h);
        }}); 
    },
    ToReturn:function(SnNum){

        var submit=function(v,h,f){
            if(v=="1"){
                var list=[];
                h.find('input[name="return_item"]').each(function(index, el) {
                    var flag=$(el).attr("checked");
                    if(flag){
                        var tr=$(el).parent().parent();
                        var Qty=$(tr).find("input[name='Qty']").val();
                        var item=JSON.parse($(el).attr("data-full"));
                        item["Qty"]=Qty;
                        list.push(item);
                    }
                });

                var Server=SaleManager.Server();
                var param={};
                param["List"]=JSON.stringify(list);
                param["SnNum"]=SnNum;
                Server.ToReturn(param,function(result){
                    if(result.Code==1){
                        // var pageSize=$("#mypager").pager("GetPageSize");
                        // SaleManager.PageClick(1,pageSize); 
                        $.jBox.tip(result.Message,"success");
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                }); 
            }
        }

        var load=function(h){

            var loadLocal=function(){
                
            }
            var param={};
            param["SnNum"]=SnNum;
            var Server=SaleManager.Server();
            Server.GetDetail(param,function(result){
                h.find('#tabDetail').DataTable({
                    destroy: true,
                    data:result.Result,
                    paging:false,
                    searching:false,
                    scrollX: "100%",
                    scrollY: "165px",
                    bAutoWidth:false,
                    bInfo:false,
                    ordering:false,
                    columns: [
                        { data: 'SnNum' ,render:function(data, type, full, meta){
                            return "<input type='checkbox' name='return_item' value='"+data+"' data-full='"+JSON.stringify(full)+"'/>";
                        }},
                        { data: 'ProductName'},
                        { data: 'BarCode'},
                        { data: 'Size'},
                        { data: 'Price',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }},
                        { data: 'UnitName'},
                        { data: 'Num',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }},
                        { data: 'Qty',render:function(data,type,full,meta){
                            var Qty=parseFloat(full.RealNum)-parseFloat(full.ReturnNum);

                            return '<input type="text" name="Qty" class="m-wrap input-small" value="'+ git.ToDecimal(Qty,2) +'">';
                        }},
                        { data: 'Amount',render:function(data,type,full,meta){
                            return git.ToDecimal(data,2);
                        }},
                    ],
                    oLanguage:{
                        sEmptyTable:"没有查询到任何数据"
                    },
                    aoColumnDefs:[
                        { "sWidth": "27px",  "aTargets": [0] },
                        { "sWidth": "60px",  "aTargets": [4] }
                    ],
                });

                //加载库位选择
                loadLocal();
            });
        }

        $.jBox.open("get:/Biz/Sale/ToReturn?SnNum="+SnNum, "销售退货", 850, 450, { buttons: {"确定":1,"关闭":4 } ,submit:submit,loaded:function(h){
            load(h);
        }}); 
    },
    ToolBar:function(){
        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                window.location.href="/Biz/Sale/Add";
            }else if(command=="Edit"){
                var list=SaleManager.GetRows();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var item=list[0];
                if(item.OrderStatus==EOrderStatusJson.CreateOrder){
                    var SN=item.OrderSnNum;
                    window.location.href="/Biz/Sale/Add?SnNum="+SN;
                }else{
                    $.jBox.tip("该订单不能编辑","warn");
                }
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=SaleManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=SaleManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                SaleManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"warn");
                            }
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=SaleManager.Server();
                var search=SaleManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                SaleManager.Refresh();
            }

        });

        //搜索 高级搜索按钮
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            SaleManager.PageClick(1,10);
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
            searchBar.find("input[name='OrderType']").val("");
            searchBar.find("input[name='CusName']").val("");
            searchBar.find("input[name='Phone']").val("");
            searchBar.find("input[name='BarCode']").val("");
            searchBar.find("input[name='ProductName']").val("");
            searchBar.find("input[name='CusOrderNum']").val("");
            searchBar.find("input[name='BeginTime']").val("");
            searchBar.find("input[name='EndTime']").val("");
        });


        //状态按钮处理
        $("div[data-group='Status']").find("button").click(function(){
            $("div[data-group='Status']").find("button").removeClass("disabled");
            $(this).addClass("disabled");
            SaleManager.PageClick(1,10);
        });
        
        //加载默认数据
        SaleManager.PageClick(1,10);
    }
}



var SaleOrder={
    Init:function(){

        //初始化绑定事件
        $("div.formbtn").find("button.btn").click(function(event){
            var command=$(this).attr("data-command");
            if(command=="Add"){
                
            }else if(command=="Save"){

                var param=SaleOrder.GetModel();
                if(!param){
                    return false;
                }
                var Server=SaleManager.Server();
                Server.Add(param,function(result){
                    if(result.Code==1){
                        window.location.href="/Biz/Sale/List";
                    }else{
                        $.jBox.tip(result.Message);
                    }
                });

            }else if(command=="Cancel"){
                window.location.href="/Biz/Sale/List";
            }
        });

        //选择客户
        $("input[name='CusNum']").CustomerDialog({
            Mult: false, callBack: function (result) {
                if (result != undefined) {
                    $("input[name='CusSnNum']").val(result.SnNum);
                    $("input[name='CusNum']").val(result.CusNum);
                    $("input[name='CusName']").val(result.CusName);
                    $("input[name='Address']").val(result.Address);
                    $("input[name='Phone']").val(result.Phone);
                    $("input[name='CusName']").val(result.Contact);
                }
            }
        });

        //自动提示客户搜索
        $("input[name='CusNum']").autocomplete({
            paramName: "KeyWord",
            url: '/Client/CustomerAjax/AutoCustomer',
            showResult: function (value, data) {
                var row = JSON.parse(value);
                return '<span>' + row.CusNum + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + row.CusName + '</span>';
            },
            onItemSelect: function (item) {

            },
            maxItemsToShow: 5,
            selectedCallback: function (selectItem) {
                $("input[name='CusSnNum']").val(selectItem.SnNum);
                $("input[name='CusNum']").val(selectItem.CusNum);
                $("input[name='CusName']").val(selectItem.CusName);
                $("input[name='Address']").val(selectItem.Address);
                $("input[name='Phone']").val(selectItem.Phone);
                $("input[name='Contact']").val(selectItem.Contact);
            }
        });
        
        SaleOrder.PageClick(1,5);

        //初始化
        SaleOrder.AddProduct();
    },
    PageClick:function(PageIndex,PageSize){
        var Server=SaleManager.Server();
        var search=SaleManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.LoadDetail(search,function(result){
            if(result.Code==1){
                SaleOrder.SetTable(result);
            }else{
                $.jBox.tip(result.Message,"warn");
            }
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
                    return "<input type='checkbox' name='sale_item' value='"+data+"' data-full='"+JSON.stringify(full)+"'/>";
                }},
                { data: 'ProductName'},
                { data: 'BarCode'},
                { data: 'Size'},
                { data: 'Price',render:function(data,type,full,meta){
                    return git.ToDecimal(data,2);
                }},
                { data: 'UnitName'},
                { data: 'Num',render:function(data,type,full,meta){
                    return git.ToDecimal(data,2);
                }},
                { data: 'Amount',render:function(data,type,full,meta){
                    return git.ToDecimal(data,2);
                }},
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
            $("#mypager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: SaleOrder.PageClick });
        }
        SaleOrder.BindTableEvent();
    },
    BindTableEvent:function(){
        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='sale_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var param={};
                    param["SnNum"]=SN;
                    var Server=SaleManager.Server();
                    Server.DelProduct(param,function(result){
                        if(result.Code==1){
                            var pageSize=$("#mypager").pager("GetPageSize");
                            SaleOrder.PageClick(1,pageSize);
                        }else{
                            $.jBox.tip(result.Message,"warn");
                        }
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });
    },
    AddProduct:function(SnNum){
        $("div.formbtn").find("button.btn[data-command='Add']").SaleProduct({Mult:true,EventName:"click",callBack:function(result){
            
            if(result!=undefined && result.length>0){
                var list=[];
                for(var i=0;i<result.length;i++){
                    var entity={};
                    entity["ProductName"]=result[i].ProductName;
                    entity["BarCode"]=result[i].BarCode;
                    entity["ProductNum"]=result[i].ProductNum;
                    entity["Num"]=result[i].Qty;
                    entity["Price"]=result[i].AvgPrice;
                    entity["Size"]=result[i].Size;
                    entity["UnitNum"]=result[i].UnitNum;
                    entity["UnitName"]=result[i].UnitName;

                    list.push(entity);
                }
                console.log(JSON.stringify(list));
                var Server=SaleManager.Server();
                var param={};
                param["List"]=JSON.stringify(list);
                Server.AddProduct(param,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        SaleOrder.PageClick(1,pageSize);
                    }else{
                        $.jBox.tip(result.Message,"warn");
                    }
                });
            }
        }});
    },
    GetModel:function(){
        var SnNum=$("input[name='SnNum']").val();
        var OrderNum=$("input[name='OrderNum']").val();
        var OrderType=$("select[name='OrderType']").val();
        var ContractOrder=$("input[name='ContractOrder']").val();
        var CreateUser=$("input[name='CreateUser']").val();
        var CusNum=$("input[name='CusNum']").val();
        var CusName=$("input[name='CusName']").val();
        var CusSnNum=$("input[name='CusSnNum']").val();
        var Contact=$("input[name='Contact']").val();
        var Phone=$("input[name='Phone']").val();
        var OrderTime=$("input[name='OrderTime']").val();
        var SendDate=$("input[name='SendDate']").val();
        var Remark=$("input[name='Remark']").val();

        //校验
        if(git.IsEmpty(OrderType)){
            $.jBox.tip("请选择销售单类型","warn");
            return false;
        }

        if(git.IsEmpty(CusSnNum)){
            $.jBox.tip("请选择客户","warn");
            return false;
        }

        var param={};
        param["SnNum"]=SnNum;
        param["OrderNum"]=OrderNum;
        param["OrderType"]=OrderType;
        param["ContractOrder"]=ContractOrder;
        param["CreateUser"]=SnNum;
        param["CusNum"]=CusNum;
        param["CusName"]=CusName;
        param["CusSnNum"]=CusSnNum;
        param["Contact"]=Contact;
        param["Phone"]=Phone;
        param["Remark"]=Remark;

        var entity={};
        entity["Entity"]=JSON.stringify(param);
        entity["SendDate"]=SendDate;
        entity["OrderTime"]=OrderTime;

        return entity;
    }
}