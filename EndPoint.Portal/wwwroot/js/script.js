/* ************************** Public ************************** */
//$("input.fanum, input.ennum").keypress(function (e) {
//    var keyCode = e.which;
//    if ((keyCode !== 8 || keyCode === 32) && (keyCode < 48 || keyCode > 57)) {
//        return false;
//    }
//});
//$(".falang").farsiInput();

$("input[type=text],textarea").keypress(function (event) {
    var ew = event.which;
    if (32 <= ew && ew <= 57)
        return true;
    if (65 <= ew && ew <= 90)
        return true;
    if (97 <= ew && ew <= 122)
        return true;
    return false;
});

/* ************************** Modal Actions ************************** */
/* ------------- Dispose ------------- */
$.fn.ModalDispose = function () {
    $('.modal-dialog').removeClass('modal-sm');
    $('.modal-dialog').removeClass('modal-lg');
    $('.modal .modal-loader').hide();
    $('.modal .modal-header').removeClass(function (index, className) {
        return (className.match(/(^|\s)background-\S+/g) || []).join(' ');
    }).show();
}

/* ------------- Popup Form ------------- */
showInPopup = (url, title, crudOperation, ModalSize) => {
    //$('#modal-loader').modal('show');
    $.ajax({
        type: 'GET',
        url: url,
        data: { httpVerb: 'GET' },
        success: function (res) {
            $.fn.ModalDispose();
            $('.modal .modal-body').html(res);
            $('.modal .modal-title').html(title);
            switch (ModalSize) {
                case "ExLarge":
                    $('.modal-dialog').addClass('modal-xl');
                    $('.modal-dialog').removeClass('modal-lg');
                    $('.modal-dialog').removeClass('modal-sm');
                    break;
                case "Large":
                    $('.modal-dialog').addClass('modal-lg');
                    $('.modal-dialog').removeClass('modal-xl');
                    $('.modal-dialog').removeClass('modal-sm');
                    break;
                case "Medium":
                    $('.modal-dialog').removeClass('modal-xl');
                    $('.modal-dialog').removeClass('modal-lg');
                    $('.modal-dialog').removeClass('modal-sm');
                    break;
                case "Small":
                    $('.modal-dialog').addClass('modal-sm');
                    $('.modal-dialog').removeClass('modal-xl');
                    $('.modal-dialog').removeClass('modal-lg');

                    break;
            }
            switch (crudOperation) {
                case "Create":
                    $('.modal .modal-header').addClass('bg-success');
                    $('.modal .modal-header').removeClass('bg-warning');
                    $('.modal .modal-header').removeClass('bg-danger');
                    $('.modal .modal-header').removeClass('bg-info');
                    break;

                case "Update":
                    $('.modal .modal-header').addClass('bg-warning');
                    $('.modal .modal-header').removeClass('bg-success');
                    $('.modal .modal-header').removeClass('bg-danger');
                    $('.modal .modal-header').removeClass('bg-info');
                    break;

                case "Delete":
                    $('.modal .modal-header').addClass('bg-danger');
                    $('.modal .modal-header').removeClass('bg-success');
                    $('.modal .modal-header').removeClass('bg-warning');
                    $('.modal .modal-header').removeClass('bg-info');
                    break;

                case "Other":
                    $('.modal .modal-header').addClass('bg-info');
                    $('.modal .modal-header').removeClass('bg-danger');
                    $('.modal .modal-header').removeClass('bg-success');
                    $('.modal .modal-header').removeClass('bg-warning');
                    $('.modal .modal-header').removeClass('bg-primary');
                    $('.modal .modal-header').removeClass('bg-secondary');
                    break;
            }
            //$('#modal-loader').hide();
            $('#modal').modal('show');
        }, error: function (error) {
            //$('#modal-loader').hide();
            alert(url + error);
            console.log(error);
        }
    })
}

/* ------------- CUD With Modal ------------- */
jQueryAjaxPost = form => {
    try {
        if ($(form).valid()) {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: $('form').serialize(),
                success: function (postRes) {
                    if (postRes.isValid == true) {
                        $('#modal').modal('hide');
                        $(postRes.dataUpdate).html(postRes.html);
                        Swal.fire({
                            icon: 'success',
                            title: postRes.message,
                            showConfirmButton: false,
                            timer: 2000
                        });
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: postRes.message,
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'ok!'
                        });
                    }
                },
                error: function (err) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        //text: 'System Error, Please contant to your administrator',
                        text: err,
                        confirmButtonText: 'ok!'
                    });
                }
            });
        }
        return false;
    }
    catch (ex) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'System Error',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'OK!'
        });
    }
}

/* ------------- CUD With File Attachment By Modal ------------- */
jQueryAjaxPostWithFile = form => {
    try {
        if ($(form).valid()) {
            var formData = new FormData(form);
            if ($("input[type=file]").length) {
                var myfile = $("input[type=file]");
                var fileAttached = myfile.val() ? true : false;
                if (fileAttached == true) {
                    formData.append('file', $("input[type=file]")[0].files[0]); 
                }
            }
            $.ajax({
                type: 'POST',
                url: form.action,
                data: formData,
                contentType: false,
                processData: false,
                success: function (postRes) {
                    if (postRes.isValid == true) {
                        $('#modal').modal('hide');
                        $(postRes.dataUpdate).html(postRes.html);
                        Swal.fire({
                            icon: 'success',
                            title: postRes.message,
                            showConfirmButton: false,
                            timer: 2000
                        });
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: postRes.message,
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'ok!'
                        });
                    }
                },
                error: function (err) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        //text: 'System Error, Please contant to your administrator',
                        text: err,
                        confirmButtonText: 'ok!'
                    });
                }
            });
        }
        return false;
    }
    catch (ex) {
        alert(ex);
    }
}


//var serializeForm = function (form) {
//    var obj = {};
//    var formData = new FormData(form);
//    for (var key of formData.keys()) {
//        obj[key] = formData.get(key);
//    }
//    return obj;
//};



/* ------------- Ajax Pagination ------------- */
$(document).on('click', 'a.page-link', function (e) {
    e.preventDefault();
    var SearchKey = $(this).closest('.datatable-wrapper').find('input[type=search]').val();
    var Page = $(this).text();
    var PageSize = $(this).closest('.datatable-wrapper').find(':selected').val();
    var parent = $(this).closest('.returnParent').parent();
    var parentId = $(this).closest('.returnParent').attr('id');
    
    var allRequestToggle = $(this).closest('.datatable-wrapper').find("[name='ShowAllDataToggle']").prop('checked');

    $.ajax({
        method: 'GET',
        url: '/' + parentId,
        data: { searchKey: SearchKey, page: Page, pageSize: PageSize, ShowAllRequestStatus: allRequestToggle },
        dataType: 'json',
        success: function (data) {
            parent.html(data.html);
            $('a.page-link').removeClass('Active');
            $(this).addClass('Active');
            $('a.page-link').removeAttr('href');
            $('a.page-link').css('cursor', 'pointer');
        },
        error: function (err) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Pagination Error',//system error
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK!'
            });
            console.log(err);
        }
    });
});

/* ------------- Page Size Change ------------- */
$('div.datatable-dropdown select').change(function (e) {
    e.preventDefault();
    var SearchKey = $(this).closest('.datatable-wrapper').find('input[type=search]').val();
    //var Page = $(this).closest('.datatable-wrapper').find('li.page-item a.active').text();
    var PageSize = $(this).find(':selected').val();
    var parent = $(this).closest('.datatable-wrapper').find('.returnParent');
    var parentId = $(this).closest('.datatable-wrapper').find('.returnParent').attr('id');
    
    //var allRequestToggle = $('#allRequestToggle').prop('checked');
    var allRequestToggle = $(this).closest('.datatable-wrapper').find("[name='ShowAllDataToggle']").prop('checked');

    $.ajax({
        method: 'GET',
        url: '/' + parentId,
        data: { searchKey: SearchKey, page: 1, pageSize: PageSize, ShowAllRequestStatus: allRequestToggle },
        dataType: 'json',
        success: function (data) {
            parent.html(data.html);
            $('a.page-link').removeClass('Active');
            $(this).addClass('Active');
            $('a.page-link').removeAttr('href');
            $('a.page-link').css('cursor', 'pointer');
        },
        error: function (data) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Pagination Error',//system error
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK!'
            });
            console.log(data);
        }
    });
});

/* ------------- Toggle button Change ------------- */
$("[name='ShowAllDataToggle']").change(function (e) {
    e.preventDefault();
    var SearchKey = $(this).closest('.datatable-wrapper').find('input[type=search]').val();
    //var Page = $(this)
    var PageSize = $(this).closest('.datatable-wrapper').find('.datatable-dropdown').find(':selected').val();
    var parent = $(this).closest('.datatable-wrapper').find('.returnParent');
    var parentId = $(this).closest('.datatable-wrapper').find('.returnParent').attr('id');
    
    //var allRequestToggle = $('#allRequestToggle').prop('checked');
    var allRequestToggle = $(this).closest('.datatable-wrapper').find("[name='ShowAllDataToggle']").prop('checked');

    $.ajax({
        method: 'GET',
        url: '/' + parentId,
        data: { searchKey: SearchKey, page: 1, pageSize: PageSize, ShowAllRequestStatus: allRequestToggle },
        dataType: 'json',
        success: function (data) {
            parent.html(data.html);
            $('a.page-link').removeClass('Active');
            $(this).addClass('Active');
            $('a.page-link').removeAttr('href');
            $('a.page-link').css('cursor', 'pointer');
        },
        error: function (data) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Pagination Error',//system error
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK!'
            });
            console.log(data);
        }
    });
});

$(document).on('click', 'a.page-link', function (e) {
    $('a.downloadfile').click(function (e) {
        e.preventDefault();
        window.location.href = $(this).attr('href');
    });
});



function FuncChangeState(UserId) {
    var Page = parseInt($('div#DataTableUser').find('li.page-item a.active').text());
    var PageSize = parseInt($('div#DataTableUser').closest('.tab-pane').find(':selected').val());
    //let formData = { httpVerb: 'POST', userId: UserId, page: Page, pageSize: PageSize };
    /*console.log(formData);*/
    debugger;
    $.ajax({
        method: 'GET',
        url: '/changeStateUser',
        data: { httpVerb: 'GET', userId: UserId, page: Page, pageSize: PageSize },
        dataType: 'json',
        success: function (data) {
            Swal.fire({
                icon: 'success',
                title: data.message,
                showConfirmButton: false,
                timer: 2000
            });
            console.log(data);
        },
        error: function (err) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Pagination Error',//system error
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK!'
            });
            console.log(err);
        }
    });
}



