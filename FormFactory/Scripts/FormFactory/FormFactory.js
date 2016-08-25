//** Property.System.Object **//
var ff = {
    behaviours: {},
    transforms: {}
};
$(document).on("focus", ".ff-behaviour", function () {
    var behaviour = $(this).data("ff-behaviour");
    if (ff.behaviours[behaviour]) {
        ff.behaviours[behaviour](this);
    }
});

$(document).on("change", ".ff-choices input.ff-choice-selector", function () { //unchecked choice radios
    var choiceArea = $(this).closest(".ff-choice");
    var choices = choiceArea.closest(".ff-choices");
    choices
            .find("> .ff-choice")
            .find(":input")
            .not(choices.find(".ff-choice-selector")
                    .not(choices.find(".ff-choices .ff-choice-selector")))
            .attr("disabled", "disabled").each(function () {
                $("span[data-valmsg-for='" + $(this).attr("name") + "']").css("display", "none");
                if ($.validator) {
                    $.validator.defaults.unhighlight(this);
                }
            });
    var myInputs = choiceArea.find(":input").not(choiceArea.find(".ff-choice input"));
    myInputs.attr("disabled", null).each(function () {
        if ($("span[data-valmsg-for='" + $(this).attr("name") + "']").css("display", "").hasClass("field-validation-error")) {
            if ($.validator) {
                $.validator.defaults.highlight(this);
            }
        }

    });

    var childChoices = choiceArea.find(".ff-choice").not(choiceArea.find(".ff-choice .ff-choice"));
    childChoices.find(".ff-choice-selector").not(childChoices.find(".ff-choices .ff-choice-selector"))
            .attr("disabled", null).not("[checked!='checked']").trigger("change");
});

$(document).on("click", ".ff-choices .ff-choice", function (e) {
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

$(document).on("change", ".ff-choice-picker", function () {
    var choices = $(this).closest(".ff-choices-area").find("> .ff-choices");
    var radios = choices.find(".ff-choice-selector")
            .not(choices.find(".ff-choices .ff-choice-selector"));
    radios.closest(".ff-choice").hide();
    $(radios[$(this).val()]).attr("checked", "checked").trigger("change").closest(".ff-choice").show();

});


//** Property.System.DateTime ** //


$.extend(ff.behaviours, {},
    {
        datepicker: function (t) {
            if (!$(t).hasClass("hasDatepicker") && $.datepicker && !$(t).attr("readonly")) {
                // TODO: a more comprehensive switch of .NET to jQuery formats. REF: http://docs.jquery.com/UI/Datepicker/formatDate
                var format = $(t).data("ff-format");
                switch (format) {
                    case "dd MMM yyyy":
                        format = "dd M yy";
                        break;
                    case "dd/MM/yyyy":
                        format = "dd/mm/yy";
                        break;
                }
                $(t).datepicker({ showOn: "focus", dateFormat: format }).focus();
                return true;
            }
            return false;
        },
        datetimepicker: function (t) {
            if (!$(t).hasClass("hasDatepicker") && $.fn.datetimepicker && !$(t).attr("readonly")) {
                $(t).datetimepicker({ showOn: "focus" }).focus();
                return true;
            }
            return false;
        }
    }
);



//Collections

$.extend(ff.transforms,
    { remove: function ($el) { $el.remove(); } },
    { swap: function ($el1, $el2) {
        $el1.before($el2);
    }
    });

$(document).ready(function () {
    function newId() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
    $(document).on("click", ".ff-add-item", function (e) {
        var $form = $($('<div/>').html($(this).closest("li").find("script[type='text/html']").html()));

        var modelName = $(this).data("modelname");
        var newObject = $('<li>').append($form.children().clone());
        newObject.find("> *").css("display", "");

        var newIndex = newId(); // $(this).closest("ul").children().length - 1;
        var renumber = function (index, attr) {
            if (!attr) return attr;
            return modelName + "[" + newIndex + "]." + attr;
        };



        $(newObject).insertBefore($(this).closest(".ff-collection").find("> ul").children().last());

        $(":input", newObject).attr("name", renumber).attr("id", renumber);
        $("input[type='hidden']", newObject).first().attr("name", modelName + ".Index").val(newIndex);

        $(newObject).find("[data-valmsg-for]").attr("data-valmsg-for", renumber);

        $form.find(":input").val(null);
        if ($.validator) {
            if ($.validator.unobtrusive.parseDynamicContent) {
                $.validator.unobtrusive.parseDynamicContent(newObject);
            }
        }
        return false;
    }); // end on click
    $(document).on("click", ".ff-remove-parent", function () {
        ff.transforms.remove($(this).closest("li"));
        return false;
    }); // live click
    $(document).on("click", ".ff-move-up", function () {
        ff.transforms.swap($(this).closest("li").prev(), $(this).closest("li"));

        return false;
    }); // live click
    $(document).on("click", ".ff-move-down", function () {
        ff.transforms.swap($(this).closest("li"), $(this).closest("li").next(":not(.ff-not-collection-item)"));
        return false;
    });

    $(document).on("dragstart", function (e) {
        $(this).addClass('ff-dragging');
        e.originalEvent.dataTransfer.setData("text", e.srcElement.id);
    });
    $(document).on("dragover", function(event) {
        event.preventDefault();
        event.stopPropagation();
    });

    $(document).on("dragleave", function (event) {
        event.preventDefault();
        event.stopPropagation();
        $(this).removeClass('ff-dragging');
    });

    $(document).on("drop", function (e) {
        e.preventDefault();
        e.stopPropagation();
        var $src = $(document.getElementById(e.originalEvent.dataTransfer.getData("text")))
            .closest("li[draggable=true]");
        var $target = $(e.target).closest("li[draggable=true]");
        if ($target.length && $target[0] !== $src[0] && $target[0].parentElement === $src[0].parentElement) {
            var beforeOrAfter = $src.index() > $target.index();
            $src.remove();
            if (beforeOrAfter) {
                $src.insertBefore($target);
            } else {
                $src.insertAfter($target);
            }
            console.log("dropped");
        }
    });
});
if ($.validator) {
    $.validator.setDefaults({
        highlight: function (element) {
            $(element).closest(".form-group").addClass("error");
        },
        unhighlight: function (element) {
            $(element).closest(".form-group").removeClass("error");
        }
    });
}
;
$(document).on("click keydown", "input[type='checkbox']", function () {
    return !($(this).attr("readonly"));
});
 