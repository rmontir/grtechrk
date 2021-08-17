using AutoMapper;
using GrTechRK.BSL.Interfaces;
using GrTechRK.DTO;
using GrTechRK.External.ZenQuotes;
using GrTechRK.External.ZenQuotes.Models;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.BSL
{
    public class QuoteService : IQuoteService
    {
        private readonly IZenquoteService _zenquoteService;
        private readonly IMapper _mapper;

        public QuoteService(
            IZenquoteService zenquoteService,
            IMapper mapper
        )
        {
            _zenquoteService = zenquoteService;
            _mapper = mapper;
        }

        public async Task<ImmutableHashSet<QuoteDto>> GetRandomDailyQuotesAsync(CancellationToken cancellationToken)
        {
            DailyQuoteResponse response = await _zenquoteService.GetDailyQuotesAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsError)
            {
                return response.Data.Select(d => _mapper.Map<QuoteDto>(d)).ToImmutableHashSet();
            }
            else
            {
                throw new InvalidOperationException(response.Message);
            }
        }
    }
}
