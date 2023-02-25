
var webApiAddress = "https://siegapi.azurewebsites.net/api/J";
var appVue = new Vue({
    el: "#appVue",
    data() {
        return {
            mID: sessionStorage.getItem("mID"),
            pID: 0,
            //pID: 102,
            productInfo: [],
            quotePriceList: [],
            bidPriceList: [],
            SizeAndPriceList: [],
            showSelSize: true,
            pSize: null,
            pPrice: null,
            activeIndex: 0,
            lastTimePrice: null,
            upDown: null,
            TranList: [],
            fontColor: '#006340',
            sID: null,
            pLength: null,
            pCateID: null,
            productNote: [],
            pInfo: [],
            tpCateID: null,
            historicalData: [],
            relatedProducts: [],
        }
    },
    mounted: async function () {
        _this = this;
        _this.pID = sessionStorage.getItem("pID");
        //sessionStorage.setItem("mID", _this.mID);

        //await _this.getProductInfo();
        await _this.getRelatedProducts();

        //await _this.getNewsListDTO();

        const datas = _this.relatedProducts.map(item => {
            const { pID, pName, pImg } = item;
            return (`
    <div class="item">
        <div class="product_col">
            <div class="productInfo_card">
                <div class="divLikeProduct">
                    <a href="product_info">
                        <img src="${item.pImg}" alt="" class="bannerImg">
                    </a>
                </div>
                <div class="productInfo_content">
                    <!-- <a href="product_info.html" class="two-row"> -->
                        <a href="product_info.html" class="">
                            <div class="two-row">${item.pName}</div>
                            @*      <div style="color:#999999;margin:0px">�̧C����</div>
                            <div class="minPrice fontSizeM">$1,780</div>
                            @*<div class="listTimeSold">�W������� : $1,780</div>*@
                        </a>
                </div>
            </div>
        </div>
    </div>
    `)
        })
        $(".banner-carousel").html(datas.join(""));
        $(".banner-carousel").owlCarousel({
            loop: true,
            autoplay: true,
            smartSpeed: 1000,
            items: 4,
            margin: 24,
            dots: false,
            nav: false,
            responsive: {
                0: {
                    items: 1
                },
                768: {
                    items: 2
                },
                992: {
                    items: 3
                },
                1200: {
                    items: 4
                }
            },
        });
        $(function () {
            $(".heart").on("click", function () {
                $(this).toggleClass("is-active");
            });
        });

    },
    methods: {
        async getProductInfo() {
            let _this = this;
            try {
                var response = await axios.get(`${webApiAddress}/GetProductInfo/${_this.pID}`);
                _this.productInfo = response.data[0];
                _this.pLength = response.data.length === 1;
                sessionStorage.setItem("pLength", _this.pLength);
                _this.pCateID = _this.productInfo.pCateID;
                //_this.getSizeAndQuote();
            } catch (error) {
                console.error(error);
            } finally {

            }
        },
        async getProductNote() {
            let _this = this;
            try {
                var response = await axios.get(`${webApiAddress}/GetProductNote/${_this.pCateID}`);
                _this.productNote = response.data;
                if (_this.productNote != null && _this.productNote[0].info != null) {
                    _this.pInfo = _this.productNote[0].info.split("|");

                } else if (_this.productNote != null && _this.productNote[0].info == null) {
                    _this.pInfo = ["", ""];
                } else {
                    _this.pInfo = ["", ""];
                    _this.productNote = ["", ""];
                }

            } catch (error) {
                console.error(error);
                _this.productNote = [];
            }
        },
        async getSizeAndQuote() {
            let _this = this;
            var response = await axios.get(`${webApiAddress}/GetSizeAndQuote/${_this.pCateID}`);
            _this.SizeAndPriceList = response.data;

            if (response.data.length >= 1) {
                console.log(`getSizeAndQuote_start_pPrice:${_this.pPrice}`)
                if (_this.pPrice == "" || _this.pPrice == null) { // �Ĥ@���i��������l���
                    _this.pPrice = _this.SizeAndPriceList[0].pPrice;
                    _this.selectSizeItem(_this.SizeAndPriceList[0].pSize, _this.SizeAndPriceList[0].pPrice, _this.activeIndex, _this.SizeAndPriceList[0].pID, _this.SizeAndPriceList[0].sID)
                }

            } else { // �ӫ~�L�����O��
                _this.pPrice = "";
                sessionStorage.removeItem("sID");
            }
        },
        async getOrderList() { //����O��
            try {
                let _this = this;
                let response = await axios.get(`${webApiAddress}/GetOrderList/${_this.pID}`);
                _this.TranList = response.data;

            } catch (error) {
                console.log(error);
            }
        },
        async getLastPrice() { //���o����� pID �̪�@�����������
            try {
                let _this = this;
                let response = await axios.get(`${webApiAddress}/GetLastDealPrice/${_this.pID}`);
                _this.lastTimePrice = response.data;
                console.log(`getLastPrice:${_this.lastTimePrice}`)
            } catch (error) {
                console.log(error);
            }
        },
        async getQuotePrice() {
            let _this = this;
            let response = await axios.get(`${webApiAddress}/QuotePriceList/${_this.pID}`);
            _this.quotePriceList = response.data;
        },
        async getBidPrice() {
            let _this = this;
            let response = await axios.get(`${webApiAddress}/BidPriceList/${_this.pID}`);
            _this.bidPriceList = response.data;
        },
        async getHistoricalData() {
            let _this = this;
            let response = await axios.get(`${webApiAddress}/GetHistoricalData/${_this.pCateID}`);
            _this.historicalData = response.data;
        },
        async getRelatedProducts() {
            let _this = this;
            let response = await axios.get(`${webApiAddress}/GetRelatedProducts/${_this.pID}`);
            _this.relatedProducts = response.data;
        },
        selectSizeItem(pSize, pPrice, index, pID, sID) {
            let _this = this;
            _this.pSize = pSize;
            _this.pPrice = pPrice;
            _this.pID = pID;
            _this.sID = sID;
            sessionStorage.setItem("pSize", pSize);
            sessionStorage.setItem("pPrice", pPrice);
            sessionStorage.setItem("selIndex", index);
            sessionStorage.setItem("pID", pID);
            sessionStorage.setItem("sID", sID);
            console.log(`selectSizeItem_pPrice:${_this.pPrice}`)
        },
        BuyNow() {
            let _this = this;
            if (_this.mID != null) {
                if (_this.DisplayselBuyPrice == "�L�����O��") {
                    Swal.fire({
                        icon: 'error',
                        title: '�L�k�ާ@!',
                        text: '�ثe�L��a���X����, �Шϥδ���X���\��',
                    })

                } else {
                    sessionStorage.setItem("bool", false);
                    sessionStorage.setItem("pID", _this.pID);
                    window.location = "/order";
                }
                sessionStorage.removeItem("bID");
            } else {
                Swal.fire({
                    icon: 'error',
                    title: '�L�k�ާ@!',
                    text: '�z�|���n�J�|��,�Х��n�J�|��',
                })
            }

        },
        submitPrice() {
            if (_this.mID != null) {
                sessionStorage.removeItem("bID");
                sessionStorage.setItem("bool", true);
                window.location = "/order";
                sessionStorage.removeItem("bID");
            } else {
                Swal.fire({
                    icon: 'error',
                    title: '�L�k�ާ@!',
                    text: '�z�|���n�J�|��,�Х��n�J�|��',
                })
            }

        },
        Quote() {
            if (_this.mID != null) {
                sessionStorage.removeItem("sID");
                sessionStorage.setItem("pPrice", 0);
                window.location = "/sell";
            } else {
                Swal.fire({
                    icon: 'error',
                    title: '�L�k�ާ@!',
                    text: '�z�|���n�J�|��,�Х��n�J�|��',
                })
            }

        },
        Format(val) {
            return numeral(val).format("$0,0");
        },
        CheckVal(value) {
            let parsedValue = parseInt(value, 10);
            return isNaN(parsedValue) ? value : this.Format(parsedValue);
        },
    },
    watch: {
        pID: {
            handler() {
                if (this.pID) {
                    //this.getRelatedProducts();
                    this.getProductInfo();
                    this.getOrderList();
                    this.getLastPrice();
                    sessionStorage.setItem("pID", this.pID)
                }
            }
        },
        pCateID: {
            handler: function () {
                if (this.pCateID) {
                    //this.getRelatedProducts();
                    this.getHistoricalData();
                    this.getSizeAndQuote();
                    this.getProductNote();
                    sessionStorage.setItem("pCateID", this.pCateID)
                }
            }
        },
        mID: {
            handler: function () {
                if (this.mID) {
                    sessionStorage.setItem("mID", this.mID)
                }
            }
        }
    },
    computed: {
        DispalyLastDealPrice() {
            return this.lastTimePrice == 0 ? "�L����O��" : this.Format(this.lastTimePrice);
        },
        GetDifference() {
            return this.lastTimePrice == 0 ? "" : this.pPrice - this.lastTimePrice;
        },
        DispalyDifference() {
            return this.lastTimePrice == 0 ? "" : this.Format(this.pPrice - this.lastTimePrice);
        },
        GetPercentage() {
            return this.lastTimePrice == 0 ? 0 : ((this.pPrice - this.lastTimePrice) / this.lastTimePrice * 100).toFixed(2);
        },
        GetUpDown() {
            return this.GetDifference > 0 ? "�� " : this.GetDifference < 0 ? "�� " : "  "
        },
        GetFontColor() {
            return this.GetDifference > 0 ? "#006340" : this.GetDifference < 0 ? "#df2b14" : "#333"
        },
        DisplayselBuyPrice() {
            return this.pPrice == "" ? "�L�����O��" : this.pPrice == null ? "�ʶR" : `�H ${this.Format(this.pPrice)} �ʶR`
        },
        DisplaySizeAndPriceList() {
            return this.SizeAndPriceList.map(item => {
                return {
                    sID: item.sID,
                    pID: item.pID,
                    price: item.pPrice,
                    size: item.pSize,
                    fPrice: item.pPrice != 0 ? this.Format(item.pPrice) : "�L"
                }
            })
        },
        DisplayQuotePriceList() {
            return this.quotePriceList.map(item => {
                return {
                    size: item.pSize,
                    price: item.pPrice,
                    count: item.pCount,
                    fPrice: this.Format(item.pPrice)
                }
            })
        },
        DisplayBidPrice() {
            return this.bidPriceList.map(item => {
                return {
                    size: item.pSize,
                    price: item.pPrice,
                    count: item.pCount,
                    fPrice: this.Format(item.pPrice)
                }
            })
        },
        DisplayTranList() {
            var _this = this;
            return _this.TranList.map(item => {
                return {
                    date: item.oTime.split('T')[0],
                    time: item.oTime.split('T')[1].split('.')[0],
                    size: item.oSize,
                    price: item.oPrice,
                    fPrice: _this.Format(item.oPrice),
                }
            })
        },
        DispalyHistoricalData() {
            return this.historicalData.map(item => {
                item.val1 = this.CheckVal(item.val1);
                item.val2 = this.CheckVal(item.val2);
                item.val3 = this.CheckVal(item.val3);
                return item;
            });
        },
    },
});
