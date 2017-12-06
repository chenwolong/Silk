
$(function () {
  var userid=$("#userid").val(); 
  var mmDOM = '\
  <section class="main-menu">\
    <a href=""><img src="images/menu.png" /></a>\
    <div>\
      <div>\
        <div><a href="/fenxiao/index_dibei.aspx"><img src="images/foo.png" /></a></div>\
        <div><a href="tel://18911695087"><img src="images/foo.png" />联系店主</a></div>\
        <div><a href="/fenxiao/userHome/SCproduct.aspx?userid='+userid+'"><img src="images/foo.png" />收藏</a></div>\
        <div><a href="/fenxiao/products.aspx?userid='+userid+'"><img src="images/foo.png" />搜索</a></div>\
        <div><a href="/fenxiao/userHome/erweima.aspx?userid='+userid+'"><img src="images/foo.png" />分享</a></div>\
        <div><a href="/fenxiao/userHome/userMain.aspx?userid='+ userid + '"><img src="images/foo.png" />会员</a></div>\
        <div><a href="/fenxiao/car.aspx?action=weiLoad?userid='+ userid + '"><img src="images/foo.png" />购物车</a></div>\
        <img src="images/foo.png" />\
      </div>\
    </div>\
  </section>';
  
  var rmDOM = '\
  <section class="right-menu">\
    <a href="category.html"><img src="images/cs.png" /></a>\
  </section>';
  
  $(document.body).append(mmDOM);
//  $(document.body).append(rmDOM);
  
  var mainMenuBtn = $('section.main-menu > a');
  var mainMenuBg = $('section.main-menu > div');
  var mainMenu = $('section.main-menu > div > div');
  
  $('section.main-menu > div').css({
    width: $(window).width(),
    height: $(window).height()
  });
  
  var storeHandler = {
    read: function (key) { return null; },
    save: function (key, val) {}
  };
  
  if (window.localStorage) {
    storeHandler = {
      read: function (key) {
        return localStorage.getItem(key);	
      },
      save: function (key, val) {
        localStorage.setItem(key, val);	
      },
    }
  }
  
  var ww = $(window).width();
  var wh = $(window).height();

  var storePos = {x: storeHandler.read('menuPosX'), y: storeHandler.read('menuPosY')};
  if (storePos.x && storePos.y) {

    storePos.x = Math.min(ww * 0.9, storePos.x);
    storePos.y = Math.min(wh * 0.9, storePos.y);

    storePos.x = Math.max(0, storePos.x);
    storePos.y = Math.max(0, storePos.y);
    
    mainMenuBtn.css({
      left: parseInt(storePos.x),
      top: parseInt(storePos.y)
    });
  } else {
    mainMenuBtn.css({
      left: ww * 0.2,
      top: wh * 0.8
    });
  }
  
  mainMenu.css({
    width: $(window).width() * 0.5
  });
  
  var precision = 0.0001;
  var enableInertia = true;
  var inertiaAnimateInterval = 50;
  var startPos = null;
  var lastPos = null;
  var menuBtnPos = null;
  var moveThreshold = 10;
  var isTouchMove = false;

  $('section.main-menu > a').click(function () {
    return false;
  });
  
  mainMenuBg.hide();
  
  function hideMenu() {
    mainMenuBg.off('click', hideMenu);
    mainMenuBg.hide();
  }
  
  function calAndShowMenu() {
    mainMenuBg.on('click', hideMenu);
    mainMenuBg.show();
    
    var menuSize = {
      w: mainMenu.width(),
      h: mainMenu.height()
    }
    
    var menuBtnSize = {
      w: mainMenuBtn.width(),
      h: mainMenuBtn.height()
    }
    
    var menuBtnCenter = {
      x: mainMenuBtn.position().left + menuBtnSize.w * 0.5,
      y: mainMenuBtn.position().top + menuBtnSize.h * 0.5
    };
    
    var menuCenter = {
      x: mainMenu.position().left + menuSize.w * 0.5,
      y: mainMenu.position().top + menuSize.h * 0.5
    };
    
    var menuCenterToMenuBtnCenter = (menuBtnSize.w + menuSize.w) * 0.5 + 10;
    /*
    var rr = menuCenterToMenuBtnCenter * menuCenterToMenuBtnCenter;
    
    var minX = Math.max(menuBtnCenter.x - menuCenterToMenuBtnCenter, menuSize.w * 0.5);
    var maxX = Math.min(menuBtnCenter.x + menuCenterToMenuBtnCenter, $(window).width() - menuSize.w * 0.5);
    var minXminX = (minX - menuBtnCenter.x) * (minX - menuBtnCenter.x);
    var maxXmaxX = (maxX - menuBtnCenter.x) * (maxX - menuBtnCenter.x);
    
    var minY = Math.max(menuBtnCenter.y - menuCenterToMenuBtnCenter, menuSize.h * 0.5);
    var maxY = Math.min(menuBtnCenter.y + menuCenterToMenuBtnCenter, $(window).height() - menuSize.w * 0.5);
    var minYminY = (menuBtnCenter.y - minY) * (menuBtnCenter.y - minY);
    var maxYmaxY = (menuBtnCenter.y - maxY) * (menuBtnCenter.y - maxY);
    
    var validAngleRange = [];
    
    
    // quadrant 0-π/2
    var angleRange = [];
    
    if (maxX >= menuBtnCenter.x) {
      
      var y = Math.sqrt(rr - maxXmaxX);
      if ((menuBtnCenter.y - y) > minY && (menuBtnCenter.y - y) < maxY) {
        if (Math.abs(y) < precision) {
          angleRange.push(0);
        } else {
          angleRange.push(Math.atan(y / (maxX - menuBtnCenter.x)));
        }
      }
      
      if (minY <= menuBtnCenter.y) {
        var x = Math.sqrt(rr - minYminY);
        if ((menuBtnCenter.x + x) > minX && (menuBtnCenter.x + x) < maxX) {
          if (Math.abs(x) < precision) {
            angleRange.push(Math.PI * 0.5);
          } else {
            angleRange.push(Math.atan((menuBtnCenter.y - minY) / x));
          }
        }
      }
      
      if (angleRange.length > 1) validAngleRange.push(angleRange);
    }
    
    // quadrant π/2-π
    angleRange = [];
    
    if (minX <= menuBtnCenter.x) {
      
      if (minY <= menuBtnCenter.y) {
        var x = Math.sqrt(rr - minYminY);
        if ((menuBtnCenter.x + x) > minX && (menuBtnCenter.x + x) < maxX) {
          if (Math.abs(x) < precision) {
            angleRange.push(Math.PI * 0.5);
          } else {
            angleRange.push(Math.atan((menuBtnCenter.y - minY) / x));
          }
        }
      }
      
      var y = Math.sqrt(rr - minXminX);
      if ((menuBtnCenter.y - y) > minY && (menuBtnCenter.y - y) < maxY) {
        if (Math.abs(y) < precision) {
          angleRange.push(Math.PI);
        } else {
          angleRange.push(Math.atan(y / (minX - menuBtnCenter.x)));
        }
      }
      
      if (angleRange.length > 1) validAngleRange.push(angleRange);
    }
    
    // quadrant π-π*3/2
    angleRange = [];
    
    if (minX <= menuBtnCenter.x) {
      
      var y = Math.sqrt(rr - minXminX);
      if ((menuBtnCenter.y - y) > minY && (menuBtnCenter.y - y) < maxY) {
        if (Math.abs(y) < precision) {
          angleRange.push(Math.PI);
        } else {
          angleRange.push(Math.atan(-y / (minX - menuBtnCenter.x)));
        }
      }
      
      if (maxY >= menuBtnCenter.y) {
        var x = Math.sqrt(rr - maxYmaxY);
        if ((menuBtnCenter.x + x) > minX && (menuBtnCenter.x + x) < maxX) {
          if (Math.abs(x) < precision) {
            angleRange.push(Math.PI * 1.5);
          } else {
            angleRange.push(Math.atan((menuBtnCenter.y - maxY) / x));
          }
        }
      }
      
      if (angleRange.length > 1) validAngleRange.push(angleRange);
    }
        
    // quadrant π*3/2-π*2
    angleRange = [];
    
    if (maxX >= menuBtnCenter.x) {
      
      if (maxY >= menuBtnCenter.y) {
        var x = Math.sqrt(rr - maxYmaxY);
        if ((menuBtnCenter.x + x) > minX && (menuBtnCenter.x + x) < maxX) {
          if (Math.abs(x) < precision) {
            angleRange.push(Math.PI * 1.5);
          } else {
            angleRange.push(Math.atan((menuBtnCenter.y - maxY) / x));
          }
        }
      }
      
      var y = Math.sqrt(rr - maxXmaxX);
      if ((menuBtnCenter.y - y) > minY && (menuBtnCenter.y - y) < maxY) {
        if (Math.abs(y) < precision) {
          angleRange.push(Math.PI * 2);
        } else {
          angleRange.push(Math.atan(-y / (maxX - menuBtnCenter.x)));
        }
      }
      
      if (angleRange.length > 1) validAngleRange.push(angleRange);
    }
    
    var currentAngle = Math.atan((menuCenter.x - menuBtnCenter.x) / (menuBtnCenter.y - menuCenter.y));
    
    var nearestAngle = Math.PI + Math.PI;
    var valide = true;
    
    for (var i = 0; i < validAngleRange.length; i++) {
      if (validAngleRange[i][0] < 0) {validAngleRange[i][0] += Math.PI + Math.PI;}
      if (validAngleRange[i][1] < 0) {validAngleRange[i][1] += Math.PI + Math.PI;}
      
      if (validAngleRange[i][0] < currentAngle
        && validAngleRange[i][1] > currentAngle) {
          console.log(validAngleRange[i]);
      } else {
        valide = false;
        if (Math.abs(validAngleRange[i][0] - currentAngle) < nearestAngle) {
          nearestAngle = validAngleRange[i][0];
        }
        if (Math.abs(validAngleRange[i][1] - currentAngle) < nearestAngle) {
          nearestAngle = validAngleRange[i][1];
        }
      }
    }
    
    if (valide) {
      nearestAngle = currentAngle;
    }
    
    mainMenu.css({
      left: menuBtnCenter.x - menuSize.w * 0.5 + Math.cos(nearestAngle) * menuCenterToMenuBtnCenter,
      top: menuBtnCenter.y - menuSize.h * 0.5 - Math.sin(nearestAngle) * menuCenterToMenuBtnCenter
    });
    */
    
    
    // angular bisector of the two minimum angle of menu button center to four corner is similar direction of menu
    
    var angelToRightTop = Math.atan(menuBtnCenter.y / ($(window).width() - menuBtnCenter.x));
    var angelToLeftTop = Math.PI - Math.atan(menuBtnCenter.y / (menuBtnCenter.x));
    var angelToLeftBottom = Math.PI + Math.atan(($(window).height() - menuBtnCenter.y) / (menuBtnCenter.x));
    var angelToRightBottom = Math.PI + Math.PI - Math.atan(($(window).height() - menuBtnCenter.y) / ($(window).width() - menuBtnCenter.x));
    
    if (angelToRightTop < 0) angelToRightTop += Math.PI + Math.PI;
    if (angelToLeftTop < 0) angelToLeftTop += Math.PI + Math.PI;
    if (angelToLeftBottom < 0) angelToLeftBottom += Math.PI + Math.PI;
    if (angelToRightBottom < 0) angelToRightBottom += Math.PI + Math.PI;
    
    var angelTop = angelToLeftTop - angelToRightTop;
    var angelLeft = angelToLeftBottom - angelToLeftTop;
    var angelBottom = angelToRightBottom - angelToLeftBottom;
    var angelRight = (Math.PI + Math.PI - angelToRightBottom) + angelToRightTop;
    
    var maxAngel = 'top';
    var maxAngelVal = angelTop;
    
    if (angelLeft > maxAngelVal) {
      maxAngel = 'left';
      maxAngelVal = angelLeft;
    }
    if (angelBottom > maxAngelVal) {
      maxAngel = 'bottom';
      maxAngelVal = angelBottom;
    }
    if (angelRight > maxAngelVal) {
      maxAngel = 'right';
      maxAngelVal = angelRight;
    }
    
    var angularBisector = 0;
    
    switch (maxAngel) {
    case 'top':
      if (angelLeft > angelRight) {
        angularBisector = (angelBottom + angelRight) * 0.5;
        angularBisector += angelToLeftBottom;
      } else {
        angularBisector = (angelLeft + angelBottom) * 0.5;
        angularBisector += angelToLeftTop;
      }
      break;
    case 'left':
      if (angelTop > angelBottom) {
        angularBisector = (angelBottom + angelRight) * 0.5;
        angularBisector += angelToLeftBottom;
      } else {
        angularBisector = (angelTop + angelRight) * 0.5;
        angularBisector += angelToRightBottom;
      }
      break;
    case 'bottom':
      if (angelLeft > angelRight) {
        angularBisector = (angelTop + angelRight) * 0.5;
        angularBisector += angelToRightBottom;
      } else {
        angularBisector = (angelLeft + angelTop) * 0.5;
        angularBisector += angelToRightTop;
      }
      break;
    case 'right':
      if (angelTop > angelBottom) {
        angularBisector = (angelLeft + angelBottom) * 0.5;
        angularBisector += angelToLeftTop;
      } else {
        angularBisector = (angelLeft + angelTop) * 0.5;
        angularBisector += angelToRightTop;
      }
      break;
    }
    
    mainMenu.css({
      left: menuBtnCenter.x - menuSize.w * 0.5 + Math.cos(angularBisector) * menuCenterToMenuBtnCenter,
      top: menuBtnCenter.y - menuSize.h * 0.5 - Math.sin(angularBisector) * menuCenterToMenuBtnCenter
    });
    
  }
  
  var inertiaAnimateHandle = null;
  
  function onTouchStart(e) {
    var touchE = (e.originalEvent.touches ? e.originalEvent.touches[0] : e.originalEvent);
    startPos = {x: touchE.clientX, y: touchE.clientY};
    lastPos = {x: touchE.clientX, y: touchE.clientY};
    menuBtnPos = {x: mainMenuBtn.position().left, y: mainMenuBtn.position().top};
    isTouchMove = false;
    
    var menuBtnSize = {
      w: mainMenuBtn.width(),
      h: mainMenuBtn.height()
    };
    
    if (inertiaAnimateHandle) {
      clearTimeout(inertiaAnimateHandle);
    }
    
    // var velocity = {x: (touchE.clientX - lastPos.x) / (1000 / inertiaAnimateInterval), y: (touchE.clientY - lastPos.y) / (1000 / inertiaAnimateInterval)};
    var velocity = {x: 0, y: 0};
    
    function onTouchMove(e) {
      var touchE = (e.originalEvent.touches ? e.originalEvent.touches[0] : e.originalEvent);
      velocity = {x: (touchE.clientX - lastPos.x) / (50 / inertiaAnimateInterval), y: (touchE.clientY - lastPos.y) / (50 / inertiaAnimateInterval)};
      lastPos = {x: touchE.clientX, y: touchE.clientY};
      if (Math.abs(touchE.clientX - startPos.x) > moveThreshold
        || Math.abs(touchE.clientY - startPos.y) > moveThreshold) {
        isTouchMove = true;
      }
      if (isTouchMove) {
        var nextPos = {
          x: menuBtnPos.x + (touchE.clientX - startPos.x),
          y: menuBtnPos.y + (touchE.clientY - startPos.y)
        };
        
        nextPos.x = Math.min(nextPos.x, $(window).width() - menuBtnSize.w);
        nextPos.x = Math.max(nextPos.x, 0);
        
        nextPos.y = Math.min(nextPos.y, $(window).height() - menuBtnSize.h);
        nextPos.y = Math.max(nextPos.y, 0);
        
        mainMenuBtn.css({
          left: nextPos.x,
          top: nextPos.y
        });
        
        return false;
      }
    }

    function onTouchEnd(e) {
      $(document.body).off('touchmove', onTouchMove);
      $(document.body).off('touchend', onTouchEnd);
      $(document.body).off('mousemove', onTouchMove);
      $(document.body).off('mouseup', onTouchEnd);
      
      storeHandler.save('menuPosX', mainMenuBtn.position().left);
      storeHandler.save('menuPosY', mainMenuBtn.position().top);
      
      if (!isTouchMove) {
        calAndShowMenu();
      } else if (enableInertia) {
        var touchE = (e.originalEvent.changedTouches ? e.originalEvent.changedTouches[0] : e.originalEvent);
        var vDecayRate = 0.9;
        
        function inertiaMove() {
          menuBtnPos = {x: mainMenuBtn.position().left, y: mainMenuBtn.position().top};
          
          velocity.x *= vDecayRate;
          velocity.y *= vDecayRate;
          
          if ((velocity.x - (-precision)) * (velocity.x - precision) < 0) {
            velocity.x = 0;
          }
          
          if ((velocity.y - (-precision)) * (velocity.y - precision) < 0) {
            velocity.y = 0;
          }
          
          if (0 == velocity.x && 0 == velocity.y) {
            storeHandler.save('menuPosX', mainMenuBtn.position().left);
            storeHandler.save('menuPosY', mainMenuBtn.position().top);
            return;
          }
          
          var nextPos = {x: menuBtnPos.x + velocity.x, y: menuBtnPos.y + velocity.y};
          
          if (nextPos.x > $(window).width() - menuBtnSize.w
            || nextPos.x < 0) {
            velocity.x = -velocity.x;
          }
          
          if (nextPos.y > $(window).height() - menuBtnSize.h
            || nextPos.y < 0) {
            velocity.y = -velocity.y;
          }
          
          mainMenuBtn.css({
            left: menuBtnPos.x + velocity.x,
            top: menuBtnPos.y + velocity.y
          });
          
          inertiaAnimateHandle = setTimeout(inertiaMove, inertiaAnimateInterval);
        }
        
        inertiaMove();
      }
    }
    
    $(document.body).on('touchmove', onTouchMove);
    $(document.body).on('touchend', onTouchEnd);
    $(document.body).on('mousemove', onTouchMove);
    $(document.body).on('mouseup', onTouchEnd);
    
    return false;
  }
  
  mainMenuBtn.on('touchstart', onTouchStart);
  mainMenuBtn.on('mousedown', onTouchStart);
  
});