
function Set_switcher(classname, colorhex) {
    elementname = document.querySelector(classname);
    switchery = new Switchery(elementname, {
        color: colorhex
    });
}

$(document).ready(function () {
    var array, elems, green, item, switchery, var_color_green;
    var_color_green = "#2b3c94";
    elems = Array.prototype.slice.call(document.querySelectorAll(".js-switch"));

    array = [];
    array.push(".js-switch-GreenSwitchery01");
    array.push(".js-switch-GreenSwitchery02");
    array.push(".js-switch-GreenSwitchery03");
    array.push(".js-switch-GreenSwitchery04");
    array.push(".js-switch-GreenSwitchery05");
    array.push(".js-switch-GreenSwitchery06");
    array.push(".js-switch-GreenSwitchery07");
    array.push(".js-switch-GreenSwitchery08");
    array.push(".js-switch-GreenSwitchery09");
    array.push(".js-switch-GreenSwitchery10");
    array.push(".js-switch-GreenSwitchery11");
    array.push(".js-switch-GreenSwitchery12");
    array.push(".js-switch-GreenSwitchery13");
    array.push(".js-switch-GreenSwitchery14");
    array.push(".js-switch-GreenSwitchery15");
    array.push(".js-switch-GreenSwitchery16");
    //array.forEach(function (entry) {
    //    green = document.querySelector(entry);
    //    switchery = new Switchery(green, {
    //        color: var_color_green
    //    });
    //});

    for (item in array) {
        Set_switcher(array[item], var_color_green);
    }


});
