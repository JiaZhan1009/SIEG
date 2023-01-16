/*標題、內文自動調適高度*/
document.addEventListener('DOMContentLoaded', function () {
    autosize(document.querySelectorAll('#story'));
}, false);

$('document').ready(function () {

    /*送出按鈕*/
    $("#submit").click(function () {
        var target = $(this);
        if (target.hasClass("done")) {
            // do nothing
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
        $('#ImgUpload').val('');
        $preview = $('.image-preview');
        $preview.attr('src', '');
        $preview.css('display', 'none');
        $control.css('display', 'none');
        $('.image-button').css('display', 'inline');
    });

    //留言收折
    //let old = document.getElementById('more').textContent;
    //let more = document.getElementById('more');
    //more.addEventListener('click', function handleClick() {
    //    $(this).next(".showcontent").slideToggle();
    //    if (more.textContent == '隱藏留言') {
    //        more.textContent = old;
    //    } else {
    //        more.textContent = '隱藏留言';
    //    }
    //});



    /*我要回復鈕*/
    let reply = document.getElementById('Reply_btn');
    reply.addEventListener('click', function () {
        $(this).next(".Reply_block").slideToggle();
    });

});

/*文章頁發文鈕*/
let open = '@Url.Action("Forum_post", "Forum")';
var animateButton = function (e) {

    e.preventDefault;
    e.target.classList.remove('animate');

    e.target.classList.add('animate');


    setTimeout(function () {
        e.target.classList.remove('animate');
        window.open('Forum_post');
    }, 500);
};

var bubblyButtons = document.getElementsByClassName("bubbly-button");

for (var i = 0; i < bubblyButtons.length; i++) {
    bubblyButtons[i].addEventListener('click', animateButton, false);
}

/*刪除修改選單*/
document.querySelector('.edit_button').addEventListener('click', function () {
    document.querySelector('.list-container').classList.toggle('active');
});



