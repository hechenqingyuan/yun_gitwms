var git = {
    //true表示格式正确
    checkEmail: function (str) {
        if (str.match(/[A-Za-z0-9_-]+[@](\S*)(net|com|cn|org|cc|tv|[0-9]{1,3})(\S*)/g) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示格式正确
    checkMobilePhone: function (str) {
        if (str.match(/^(?:13\d|15[0-9]|18[0123456789])-?\d{5}(\d{3}|\*{3})$/) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示为数字
    checkNum: function (str) {
        if (str.match(/\D/) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示为小数
    checkDecimal: function (str) {
        if (str.match(/^-?\d+(\.\d+)?$/g) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示正整数
    checkRate: function (str) {
        if (str.match(/^[1-9]+[0-9]*]*$/) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //判断数字长度
    checkNumLenght: function (str, a, b) {
        if (parseInt(a) <= $.trim(str).length <= parseInt(b)) {
            return false;
        }
        else {
            return true;
        }
    },
    //判断数字大小
    checkNumBig: function (str, b) {
        if (parseInt(str) < (parseInt(b) + 1)) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示为小数
    checkInteger: function (str) {
        if (str.match(/^[-+]?\d*$/) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示为全部为字符 不包含汉字
    checkStr: function (str) {
        if (/[^\x00-\xff]/g.test(str)) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示包含汉字
    checkChinese: function (str) {
        if (escape(str).indexOf("%u") != -1) {
            return true;
        }
        else {
            return false;
        }
    },
    //true表示格式正确
    checkTelephone: function (str) {
        if (str.match(/^(([0\+]\d{2,3}-)?(0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$/) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //true表示格式正确 检查输入的身份证号是否正确
    checkCard: function (str) {
        //15位数身份证正则表达式
        var arg1 = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/;
        //18位数身份证正则表达式
        var arg2 = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$/;
        if (str.match(arg1) == null && str.match(arg2) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //检查输入的IP地址是否正确 true表示格式正确
    checkIP: function (str) {
        var arg = /^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/;
        if (str.match(arg) == null) {
            return false;
        }
        else {
            return true;
        }
    },
    //检查输入的URL地址是否正确 true表示格式正确
    checkURL: function (str) {
        //if (str.match(/(http[s]?|ftp):\/\/[^\/\.]+?\..+\w$/i) == null) {
        if (str.match(/(http[s]?|ftp):\/\/([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?/) == null) {
            return false
        }
        else {
            return true;
        }
    },
    //检查字符串是否为空
    IsEmpty: function (str) {
        if (str == undefined || str == "") {
            return true;
        } else {
            return false;
        }
    },
    //域名验证
    IsURL: function (URL) {
        var str = URL;
        var Expression = /http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?/;
        var objExp = new RegExp(Expression);
        if (objExp.test(str) == true) {
            return true;
        } else {
            return false;
        }
    },
    //true只有数字和字母和下划线
    IsNumOrLet: function (parVal) {
        if (parVal.match(/[^a-zA-Z0-9_]+/)) {
            return true;
        } else {
            return false;
        }
    },
    //验证Money，true表示正确
    checkMoney: function (str) {
        if (str.match(/^[0-9][\d]{0,11}([\.][\d]{0,4})?$/)) {
            return true;
        } else {
            return false;
        }
    },
    //true表示开始时间大于或等于结束时间
    CompareTime: function (stime, etime) {
        var starttimes = stime.split('-');
        var endtimes = etime.split('-');
        var starttimeTemp = starttimes[0] + '/' + starttimes[1] + '/' + starttimes[2];
        var endtimesTemp = endtimes[0] + '/' + endtimes[1] + '/' + endtimes[2];
        if (Date.parse(new Date(starttimeTemp)) > Date.parse(new Date(endtimesTemp)) || Date.parse(new Date(starttimeTemp)) == Date.parse(new Date(endtimesTemp))) {
            return true;
        }
    },
    //true表示开始时间大于或等于结束时间,只有年月日
    checkCompareTime: function (stime, etime) {
        var starttimes = stime.split('-');
        var endtimes = etime.split('-');
        var starttimeTemp = starttimes[0] + '/' + starttimes[1] + '/' + starttimes[2];
        var endtimesTemp = endtimes[0] + '/' + endtimes[1] + '/' + endtimes[2];
        if (Date.parse(new Date(starttimeTemp)) > Date.parse(new Date(endtimesTemp))) {
            return true;
        }
    },
    Escape: function (value) {
        if (value != undefined) {
            return escape(value);
        }
        return undefined;
    },
    UnEscape: function (value) {
        if (value != undefined) {
            return unescape(value);
        }
        return undefined;
    },
    //.net Json后的日期转换
    JsonToDateTime: function (jsonDate,format) {

        /**
        *YYYY: 年份 2012
        *YY: 年份 12
        *
        *MM:月份 01 02 03 ... 11
        *M:月份 1,2,3,...11
        *
        *DD:日期 两位
        *D: 日期 一位
        *
        *HH: 时 两位 H 一位
        *mm: 分钟 两位 m 一位
        *ss: 秒 两位 s 一位
        *SS: 毫秒 多位SSSS毫秒
        **/
        if (jsonDate == undefined || moment(jsonDate).format("YYYY-MM-DD")=="0001-01-01") {
            return "";
        }
        if (git.IsEmpty(format)) {
            var value = moment(jsonDate).format("YYYY-MM-DD");
            return value;
        } else {
            return moment(jsonDate).format(format);
        }
    },
    //获得访问的路径/Home/Index
    GetPath: function () {
        return window.location.pathname;
    },
    //获得
    GetUrl: function () {
        return window.location.href;
    },
    //获得访问协议 http https
    GetProtocol: function () {
        return window.location.protocol;
    },
    //获取域名
    GetDomain: function () {
        return document.domain;
    },
    //获得端口
    GetPort: function () {
        return window.location.port;
    },
    GetArgs: function () {
        var args = {};
        var query = location.search.substring(1);
        var pairs = query.split("&");
        for (var i = 0; i < pairs.length; i++) {
            var pos = pairs[i].indexOf('=');
            if (pos == -1) {
                continue;
            } else {
                var argname = pairs[i].substring(0, pos);
                var value = pairs[i].substring(pos + 1);
                value = decodeURIComponent(value);
                args[argname] = value;
            }
        }
        return args;
    },
    Trim: function (value) {
        if (value == undefined || value == "") {
            return value;
        }
        value = value.replace(/[ ]/g, "");
        return value;
    },
    LTrim: function (value) {
        if (value == undefined || value == "") {
            return value;
        }
        value = value.replace(/(^\s*)/g, "");
        return value;
    },
    RTrim: function (value) {
        if (value == undefined || value == "") {
            return value;
        }
        value = value.replace(/(\s*$)/g, "");
        return value;
    },
    ToDecimal: function (x, y) {
        var f_x = parseFloat(x);
        if (isNaN(f_x)) {

            return false;
        }
        var f_x = Math.round(x * 100000) / 100000;
        var s_x = f_x.toString();

        console.log("计算后的表达式" + s_x);

        if(s_x.indexOf(".")>0){
            var numStr=s_x.substring(0,s_x.indexOf('.')+1);
            console.log(numStr);
            var str=s_x.substring(s_x.indexOf('.')+1);
            console.log(str);
            if(str.length>=y){
                str=str.substring(0,y);
            }else{
                var index=str.length;
                while (index<y) {
                    str=str+'0';
                    index++;
                }
            }
            s_x=numStr+str;
        }else{
            s_x=s_x+'.';
            var index=0;
            while (index<y) {
                s_x=s_x+'0';
                index++;
            }
        }
        return s_x;
    },
    GetEnumDesc: function (item, value) {
        if (item != undefined) {
            var returnValue = "";
            for (var i = 0; i < item.length; i++) {
                var json = item[i];
                if (json.Value == value) {
                    returnValue = json.Description;
                    break;
                }
            }
            return returnValue;
        }
        return "";
    },
    //截取字符串
    GetStrSub: function (str,lth)
    {
        var resultStr = "";
        if (str == undefined || str == "") {
            resultStr = "";
        }
        else {
            if (str.length > lth) {
                resultStr = str.substring(0, lth) + '...';
            }
            else {
                resultStr = str;
            }
        }
        return resultStr;
    }

};

/***
$.cookie('the_cookie'); // 获得cookie
$.cookie('the_cookie', 'the_value'); // 设置cookie
$.cookie('the_cookie', 'the_value', { expires: 7 }); //设置带时间的cookie
$.cookie('the_cookie', '', { expires: -1 }); // 删除
$.cookie('the_cookie', null); // 删除 cookie
$.cookie(’the_cookie’, ‘the_value’, {expires: 7, path: ‘/’, domain: ‘jquery.com’, secure: true});//新建一个cookie 包括有效期 路径 域名等
**/
jQuery.cookie = function (name, value, options) {
    if (typeof value != 'undefined') {
        options = options || {};
        if (value === null) {
            value = '';
            options = $.extend({}, options);
            options.expires = -1;
        }
        var expires = '';
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == 'number') {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
            } else {
                date = options.expires;
            }
            expires = '; expires=' + date.toUTCString();
        }
        var path = options.path ? '; path=' + (options.path) : '';
        var domain = options.domain ? '; domain=' + (options.domain) : '';
        var secure = options.secure ? '; secure' : '';
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    } else {
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};





/************************************************JSON转化对象********************************************/

var JSON;
if (!JSON) {
    JSON = {};
}

(function () {
    function f(n) {
        return n < 10 ? '0' + n : n;
    }
    if (typeof Date.prototype.toJSON !== 'function') {
        Date.prototype.toJSON = function (key) {
            return isFinite(this.valueOf()) ?
                this.getUTCFullYear() + '-' +
                f(this.getUTCMonth() + 1) + '-' +
                f(this.getUTCDate()) + 'T' +
                f(this.getUTCHours()) + ':' +
                f(this.getUTCMinutes()) + ':' +
                f(this.getUTCSeconds()) + 'Z' : null;
        };
        String.prototype.toJSON =
            Number.prototype.toJSON =
            Boolean.prototype.toJSON = function (key) {
                return this.valueOf();
            };
    }
    var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
        escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
        gap,
        indent,
        meta = {    // table of character substitutions
            '\b': '\\b',
            '\t': '\\t',
            '\n': '\\n',
            '\f': '\\f',
            '\r': '\\r',
            '"': '\\"',
            '\\': '\\\\'
        },
        rep;
    function quote(string) {
        escapable.lastIndex = 0;
        return escapable.test(string) ? '"' + string.replace(escapable, function (a) {
            var c = meta[a];
            return typeof c === 'string' ? c :
                '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
        }) + '"' : '"' + string + '"';
    }
    function str(key, holder) {
        var i,          // The loop counter.
            k,          // The member key.
            v,          // The member value.
            length,
            mind = gap,
            partial,
            value = holder[key];
        if (value && typeof value === 'object' &&
                typeof value.toJSON === 'function') {
            value = value.toJSON(key);
        }
        if (typeof rep === 'function') {
            value = rep.call(holder, key, value);
        }
        switch (typeof value) {
            case 'string':
                return quote(value);
            case 'number':
                return isFinite(value) ? String(value) : 'null';

            case 'boolean':
            case 'null':
                return String(value);
            case 'object':
                if (!value) {
                    return 'null';
                }
                gap += indent;
                partial = [];
                if (Object.prototype.toString.apply(value) === '[object Array]') {
                    length = value.length;
                    for (i = 0; i < length; i += 1) {
                        partial[i] = str(i, value) || 'null';
                    }
                    v = partial.length === 0 ? '[]' : gap ?
                    '[\n' + gap + partial.join(',\n' + gap) + '\n' + mind + ']' :
                    '[' + partial.join(',') + ']';
                    gap = mind;
                    return v;
                }
                if (rep && typeof rep === 'object') {
                    length = rep.length;
                    for (i = 0; i < length; i += 1) {
                        if (typeof rep[i] === 'string') {
                            k = rep[i];
                            v = str(k, value);
                            if (v) {
                                partial.push(quote(k) + (gap ? ': ' : ':') + v);
                            }
                        }
                    }
                } else {
                    for (k in value) {
                        if (Object.prototype.hasOwnProperty.call(value, k)) {
                            v = str(k, value);
                            if (v) {
                                partial.push(quote(k) + (gap ? ': ' : ':') + v);
                            }
                        }
                    }
                }
                v = partial.length === 0 ? '{}' : gap ?
                '{\n' + gap + partial.join(',\n' + gap) + '\n' + mind + '}' :
                '{' + partial.join(',') + '}';
                gap = mind;
                return v;
        }
    }
    if (typeof JSON.stringify !== 'function') {
        JSON.stringify = function (value, replacer, space) {
            var i;
            gap = '';
            indent = '';
            if (typeof space === 'number') {
                for (i = 0; i < space; i += 1) {
                    indent += ' ';
                }
            } else if (typeof space === 'string') {
                indent = space;
            }
            rep = replacer;
            if (replacer && typeof replacer !== 'function' &&
                    (typeof replacer !== 'object' ||
                    typeof replacer.length !== 'number')) {
                throw new Error('JSON.stringify');
            }
            return str('', { '': value });
        };
    }
    if (typeof JSON.parse !== 'function') {
        JSON.parse = function (text, reviver) {
            var j;

            function walk(holder, key) {
                var k, v, value = holder[key];
                if (value && typeof value === 'object') {
                    for (k in value) {
                        if (Object.prototype.hasOwnProperty.call(value, k)) {
                            v = walk(value, k);
                            if (v !== undefined) {
                                value[k] = v;
                            } else {
                                delete value[k];
                            }
                        }
                    }
                }
                return reviver.call(holder, key, value);
            }
            text = String(text);
            cx.lastIndex = 0;
            if (cx.test(text)) {
                text = text.replace(cx, function (a) {
                    return '\\u' +
                        ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
                });
            }
            if (/^[\],:{}\s]*$/
                    .test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@')
                        .replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']')
                        .replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) {
                j = eval('(' + text + ')');
                return typeof reviver === 'function' ?
                    walk({ '': j }, '') : j;
            }
            throw new SyntaxError('JSON.parse');
        };
    }
}());



/**************************************************************************************************************/
/***************************************************分页插件*************************************************/
/*
*参数说明
*options.recordCount 总数
*options.pageSize  每页大小
*options.pagenumber  当前页数
*options.customerText 是否自定义文本，默认不显示
*options.buttonClickCallback 分页的回调函数
* 用法 ：$("#mypager").pager({ pagenumber: pageclickednumber, recordCount: recordCount, pageSize: pageSize,cKey:value, buttonClickCallback: PageClick})
*/
; (function ($) {
    $.fn.pager = function (options) {

        if(options!=undefined && typeof options =="string"){
            
            if(options=="GetPageSize"){
                var pageSize=0;
                pageSize=$(this).find("select.input-small").val();
                return pageSize;
            }
            else if (options=="GetCurrent") {
                var pageIndex=1;
                pageIndex=$(this).find("ul.pager_items").find('li.active').attr("data");
                return pageIndex;
            }
        }
        else{
            var opts = $.extend({}, $.fn.pager.defaults, options);
            var pagecount = GetPageCount(parseInt(opts.recordCount), parseInt(opts.pageSize));
            opts.pagecount = pagecount;
            $(this).empty();
            $(this).append(renderpager(parseInt(opts.pagenumber), pagecount, parseInt(opts.pageSize), parseInt(opts.recordCount), opts.customerText, opts.buttonClickCallback));

            $(this).css({
                marginTop:"5px"
            });

            if (opts.buttonClickCallback != undefined && opts.buttonClickCallback != null) {
                $(this).find(".pager_items").children("li").click(function () {
                    var data = $(this).attr("data");
                    if (data > 0) {
                        opts.buttonClickCallback(data, opts.pageSize);
                    }
                });

                $(this).find("select.input-small").change(function () {
                    var size = $(this).val();
                    opts.buttonClickCallback(1, size);
                });
            }
        }
    };

    //获得总页数
    function GetPageCount(recordCount, pageSize) {
        if (recordCount <= 0) return 1;
        if (recordCount % pageSize == 0)
            return parseInt(recordCount / pageSize);
        else
            return parseInt(recordCount / pageSize) + 1;
    }
    //pagenumber
    function renderpager(pagenumber, pagecount, pageSize, recordCount, customerText, buttonClickCallback) {
        var items = [1,2,3,4, 5, 10, 15, 20, 30, 50];
        var $pager = "";
        
        $pager+='<div class="span2">';
        $pager+='<div class="control-group">';
        $pager+='<div class="controls">';
        $pager+='共'+recordCount+'行,每页';
        $pager+='<select class="input-small" style="width:60px">';
        for (var i = 0; i < items.length; i++) {
            if (items[i] == pageSize) {
                $pager += "<option selected=\"selected\" value=\"" + items[i] + "\">" + items[i] + "</option>";
            } else {
                $pager += "<option value=\"" + items[i] + "\">" + items[i] + "</option>";
            }
        }
        $pager+='</select>行';
        $pager+='</div>';
        $pager+='</div>';
        $pager+='</div>';


        $pager += "<div class=\"span10\">";
        $pager += "<div class=\"dataTables_paginate paging_bootstrap pagination\" >";
        $pager += "<ul class='pager_items'>";

        if (pagenumber <= 1) {
            $pager += "<li class=\"prev disabled\" data=\"1\"><a href=\"javascript:void(0)\">首页</a></li>";
        } else {
            $pager += "<li class=\"prev\" data=\"1\"><a href=\"javascript:void(0)\">首页</a></li>";
        }

        var start = pagenumber - 3;
        start = start <= 1 ? 1 : start;
        if (start > 1) {
            $pager += "<li data=\"0\"><a href=\"javascript:void(0)\">...</a></li>";
        }
        for (var i = start; i < pagenumber; i++) {
            $pager += "<li data=\"" + i + "\"><a href=\"javascript:void(0)\">" + i + "</a></li>";
        }
        $pager += "<li class=\"active\" data=\"" + pagenumber + "\"><a href=\"javascript:void(0)\">" + pagenumber + "</a></li>";

        var end = pagenumber + 3;
        end = end >= pagecount ? pagecount : end;

        for (var i = pagenumber + 1; i <= end; i++) {
            $pager += "<li data=\"" + i + "\"><a href=\"javascript:void(0)\">" + i + "</a></li>";
        }
        if (pagecount > end) {
            $pager += "<li data=\"0\"><a href=\"javascript:void(0)\">...</a></li>";
        }

        if (pagenumber >= pagecount) {
            $pager += "<li class=\"next disabled\" data=\"" + pagecount + "\"><a href=\"javascript:void(0)\">尾页</a></li>";
        } else {
            $pager += "<li class=\"next\" data=\"" + pagecount + "\"><a href=\"javascript:void(0)\">尾页</a></li>";
        }
        $pager += "</ul>";
        $pager += "</div>";
        $pager += "</div>";
        return $pager;
    }


    function GetPageNum() {
        alert("请输入要跳转的页码！");
    }

    $.fn.pager.defaults = {
        pagenumber: 1,
        pagecount: 1,
        recordCount: 0,
        pageSize: 10
    };
})(jQuery);



; (function ($) {
    $.fn.minpager = function (options) {
        if(options!=undefined && typeof options =="string"){
            
            if(options=="GetPageSize"){
                var pageSize=0;
                pageSize=$(this).find("select.input-small").val();
                return pageSize;
            }
            else if (options=="GetCurrent") {
                var pageIndex=1;
                pageIndex=$(this).find("ul.pager_items").find('li.active').attr("data");
                return pageIndex;
            }
        }
        else{
            var opts = $.extend({}, $.fn.pager.defaults, options);
            var pagecount = GetPageCount(parseInt(opts.recordCount), parseInt(opts.pageSize));
            opts.pagecount = pagecount;
            $(this).empty();
            $(this).append(renderpager(parseInt(opts.pagenumber), pagecount, parseInt(opts.pageSize), parseInt(opts.recordCount), opts.customerText, opts.buttonClickCallback));

            $(this).css({
                marginTop:"5px"
            });

            if (opts.buttonClickCallback != undefined && opts.buttonClickCallback != null) {
                $(this).find(".pager_items").children("li").click(function () {
                    var data = $(this).attr("data");
                    if (data > 0) {
                        opts.buttonClickCallback(data, opts.pageSize);
                    }
                });

                $(this).find("select.input-small").change(function () {
                    var size = $(this).val();
                    opts.buttonClickCallback(1, size);
                });
            }
        }
    };

    //获得总页数
    function GetPageCount(recordCount, pageSize) {
        if (recordCount <= 0) return 1;
        if (recordCount % pageSize == 0)
            return parseInt(recordCount / pageSize);
        else
            return parseInt(recordCount / pageSize) + 1;
    }
    //pagenumber
    function renderpager(pagenumber, pagecount, pageSize, recordCount, customerText, buttonClickCallback) {
        var items = [1,2,3,4, 5, 10, 15, 20, 30, 50];
        var $pager = "";
        
        $pager += "<div class=\"span12\">";
        $pager += "<div class=\"dataTables_paginate paging_bootstrap pagination\" >";
        $pager += "<ul class='pager_items'>";

        if (pagenumber <= 1) {
            $pager += "<li class=\"prev disabled\" data=\"1\"><a href=\"javascript:void(0)\">首页</a></li>";
        } else {
            $pager += "<li class=\"prev\" data=\"1\"><a href=\"javascript:void(0)\">首页</a></li>";
        }

        var start = pagenumber - 3;
        start = start <= 1 ? 1 : start;
        if (start > 1) {
            $pager += "<li data=\"0\"><a href=\"javascript:void(0)\">...</a></li>";
        }
        for (var i = start; i < pagenumber; i++) {
            $pager += "<li data=\"" + i + "\"><a href=\"javascript:void(0)\">" + i + "</a></li>";
        }
        $pager += "<li class=\"active\" data=\"" + pagenumber + "\"><a href=\"javascript:void(0)\">" + pagenumber + "</a></li>";

        var end = pagenumber + 3;
        end = end >= pagecount ? pagecount : end;

        for (var i = pagenumber + 1; i <= end; i++) {
            $pager += "<li data=\"" + i + "\"><a href=\"javascript:void(0)\">" + i + "</a></li>";
        }
        if (pagecount > end) {
            $pager += "<li data=\"0\"><a href=\"javascript:void(0)\">...</a></li>";
        }

        if (pagenumber >= pagecount) {
            $pager += "<li class=\"next disabled\" data=\"" + pagecount + "\"><a href=\"javascript:void(0)\">尾页</a></li>";
        } else {
            $pager += "<li class=\"next\" data=\"" + pagecount + "\"><a href=\"javascript:void(0)\">尾页</a></li>";
        }
        $pager += "</ul>";
        $pager += "</div>";
        $pager += "</div>";
        return $pager;
    }


    function GetPageNum() {
        alert("请输入要跳转的页码！");
    }

    $.fn.pager.defaults = {
        pagenumber: 1,
        pagecount: 1,
        recordCount: 0,
        pageSize: 10
    };
})(jQuery);


/***************************************************封装Ajax请求 开始*************************************************/
jQuery.extend({
    gitAjax: function (options) {
        var defaultOptions = {
            url: undefined,
            data: undefined,
            type: "post",
            dataType: "json",
            async: true,
            success: function (result) { }
        };
        defaultOptions = $.extend(defaultOptions, options);

        //添加随机参数

        if (defaultOptions.url.indexOf("?") > 0) {
            defaultOptions.url = defaultOptions.url + "&t=" + Math.random();
        } else {
            defaultOptions.url = defaultOptions.url + "?t=" + Math.random();
        }
        if (defaultOptions.data == undefined) {
            defaultOptions.data = {};
        }
        $.ajax({
            url: defaultOptions.url,
            data: defaultOptions.data,
            type: defaultOptions.type,
            dataType: defaultOptions.dataType,
            async: defaultOptions.async,
            success: function (result) {
                if ("4"==result.Code) {
                    var submit = function (v, h, f) {
                        if (v == "ok") {
                            window.location.href = "/Home/Index?returnurl=" + git.GetPath();
                        }
                    }
                    $.jBox.confirm("您已经退出系统，请重新登录再操作", "温馨提示", submit);
                    return false;
                } else if ("2"==result.Code) {
                    $.jBox.error("您没有任何权限操作该项", "温馨提示");
                    return false;
                } else {
                    if (defaultOptions.success != undefined && defaultOptions.success != null) {
                        defaultOptions.success(result);
                    }
                }
            }
        });
    }
});
