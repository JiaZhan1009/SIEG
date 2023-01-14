/*標題、內文自動調適高度*/
document.addEventListener('DOMContentLoaded', function () {
    autosize(document.querySelectorAll('#story'));
}, false);

/*送出按鈕*/
$("button").click(function () {
    var target = $(this);
    if (target.hasClass("done")) {
        // Do nothing
    } else {
        target.addClass("processing");
        setTimeout(function () {
            target.removeClass("processing");
            target.addClass("done");
        }, 2200);
    }
});

/*發文頁照片*/
$('#imageInput').on('change', function () {
    $input = $(this);
    if ($input.val().length > 0) {
        fileReader = new FileReader();
        fileReader.onload = function (data) {
            $('.image-preview').attr('src', data.target.result);
        }
        fileReader.readAsDataURL($input.prop('files')[0]);
        $('.image-button').css('display', 'none');
        $('.change-image').css('display', 'inline');
        $('.image-preview').css('display', 'inline');
    }
});

$('.change-image').on('click', function () {
    $control = $(this);
    $('#imageInput').val('');
    $preview = $('.image-preview');
    $preview.attr('src', '');
    $preview.css('display', 'none');
    $control.css('display', 'none');
    $('.image-button').css('display', 'inline');
});

/*文章頁發文鈕*/
var animateButton = function (e) {

    e.preventDefault;
    e.target.classList.remove('animate');

    e.target.classList.add('animate');


    setTimeout(function () {
        e.target.classList.remove('animate');
        window.open('forum_post.html');
    }, 500);
};

var bubblyButtons = document.getElementsByClassName("bubbly-button");

for (var i = 0; i < bubblyButtons.length; i++) {
    bubblyButtons[i].addEventListener('click', animateButton, false);
}

/*按讚鈕*/
$(function () {
    $(".heart").on("click", function () {
        $(this).toggleClass("is-active");
    });
});

/*收藏*/
$('button').on('mousedown', function () {
    if (!$(this).hasClass('active')) {
        $(this).addClass('pressed')
    }
})

$('button').on('click', function () {
    $(this).removeClass('pressed')
    $(this).toggleClass('active')
})

/*留言收折*/
let old = document.getElementById('more').textContent;
let more = document.getElementById('more');
more.addEventListener('click', function handleClick() {
    $(this).next(".showcontent").slideToggle();
    if (more.textContent == '隱藏留言') {
        more.textContent = old;
    } else {
        more.textContent = '隱藏留言';
    }
});

/*我要回復鈕*/
let reply = document.getElementById('Reply_btn');
reply.addEventListener('click', function () {
    $(this).next(".Reply_block").slideToggle();
});

/*取消鈕*/
let cancel = document.getElementById('cancel');
cancel.addEventListener('click', function () {
    $('.Reply_block').css('display', 'none');
});