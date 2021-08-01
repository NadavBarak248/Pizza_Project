// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// ---------------------------------- Restaurants map/cards switch ---------------------
$(function () {
    $('#restSwitchButton').on("click", function (e) {
        e.preventDefault();
        $("#map").show();
        if ($("#cardDiv").is(":hidden")) {
            $("#cardDiv").show();
            $("#map").hide()
            $("#modeIndicator").html("Cards");
        }
        else {
            $("#cardDiv").hide();
            $("#map").show();
            $("#modeIndicator").html("Map");
        }

    });
});
