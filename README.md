# OAuthService in .Net Core 
 rramesh2000/angular.js

 
 ## OAuth Service in 
 
 This is an open source OAuth Service written in .Net Core 
 
 The OAuth design uses JWT style tokens to impliment OAuth flows. 
 
 
 ##Architecture
 
 This .Net core service is built on clean architecture principles.
 
 ## Authentication and Validation
 
 The validation and authentication uses the chain of responsibility pattern so you can add any number of handlers to extend the token validation process. 
 Currently it supports a signature validation handler and a revocation handler.
 
 ## JWT token 
 
 You can use any other type of token by implimenting the ITokenService interface.  
 
 
 ## Error handling 
 
 ## Installation
 
 # Usage
 
 ## Extending 
  
 ## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.
 
 ## License
[MIT](https://choosealicense.com/licenses/mit/)