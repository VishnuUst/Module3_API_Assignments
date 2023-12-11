using Newtonsoft.Json.Linq;
using RestSharp;
using System.Data.Common;

string baseUrl = "https://jsonplaceholder.typicode.com/";
var client = new RestClient(baseUrl);

GetAllUser(client);
SingleUsers(client);
CreateUser(client);
UpdateUser(client);
DeleteUser(client);

static void GetAllUser(RestClient client)
{
    var getAllUser = new RestRequest("posts", Method.Get);
    var getAllUserResponse = client.Execute(getAllUser);
    Console.WriteLine(getAllUserResponse.Content);
}
static void SingleUsers(RestClient client)
{
    var getUserRequest = new RestRequest("posts/1", Method.Get);
                                                                
    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine("Get Response:");
    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        JObject? userJson = JObject.Parse(getUserResponse?.Content);
        string? userId = userJson["userId"].ToString();
        string? userTitle = userJson["title"].ToString();
        Console.WriteLine($" user Id: {userId}\nUser Title: {userTitle}");
    }
    else
    {
        Console.WriteLine($"Error:{getUserResponse.ErrorMessage}");
    }
}
static void CreateUser(RestClient client)
{
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new { userId = "123", title = "Lead" });
                                                                          
    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("POST Create User Response:");
    Console.WriteLine(createUserResponse.Content);

}
static void UpdateUser(RestClient client)
{
    var updateRequest = new RestRequest("posts/2", Method.Put);
                                                               
    updateRequest.AddHeader("Content-Type","application/json");
    updateRequest.AddJsonBody(new {userId="23updated",title ="updated lead engineer"});
    var updateResponse = client.Execute(updateRequest);
    Console.WriteLine("Updated Response");
    if (updateResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        JObject? userJson = JObject.Parse(updateResponse?.Content);
       
        string? userId = userJson["userId"].ToString();
        string? userTitle = userJson["title"].ToString();
        Console.WriteLine($"Page and user Name: {userId} {userTitle}");
    }
    else
    {
        Console.WriteLine($"Error:{updateResponse.ErrorMessage}");
    }
}
static void DeleteUser(RestClient client)
{
    var DeleteRequest = new RestRequest("posts/2", Method.Delete);
    var DeleteResponse = client.Execute(DeleteRequest);
    Console.WriteLine("DELETE User Request");
    if (DeleteResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        JObject? userJson = JObject.Parse(DeleteResponse?.Content);
       
       
        Console.WriteLine($"Successfully deleted:");
    }
    else
    {
        Console.WriteLine($"Error:{DeleteResponse.ErrorMessage}");
    }
}
