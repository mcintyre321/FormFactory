$(document).on("focus", '[data-provide="ajax-typeahead"]', function () {
    $(this).attr("data-provide", "");
    $(this).typeahead({
        source: function(query, process) {
            return $.ajax({
                url: $(this)[0].$element[0].dataset.link,
                type: 'post',
                data: { query: query },
                dataType: 'json',
                success: function(data, result) {
                    return process(data);
                }
            });
        }
    });
});