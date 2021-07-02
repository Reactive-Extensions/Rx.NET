Reactive Extensions
======================

Channel  | Rx | Ix |
-------- | :------------: | :-------------: |
Build | [![Build Status](https://dev.azure.com/dotnet/Rx.NET/_apis/build/status/Rx.NET-CI?branchName=master)](https://dev.azure.com/dotnet/Rx.NET/_build/latest?definitionId=9) | [![Build Status](https://dev.azure.com/dotnet/Rx.NET/_apis/build/status/Ix.NET-CI?branchName=master)](https://dev.azure.com/dotnet/Rx.NET/_build/latest?definitionId=28)
NuGet.org | [![#](https://img.shields.io/nuget/v/System.Reactive.svg)](https://www.nuget.org/packages/System.Reactive/) | [![#](https://img.shields.io/nuget/v/System.Interactive.svg)](https://www.nuget.org/packages/System.Interactive/)
[Azure<br>Artifacts](https://dev.azure.com/dotnet/Rx.NET/_packaging?_a=feed&feed=RxNet) | [![System.Reactive package in RxNet feed in Azure Artifacts](https://azpkgsshield.azurevoodoo.net/dotnet/Rx.NET/RxNet/System.Reactive)](https://dev.azure.com/dotnet/Rx.NET/_packaging?_a=package&feed=5afc77bd-23b4-46f8-b725-40ebedab630c&package=3c02dce4-f7e9-43ec-a014-28ea9fc46f82&preferRelease=true) | [![System.Interactive package in RxNet feed in Azure Artifacts](https://azpkgsshield.azurevoodoo.net/dotnet/Rx.NET/RxNet/System.Interactive)](https://dev.azure.com/dotnet/Rx.NET/_packaging?_a=package&feed=5afc77bd-23b4-46f8-b725-40ebedab630c&package=a3311bc0-c6ea-4460-bea8-b65d633e2583&preferRelease=true) 

### Join the conversation

Catch us in the #rxnet channel over at http://reactiveui.net/slack

### Get nightly builds
- NuGet v3 feed url (VS 2015+): `https://pkgs.dev.azure.com/dotnet/Rx.NET/_packaging/RxNet/nuget/v3/index.json`

## System.Linq.Async / System.Interactive.Async / System.Interactive

### v4.0 changes

Ix Async 4.0 has a breaking change from prior versions due to being the first LINQ implementation
to support the new C# 8 [async streams](https://github.com/dotnet/roslyn/blob/master/docs/features/async-streams.md) feature. This means for .NET Standard 2.1 and .NET Core 3 targets, we use the in-box interfaces for `IAsyncEnumerable<T>` and friends. On other platforms, we provide the implementation, so you can use `await foreach` and create async iterators as you would expect. The types will unify to the system ones where the platform provides it.

There are many breaking changes here; a full set of changenotes is on the way.

## System.Reactive

### v5.0 changes
Rx.NET v5 is required for .NET 5 support of CSWinRT; earlier versions of Rx won't work correctly. To get the Windows API, you need to use the latest Windows SDK (19041), though you can target earlier versions of Windows. 

**Breaking changes**
In Windows, since the `net5.0-windows10.0.19041` target now supports all Windows desktop object models, on UWP `DispatcherObservable` and releated SubscribeOn/ObserveOn were renamed to be `CoreDispatcherObservable`, `SubscribeOnCoreDispatcher`, and `ObserveOnCoreDispatcher`. This reflects the OS type names. `Dispatcher` refers only to the WPF type.

#### Supported Platforms
Rx 5.x supports the following platforms

- .NET 5
- .NET 5 with the Windows 10 19041 SDK (able to target earlier Windows versions)
- .NET Framework 4.7
- UWP 10.0.16299
- .NET Standard 2.0

Support for UWP 10.0 has been removed.

### v4.0 changes
Due to the [overwhelming](https://github.com/dotnet/reactive/issues/299) [pain](https://github.com/dotnet/reactive/issues/305) that fixing [#205 - Implement assembly version strategy](https://github.com/dotnet/reactive/issues/205) caused, we have refactored the libraries into a single library `System.Reactive`. To prevent breaking existing code that references the v3 libraries, we have facades with TypeForwarders to the new assembly. If you have a reference to a binary built against v3.0, then use the new `System.Reactive.Compatibility` package. 

#### Supported Platforms
Rx 4.1 supports the following platforms

- .NET Framework 4.6+
- .NET Standard 2.0+ (including .NET Core, Xamarin and others)
- UWP

Notably, Windows 8, Windows Phone 8 and legacy PCL libraries are no longer supported. 

### v3.0 breaking changes
The NuGet packages have changed their package naming in the move from v2.x.x to v3.0.0
 * ~~`Rx-Main`~~ is now [`System.Reactive`](https://www.nuget.org/packages/System.Reactive/)
 * ~~`Rx-Core`~~ is now [`System.Reactive.Core`](https://www.nuget.org/packages/System.Reactive.Core/)
 * ~~`Rx-Interfaces`~~  is now [`System.Reactive.Interfaces`](https://www.nuget.org/packages/System.Reactive.Interfaces/)
 * ~~`Rx-Linq`~~  is now [`System.Reactive.Linq`](https://www.nuget.org/packages/System.Reactive.Linq/)
 * ~~`Rx-PlatformServices`~~  is now [`System.Reactive.PlatformServices`](https://www.nuget.org/packages/System.Reactive.PlatformServices/)
 * ~~`Rx-Testing`~~  is now [`Microsoft.Reactive.Testing`](https://www.nuget.org/packages/Microsoft.Reactive.Testing/)

This brings the NuGet package naming in line with NuGet guidelines and also the dominant namespace in each package.
The strong name key has also changed, which is considered a breaking change.
However, there are no expected API changes, therefore, once you make the NuGet change, no code changes should be necessary.

A Brief Intro
-------------------

The Reactive Extensions (Rx) is a library for composing asynchronous and event-based programs using observable sequences and LINQ-style query operators. Using Rx, developers *__represent__* asynchronous data streams with [Observables](https://docs.microsoft.com/dotnet/api/system.iobservable-1), *__query__* asynchronous data streams using [LINQ operators](http://msdn.microsoft.com/en-us/library/hh242983.aspx), and *__parameterize__* the concurrency in the asynchronous data streams using [Schedulers](http://msdn.microsoft.com/en-us/library/hh242963.aspx). Simply put, Rx = Observables + LINQ + Schedulers.

Whether you are authoring a traditional desktop or web-based application, you have to deal with asynchronous and event-based programming from time to time. Desktop applications have I/O operations and computationally expensive tasks that might take a long time to complete and potentially block other active threads. Furthermore, handling exceptions, cancellation, and synchronization is difficult and error-prone.

Using Rx, you can represent multiple asynchronous data streams (that come from diverse sources, e.g., stock quote, tweets, computer events, web service requests, etc.), and subscribe to the event stream using the `IObserver<T>` interface. The `IObservable<T>` interface notifies the subscribed `IObserver<T>` interface whenever an event occurs.

Because observable sequences are data streams, you can query them using standard LINQ query operators implemented by the Observable extension methods. Thus you can filter, project, aggregate, compose and perform time-based operations on multiple events easily by using these standard LINQ operators. In addition, there are a number of other reactive stream specific operators that allow powerful queries to be written.  Cancellation, exceptions, and synchronization are also handled gracefully by using the extension methods provided by Rx.

Rx complements and interoperates smoothly with both synchronous data streams (`IEnumerable<T>`) and single-value asynchronous computations (`Task<T>`) as the following diagram shows:


<table>
   <th></th><th>Single return value</th><th>Multiple return values</th>
   <tr>
      <td>Pull/Synchronous/Interactive</td>
      <td>T</td>
      <td>IEnumerable&lt;T&gt;</td>
   </tr>
   <tr>
      <td>Push/Asynchronous/Reactive</td>
      <td>Task&lt;T&gt;</td>
      <td>IObservable&lt;T&gt;</td>
   </tr>
</table>

Additional documentation, video, tutorials and HOL are available on [MSDN](https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh242985(v=vs.103)), on [*Introduction to Rx*](http://introtorx.com/), [*ReactiveX*](http://reactivex.io/), and [ReactiveUI](https://reactiveui.net/).

Flavors of Rx
---------------

* __Rx.NET__: *(this repository)* The Reactive Extensions (Rx) is a library for composing asynchronous and event-based programs using observable sequences and LINQ-style query operators.
* [RxJS](https://github.com/ReactiveX/rxjs): The Reactive Extensions for JavaScript (RxJS) is a library for composing asynchronous and event-based programs using observable sequences and LINQ-style query operators in JavaScript which can target both the browser and Node.js.
* [RxJava](https://github.com/ReactiveX/RxJava): Reactive Extensions for the JVM – a library for composing asynchronous and event-based programs using observable sequences for the Java VM.
* [RxScala](https://github.com/ReactiveX/RxScala): Reactive Extensions for Scala – a library for composing asynchronous and event-based programs using observable sequences
* [RxCpp](https://github.com/Reactive-Extensions/RxCpp): The Reactive Extensions for Native (RxCpp) is a library for composing asynchronous and event-based programs using observable sequences and LINQ-style query operators in both C and C++.
* [Rx.rb](http://rxrb.codeplex.com): A prototype implementation of Reactive Extensions for Ruby (Rx.rb).
* [RxPy](https://github.com/ReactiveX/RxPY): The Reactive Extensions for Python 3 (Rx.Py) is a set of libraries to compose asynchronous and event-based programs using observable collections and LINQ-style query operators in Python 3.


Interactive Extensions
-----------------------
* __Ix.NET__: *(included in this repository)* The Interactive Extensions (Ix) is a .NET library which extends LINQ to Objects to provide many of the operators available in Rx but targeted for IEnumerable<T>.
* [IxJS](https://github.com/ReactiveX/IxJS): An implementation of LINQ to Objects and the Interactive Extensions (Ix) in JavaScript.
* [IxCpp](https://github.com/Reactive-Extensions/RxCpp): An implementation of LINQ for Native Developers in C++

Applications
-------------
* [Tx](https://github.com/Reactive-Extensions/Tx): a set of code samples showing how to use LINQ to events, such as real-time standing queries and queries on past history from trace and log files, which targets ETW, Windows Event Logs and SQL Server Extended Events.
* [LINQ2Charts](http://linq2charts.codeplex.com): an example for Rx bindings.  Similar to existing APIs like LINQ to XML, it allows developers to use LINQ to create/change/update charts in an easy way and avoid having to deal with XML or other underneath data structures. We would love to see more Rx bindings like this one.

Contributing
------------------

### Source code

* Clone the sources: `git clone https://github.com/dotnet/reactive`
* [Building, testing and debugging the sources](https://github.com/dotnet/reactive/wiki/Building%20Testing%20and%20Debugging)

### Contribute!

Some of the best ways to contribute are to try things out, file bugs, and join in design conversations.

* [How to Contribute](https://github.com/dotnet/reactive/wiki/Contributing-Code)
* [Pull requests](https://github.com/dotnet/reactive/pulls): [Open](https://github.com/dotnet/reactive/pulls?q=is%3Aopen+is%3Apr)/[Closed](https://github.com/dotnet/reactive/pulls?q=is%3Apr+is%3Aclosed)

Looking for something to work on? The list of [up for grabs issues](https://github.com/dotnet/reactive/issues?q=is%3Aopen+is%3Aissue+label%3A%22Up+for+Grabs%22) is a great place to start.

This project has adopted a code of conduct adapted from the [Contributor Covenant](http://contributor-covenant.org/) to clarify expected behavior in our community. This code of conduct has been [adopted by many other projects](http://contributor-covenant.org/adopters/). For more information see the [Code of conduct](http://www.dotnetfoundation.org/code-of-conduct).

<h2 align="center">.NET Foundation</h2>

System.Reactive is part of the [.NET Foundation](https://www.dotnetfoundation.org/). Other projects that are associated with the foundation include the Microsoft .NET Compiler Platform ("Roslyn") as well as the Microsoft ASP.NET family of projects, Microsoft .NET Core & Xamarin Forms.

<h2 align="center">Core Team</h2>

<table>
  <tbody>
    <tr>
      <td align="center" valign="top">
        <img width="150" height="150" src="https://github.com/ghuntley.png?s=150">
        <br>
        <a href="https://github.com/ghuntley">Geoffrey Huntley</a>
        <p>Sydney, Australia</p>
      </td>
      <td align="center" valign="top">
        <img width="150" height="150" src="https://github.com/clairernovotny.png?s=150">
        <br>
        <a href="https://github.com/clairernovotny">Claire Novotny</a>
        <p>New York, USA</p>
      </td>
      <td align="center" valign="top">
        <img width="150" height="150" src="https://github.com/akarnokd.png?s=150">
        <br>
        <a href="https://github.com/akarnokd">David Karnok</a>
        <p>Budapest, Hungary</p>
      </td>
      <td align="center" valign="top">
        <img width="150" height="150" src="https://github.com/danielcweber.png?s=150">
        <br>
        <a href="https://github.com/danielcweber">Daniel C. Weber</a>
        <p>Aachen, Germany</p>
      </td>
     </tr>
  </tbody>
</table>
