namespace GrTechRK.DTO
{
    public class CompanyDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public byte[] Logo { get; set; }

        public string LogoSrc { get; set; }

        public string Website { get; set; }

        public CompanyDto() { }

        public CompanyDto(
            int id,
            string name,
            string email,
            byte[] logo,
            string logoSrc,
            string website
        )
        {
            Id = id;
            Name = name;
            Email = email;
            Logo = logo;
            LogoSrc = logoSrc;
            Website = website;
        }
    }
}
