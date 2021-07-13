# Coding Assesment

## Requirements

The concept is to request several companies&#39; API for offers and select the best deal.

Conditions:
No UI expected.
No SQL required.
Must be unit-tested.

Process Input:
* one set of data {{source address}, {destination address}, [{carton dimensions}]}
* Multiple API using the same data with different signatures
Process Output:
* All API respond with the same data in different formats
* Process must query, then select the lowest offer and return it in the least amount of time
 
Sample APIs, each with its own url and credentials

API1 (JSON)

- Input {contact address, warehouse address, package dimensions:[]}
- Output {total}

API2 (JSON)

- Input {consignee, consignor, cartons:[]}
- Output { amount }


API3 (XML)

Input: &lt;xml&gt;&lt;source/&gt;&lt;destination/&gt;&lt;packages&gt;&lt;package/&gt;&lt;/packages&gt;&lt;/xml&gt;

Output: &lt;xml&gt;&lt;quote/&gt;&lt;/xml&gt;



## Getting Started

### Solution Structure

```
├── samples
│   ├── Api1  // simple web service with api1 json data
│   ├── Api2 // simple web service with api2 json data
│   ├── Api3 // simple web service with api3 xml data 
├── src
│   ├── Controllers // web page to trigger service execution, this can be implemented as command line but DI set up is simpler with asp .net core project
│   ├── Helpers // json camel case serializer setting
│   ├── Services
│   │   ├── InputDataService  // Loads initial request data
│   │   ├── Service1  // Api1 consumer service, using api key as auth
│   │   ├── Service2  // Api2 consumer service, using usr/pswd as auth
│   │   ├── Service3  // Api3 consumer service, using usr/pswed as auth
├── tests
├── readme.md
├── run.bat
├── sampleInput.json // sample data for initial request
└── .gitignore
```

## Comments

* In code comments can be searched, includes: (GK)

* The main project folder structure is following microsoft convention, not splitting business units into separate projects.

Example:

```
├── Project Name
│   ├── Services
│   │   ├── Service1.cs
│   │   ├── IService1.cs
```


### Executing program

Build in VS or CLI and execute run.bat
Will run 3 sample web services and the web app which queries them.

## Help

Make sure local ssl dev cert is configured, if not run

```
dotnet dev-certs https --trust

```

