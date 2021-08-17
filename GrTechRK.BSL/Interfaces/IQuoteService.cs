using GrTechRK.DTO;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.BSL.Interfaces
{
    public interface IQuoteService
    {
        Task<ImmutableHashSet<QuoteDto>> GetRandomDailyQuotesAsync(CancellationToken cancellationToken);
    }
}
