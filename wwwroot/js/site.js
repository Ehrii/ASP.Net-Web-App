// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
  $(".edit-habit-btn").click(function () {
    let habitId = $(this).data("id");
    let habitName = $(this).data("name");
    let dueDate = $(this).data("date");
    let timeIn = $(this).data("timein");
    let timeOut = $(this).data("timeout");

    console.log("Habit ID:", habitId);
    console.log("Habit Name:", habitName);
    console.log("Due Date:", dueDate);
    console.log("Time In:", timeIn);
    console.log("Time Out:", timeOut);

    $("#habitId").val(habitId);
    $("#habitName").val(habitName);
    $("#dueDate").val(dueDate);
    $("#timeIn").val(timeIn);
    $("#timeOut").val(timeOut);
  });
});

$("#timeIn").timepicker({
  uiLibrary: "bootstrap5",
});
$("#timeOut").timepicker({
  uiLibrary: "bootstrap5",
});
