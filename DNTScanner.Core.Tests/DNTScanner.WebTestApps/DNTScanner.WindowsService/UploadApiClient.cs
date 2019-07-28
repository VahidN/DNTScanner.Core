using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DNTScanner.WindowsService
{
    public class UploadApiClient
    {
        private readonly HttpClient _httpClient;

        public UploadApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task PostImagesAsync(string path, List<byte[]> files, string paramName, string fileExtension)
        {
            var content = new MultipartFormDataContent();
            files.ForEach(file => content.Add(new ByteArrayContent(file), paramName, $"{DateTime.Now.Ticks}{fileExtension}"));

            var result = await _httpClient.PostAsync(path, content);
            result.EnsureSuccessStatusCode();
        }
    }
}