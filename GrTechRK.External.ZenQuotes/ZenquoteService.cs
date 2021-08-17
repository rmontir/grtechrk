using GrTechRK.External.ZenQuotes.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Immutable;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.External.ZenQuotes
{
    public class ZenquoteService : IZenquoteService, IDisposable
    {
        private readonly IOptions<ZenquoteOptions> _optionsAccessor;
        private readonly HttpClient _httpClient;

        private string ApiKey => _optionsAccessor.Value.ApiKey ?? throw new InvalidOperationException("Api Key is not set");

        public ZenquoteService(
            IOptions<ZenquoteOptions> optionsAccessor
        )
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://zenquotes.io/api/")
            };
            _optionsAccessor = optionsAccessor;
        }

        public async Task<DailyQuoteResponse> GetDailyQuotesAsync(CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(
                        //requestUri: new Uri($"quotes/{ApiKey}", UriKind.Relative) // use this if you have an api key
                        requestUri: new Uri($"quotes", UriKind.Relative)
                    ).ConfigureAwait(false);

                httpResponseMessage.EnsureSuccessStatusCode();

                string json = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                ImmutableHashSet<DailyQuote> results = JsonSerializer.Deserialize<ImmutableHashSet<DailyQuote>>(json);

                return new DailyQuoteResponse(
                    isError: false,
                    message: $"{results.Count} daily quotes retrieved",
                    data: results
                );
            }
            catch (Exception exc)
            {
                return new DailyQuoteResponse(
                    isError: true,
                    message: exc.Message,
                    data: ImmutableHashSet<DailyQuote>.Empty
                );
            }
        }

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
