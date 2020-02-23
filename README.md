# Fogger
A Work In Progress C# API Wrapper for Fogbugz

# Usage
Fogger commands are explained below. You can refer to the 'TestApi' sample project in the repository. This should contain an example that you can run and observe on your own.

## Initialization
In order to use Fogger, you must first initialize the FogBugzApiWrapper object, which is the main class that contains everything needed to interact with the API. The API wrapper uses a constructor that takes three parameters: **domain**, **username** and **password**. These parameters are your existing FogBugz credentials.
```
//Init API Wrapper
var ApiWrapper = new FogBugzApiWrapper("http://www.example.com/fogbugz", PhoenixWright, sup3rs3cr3th4x);
```
If the credentials are valid the constructor will create a FogBugz API token which is used during your session. The token is stored in the nested HttpClientHelper object. The token can be invalidated by executing the Logoff() method. FogBugz recommends token reuse instead of repeated logins.
```
//Get api token
var token = ApiWrapper.HttpClientHelper.ApiToken;

//Logoff
ApiWrapper.Logoff();
```
## Filters
Fogger uses 'Filter' objects. These filters correspond to filters on the FogBugz domain. You can get all the available filters using the GetFilters() method.
```
IList<Filter> filters = ApiWrapper.GetFilters();
```
This returns a list of filters. You can set the current filter using SetCurrentFilter(Filter filter)
```
ApiWrapper.SetCurrentFilter(filters[0]);
```
Finally you can search the current filter using SearchCurrentFilter() method. This method has a few overloads for specifying the column information to return and the maximum number of search results.
```
//List all cases in filter
var cases = ApiWrapper.SearchCurrentFilter();

//Search for certain string
var cases = ApiWrapper.SearchCurrentFilter("What is love?"); //bb dont hurt me

//Search for certain string listing only case names
var cases = ApiWrapper.SearchCurrentFilter("Mario", new List<string>{"sTitle"});

//Search for certain string listing only case names with a maximum of 10
var cases = ApiWrapper.SearchCurrentFilter("Mario", new List<string>{"sTitle"}, 10);
```
You can find a comprehensive list of column information here: https://support.fogbugz.com/hc/en-us/articles/360011343413-FogBugz-XML-API-Version-8#Sample_XML_Payloads

## Cases
The output of these filter searches are Case objects.
Case objects contain all the information about a case. Using the brilliant Fody NotifyPropertyChanged nuget package, each property changed is stored on the case object under the Changeset property. This generates a list of commands that Fogger will interpret in order to edit, assign, resolve, new and otherwise change a case.

More info coming soon...
