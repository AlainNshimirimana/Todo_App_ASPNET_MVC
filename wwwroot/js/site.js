// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Delete method
function deleteTask(i){
    $.ajax({
        url: 'Home/Delete',
        type: 'POST',
        data: {
            id: i
        },
        success: function(){  // if successfully deleted, reload page
            window.location.reload();
        }
    });
}

// Update method
function populateForm(i){
    $.ajax({
        url: 'Home/PopulateForm',
        type: 'GET',
        data: {
            id: i
        },
        dataType: 'json',
        success: function(response){ 
            $("#todo_Name").val(response.name);
            $("#todo_Id").val(response.id);
            $("#form-button").val("Update");
            $("#form-action").attr("action", "/Home/Update");
        }
    });
}