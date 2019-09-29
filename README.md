# ecom-angular
eCommerce app using angular and asp.net core 

This app has been developed with the below set of tools.

* asp.net core 2.1
* entity core
* asp.net core webapi
* angular 8 
* bootstrap
* NUnit unit test

The solution folder contains below folders.
* webui-ang
  This folder contains the angular web ui section. You can open this using visual studio or VS code. 
  You have to run npm update before building this project which will get all the necessary packages installed.
  
* webapi
  This folder contains the asp.net core web api. 
  
* order.queryservices & order.commandservice
   These folders contains the CQRS implementation using entity core framework as the ORM tool. 
  
* unittests
   The unit test project is added here. The unit test covers all the necessary code sections.
   
 * dbProject
    This folder contains the db project being used in this applicaiton. The DB scripts and product data can be found in the Database folder     in githib repository root.  
  
