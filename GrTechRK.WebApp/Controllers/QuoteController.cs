using GrTechRK.BSL.Interfaces;
using GrTechRK.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GrTechRK.WebApp.Controllers
{
    [AllowAnonymous]
    [Route("daily-quotes")]
    public class QuoteController : BaseController
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(
            IQuoteService quoteService
        )
        {
            _quoteService = quoteService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            ImmutableHashSet<QuoteDto> quoteDtos = await _quoteService.GetRandomDailyQuotesAsync(cancellationToken).ConfigureAwait(false);
            HttpContext.Session.SetString("_Quotes", JsonSerializer.Serialize(quoteDtos));

            return View();
        }

        [HttpGet("[action]")]
        public async Task RefreshAsync(CancellationToken cancellationToken)
        {
            ImmutableHashSet<QuoteDto> quoteDtos = await _quoteService.GetRandomDailyQuotesAsync(cancellationToken).ConfigureAwait(false);
            HttpContext.Session.SetString("_Quotes", JsonSerializer.Serialize(quoteDtos));
        }

        [HttpGet("daily-quotes")]
        public async Task<JsonResult> DailyQuotesAsync(
            [FromQuery(Name = "draw")] int? draw,
            [FromQuery(Name = "start")] int? start,
            [FromQuery(Name = "length")] int? length,
            [FromQuery(Name = "search[value]")] string search,
            [FromQuery(Name = "order[0][dir]")] string sortDir,
            CancellationToken cancellationToken
        )
        {
            try
            {
                int pageSize = length ?? 10;
                int skip = start ?? length ?? 10;
                int recordsTotal = 0;
                int recordsFiltered = 0;
                string sortColumn = Request.Query["columns[" + Request.Query["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                string jsonText = HttpContext.Session.GetString("_Quotes");
                IEnumerable<QuoteDto> quoteDtos;
                if (string.IsNullOrWhiteSpace(jsonText))
                {
                    quoteDtos = await _quoteService.GetRandomDailyQuotesAsync(cancellationToken).ConfigureAwait(false);
                    HttpContext.Session.SetString("_Quotes", JsonSerializer.Serialize(quoteDtos));
                } else
                {
                    quoteDtos = JsonSerializer.Deserialize<IEnumerable<QuoteDto>>(jsonText);
                }

                recordsTotal = recordsFiltered = quoteDtos.Count();
                if (!string.IsNullOrWhiteSpace(sortColumn) && !string.IsNullOrWhiteSpace(sortDir))
                {
                    string sort = $"{sortColumn} {sortDir}";
                    quoteDtos = sort switch
                    {
                        "quote asc" => quoteDtos.OrderBy(e => e.Quote).Skip(skip).Take(pageSize),
                        "quote desc" => quoteDtos.OrderByDescending(e => e.Quote).Skip(skip).Take(pageSize),
                        "author asc" => quoteDtos.OrderBy(e => e.Author).Skip(skip).Take(pageSize),
                        "author desc" => quoteDtos.OrderByDescending(e => e.Author).Skip(skip).Take(pageSize),
                        _ => quoteDtos.OrderBy(e => e.Quote).Skip(skip).Take(pageSize),
                    };
                }

                return Json(new { draw, recordsTotal, recordsFiltered, data = quoteDtos });
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
