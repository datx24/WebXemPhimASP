using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MovieWeb.Controllers
{
    public class VNPayLibrary
    {
        private SortedList<string, string> requestData = new SortedList<string, string>();
        private SortedList<string, string> responseData = new SortedList<string, string>(); // Dữ liệu phản hồi

        // Thêm dữ liệu vào requestData
        public void AddRequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                requestData[key] = value;
            }
        }

        // Thêm dữ liệu vào responseData
        public void AddResponseData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                responseData[key] = value;
            }
        }

        // Lấy dữ liệu phản hồi từ responseData
        public string GetResponseData(string key)
        {
            if (responseData.ContainsKey(key))
            {
                return responseData[key];
            }
            return null;
        }

        public string CreateRequestUrl(string baseUrl, string hashSecret)
        {
            string rawData = string.Join("&", requestData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            string secureHash = ComputeHash(hashSecret, rawData);
            return $"{baseUrl}?{rawData}&vnp_SecureHash={secureHash}";
        }

        public bool ValidateSignature(string secureHash, string hashSecret)
        {
            string rawData = string.Join("&", requestData.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            string computedHash = ComputeHash(hashSecret, rawData);
            return secureHash == computedHash;
        }

        private string ComputeHash(string secretKey, string data)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToUpper();
            }
        }
    }


}