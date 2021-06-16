function update_page(page_data, currency) {
    $('#cards-pizza').html('');

    var template = $('#tableSearchTemplate').html();

    $.each(page_data, function (i, val) {
        var temp = template;

        $.each(val, function (key, value) {
            if (key == "pizza_image" && value != null) {
                temp = temp.replace('{' + key + '}', value.image);
            }
            else if (key == "pizza_tags" && value.length > 0) {
                var tags = ''
                for (var i = 0; i < value.length; i++) {
                    tags += value[i].name + ' | ';
                }
                tags = tags.substring(0, tags.length - 3);
                temp = temp.replace('{' + key + '}', tags);
            }
            else if (key == "with_cheese") {
                if (value == false)
                    temp = temp.replace('{' + key + '}', "ללא גבינה");
                else
                    temp = temp.replace('{' + key + '}', "עם גבינה");
            }
            else if (key == "price") {
                switch (currency) {
                    case "ILS":
                        temp = temp.replace('{' + key + '}', value + " שקלים");
                        break;
                    case "USD":
                        temp = temp.replace('{' + key + '}', value + " דולר");
                        break;
                    case "EUR":
                        temp = temp.replace('{' + key + '}', value + " יורו");
                        break;
                }
            }
            else {
                temp = temp.replaceAll('{' + key + '}', value);
            }
        });

        $('#cards-pizza').append(temp);
    });
}

function handle_data(e) {
    e.preventDefault();
    var checked = true;
    var searchval = $('#searchQuery').val();
    var sauceval = $('#saucesearch').val();
    var searchtags = $('#tagsearch').val();
    var cur = $('#currency').val();

    var searchprice = $('#searchprice').val();
    $('#priceerror').html('');
    if (!searchprice) {
        searchprice == 9999;
    }
    else if ((parseFloat(searchprice) != searchprice) || parseFloat(searchprice) < 0) {
        $('#priceerror').append('ערכים חיוביים בלבד!');
        checked = false;
    }
    if (checked) {
        $.ajax({
            type: 'post',
            url: "/Pizzas/Search",
            dataType: 'JSON',
            data: {
                tagids: searchtags,
                searchquery: searchval,
                sauceid: sauceval,
                pricelimit: searchprice,
                currencyTo: cur
            }
        }).done(function (ret_data) {
            update_page(ret_data, cur);
        });
    }
}



$(function () {
    $('#searchQuery').keyup(function (e) {
        handle_data(e);
    });
    $('#searchprice').keyup(function (e) {
        handle_data(e);
    });
    $('#saucesearch').change(function (e) {
        handle_data(e);
    });

    $('#tagsearch').change(function (e) {
        handle_data(e);
    });

    $('#currency').change(function (e) {
        handle_data(e);
    });
});