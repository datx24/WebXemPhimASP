using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebSale.Controllers
{
    public class ThanhToanController : Controller
    {
        private const string Endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
        private const string PartnerCode = "MOMOBKUN20180529";
        private const string AccessKey = "klm05TvNBzhg7h7j";
        private const string SecretKey = "at67qH6mk8w5Y1nAyMoYKMWACiEi2bsa";

        public async Task<ActionResult> ThanhToan()
        {
            try
            {
                // Step 1: Set up payment request data
                string orderInfo = "Thanh toán qua MoMo";
                string amount = "10000"; // Total amount to be paid
                string orderIdStr = "1";
                string redirectUrl = "https://localhost:44338/thanks"; // URL to redirect after payment
                string ipnUrl = "https://localhost:44338/thanks"; // URL for MoMo IPN notification

                string requestId = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
                string requestType = "payWithATM"; // Payment method type

                // Step 2: Generate the signature
                string rawHash = $"accessKey={AccessKey}&amount={amount}&extraData=&ipnUrl={ipnUrl}&orderId={orderIdStr}&orderInfo={orderInfo}&partnerCode={PartnerCode}&redirectUrl={redirectUrl}&requestId={requestId}&requestType={requestType}";
                string signature;
                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey)))
                {
                    signature = BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawHash))).Replace("-", "").ToLower();
                }

                // Step 3: Prepare the request data
                var paymentData = new
                {
                    partnerCode = PartnerCode,
                    partnerName = "Test",
                    storeId = "MomoTestStore",
                    requestId = requestId,
                    amount = amount,
                    orderId = orderIdStr,
                    orderInfo = orderInfo,
                    redirectUrl = redirectUrl,
                    ipnUrl = ipnUrl,
                    lang = "vi",
                    extraData = "",
                    requestType = requestType,
                    signature = signature
                };

                // Step 4: Send the POST request to MoMo
                using (var client = new HttpClient())
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(paymentData), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(Endpoint, jsonContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        return View("Error", new { errorMessage = "Lỗi trong quá trình xử lý thanh toán." });
                    }

                    var resultContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(resultContent);
                    string paymentUrl = result.payUrl.ToString();

                    // Step 5: Redirect the user to the payment URL
                    return Redirect(paymentUrl);
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return View("Error", new { errorMessage = $"Đã xảy ra lỗi: {ex.Message}" });
            }
        }
    }
}
