jQuery.validator.setDefaults({
    highlight: function (element, errorClass, validClass) {
        //if (element.type === 'radio') {
          ///  this.findByName(element.name).addClass(errorClass).removeClass(validClass);
        //} else {
        //    $(element).addClass(errorClass).removeClass(validClass);
            $(element).closest('.control-group').removeClass('has-success').addClass('has-error');
        //}
    },
    unhighlight: function (element, errorClass, validClass) {
        //if (element.type === 'radio') {
        //    this.findByName(element.name).removeClass(errorClass).addClass(validClass);
        //} else {
        //    $(element).removeClass(errorClass).addClass(validClass);
            $(element).closest('.control-group').removeClass('has-error').addClass('has-success');
        //}
    }
});