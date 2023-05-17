using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ChatGPTByPhongLV.Repository
{
    public class ChatGPTClient
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey;
        private readonly string apiUrl;

        public ChatGPTClient(string apiKey, string apiUrl)
        {
            this.apiKey = apiKey;
            this.apiUrl = apiUrl;
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {this.apiKey}");
        }

        public async Task<string> GetChatResponse(string question)
        {
            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {

                new { role = "user", content = question }
                      }
            };

            var jsonContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                // Xử lý câu trả lời từ jsonResponse và trả về
                // Ví dụ:
                var answer = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonResponse).choices[0].message.content.ToString();
                return answer;
            }
            else
            {
                throw new Exception("Request failed with status code: " + response.StatusCode);
            }
        }
        public void TestTransection()
        {
            using (SqlConnection connection = new SqlConnection("hihi"))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Thực thi stored procedure trong giao dịch
                    SqlCommand command1 = new SqlCommand("sp_InsertCustomer", connection, transaction);
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.AddWithValue("@CustomerName", "Company Inc");
                    command1.Parameters.AddWithValue("@ContactName", "John Doe");
                    command1.Parameters.AddWithValue("@Country", "USA");
                    command1.ExecuteNonQuery();

                    SqlCommand command12 = new SqlCommand("sp_InsertCustomer", connection, transaction);
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.AddWithValue("@CustomerName", "Company Inc");
                    command1.Parameters.AddWithValue("@ContactName", "John Doe");
                    command1.Parameters.AddWithValue("@Country", "USA");
                    command1.ExecuteNonQuery();

                    // Hoàn thành giao dịch
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Xảy ra lỗi trong quá trình thực thi giao dịch, rollback giao dịch
                    Console.WriteLine("Error occurred, rolling back transaction: " + ex.Message);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine("Rollback failed: " + ex2.Message);
                    }
                }
            }

        }
    }
}
