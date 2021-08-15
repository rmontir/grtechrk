using GrTechRK.Database.Models;

namespace GrTechRK.BSL.Interfaces
{
    public interface IMailService
    {
        void SendAsyc(string to, Employee employee);
    }
}
