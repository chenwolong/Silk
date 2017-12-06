
function ScrollToTopDiv(scrollToName, onClick) {
  var toTopBtn = $('<a href="#' + scrollToName +'">^</a>');
  
  var scrollTo = $('a[name="' + scrollToName + '"]');
  
  var onClick = onClick || function () {};
  
  toTopBtn.css({
    'text-decoration': 'none',
    'color': '#eee',
    'background-color': 'rgba(0, 0, 0, 0.5)',
    'display': 'block',
    'position': 'fixed',
    'z-index': '999',
    'right': '5%',
    'bottom': '12%',
    'text-align': 'center',
    'border-radius': '3px',
    'width': '30px',
    'height': '30px',
    'font-size': '40px',
    'line-height': '40px'
  });
  
  var speed = 0;
  var delay = 0.5;
  var interval = 10;
  
  toTopBtn.click(function () {
    onClick();
    
    speed = (scrollTo.offset().top - $(window).scrollTop()) / (delay * 1000 / interval);
    
    if (0 == speed) {
      return false;
    }
    
    var handler = setInterval(nextScroll, interval);
    
    function nextScroll() {
      if ((speed > 0 && scrollTo.offset().top <= $(window).scrollTop())
          || (speed < 0 && scrollTo.offset().top >= $(window).scrollTop())) {
        $(window).scrollTop(scrollTo.offset().top);
        clearInterval(handler);
        return;
      }
      $(window).scrollTop($(window).scrollTop() + speed);
    }
    return false;
  });
  
  return toTopBtn;
}

//多选全选
function bindCheckBox(parentCheckbox, childrenCheckBox)
{
    parentCheckbox.off('change');
    parentCheckbox.on('change', function () {
        var isChecked = $(this).is(':checked');
        childrenCheckBox.attr('checked', isChecked);
        childrenCheckBox.each(function () {
            $(this).get(0).checked = isChecked;
        });
    });

    childrenCheckBox.off('change');
    childrenCheckBox.on('change', function () {
        if ($(this).is(':checked')) {
            // checked
            var allChecked = true;
            childrenCheckBox.each(function () {
                if (!$(this).is(':checked')) {
                    allChecked = false;
                    return false;
                }
            });

            if (allChecked) {
                parentCheckbox.attr('checked', true);
                parentCheckbox.get(0).checked = true;
            }
        } else {
            parentCheckbox.attr('checked', false);
            parentCheckbox.get(0).checked = false;
        }
    });
}

var cTools = {
    Alert: function (msg, delay, style) {
      delay = delay || 1000;
      var alertBox = $('<div>' + msg + '</div>');
      alertBox.css({
        position: 'fixed',
        top: '0px',
        left: '0px',
        width: '100%',
        'background-color': 'red',
        color: 'white',
        'text-align': 'center',
        padding: '5px',
        visibility: 'hidden',
        'box-shadow': '0px 1px 1px 1px #666',
        'z-index': 99999,
      });
      
      if (style) {
        for (var i in style) {
          alertBox.css(i, style[i]);
        }
      }
      
      $(document.body).append(alertBox);
      
      var boxHeight = alertBox.height() + parseInt(alertBox.css('padding-top')) + parseInt(alertBox.css('padding-bottom'));
      alertBox.css({
        top: -boxHeight,
        visibility: 'visible',
      });

      alertBox.animate({
        top: 0
      }, function () {
        setTimeout(function () {
          alertBox.animate({
            top: -boxHeight
          }, function () {
            alertBox.remove();
          });
        }, delay);
      });
      
    },
};

(function () {
  
  //TipsDialog
  /*
   * param
   *      closeChecker Function   - return if this dialog should close
   *      onClose Function        - will call before close this dialog
   *      
   * */
  function TipsDialog(param) {
      var _this = this;
      
      this._shouldCloseChecker = function () {return false;};
      this._onCloseCallback = function () {};
      this._countDownHandler = null;

      this.life = 0;
      this.dlgBound = null;
      this.dlg = null;
      this.mask = null;
      this.tips = null;
      this.closeBtn = null;
      this.showCloseBtn = param.showCloseBtn || false;
      
      if (param && param.closeChecker && 'function' === typeof(param.closeChecker)) {
          this._shouldCloseChecker = param.closeChecker;
      }
      if (param && param.onClose && 'function' === typeof(param.onClose)) {
          this._onCloseCallback = param.onClose;
      }

      this.countDown = function () {
          if (this._shouldCloseChecker() || this.life <= 0) {
              if (this.dlgBound) {
                  this.close.call(this);
              }
          } else if (this.life > 0) {
              this.closeBtn.html('关闭(' + this.life + ')');
              this.life--;
              this._countDownHandler = setTimeout(function () {_this.countDown.call(_this);}, 1000);
          }
      };
  
      return this;
  }
  
  TipsDialog.prototype.close = function () {
      var closeParam = {canClose: true};
      this._onCloseCallback(closeParam);
      if (!closeParam.canClose) return;

      var _this = this;
      
      if (this._countDownHandler) {
          clearTimeout(this._countDownHandler);
          this._countDownHandler = null;
      }
      this.dlg.remove();
      this.mask.fadeTo(200, 0, function () {
          _this.dlgBound.remove();
          _this.dlgBound = null;
      });
  };
  
  /*
   * tip String   - tip text
   * 
   * tip Object
   *      info String     -tip text
   * 
   * life Number  - dialog show seconds
   * 
   * */
  TipsDialog.prototype.show = function (tip, life) {
      var _this = this;
      var tipInfo = '';
      var dlgParam = {width: 200, height: 180};
      
      if ('object' === typeof(tip)) {
          tipInfo = tip.info;
      } else if ('string' === typeof(tip)) {
          tipInfo = tip;
      }
      
      this.dlgBound = $('<div style="width: 100%; height: 100%; position: fixed; left: 0%; top: 0%"></div>');
      this.mask = $('<div style="width: 100%; height: 100%; background-color: #000; position: absolute;"></div>');
      this.dlg = $('<div style="width: ' + dlgParam.width + 'px; height: ' + dlgParam.height + 'px; background-color: #DDD; position: absolute; left: 50%; top: 50%; margin-left: -100px; margin-top: -' + parseInt(dlgParam.height/2) + 'px;"></div>');
      this.tips = $('<div style="width: 80px; height: 50px; margin: auto; margin-top: 25px; text-align: center;">' + tipInfo + '</div>');
      this.closeBtn = $('<div style="width: 60px; margin: auto; margin-top: 25px; cursor: pointer; background-color: #555; text-align: center;">关闭</div>');
      
      if (!this.showCloseBtn) {
        this.closeBtn.css('display', 'none');
      }
      
      this.closeBtn.on('click', function () {_this.close.call(_this);});
      
      this.mask.fadeTo(0, 0);

      this.dlgBound.append(this.mask);
      this.dlgBound.append(this.dlg);
      this.dlg.append(this.tips);
      this.dlg.append(this.closeBtn);
      
      $(document.body).append(this.dlgBound);
      this.mask.fadeTo(0, 0);
      
      this.life = life || 2;
      this.countDown();
      
      return this;
  };

  if (!window.TipsDialog) {
      window.TipsDialog = TipsDialog;
  }

})();
