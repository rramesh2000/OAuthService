using Application.Common.Models;
using System;

namespace Application.Common.Interfaces
{
    public interface IClientService
    {
        ClientDTO GetClient(Guid Client_Id);
    }
}