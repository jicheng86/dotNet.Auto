$(function () {
    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();
    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();
});

//初始化Table list数据
var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_list').bootstrapTable({
            // data: [],//要加载的数据。
            url: '/Corporation/ListPageData', //从远程站点请求数据的URL。     请注意，所需的服务器响应格式取决于是否'sidePagination' 指定了该选项
            method: 'post', //请求方式,默认:'get'，可选"post"...
            toolbar: '#toolbar', //工具按钮用哪个容器
            toolbarAlign: "left", //指示如何对齐自定义工具栏。'left'(默认), 'right'可以使用。
            buttonsToolbar: "",//jQuery选择，指示自定义按钮工具栏，例如：#buttons-toolbar, .buttons-toolbar，或DOM节点。默认: undefined
            buttonsAlign: "right",//指示如何对齐工具栏按钮。 'left', 'right'(默认)可以使用。
            buttonsOrder: ['paginationSwitch', 'refresh', 'toggle', 'detailOpen', 'detailClose', 'columns'],//指示如何自定义工具栏按钮的顺序。 默认: ['paginationSwitch', 'refresh', 'toggle', 'fullscreen', 'columns']
            icons: {
                paginationSwitchDown: 'fa-caret-square-down',
                paginationSwitchUp: 'fa-caret-square-up',
                refresh: 'fa-sync',
                toggleOff: 'fa-toggle-off',
                toggleOn: 'fa-toggle-on',
                columns: 'fa-th-list',
                fullscreen: 'fa-arrows-alt',
                detailOpen: 'fa-plus',
                detailClose: 'fa-minus'
            },
            //定义在工具栏，分页和详细信息视图中使用的图标。
            //默认:{
            //    paginationSwitchDown: 'fa-caret-square-down',
            //    paginationSwitchUp: 'fa-caret-square-up',
            //    refresh: 'fa-sync',
            //    toggleOff: 'fa-toggle-off',
            //    toggleOn: 'fa-toggle-on',
            //    columns: 'fa-th-list',
            //    fullscreen: 'fa-arrows-alt',
            //    detailOpen: 'fa-plus',
            //    detailClose: 'fa-minus'
            //}
            striped: true, //是否显示行间隔色
            cache: false, //是否使用缓存，默认为true，设置false：禁用AJAX请求的缓存。
            contentType: "application/json", //请求远程数据的contentType，例如：application/x-www-form-urlencoded. 默认: 'application/json'
            dataType: "json",//您期望从服务器返回的数据类型。默认: 'json'
            //ajax: undefined,// Function 类型， 一种替换ajax调用的方法。应该实现与jQuery ajax方法相同的API。
            //ajaxOptions: {},//默认:{}
            sortable: true, //是否启用排序
            sortOrder: "desc", //排序方式,默认"asc"
            silentSort: "true",//设置false 为使用加载消息对数据进行排序。当sidePagination选项设置为"'server'"时，此选项有效 .默认: true
            queryParams: oTableInit.queryParams, //请求远程数据时，可以通过修改queryParams发送其他参数。            如果 queryParamsType = 'limit'，params对象包含：limit, offset, search, sort, order.   否则，它包含：pageSize, pageNumber, searchText, sortName, sortOrder. 返回 false停止请求。       默认: function (params) { return params }
            responseHandler: function (res) {
                return res;
            },//默认： function(res) { return res }
            queryParamsType: "", //默认: 'limit'  "" = {pageSize:10, pageNumber:1, searchText, sortName, sortOrder}
            sidePagination: "server", //分页方式：client客户端分页，server服务端分页定义, 使用 'server'side需要设置'url' 或 'ajax' 选项。 请注意，根据 'sidePagination' 选项设置为 'client' 还是，所需的服务器响应格式会有所不同 'server'。
            pagination: true, //是否显示分页（*）
            pageNumber: 1, //初始化加载第一页，默认第一页
            pageSize: 10, //每页的记录行数（*）
            pageList: [10, 25, 50, 100, 200], //默认:[10, 25, 50, 100],可供选择的每页的行数（*）
            paginationSuccessivelySize: 5,//可选页码数量。如："<①②③④⑤>"
            paginationPagesBySide: 1,
            paginationUseIntermediate: true,//计算并显示中间页面以便快速访问。
            search: true,   //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            searchOnEnterKey: true,//按下enter健才搜索
            strictSearch: true, //启用严格搜索。禁用比较检查。默认: false
            showColumns: true, //是否显示所有的列
            showRefresh: true, //是否显示刷新按钮
            showColumnsToggleAll: true,//设置true 为在列选项/下拉列表中显示“全部切换”复选框。
            showFullscreen: true,//设置true显示全屏按钮。
            minimumCountColumns: 2, //最少允许的列数
            escape: false,//转义用于插入HTML的字符串，并替换 &, <, >, “, `, and ‘ 字符。默认: false
            clickToSelect: true, //是否启用点击选中行
            checkboxHeader: true,//设置false为隐藏标题行中的所有复选框。默认: true
            singleSelect: true, //禁止多选
            cardView: false,//设置true 为显示名片视图表，例如移动视图。默认: false
            detailView: false,//设置true为显示详细视图表。默认: false
            detailViewByClick: false,//设置true单击以设置切换细节视图。默认: false
            height: 500,      //表的高度，启用表的固定标题。
            classes: "table table-bordered table-hover",//使用classes选项设置表格样式。默认classes 值为 'table table-bordered table-hover'.
            theadClasses: "", //使用theadClasses选项设置表标题样式。三种模式  undefined(默认)  .thead-light 或 .thead-dark 
            headerStyle: function (column) {
                return {
                    css: { 'font-weight': 'bolder' } //,
                   // classes: 'red'
                };
            }, //标头样式格式化程序函数采用一个参数
            rowStyle: function (row, index) {
                var classes = ['active', 'success', 'info', 'warning', 'danger'];
                if (index % 2 === 0 && index / 2 < classes.length) {
                    return {
                        classes: classes[index / 2]
                    };
                }
                return {
                    //css: {
                    //    color: 'green'
                    //}
                };


            },//行样式
            locale: "zh-CN",//本地语言
            uniqueId: "id", //每一行的唯一标识，一般为主键列
            iconsPrefix: "fa",//定义图标集名称（'glyphicon' 或 'fa'）。默认情况下'fa'用于Bootstrap v4。
            showToggle: true, //是否显示详细视图和列表视图的切换按钮
            columns: [
                {
                    checkbox: true
                }, {
                    field: 'fullName',
                    title: '公司名称',
                    visible: false
                }, {
                    field: 'name',
                    title: '公司简称',
                    formatter: function (value, row, index) {
                        return value;
                    }
                }, {
                    field: 'businessLicense', 
                    title: '营业执照'
                }, {
                    field: 'telephone',
                    title: '公司联系电话',
                    visible: false
                }, {
                    field: 'legalPerson',
                    title: '法人',
                    visible: false
                }, {
                    field: 'legalPersonIDCardNo',
                    title: '法人证件号',
                    visible: false
                }, {
                    field: 'legalPersonPhone',
                    title: '法人电话',
                    visible: false
                }, {
                    field: 'faxNumber',
                    title: '公司传真',
                    visible: false
                }, {
                    field: 'creationUserID',
                    title: '创建者',
                    visible: false
                }, {
                    field: 'creationTime',
                    title: '创建时间',
                    visible: false,
                    formatter: function (value, row, index) {
                        return jsonDateFormatting(value, 'yyyy-mm-dd hh:mm:ss');
                    }
                }, {
                    field: 'isActive',
                    title: '是否有效',
                    formatter: function (value, row, index) {
                        if (row.IsActive && row.IsDeleted) {
                            return '是';
                        }
                        else {
                            return '否';
                        }
                    }
                }, {
                    field: 'opid',
                    title: '操作',
                    formatter: function (value, row, index) {
                        var button = '<button id="btnUpdateCustomer" class="btn btn-xs btn-primary" style="margin-left:5px;" onclick="UpdateCustomer(' + row.ID + ')">详情</button>';
                        return button;
                    }
                }
            ],
            formatNoMatches: function () { //没有匹配的结果
                return '没有找到匹配的记录';
            }
        });
    };
    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var fromPage = {
            //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            //queryParamsType:"", pageSize:10, pageNumber:1, searchText, sortName, sortOrder
            pageSize: params.pageSize, //页面大小
            pageNumber: params.pageNumber, //当前页面及之前数据总和 
            searchText: params.searchText,//查询字符串
            sortName: params.sortName,//排序名称
            sortOrder: params.sortOrder//排序顺序
        };
        return fromPage;
    };
    return oTableInit;
};
//初始化按钮方法
var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        //初始化页面上面的按钮事件
        $('#btnCreation').click(function () {
            window.location.href="/Corporation/Creation"
        });
        //修改按钮事件注册
        $('#btnUpdate').click(function () {
            var row = $('#tb_list').bootstrapTable('getSelections')[0];
            if (row) {
                window.location.href = '/Customer/CustomerEdit?CustomerId=' + row.Cst_ID;
            } else {
                swal({ title: "请选择一条数据！", icon: "warning", button: "确定" });
            }
        });
        //3.列表查询
        $('#btnSearch').click(function () {
            //将当前页初始化第一页
            $('#tb_list').bootstrapTable('refreshOptions', { pageNumber: 1 });
            //列表刷新
            $('#tb_list').bootstrapTable('refresh');
        });

    };
    return oInit;
};