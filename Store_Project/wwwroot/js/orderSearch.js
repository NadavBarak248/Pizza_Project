function update_page(page_data) {
    $('#allorders').html('');

    var template = $('#tableSearchTemplate').html();

    $.each(page_data, function (i, val) {
        var temp = template;

        $.each(val, function (key, value) {
            console.log(key)
            if (key == "pizzaOrder") {
                var base = "<a href='\\Pizzas\\Details\\{id}'>{name}</a> ";
                var pizzas = '';
                for (var i = 0; i < value.length; i++) {
                    var pizzabase = base;
                    pizzabase = pizzabase.replace('{id}', value[i].pizzas.id);
                    pizzabase = pizzabase.replace('{name}', value[i].pizzas.name);
                    pizzas += pizzabase;
                }
                temp = temp.replace('{pizzas}', pizzas);
            }

            else if (key == "user_order") {
                temp = temp.replaceAll('{' + key + '}', value.username);
            }

            else if (key == "branch") {
                temp = temp.replaceAll('{' + key + '}', value.branch_name);
            }

            
            else {
                temp = temp.replaceAll('{' + key + '}', value);
            }
        });

        $('#allorders').append(temp);
    });
}

function handle_data(e) {
    e.preventDefault();
    var checked = true;
    var searchuser = $('#searchuser').val();
    var searchbranch = $('#searchbranch').val();
    var searchprice = $('#searchprice').val();
    var searchdate = $('#orderdate').val()
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
            url: "/Orders/Search",
            dataType: 'JSON',
            data: {
                __RequestVerificationToken: token,
                user: searchuser,
                branch: searchbranch,
                pricelimit: searchprice,
                endingdate: searchdate
            }
        }).done(function (ret_data) {
            update_page(ret_data);
        });
    }
}



$(function () {
    $('#searchuser').keyup(function (e) {
        handle_data(e);
    });
    $('#searchprice').keyup(function (e) {
        handle_data(e);
    });
    $('#searchbranch').keyup(function (e) {
        handle_data(e);
    });

    $('#orderdate').change(function (e) {
        handle_data(e);
    });
});