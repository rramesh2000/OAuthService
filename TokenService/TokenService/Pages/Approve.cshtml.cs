using Application.Authorization;
using Application.Clients;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Web;

namespace TokenService.Pages
{
    public class ApproveModel : BasePage
    {
        public ApproveModel(IConfiguration configuration) : base(configuration)
        { }

        [BindProperty]
        public string Message { get; set; }

        [BindProperty]
        public AuthorizationRequestDTO authorizationRequestDTO { get; set; }
        public void OnGet(AuthorizationGrantType Response_Type, string Client_Id, string Redirect_Uri, string Scope, string State)
        {
            ClientService clientService = new ClientService(refToken, configuration,
                      itsLogger,
                      jwtToken,
                      OAuthDbContext,
                      EncryptionService);
            authorizationRequestDTO = new AuthorizationRequestDTO
            {
                Response_Type = Response_Type,
                Client_Id = Guid.Parse(Client_Id),
                Redirect_Uri = Redirect_Uri,
                Scope = Scope,
                State = State
            };
            ClientDTO clientDTO = clientService.GetClient(Guid.Parse(Client_Id));
            Message = clientDTO.ClientName + " would like access to " + Scope;
        }

        public IActionResult OnPost()
        {
            AuthorizationResponseDTO authResponseDTO = new AuthorizationResponseDTO();
            IAuthorizationService authorizationService = new AuthorizationService(
                refToken,
                         configuration,
                         itsLogger,
                         jwtToken,
                         OAuthDbContext,
                         EncryptionService);
            authResponseDTO = authorizationService.GetAuthorization(authorizationRequestDTO);
            return Redirect(authorizationRequestDTO.Redirect_Uri + "?code=" + HttpUtility.UrlEncode(authResponseDTO.Code) + "&state=" + authorizationRequestDTO.State);
        }

    }
}