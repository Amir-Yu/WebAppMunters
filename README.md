
# GIPHY WebAPI proxy service

This WebAPI expose two endpoints:

1. **Search url:**

 **https://{Your-Server-IP:Port}/Giphy/search** 

Required parameters:

 **searchVal**- the string value used to query the Gipty services


2. **Trending url:**

 **https://{Your-Server-IP:Port}/Giphy/trending** 

**Optional parameters:**

You can add results limiter to each off the queries in order to limit the amount of returned result values

 \*\* If NOT specified in the url the default returned results will be 25 \*\*

**Example url's:**

- This url will send the &quot;queen&quot; search query to Gipty service on local host server and will limit the search results to the top 5.

_https://localhost:44348/Giphy/search?searchVal=queen&amp;resLimit=5_

- This url will query the Gipty service for the top 10 current trending results

_https://localhost:44348/Giphy/trending?reslimit=10_
### Use ```appsettings.json``` file to define your settings
```
{
  "Logging": {
    "LogLevel": {
	// ....
    }
  },
  "AllowedHosts": "*",
  "ApiBaseURL": "https://api.giphy.com/v1/gifs/",
  "ApiKey": "Your-Giphy-API-Key", 
  // <-------------- don’t forget to set your Giphy API key
  "CacheExpirationInSeconds": 3600 
  // <-------------- define a cache expiry of 1 hour (3600 sec)
}

```
