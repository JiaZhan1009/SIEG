
var webApiBaseAddress = "https://siegapi.azurewebsites.net";
var appVue = new Vue({
    el: "#headerVue",
    name: "headerVue",
    data: {
        Filter: "",
        NewsSortDTO: [],
        newssortId: null,
        newssortName: null,
        ProductSortDTO: [],
        productsortId: null,
        productsortImg: null,
        productsortBrand: null,
        productsortSort: null,
    },
    mounted: function () {
        let _this = this;
        _this.getNewsSortDTO();
        _this.getProductSortDTO();

    },
    methods: {
        getNewsSortDTO: function () {
            let _this = this;
            axios.get(`${webApiBaseAddress}/api/E_NewsSort`).then(
                response => {
                    _this.NewsSortDTO = response.data;
                    var NewsSort = [];
                    for (let i = 0; i < _this.NewsSortDTO.length; i++) {
                        let item = {};
                        item = _this.NewsSortDTO[i];
                        NewsSort.push(item);
                    }
                    _this.NewsSortDTO = NewsSort;
                }
            );
        },
        newsSortToList: function (sortName) {
            sessionStorage.setItem("indexNSortName", sortName);
            window.location = "/news_list";
        },
        getProductSortDTO: function () {
            let _this = this;
            axios.get(`${webApiBaseAddress}/api/E_ProductSort`).then(
                response => {
                    _this.ProductSortDTO = response.data;
                    //console.log(response.data);
                }
            );
        },
        ProductSortToList: function (sortName) {
            sessionStorage.setItem("indexPSortName", sortName);
            window.location = "/product_list";
        },
        ForumSortToList: function (sortName) {
            sessionStorage.setItem("indexPSortName", sortName);
            window.location = "/forum_list";
        },
        memberLogin: function () {
            mID = sessionStorage.getItem("mID");
            //console.log(mID);
            if (mID == "" || mID == null) {
                window.location = "/D_Personal";
            }
            else {
                window.location = "/Member/Kyccertified";
            }
        },
        searchProsuct: function () {
            sessionStorage.setItem("pKeyword", $(".searchInput").val());
            window.location = "/product_list";
        },
    },
});
let mID = sessionStorage.getItem("mID");
if (mID == "" || mID == null) {
    $('.member_login').hide();
    $('.member_login').addClass('div_hide');
}
else {
    $('.member_login').show();
    $('.member_login').addClass('div_show');
}


window.fbAsyncInit = function () {
    FB.init({
        appId: '872139614081838',
        cookie: true,
        xfbml: true,
        version: 'v16.0'
    });

    FB.AppEvents.logPageView();

    FB.getLoginStatus(function(response) {
        statusChangeCallback(response);
    });

    function statusChangeCallback(response) {
        console.log('statusChangeCallback');
        console.log(response);

    }
};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
