namespace Domain.Enums
{
    public enum ALG { HS256, HS384, HS512, RS256 }

    public enum ResponseType { code }

    public enum AuthorizationGrantType {code, devicecode, clientcredentials, refreshtoken }
}
