namespace Domain.Enums
{
    public enum ALG { HS256, HS384, HS512, RS256 }

    public enum ResponseType { code }

    public enum AuthorizationGrantType { authorization_code, client_credentials, password, refresh_token }
}
