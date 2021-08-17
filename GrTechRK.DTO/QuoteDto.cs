namespace GrTechRK.DTO
{
    public class QuoteDto
    {
        public string Quote { get; set; }

        public string Author { get; set; }

        public QuoteDto() { }

        public QuoteDto(
            string quote,
            string author
        )
        {
            Quote = quote;
            Author = author;
        }
    }
}
