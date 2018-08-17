$(function () {
    $("#loaderbody").addClass('hide');


    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});


function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        };
        reader.readAsDataURL(imageUploader.files[0]);
    }
}

function jQueryAjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    refreshAddNewTab($(form).attr('data-restUrl'), true);
                    //success messege
                    $.notify(response.message, "success");
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();
                }
                else {
                    //error message
                    $.notify(response.message, "error");
                }
            }
        }
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);

    }
    return false;
}

function refreshAddNewTab(resetUrl, showViewTab) {
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New');
            if (showViewTab)
                $('ul.nav.nav-tabs a:eq(0)').tab('show');
        }

    });
}

function Edit(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Edit');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');
        }
    });
}

function Delete(url) {
    if (confirm('Are you sure to delete this record ?') == true) {
        $.ajax({
            type: 'POST',
            url: url,
            data: {"__RequestVerificationToken" : $('input[name=__RequestVerificationToken]').val() },
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    $.notify(response.message, "warn");
                    if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                        activatejQueryTable();
                }
                else {
                    $.notify(response.message, "error");
                }
            }

        });

    }
}




function GetDropDown(_Id, url, target) {
 //   alert(_Id.value + url+ target);
//Subash Pasachhe

    $.ajax({
        type: "get",
        url:url,
        data: { id:  _Id},
        success: function (data) {
            var a = data; // This line shows error.
            var markup = "<option value='0'>-- Select --</option>";
            for (var x = 0; x < data.length; x++) {
                markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
            }
            $("#" + target).html(markup).show();
        }
    });
}



function GetDropDownFor(_Id, url, target) {
    //   alert(_Id.value + url+ target);
    //Subash Pasachhe

    $.ajax({
        type: "get",
        url: url,
        data: { id: $("#"+_Id).val() },
        success: function (data) {
            var a = data; // This line shows error.
            var markup = "<option value='0'>-- Select --</option>";
            for (var x = 0; x < data.length; x++) {
                markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
            }
            $("#" + target).html(markup).show();
        }
    });
}


function GetDetailFor(_Id, url, target) {
    //   alert(_Id.value + url+ target);

    $.ajax({
        type: "get",
        url: url,
        data: { id: $("#" + _Id).val() },
        success: function (data) {
            
            //var a = data; // This line shows error.
            //var markup = "<option value='0'>-- Select --</option>";
            //for (var x = 0; x < data.length; x++) {
            //    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
            //}
            $("#" + target).html(data).show();
        }
    });
}

function SaveTransactionMain(BillNumber, Description, Date, UserName) {
    //   alert(_Id.value + url+ target);

    $.ajax({
        type: "post",
        url: "/TransactionMain/AddorEdit",
        data: { BillNumber: BillNumber, Description: Description, Date: Date, UserName: UserName, __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() },
        success: function (data) {
            return data.message;


            //var a = data; // This line shows error.
            //var markup = "<option value='0'>-- Select --</option>";
            //for (var x = 0; x < data.length; x++) {
            //    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
            //}
            //$("#" + target).html(data).show();
        }
    });
}

function SaveTransactionDetail(TransactionMainID, LedgerNumber, Description, Debit, Credit, CustomerID) {
    //   alert(_Id.value + url+ target);

    $.ajax({
        type: "post",
        url: "/TransactionDetail/AddorEdit",
        data: { TransactionMainID: TransactionMainID, LedgerNumber: LedgerNumber, Description: Description, Debit: Debit, Credit: Credit, CustomerID: CustomerID, __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() },
        success: function (data) {

            //var a = data; // This line shows error.
            //var markup = "<option value='0'>-- Select --</option>";
            //for (var x = 0; x < data.length; x++) {
            //    markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
            //}
            //$("#" + target).html(data).show();
        }
    });
}

//for customer and ledger number
function SaveCustomer(CustomerName, Type, PreviousBalance) {
    $.ajax({
        type: "post",
        url: "/Customer/AddorEdit",
        data: { CustomerName: CustomerName, Type: Type, PreviousBalance: PreviousBalance, __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() },
        success: function (data) {
            return data.message;
        }
    });
}
function SaveLedger(LedgerNumber, Type, CustomerID) {
    $.ajax({
        type: "post",
        url: "/Ledger/AddorEdit",
        data: { LedgerNumber: LedgerNumber, Type: Type, CustomerID: CustomerID, __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() },
        success: function (data) {
            return data.message;
        }
    });
}







