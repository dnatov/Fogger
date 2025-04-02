# Fogger
A Work In Progress C# API Wrapper for Fogbugz

# Usage
Fogger commands are explained below. You can refer to the 'TestApi' sample project in the repository. This should contain an example that you can run and observe on your own.

## Initialization
Initialize the `FoggerApiWrapper` object with three parameters: **domain**, **username** and **password**.
```
//Init API Wrapper
var OurApiWrapper = new FoggerApiWrapper("http://www.example.com/fogbugz", PhoenixWright, sup3rs3cr3th4x);
```

Internally, this creates a token used for API calls.

### Token

If the credentials are valid the constructor will create a FogBugz API token which is used during your session. The token is stored in the nested HttpClientHelper object. The token can be invalidated by executing the `Logoff()` method. 

**Note:** FogBugz recommends token reuse instead of repeated logins.

### Accessing the Token

```
//Get api token, if you need it
var token = OurApiWrapper.HttpClientHelper.ApiToken;
```

### Invalidate the Token

```
//Logoff
OurApiWrapper.Logoff();
```
If you already have previously generated a token using the API or the web interface you can reinitialize the API with the alternate constructor.
```
//Login with an existing token
var OurApiWrapper = new FoggerApiWrapper("http://www.example.com/fogbugz", "<TokenStringHere>");
```
## Filters
Fogger uses 'Filter' objects. These filters correspond to filters on the FogBugz domain.

### Getting Filters

```
IList<Filter> filters = OurApiWrapper.GetFilters();
```

### Setting Current Filter

```
OurApiWrapper.SetCurrentFilter(filters[0]);
```

### Searching Current Filter

```
//List all cases in filter
List<Case> cases = OurApiWrapper.Search(""); //Alternatively you can use SearchCurrentFilter() method

//Search for a certain string in current filter
List<Case> cases = OurApiWrapper.Search("What is love?"); //bb dont hurt me
```
**Note:** Unless specified, all Search methods return the `Area, Number, Category, PriorityId, Priority Name, Project Name, Status and Title` of each case by default. They also default to a maximum of 50 case results. To specify your search further you must use the next two overloads.

### Refining your Search

There are two additional Search overloads that utilize the static class `CaseHtmlValue`. This class contains all the default column names defined in the HTML API so you don't have to look for the strings yourself.

```
//Search for a certain string listing only case titles
List<Case> cases = OurApiWrapper.Search("Mario", new List<string>{CaseHtmlValue.Title});

//Search for a certain string listing case names and numbers
List<Case> cases = OurApiWrapper.Search("Falco Lombardi", new List<string>{CaseHtmlValue.Title, CaseHtmlValue.Number});

//Search for strings without using CaseHtmlValue
List<Case> cases = OurApiWrapper.Search("Falco Lombardi", new List<string>{"sTitle","ixBug"});

//Search for a certain string listing only case names with a maximum of 10 results
List<Case> cases = OurApiWrapper.Search("Mario", new List<string>{CaseHtmlValue.Title}, 10);
```
You will notice that this method's output is a `List<Case>()`. Cases are the main way to change and store information while using Fogger. More on cases below.

## Cases
The output of these filter searches are Case objects. Case objects contain all the information about a case. Using the brilliant Fody NotifyPropertyChanged nuget package, each property changed is stored on the case object under the Changeset property. This generates a list of commands that Fogger will interpret in order to edit, assign, resolve, new and otherwise change a case.

### Editing Cases

All you have to do is grab the output of a Filter, adjust the case property you want and feed it into the CaseManager object in the api.
```
List<Case> cases = OurApiWrapper.Search("");
var case = cases[0]; //Assuming cases.Count() > 0

case.Title.Value = "Im bad at titles..."; //API is aware this is the only property changed

OurApiWrapper.CaseManager.EditCase(case); //This line updates the case on the server
```

More info coming soon...
