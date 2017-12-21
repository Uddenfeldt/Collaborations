$(document).ready(function () {
    getEmails();
})


function getEmails() {
    $.ajax({
        url: 'check/getemails',
        method: 'GET',
        dataType: 'json',
        success: function (result) {
            console.log(result[0].email);
            buildEmailList(result);
        },
    });
}

function buildEmailList(result) {

    var html = "";

    for (var i = 0; i < result.length; i++)
    {
        html += "<option>" + result[i].email + "</option>";
    }

    $("#emails").append(html);

    //return newRow;
}

$("#recreate").click(function () {
    $.ajax({
        url: 'check/Recreate',
        method: 'GET',
        success: function () {
            console.log(result);
        },

        error: function (request, message, error) {
            console.log(request, message, error);
        }
    });
});



$("#loginButton").click(function () {
    var email = $("#emails").find("option:selected").text();
    var selectedValue = $("#emails").val();
    $.ajax({
        url: 'login/signin',
        method: 'GET',
        data: { email: email }
    }).
        done( function (result) {
            console.log(result);
        })
});
$("#openNews").click(function () {
    $.ajax({
        url: 'check/opennews',
        method: 'GET',
    })
});
$("#hiddenNews").click(function () {
    $.ajax({
        url: 'check/hiddennews',
        method: 'GET'
    })
});
$("#oldHiddenNews").click(function () {
    $.ajax({
        url: 'check/oldhiddennews',
        method: 'GET'
    })
});
$("#canPublish").click(function () {
    $.ajax({
        url: 'check/canpublish',
        method: 'GET'
    })
});
