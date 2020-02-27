/*
Template Name: Admin Template
Author: Wrappixel

File: js
*/
// ============================================================== 
// Auto select left navbar
// ============================================================== 
var NanocellSideBarmenu = function () {
    var url = window.location;
    var element = $('ul#sidebarnav a').filter(function () {
        return this.href == url;
    }).addClass('active').parent().addClass('active');
    while (true) {
        if (element.is('li')) {
            element = element.parent().addClass('in').parent().addClass('active').children('a').addClass('active');
        }
        else {
            break; 
        }
    }
    $('#sidebarnav a').on('click', function (e) {
        console.log($(this));
        if (!$(this).hasClass("active")) {
            // hide any open menus and remove all other classes
           // $("ul", $(this).parents("ul:first")).removeClass("in");
            //$("a", $(this).parents("ul:first")).removeClass("active");
            $('ul#sidebarnav a').filter(function () { return this.href }) .removeClass("active");
            $('ul#sidebarnav li').removeClass("active");
            // open our new menu and add the open class
            $(this).next("ul").addClass("in");
            $(this).addClass("active");
            $(this).parent().addClass("active");
            //$("ul", $(this).parents("ul:first")).parent().addClass("active");
            $(this).parents("ul:first").parent().addClass("active").children('a').addClass('active');
            console.log($(this).parents("ul:first"));
        }
        else if ($(this).hasClass("active")) {
            $(this).removeClass("active");
            $(this).parents("ul").removeClass("active");
            $(this).next("ul").removeClass("in");
        }
    });
    $('#sidebarnav >li >a.has-arrow').on('click', function (e) {
        //var ul = $(this).next("ul");
        //console.log(ul);
        //if (ul.hasClass("in")) {
        //    //e.preventDefault();
        //    ul.removeClass("in");
        //}
        //else {
        //    ul.addClass("in")
        //}
        e.preventDefault();
        
    });
};