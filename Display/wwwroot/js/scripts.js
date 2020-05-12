////Activates the dropdowns in bootstrap
//$('.dropdown-toggle').dropdown()

////Activates the tooltips in Bootstrap
//$(function () {
//    $('[data-toggle="tooltip"]').tooltip()
//});

//$(document).ready(function () {
//    //Activates the popovers in Bootstrap
//    $('[data-toggle="popover"]').popover({
//        container: 'body'
//    });

//    //Apply the DatePicker editor template 
//    //To any class member with the attirbute [DataType(DataType.Date)]
//    $('.datepicker').attr({
//        autoclose: true,
//        format: 'mm/dd/yyyy',
//        todayBtn: true,
//        todayHighlight: true
//    });

//    // Enable Live Search.  
//    $('#SelectPicker').attr('data-live-search', true)
//});

//$(function () {
//    $("#loaderbody").addClass('hide');


//    $(document).bind('ajaxStart', function () {
//        $("#loaderbody").removeClass('hide');
//    }).bind('ajaxStop', function () {
//        $("#loaderbody").addClass('hide');
//    });
//});


//function ShowImagePreview(imageUploader, previewImage) {
//    if (imageUploader.files && imageUploader.files[0]) {
//        var reader = new FileReader();
//        reader.onload = function (e) {
//            $(previewImage).attr('src', e.target.result);
//        }
//        reader.readAsDataURL(imageUploader.files[0]);
//    }
//}


//Construct all columns for a table
function constructTableColumns(incolumns, columns) {
    // Check for empty array
    if (!Array.isArray(columns) || !columns.length) {
        columns = [];
    }
    //columns for DataTables
    $.each(JSON.parse(incolumns), function (i, val) {
        //alert(val["format"] + '-' + val["data"] + '-' + val["name"] + '-' + val["orderable"] + '-' + val["isvisible"]);
        if (val["format"] == "Currency") { var col = { data: val["data"], title: val["name"], orderable: val["orderable"], visible: val["isvisible"], render: $.fn.dataTable.render.number(',', '.', 2, '$') }; }
        else if (val["format"] == "Decimal") { var col = { data: val["data"], title: val["name"], orderable: val["orderable"], visible: val["isvisible"], render: $.fn.dataTable.render.number(',', '.', 8, '') }; }
        else { var col = { data: val["data"], title: val["name"], orderable: val["orderable"], visible: val["isvisible"] }; }
        //alert(JSON.stringify(col));
        columns.push(col);
    });
    //alert(columns.toString());
    return [columns];
}

////Construct all columns for a table
//function constructTableColumns(response, columns) {
//    //columns for DataTables
//    $.each(JSON.parse(response), function (i, val) {
//        if (val["format"] == "Currency") { var col = { data: val["data"], title: val["name"], render: $.fn.dataTable.render.number(',', '.', 2, '$') }; }
//        else if (val["format"] == "Decimal") { var col = { data: val["data"], title: val["name"], render: $.fn.dataTable.render.number(',', '.', 2, '') }; }
//        else { var col = { data: val["data"], title: val["name"] }; }
//        columns.push(col);
//    });
//    return [columns];
//}

//Construct the details +/- for tables
function constructDetailColumn(detailClassName) {
    var columns = [];
    columns.push({
        width: "10px",
        className: detailClassName,
        orderable: false,
        sortable: false,
        data: null,
        defaultContent: '',
        render: function () {
            return '<i class="fa fa-plus-circle expand-control" aria-hidden="true"></i>';
        },
    });
    return [columns];
}

// Toggle every object that has .has-spinner attribute
function toggleSpinner(active) {
    //alert('here!');
    spinnerObjects = $(".has-spinner");
    spinnerObjects.each(function () {
        if (active) {
            $(this).addClass(' active');
        }
        else {
            $(this).removeClass('active');
        }
    });
}

// display notification in upper right corner
function notifyStatus(success, statusMessage) {
    //alert("success: " + success + " statusMessage: " + statusMessage)
    // Hide the "busy" gif:
    toggleSpinner(false);
    if (typeof statusMessage != "undefined") {
        var message = statusMessage;
        var res = message.split("/r/n");
        if (success) {
            res.forEach(function (element) {
                $.notify(element, "success");
            });
        }
        else {
            res.forEach(function (element) {
                $.notify(element, "error");
            });
        };
    };
}

// Get a JSON wrapped response from url and discplay it in divName
function refreshView(url, divName) {
    toggleSpinner(true);
    $.ajax({
        //type: 'POST',
        url: url,
        success: function (response) {
            notifyStatus(response);
        },
        error: function (jqxhr, status, exception) {
            console.log("error");
            alert('RefreshViewAsync - Status: ' + status + ' - Exception: ' + exception + ' -  jqxhr: ' + jqxhr);
        }
    });
}

// Get a JSON wrapped response by submitting the form to url and discplay it in divName
function refreshViewAsync(url, divName, reqType, formName) {
    toggleSpinner(true);
    var formData = new FormData(document.getElementById(formName));
    $.ajax({
        type: reqType,
        data: formData,
        url: url,
        processData: false,
        contentType: false,
    }).done(function (response) {
        if (typeof response.html != "undefined") {
            $(divName).html(response.html);
        }
        else {
            window.location.replace("Index");
        }
        notifyStatus(response);
    }).catch(function (jqxhr, status, exception) {
        console.log("error");
        alert('RefreshViewAsync - Exception:' + exception + ' - Status: ' + status + ' -  jqxhr: ');
    });
}

// Get a partial page response from url and discplay it in divName
function loadView(url, divName) {
    toggleSpinner(true);
    $(divName).load(url);
    toggleSpinner(false);
}

// Get a page response from url and display it in divName
function loadDataView(url, divName) {
    //toggleSpinner(true);
    //alert("url: " + url + " divName: " + divName);
    $.ajax({
        type: "GET",
        url: url,
    }).done(function (response) {
        //alert("success: " + response.success + " statusMessage: " + response.statusMessage + " redirectUrl: " + response.redirectUrl + " reloadIndex: " + response.reloadIndex + " html: " + response.html);
        //Show whatever content is in the response
        //Happens with invalid ModelState
        if (typeof response.html != "undefined") {
            $(divName).html(response.html);
        }
        //Catch everything else and default reload
        else {
            window.location.replace("Index");
        }
        notifyStatus(response.success, response.statusMessage);
    }).catch(function (jqxhr, status, exception) {
        console.log("error");
        alert('loadFormView - Exception:' + exception + ' - Status: ' + status + ' -  jqxhr: ' + jqxhr);
    });
}

// Get a page response from url and display it in divName
function loadFormView(url, divName, reqType, formName) {
    toggleSpinner(true);
    var formData = new FormData(document.getElementById(formName));
    //alert("URL: " + url + " FormName: " + formName + " reqType: " + reqType + " Data: " + JSON.stringify(formData) + formData);
    $.ajax({
        type: reqType,
        data: formData,
        url: url,
        processData: false,
        contentType: false,
    }).done(function (response) {
        //alert("success: " + response.success + " statusMessage: " + response.statusMessage + " redirectUrl: " + response.redirectUrl + " reloadIndex: " + response.reloadIndex + " html: " + response.html);
        // Form redirect
        if (response.redirectUrl !== undefined) {
            loadDataView(response.redirectUrl, divName);
            //$(divName).load(response.redirectUrl);
        }
        //Reload the entire page 
        //Used after Log In and Log Out from submit to refresh Nav Bar
        else if (response.reloadIndex !== undefined) {
            window.location.replace("Index");
            $(divName).load(response.reloadIndex);
        }
        //Show whatever content is in the response
        //Happens with invalid ModelState
        else if (typeof response.html != "undefined") {
            $(divName).html(response.html);
        }
        //Catch everything else and default reload
        else {
            window.location.replace("Index");
        }
        notifyStatus(response.success, response.statusMessage);
    }).catch(function (jqxhr, status, exception) {
        console.log("error");
        alert('loadFormView - Exception:' + exception + ' - Status: ' + status + ' -  jqxhr: ' + jqxhr);
    });
}

// Forces user to confirm delete by asking them to confirm
function confirmDelete(uniqueID, isTrue) {
    var deleteSpan = 'deleteSpan_' + uniqueID;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueID;
    if (isTrue) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
};

// Switch between grid and list view
function switchView(isTrue) {
    if (isTrue) {

        $('#view2Div').hide();
        $('#view1Div').show();
        $('#view2Btn').hide();
        $('#view1Btn').show();
    } else {
        $('#view2Div').show();
        $('#view1Div').hide();
        $('#view2Btn').show();
        $('#view1Btn').hide();
    }
};

//Configures the passed in datatable dt with buttons
function configureDataTables(dt) {
    // Name of the filename when exported (except for extension)
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    var export_filename = yyyy + '-' + mm + '-' + dd + 'Filename-';

    // Configure Export Buttons
    new $.fn.dataTable.Buttons(dt, {
        buttons: [
            {
                text: '<i class="fa fa-lg fa-clipboard"></i>',
                extend: 'copy',
                className: 'btn btn-xs btn-primary p-5 m-0 width-35 assets-export-btn export-copy ttip'
            }, {
                text: '<i class="fa fa-lg fa-file-text-o"></i>',
                extend: 'csv',
                className: 'btn btn-xs btn-primary p-5 m-0 width-35 assets-export-btn export-csv ttip',
                title: export_filename,
                extension: '.csv'
            }, {
                text: '<i class="fa fa-lg fa-file-excel-o"></i>',
                extend: 'excel',
                className: 'btn btn-xs btn-primary p-5 m-0 width-35 assets-export-btn export-xls ttip',
                title: export_filename,
                extension: '.xls'
            }, {
                text: '<i class="fa fa-lg fa-file-pdf-o"></i>',
                extend: 'pdf',
                className: 'btn btn-xs btn-primary p-5 m-0 width-35 assets-export-btn export-pdf ttip',
                title: export_filename,
                extension: '.pdf'
            }
        ]
    });

    // Add the Export buttons to the toolbox
    dt.buttons(0, null).container().appendTo('#export-assets');


    // Configure Print Button
    new $.fn.dataTable.Buttons(dt, {
        buttons: [
            {
                text: '<i class="fa fa-lg fa-print"></i> Print Assets',
                extend: 'print',
                className: 'btn btn-primary btn-sm m-5 width-140 assets-select-btn export-print'
            }
        ]
    });

    // Add the Print button to the toolbox
    dt.buttons(1, null).container().appendTo('#print-assets');


    // Select Buttons
    new $.fn.dataTable.Buttons(dt, {
        buttons: [
            {
                extend: 'selectAll',
                className: 'btn btn-xs btn-primary p-5 m-0 width-70 assets-select-btn'
            }, {
                extend: 'selectNone',
                className: 'btn btn-xs btn-primary p-5 m-0 width-70 assets-select-btn'
            }
        ]
    });

    // Add the Select buttons to the toolbox
    dt.buttons(2, null).container().appendTo('#select-assets');


    // Configure Selected Assets Buttons (delete, timeline, etc)
    new $.fn.dataTable.Buttons(dt, {
        buttons: [
            {
                text: 'Delete Selected',
                action: function () {
                    assets.delete_from_list(dt, assets.selected_ids(dt));
                },
                className: 'btn btn-primary btn-sm m-5 width-140 assets-select-btn toolbox-delete-selected'
            }, {
                text: 'View Timeline',
                action: function () {
                    console.log(assets.selected_ids(dt));
                },
                className: 'btn btn-primary btn-sm m-5 width-140 assets-select-btn'
            }
        ]
    });

    // Add the selected assets buttons to the toolbox
    dt.buttons(3, null).container().appendTo('#selected-assets-btn-group');


    // Configure Select Columns
    new $.fn.dataTable.Buttons(dt, {
        buttons: [
            {
                extend: 'collection',
                text: 'Select Columns',
                buttons: [{
                    extend: 'columnsToggle',
                    columns: ':not([data-visible="false"])'
                }],
                className: 'btn btn-primary btn-sm m-5 width-140 assets-select-btn'
            }
        ],
        fade: true
    });

    // Add the select columns button to the toolbox
    dt.buttons(4, null).container().appendTo('#toolbox-column-visibility');
}