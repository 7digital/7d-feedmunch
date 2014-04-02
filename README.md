7d-feedmunch
============

An api to help download, read and filter 7digital feeds.

It allows you to:

* Download the current latest Full or incremental feed with the minumum of fuss
* Filter feeds using a simple field name filter expression
* Write the feed to any .NET `Stream` or custom `IFeedStreamWriter` you wish

SevenDigital.FeedMunch
==

The basic syntax is as follows:

```C#
FeedMunch.Download
				.WithConfig(feedMunchConfig)
				.InvokeAndWriteTo(stream);
```

You can fire it without supplying a `FeedMunchConfig`, which uses default settings. 

Building Custom IFeedStreamWriter
==

As well as the standard .NET `Stream` object, the `InvokeAndWriteTo` method accepts an `IFeedStreamWriter` with a single `Write` method. This allows you to create custom instructions for writing the results of the feed.

Currently there are 2 supplied as part of example projects (see below):

`GzippedHttpFeedStreamWriter` : Takes the `Stream` and writes it to the supplied `HttpResponseBase` output stream as a `GZipStream`, and updates the response headers accordingly

`GzippedFileFeedStreamWriter` : Takes the `Stream` and writes it to a file path specified in the `FeedMunchConfig` compressed to gzip.

SevenDigital.Api.Feeds.Filtered
==

This is an example of a .NET web service that uses the `SevenDigital.FeedMunch` api to write the results to the Http response stream as a gzipped file.

The custom `IFeedStreamWriter` used is `GzippedHttpFeedStreamWriter`.

Feeds can be accessed via the following endpoint template:

`~/{artist|release|track}/{full|updates}?filter={filterexpression}&country={country}`

Where `filterexpression` can be `name=U2` or `licensorId!=1`, but the `=` or `!=` must be urlencoded (`%3D`) 
and `country` is the 2 letter country code (e.g. country=US), defaulting to GB.

SevenDigital.FeedMuncher.exe
===========

`FeedMuncher` is a .NET 4.5 console app that does the following

Grabs a feed either directly from the feeds-api or, if you specify one, a local feed in *.gz format.

Accepts the following parameters:

* `/feed` - ONe of either "Full" for a Full feed, or "Update" to indicate an incremental feed
* `/catalog` - One of either "Artist", "Release" or "Track"
* `/filter` - A filter expression based on a fieldname, e.g. '/filter licensorID=1'
  accepts either `=` or `!=` as operators and can be passed an array. Therefore:
  licensorID=1 would return only rows that match this
  licensorID!=2,3 would only return rows that were NOT licensorID=2 or licensorID=3
  licensorID=2,3 would only return rows that were licensorID=2 or licensorID=3
* `/output` - A file system path where you want the zipped output feed to be written to
* `/country` - The country code in ISO format (e.g. GB) of the feed you want.
* `/existing` - The local path to an existing feed you wish to filter, the values you specify with `/feed` and `catalog` must match the feed that you are passing, otherwise an exception will be thrown.

It uses `GzippedFileFeedStreamWriter` to write the data to a file.

Other Custom FeedStreamWriters
==

There are currently only 2 supplied output options at the moment, but it wouldn't take a huge leap of imagination to build other types for example a `MongoDbFeedStreamWriter` that writes the Stream produced by `SevenDigital.FeedMunch` to a MongoDb collection, or a `RedisFeedStreamWriter` that does thesame for Redis.

Pull requests accepted!



