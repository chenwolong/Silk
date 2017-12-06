
$(function () {

    var lastScrollTop = $(window).scrollTop();
    var headerAnimating = false;
    var footerAnimating = false;
    
    $('header ul li').on('click', function () {
        if ($(this).hasClass('desc')) {
            $(this).removeClass('desc');
        } else {
            $(this).addClass('desc');
        }
        
        $('header ul li').removeClass('selected');
        $(this).addClass('selected');
    });
    
    $('.products > ul > li > a > div').each(function () {
        $(this).children('div').last().css('margin-top', parseInt($(this).find('img').css('height')) * -0.5);
    });
    
    var onFooterAnimate = function () {};
    window.onFooterAnimate = function (callback) {
      onFooterAnimate = callback || function () {};
    };
    
    var headerFixHeight = parseInt($('header').height());
    $('header > div:eq(1)').css('top', headerFixHeight);
    
    $(window).on('scroll', function (e) {
        var headerTop = parseInt($('header > div:eq(1)').css('top'));
        var headerHeight = parseInt($('header > div:eq(1)').height());
        
        //console.log(headerFixHeight);
        /*
        if (!headerAnimating) {
            if (lastScrollTop > $(window).scrollTop()) {
                // down to up
                if (0 == $(window).scrollTop()) {
                    headerAnimating = true;
                    $('header > div:eq(1)').animate({
                        'top': headerFixHeight
                    }, function () { headerAnimating = false;});
                } else if (-headerHeight < headerTop) {
                    headerAnimating = true;
                    $('header > div:eq(1)').animate({
                        'top': headerFixHeight - headerHeight
                    }, function () { headerAnimating = false;});
                }
            } else {
                // up to down
                if (headerTop < headerFixHeight) {
                    headerAnimating = true;
                    $('header > div:eq(1)').animate({
                        'top': headerFixHeight
                    }, function () { headerAnimating = false;});
                }
            }
        }
        */

        var footerBottom = parseInt($('footer').css('bottom'));
        var footerHeight = parseInt($('footer').css('height'));
        
        if (!footerAnimating) {
            if (lastScrollTop > $(window).scrollTop()) {
                // down to up
                if (footerBottom < 0) {
                    footerAnimating = true;
                    onFooterAnimate(0);
                    $('footer').animate({
                        'bottom': 0
                    }, function () { footerAnimating = false;});
                }
            } else {
                // up to down
                if ($(document).height() - $(window).height() == $(window).scrollTop()) {
                    footerAnimating = true;
                    onFooterAnimate(0);
                    $('footer').animate({
                        'bottom': 0
                    }, function () { footerAnimating = false;});
                } else if (-footerHeight < footerBottom) {
                    footerAnimating = true;
                    onFooterAnimate(-footerHeight);
                    $('footer').animate({
                        'bottom': -footerHeight
                    }, function () { footerAnimating = false;});
                }
            }
        }
        
        lastScrollTop = $(window).scrollTop();
    });
    
    window.ShowFooterMenu = function(callback) {
//      var footerHeight = parseInt($('footer').css('height'));
      footerAnimating  = true;
      $('footer').animate({
        'bottom': 0
      }, function () { footerAnimating = false; callback();});
    };
});