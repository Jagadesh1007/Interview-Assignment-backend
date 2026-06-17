using System.Threading.Tasks;
using TestApi.Models;

namespace TestApi.Services
{
    public interface ICandidateService
    {
        Task SaveCandidateAsync(CandidateRequest request);
    }
}
