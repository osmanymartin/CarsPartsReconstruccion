$(function () {
    var ajaxFormSubmit = function () {
        debugger;
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-carpart-target"));
            $target.replaceWith(data);
        });

        return false;
    }

    $("form[data-carpart-ajax='true']").submit(ajaxFormSubmit);
});