//** Property.System.Object **//
var ff = { behaviours: {} };
$(document).ready(function () {
    $(".ff-behaviour").live("focus", function () {
        var behaviour = $(this).data("ff-behaviour");
        if (ff.behaviours[behaviour]) {
            ff.behaviours[behaviour](this);
        }
    });
});
$(document).ready(function () {

    $(".ff-choices input.ff-choice-selector:!checked").live("change", function () { //unchecked choice radios
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

    $(".ff-choices .ff-choice").live("click", function (e) {
        if ($(e.target).parents().index($(this)) >= 0) {
            var option = $(this).find("> * > .ff-choice-selector[disabled!='disabled'][checked!='checked']");
            if (option.length) {
                var choicesArea = $(this).closest(".ff-choices-area");
                var picker = choicesArea.find(".ff-choice-picker").not(choicesArea.find(".ff-choices-area .ff-choice-picker"));
                if (picker.length) {
                    picker.find("option:eq(" + $(this).index() + ")").attr("selected", "selected").trigger("change");
                } else {
                    option.attr("checked", "checked").trigger("change");
                }
                e.stopPropagation();
                $(e.target).click();
            }
        }
    });

    $(".ff-choice-picker").live("change", function () {
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


    $(document).ready(function () {
        $(".ff-add-item").live("click", function (e) {
            var $form = $($('<div/>').html($(this).closest("li").find("script[type='text/html']").html()).text());
            var modelName = $(this).data("modelname");
            var renumber = function (index, attr) {
                if (!attr) return attr;
                return modelName + "[" + newIndex + "]." + attr;
            };

            var newObject = $('<li>').append($form.children().clone());
        

            var newIndex = $(this).closest("ul").children().length - 1;


            $(newObject).insertBefore($(this).closest(".ff-collection").find("> ul").children().last());

            $(":input", newObject).attr("name", renumber).attr("id", renumber);
            $(newObject).find("[data-valmsg-for]").attr("data-valmsg-for", renumber);

            $form.find(":input").val(null);
            if ($.validator.unobtrusive.parseDynamicContent) {
                $.validator.unobtrusive.parseDynamicContent(newObject);
            }

            return false;
        }); // end on click
        $(".ff-remove-parent").live("click", function () {
            $(this).closest("li").remove();
            return false;
        }); // live click
        $(".ff-move-up").live("click", function () {
            $(this).closest("li").prev().before($(this).closest("li"));
            return false;
        }); // live click
        $(".ff-move-down").live("click", function () {
            $(this).closest("li").next(":not(.ff-not-collection-item)").after($(this).closest("li"));
            return false;
        }); // live click
    })