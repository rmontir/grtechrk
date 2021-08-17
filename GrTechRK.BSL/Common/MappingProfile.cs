using AutoMapper;
using GrTechRK.Database.Models;
using GrTechRK.DTO;
using GrTechRK.External.ZenQuotes.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace GrTechRK.BSL.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo))
                .ForMember(dest => dest.LogoSrc, opt => opt.MapFrom(src => MapLogoSrc(src)))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => MapWebsite(src)));

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => MapCompany(src)));

            CreateMap<DailyQuote, QuoteDto>();
        }

        private string MapCompany(Employee employee)
        {
            return employee.Company?.Name;
        }

        private string MapWebsite(Company company)
        {
            return company.Website?.AbsoluteUri;
        }

        private static string MapLogoSrc(Company company)
        {
            if (company.Logo != null && company.Logo.Length > 0)
            {
                using MemoryStream ms = new MemoryStream(company.Logo);
                Image image = Image.FromStream(ms);
                ImageFormat imageFormat = image.RawFormat;
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                string type = codecs.First(codec => codec.FormatID == imageFormat.Guid).MimeType;

                return $"data:{type};base64,{Convert.ToBase64String(company.Logo, Base64FormattingOptions.None)}";
            } else
            {
                return "";
            }
        }
    }
}
