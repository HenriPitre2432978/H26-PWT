// Execute the code when the DOM is fully loaded
$(function () {

    // 1
    // Change the color of all <h2> elements when the mouse enters/leaves
    // hover() takes two functions: mouseenter and mouseleave
    $("h2").hover(
        function () {
            $(this).css("color", "red"); // When hovering, text becomes red
        },
        function () {
            $(this).css("color", "black"); // When leaving, revert to black
        }
    );

    // 2
    // Hide the element with id="green" when the page loads
    $("#green").hide();

    // 3
    // Transform all checkboxes into jQuery UI styled checkboxradio widgets
    // icon:false removes the default icon from the checkbox
    $("input[type='checkbox']").checkboxradio({
        icon: false
    });
    
    // 4
    // When a rectangle is clicked:
    // - get its id (which represents a color)
    // - apply that color to all <label> elements
    $(".rectangle").click(
        function () {
            let couleur = $(this).attr("id"); // Get rectangle id (color name)
            $("label").css("color", couleur); // Change label text color
        }
    );

    // 5
    // When a checkbox is clicked:
    // - retrieve its value (which corresponds to a color name)
    // - toggle visibility of the rectangle with that color id
    $("input[type='checkbox']").click(function () {
        let couleur = $(this).val(); // Value contains color name
        $("#" + couleur).toggle(); // Show/hide the matching rectangle
    });

    // 6
    // Create a jQuery UI slider controlling the width of rectangles
    $("#slider").slider({
        min: 10,   // Minimum width
        max: 140,  // Maximum width
        value: 140, // Default starting width

        // Triggered when the slider moves
        slide: function (event, ui) {
            $(".rectangle").width(ui.value); // Change rectangle width
            $("#largeur label").text(ui.value + "px"); // Display current width
        }
    });

    // 7
    // Enable a tooltip on the slider element
    $("#slider").tooltip();

    // 8
    // Transform the button inside #message into a jQuery UI button
    // and add a "comment" icon
    $("#message button").button({
        icons: {
            primary: "ui-icon-comment"
        }
    });

    // Create a dialog box (initially hidden)
    $("#dialog").dialog({
        autoOpen: false, // Dialog won't open automatically
        modal: true      // Prevent interaction with background
    });

    // Open the dialog when the button is clicked
    $("#message button").click(function () {
        $("#dialog").dialog("open");
    });

    // 9
    // Create a jQuery UI button with a plus icon
    $("#add").button({
        icons: {
            primary: "ui-icon-plus"
        }
    });

    // When clicking "add":
    // - duplicate the first paragraph
    // - insert it before the button
    $("#add").click(function () {
        let texte = $("#paragraphe p:first").text(); // Get text of first paragraph
        $("<p>" + texte + "</p>").insertBefore("#add"); // Insert new paragraph
    });

    // 10
    // Create a button with a minus icon
    $("#del").button({
        icons: {
            primary: "ui-icon-minus"
        }
    });

    // Remove the last paragraph when clicking delete
    $("#del").click(function () {
        $("#paragraphe p").last().remove();
    });

    // 11
    // Add a play icon to the animation button
    $("#animation button").button({
        icons: {
            primary: "ui-icon-play"
        }
    });

    // Animate the button movement when clicked
    $("#animation button").click(function () {

        // Move right then return to original position
        $(this).animate({ left: "300px" }, "slow")
               .animate({ left: "0px" }, "slow");
    });

    // 12
    // Button with help icon used to trigger AJAX
    $("#ajax button").button({
        icons: {
            primary: "ui-icon-help"
        }
    });

    // Load external content using AJAX
    $("#ajax button").click(function () {
        $("#reponse").load("jquery.txt"); // Insert file content into #reponse
    });

    // 13
    // Activate jQuery UI date picker for selecting dates
    $("#datepicker").datepicker();

    // 14
    // Create a numeric spinner input with min/max limits
    $("#spinner").spinner({
        min: 0,
        max: 10
    });

    // 15
    // Double-click animation: rectangle fades out then fades in
    $(".rectangle").dblclick(function () {
        $(this).fadeOut().fadeIn();
    });

    // Highlight paragraphs when hovering
    $("#paragraphe p").hover(function () {
        $(this).css("background-color", "#ffffcc"); // Highlight
    }, function () {
        $(this).css("background-color", ""); // Remove highlight
    });

    // Create a button for resetting the slider
    $("#resetSlider").button();

    // Reset slider value and rectangle width to default
    $("#resetSlider").click(function () {
        $("#slider").slider("value", 140); // Reset slider
        $(".rectangle").width(140);        // Reset rectangle width
        $("#largeur label").text("140px"); // Update label text
    });

});