//** Property.System.Object **//
var ff = { behaviours: {} };
$(document).ready(function () {
    $(".ff-behaviour").on("focus", function () {
        var behaviour = $(this).data("ff-behaviour");
        if (ff.behaviours[behaviour]) {
            ff.behaviours[behaviour](this);
        }
    });
});
$(document).ready(function() {
    
    $(".ff-choices input.ff-choice-selector:!checked").on("change", function() { //unchecked choice radios
        var choiceArea = $(this).closest(".ff-choice");
        var choices = choiceArea.closest(".ff-choices");
        choices
            .find("> .ff-choice")
            .find(":input")
            .not(choices.find(".ff-choice-selector")
                    .not(choices.find(".ff-choices .ff-choice-selector")))
            .attr("disabled", "disabled");
        var myInputs = choiceArea.find(":input").not(choiceArea.find(".ff-choice input"));
        myInputs.attr("disabled", null);
        var childChoices = choiceArea.find(".ff-choice").not(choiceArea.find(".ff-choice .ff-choice"));
        childChoices.find(".ff-choice-selector").not(childChoices.find(".ff-choices .ff-choice-selector"))
            .attr("disabled", null).not("[checked!='checked']").trigger("change");
    });
    
    $(".ff-choices .ff-choice").on("click", function(e) {
        if ($(e.target).parents().index($(this)) >= 0) {
            var option = $(this).find("> * > .ff-choice-selector[disabled!='disabled'][checked!='checked']");
            if (option.length) {
                option.attr("checked", "checked").trigger("change");
                e.stopPropagation();
                $(e.target).click();
            }
        }
    });
    
    $(".ff-choice-picker").on("change", function() {
        var choices = $(this).closest(".ff-choices-area").find("> .ff-choices");
        var radios = choices.find(".ff-choice-selector")
            .not(choices.find(".ff-choices .ff-choice-selector"));
        radios.closest(".ff-choice").hide();
        $(radios[$(this).val()]).attr("checked", "checked").trigger("change").closest(".ff-choice").show();

    });
});


//** Property.System.DateTime ** //


$.extend(ff.behaviours, {},
    {
        datepicker: function (t) {
            if (!$(t).hasClass("hasDatepicker") && $.datepicker) {
                $(t).datepicker({ showOn: "focus" }).focus();
                return true;
            }
        },
        datetimepicker: function (t) {
            if (!$(t).hasClass("hasDatepicker") && $.datetimepicker) {
                $(t).datetimepicker({ showOn: "focus" }).focus();
                return true;
            }
        }
    }
);