﻿//打开layui加载层
var openloading = function () {
    layer.load(0, { time: 10 * 1000 });
};
//关闭所有layui加载层
var closeAllloading = function () {
    layer.closeAll('loading');
};
//判断值是否是为空
var IsUndefinedOrNull = function (value) {
    if (value = undefined || value == null || value == '') {
        return true;
    }
    return false;
};


/*
 * json日期格式转换为正常格式
 * JsonDate类型: timestamp【/Date(1596600000)】 /本地时间【2020-08-03T12:00:00.0000000】
 */
function jsonDateFormatting(JsonDate, Format = 'yyyy-MM-dd hh:mm:ss') {
    try {
        var isTimestamp = false;
        if (JsonDate.indexOf("/Date(") > -1) {
            isTimestamp = true;
        }
        //2020-08-03T12:00:00.0000000
        var dateTime = new Date(parseInt(JsonDate.replace("/Date(", "").replace(")/", ""), 10));
        var year = isTimestamp ? date.getFullYear() : JsonDate.substring(0, 4);
        var month = isTimestamp ? dateTime.getMonth() + 1 < 10 ? "0" + (dateTime.getMonth() + 1) : dateTime.getMonth() + 1 : JsonDate.substring(5, 7);
        var day = isTimestamp ? dateTime.getDate() < 10 ? "0" + dateTime.getDate() : dateTime.getDate() : JsonDate.substring(8, 10);
        var hours = isTimestamp ? dateTime.getHours() < 10 ? "0" + dateTime.getHours() : dateTime.getHours() : JsonDate.substring(11, 13);
        var minutes = isTimestamp ? dateTime.getMinutes() < 10 ? "0" + dateTime.getMinutes() : dateTime.getMinutes() : JsonDate.substring(14, 16);
        var seconds = isTimestamp ? dateTime.getSeconds() < 10 ? "0" + dateTime.getSeconds() : dateTime.getSeconds() : JsonDate.substring(17, 19);
        var milliseconds = isTimestamp ? dateTime.getMilliseconds() : JsonDate.substring(20, 23);
        switch (Format) {
            case 'yyyy':
                return year;
            case 'yyyy-MM':
            case 'yyyy-mm':
                return year + "-" + month;
            case 'yyyy-MM-dd':
            case 'yyyy-mm-dd':
                return year + "-" + month + '-' + day;
            case 'yyyy-MM-dd hh:mm':
            case 'yyyy-mm-dd hh:mm':
                return year + "-" + month + '-' + day + ' ' + hours + ':' + minutes;
            case 'yyyy-MM-dd hh:mm:ss':
            case 'yyyy-mm-dd hh:mm:ss':
                return year + "-" + month + '-' + day + ' ' + hours + ':' + minutes + ':' + seconds;
            case 'yyyy-MM-dd hh:mm:ss.SSS':
            case 'yyyy-mm-dd hh:mm:ss.SSS':
                return year + "-" + month + '-' + day + ' ' + hours + ':' + minutes + ':' + seconds + "." + milliseconds;
            default:
                return year + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;
        }
        //return year + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;
    } catch (ex) {
        return "-";
    }
}



//区划地址选择select加载数据
var loadingAreasOptions = function (initialLoading, thisSelectID, nextSelectID, thisSelectedValue, thisParentIDValue) {
    var select = $('#' + thisSelectID);//当前操作ID
    if (initialLoading) {
        //隐藏所有select 
        $('.show-tick').selectpicker('hide');
        if (IsUndefinedOrNull(thisSelectedValue)) {
            thisSelectedValue = 0;
        }
        if (IsUndefinedOrNull(thisParentIDValue)) {
            thisParentIDValue = 1000;
        }

        //加载区划下拉菜单
        $.ajax({
            url: "/Area/GetAreasSelectOptions",
            type: "post",
            datatype: "json",
            data: { ParentID: thisParentIDValue, SelectedID: thisSelectedValue, HadEmptyItem: false },
            beforeSend: function () {
                $('.show-tick').selectpicker('hide');
                select.selectpicker('show');
                openloading();
            },
            success: function (jsondata) {
                if (jsondata.code == 1) {
                    select.empty();
                    select.append(jsondata.data);
                    select.selectpicker('show');
                    if (thisSelectedValue != 0) {
                        var sid = select.find("option:selected").val();
                    }
                    select.selectpicker('refresh');
                    if (thisSelectedValue != 0) {
                        select.selectpicker('val', sid);
                    }
                }
            },
            complete: function () {
                closeAllloading();
            }
        });
    }
    else {
        if (IsUndefinedOrNull(thisSelectedValue)) {
            var selectedID = select.selectpicker('val');
            select.selectpicker('val', IsUndefinedOrNull(thisSelectedValue) ? selectedID : thisSelectedValue);
            select.selectpicker('refresh');
        }
        else {
            var selectedID = 1000;
        }
        var next = null;
        if (!IsUndefinedOrNull(nextSelectID)) {
            next = $('#' + nextSelectID);//关联下级ID
        }
        $.ajax({
            url: "/Area/GetAreasSelectOptions",
            type: "post",
            async: true,
            datatype: "json",
            data: { ParentID: selectedID, SelectedID: thisSelectedValue },
            beforeSend: function () {
                select.prop('disabled', true);
                select.selectpicker('refresh');
                $('.show-tick').selectpicker('hide');
                select.selectpicker('show');
                select.parent().prevAll().find('select').selectpicker('show');
                openloading();
            },
            success: function (jsondata) {
                if (jsondata.code == 1 && next != null) {
                    if (IsUndefinedOrNull(thisSelectedValue)) {
                        next.empty();
                        next.append(jsondata.data);
                        next.selectpicker('show');
                        next.selectpicker('render');
                        next.selectpicker('refresh');
                    }
                }
            },
            complete: function () {
                select.prop('disabled', false);
                select.selectpicker('refresh');
                closeAllloading();
            }
        });
    }
};


//初始化图片上传组件
//var webuploadInitialize = function (uploadUrl = '/Corportion/Creation', autoUpload = false, swfSrc = "~/lib/webuploader/v0.1.8/dist/Uploader.swf", acceptTitle = "Images", acceptExtensions = "gif,jpg,jpeg,bmp,png", acceptMimeTypes = "image/*", Seletor = "#filePicker", innerHTML = "请选择文件", multiple = true, method = "POST") {
//    var $wrap = $('#uploader'),

//        // 图片容器
//        $queue = $('<ul class="filelist"></ul>')
//            .appendTo($wrap.find('.queueList')),

//        // 状态栏，包括进度和控制按钮
//        $statusBar = $wrap.find('.statusBar'),

//        // 文件总体选择信息。
//        $info = $statusBar.find('.info'),

//        // 上传按钮
//        $upload = $wrap.find('.uploadBtn'),

//        // 没选择文件之前的内容。
//        $placeHolder = $wrap.find('.placeholder'),

//        $progress = $statusBar.find('.progress').hide(),

//        // 添加的文件数量
//        fileCount = 0,

//        // 添加的文件总大小
//        fileSize = 0,

//        // 优化retina, 在retina下这个值是2
//        ratio = window.devicePixelRatio || 1,

//        // 缩略图大小
//        thumbnailWidth = 110 * ratio,
//        thumbnailHeight = 110 * ratio,

//        // 可能有pedding, ready, uploading, confirm, done.
//        state = 'pedding',

//        // 所有文件的进度信息，key为file id
//        percentages = {},
//        // 判断浏览器是否支持图片的base64
//        isSupportBase64 = (function () {
//            var data = new Image();
//            var support = true;
//            data.onload = data.onerror = function () {
//                if (this.width != 1 || this.height != 1) {
//                    support = false;
//                }
//            };
//            data.src = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==";
//            return support;
//        })(),

//        // 检测是否已经安装flash，检测flash的版本
//        flashVersion = (function () {
//            var version;

//            try {
//                version = navigator.plugins['Shockwave Flash'];
//                version = version.description;
//            } catch (ex) {
//                try {
//                    version = new ActiveXObject('ShockwaveFlash.ShockwaveFlash')
//                        .GetVariable('$version');
//                } catch (ex2) {
//                    version = '0.0';
//                }
//            }
//            version = version.match(/\d+/g);
//            return parseFloat(version[0] + '.' + version[1], 10);
//        })(),

//        supportTransition = (function () {
//            var s = document.createElement('p').style,
//                r = 'transition' in s ||
//                    'WebkitTransition' in s ||
//                    'MozTransition' in s ||
//                    'msTransition' in s ||
//                    'OTransition' in s;
//            s = null;
//            return r;
//        })(),

//        // WebUploader实例
//        uploader;

//    if (!WebUploader.Uploader.support('flash') && WebUploader.browser.ie) {

//        // flash 安装了但是版本过低。
//        if (flashVersion) {
//            (function (container) {
//                window['expressinstallcallback'] = function (state) {
//                    switch (state) {
//                        case 'Download.Cancelled':
//                            alert('您取消了更新！');
//                            break;

//                        case 'Download.Failed':
//                            alert('安装失败');
//                            break;

//                        default:
//                            alert('安装已成功，请刷新！');
//                            break;
//                    }
//                    delete window['expressinstallcallback'];
//                };

//                var swf = './expressInstall.swf';
//                // insert flash object
//                var html = '<object type="application/' +
//                    'x-shockwave-flash" data="' + swf + '" ';

//                if (WebUploader.browser.ie) {
//                    html += 'classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" ';
//                }

//                html += 'width="100%" height="100%" style="outline:0">' +
//                    '<param name="movie" value="' + swf + '" />' +
//                    '<param name="wmode" value="transparent" />' +
//                    '<param name="allowscriptaccess" value="always" />' +
//                    '</object>';

//                container.html(html);

//            })($wrap);

//            // 压根就没有安转。
//        } else {
//            $wrap.html('<a href="http://www.adobe.com/go/getflashplayer" target="_blank" border="0"><img alt="get flash player" src="http://www.adobe.com/macromedia/style_guide/images/160x41_Get_Flash_Player.jpg" /></a>');
//        }

//        return;
//    } else if (!WebUploader.Uploader.support()) {
//        alert('Web Uploader 不支持您的浏览器！');
//        return;
//    }

//    // 实例化
//    uploader = WebUploader.create({
//        pick: {
//            id: '#filePicker',
//            label: '点击选择图片'
//        },
//        formData: {
//            uid: 123
//        },
//        dnd: '#dndArea',
//        paste: '#uploader',
//        swf: swfSrc,
//        chunked: false,
//        chunkSize: 512 * 1024,
//        server: uploadUrl,
//        // runtimeOrder: 'flash',

//        // accept: {
//        //     title: 'Images',
//        //     extensions: 'gif,jpg,jpeg,bmp,png',
//        //     mimeTypes: 'image/*'
//        // },

//        // 禁掉全局的拖拽功能。这样不会出现图片拖进页面的时候，把图片打开。
//        disableGlobalDnd: true,
//        fileNumLimit: 300,
//        fileSizeLimit: 200 * 1024 * 1024,    // 200 M
//        fileSingleSizeLimit: 50 * 1024 * 1024    // 50 M
//    });

//    // 拖拽时不接受 js, txt 文件。
//    uploader.on('dndAccept', function (items) {
//        var denied = false,
//            len = items.length,
//            i = 0,
//            // 修改js类型
//            unAllowed = 'text/plain;application/javascript ';

//        for (; i < len; i++) {
//            // 如果在列表里面
//            if (~unAllowed.indexOf(items[i].type)) {
//                denied = true;
//                break;
//            }
//        }

//        return !denied;
//    });

//    uploader.on('dialogOpen', function () {
//        console.log('here');
//    });

//    // uploader.on('filesQueued', function() {
//    //     uploader.sort(function( a, b ) {
//    //         if ( a.name < b.name )
//    //           return -1;
//    //         if ( a.name > b.name )
//    //           return 1;
//    //         return 0;
//    //     });
//    // });

//    // 添加“添加文件”的按钮，
//    uploader.addButton({
//        id: '#filePicker2',
//        label: '继续添加'
//    });

//    uploader.on('ready', function () {
//        window.uploader = uploader;
//    });

//    // 当有文件添加进来时执行，负责view的创建
//    function addFile(file) {
//        var $li = $('<li id="' + file.id + '">' +
//            '<p class="title">' + file.name + '</p>' +
//            '<p class="imgWrap"></p>' +
//            '<p class="progress"><span></span></p>' +
//            '</li>'),

//            $btns = $('<div class="file-panel">' +
//                '<span class="cancel">删除</span>' +
//                '<span class="rotateRight">向右旋转</span>' +
//                '<span class="rotateLeft">向左旋转</span></div>').appendTo($li),
//            $prgress = $li.find('p.progress span'),
//            $wrap = $li.find('p.imgWrap'),
//            $info = $('<p class="error"></p>'),

//            showError = function (code) {
//                switch (code) {
//                    case 'exceed_size':
//                        text = '文件大小超出';
//                        break;

//                    case 'interrupt':
//                        text = '上传暂停';
//                        break;

//                    default:
//                        text = '上传失败，请重试';
//                        break;
//                }

//                $info.text(text).appendTo($li);
//            };

//        if (file.getStatus() === 'invalid') {
//            showError(file.statusText);
//        } else {
//            // @todo lazyload
//            $wrap.text('预览中');
//            uploader.makeThumb(file, function (error, src) {
//                var img;

//                if (error) {
//                    $wrap.text('不能预览');
//                    return;
//                }

//                if (isSupportBase64) {
//                    img = $('<img src="' + src + '">');
//                    $wrap.empty().append(img);
//                } else {
//                    $.ajax('../../server/preview.php', {
//                        method: 'POST',
//                        data: src,
//                        dataType: 'json'
//                    }).done(function (response) {
//                        if (response.result) {
//                            img = $('<img src="' + response.result + '">');
//                            $wrap.empty().append(img);
//                        } else {
//                            $wrap.text("预览出错");
//                        }
//                    });
//                }
//            }, thumbnailWidth, thumbnailHeight);

//            percentages[file.id] = [file.size, 0];
//            file.rotation = 0;
//        }

//        file.on('statuschange', function (cur, prev) {
//            if (prev === 'progress') {
//                $prgress.hide().width(0);
//            } else if (prev === 'queued') {
//                $li.off('mouseenter mouseleave');
//                $btns.remove();
//            }

//            // 成功
//            if (cur === 'error' || cur === 'invalid') {
//                console.log(file.statusText);
//                showError(file.statusText);
//                percentages[file.id][1] = 1;
//            } else if (cur === 'interrupt') {
//                showError('interrupt');
//            } else if (cur === 'queued') {
//                $info.remove();
//                $prgress.css('display', 'block');
//                percentages[file.id][1] = 0;
//            } else if (cur === 'progress') {
//                $info.remove();
//                $prgress.css('display', 'block');
//            } else if (cur === 'complete') {
//                $prgress.hide().width(0);
//                $li.append('<span class="success"></span>');
//            }

//            $li.removeClass('state-' + prev).addClass('state-' + cur);
//        });

//        $li.on('mouseenter', function () {
//            $btns.stop().animate({ height: 30 });
//        });

//        $li.on('mouseleave', function () {
//            $btns.stop().animate({ height: 0 });
//        });

//        $btns.on('click', 'span', function () {
//            var index = $(this).index(),
//                deg;

//            switch (index) {
//                case 0:
//                    uploader.removeFile(file);
//                    return;

//                case 1:
//                    file.rotation += 90;
//                    break;

//                case 2:
//                    file.rotation -= 90;
//                    break;
//            }

//            if (supportTransition) {
//                deg = 'rotate(' + file.rotation + 'deg)';
//                $wrap.css({
//                    '-webkit-transform': deg,
//                    '-mos-transform': deg,
//                    '-o-transform': deg,
//                    'transform': deg
//                });
//            } else {
//                $wrap.css('filter', 'progid:DXImageTransform.Microsoft.BasicImage(rotation=' + (~~((file.rotation / 90) % 4 + 4) % 4) + ')');
//                // use jquery animate to rotation
//                // $({
//                //     rotation: rotation
//                // }).animate({
//                //     rotation: file.rotation
//                // }, {
//                //     easing: 'linear',
//                //     step: function( now ) {
//                //         now = now * Math.PI / 180;

//                //         var cos = Math.cos( now ),
//                //             sin = Math.sin( now );

//                //         $wrap.css( 'filter', "progid:DXImageTransform.Microsoft.Matrix(M11=" + cos + ",M12=" + (-sin) + ",M21=" + sin + ",M22=" + cos + ",SizingMethod='auto expand')");
//                //     }
//                // });
//            }


//        });

//        $li.appendTo($queue);
//    }

//    // 负责view的销毁
//    function removeFile(file) {
//        var $li = $('#' + file.id);

//        delete percentages[file.id];
//        updateTotalProgress();
//        $li.off().find('.file-panel').off().end().remove();
//    }

//    function updateTotalProgress() {
//        var loaded = 0,
//            total = 0,
//            spans = $progress.children(),
//            percent;

//        $.each(percentages, function (k, v) {
//            total += v[0];
//            loaded += v[0] * v[1];
//        });

//        percent = total ? loaded / total : 0;


//        spans.eq(0).text(Math.round(percent * 100) + '%');
//        spans.eq(1).css('width', Math.round(percent * 100) + '%');
//        updateStatus();
//    }

//    function updateStatus() {
//        var text = '', stats;

//        if (state === 'ready') {
//            text = '选中' + fileCount + '张图片，共' +
//                WebUploader.formatSize(fileSize) + '。';
//        } else if (state === 'confirm') {
//            stats = uploader.getStats();
//            if (stats.uploadFailNum) {
//                text = '图片：已成功上传' + stats.successNum + '张，失败上传' +
//                    stats.uploadFailNum + '张，<a class="retry" >重新上传</a>失败图片或<a class="ignore">忽略</a>';
//            }

//        } else {
//            stats = uploader.getStats();
//            text = '共' + fileCount + '张（' +
//                WebUploader.formatSize(fileSize) +
//                '），已上传' + stats.successNum + '张';

//            if (stats.uploadFailNum) {
//                text += '，失败' + stats.uploadFailNum + '张';
//            }
//        }

//        $info.html(text);
//    }

//    function setState(val) {
//        var file, stats;

//        if (val === state) {
//            return;
//        }

//        $upload.removeClass('state-' + state);
//        $upload.addClass('state-' + val);
//        state = val;

//        switch (state) {
//            case 'pedding':
//                $placeHolder.removeClass('element-invisible');
//                $queue.hide();
//                $statusBar.addClass('element-invisible');
//                uploader.refresh();
//                break;

//            case 'ready':
//                $placeHolder.addClass('element-invisible');
//                $('#filePicker2').removeClass('element-invisible');
//                $queue.show();
//                $statusBar.removeClass('element-invisible');
//                uploader.refresh();
//                break;

//            case 'uploading':
//                $('#filePicker2').addClass('element-invisible');
//                $progress.show();
//                $upload.text('暂停上传');
//                break;

//            case 'paused':
//                $progress.show();
//                $upload.text('继续上传');
//                break;

//            case 'confirm':
//                $progress.hide();
//                $('#filePicker2').removeClass('element-invisible');
//                $upload.text('开始上传');

//                stats = uploader.getStats();
//                if (stats.successNum && !stats.uploadFailNum) {
//                    setState('finish');
//                    return;
//                }
//                break;
//            case 'finish':
//                stats = uploader.getStats();
//                if (stats.successNum) {
//                    alert('上传成功');
//                } else {
//                    // 没有成功的图片，重设
//                    state = 'done';
//                    location.reload();
//                }
//                break;
//        }

//        updateStatus();
//    }

//    uploader.onUploadProgress = function (file, percentage) {
//        var $li = $('#' + file.id),
//            $percent = $li.find('.progress span');

//        $percent.css('width', percentage * 100 + '%');
//        percentages[file.id][1] = percentage;
//        updateTotalProgress();
//    };

//    uploader.onFileQueued = function (file) {
//        fileCount++;
//        fileSize += file.size;

//        if (fileCount === 1) {
//            $placeHolder.addClass('element-invisible');
//            $statusBar.show();
//        }

//        addFile(file);
//        setState('ready');
//        updateTotalProgress();
//    };

//    uploader.onFileDequeued = function (file) {
//        fileCount--;
//        fileSize -= file.size;

//        if (!fileCount) {
//            setState('pedding');
//        }

//        removeFile(file);
//        updateTotalProgress();

//    };

//    uploader.on('all', function (type) {
//        var stats;
//        switch (type) {
//            case 'uploadFinished':
//                setState('confirm');
//                break;

//            case 'startUpload':
//                setState('uploading');
//                break;

//            case 'stopUpload':
//                setState('paused');
//                break;

//        }
//    });

//    uploader.onError = function (code) {
//        alert('Eroor: ' + code);
//    };

//    $upload.on('click', function () {
//        if ($(this).hasClass('disabled')) {
//            return false;
//        }

//        if (state === 'ready') {
//            uploader.upload();
//        } else if (state === 'paused') {
//            uploader.upload();
//        } else if (state === 'uploading') {
//            uploader.stop();
//        }
//    });

//    $info.on('click', '.retry', function () {
//        uploader.retry();
//    });

//    $info.on('click', '.ignore', function () {
//        alert('todo');
//    });

//    $upload.addClass('state-' + state);
//    updateTotalProgress();
//    //var $ = jQuery,
//    //    $list = $('#fileList'),
//    //    // 优化retina, 在retina下这个值是2
//    //    ratio = window.devicePixelRatio || 1,

//    //    // 缩略图大小
//    //    thumbnailWidth = 120 * ratio,
//    //    thumbnailHeight = 120 * ratio,

//    //    // Web Uploader实例
//    //    uploader;

//    //// 初始化Web Uploader
//    //uploader = WebUploader.create({

//    //    // 自动上传。
//    //    auto: autoUpload,

//    //    // swf文件路径
//    //    swf: swfSrc,

//    //    // 文件接收服务端。
//    //    server: uploadUrl,

//    //    // 选择文件的按钮。可选。
//    //    // 内部根据当前运行是创建，可能是input元素，也可能是flash.
//    //    pick: '#filePicker',

//    //    // 只允许选择文件，可选。
//    //    accept: {
//    //        title: acceptTitle,
//    //        extensions: acceptExtensions,
//    //        mimeTypes: acceptMimeTypes
//    //    }
//    //});

//    //// 当有文件添加进来的时候
//    //uploader.on('fileQueued', function (file) {
//    //    var $li = $(
//    //        '<div id="' + file.id + '" class="file-item thumbnail">' +
//    //        '<img>' +
//    //        '<div class="info">' + file.name + '</div>' +
//    //        '</div>'
//    //    ),
//    //        $img = $li.find('img');

//    //    $list.append($li);

//    //    // 创建缩略图
//    //    uploader.makeThumb(file, function (error, src) {
//    //        if (error) {
//    //            $img.replaceWith('<span>不能预览</span>');
//    //            return;
//    //        }

//    //        $img.attr('src', src);
//    //    }, thumbnailWidth, thumbnailHeight);
//    //});

//    //// 文件上传过程中创建进度条实时显示。
//    //uploader.on('uploadProgress', function (file, percentage) {
//    //    var $li = $('#' + file.id),
//    //        $percent = $li.find('.progress span');

//    //    // 避免重复创建
//    //    if (!$percent.length) {
//    //        $percent = $('<p class="progress"><span></span></p>')
//    //            .appendTo($li)
//    //            .find('span');
//    //    }

//    //    $percent.css('width', percentage * 100 + '%');
//    //});

//    //// 文件上传成功，给item添加成功class, 用样式标记上传成功。
//    //uploader.on('uploadSuccess', function (file) {
//    //    $('#' + file.id).addClass('upload-state-done');
//    //});

//    //// 文件上传失败，现实上传出错。
//    //uploader.on('uploadError', function (file) {
//    //    var $li = $('#' + file.id),
//    //        $error = $li.find('div.error');

//    //    // 避免重复创建
//    //    if (!$error.length) {
//    //        $error = $('<div class="error"></div>').appendTo($li);
//    //    }

//    //    $error.text('上传失败');
//    //});

//    //// 完成上传完了，成功或者失败，先删除进度条。
//    //uploader.on('uploadComplete', function (file) {
//    //    $('#' + file.id).find('.progress').remove();
//    //});



//    // 文件上传
//    //debugger;
//    //var $ = jQuery,
//    //    $list = $('#fileList'),
//    //    // 优化retina, 在retina下这个值是2
//    //    ratio = window.devicePixelRatio || 1,
//    //    // 缩略图大小
//    //    thumbnailWidth = 120 * ratio,
//    //    thumbnailHeight = 120 * ratio,
//    //  uploader;
//    //// 初始化Web Uploader
//    // uploader = WebUploader.create({
//    //    // [默认值：false] 设置为 true 后，不需要手动调用上传，有文件选择即开始上传。
//    //    auto: autoUpload,
//    //    // swf文件路径
//    //    swf: swfSrc,
//    //    // 文件接收服务端。
//    //    server: uploadUrl,
//    //    //[可选][默认值：undefined]指定选择文件的按钮容器，不指定则不创建按钮。
//    //    //Seletor| dom, 指定选择文件的按钮容器，不指定则不创建按钮。支持 id, 还支持 class, 或者 dom 节点。
//    //    //innerHTML { String; } 指定按钮文字。不指定时优先从指定的容器中看是否自带文字。
//    //    //multiple { Boolean; } 是否开起同时选择多个文件能力。
//    //    pick: {
//    //        Seletor: Seletor,
//    //        innerHTML: innerHTML,
//    //        multiple: multiple
//    //    },
//    //    // 只允许选择图片文件。
//    //    accept: {
//    //        title: acceptTitle,
//    //        extensions: acceptExtensions,
//    //        mimeTypes: acceptMimeTypes
//    //    },
//    //    //[可选] 配置生成缩略图的选项。
//    //    thumb: {
//    //        width: thumbnailWidth,
//    //        height: thumbnailHeight,

//    //        // 图片质量，只有type为`image/jpeg`的时候才有效。
//    //        quality: 100,

//    //        // 是否允许放大，如果想要生成小图的时候不失真，此选项应该设置为false.
//    //        allowMagnify: true,

//    //        // 是否允许裁剪。
//    //        crop: true,

//    //        // 为空的话则保留原有图片格式。
//    //        // 否则强制转换成指定的类型。
//    //        type: 'image/jpeg'
//    //    },
//    //    //配置压缩的图片的选项。如果此选项为false, 则图片在上传前不进行压缩。
//    //    compress: false,

//    //    //[可选][默认值：html5, flash]指定运行时启动顺序。默认会想尝试 html5 是否支持，如果支持则使用 html5, 否则则使用 flash.可以将此值设置成 flash，来强制使用 flash 运行时。
//    //    //runtimeOrder: ["html5", "flash"],
//    //    //[可选][默认值：false]是否要分片处理大文件上传。
//    //    chunked: true,
//    //    //[可选][默认值：5242880]如果要分片，分多大一片？ 默认大小为5M.
//    //    chunkSize: 2 * 1024,
//    //    //[可选][默认值：2]如果某个分片由于网络问题出错，允许自动重传多少次？
//    //    chunkRetry: 3,
//    //    //[可选][默认值：'POST']文件上传方式，POST或者GET。
//    //    method: method,
//    //    // { Object }[可选][默认值：false]是否已二进制的流的方式发送文件，这样整个上传内容input都为文件内容， 其他参数在GET数组中。
//    //    sendAsBinary: false,
//    //    //[可选][默认值：undefined]验证文件总数量, 超出则不允许加入队列。
//    //    fileNumLimit: 10,
//    //    //[可选][默认值：undefined]验证文件总大小是否超出限制, 超出则不允许加入队列。
//    //    fileSizeLimit: 100 * 1024,
//    //    // { int }[可选][默认值：undefined]验证单个文件大小是否超出限制, 超出则不允许加入队列。
//    //    fileSingleSizeLimit: 10 * 1024,
//    //    //[可选][默认值：undefined]去重， 根据文件名字、文件大小和最后修改时间来生成hash Key.
//    //    duplicate: true,
//    //    //[可选][默认值：undefined]默认所有 Uploader.register 了的 widget 都会被加载，如果禁用某一部分，请通过此 option 指定黑名单。
//    //    disableWidgets: undefined,

//    //});
//    //// 当有文件添加进来的时候
//    //uploader.on('fileQueued', function (file) {
//    //    var $li = $(
//    //        '<div id="' + file.id + '" class="file-item thumbnail">' + 
//    //        '<img>' +
//    //        '<div class="info">' + file.name + '</div>' +
//    //        '</div>'
//    //    ),
//    //        $img = $li.find('img');

//    //    $list.append($li);

//    //    // 创建缩略图
//    //    uploader.makeThumb(file, function (error, src) {
//    //        if (error) {
//    //            $img.replaceWith('<span>不能预览</span>');
//    //            return;
//    //        }

//    //        $img.attr('src', src);
//    //    }, thumbnailWidth, thumbnailHeight);
//    //});

//    //// 文件上传过程中创建进度条实时显示。
//    //uploader.on('uploadProgress', function (file, percentage) {
//    //    var $li = $('#' + file.id),
//    //        $percent = $li.find('.progress span');

//    //    // 避免重复创建
//    //    if (!$percent.length) {
//    //        $percent = $('<p class="progress"><span></span></p>')
//    //            .appendTo($li)
//    //            .find('span');
//    //    }

//    //    $percent.css('width', percentage * 100 + '%');
//    //});

//    //// 文件上传成功，给item添加成功class, 用样式标记上传成功。
//    //uploader.on('uploadSuccess', function (file) {
//    //    $('#' + file.id).addClass('upload-state-done');
//    //});

//    //// 文件上传失败，现实上传出错。
//    //uploader.on('uploadError', function (file) {
//    //    var $li = $('#' + file.id),
//    //        $error = $li.find('div.error');

//    //    // 避免重复创建
//    //    if (!$error.length) {
//    //        $error = $('<div class="error"></div>').appendTo($li);
//    //    }

//    //    $error.text('上传失败');
//    //});

//    //// 完成上传完了，成功或者失败，先删除进度条。
//    //uploader.on('uploadComplete', function (file) {
//    //    $('#' + file.id).find('.progress').remove();
//    //});
//};