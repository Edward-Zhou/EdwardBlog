//date picker
$(function () { // will trigger when the document is ready
    $('.datepicker').datepicker(); //Initialise any date pickers
    //dateFormat: 'yy-mm-dd'
    //checkbox select or unselect
    $('#parentCheckbox').click(
        function () {
            if ($('#parentCheckbox').is(':checked')) {
                $("input[name='SelectedRows']").each(function (index, item) {
                    item.checked = true;
                });
            }
            else {
                $("input[name='SelectedRows']").each(function (index, item) {
                    item.checked = false;
                });
            }
        }
        );  
    //delete feedbacks
    //$('#deleteFeedBacks').on("click", deleteRows());

    $('#deleteFeedBacks').click(function deleteRows() {
        var list = getCheckedBoxList();
        $.ajax({
            method: "post",
            datatype: "json",
            traditional : true,
            url:"/Feedbacks/DeleteConfirmed", //"<%= Url.Action('DeleteConfirmed','Feedbacks')%>",
            data: { 'SelectedRows': list },
            success: function (result) {
                $("#deleteFeedbackconfirm").modal('hide');
                window.location.href = "/Feedbacks/index";
            }
        });
    });


});
//get selected checkboxes in feedback list
function getCheckedBoxList()
{
    var SelectedList=[];
    $("#SelectedRows:checked").each(
        function (index, item) {
            SelectedList.push(item.value);          
        }
    );
    return SelectedList;
}
function unCheckedBox() {
    var SelectedList = [];
    $("#SelectedRows:checked").each(
        function (index, item) {
            item.attr('checked', false);
        }
    );
}

//set update feedback partial view value from feedback list
function getSelectedRow() {    
    $("#EditFeedback #Feedback_Id").val($("#SelectedRows:checked").first().val());
    $("#EditFeedback #Feedback_UserName").val($("#SelectedRows:checked").first().closest("tr").find('td:eq(1)').text().trim());
    $("#EditFeedback #Feedback_Content").val($("#SelectedRows:checked").first().closest("tr").find('td:eq(2)').text().trim());
    $("#EditFeedback #Feedback_SubmitDate").val($("#SelectedRows:checked").first().closest("tr").find('td:eq(3)').text().trim());
    $("#EditFeedback #Feedback_Fixed").attr('checked', $("#SelectedRows:checked").first().closest("tr").find('.check-box').prop('checked'));
}

function SetInput() {
    alert($("#SelectedRows:checked").first().closest("tr").find('.check-box').prop('checked'));
    alert($("#SelectedRows:checked").first().closest("tr").find('td:eq(4)').hasOwnProperty('checked'));

    alert($("#SelectedRows:checked").first().closest("tr").find('td:eq(1)'));
}




