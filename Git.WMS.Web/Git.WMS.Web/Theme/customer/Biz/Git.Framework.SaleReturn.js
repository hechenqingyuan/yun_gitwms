
var SaleReturnManager = {
    TabGrid:undefined,
    Server: function () {
        var config = (function () {

            var URL_GetList = "/Biz/SaleReturnManagerAjax/GetList";
            var URL_Delete = "/Biz/SaleReturnManagerAjax/Delete";
            var URL_Cancel = "/Biz/SaleReturnManagerAjax/Cancel";
            var URL_Audite = "/Biz/SaleReturnManagerAjax/Audite";
            var URL_GetDetail = "/Biz/SaleReturnManagerAjax/GetDetail";
            var URL_ToExcel = "/Biz/SaleReturnManagerAjax/ToExcel";

            return {
                URL_GetList: URL_GetList,
                URL_Delete: URL_Delete,
                URL_Cancel: URL_Cancel,
                URL_Audite: URL_Audite,
                URL_GetDetail: URL_GetDetail,
                URL_ToExcel: URL_ToExcel,
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
                Cancel: Cancel,
                Audite: Audite,
                GetDetail: GetDetail,
                ToExcel: ToExcel,
            }

        })($, config);
        return dataServer;
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=SaleReturnManager.Server();
        var search=SaleReturnManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            $.jBox.closeTip();
            if(result.Code==1){
                SaleReturnManager.SetTable(result);
            }else{
                $.jBox.tip(result.Message,"warn");
            }
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=SaleReturnManager.Server();
        var search=SaleReturnManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            $.jBox.closeTip();
            if(result.Code==1){
                SaleReturnManager.SetTable(result);
            }else{
                $.jBox.tip(result.Message,"warn");
            }
        });
    },
    SetTable:function(result){
        var cols=[
            {title:'退货单号', name:'OrderNum', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
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
            {title:'单价', name:'Price', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.ToDecimal(data,2);
            }},
            {title:'退货数', name:'ReturnNum', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'单位', name:'UnitName', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'总额', name:'Amount', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.ToDecimal(data,2);
            }},
            {title:'销售单号', name:'SaleOrderNum', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'客户名称', name:'CusName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return data;
            }},
            {title:'状态', name:'Status', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                
                return git.GetEnumDesc(ESaleReturnStatus,data);
            }},
            {title:'创建时间', name:'CreateTime', width: 75, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                return git.JsonToDateTime(data);
            }},
            {title:'操作', name:'ID', width: 150, align: 'left',lockWidth:false,  renderer: function(data,item,rowIndex){
                var html="";
                html+='<a class="view" href="javascript:void(0)">查看</a>&nbsp;';
                
                if(ESaleReturnStatusJson.CreateOrder==item.Status){
                    html+='<a class="audite" href="javascript:void(0)">确认</a>&nbsp;';
                    html+='<a class="cancel" href="javascript:void(0)">取消</a>&nbsp;';
                    html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                }
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
            SaleReturnManager.BindEvent();
        }else{
            this.TabGrid.load(result.Result);
        }
        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: SaleReturnManager.PageClick });
        }
    },
    BindEvent:function(){
        this.TabGrid.on("cellSelected",function(e, item, rowIndex, colIndex){
            if($(e.target).is("a.edit")){
            }else if($(e.target).is("a.audite")){
                //--确认
                var SN=item.OrderSnNum;
                var param={};
                param["SnNum"]=SN;
                param["Status"]=2;
                param["Reason"]="";
                var Server=SaleReturnManager.Server();
                Server.Audite(param,function(result){
                    if(result.Code==1){
                        SaleReturnManager.Refresh();
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
                var Server=SaleReturnManager.Server();
                Server.Audite(param,function(result){
                    if(result.Code==1){
                        SaleReturnManager.Refresh();
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
                        var Server=SaleReturnManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                SaleReturnManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"success");
                            }
                        });
                    }
                }
                $.jBox.confirm("该操作将删除所有关联数据,确定要删除吗？", "提示", submit);
            }else if($(e.target).is("a.view")){
                //--采购单查看详细
                var SN=item.OrderSnNum;
                SaleReturnManager.Detail(SN,"View");
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

        var Status=$("div[data-group='Status']").find("button.disabled").attr("value");
        var OrderNum=searchBar.find("input[name='OrderNum']").val();
        var SaleOrderNum=searchBar.find("input[name='SaleOrderNum']").val();
        var BarCode=searchBar.find("input[name='BarCode']").val();
        var ProductName=searchBar.find("input[name='ProductName']").val();
        var CusNum=searchBar.find("input[name='CusNum']").val();
        var CusName=searchBar.find("input[name='CusName']").val();
        var BeginTime=searchBar.find("input[name='BeginTime']").val();
        var EndTime=searchBar.find("input[name='EndTime']").val();

        var search={};
        search["OrderNum"]=OrderNum;
        search["SaleOrderNum"]=SaleOrderNum;
        search["BarCode"]=BarCode;
        search["ProductName"]=ProductName;
        search["CusNum"]=CusNum;
        search["CusName"]=CusName;
        search["BeginTime"]=BeginTime;
        search["EndTime"]=EndTime;
        search["Status"]=Status;

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
                var Server=SaleReturnManager.Server();
                Server.Audite(param,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        SaleReturnManager.PageClick(1,pageSize);
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
                var Server=SaleReturnManager.Server();
                Server.Audite(param,function(result){
                    if(result.Code==1){
                        var pageSize=$("#mypager").pager("GetPageSize");
                        SaleReturnManager.PageClick(1,pageSize);
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
            var Server=SaleReturnManager.Server();
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
                        { data: 'ReturnNum',render:function(data,type,full,meta){
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
            $.jBox.open("get:/Biz/SaleReturn/Detail?SnNum="+SnNum, "销售退货单详细", 850, 450, { buttons: {"关闭":4 } ,submit:submit,loaded:function(h){
                load(h);
            }}); 
        }else if(Command=="Audite"){
            $.jBox.open("get:/Biz/SaleReturn/Detail?SnNum="+SnNum, "销售退货单详细", 850, 450, { buttons: {"审核通过": 1, "审核不通过": 2,"打印":3,"关闭":4 } ,submit:submit,loaded:function(h){
                load(h);
            }}); 
        }
    },
    Init:function(){
        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                window.location.href="/Biz/Sale/Add";
            }else if(command=="Edit"){
                var list=SaleReturnManager.GetRows();
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
                        var list=SaleReturnManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=SaleReturnManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                SaleReturnManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"success");
                            }
                        });
                    }
                }
                $.jBox.confirm("该操作将删除所有关联数据,确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=SaleReturnManager.Server();
                var search=SaleReturnManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                SaleReturnManager.Refresh();
            }

        });

        //搜索 高级搜索按钮
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            SaleReturnManager.PageClick(1,10);
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
            SaleReturnManager.PageClick(1,10);
        });
        
        //加载默认数据
        SaleReturnManager.PageClick(1,10);
    }
}

