7d-feedmunch
============

A few basic classes to help download and read 7digital feeds

The idea 
--------

To create a set of helper classes and a client to enable users to easily download, read, filter and dump our csv feeds into various custom formats.

### Use case 
i.e. a use case could be:

* feedType: track full
* filter: {labelId != 34}
* output: filesystem / mongodb / redis / whatever

With the idea that it would be something we could point clients at to enable to them to quickly ingest our feeds into whatever datastore they wanted.

### Custom Filtering / Dumping 

We could offer out of the box filter classes (based on cmd line parameters for example), with the ability for consumers to extend if they wanted more complex functionality. 

We could also offer out of the box data store conversion classes to enable a user to dump a feed to disk in the simplest case, or to redis or mongodb.
