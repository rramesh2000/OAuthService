using Application.Authentication;
using Application.Authorization;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

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
            authorizationRequestDTO = new AuthorizationRequestDTO();
            authorizationRequestDTO.Response_Type = Response_Type;
            authorizationRequestDTO.Client_Id = Guid.Parse(Client_Id);
            authorizationRequestDTO.Redirect_Uri = Redirect_Uri;
            authorizationRequestDTO.Scope = Scope;
            authorizationRequestDTO.State = State;
        }

        public IActionResult OnPost()
        {
            AuthorizationResponseDTO authResponseDTO = new AuthorizationResponseDTO();
            IAuthorizationService authorizationService = new AuthorizationService(
                         configuration,
                         itsLogger,
                         JWTTokenService,
                         OAuthDbContext,
                         EncryptionService);
            authResponseDTO = authorizationService.GetAuthorization(authorizationRequestDTO);
            return Redirect(authorizationRequestDTO.Redirect_Uri + "?code=" + authResponseDTO.Code + "&state=" + authorizationRequestDTO.State);
        }

    }
}