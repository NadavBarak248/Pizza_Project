function update_page(page_data) {
    $('tbody').html('');

    var template = $('#tableSearchTemplate').html();

    $.each(page_data, function (i, val) {
        var temp = template;

        $.each(val, function (key, value) {
            if (key == "pizza_tag") {
                var tags = ''
                if (value.length > 0)
                    for (var i = 0; i < value.length; i++) {
                        tags += value[i].name + '<br />';
                    }
                    tags = tags.substring(0, tags.length - 6);
                temp = temp.replace('{pizzas}', tags);
                temp = temp.replace('{count}', value.length);
            }
            else
                temp = temp.replaceAll('{' + key + '}', value);
        });

        $('tbody').append(temp);
    });
}

function handle_data(e) {
    e.preventDefault();
    var searchval = $('#searchQuery').val();

    $.ajax({
        type: 'post',
        url: "/Tags/Search",
        dataType: 'JSON',
        data: {
            searchquery: searchval
        }
    }).done(function (ret_data) {
        update_page(ret_data);
    });
}


$(function () {
    $('#searchQuery').keyup(function (e) {
        handle_data(e);
    });
});