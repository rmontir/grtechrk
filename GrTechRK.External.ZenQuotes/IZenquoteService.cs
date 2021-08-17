using GrTechRK.External.ZenQuotes.Models;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.External.ZenQuotes
{
    public interface IZenquoteService
    {
        Task<DailyQuoteResponse> GetDailyQuotesAsync(CancellationToken cancellationToken);
    }
}
