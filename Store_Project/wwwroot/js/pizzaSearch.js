function update_page(page_data, currency) {
    $('#cards-pizza').html('');

    var template = $('#tableSearchTemplate').html();

    $.each(page_data, function (i, val) {
        var temp = template;

        $.each(val, function (key, value) {
            if (key == "pizza_image" && value != null) {
                temp = temp.replace('{' + key + '}', value.image_content);
            }
            else if (key == "pizza_toppings" && value.length > 0) {
                var tops = ''
                for (var i = 0; i < value.length; i++) {
                    tops += value[i].name + ' | ';
                }
                tops = tops.substring(0, tops.length - 3);
                temp = temp.replace('{' + key + '}', tops);
            }
            else if (key == "pizza_tags" && value.length > 0) {

                var base = "<div class='col-4 badge badge-secondary' style='background-color:{tag_color}'><span name='Tags' value='{tag_id}' disabled></span><label>{tag_name}</label> <br></div> "
                var tags = ''
                for (var i = 0; i < value.length; i++) {
                    var tagbase = base;
                    tagbase = tagbase.replace('{tag_color}', value[i].color);
                    tagbase = tagbase.replace('{tag_id}', value[i].id);
                    tagbase = tagbase.replace('{tag_name}', value[i].name);
                    tags += tagbase;
                }
                temp = temp.replace('{' + key + '}', tags);
            }
            else if (key == "with_cheese") {
                if (value == false)
                    temp = temp.replace('{' + key + '}', "With Cheese");
                else
                    temp = temp.replace('{' + key + '}', "Without Cheese");
            }
            else if (key == "price") {
                switch (currency) {
                    case "ILS":
                        temp = temp.replace('{' + key + '}', "<span>&#8362;</span>" + value.toFixed(2));
                        break;
                    case "USD":
                        temp = temp.replace('{' + key + '}', "<span>&#36;</span>" + value.toFixed(2));
                        break;
                    case "EUR":
                        temp = temp.replace('{' + key + '}', "<span>&#8364;</span>" + value.toFixed(2));
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
    var tops = $('#toppingsearch').val();

    var searchprice = $('#searchprice').val();
    $('#priceerror').html('');
    if (!searchprice) {
        searchprice == 9999;
    }
    else if ((parseFloat(searchprice) != searchprice) || parseFloat(searchprice) < 0) {
        $('#priceerror').append('Only Positive Values!');
        checked = false;
    }
    if (checked) {
        var form = $('#__AjaxAntiForgeryForm');
        var token = $('input[name="__RequestVerificationToken"]', form).val();
        $.ajax({
            type: 'post',
            url: "/Pizzas/Search",
            dataType: 'JSON',
            data: {
                __RequestVerificationToken: token,
                tagids: searchtags,
                searchquery: searchval,
                sauceid: sauceval,
                pricelimit: searchprice,
                currencyTo: cur,
                toppings: tops
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

    $('#toppingsearch').change(function (e) {
        handle_data(e);
    });
});