[![Build Status](https://travis-ci.org/gregsochanik/7d-feedmunch.png?branch=master)](https://travis-ci.org/gregsochanik/7d-feedmunch)

7d-feedmunch
============

A few basic classes to help download and read 7digital feeds

The idea 
--------

To create a set of helper classes and a client to enable users to easily download, read, filter and dump our csv feeds into various custom formats.

### Use case 
i.e. a use case would be:

* feedCatalog: track
* feedType: update
* filter: labelId != 3
* output: filesystem / mongodb / redis / whatever

### Custom Filtering / Dumping 

We could offer out of the box filter classes (based on cmd line parameters for example), with the ability for consumers to extend if they wanted more complex functionality. 

We could also offer out of the box data store conversion classes to enable a user to dump a feed to disk in the simplest case, or to redis or mongodb.

FeedMuncher
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
 



