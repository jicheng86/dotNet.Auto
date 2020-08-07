$(function () {
    bvCorporationCreation();
    loadingAreaData();
    //初始化按钮注册事件
    var btnInit = new ButtonInit();
    btnInit.Init();

});

var ButtonInit = function () {
    var oInit = new Object();
    oInit.Init = function () {
        //初始化页面上面的按钮事件
        var FormCorporationCreation = $("#FormCorporationCreation");
        //按钮点击事件注册;
        /*
         * 新增公司提交事件注册
         */
        $('#btnSubmit').click(function () {
            var isValid = FormCorporationCreation.data('bootstrapValidator').validate().isValid();
            if (isValid) {
                //触发from提交事件
                FormCorporationCreation.ajaxSubmit({
                    // $.ajax({
                    //url: "/Corporation/Creation",          //默认是form的action， 如果声明，则会覆盖  
                    //type: "post",      //默认是form的method（get or post），如果申明，则会覆盖  
                    //data: FormCorporationCreation.serialize(),//序列化表单，$("form").serialize()只能序列化数据，不能序列化文件
                    dataType: 'json',               //html(默认), xml, script, json...接受服务端返回的类型  
                    timeout: 8 * 1000,              //请求的超时时间：5秒  
                    clearForm: false,               //成功提交后，清除所有表单元素的值  
                    resetForm: false,               //成功提交后，重置所有表单元素的值  
                    processData: false,             //默认情况下，processData 的值是 true，其代表以对象的形式上传的数据都会被转换为字符串的形式上传。而当上传文件的时候，则不需要把其转换为字符串，因此上传文件需要改成false
                    contentType: false,             //前端发送数据的格式, 默认值："application/x-www-form-urlencoded;charset=UTF-8" 代表的是 ajax 的 data 是以键值对字符串的形式,使用这种传数据的格式，无法传输复杂的数据，比如多维数组、文件等。若 form 标签中设置了enctype = “multipart/form-data”,这样请求中的 contentType 就会默认为 multipart/form-data 。而我们在 ajax 中 contentType 设置为 false 是为了避免 JQuery 对其操作。
                    beforeSubmit: function () {
                        //提交之前处理
                        openloading();
                    },
                    //beforeSend: function () {
                    //    //提交之前处理
                    //    openloading();
                    //},
                    success: function (resultData) {
                        var olderrors = $('.field-validation-error');
                        if (resultData.code == 4) {
                            if (!IsUndefinedOrNull(resultData.data.areaIDs)) {
                                loadingAreaData(resultData.data.areaIDs);
                            }
                            var newerrors = resultData.data.modelErrors;
                            //后端校验提示
                            if (!IsUndefinedOrNull(newerrors)) {
                                //当前后台校验不通过
                                var Processed = false;
                                if (olderrors.length > 0) {
                                    $('#divModelErrors').empty();
                                    $('#divModelErrors').addClass('hidden');
                                    olderrors.each(function (index, element) {
                                        Processed = false;
                                        $.each(newerrors, function (key, value) {
                                            if (!IsUndefinedOrNull(key)) {
                                                if ($(element).attr('data-valmsg-for') == key) {
                                                    Processed = true;
                                                    return false;//break
                                                }
                                                else {
                                                    Processed = false;
                                                    return true; //continue
                                                }
                                            } else {
                                                var error = "<div data-valmsg-summary='true' class='col-sm-9 col-sm-offset-2 text-danger validation-summary-errors'><ul><li>" + value + "</li></ul ></div >";
                                                $('#divModelErrors').append(error).removeClass('hidden');
                                            }
                                        });
                                        if (!Processed) {
                                            $(element).addClass('field-validation-valid');
                                            $(element).removeClass('field-validation-error');
                                            $(element).html('');
                                        }
                                    });
                                }
                                $.each(newerrors, function (key, value) {
                                    if (!IsUndefinedOrNull(key)) {
                                        var selector = $('span[data-valmsg-for=' + key + ']');
                                        selector.removeClass('field-validation-valid');
                                        selector.addClass('field-validation-error');
                                        selector.html(value);
                                    } else {
                                        $('#divModelErrors').empty();
                                        var error = "<ul><li> " + value + "</li></ul>";
                                        $('#divModelErrors').append(error).removeClass('hidden');
                                    }
                                });
                            }
                            else {
                                //modelstate校验通过，隐藏modelstate错误
                                $('#divModelErrors').empty();
                                $('#divModelErrors').addClass('hidden');
                                olderrors.each(function (index, element) {
                                    $(element).removeClass('field-validation-error');
                                    $(element).addClass('field-validation-valid');
                                    $(element).html('');
                                });
                            }
                            layer.alert(resultData.message, { icon: 2, title: "操作提示", time: 5 * 1000, anim: 0, shadeClose: false, shade: [0.38, '#393D49'] }, function (index) {
                                //do something
                                layer.close(index);
                            });
                        }
                        else {
                            //modelstate校验通过，隐藏modelstate错误
                            $('#divModelErrors').empty();
                            $('#divModelErrors').addClass('hidden');
                            if (olderrors.length > 0) {
                                olderrors.each(function (index, element) {
                                    $(element).removeClass('field-validation-error');
                                    $(element).addClass('field-validation-valid');
                                    $(element).html('');
                                });
                            }
                            // successful message
                            layer.alert(resultData.message, { icon: 1, title: "操作提示", time: 5 * 1000, anim: 0, shadeClose: false, shade: [0.38, '#393D49'] }, function (index) {
                                //do something
                                layer.close(index);
                                window.location.href = "/Corporation/List";
                            });
                        }
                    },
                    complete: function () {
                        //方法完成处理
                        closeAllloading();
                    },
                    error: function (resultData, txtState) {
                        //方法异常处理
                        // layer.msg("请求异常！" + txtState + resultData.message, { time: 3 * 1000, icon: 2 });
                        layer.alert("请求异常！状态：" + txtState + "。返回信息：" + resultData.message, { icon: resultData.code == 1 ? 1 : 2, time: 5 * 1000, anim: 0, shadeClose: false, shade: [0.38, '#393D49'] }, function (index) {
                            //do something
                            layer.close(index);
                        });
                        closeAllloading();
                    }
                });
            };
            return false; //阻止表单默认提交  

        });

        /*
         * 省级地址选择事件联动加载
         */
        $('#seltProvinceAreaID').change(function () {
            loadingAreasOptions(false, "seltProvinceAreaID", "seltCityAreaID");
        });
        /*
        * 市级地址选择联动加载
        */
        $('#seltCityAreaID').change(function () {
            loadingAreasOptions(false, "seltCityAreaID", "seltCistrictAreaID");
        });
        /*
        * 县/区级地址选择事件联动加载
        */
        $('#seltCistrictAreaID').change(function () {
            loadingAreasOptions(false, "seltCistrictAreaID", "seltStreetAreaID");
        });
        /*
         * 选择街道选择事件联动加载
         */
        $('#seltStreetAreaID').change(function () {
            loadingAreasOptions(false, "seltStreetAreaID");
        });

    };
    return oInit;
};
var FormCorporationCreation = $('#FormCorporationCreation');
//预加载验证表单设置
var bvCorporationCreation = function () {
    FormCorporationCreation.bootstrapValidator({
        excluded: [":disabled"],
        message: '这个值没有被验证',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            Name: {
                validators: {
                    notEmpty: { message: '请输入公司简称' }
                    , stringLength: {
                        min: 2,
                        max: 50,
                        message: '请填写2-50个字符'
                    }
                }
            },
            FullName: {
                validators: {
                    notEmpty: { message: '请输入公司名称' }
                    , stringLength: {
                        min: 5,
                        max: 50,
                        message: '请填写5-50个字符'
                    }
                }
            },
            BusinessLicense: {
                validators: {
                    notEmpty: { message: '请输入公司公司营业执照/组织机构代码/税务登记编号' }
                    , stringLength: {
                        min: 10,
                        max: 20,
                        message: '请填写10-20个字符'
                    }
                }
            },
            LegalPerson: {
                validators: {
                    notEmpty: { message: '请输入公司法人名称' }
                    , stringLength: {
                        min: 2,
                        max: 50,
                        message: '请填写2-50个字符'
                    }
                }
            },
            LegalPersonIDCardNo: {
                message: '必填',
                validators: {
                    notEmpty: { message: '请输入公司法人证件号' }
                    , stringLength: {
                        min: 5,
                        max: 50,
                        message: '请填写5-50个字符'
                    }
                }
            },
            LegalPersonPhone: {
                validators: {
                    notEmpty: { message: '请输入公司法人联系电话' }
                    , regexp: {
                        regexp: /^1[3-9]\d{9}$/,
                        message: '请输入正确的手机号码'
                    }
                }
            },
            Telephone: {
                validators: {
                    regexp: {
                        regexp: /^1[3-9]\d{9}|(0[0-9]{2,3}\-)([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/,
                        message: '请输入正确的联系号码'
                    }
                }
            },
            FaxNumber: {
                validators: {
                    regexp: {
                        regexp: /^(0[0-9]{2,3}\-)([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/,
                        message: '请输入正确的传真号码'
                    }
                }
            },
            SupportEMail: {
                validators: {
                    emailAddress: {
                        message: '请输入正确的邮箱地址'
                    }
                }
            },
            AreaID: {
                validators: {
                    notEmpty: { message: '请选择公司所在区划' }
                }
            },
            CorporationAddress: {
                validators: {
                    notEmpty: { message: '请填写公司所在区划之后的详细地址' },
                    stringLength: {
                        min: 5,
                        max: 200,
                        message: '详细地址请填写5-200个字符'
                    }
                }
            },
            Remarks: {
                validators: {
                    stringLength: {
                        min: 0,
                        max: 500,
                        message: '备注信息请填写0-500个字符'
                    }
                }
            }
        }
    });
};

//加载页面区划控件数据
var loadingAreaData = function (AreaIDs) {

    if (IsUndefinedOrNull(AreaIDs)) {
        /*
         * 页面载入时加载省级下拉菜单
         */
        loadingAreasOptions(true, "seltProvinceAreaID", "seltCityAreaID");
    }
    else {
        var AreaIDList = AreaIDs.split(',');
        if (AreaIDList.length == 2) {
            loadingAreasOptions(true, "seltProvinceAreaID", "seltCityAreaID", AreaIDList[0]);
            loadingAreasOptions(true, "seltCityAreaID", "seltCistrictAreaID", AreaIDList[1], AreaIDList[0]);
        }
        if (AreaIDList.length == 4) {
            loadingAreasOptions(true, "seltProvinceAreaID", "seltCityAreaID", AreaIDList[0]);
            loadingAreasOptions(true, "seltCityAreaID", "seltCistrictAreaID", AreaIDList[1], AreaIDList[0]);
            loadingAreasOptions(true, "seltCistrictAreaID", "seltStreetAreaID", AreaIDList[2], AreaIDList[1]);
            loadingAreasOptions(true, "seltStreetAreaID", "", AreaIDList[3], AreaIDList[2]);
        }
    }
};

