$(function() {
/*
  $('.preview').css(
      'background-image',
      'url("'
          + $('.preview .color-select div.container a:first img').attr('src')
          + '")');

  if (1 == $('.preview .color-select div.container a').length) {
    // color only one, hide select bar
    $('.preview .color-select').hide();
  } else {
    var selectColorToggle = false;
    var _width = parseInt($('.preview .color-select').children().eq(1).css(
        'width'))
        + parseInt($('.preview .color-select').children().eq(1).css(
            'padding-left'))
        * 2
        + parseInt($('.preview .color-select').children().eq(0).css(
            'border-right-width'));

    var allImageWidth = $('.preview .color-select div.container a').length
        * (parseInt($('.preview .color-select div.container a').width()) + parseInt($(
            '.preview .color-select div.container a').css('padding-left')) * 2);
    var containerWidth = Math.min($(window).width() - _width, allImageWidth);

    $('.preview .color-select div.container').css('width', allImageWidth);
    $('.preview .color-select').css({
      'right' : -containerWidth,
    });

    $('.preview .color-select > div:last').css({
      'width' : containerWidth
    });

    $('.preview .color-select div.container a').on(
        'click',
        function() {
          var fadeInDiv = $('<div></div>');
          fadeInDiv.css({
            'width' : $('.preview img').css('width'),
            'height' : $('.preview img').css('height'),
            'position' : 'absolute',
            'left' : '0px',
            'top' : '0px',
            'background-size' : 'cover'
          });

          fadeInDiv.css('background-image', 'url("'
              + $(this).children('img').attr('src') + '")');
          $('.preview').prepend(fadeInDiv);
          var _this = $(this);

          fadeInDiv.fadeTo(0, 0);
          fadeInDiv.fadeTo(1000, 1, function() {
            fadeInDiv.remove();
            $('.preview').css('background-image',
                'url("' + _this.children('img').attr('src') + '")');
          });

          return false;
        });

    $('.preview .color-select > div').not(
        $('.preview .color-select > div:last')).on(
        'click',
        function() {

          if (!selectColorToggle) {
            $('.preview .color-select').animate({
              'right' : 0,
            });

            var borderWidth = $('.preview .color-select').children().eq(0).css(
                'border-right-width');
            $('.preview .color-select').children().eq(0).animate({
              'border-right-width' : '0px'
            });

            $('.preview .color-select').children().eq(2).animate({
              'border-left-width' : borderWidth
            });

          } else {

            $('.preview .color-select').animate(
                {
                  'right' : -parseInt($('.preview .color-select > div:last')
                      .css('width')),
                });

            var borderWidth = $('.preview .color-select').children().eq(2).css(
                'border-left-width');
            $('.preview .color-select').children().eq(0).animate({
              'border-right-width' : borderWidth
            });

            $('.preview .color-select').children().eq(2).animate({
              'border-left-width' : 0
            });
          }

          selectColorToggle = !selectColorToggle;
        });
  }
*/

  $('.preview-container').yxMobileSlider({height: $('.preview-container ul li:eq(0) img').height()});

  // select size type and so on
  $('.products .options ul li a').click(function() {
    return false;
    if ($(this).hasClass('selected')) {
      $(this).removeClass('selected');
      $('.products .operator-bar input').attr('disabled', 'disabled');
      return false;
    }
    $(this).parent().parent().find('a').removeClass('selected');
    $(this).addClass('selected');

    var allTypeSelected = true;
    $('.products .options > div').each(function() {
      var allSelects = $(this).find('ul li a').length;
      if (allSelects > 0) {
        console.log($(this).find('ul li a.selected').length);
        if ($(this).find('ul li a.selected').length < 1) {
          allTypeSelected = false;
          return false;
        }
      }
    });

    if (allTypeSelected) {
      $('.footer-menus ul li.operator input').removeAttr('disabled');
    } else {
      $('.footer-menus ul li.operator input').attr('disabled', 'disabled');
    }

    return false;
  });

  $('.products .options > div:last-child input[type=button]').eq(0).click(
      function() {
        $(this).next().val($(this).next().val() - 1);
        $('.products .options > div:last-child input[type=button]').eq(1)
            .removeAttr('disabled');
        if (1 == $(this).next().val()) {
          $(this).attr('disabled', 'disabled');
        }
        
        if (parseInt($(this).next().val()) < 1) {
          $('.footer-menus ul li.operator input').attr('disabled', 'disabled');
        }
      });

  $('.products .options > div:last-child input[type=button]').eq(1).click(
      function() {
        if (parseInt($(this).prev().val()) < parseInt($(this).next().find(
            'span').text())) {
          $(this).prev().val(parseInt($(this).prev().val()) + 1);
          $('.products .options > div:last-child input[type=button]').eq(0)
              .removeAttr('disabled');
        }
        if ($(this).prev().val() == $(this).next().find('span').text()) {
          $(this).attr('disabled', 'disabled');
        }
        if (parseInt($(this).prev().val()) > 0) {
          $('.footer-menus ul li.operator input').removeAttr('disabled');
        }
      });

  //$('.products .options > div:last-child input:eq(1)').val(1);
  
  $('.products .options > div:last-child input:eq(1)').change(function () {
    if ($(this).val() >= $(this).next().next().find('span').text()) {
      $(this).val($(this).next().next().find('span').text());
    }
        
    if (parseInt($(this).val()) > 0) {
      $('.footer-menus ul li.operator input').removeAttr('disabled');
      $('.products .options > div:last-child input:eq(0)').removeAttr('disabled');
      if (parseInt($(this).val()) < $(this).next().next().find('span').text()) {
        $('.products .options > div:last-child input:eq(2)').removeAttr('disabled');
      } else {
        $('.products .options > div:last-child input:eq(2)').attr('disabled', 'disabled');
      }
    } else {
      $('.products .options > div:last-child input:eq(0)').attr('disabled', 'disabled');
      $('.products .options > div:last-child input:eq(2)').removeAttr('disabled');
      $('.footer-menus ul li.operator input').attr('disabled', 'disabled');
    }
  });

  // click tab
  $('.products .page-tab a').click(function() {
    if ($(this).hasClass('selected'))
      return false;
    $('.products .page-tab a').removeClass('selected');
    $(this).addClass('selected');
    $('.product-detail').hide();
    $('.property').hide();
    $('.comment').hide();
    return false;
  });

  $('.products .page-tab a').eq(0).click(function() {
    $('.product-detail').show();
    $(window).scroll();
    return false;
  });

  $('.products .page-tab a').eq(1).click(function() {
    $('.comment').show();
    $(window).scroll();
    return false;
  });

  var pageTabFooDiv = null;
  
  var toTop = null;
  $(window).on('scroll', function() {
    
    if (pageTabFooDiv) {
      if ($(window).scrollTop() <= pageTabFooDiv.offset().top) {
        pageTabFooDiv.remove();
        pageTabFooDiv = null;
        $('.products .page-tab').css({
          'position': 'static',
          'z-index': 1
        });
        
        if (toTop) {
          toTop.fadeOut(function () {
            toTop.remove();
            toTop = null;
          });
        }
      }
    } else {
      if ($(window).scrollTop() >= $('.products .page-tab').offset().top) {
        pageTabFooDiv = $('<div></div>');
        pageTabFooDiv.css({
          'width': $('.products .page-tab').css('width'),
          'height': $('.products .page-tab').css('height'),
        });
        pageTabFooDiv.insertBefore($('.products .page-tab'));
        $('.products .page-tab').css({
          'position' : 'fixed',
          'top' : '0px',
          'left' : '0px'
        });
        
        toTop = ScrollToTopDiv('PageTab', function () {
          toTop.fadeOut(function () {
            toTop.remove();
            toTop = null;
          });
        });
        toTop.fadeIn();
        $(document.body).append(toTop);
      } 
    }
    
  });

  // click cart
  $('.footer-menus ul li:eq(1) input').click(function() {
    addToCartAnimation($('.products .options > div:last-child input').eq(1),
      $('.products .options > div:last-child input').eq(1).val());

  });

  function addToCartAnimation(elem, num) {
    var animateIcon = $('<div>' + num + '</div>');
    animateIcon.css({
      'width' : '16px',
      'height' : '16px',
      'line-height' : '16px',
      'text-align' : 'center',
      'padding' : '2px',
      'border-radius' : '50%',
      //'border' : '1px solid #fff',
      'color' : '#fff',
      'font-size' : '14px',
      'background-color' : '#f27501',
      'position' : 'absolute',
      'z-index' : '10',
      padding: 2,
      'top' : elem.offset().top,
      'left' : elem.offset().left + parseInt(elem.css('width')) * 0.5 - 8
    });

    $(document.body).append(animateIcon);

    animateIcon.fadeIn(500);

    animateIcon.delay(500).animate({
      'left' : $('.footer-menus ul li a span.num').offset().left,
      'top' : $('.footer-menus ul li a span.num').offset().top
    }, 800, function() {
      animateIcon.fadeOut(500);
      $('.footer-menus ul li a span.num').text(parseInt($('.footer-menus ul li a span.num').text()) + parseInt(num));

      //
        var openid = $("#openid").val();
        var userid = $("#userid").val();
        var pid = $("#hidpid").val();
        var color = $("#Procolor").val();
        var num2 = $("#num").val(); //����
        var bat = { "openid": openid, "userid": userid, "num": num2, "hidpid": pid, "Procolor": color };
        $.post("/fenxiao/prodctDetail.aspx?action=fullCar&da=" + new Date(), bat, function (data) {
            if (data == "OK") {
                //alert("加入购物车成功！");
            }
            else if (data == "overCount") {
                alert("购买数量超出商品上架数量！");
                return;
            }
            else {
                alert("系统正忙，请与管理员联系！");
            }
        });
    });

  }

});