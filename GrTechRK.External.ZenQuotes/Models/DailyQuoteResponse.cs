using System.Collections.Immutable;

namespace GrTechRK.External.ZenQuotes.Models
{
    public class DailyQuoteResponse
    {
        public bool IsError { get; }

        public string Message { get; }

        public ImmutableHashSet<DailyQuote> Data { get; }

        public DailyQuoteResponse(
            bool isError,
            string message,
            ImmutableHashSet<DailyQuote> data
        )
        {
            IsError = isError;
            Message = message;
            Data = data;
        }
    }
}
