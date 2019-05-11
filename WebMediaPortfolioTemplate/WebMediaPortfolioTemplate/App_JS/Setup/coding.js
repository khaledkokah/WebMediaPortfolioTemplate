function getRecord(id) {

    $.ajax({
        type: "POST",
        url: "Coding.aspx/GetRecord",
        data: '{id: "' + id + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            fillModal(response.d);
        }
        ,
        failure: function (response) {
            alert(response.d);
        }
    });
}


function fillModal(item) {
    $("#txtDesc").val(item.Coding_Description);

    // $("#<%= txtAttachDocumentDate.ClientID %>").val(item[0].DocumentDate);
    $("#hdRecId").val(item.Coding_Id);

    fillStamp(item);
}


function validate() {

    //custom page fields
    var desc = document.getElementById('txtDesc').value;
    if (desc === '') {
        alert('Please insert a description.');
        return false;
    }

    return validateStamp();

    //$("form").validate({
    //    onfocusout: true,
    //    rules: {
    //        desc:
    //        {
    //            required: true,
    //            maxlength: 150
    //        }
    //    },
    //    messages: {
    //        desc:
    //        {
    //            required: "please insert description",
    //            maxlength: "character must be less than 150"
    //        }
    //    }
    //});

    //var isvalid = $("form").valid();
    //return isvalid;
}

