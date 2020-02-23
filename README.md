# Fogger
A Work In Progress C# API Wrapper for Fogbugz

# Usage
Fogger commands are explained below. You can refer to the 'TestApi' sample project in the repository. This should contain an example that you can run and observe on your own.

## Initialization
First create a wrapper object using the api constructor. The constructor takes three parameters: **domain**, **username** and **password**.
```
//Init API Wrapper
var ApiWrapper = new FogBugzApiWrapper("http://www.example.com/fogbugz", PhoenixWright, sup3rs3cr3th4x);
```
This should log you into fogbugz and generate an api token Fogger uses for each command. The token will last indefinately until you call the Logoff() method.
```
//Get api token
var token = ApiWrapper.HttpClientHelper.ApiToken;

//Logoff
ApiWrapper.Logoff();
```
## Filters
Fogger uses 'Filter' objects. These filters correspond to filters on the FogBugz domain. You can get all the availiable filters using the GetFilters() method.
```
IList<Filter> filters = ApiWrapper.GetFilters();
```
This returns a list of filters. You can set the current filter using SetCurrentFilter(Filter filter)
```
ApiWrapper.SetCurrentFilter(filters[0]);
```
Finally you can search the current filter using SearchCurrentFilter() method. This method has a few overloads for specifying the column information to return and the maximum number of serach results.
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
The output of these filter searchs are Case objects.
Case objects contain all the information about a case. Using the brilliant Fody NotifyPropertyChanged nuget package, each property changed is stored on the case object under the Changeset property. This generates a list of commands that Fogger will interpret in order to edit, assign, resolve, new and otherwise change a case.

# More info coming soon...
