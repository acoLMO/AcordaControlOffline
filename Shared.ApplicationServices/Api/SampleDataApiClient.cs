﻿using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using Mandate = Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api
{
    public class SampleDataApiClient : IApiClient
    {
        private const int DefaultDelayInMs = 3000;
        private readonly HttpClient httpClient_;
        public SampleDataApiClient(HttpClient httpClient)
        {
            httpClient_ = httpClient;
        }

        public async Task<Result<ViewModel.MandateList.Mandate[]>> FetchAllMandatesAsync(string uri)
        {
            return await FetchJsonDataAsync<ViewModel.MandateList.Mandate[]>(uri);
        }

        public async Task<Result<Mandate>> FetchMandateDetailAsync(string uri)
        {
            return await FetchJsonDataAsync<Mandate>(uri);
        }

        public async Task<Result<ViewModel.FarmDetail.Farm>> FetchFarmDetailAsync(string uri)
        {
            return await FetchJsonDataAsync<ViewModel.FarmDetail.Farm>(uri);
        }

        public async Task<Result<ChecklistSample>> FetchChecklistSampleAsync(string uri)
        {
            return await FetchJsonDataAsync<ChecklistSample>(uri);
        }

        private async Task<Result<T>> FetchJsonDataAsync<T>(string uri, int delayInMs = DefaultDelayInMs)
        {
            if (delayInMs > 0)
            {
                await Task.Delay(delayInMs);
            }
            
            using var httpResponse = await httpClient_.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.Content == null || httpResponse.Content.Headers.ContentType.MediaType != "application/json")
                return Result.Failure<T>("HTTP Response has no content or content is not json.");

            var contentStream = await httpResponse.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);
            var serializer = new JsonSerializer();
            try
            {
                var data = serializer.Deserialize<T>(jsonReader);
                return Result.Success(data);
            }
            catch (JsonReaderException)
            {
                return Result.Failure<T>($"Error while deserializing json: {nameof(JsonReaderException)} exception encountered.");
            }
        }
    }
}
