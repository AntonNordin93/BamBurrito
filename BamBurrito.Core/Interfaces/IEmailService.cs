using System.Threading.Tasks;
using BamBurrito.Core.Entities;

namespace BamBurrito.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendOfferRequestEmailAsync(OfferRequest request);
    }
}
