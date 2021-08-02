function update_page(page_data) {
    $('tbody').html('');

    var template = $('#tableSearchTemplate').html();

    $.each(page_data, function (i, val) {
        var temp = template;

        $.each(val, function (key, value) {
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
        url: "/Branches/Search",
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