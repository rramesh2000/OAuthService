namespace Application.TokenValidation.Handlers
{
    public abstract class TokenValidationHandler
    {
        protected TokenValidationHandler successor;
        public AuthorizationVm authorizationVm { get; set; } 
        public void SetSuccessor(TokenValidationHandler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleRequest(AuthorizationVm auth);
    }
}
