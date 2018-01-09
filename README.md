# MapNavigator
Demo Project

**Domain:** Maps Navigation

**Integration:** Web API

This is a basic Web API / library project meant to intepret a collection of map instructions and reduce them to their simplest components.

The default implementation will take a collection of instructions in the following format:

> L3, R6, L8, R1, R1, L18

And simplify the instructions into absolute X and Y componenets in order to derive a "blocks traveled" metric.

**Technical Details**

Projects:

* MapNavigator
* MapNavigator.Tests
* MapNavigatorWeb
* MapNavigatorWeb.Tests

The two unit test projects are targetting .Net Core 2.0, using XUnit as the test framework.

MapNavigator is written as a .Net Standard library, targetting .Net Standard 2.0.

MapNavigatorWeb is a .Net Core 2.0 unified MVC project.

The entire solution should be able to be built and run on platforms other than Windows.

**Build Instructions**

No special steps should be needed to build and run this solution or its tests besides using the most recent version of Visual Studio 2017.

A nuget restore should happen automatically for all dependencies, and the built in TestExplorer will recognize and be able to run all tests.

**API Access Instructions**

Once you have the MapNavigatorWeb project running (it should be set as the startup project), the only API endpoint available is:

> http://localhost:49850/api/map

This endpoint is expecting you to make a POST request, where the POST body contains json that contains an "instructions" property with the
instruction string to run.

EXAMPLE POST BODY:

`
{
  "instructions": "L1, R1, L2, R2"
}
`

*You will also want to make sure your Content-Type header is set to application/json*

I usually use an application called Postman to run local requests. When filling in the request body, it has a dropdown to
choose the appropriate content type (JSON, in this case).

**API Output**

The API returns several properties beyond what the original instruction set asks for. The field that matches the project description
is "blocksToTravel." The other fields may be more useful when using other configurations.

**Input Validation**

Beyond validating that the "instructions" parameter has been supplied to the endpoint, by default (without changing compile time settings manually),
only "L" & "R" are valid directions. Distances must be integers, and negative integers are allowed.

These are the defaults to match the project description. The library was written so that any of this can easily change with minimal adjustments.

Anticipated errors should be returned in a 400 (Bad Request) HTTP response.


