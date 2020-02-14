// window.external.end_login;
clearTimeout(window.t || 0);

if (document.getElementById("iUserName")) {
  enter_room();
} else {
  if (!window.entered) {
    window.entered = true;
    login();
    waitFor(function() {
      return document.getElementById("iUserName");
    }, enter_room);
  }
}

function enter_room() {
  window.external.end_login;
  location.href = "{{AreaValue}}";
}

function getAreas() {
  var items = $(".area-tag")
    .map(function() {
      var $ele = $(this);
      return {
        name: $ele.attr("title"),
        value: $ele.attr("href")
      };
    })
    .get();
  return items;
}

function login() {
  var iframe = $(".aside iframe")[0];
  var doc = $(iframe.contentDocument);

  function handler() {
    var account = doc.find("input[name=account]");

    if (!account.length) {
      return setTimeout(handler, 30);
    }

    account.val("{{Name}}");
    doc.find("input[name=password]").val("{{Password}}");
    doc.find("input[type=submit]").click(); // waitFor(
    //   () => $(".quc-field-captcha:visible").length > 0,
    //   () => {
    //     // var img = $('.quc-field-captcha img')[0]
    //     // img.setAttribute("crossOrigin",'Anonymous')
    //     // // var img = new Image()
    //     // var cv = document.createElement('canvas')
    //     // var ctx = cv.getContext('2d')
    //     // cv.width = img.naturalWidth
    //     // cv.height = img.naturalHeight
    //     // ctx.drawImage(img, 0, 0)
    //     // console.log(cv.toDataURL())
    //     // window.external.procMessage("captcha");
    //   }
    // );
  }

  handler();
} // document.querySelector(".quc-field-captcha");

function waitFor(test, cb) {
  if (!test()) {
    window.t = setTimeout(function() {
      waitFor(test, cb);
    }, 60);
    return;
  }

  cb();
}
