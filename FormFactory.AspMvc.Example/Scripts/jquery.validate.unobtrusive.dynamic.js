/// <reference path="../jquery-1.7.js" />
/// <reference path="../jquery.validate.js" />
/// <reference path="../jquery.validate.unobtrusive.js" />

// Originally by XHalent, @xhalent
// http://xhalent.wordpress.com/
// http://xhalent.wordpress.com/2011/01/24/applying-unobtrusive-validation-to-dynamic-content/

// Modified by Joel Purra
// http://joelpurra.com/
// https://gist.github.com/gists/2360001

(function ($, undefined) {
    $.validator.unobtrusive.parseDynamicContent = function (selector) {

        var $selector;

        if (selector instanceof jQuery) {
            $selector = selector;
        }
        else {
            $selector = $(selector);
        }

        if ($selector.length == 0) {
            return;
        }

        //use the normal unobstrusive.parse method
        $.validator.unobtrusive.parse($selector);

        //get the relevant form
        var form = $selector.first().closest('form');

        //get the collections of unobstrusive validators, and jquery validators
        //and compare the two
        var unobtrusiveValidation = form.data('unobtrusiveValidation');
        var validator = form.validate();

        $.each(unobtrusiveValidation.options.rules, function (elname, elrules) {
            if (validator.settings.rules[elname] == undefined) {
                var args = {};
                $.extend(args, elrules);
                args.messages = unobtrusiveValidation.options.messages[elname];
                //edit:use quoted strings for the name selector
                $("[name='" + elname + "']", $selector).rules("add", args);
            } else {
                $.each(elrules, function (rulename, data) {
                    if (validator.settings.rules[elname][rulename] == undefined) {
                        var args = {};
                        args[rulename] = data;
                        args.messages = unobtrusiveValidation.options.messages[elname][rulename];
                        //edit:use quoted strings for the name selector
                        $("[name='" + elname + "']", $selector).rules("add", args);
                    }
                });
            }
        });
    };
})(jQuery);