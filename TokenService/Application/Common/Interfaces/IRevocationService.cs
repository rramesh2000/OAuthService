using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IRevocationService
    {
        string TokenRevocation(RevocationDTO revocationDTO);
    }
}