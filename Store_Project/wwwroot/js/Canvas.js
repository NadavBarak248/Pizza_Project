//---------------------canvas--------

$(function () {
    var img = new Image();
    img.onload = function () {
        var ctx = $("#logoCanvas").get(0).getContext("2d");
        ctx.drawImage(img, 0, 0, 100, 50);
    }

    img.src = '/pictures/MonAmour.jpg'
})
