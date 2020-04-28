using Application.Authentication;
using Application.Clients;
using Application.Common.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace TokenService.Pages
{
    public class AuthModel : BasePage
    {
        public AuthModel(IConfiguration configuration) : base(configuration)
        { }

        [BindProperty]
        public UserDTO user { get; set; }

        [BindProperty]
        public AuthorizationRequestDTO authorizationRequestDTO { get; set; }
        public void OnGet(string Response_Type, string Client_Id, string Redirect_Uri, string Scope, string State)
        {
            ClientService clientService = new ClientService(refToken,
                configuration,
                    itsLogger,
                    jwtToken,
                    OAuthDbContext,
                    EncryptionService);
            authorizationRequestDTO = new AuthorizationRequestDTO
            {
                Response_Type = (AuthorizationGrantType)Enum.Parse(typeof(AuthorizationGrantType), Response_Type, true),
                Client_Id = Guid.Parse(Client_Id),
                Redirect_Uri = Redirect_Uri,
                Scope = Scope,
                State = State
            };
            ClientDTO clientDTO = clientService.GetClient(Guid.Parse(Client_Id));

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            UserLogin userLogin = new UserLogin(refToken,
                    configuration,
                    itsLogger,
                    jwtToken,
                    OAuthDbContext,
                    EncryptionService);
            UserDTO userDTO = userLogin.Login(user);
            if (userDTO.IsAuthenticated)
            {
                authorizationRequestDTO.UserId = userDTO.UserId;
                return RedirectToPage("Approve", authorizationRequestDTO);
            }
            return RedirectToPage();
        }
    }
}