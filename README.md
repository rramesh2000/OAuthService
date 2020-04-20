# OAuthService in .Net Core by Ritesh Ramesh

 
 ## About OAuth Service  
 
 OAuth Service is an open source OAuth 2.0 service prototype written in .Net Core v3. This is written and maintained by Ritesh Ramesh. The aim of this project is to allow .Net developers and architects to use as a prototype for in house implementations of OAuth services. Contributions suggestions and ideas are all welcome please see contributions section to actively contribute.

 You can use this service to secure REST API resources, SOAP based webservice and web applications.   
 
 ## Architecture
 
 This .Net core service is built on clean architecture principles.
 
 ## Specifications 
 
 JWT token supports the following algorithms HS256, HS384, HS512, RS256
 
 ### Authentication and Validation
 
 The validation and authentication uses the chain of responsibility pattern so you can add any number of handlers to extend the token validation process. 
 Currently it supports a signature validation handler and a revocation handler.
 
 ### JWT token 
 
 You can use any other type of token by implementing the ITokenService interface.  
 
 ### Error handling 
 
 Invalid User Error indicates that the user credentials are invalid.   
 Invalid Token Error indicates that the token is invalid.  
 
 ## Installation
 
 Install the required .NET Core SDK  
 Install Git  
 Install Docker 
 
 ## Usage
 
 All API's are exposed via a Swagger documentation tool at this endpoint {host}/swagger/index.html.  
 Also in the source code is a test folder that contains Postman environment and collection files that you can use with Postman.
 
  
 ###  This is also available as a docker container https://registry.hub.docker.com/rramesh1000
  
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
