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
If you already have previously generated a token using the API or the web interface you can reinitialize the API with the alternate constructor.
```
//Login with an existing token
var ApiWrapper = new FogBugzApiWrapper("http://www.example.com/fogbugz", "<TokenStringHere>");
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
You can search the current filter using Search() method. This method has a few overloads for specifying the column information to return and the maximum number of search results.
```
//List all cases in filter
List<Case> cases = ApiWrapper.Search(""); //Alternatively you can user SearchCurrentFilter() method

//Search for certain string
List<Case> cases = ApiWrapper.Search("What is love?"); //bb dont hurt me
```
The previous two Search overloads return the Area, Number, Category, PriorityId, Priority Name, Project Name, Status and Title of each case. They also default to a maximum of 50 case results. To specify your search further you must use the next two overloads.

There two additional Search overloads can utilize the static class **CaseHtmlValue**. This class contains all the default column names defined in the HTML API so you don't have to look for the strings yourself.

```
//Search for a certain string listing only case titles
List<Case> cases = ApiWrapper.Search("Mario", new List<string>{CaseHtmlValue.Title});

//Search for a certain string listing case names and numbers
List<Case> cases = ApiWrapper.Search("Falco Lombardi", new List<string>{CaseHtmlValue.Title, CaseHtmlValue.Number});

//Search for strings without using CaseHtmlValue
List<Case> cases = ApiWrapper.Search("Falco Lombardi", new List<string>{"sTitle","ixBug"});

//Search for a certain string listing only case names with a maximum of 10 results
List<Case> cases = ApiWrapper.Search("Mario", new List<string>{CaseHtmlValue.Title}, 10);
```
You will notice that this method's output is a List\<Case\>\(\). Cases are the main way to change and store information while using Fogger. More on cases below.

## Cases
The output of these filter searches are Case objects. Case objects contain all the information about a case. Using the brilliant Fody NotifyPropertyChanged nuget package, each property changed is stored on the case object under the Changeset property. This generates a list of commands that Fogger will interpret in order to edit, assign, resolve, new and otherwise change a case.

Editing a case is simple. All you have to do is grab the output of a Filter, adjust the case property you want and feed it into the CaseManager object in the api.
```
List<Case> cases = ApiWrapper.Search("");
var case = cases[0]; //Assuming cases.Count() > 0

case.Title.Value = "Im bad at titles..."; //API is aware this is the only property changed

ApiWrapper.CaseManager.EditCase(case); //This line updates the case on the server
```

More info coming soon...
