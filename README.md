# OSMScout

## OpenStreetMap Scout

![OpenStreetMap Scout](/OpenStreetMapScoutScreenshot1.png)

## Project Description

OpenStreetMap Client in .Net

Displays gpx tracks and OSM-relations on a SlippyMap. Includes also a OpenStreetMapApi to access the database.

This is a Visualisation Tool for the OpenStreetMap (OSM) www.OpenStreetMap.org.

It can be used online and offline, when you have own map images in the "tile" sturcture.


- show the different public available maps, that are based on OpenStreetMap data ( or not ;-) )
- map images will be cached in temp directory to speed up the map
- paint own GPX-Tracks on top of the map
- import osm dump into memory
- or get osm data from http://api.openstreetmap.org/api/0.5/
- show a selected relation ( set of ways ) on the map
- filter and show osm nodes on the map
- export the seen map as png file

Why should i use this tool?
- You have added a hiking-track to the OSM, but the MAPNIK layer doesn't show it? Paint it on to of existing maps with OSM Scout.
- You have a GPX-Track from a website and can't make it public in the OSM, course you doesn't have the license? Import it to OSM Scout and it will paint it for you.

![Context Menu](/OpenStreetMapScoutScreenshot2.png)


The Scout is based on the http://sourceforge.net/projects/osmclient/, a implementation of the SlippyMap. The Scout inherits from the Client the tile downloader and the tile cache mechanism.

If you don't know what "tiles" or "relations" are, please look into the FAQ
