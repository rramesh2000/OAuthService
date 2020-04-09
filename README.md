# OAuthService in .Net Core by Ritesh Ramesh

 
 ## OAuth Service  
 
 This is an open source OAuth Service written in .Net Core  
 The OAuth design uses JWT style tokens to impliment OAuth flows.  
 
 ## Architecture
 
 This .Net core service is built on clean architecture principles.
 
 ## Authentication and Validation
 
 The validation and authentication uses the chain of responsibility pattern so you can add any number of handlers to extend the token validation process. 
 Currently it supports a signature validation handler and a revocation handler.
 
 ## JWT token 
 
 You can use any other type of token by implimenting the ITokenService interface.  
 
 ## Error handling 
 
 Invalid User Error indicates that the user credentials are invalid.   
 Invalid Token Errro inidcates that the token is invalid.  
 
 ## Installation
 
 ## Usage
 
 ### Register User 
 Usage: (POST) api/register
 Description: Using this API you can register new users. This API accepts a username and password then returns a full User.  
 
 ### Login User 
 Usage : (POST) api/token
 Description: Using this API with a username and password  in teh request you can get an access token and refresh token.
 ** Access tokens are shortlived between 5 - 10 mins while refresh tokens are longlived 1 to 2 weeks. 
 ** The idea is you use the refresh token to retrive new access tokens periodically (E.g: every 15 mins). 
 
 ### Verify Token: 
 Usage : (POST) api/token/verify
 Description: Using this API you can verify if a token is still valid. 
 
 ### Refresh Token: 
 Usage : (POST) api/token/refresh
 Description: Using this APi you can provide an existing refresh token and retrieve a new refresh token and access token.
 Only one Refresh Token is issued per user at any given time. 
  
 ### Revocation 
 Usage : (POST) api/token/revoke
 Description:  You can revoke the refresh token at anytime which will invalidate the accesstokens. 
 
 ## Extending 
 The User credentials has been kept relatively simple with no authorization built out of the box. The idea is to allow users of this Oauth service to integrate it easily with any existing authorization system or extend this to build the authorization out as required.  
 
 
 ## Contributing
 Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

 Please make sure to update tests as appropriate.
 
 ## License
[MIT](https://choosealicense.com/licenses/mit/)