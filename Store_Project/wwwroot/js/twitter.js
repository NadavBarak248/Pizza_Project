// -------twitter------------
function embedTweet(tweets) {
    for (var i = 0; i < tweets.data.length; i++) {
        var template = $('#hidden-template').html();
        template = template.replaceAll("{id}", i);
        $('tbody').append(template);
    }

    for (var i = 0; i < tweets.data.length; i++) {
        twttr.widgets.createTweet(tweets.data[i].id, document.getElementById('tweet' + i), { theme: 'light' });
    }
}
