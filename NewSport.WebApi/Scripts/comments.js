function dateAgo(date) {
    var seconds = (new Date().getTime() - date) / 1000;
    if (seconds < 60) {
        return parseInt(seconds) + " sekund temu";
    } else if (seconds >= 60 && seconds < 3600) {
        return parseInt(seconds / 60) + " minut temu";
    } else if (seconds >= 3600 && seconds < 86400) {
        return parseInt(seconds / 3600) + " godzin temu";
    } else {
        return parseInt(seconds / 86400) + " dni temu";
    }
}
function processData(data) {
    var target = $("#comments");
    target.empty();
    target.append("<div class='page-header'>\n"
        + "<h1><small class='pull-right'>" + data.length + " Comments </small> Comments </h1>\n"
        + "</div>");


    for (var i = data.length - 1; i >= 0; i--) {
        console.log(data.Message);
        var comment = data[i];
        target.append("<div class='comments-list'>\n"
            + "<div class='media'>\n"
            + "<p class='pull-right'>\n"
            + "<small>" + dateAgo(parseInt(comment.CommentsDate.substring(6, comment.CommentsDate.length - 2))) + "</small>\n"
            + "</p>\n"
            + "<a class='media-left' href='#'>\n"
            + "<img src='http://lorempixel.com/40/40/people/1/'>\n"
            + "</a>\n"
            + "<div class='media-body'>\n"
            + "<h4 class='media-heading user_name'>" + comment.Author.Username + "</h4>\n"
            + comment.Message + "\n"
            + "<p>"
            + "<small><a href=''>Like</a> - <a href=''>Share</a></small>"
            + "</p>"
            + "</div>"
            + "</div>"
            + "</div>");
    }
}