# OAuthService in .Net Core by Ritesh Ramesh

 
 ## OAuth Service  
 
 This is an open source light weight OAuth Service prototype code repository written in .Net Core. The aim of this project is to allow .Net developers and architects to use as a prototype for in house implementation of OAuth services. Contributions suggestions and ideas are all welcome please see contributions section to actively contribute.
   
 ## Architecture
 
 This .Net core service is built on clean architecture principles.
 
 ## Authentication and Validation
 
 The validation and authentication uses the chain of responsibility pattern so you can add any number of handlers to extend the token validation process. 
 Currently it supports a signature validation handler and a revocation handler.
 
 ## JWT token 
 
 You can use any other type of token by implementing the ITokenService interface.  
 
 ## Error handling 
 
 Invalid User Error indicates that the user credentials are invalid.   
 Invalid Token Error indicates that the token is invalid.  
 
 ## Installation
 
 ## Usage
 
 ### Register User   
 Usage: (POST) api/register    
 Description: Using this API you can register new users. This API accepts a username and password then returns a full User.  
 
 ### Login User  
 Usage : (POST) api/token  
 Description: Using this API with a username and password in the request you can get an access token and refresh token.  
 ** Access tokens are short lived between 5 - 10 mins while refresh tokens are long lived 1 to 2 weeks. 
 ** The idea is you use the refresh token to retrieve new access tokens periodically (E.g. every 15 mins).  
 
 ### Verify Token:   
 Usage : (POST) api/token/verify  
 Description: Using this API you can verify if a token is still valid.  
 
 ### Refresh Token   
 Usage : (POST) api/token/refresh   
 Description: Using this API you can provide an existing refresh token and retrieve a new refresh token and access token.  
 Only one Refresh Token is issued per user at any given time.   
  
 ### Revocation   
 Usage : (POST) api/token/revoke   
 Description:  You can revoke the refresh token at any time which will invalidate the accesstokens.   
 
 ## Extending   
 
 Access token:  are generated using the JWT standard by default. This can be swapped with other token specifications by implementing the ITokenService interface. See here https://wesleyhill.co.uk/p/alternatives-to-jwt-tokens/ for alternative formats to JWT. 
 
 Authorization: is not implemented in this repo, to allow consumers of the code to integrate with other system or extend as required.   
 
 Data persistence: This implementation uses MSSQL to persist data by default. The data persistence technology can be switched to any alternative by implementing the IDBService interface.
 
 Encryption: This implementation uses SHA 1 and RNG crypto standards for hashing and encryption. These can be replaced by implementing the IEncryptionService interface.
 
 ## Contributing
 Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

 Please make sure to update tests as appropriate.
 
 ## License
[MIT](https://choosealicense.com/licenses/mit/)
