# try-lazy-dotnet

This project demonstrates three aspect of using dotnet Lazy<T> class:

1) Common usage of lazy loading pattern when you don't need an actual instance in place of initialization.
2) Singleton pattern implementation without dealing with CLR side effects and thread safety.
3) LazyThreadSafetyMode.PublicationOnly parameter to avoid persisting exceptions between threads.

The first aspect demonstrated with database and two related entities. You may find Car entity that contains reference to Brand entity.
When we're getting a Car, Brand property is lazy-loaded using Lazy<T>. Brand property will be loaded only in case we want to print the Brand.

The second aspect demonstrated by implementing Singleton pattern in CarRepository implementation.
When we want a single instance of repository, we may use Lazy<T> to initialize instance only when we actually ask for it.
Plus it will make its initializer thread safe and will avoid CLR side effect described here http://www.yoda.arachsys.com/csharp/beforefieldinit.html.

The third aspect demonstrated by using LazyThreadSafetyMode.PublicationOnly parameter in CarRepository implementation.
It says to re-create an instance again in case if on the other thread throws an exception during value initialization.
Lazy<T> value initializer throws an exception when it was called from a background thread. But it repeats initialization on the main thread.

[![Build Status](https://travis-ci.org/nikaburu/try-lazy-dotnet.svg?branch=master)](https://travis-ci.org/nikaburu/try-lazy-dotnet)
