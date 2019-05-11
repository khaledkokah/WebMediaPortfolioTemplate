//=== KOKAH 2018, JANUARY, 28 ===//
//=== This is a template for all the functions that will be used in pages with modals ===//

$(document).ready(function () {
    $("#cp_content_GridView1").prepend($("<thead></thead>").append($("#cp_content_GridView1").find("tr:first"))).dataTable();
    $('#cp_content_GridView1').dataTable();
    styleControls();
});

function showModal(id, isNew) {
    // alert(id);
    $("#modalRec").one('show.bs.modal', function (e) {
        clearModal(this, false);

        if (isNew) {
            $(".modal-header #myModalLabel").text('New Record');
            $("#txtRecId").val("");
            $("#hdRecId").val("");
            disableModal(false);
            //    $(".modal-footer #btnCloseNode").hide();
            //    $(".modal-footer #btnCancelNode").show();
        }
        else {
            $(".modal-header #myModalLabel").text('Existing Record');
            //Get record info
            getRecord(id);
        }
    });
    $("#modalRec").modal({ backdrop: 'static', keyboard: false, show: true });
}

function clearModal(modal, showMsg) {
    if (showMsg) {
        if (confirm('Are you sure you want to clear all fields?')) {
            clearModalFields();
        }
    }
    else {
        clearModalFields()
    }
}

function clearModalFields() {
    $('#modalContent')
        .find("input,textarea,select")
        .val('')
        .end()
        .find("input[type=checkbox], input[type=radio]")
        .prop("checked", "")
        .end();

    $("#gridInstalment").empty();
    $('#btnSave').val('Save');

}

function disableModal(disabled) {
    $("#modalAttach")
        .find("input,textarea,select")
        .prop("disabled", disabled)
        .end()
        .find("input[type=checkbox], input[type=radio]")
        .prop("disabled", disabled)
        .end();

  
    //if (disabled) {
    //    document.getElementById('#btnSave').style.visibility = 'hidden';
    //    document.getElementById('#btnClear').style.visibility = 'hidden';
    //}
    //else {
    //    document.getElementById('#btnSave').style.visibility = 'visible';
    //    document.getElementById('#btnClear').style.visibility = 'visible';
    //}

}

function formatDate(val) {
    var date = new Date(val);
    return date.getFullYear() + "/" + (date.getMonth() + 1) + '/' + date.getDate();
}

function setFocus(id) {
    document.getElementById(id).focus();
}

function fillStamp(item) {
    $("#txtValidFrom").val(item.VALID_FROM);
    $("#txtValidTo").val(item.VALID_TO);
    $("#lblCreateBy").text(item.CREATE_BY);
    $("#lblCreateDate").text(item.CREATE_DATE);
    $("#lblUpdateBy").text(item.UPDATE_BY);
    $("#lblUpdateDate").text(item.UPDATE_DATE);
}

function validateStamp() {
    var validFrom = new Date(document.getElementById('txtValidFrom').value);
    var validTo = new Date(document.getElementById('txtValidTo').value);

    //Validate only if both fields are not empty
    if (validTo === "" && validFrom === "")
        return true;

    if (validTo  <= validFrom) {
        alert("Valid To must be greater than Valid From date.");
        return false;
    }

    return true;
}

//Styling modal controls
function styleControls() {
    $("#tblForm").find("input,button,textarea,select").attr('class', 'form-control');
}