
var CarrierManager = {
    TabGrid:undefined,
    Server: function () {
        var config = (function () {
            var URL_GetList = "/Storage/CarrierAjax/GetList";
            var URL_Delete = "/Storage/CarrierAjax/Delete";
            var URL_Add = "/Storage/CarrierAjax/Add";

            return {
                URL_GetList: URL_GetList,
                URL_Delete: URL_Delete,
                URL_Add: URL_Add,
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

            return {
                GetList: GetList,
                Delete: Delete,
                Add: Add
            }

        })($, config);
        return dataServer;
    },
    Dialog:function(Sn,Command){
        var submit=function(v, h, f){
            if(v){
                var SnNum = h.find('input[name="SnNum"]').val();
                var CarrierNum = h.find('input[name="CarrierNum"]').val();
                var CarrierName = h.find('input[name="CarrierName"]').val();
                var Remark = h.find('input[name="Remark"]').val();
                
                if (git.IsEmpty(CarrierName)) {
                    $.jBox.tip("请输入承运商名称","warn");
                    return false;
                }
                var param={};
                param["SnNum"] = SnNum;
                param["CarrierNum"] = CarrierNum;
                param["CarrierName"] = CarrierName;
                param["Remark"] = Remark;

                var Server=CarrierManager.Server();
                Server.Add(param, function (result) {
                    if (result.Code != 1) {
                        $.jBox.tip(result.Message, "warn");
                        return false;
                    }else{
                        var pageSize=$("#mypager").pager("GetPageSize");
                        var pageIndex=$("#mypager").pager("GetCurrent");
                        if(Command=="Add"){
                            CarrierManager.PageClick(1,pageSize);
                        }else if(Command=="Edit"){
                            CarrierManager.Refresh();
                        }
                        $.jBox.close();
                        $.jBox.tip(result.Message,"success");
                    }
                });
                return false;
            }
        }

        if(Command=="Add"){
            $.jBox.open("get:/Storage/Carrier/Add", "新增", 380, 250, { buttons: { "确定": true, "关闭": false }, submit: submit, loaded: undefined });
        }else if(Command=="Edit"){
            $.jBox.open("get:/Storage/Carrier/Add?SnNum=" + Sn, "编辑", 380, 250, { buttons: { "确定": true, "关闭": false }, submit: submit, loaded: undefined });
        }
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=CarrierManager.Server();
        var search=CarrierManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        console.log(JSON.stringify(search));
        Server.GetList(search,function(result){
            $.jBox.closeTip();
            if(result.Code==1){
                CarrierManager.SetTable(result);
            }else{
                $.jBox.tip(result.Message,"warn");
            }
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=CarrierManager.Server();
        var search=CarrierManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            $.jBox.closeTip();
            if(result.Code==1){
                CarrierManager.SetTable(result);
            }else{
                $.jBox.tip(result.Message,"warn");
            }
        });
    },
    SetTable:function(result){

        var cols=[
            {title:'承运商编号', name:'CarrierNum', width: 100, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'承运商名称', name:'CarrierName', width: 150, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'备注', name:'Remark', width: 250, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                return val;
            }},
            {title:'操作', name:'ID', width: 100, align: 'center',lockWidth:false,  renderer: function(val,item,rowIndex){
                var html="";
                html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;&nbsp;';
                html+='<a class="delete" href="javascript:void(0)">删除</a>&nbsp;';
                return html;
            }}
        ];
        
        if(this.TabGrid==undefined){
            this.TabGrid=$("#tabList").mmGrid({
                cols:cols,
                items:result.Result,
                checkCol:true,
                nowrap:true,
                height:450
            });

            //绑定编辑 删除事件
            CarrierManager.BindEvent();
        }else{
            this.TabGrid.load(result.Result);
        }

        var pageInfo=result.PageInfo;
        if(pageInfo!=undefined){
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: CarrierManager.PageClick });
        }
    },
    BindEvent:function(){

        this.TabGrid.off("cellSelected").on("cellSelected",function(e, item, rowIndex, colIndex){
            
            if($(e.target).is("a.edit")){
                var SN=item.SnNum;
                CarrierManager.Dialog(SN, "Edit");
            }else if($(e.target).is("a.delete")){
                var SN=item.SnNum;
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=[];
                        list.push(SN);
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=CarrierManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                CarrierManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"warn");
                            }
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);
            }
        });
    },
    GetSelect:function(){
        var list=[];
        
        if(this.TabGrid!=undefined){
            var rows=this.TabGrid.selectedRows();
            if(rows!=undefined && rows.length>0){
                for(var i=0;i<rows.length;i++){
                    list.push(rows[i].SnNum);
                }
            }
        }
        return list;
    },
    GetSearch:function(){
        var searchBar=$("div[data-condition='search']");
        var CarrierName = searchBar.find("input[name='CarrierName']").val();

        var search={};
        search["CarrierName"] = CarrierName;
        return search;
    },
    Init:function(){

        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                CarrierManager.Dialog(undefined,command);
            }else if(command=="Edit"){
                var list=CarrierManager.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var UserNum=list[0];
                CarrierManager.Dialog(UserNum,command);
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=CarrierManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=CarrierManager.Server();
                        Server.Delete(param,function(result){
                            if(result.Code==1){
                                var pageSize=$("#mypager").pager("GetPageSize");
                                CarrierManager.PageClick(1,pageSize);
                            }else{
                                $.jBox.tip(result.Message,"warn");
                            }
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=CarrierManager.Server();
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
                CarrierManager.Refresh();
            }

        });

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            CarrierManager.PageClick(1,10);
        });

        //加载默认数据
        CarrierManager.PageClick(1,10);
    }
};