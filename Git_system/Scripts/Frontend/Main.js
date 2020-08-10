//GIT_NAV เพิ่มคลาสให้กับ GIT_NAV
$(document).ready(function () {
    var number_of_Nav = $('.GIT_Nav').size();
    var item = 1;
    while (item <= number_of_Nav) {
        var number_of_items = $('.GIT_Nav:nth-child(' + item + ')').children().size();
        end = number_of_items - 1;
        $('.GIT_Nav:nth-child(' + item + ')').children().slice(0, end).addClass('children');
        item++;
    }
});

//Cokie
function getCookie(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    } else {
        begin += 2;
    }
    var end = document.cookie.indexOf(";", begin);
    if (end == -1) {
        end = dc.length;
    }
    return unescape(dc.substring(begin + prefix.length, end));
}

function setLanguage(language) {
    var xmlhttp = new XMLHttpRequest();
    var url = "/Home/ChangeLanuage?language=" + language;
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {;
            location.reload();
        }
    }
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}