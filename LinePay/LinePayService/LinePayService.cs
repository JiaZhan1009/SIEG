using System.Net.Http.Headers;
using System.Text;
using System.Web;
using LinePay.Dtos;
using LinePay.Providers;

namespace LinePay.LinePayService
{
    public class LinePayService
    {
        public LinePayService()
        {
            // HttpClient 用於與 Line Pay 進行 Http 通訊
            client = new HttpClient();
            // JsonProvider 用於將物件序列化成 JSON 格式, 或是反序列化
            _jsonProvider = new JsonProvider();
        }

        private readonly string channelId = "1657834524";
        private readonly string channelSecretKey = "4ae558ec2f00f26cd35db45715cc1522";


        private readonly string linePayBaseApiUrl = "https://sandbox-api-pay.line.me";

        private static HttpClient client;
        private readonly JsonProvider _jsonProvider;

        // 送出建立交易請求至 Line Pay Server
        public async Task<PaymentResponseDto> SendPaymentRequest(PaymentRequestDto dto)
        {
            // 序列化成 JSON 物件
            var json = _jsonProvider.Serialize(dto);
            // 產生 GUID Nonce
            var nonce = Guid.NewGuid().ToString();
            // 要放入 signature 中的 requestUrl
            var requestUrl = "/v3/payments/request";

            // 使用 HMACSHA256 將 channelSecretKey + channelSecretKey & requestUrl & jsonBody & nonce 做編碼簽章
            var signature = SignatureProvider.HMACSHA256(channelSecretKey, channelSecretKey + requestUrl + json + nonce);

            // 創建一個 HttpRequestMessage 物件, 用於發送 HTTP 請求。
            var request = new HttpRequestMessage(HttpMethod.Post, linePayBaseApiUrl + requestUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            // 帶入 Line Pay 必要的 Headers 欄位設定
            client.DefaultRequestHeaders.Add("X-LINE-ChannelId", channelId);
            client.DefaultRequestHeaders.Add("X-LINE-Authorization-Nonce", nonce);
            client.DefaultRequestHeaders.Add("X-LINE-Authorization", signature);

            var response = await client.SendAsync(request);
            // 用 JsonProvider 的 Deserialize 方法將回應內容(ReadAsStringAsync)轉換成 PaymentResponseDto 物件回傳。
            var linePayResponse = _jsonProvider.Deserialize<PaymentResponseDto>(await response.Content.ReadAsStringAsync());

            Console.WriteLine(nonce);
            Console.WriteLine(signature);

            return linePayResponse;
        }

        // 取得 transactionId 後進行確認交易
        public async Task<PaymentConfirmResponseDto> ConfirmPayment(string transactionId, string orderId, PaymentConfirmDto dto) //加上 OrderId 去找資料
        {
            // 收到的 dto 轉換成 JSON 格式
            var json = _jsonProvider.Serialize(dto);

            var nonce = Guid.NewGuid().ToString();
            var requestUrl = string.Format("/v3/payments/{0}/confirm", transactionId);
            var signature = SignatureProvider.HMACSHA256(channelSecretKey, channelSecretKey + requestUrl + json + nonce);

            var request = new HttpRequestMessage(HttpMethod.Post, String.Format(linePayBaseApiUrl + requestUrl, transactionId))
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            client.DefaultRequestHeaders.Add("X-LINE-ChannelId", channelId);
            client.DefaultRequestHeaders.Add("X-LINE-Authorization-Nonce", nonce);
            client.DefaultRequestHeaders.Add("X-LINE-Authorization", signature);

            var response = await client.SendAsync(request);
            var responseDto = _jsonProvider.Deserialize<PaymentConfirmResponseDto>(await response.Content.ReadAsStringAsync());
            return responseDto;
        }

        public async void TransactionCancel(string transactionId)
        {
            //使用者取消交易則會到這裏。
            Console.WriteLine($"訂單 {transactionId} 已取消");
        }
    }
}

