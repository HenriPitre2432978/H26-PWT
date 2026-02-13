
$(function () {
//1
    $("h2").hover(
        function () {
            $(this).css("color", "red");
        },
        function () {
            $(this).css("color", "black");
        }
    );

    //2
    $("#green").hide();

    //3
    $("input[type='checkbox']").checkboxradio({
        icon: false
    });
    
    //4
    $(".rectangle").click(
        function () {
            let couleur = $(this).attr("id");
                $("label").css("color", couleur);
            }
    );

    //5
       $("input[type='checkbox']").click(function () {
        let couleur = $(this).val();
        $("#" + couleur).toggle();
    });
        //6
        $("#slider").slider({
        min: 10,
        max: 140,
        value: 140,
        slide: function (event, ui) {
            $(".rectangle").width(ui.value);
            $("#largeur label").text(ui.value + "px");
        }
    });

    //7
        $("#slider").tooltip();

        //8
        $("#message button").button({
    icons: {
        primary: "ui-icon-comment"
    }
});

$("#dialog").dialog({
    autoOpen: false,
    modal: true
});

// Ouvrir au clic
$("#message button").click(function () {
    $("#dialog").dialog("open");
});

//9 

$("#add").button({
    icons: {
        primary: "ui-icon-plus"
    }
});

$("#add").click(function () {
    let texte = $("#paragraphe p:first").text();
    $("<p>" + texte + "</p>").insertBefore("#add");
});

//10
$("#del").button({
    icons: {
        primary: "ui-icon-minus"
    }
});

$("#del").click(function () {
    $("#paragraphe p").last().remove();
});

//11
$("#animation button").button({
    icons: {
        primary: "ui-icon-play"
    }
});

$("#animation button").click(function () {
    $(this).animate({ left: "300px" }, "slow")
           .animate({ left: "0px" }, "slow");
});

//12
$("#ajax button").button({
    icons: {
        primary: "ui-icon-help"
    }
});

$("#ajax button").click(function () {
    $("#reponse").load("jquery.txt");
});

//13
$("#datepicker").datepicker();

//14
$("#spinner").spinner({
    min: 0,
    max: 10
});

//15
$(".rectangle").dblclick(function () {
    $(this).fadeOut().fadeIn();
});

$("#paragraphe p").hover(function () {
    $(this).css("background-color", "#ffffcc");
}, function () {
    $(this).css("background-color", "");
});

$("#resetSlider").button();

$("#resetSlider").click(function () {
    $("#slider").slider("value", 140);
    $(".rectangle").width(140);
    $("#largeur label").text("140px");
});

});
