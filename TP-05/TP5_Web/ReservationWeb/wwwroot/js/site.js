// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Code for spinner and date/hour in reservation form
$(function () {

    if ($("#spinner").length) {
        $("#spinner").spinner({
            min: 1,
            max: 20
        });
    }

    if ($("#datetimepicker").length) {
        flatpickr("#datetimepicker", {
            enableTime: true,
            time_24hr: true,
            minuteIncrement: 15,
            dateFormat: "Y-m-d H:i",
            minDate: "today",
            maxDate: new Date().fp_incr(365),
            allowInput: true,
            clickOpens: true
        });
    }

});