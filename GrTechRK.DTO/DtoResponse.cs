using System.Collections.Generic;
using System.Linq;

namespace GrTechRK.DTO
{
    public class DtoResponse
    {
        public bool IsError { get; } = false;

        public List<string> ErrorMessages { get; } = new List<string>();

        protected DtoResponse() { }

        public DtoResponse(IEnumerable<string> errorMessages)
        {
            IsError = true;
            ErrorMessages = errorMessages.ToList();
        }
    }

    public class DtoResponse<TDto> : DtoResponse
    {
        public TDto Dto { get; private set; }

        private DtoResponse() { Dto = default!; }

        public DtoResponse(TDto dto) : base()
        {
            Dto = dto;
        }

        public DtoResponse(TDto dto, IEnumerable<string> errorMessages) : base(errorMessages)
        {
            Dto = dto;
        }
    }
}
