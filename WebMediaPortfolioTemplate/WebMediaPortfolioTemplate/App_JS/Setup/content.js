function getRecord(id) {

    $.ajax({
        type: "POST",
        url: "Content.aspx/GetRecord",
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
    $("#txtDesc").val(item.DESCRIPTION);
   
    $("#txtVideoUrl").val(item.VIDEO_URL);
    // $("#<%= txtAttachDocumentDate.ClientID %>").val(item[0].DocumentDate);
    $("#hdRecId").val(item.CONTENT_ID);

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

}

