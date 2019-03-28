# Video Indexer API call (Azure Function)

## Description

Azure function that calls the Video Indexer API to get any data from all the videos in an account.

When you start the Azure function, you can call the API to get the selected data (example result in *result.json*).

In this sample, for each video in a given Video Indexer account, these fields are extracted from the video index:
- ID
- Name
- Keywords
- Known faces

### Example console output

![Example console output](https://github.com/Kagigz/video-indexer-api/blob/master/videoindexeroutput.jpg)

## How to use

- Clone or download this repo
- In *function1.cs*, replace the values for the Subscription key and Account ID
- Build the project

If you want to customize the returned fields, you can take a look at the helper classes and choose which field you want to retrieve from the index.


