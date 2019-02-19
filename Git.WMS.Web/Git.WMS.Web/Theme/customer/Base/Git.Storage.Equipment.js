
/**
*
*设备管理功能
*
**/

var EquipmentManager = {
    Server: function () {
        var config = (function () {
            var URL_GetList = "/Storage/EquipmentAjax/GetList";
            var URL_Delete = "/Storage/EquipmentAjax/Delete";
            var URL_Add = "/Storage/EquipmentAjax/Add";
            var URL_ToExcel = "/Storage/EquipmentAjax/ToExcel";
            var URL_CreateFlag = "/Storage/EquipmentAjax/CreateFlag";

            return {
                URL_GetList: URL_GetList,
                URL_Delete: URL_Delete,
                URL_Add: URL_Add,
                URL_ToExcel: URL_ToExcel,
                URL_CreateFlag:URL_CreateFlag
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

            var CreateFlag=function(data,callback){
                $.gitAjax({
                    url: config.URL_CreateFlag,
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
                ToExcel: ToExcel,
                CreateFlag:CreateFlag
            }

        })($, config);
        return dataServer;
    },
    Dialog:function(SnNum,Command){
        var submit=function(v, h, f){
            if(v){
                var SnNum=h.find('input[name="SnNum"]').val();
                var EquipmentName=h.find('input[name="EquipmentName"]').val();
                var EquipmentNum=h.find('input[name="EquipmentNum"]').val();
                var IsImpower=0;
                if(h.find('input[name="IsImpower"]').attr("checked")){
                    IsImpower=1;
                }
                var Flag=h.find('input[name="Flag"]').val();
                var Status=h.find('select[name="Status"]').val();
                var Remark=h.find('input[name="Remark"]').val();

                if(git.IsEmpty(EquipmentName)){
                    $.jBox.tip("请输入设备名","warn");
                    return false;
                }
                if(git.IsEmpty(Status)){
                    $.jBox.tip("请选择设备状态","warn");
                    return false;
                }

                var param={};
                param["SnNum"]=SnNum;
                param["EquipmentName"]=EquipmentName;
                param["EquipmentNum"]=EquipmentNum;
                param["IsImpower"]=IsImpower;
                param["Flag"]=Flag;
                param["Status"]=Status;
                param["Remark"]=Remark;

                var Server=EquipmentManager.Server();
                Server.Add(param,function(result){
                    var pageSize=$("#mypager").pager("GetPageSize");
                    var pageIndex=$("#mypager").pager("GetCurrent");
                    if(Command=="Add"){
                        EquipmentManager.PageClick(1,pageSize);
                    }else if(Command=="Edit"){
                        EquipmentManager.Refresh();
                    }
                    $.jBox.tip(result.Message,"success");
                });
            }
        }

        var init=function(h){
            h.find("input[name='IsImpower']").click(function(){
                var flag=$(this).attr("checked");
                if(flag){
                    h.find("input[name='Flag']").attr("disabled",false);
                }else{
                    h.find("input[name='Flag']").attr("disabled",true);
                    h.find("input[name='Flag']").val("");
                }
            });

            var IsImpower=h.find("input[name='IsImpower']").attr("checked");
            if(IsImpower){
                h.find("input[name='Flag']").attr("disabled",false);
            } else{
                h.find("input[name='Flag']").attr("disabled",true);
                h.find("input[name='Flag']").val("");
            }

            h.find("input[name='Flag']").dblclick(function(event) {
                var Server=EquipmentManager.Server();
                Server.CreateFlag(undefined,function(result){
                    h.find("input[name='Flag']").val(result.Result);
                });
            });
        };

        if(Command=="Add"){
            $.jBox.open("get:/Storage/Equipment/Add", "新增设备", 650, 270, { buttons: { "确定": true, "关闭": false } ,submit:submit,loaded:init});
        }else if(Command=="Edit"){
            $.jBox.open("get:/Storage/Equipment/Add?SnNum="+SnNum, "编辑设备", 650, 270, { buttons: { "确定": true, "关闭": false } ,submit:submit,loaded:init});
        }
    },
    PageClick:function(PageIndex,PageSize){
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=EquipmentManager.Server();
        var search=EquipmentManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            EquipmentManager.SetTable(result);
            $.jBox.closeTip();
        });
    },
    Refresh:function(){
        var PageSize=$("#mypager").pager("GetPageSize");
        var PageIndex=$("#mypager").pager("GetCurrent");
        $.jBox.tip("正在努力加载数据...","loading");
        var Server=EquipmentManager.Server();
        var search=EquipmentManager.GetSearch();
        search["PageIndex"]=PageIndex;
        search["PageSize"]=PageSize;
        Server.GetList(search,function(result){
            EquipmentManager.SetTable(result);
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
                    return "<input type='checkbox' name='equipment_item' value='"+data+"'/>";
                }},
                { data: 'EquipmentNum'},
                { data: 'EquipmentName'},
                { data: 'IsImpower',render:function(data,type,full,meta){
                    return git.GetEnumDesc(EBool, data);
                }},
                { data: 'Flag'},
                { data: 'Status',render:function(data,type,full,meta){
                    return git.GetEnumDesc(EEquipmentStatus,data);
                }},
                { data: 'Remark'},
                {data:"ID",render:function(data,type,full,meta){
                    var html="";
                    html+='<a class="edit" href="javascript:void(0)">编辑</a>&nbsp;&nbsp;';
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
            $("#mypager").pager({ pagenumber: pageInfo.PageIndex, recordCount: pageInfo.RowCount, pageSize: pageInfo.PageSize, buttonClickCallback: EquipmentManager.PageClick });
        }

        //绑定编辑 删除事件
        EquipmentManager.BindEvent();
    },
    BindEvent:function(){
        $("#tabList").find("a.edit").click(function(){
            var SN=$(this).parent().parent().find("input[name='equipment_item']").val();
            EquipmentManager.Dialog(SN,"Edit");
        });

        $("#tabList").find("a.delete").click(function(){
            var SN=$(this).parent().parent().find("input[name='equipment_item']").val();
            var submit=function(v,h,f){
                if(v=="ok"){
                    var list=[];
                    list.push(SN);
                    var param={};
                    param["list"]=JSON.stringify(list);
                    var Server=EquipmentManager.Server();
                    Server.Delete(param,function(result){
                        $.jBox.tip(result.Message,"success");
                        var pageSize=$("#mypager").pager("GetPageSize");
                        EquipmentManager.PageClick(1,pageSize);
                    });
                }
            }
            $.jBox.confirm("确定要删除吗？", "提示", submit);
        });
    },
    SelectAll:function(item){
        var flag=$(item).attr("checked");
        if(flag){
            $("#tabList").find("input[name='equipment_item']").attr("checked",true);
        }else{
            $("#tabList").find("input[name='equipment_item']").attr("checked",false);
        }
    },
    GetSelect:function(){
        var list=[];
        $("#tabList").find("input[name='equipment_item']").each(function(i,item){
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
        var EquipmentNum=searchBar.find("input[name='EquipmentNum']").val();
        var EquipmentName=searchBar.find("input[name='EquipmentName']").val();
        var search={};
        search["EquipmentNum"]=EquipmentNum;
        search["EquipmentName"]=EquipmentName;
        return search;
    },
    ToolBar:function(){

        //工具栏按钮点击事件
        $("div.toolbar").find("a.btn").click(function(){
            var command=$(this).attr("data-command");

            if(command=="Add"){
                EquipmentManager.Dialog(undefined,command);
            }else if(command=="Edit"){
                var list=EquipmentManager.GetSelect();
                if(list.length==0){
                    $.jBox.tip("请选择要编辑的项","warn");
                    return false;
                }
                var SN=list[0];
                EquipmentManager.Dialog(SN,command);
            }else if(command=="Delete"){
                var submit=function(v,h,f){
                    if(v=="ok"){
                        var list=EquipmentManager.GetSelect();
                        if(list.length==0){
                            $.jBox.tip("请选择要删除的项","warn");
                            return false;
                        }
                        var param={};
                        param["list"]=JSON.stringify(list);
                        var Server=EquipmentManager.Server();
                        Server.Delete(param,function(result){
                            $.jBox.tip(result.Message,"success");
                            var pageSize=$("#mypager").pager("GetPageSize");
                            EquipmentManager.PageClick(1,pageSize);
                        });
                    }
                }
                $.jBox.confirm("确定要删除吗？", "提示", submit);

            }else if(command=="Excel"){
                var Server=EquipmentManager.Server();
                var search=EquipmentManager.GetSearch();
                Server.ToExcel(search,function(result){
                    
                    if(result.Code==1000){
                        var path = unescape(result.Message);
                        window.location.href = path;
                    }else{
                        $.jBox.info(result.Message, "提示");
                    }
                });
            }else if(command=="Refresh"){
                EquipmentManager.Refresh();
            }

        });

        //搜索
        var searchBar=$("div[data-condition='search']");
        searchBar.find("a[data-command='search']").click(function(){
            EquipmentManager.PageClick(1,10);
        });
        
        //加载默认数据
        EquipmentManager.PageClick(1,10);
    }
}