﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SevenDigital.Api.FeedReader.Http
{
	public class WebClientWrapper : IWebClientWrapper
	{
		public async Task DownloadFile(string address, string fileName)
		{
			var httpClient = new HttpClient
			{
				Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite)
			};

			if (File.Exists(fileName))
			{
				File.Delete(fileName);
			}

			await DownloadFileAsync(httpClient, address, fileName);
		}

		public async Task ResumeDownloadFile(string address, string fileName)
		{
			var fileInfo = new FileInfo(fileName);
			var length = fileInfo.Length;
			var startRange = length;

			var httpClient = new HttpClient
			{
				Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite)
			};
			httpClient.DefaultRequestHeaders.Range = new RangeHeaderValue(startRange, null);

			await DownloadFileAsync(httpClient, address, fileName);
		}

		private static async Task DownloadFileAsync(HttpClient httpClient, string address, string fileName)
		{
			using (httpClient)
			{
				var httpResponseMessage = await httpClient.GetAsync(address, HttpCompletionOption.ResponseHeadersRead);
				using (var fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write))
				{
					using (var httpStream = await httpResponseMessage.Content.ReadAsStreamAsync())
					{
						await httpStream.CopyToAsync(fileStream);
						fileStream.Flush();
					}
				}
			}
		}
	}
}