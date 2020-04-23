using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Authentication;
using Application.Common.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

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
            authorizationRequestDTO = new AuthorizationRequestDTO();
            authorizationRequestDTO.Response_Type = (AuthorizationGrantType)Enum.Parse(typeof(AuthorizationGrantType), Response_Type, true);
            authorizationRequestDTO.Client_Id = Guid.Parse(Client_Id);
            authorizationRequestDTO.Redirect_Uri = Redirect_Uri;
            authorizationRequestDTO.Scope = Scope;
            authorizationRequestDTO.State = State;
     
        }
        public IActionResult OnPost() {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            UserLogin userLogin = new UserLogin(
                    configuration,
                    itsLogger,
                    JWTTokenService,
                    OAuthDbContext,
                    EncryptionService);
            if (userLogin.Login(user).IsAuthenticated) {
                return RedirectToPage("Approve", authorizationRequestDTO);
            }
            return RedirectToPage();
        }
    }
}