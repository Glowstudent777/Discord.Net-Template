using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houston.Bot.Common;

public static class ApiClient
{
	public static async Task<ApiResponse> GetAsync(string url, Dictionary<string, string> headers = null)
	{
		using (HttpClient httpClient = new HttpClient())
		{
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			request = AddHeaders(request, headers);

			HttpResponseMessage response = await httpClient.SendAsync(request);

			ApiResponse apiResponse = new ApiResponse
			{
				StatusCode = (int)response.StatusCode,
				Message = await response.Content.ReadAsStringAsync()
			};

			return apiResponse;
		}
	}

	public static async Task<ApiResponse> PostAsync(string url, string content, Dictionary<string, string> headers = null)
	{
		using (HttpClient httpClient = new HttpClient())
		{
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
			request.Content = new StringContent(content);
			request = AddHeaders(request, headers);

			HttpResponseMessage response = await httpClient.SendAsync(request);

			ApiResponse apiResponse = new ApiResponse
			{
				StatusCode = (int)response.StatusCode,
				Message = await response.Content.ReadAsStringAsync()
			};

			return apiResponse;
		}
	}

	private static HttpRequestMessage AddHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
	{
		if (headers is not null)
		{
			foreach (KeyValuePair<string, string> header in headers)
			{
				request.Headers.Add(header.Key, header.Value);
			}
		}
		return request;
	}

	public class ApiResponse
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
	}
}