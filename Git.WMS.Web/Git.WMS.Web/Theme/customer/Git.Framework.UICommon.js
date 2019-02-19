
$(document).ready(function () {
    
    var height = $(document).height();
    height = parseInt(height) - 80;
    $("#body").css("min-height", height);

    masterUI.Ad();

    masterUI.ChangeStorage();
});





/********************************************左侧菜单***********************************************/
var masterUI = {
    ToggleMenu: function () {
        //用于左侧菜单展开以及显示
        $(".sidebar-toggler").click(function () {
            var MenuStatus = "open";
            if (!$("#container").hasClass("sidebar-closed")) {
                $("#container").addClass("sidebar-closed");
                MenuStatus = "close";
            } else {
                $("#container").removeClass("sidebar-closed");
                MenuStatus = "open";
            }
            var param = {};
            param["MenuStatus"] = MenuStatus;
            $.gitAjax({
                url: "/Common/SetMenuStatus", type: "post", data: param, success: function (result) {
                }
            });
        });

        //右侧菜单点击操作
        $("#sidebar ul .has-sub").click(function () {
            var index = $("#sidebar ul .has-sub").index($(this));
            $("#sidebar ul .has-sub").each(function (i, item) {
                if (index == i) {
                    $(this).children(".sub").slideDown(500, function () {
                        if (!$(item).hasClass("active")) {
                            $(item).children("a").children(".arrow").addClass("open");
                            $(item).addClass("active");
                        }
                    });
                } else {
                    $(item).children(".sub").slideUp(500, function () {
                        if ($(item).hasClass("active")) {
                            $(item).removeClass("active");
                            $(item).children("a").children(".arrow").removeClass("open");
                        }
                    });
                }
            });
        });
    },
    Ad: function () {
        //$.jBox.messager("<img alt='' src='/Theme/img/PDA.jpg'/>", "&nbsp;&nbsp;系统动态:新增Android版本手持机支持 ", 10000, {
        //    width: 350, showType: 'fade', buttons: { '关闭': true }, submit: function (v, h, f) {
        //        //window.open("http://www.jooshow.com/");
        //        return true;
        //    }
        //});
    },
    ChangeStorage:function(){
        //切换仓库
        $("li.storage_item").click(function(){
            var StorageNum=$(this).attr("data-StorageNum");
            var param = {};
            param["StorageNum"] = StorageNum;
            $.gitAjax({
                url: "/Common/ChangeStorage", type: "post", data: param, success: function (result) {
                    window.location.reload();
                }
            });
        });
    }
};

/********************************************选择库位***********************************************/
; (function ($) {
    $.fn.LocalDialog = function (options) {
        var defaultOption = {
            data: {},
            Mult: true,
            StorageSearch:false,
            EventName: "dblclick",
            callBack: undefined
        };
        defaultOption = $.extend(defaultOption, options);
        var current=undefined;
        var DataServer={
            TabGrid:undefined,
            Server: function () {
                var config = (function () {
                    var URL_GetList = "/Storage/LocationAjax/GetList";
                    return {
                        URL_GetList: URL_GetList
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

                    return {
                        GetList: GetList
                    }

                })($, config);
                return dataServer;
            },
            PageClick:function(PageIndex,PageSize){
                $.jBox.tip("正在努力加载数据...","loading");
                
                var LocalName=current.find("input[name='LocalName']").val();
                var LocalBarCode=current.find("input[name='LocalBarCode']").val();
                var StorageNum=current.find("select[name='StorageNum']").val();

                var search=defaultOption.data||{};
                if(search.ListLocalType==undefined){
                    search.ListLocalType=JSON.stringify([]);
                }
                search["PageIndex"]=PageIndex;
                search["PageSize"]=PageSize;
                search["LocalName"]=LocalName;
                search["LocalBarCode"]=LocalBarCode;
                search["StorageNum"]=StorageNum;

                var Server=DataServer.Server();
                Server.GetList(search,function(result){
                    DataServer.SetTable(result);
                    $.jBox.closeTip();
                });
            },
            SetTable:function(result){
                var cols=[
                    {title:'库位编号', name:'LocalBarCode', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'库位名称', name:'LocalName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'库位类型', name:'LocalType', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return git.GetEnumDesc(ELocalType,data); 
                    }},
                    {title:'仓库', name:'StorageName', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'是否禁用', name:'IsForbid', width: 85, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        if(data==EBoolJson.Yes){
                            return "<input type='checkbox' checked='checked' name='IsForbid_item' disabled='disabled'/>";
                        }else{
                            return "<input type='checkbox' name='IsForbid_item' disabled='disabled'/>";
                        }
                    }},
                    {title:'是否默认', name:'IsDefault', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        if(data==EBoolJson.Yes){
                            return "<input type='checkbox' checked='checked' name='IsForbid_item' disabled='disabled'/>";
                        }else{
                            return "<input type='checkbox' name='IsForbid_item' disabled='disabled'/>";
                        }
                    }},
                ];

                if(this.TabGrid==undefined){
                    this.TabGrid=current.find("#tabInfo").mmGrid({
                        cols:cols,
                        items:result.Result,
                        checkCol:true,
                        nowrap:true,
                        height:335,
                        nowrap:true,
                        multiSelect:defaultOption.Mult
                    });
                    //绑定事件
                    DataServer.BindEvent();
                }else{
                    this.TabGrid.load(result.Result);
                }

                var pageInfo=result.PageInfo;
                if(pageInfo!=undefined){
                    current.find("#myMinPager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: DataServer.PageClick });
                }
            },
            BindEvent:function(){
                //搜索
                current.find(".search").find('button[data-command="Search"]').unbind("click").bind("click",function(event) {
                    DataServer.PageClick(1,10);
                });
            },
            GetSelect:function(){
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
            }
        }

        return $(this).each(function(index, el) {
             var submit = function (v, h, f) {
                if (v == 1) {
                    var list=DataServer.GetSelect();
                    if(list==undefined || list.length==0){
                        $.jBox.tip("请选择库位","warn");
                        return false;
                    }
                    if (defaultOption.callBack != undefined && typeof (defaultOption.callBack) == "function") {
                        if(defaultOption.Mult){
                            defaultOption.callBack.call(el,list);
                        }else{
                            defaultOption.callBack.call(el,list[0]);
                        }
                    }
                }
            };
            $(el).bind(defaultOption.EventName, function () {
                $.jBox.open("get:/Storage/Location/Dialog", "选择库位", 850, 530, {
                    buttons: { "选择": 1, "关闭": 2 }, submit: submit, loaded: function (h) {
                        current=h;
                        DataServer.TabGrid=undefined;
                        DataServer.PageClick(1,10);
                    }
                });
            });
        });
    };
})(jQuery);


/********************************************选择产品信息***********************************************/
/**
* data: 传入参数
* Mult:是否允许选择多个checkbox，默认是true
* callBack: 选择之后的回调函数
**/
; (function ($) {
    $.fn.ProductDialog = function (options) {
        var defaultOption = {
            data: {},
            Mult: true,
            EventName: "dblclick",
            callBack: undefined
        };
        defaultOption = $.extend(defaultOption, options);
        var current=undefined;

        var DataServer={
            TabGrid:undefined,
            Server: function () {
                var config = (function () {
                    var URL_GetList = "/Storage/ProductAjax/GetList";
                    return {
                        URL_GetList: URL_GetList
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

                    return {
                        GetList: GetList
                    }

                })($, config);
                return dataServer;
            },
            PageClick:function(PageIndex,PageSize){
                $.jBox.tip("正在努力加载数据...","loading");
                var Server=DataServer.Server();
                var search=defaultOption.data || {};
                var ProductName=current.find('input[name="ProductName"]').val();
                var BarCode=current.find('input[name="BarCode"]').val();
                search["PageIndex"]=PageIndex;
                search["PageSize"]=PageSize;
                search["ProductName"]=ProductName;
                search["BarCode"]=BarCode;
                Server.GetList(search,function(result){
                    DataServer.SetTable(result);
                    $.jBox.closeTip();
                });
            },
            SetTable:function(result){

                var cols=[
                    {title:'产品编码', name:'BarCode', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'产品名称', name:'ProductName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'规格', name:'Size', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'类别', name:'CateName', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'存储单位', name:'UnitName', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'包装类型', name:'IsSingle', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'单价', name:'AvgPrice', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'重量', name:'NetWeight', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'显示名', name:'Display', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                ];

                if(this.TabGrid==undefined){
                    this.TabGrid=current.find("#tabInfo").mmGrid({
                        cols:cols,
                        items:result.Result,
                        checkCol:true,
                        nowrap:true,
                        height:335,
                        nowrap:true,
                        multiSelect:defaultOption.Mult
                    });
                    //绑定事件
                    DataServer.BindEvent();
                }else{
                    this.TabGrid.load(result.Result);
                }
                var pageInfo=result.PageInfo;
                if(pageInfo!=undefined){
                    current.find("#myMinPager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: DataServer.PageClick });
                }
            },
            BindEvent:function(){
                //搜索
                current.find(".search").find('button[data-command="Search"]').unbind("click").bind("click",function(event) {
                    DataServer.PageClick(1,10);
                });
            },
            GetSelect:function(){
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
            }
        }

        var submit = function (v, h, f) {
            if (v == 1) {
                var list=DataServer.GetSelect();
                if(list==undefined || list.length==0){
                    $.jBox.tip("请选择产品","warn");
                    return false;
                }
                if (defaultOption.callBack != undefined && typeof (defaultOption.callBack) == "function") {
                    if(defaultOption.Mult){
                        defaultOption.callBack(list);
                    }else{
                        defaultOption.callBack(list[0]);
                    }
                }
            }
        };
        
        $(this).bind(defaultOption.EventName, function () {
            $.jBox.open("get:/Storage/Product/Dialog", "选择产品", 850, 530, {
                buttons: { "选择": 1, "关闭": 2 }, submit: submit, loaded: function (h) {
                    current=h;
                    DataServer.TabGrid=undefined;
                    DataServer.PageClick(1,10);
                }
            });
        });
    };
})(jQuery);


/********************************************采购订单--选择产品***********************************************/
/**
* data: 传入参数
* Mult:是否允许选择多个checkbox，默认是true
* callBack: 选择之后的回调函数
* 产品的基本信息,包含数量数据库
**/
; (function ($) {
    $.fn.PurchaseDialog = function (options) {
        var defaultOption = {
            data: {},
            Mult: true,
            EventName: "dblclick",
            callBack: undefined
        };
        defaultOption = $.extend(defaultOption, options);

        var DataServer={
            TabGrid:undefined,
            Server: function () {
                var config = (function () {
                    var URL_GetList = "/Storage/ProductAjax/GetList";
                    return {
                        URL_GetList: URL_GetList
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

                    return {
                        GetList: GetList
                    }

                })($, config);
                return dataServer;
            },
            PageClick:function(PageIndex,PageSize){
                $.jBox.tip("正在努力加载数据...","loading");
                var Server=DataServer.Server();
                var search=defaultOption.data || {};
                var ProductName=current.find('input[name="ProductName"]').val();
                var BarCode=current.find('input[name="BarCode"]').val();
                search["PageIndex"]=PageIndex;
                search["PageSize"]=PageSize;
                search["ProductName"]=ProductName;
                search["BarCode"]=BarCode;
                Server.GetList(search,function(result){
                    DataServer.SetTable(result);
                    $.jBox.closeTip();
                });
            },
            SetTable:function(result){

                var cols=[
                    {title:'产品编码', name:'BarCode', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'产品名称', name:'ProductName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'规格', name:'Size', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'类别', name:'CateName', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'存储单位', name:'UnitName', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'包装类型', name:'IsSingle', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'单价', name:'AvgPrice', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'重量', name:'NetWeight', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'数量', name:'Qty', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return '<input type="text" name="Qty" class="m-wrap input-small" value="" style="width:35px;">';
                    }},
                ];

                if(this.TabGrid==undefined){
                    this.TabGrid=current.find("#tabInfo").mmGrid({
                        cols:cols,
                        items:result.Result,
                        checkCol:true,
                        nowrap:true,
                        height:335,
                        nowrap:true,
                        multiSelect:defaultOption.Mult
                    });
                    //绑定事件
                    DataServer.BindEvent();
                }else{
                    this.TabGrid.load(result.Result);
                }
                var pageInfo=result.PageInfo;
                if(pageInfo!=undefined){
                    current.find("#myMinPager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: DataServer.PageClick });
                }
            },
            BindEvent:function(){
                //搜索
                current.find(".search").find('button[data-command="Search"]').unbind("click").bind("click",function(event) {
                    DataServer.PageClick(1,10);
                });
            },
            GetSelect:function(){
                var list=[];
                current.find('#tabInfo').find('tr').each(function(index, el) {
                    var flag=$(el).find('input.mmg-check').attr("checked");
                    if(flag){
                        var Qty=$(el).find("input[name='Qty']").val();
                        if(!isNaN(Qty) && parseFloat(Qty)>0){
                            var item=DataServer.TabGrid.row(index);
                            item["Qty"]=Qty;
                            list.push(item);
                        }
                    }
                });
                return list;
            }
        }

        var submit = function (v, h, f) {
            if (v == 1) {
                var list=DataServer.GetSelect();
                if(list==undefined || list.length==0){
                    $.jBox.tip("请选择产品","warn");
                    return false;
                }
                if (defaultOption.callBack != undefined && typeof (defaultOption.callBack) == "function") {
                    if(defaultOption.Mult){
                        defaultOption.callBack(list);
                    }else{
                        defaultOption.callBack(list[0]);
                    }
                }
            }
        };
        
        $(this).bind(defaultOption.EventName, function () {
            $.jBox.open("get:/Biz/Purchase/AddProduct", "采购订单-选择产品", 850, 530, {
                buttons: { "选择": 1, "关闭": 2 }, submit: submit, loaded: function (h) {
                    current=h;
                    DataServer.TabGrid=undefined;
                    DataServer.PageClick(1,10);
                }
            });
        });
    };
})(jQuery);


/*******************************************销售订单选择产品***********************************************/
/**
* data: 传入参数
* Mult:是否允许选择多个checkbox，默认是true
* callBack: 选择之后的回调函数
* 销售订单选择产品: 产品信息,正式库位(待入库未)库存总和,输入数量框
**/
; (function ($) {
    $.fn.SaleProduct = function (options) {
        var defaultOption = {
            data: {},
            Mult: true,
            EventName: "dblclick",
            callBack: undefined
        };
        defaultOption = $.extend(defaultOption, options);

        var DataServer={
            TabGrid:undefined,
            Server: function () {
                var config = (function () {
                    var URL_GetList = "/Storage/StockAjax/GetLocalProduct";
                    return {
                        URL_GetList: URL_GetList
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

                    return {
                        GetList: GetList
                    }

                })($, config);
                return dataServer;
            },
            PageClick:function(PageIndex,PageSize){
                $.jBox.tip("正在努力加载数据...","loading");
                var ProductName=current.find('input[name="ProductName"]').val();
                var BarCode=current.find('input[name="BarCode"]').val();
                var Server=DataServer.Server();
                var search={};
                search["PageIndex"]=PageIndex;
                search["PageSize"]=PageSize;
                search["ProductName"]=ProductName;
                search["BarCode"]=BarCode;
                Server.GetList(search,function(result){
                    DataServer.SetTable(result);
                    $.jBox.closeTip();
                });
            },
            SetTable:function(result){
                var cols=[
                    {title:'产品编码', name:'BarCode', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'产品名称', name:'ProductName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'规格', name:'Size', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'类别', name:'CateName', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'存储单位', name:'UnitName', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'单价', name:'OutPrice', width: 55, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'库存数', name:'Num', width: 55, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'数量', name:'Qty', width: 50, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return '<input type="text" name="Qty" class="m-wrap input-small" value="" style="width:35px;">';
                    }},
                ];

                if(this.TabGrid==undefined){
                    this.TabGrid=current.find("#tabInfo").mmGrid({
                        cols:cols,
                        items:result.Result,
                        checkCol:true,
                        nowrap:true,
                        height:335,
                        nowrap:true,
                        multiSelect:defaultOption.Mult
                    });
                    //绑定事件
                    DataServer.BindEvent();
                }else{
                    this.TabGrid.load(result.Result);
                }
                var pageInfo=result.PageInfo;
                if(pageInfo!=undefined){
                    current.find("#myMinPager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: DataServer.PageClick });
                }
            },
            BindEvent:function(){
                current.find(".search").find('button[data-command="Search"]').unbind("click").bind("click",function(event) {
                    DataServer.PageClick(1,10);
                });
            },
            GetSelect:function(){
                var list=[];
                current.find('#tabInfo').find('tr').each(function(index, el) {
                    var flag=$(el).find('input.mmg-check').attr("checked");
                    if(flag){
                        var Qty=$(el).find("input[name='Qty']").val();
                        if(!isNaN(Qty) && parseFloat(Qty)>0){
                            var item=DataServer.TabGrid.row(index);
                            item["Qty"]=Qty;
                            list.push(item);
                        }
                    }
                });
                return list;
            }
        }

        var submit = function (v, h, f) {
            if (v == 1) {
                var list=DataServer.GetSelect();
                if(list==undefined || list.length==0){
                    $.jBox.tip("请选择产品","warn");
                    return false;
                }
                if (defaultOption.callBack != undefined && typeof (defaultOption.callBack) == "function") {
                    if(defaultOption.Mult){
                        defaultOption.callBack(list);
                    }else{
                        defaultOption.callBack(list[0]);
                    }
                }
            }
        };
        
        $(this).bind(defaultOption.EventName, function () {
            $.jBox.open("get:/Storage/Product/Product", "销售订单-选择产品", 850, 530, {
                buttons: { "选择": 1, "关闭": 2 }, submit: submit, loaded: function (h) {
                    current=h;
                    DataServer.TabGrid=undefined;
                    DataServer.PageClick(1,10);
                }
            });
        });
    };
})(jQuery);


/********************************************选择出库单产品***********************************************/
/**
* data: 传入参数
* Mult:是否允许选择多个checkbox，默认是true
* callBack: 选择之后的回调函数
* 用于出库单的选择,自建出库单需要显示产品库位等信息
**/
; (function ($) {
    $.fn.LocalProduct = function (options) {
        var defaultOption = {
            title:"选择库存产品",
            data: {},
            Mult: true,
            EventName: "click",
            callBack: undefined
        };
        defaultOption = $.extend(defaultOption, options);
        var current=undefined;
        var DataServer={
            TabGrid:undefined,
            Server: function () {
                var config = (function () {
                    var URL_GetList = "/Storage/StockAjax/GetOutAbleList";
                    return {
                        URL_GetList: URL_GetList
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
                    return {
                        GetList: GetList
                    }

                })($, config);
                return dataServer;
            },
            PageClick:function(PageIndex,PageSize){
                $.jBox.tip("正在努力加载数据...","loading");
                var Server=DataServer.Server();
                var BarCode=current.find("input[name='BarCode']").val();
                var ProductName=current.find("input[name='ProductName']").val();
                var LocalNum=current.find("select[name='LocalNum']").val();
                var search={};
                search["PageIndex"]=PageIndex;
                search["PageSize"]=PageSize;
                search["BarCode"]=BarCode;
                search["ProductName"]=ProductName;
                search["LocalNum"]=LocalNum;
                Server.GetList(search,function(result){
                    DataServer.SetTable(result);
                    $.jBox.closeTip();
                });
            },
            SetTable:function(result){

                var cols=[
                    {title:'产品编号', name:'BarCode', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'产品名称', name:'ProductName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'产品批次', name:'BatchNum', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'库位', name:'LocalName', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'库存数', name:'Num', width: 85, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'数量', name:'Qty', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return '<input type="text" name="Qty" class="m-wrap input-small" value="" style="width:35px;">';
                    }},
                ];

                if(this.TabGrid==undefined){
                    this.TabGrid=current.find("#tabInfo").mmGrid({
                        cols:cols,
                        items:result.Result,
                        checkCol:true,
                        nowrap:true,
                        height:335,
                        nowrap:true,
                        multiSelect:defaultOption.Mult
                    });
                }else{
                    this.TabGrid.load(result.Result);
                }
                var pageInfo=result.PageInfo;
                if(pageInfo!=undefined){
                    current.find("#myMinPager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: DataServer.PageClick });
                }
            },
            GetSelect:function(){
                var list=[];
                current.find('#tabInfo').find('tr').each(function(index, el) {
                    var flag=$(el).find('input.mmg-check').attr("checked");
                    if(flag){
                        var Qty=$(el).find("input[name='Qty']").val();
                        if(!isNaN(Qty) && parseFloat(Qty)>0){
                            var item=DataServer.TabGrid.row(index);
                            item["Qty"]=Qty;
                            list.push(item);
                        }
                    }
                });
                return list;
            },
            Init:function(){
                //搜索
                current.find(".search").find('button[data-command="Search"]').click(function(event) {
                    DataServer.PageClick(1,10);
                });
            }
        }

        var submit = function (v, h, f) {
            if (v == 1) {
                var list=DataServer.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择产品信息");
                    return false;
                }
                if (defaultOption.callBack != undefined && typeof (defaultOption.callBack) == "function") {
                    if(defaultOption.Mult){
                        defaultOption.callBack(list);
                    }else{
                        defaultOption.callBack(list[0]);
                    }
                }
            }
        };

        $(this).bind(defaultOption.EventName, function () {
            $.jBox.open("get:/Order/OutStorage/AddProduct", defaultOption.title, 850, 530, {
                buttons: { "选择": 1, "关闭": 2 }, submit: submit, loaded: function (h) {
                    current=h;
                    DataServer.TabGrid=undefined;
                    DataServer.PageClick(1,10);
                    DataServer.Init();
                }
            });
        });
    };
})(jQuery);


/********************************************选择客户***********************************************/
/**
* data: 传入参数
* Mult:是否允许选择多个checkbox，默认是true
* callBack: 选择之后的回调函数
**/
; (function ($) {
    $.fn.CustomerDialog = function (options) {
        var defaultOption = {
            data: {},
            Mult: true,
            EventName: "dblclick",
            callBack: undefined
        };
        defaultOption = $.extend(defaultOption, options);
        var current=undefined;

        var DataServer={
            CusGrid:undefined,
            Server: function () {
                var config = (function () {
                    var URL_GetList = "/Client/CustomerAjax/GetAddressList";
                    return {
                        URL_GetList: URL_GetList
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

                    return {
                        GetList: GetList
                    }

                })($, config);
                return dataServer;
            },
            PageClick:function(PageIndex,PageSize){
                $.jBox.tip("正在努力加载数据...","loading");
                var Server=DataServer.Server();
                var CusNum=current.find("input[name='CusNum']").val();
                var CusName=current.find("input[name='CusName']").val();
                var Address=current.find("input[name='Address']").val();
                var search={};
                search["PageIndex"]=PageIndex;
                search["PageSize"]=PageSize;
                search["CusNum"]=CusNum;
                search["CusName"]=CusName;
                search["Address"]=Address;
                Server.GetList(search,function(result){
                    DataServer.SetTable(result);
                    $.jBox.closeTip();
                });
            },
            SetTable:function(result){
                var cols=[
                    {title:'客户编号', name:'CusNum', width: 90, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'客户名称', name:'CusName', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'地址', name:'Address', width: 120, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'联系人', name:'Contact', width: 90, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'电话', name:'Phone', width: 70, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'备注', name:'Remark', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'邮箱', name:'Email', width: 70, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'传真', name:'Fax', width: 70, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'创建时间', name:'CreateTime', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return git.JsonToDateTime(data);
                    }}
                ];

                if(this.DataServer==undefined){
                    this.CusGrid=current.find("#tabInfo").mmGrid({
                        cols:cols,
                        items:result.Result,
                        checkCol:true,
                        nowrap:true,
                        multiSelect:defaultOption.Mult,
                        height:380
                    });
                    //绑定事件
                    DataServer.BindEvent();
                }else{
                    this.CusGrid.load(result.Result);
                }
                var pageInfo=result.PageInfo;
                if(pageInfo!=undefined){
                    current.find("#myMinPager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: DataServer.PageClick });
                }
            },
            BindEvent:function(){
                //搜索
                current.find(".search").find('button[data-command="Search"]').unbind("click").bind("click",function(event) {
                    DataServer.PageClick(1,10);
                });
            },
            GetSelect:function(){
                var list=[];
                if(this.CusGrid!=undefined){
                    var rows=this.CusGrid.selectedRows();
                    if(rows!=undefined && rows.length>0){
                        for(var i=0;i<rows.length;i++){
                            list.push(rows[i]);
                        }
                    }
                }
                return list;
            }
        }

        var submit = function (v, h, f) {
            if (v == 1) {
                var list=DataServer.GetSelect();
                if(list==undefined || list.length==0){
                    $.jBox.tip("请选择客户","warn");
                    return false;
                }
                if (defaultOption.callBack != undefined && typeof (defaultOption.callBack) == "function") {
                    if(defaultOption.Mult){
                        defaultOption.callBack(list);
                    }else{
                        defaultOption.callBack(list[0]);
                    }
                }
            }
        };
        
        $(this).bind(defaultOption.EventName, function () {
            $.jBox.open("get:/Client/Customer/Dialog", "选择客户", 850, 580, {
                buttons: { "选择": 1, "关闭": 2 }, submit: submit, loaded: function (h) {
                    current=h;
                    DataServer.CusGrid=undefined;
                    DataServer.PageClick(1,10);
                }
            });
        });
    };
})(jQuery);

/********************************************选择供应商***********************************************/
/**
* data: 传入参数
* Mult:是否允许选择多个checkbox，默认是true
* callBack: 选择之后的回调函数
**/
; (function ($) {
    $.fn.SupplierDialog = function (options) {
        var defaultOption = {
            data: {},
            Mult: true,
            EventName: "dblclick",
            callBack: undefined
        };
        defaultOption = $.extend(defaultOption, options);
        var current=undefined;

        var DataServer={
            TabGrid:undefined,
            Server: function () {
                var config = (function () {
                    var URL_GetList = "/Client/SupplierAjax/GetList";
                    return {
                        URL_GetList: URL_GetList
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

                    return {
                        GetList: GetList
                    }

                })($, config);
                return dataServer;
            },
            PageClick:function(PageIndex,PageSize){
                $.jBox.tip("正在努力加载数据...","loading");
                var Server=DataServer.Server();
                var SupNum=current.find("input[name='SupNum']").val();
                var SupName=current.find("input[name='SupName']").val();
                var Phone=current.find("input[name='Phone']").val();
                var search={};
                search["PageIndex"]=PageIndex;
                search["PageSize"]=PageSize;
                search["SupNum"]=SupNum;
                search["SupName"]=SupName;
                search["Phone"]=Phone;
                Server.GetList(search,function(result){
                    DataServer.SetTable(result);
                    $.jBox.closeTip();
                });
            },
            SetTable:function(result){
                var cols=[
                    {title:'供应商编号', name:'SupNum', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'供应商名称', name:'SupName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'电话', name:'Phone', width: 100, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'传真', name:'Fax', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'Email', name:'Email', width: 85, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'联系人', name:'ContactName', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'地址', name:'Address', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                ];

                if(this.TabGrid==undefined){
                    this.TabGrid=current.find("#tabInfo").mmGrid({
                        cols:cols,
                        items:result.Result,
                        checkCol:true,
                        nowrap:true,
                        height:335,
                        nowrap:true,
                        multiSelect:defaultOption.Mult
                    });
                    //绑定事件
                    DataServer.BindEvent();
                }else{
                    this.TabGrid.load(result.Result);
                }
                var pageInfo=result.PageInfo;
                if(pageInfo!=undefined){
                    current.find("#myMinPager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: DataServer.PageClick });
                }
            },
            BindEvent:function(){
                //搜索
                current.find(".search").find('button[data-command="Search"]').click(function(event) {
                    DataServer.PageClick(1,10);
                });
            },
            GetSelect:function(){
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
            }
        }

        var submit = function (v, h, f) {
            if (v == 1) {
                var list=DataServer.GetSelect();
                if(list==undefined || list.length==0){
                    $.jBox.tip("请选择供应商","warn");
                    return false;
                }
                if (defaultOption.callBack != undefined && typeof (defaultOption.callBack) == "function") {
                    if(defaultOption.Mult){
                        defaultOption.callBack(list);
                    }else{
                        defaultOption.callBack(list[0]);
                    }
                }
            }
        };
        
        $(this).bind(defaultOption.EventName, function () {
            $.jBox.open("get:/Client/Supplier/Dialog", "选择供应商", 850, 530, {
                buttons: { "选择": 1, "关闭": 2 }, submit: submit, loaded: function (h) {
                    current=h;
                    DataServer.TabGrid=undefined;
                    DataServer.PageClick(1,10);
                }
            });
        });

    };
})(jQuery);


/********************************************选择员工***********************************************/
/**
* data: 传入参数
* Mult:是否允许选择多个checkbox，默认是true
* callBack: 选择之后的回调函数
**/
; (function ($) {
    $.fn.UserDialog = function (options) {
        var defaultOption = {
            data: {},
            Mult: true,
            EventName: "dblclick",
            callBack: undefined
        };
        defaultOption = $.extend(defaultOption, options);
        var submit = function (v, h, f) {
            if (v == 1) {
                var result = undefined;
                h.find("input[type='checkbox'][name='user_item']").each(function (i, item) {
                    var flag = $(item).attr("checked");
                    if (flag || flag == "checked") {
                        var data = $(item).attr("data-value");
                        if (!git.IsEmpty(data)) {
                            result = JSON.parse(unescape(data));
                        }
                    }
                });
                if (defaultOption.callBack != undefined && typeof (defaultOption.callBack) == "function") {
                    defaultOption.callBack(result);
                }
            }
        };
        $(this).bind(defaultOption.EventName, function () {
            $.jBox.open("get:/Home/Dialog", "选择用户", 850, 500, {
                buttons: { "选择": 1, "关闭": 2 }, submit: submit, loaded: function (h) {

                }
            });
        });
    };
})(jQuery);







/********************************************选择移库产品***********************************************/
/**
* data: 传入参数
* Mult:是否允许选择多个checkbox，默认是true
* callBack: 选择之后的回调函数
**/
; (function ($) {
    $.fn.MoveProduct = function (options) {
        var defaultOption = {
            title:"选择移库产品",
            data: {},
            Mult: true,
            EventName: "click",
            callBack: undefined
        };
        defaultOption = $.extend(defaultOption, options);
        
        var current=undefined;

        var DataServer={
            TabGrid:undefined,
            Server: function () {
                var config = (function () {
                    var URL_GetList = "/Storage/StockAjax/GetList";
                    return {
                        URL_GetList: URL_GetList
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

                    return {
                        GetList: GetList
                    }

                })($, config);
                return dataServer;
            },
            PageClick:function(PageIndex,PageSize){
                $.jBox.tip("正在努力加载数据...","loading");
                var Server=DataServer.Server();
                var BarCode=current.find("input[name='BarCode']").val();
                var ProductName=current.find("input[name='ProductName']").val();
                var LocalNum=current.find("select[name='LocalNum']").val();
                var search={};
                search["PageIndex"]=PageIndex;
                search["PageSize"]=PageSize;
                search["BarCode"]=BarCode;
                search["ProductName"]=ProductName;
                search["LocalNum"]=LocalNum;
                Server.GetList(search,function(result){
                    DataServer.SetTable(result);
                    $.jBox.closeTip();
                });
            },
            SetTable:function(result){

                var cols=[
                    {title:'产品编码', name:'BarCode', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'产品名称', name:'ProductName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'批次', name:'BatchNum', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'库位', name:'LocalName', width: 120, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'库存数', name:'Num', width: 55, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'数量', name:'Qty', width: 50, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return '<input type="text" name="Qty" class="m-wrap input-small" value="" style="width:35px;">';
                    }},
                    {title:'移入库位', name:'ToLocalNum', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        var html='';
                        html+='<input type="hidden" name="ToLocalNum">';
                        html+='<input type="text" name="ToLocalName" class="m-wrap input-small" value="">';
                        return html;
                    }},
                ];

                if(this.TabGrid==undefined){
                    this.TabGrid=current.find("#tabInfo").mmGrid({
                        cols:cols,
                        items:result.Result,
                        checkCol:true,
                        nowrap:true,
                        height:335,
                        nowrap:true,
                        multiSelect:defaultOption.Mult
                    });
                    //绑定事件
                    DataServer.BindEvent();
                }else{
                    this.TabGrid.load(result.Result);
                }
                var pageInfo=result.PageInfo;
                if(pageInfo!=undefined){
                    current.find("#myMinPager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: DataServer.PageClick });
                }
            },
            BindEvent:function(){
                this.TabGrid.off("loadSuccess").on('loadSuccess', function(e, data){
                    //绑定选择库位事件
                    current.find("#tabInfo").find("input[name='ToLocalName']").LocalDialog({Mult:false,callBack:function(result){
                        $(this).val(result.LocalName);
                        $(this).parent().children('input[name="ToLocalNum"]').val(result.LocalNum);
                    }});
                });
            },
            GetSelect:function(){
                var list=[];
                current.find('#tabInfo').find('tr').each(function(index, el) {
                    var flag=$(el).find('input.mmg-check').attr("checked");
                    if(flag){
                        var Qty=$(el).find("input[name='Qty']").val();
                        var ToLocalNum=$(el).find("input[name='ToLocalNum']").val();
                        var ToLocalName=$(el).find("input[name='ToLocalName']").val();
                        if(!isNaN(Qty) && parseFloat(Qty)>0){
                            var item=DataServer.TabGrid.row(index);
                            item["Qty"]=Qty;
                            item["ToLocalNum"]=ToLocalNum;
                            item["ToLocalName"]=ToLocalName;
                            list.push(item);
                        }
                    }
                });
                return list;
            },
            Init:function(){
                //搜索
                current.find(".search").find('button[data-command="Search"]').unbind("click").bind("click",function(event) {
                    DataServer.PageClick(1,10);
                });
            }
        }

        var submit = function (v, h, f) {
            if (v == 1) {
                var list=DataServer.GetSelect();
                
                if (defaultOption.callBack != undefined && typeof (defaultOption.callBack) == "function") {
                    if(defaultOption.Mult){
                        defaultOption.callBack(list);
                    }else{
                        defaultOption.callBack(list[0]);
                    }
                }
            }
        };

        $(this).bind(defaultOption.EventName, function () {
            $.jBox.open("get:/Order/Move/AddProduct", defaultOption.title, 850, 530, {
                buttons: { "选择": 1, "关闭": 2 }, submit: submit, loaded: function (h) {
                    current=h;
                    DataServer.TabGrid=undefined;
                    DataServer.Init();
                    DataServer.PageClick(1,10);
                }
            });
        });

    };
})(jQuery);


/********************************************选择报损产品***********************************************/
/**
* data: 传入参数
* Mult:是否允许选择多个checkbox，默认是true
* callBack: 选择之后的回调函数
**/
; (function ($) {
    $.fn.BadProduct = function (options) {
        var defaultOption = {
            title:"选择报损产品",
            data: {},
            Mult: true,
            EventName: "click",
            callBack: undefined
        };
        defaultOption = $.extend(defaultOption, options);
        var current=undefined;

        var DataServer={
            TabGrid:undefined,
            Server: function () {
                var config = (function () {
                    var URL_GetList = "/Storage/StockAjax/GetBadAbleList";
                    return {
                        URL_GetList: URL_GetList
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
                    return {
                        GetList: GetList
                    }
                })($, config);
                return dataServer;
            },
            PageClick:function(PageIndex,PageSize){
                $.jBox.tip("正在努力加载数据...","loading");
                var Server=DataServer.Server();
                var BarCode=current.find("input[name='BarCode']").val();
                var ProductName=current.find("input[name='ProductName']").val();
                var LocalNum=current.find("select[name='LocalNum']").val();
                var search={};
                search["PageIndex"]=PageIndex;
                search["PageSize"]=PageSize;
                search["BarCode"]=BarCode;
                search["ProductName"]=ProductName;
                search["LocalNum"]=LocalNum;
                Server.GetList(search,function(result){
                    DataServer.SetTable(result);
                    $.jBox.closeTip();
                });
            },
            SetTable:function(result){

                var cols=[
                    {title:'产品编码', name:'BarCode', width: 65, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'产品名称', name:'ProductName', width: 150, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'批次', name:'BatchNum', width: 80, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'库位', name:'LocalName', width: 120, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'库存数', name:'Num', width: 55, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return data;
                    }},
                    {title:'数量', name:'Qty', width: 50, align: 'center',lockWidth:false,  renderer: function(data,item,rowIndex){
                        return '<input type="text" name="Qty" class="m-wrap input-small" value="" style="width:35px;">';
                    }},
                ];

                if(this.TabGrid==undefined){
                    this.TabGrid=current.find("#tabInfo").mmGrid({
                        cols:cols,
                        items:result.Result,
                        checkCol:true,
                        nowrap:true,
                        height:335,
                        nowrap:true,
                        multiSelect:defaultOption.Mult
                    });
                    //绑定事件
                    DataServer.BindEvent();
                }else{
                    this.TabGrid.load(result.Result);
                }
                var pageInfo=result.PageInfo;
                if(pageInfo!=undefined){
                    current.find("#myMinPager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: DataServer.PageClick });
                }
            },
            BindEvent:function(){
            },
            GetSelect:function(){
                var list=[];
                current.find('#tabInfo').find('tr').each(function(index, el) {
                    var flag=$(el).find('input.mmg-check').attr("checked");
                    if(flag){
                        var Qty=$(el).find("input[name='Qty']").val();
                        if(!isNaN(Qty) && parseFloat(Qty)>0){
                            var item=DataServer.TabGrid.row(index);
                            item["Qty"]=Qty;
                            list.push(item);
                        }
                    }
                });
                return list;
            },
            Init:function(){
                //搜索
                current.find(".search").find('button[data-command="Search"]').unbind("click").bind("click",function(event) {
                    DataServer.PageClick(1,10);
                });
            }
        }

        var submit = function (v, h, f) {
            if (v == 1) {
                var list=DataServer.GetSelect();
                
                if (defaultOption.callBack != undefined && typeof (defaultOption.callBack) == "function") {
                    if(defaultOption.Mult){
                        defaultOption.callBack(list);
                    }else{
                        defaultOption.callBack(list[0]);
                    }
                }
            }
        };

        $(this).bind(defaultOption.EventName, function () {
            $.jBox.open("get:/Order/Bad/AddProduct", defaultOption.title, 850, 530, {
                buttons: { "选择": 1, "关闭": 2 }, submit: submit, loaded: function (h) {
                    current=h;
                    DataServer.TabGrid=undefined;
                    DataServer.PageClick(1,10);
                    DataServer.Init();
                }
            });
        });

    };
})(jQuery);



/********************************************选择自定义报表格式***********************************************/
/**
* data: 传入参数
* Mult:是否允许选择多个checkbox，默认是true
* callBack: 选择之后的回调函数
**/
; (function ($) {
    $.fn.CusReportDialog = function (options) {
        var defaultOption = {
            title:"选择打印模板",
            data: {},
            Mult: false,
            EventName: "click",
            callBack: undefined,
            ReportType:undefined
        };
        defaultOption = $.extend(defaultOption, options);
        
        var current=undefined;
        var target=$(this);

        var DataServer={
            Server: function () {
                var config = (function () {
                    var URL_GetList = "/Report/ManagerAjax/GetList";
                    return {
                        URL_GetList: URL_GetList
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

                    return {
                        GetList: GetList
                    }

                })($, config);
                return dataServer;
            },
            SetTable:function(result){
                current.find("#tabInfo").DataTable({
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
                            return "<input type='checkbox' name='item_report' value='"+data+"' data-full='"+JSON.stringify(full)+"'/>";
                        }},
                        { data: 'ReportNum'},
                        { data: 'ReportName'},
                        { data: 'Remark'}
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
                    current.find("#myMinPager").minpager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: DataServer.PageClick });
                }

                DataServer.BindEvent();
            },
            BindEvent:function(){
                if(defaultOption.Mult){
                    current.find("#tabInfo").find("input[name='item_all']").click(function(event) {
                        var flag=$(this).attr("checked");
                        if(flag){
                            current.find("#tabInfo").find("input[name='item_report']").attr("checked",true);
                        }else{
                            current.find("#tabInfo").find("input[name='item_report']").attr("checked",false);
                        }
                    });
                }
                else{
                    current.find("#tabInfo").find("input[name='item_all']").hide();
                    current.find("#tabInfo").find("input[name='item_report']").click(function(event) {
                        current.find("#tabInfo").find("input[name='item_report']").attr('checked', false);
                        $(this).attr("checked",true);
                    });
                }
            },
            GetSelect:function(){
                var list=[];
                current.find("#tabInfo").find("input[name='item_report']").each(function(i,item){
                    var flag=$(item).attr("checked");
                    if(flag){
                        var value=$(item).attr("data-full");
                        var item=JSON.parse(value);
                        list.push(item);
                    }
                });
                return list;
            }
        }

        var submit = function (v, h, f) {
            if (v == 1) {
                var list=DataServer.GetSelect();
                
                if (defaultOption.callBack != undefined && typeof (defaultOption.callBack) == "function") {
                    if(defaultOption.Mult){
                        defaultOption.callBack.call(target,list);
                    }else{
                        defaultOption.callBack.call(target,list[0]);
                    }
                }
            }
        };

        $(this).bind(defaultOption.EventName, function () {


            var Server=DataServer.Server();
            var search={};
            search["ReportType"]=defaultOption.ReportType;

            Server.GetList(search,function(result){
                
                var data=result.Result;
                if(data!=undefined && data.length>1){
                    $.jBox.open("get:/Report/Manager/Dialog", defaultOption.title, 650, 400, {
                        buttons: { "选择": 1, "关闭": 2 }, submit: submit, loaded: function (h) {
                            current=h;
                            DataServer.SetTable(result);
                        }
                    });
                }else{
                    defaultOption.callBack.call(target,data[0]);
                }
            });

            
        });

    };
})(jQuery);






/****************************************************账户设置************************************************/
var AccountSetting = {
    Add: function () {
        var submit = function (v, h, f) {
            if (v == true) {
                var userCode = h.find("#txtUserCode").val();
                var userName = h.find("#txtUserName").val();
                var realName = h.find("#txtRealName").val();
                var email = h.find("#txtEmail").val();
                var phone = h.find("#txtPhone").val();
                var mobile = h.find("#txtMobile").val();
                var roleNum = h.find("#ddlRole").val();
                var departNum = h.find("#ddlDepart").val();
                if (userName == undefined || userName == "") {
                    $.jBox.tip("请输入用户名", "warn");
                    return false;
                }
                var param = {};
                param["UserCode"] = userCode;
                param["UserName"] = userName;
                param["RealName"] = realName;
                param["Email"] = email;
                param["Phone"] = phone;
                param["Mobile"] = mobile;
                param["RoleNum"] = roleNum;
                param["DepartNum"] = departNum;
                $.gitAjax({
                    url: "/UserAjax/AddUser", type: "post", data: { "entity": JSON.stringify(param) }, success: function (result) {
                        if (result.d == "success") {
                            $.jBox.tip("编辑成功", "success");
                            User.PageClick(1, 10);
                        } else {
                            $.jBox.tip("编辑失败", "error");
                        }
                    }
                });
                return true;
            } else {
                return true;
            }
        }
        $.jBox.open("get:/Home/AccountSetting", "编辑用户", 500, 270, { buttons: { "确定": true, "关闭": false }, submit: submit });
    },
    Edit: function () {
        var submit = function (v, h, f) {
            if (v == true) {
                var CurrentPass = h.find('input[name="CurrentPass"]').val();
                var NewPass = h.find('input[name="NewPass"]').val();
                var ConfirmPass = h.find('input[name="ConfirmPass"]').val();
                var UserNum = h.find('input[name="UserNum"]').val();

                if (git.IsEmpty(CurrentPass)) {
                    $.jBox.tip("请输入当前密码", "warn");
                    return false;
                }
                if (git.IsEmpty(NewPass)) {
                    $.jBox.tip("请输入密码", "warn");
                    return false;
                }
                if (git.IsEmpty(ConfirmPass)) {
                    $.jBox.tip("请输入确认密码", "warn");
                    return false;
                }
                if (NewPass != ConfirmPass) {
                    $.jBox.tip("密码和确认密码不一致", "warn");
                    return false;
                }
                if(git.IsEmpty(UserNum)){
                    $.jBox.tip("该非法操作已经被禁止", "warn");
                    return false;
                }

                var param = {};
                param["CurrentPass"] = CurrentPass;
                param["NewPass"] = NewPass;
                param["ConfirmPass"] = ConfirmPass;
                param["UserNum"] = UserNum;

                $.gitAjax({
                    url: "/UserAjax/ChangePass", type: "post", data: param, success: function (result) {
                        if (result.Code == 1) {
                            $.jBox.tip("修改成功,请重新登录", "success");
                            $.jBox.close();
                        } else {
                            $.jBox.tip(result.Message, "error");
                            
                        }
                    }
                });
                return false;
            } else {
                return true;
            }
        }
        $.jBox.open("get:/Home/EditPass", "修改密码", 350, 250, { buttons: { "确定": true, "关闭": false }, submit: submit });
    }
};