using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using System.Net.Http;

namespace VideoIndexerAPICall
{
    #region Helper classes
    public class SocialObject
    {
        public bool likedByUser;
        public int likes;
        public int views;
    }

    public class VideoIndexerObject
    {
        public string accountId;
        public string id;
        public string partition;
        public string externalId;
        public string name;
        public string description;
        public string created;
        public string lastModified;
        public string lastIndexed;
        public string privacyMode;
        public string userName;
        public bool isOwned;
        public bool isBase;
        public string state;
        public string processingProgress;
        public float durationInSeconds;
        public string thumbnailVideoId;
        public string thumbnailId;
        public SocialObject social;
        public List<string> searchMatches;
        public string indexingPreset;
        public string streamingPreset;
        public string sourceLanguage;
    }

    public class VideoListObject
    {
        public List<VideoIndexerObject> results;
    }

    public class DurationObject
    {
        public string time;
        public float seconds;
    }

    public class AppearancesObject
    {
        public string startTime;
        public string endTime;
        public float startSeconds;
        public float endSeconds;
    }

    public class InstanceObject
    {
        public string adjustedStart;
        public string adjustedEnd;
        public string start;
        public string end;
        public string duration;
    }

    public class SpeakerTalkToListenRationObject
    {
        public int field;
    }

    public class SpeakerLongestMonologObject
    {
        public int field;
    }

    public class SpeakerNumberOfFragmentsObject
    {
        public int field;
    }

    public class SpeakerWordCountObject
    {
        public int field;
    }

    public class StatisticsObject
    {
        public int correspondenceCount;
        public SpeakerTalkToListenRationObject speakerTalkToListenRatio;
        public SpeakerLongestMonologObject speakerLongestMonolog;
        public SpeakerNumberOfFragmentsObject speakerNumberOfFragments;
        public SpeakerWordCountObject speakerWordCount;
    }

    public class AudioEffectObject
    {

    }

    public class SentimentObject
    {
        public string sentimentKey;
        public List<AppearancesObject> appearances;
        public float seenDurationRatio;
    }

    public class SentimentObject2
    {
        public int id;
        public float averageScore;
        public string sentimentType;
        public List<InstanceObject> instances;
    }

    public class EmotionObject
    {
        public string type;
        public List<AppearancesObject> appearances;
        public float seenDurationRatio;
    }

    public class EmotionObject2
    {
        public int id;
        public string type;
        public List<InstanceObject> instances;
    }

    public class FaceObject
    {
        public int id;
        public string videoId;
        public string referenceId;
        public string referenceType;
        public string knownPersonId;
        public float confidence;
        public string name;
        public string description;
        public string title;
        public string thumbnailId;
        public List<AppearancesObject> appearances;
        public float seenDuration;
        public float seenDurationRatio;
    }

    public class ThumbnailObject
    {
        public string id;
        public string fileName;
        public List<InstanceObject> instances;
    }

    public class FaceObject2
    {
        public int id;
        public string name;
        public float confidence;
        public string description;
        public string thumbnailId;
        public string referenceId;
        public string referenceType;
        public string title;
        public string imageUrl;
        public List<ThumbnailObject> thumbnails;
        public List<InstanceObject> instances;
    }

    public class KeywordObject
    {
        public int id;
        public string name;
        public List<AppearancesObject> appearances;
        public bool isTranscript;
    }

    public class KeywordObject2
    {
        public int id;
        public string text;
        public float confidence;
        public string language;
        public List<InstanceObject> instances;
    }


    public class LabelObject
    {
        public int id;
        public string name;
        public List<AppearancesObject> appearances;
    }

    public class LabelObject2
    {
        public int id;
        public string name;
        public string language;
        public List<InstanceObject> instances;
    }

    public class KeyFrameObject
    {
        public int id;
        public List<InstanceObject> instances;
    }

    public class ShotObject
    {
        public int id;
        public List<KeyFrameObject> keyFrames;
        public List<InstanceObject> instances;
    }

    public class BrandObject
    {
        /*public int id;
        public string name;
        public List<AppearancesObject> appearances;*/
    }

    public class TopicObject
    {
        public int id;
        public string name;
        public string referenceUrl;
        public string iptcName;
        public string iabName;
        public float confidence;
        public List<AppearancesObject> appearances;
    }

    public class TopicObject2
    {
        public int id;
        public string name;
        public string referenceId;
        public string referenceType;
        public string iptcName;
        public float confidence;
        public string iabName;
        public string language;
        public List<InstanceObject> instances;
    }

    public class TranscriptObject
    {
        public int id;
        public string text;
        public float confidence;
        public int speakerId;
        public string language;
        public List<InstanceObject> instances;
    }

    public class OCRObject
    {
        public int id;
        public string text;
        public float confidence;
        public int left;
        public int top;
        public int width;
        public int height;
        public string language;
        public List<InstanceObject> instances;
    }

    public class BlockObject
    {
        public int id;
        public List<InstanceObject> instances;
    }

    public class SpeakerObject
    {
        public int id;
        public string name;
        public List<InstanceObject> instances;
    }

    public class TextualContentModerationObject
    {
        public int id;
        public int bannedWordsCount;
        public float bannedWordsRatio;
        public List<InstanceObject> instances;
    }

    public class SummarizedInsightsObject
    {
        public string name;
        public string id;
        public string privacyMode;
        public DurationObject duration;
        public string thumbnailVideoId;
        public string thumbnailId;
        public List<FaceObject> faces;
        public List<KeywordObject> keywords;
        public List<SentimentObject> sentiments;
        public List<EmotionObject> emotions;
        public List<AudioEffectObject> audioEffects;
        public List<LabelObject> labels;
        public List<BrandObject> brands;
        public StatisticsObject statistics;
        public List<TopicObject> topics;
    }


    public class InsightsObject
    {
        public string version;
        public string duration;
        public string sourceLanguage;
        public string language;
        public List<TranscriptObject> transcript;
        public List<OCRObject> ocr;
        public List<KeywordObject2> keywords;
        public List<TopicObject2> topics;
        public List<FaceObject2> faces;
        public List<LabelObject2> labels;
        public List<ShotObject> shots;
        public List<SentimentObject2> sentiments;
        public List<EmotionObject2> emotions;
        public List<BlockObject> blocks;
        public List<SpeakerObject> speakers;
        public TextualContentModerationObject textualContentModeration;
        public StatisticsObject statistics;
        public float sourceLanguageConfidence;
    }

    public class VideoObject
    {
        public string accountId;
        public string id;
        public string state;
        public string moderationState;
        public string reviewState;
        public string privacyMode;
        public string processingProgress;
        public string failureCode;
        public string failureMessage;
        public string externalId;
        public string externalUrl;
        public string metadata;
        public InsightsObject insights;
        public string thumbnailId;
        public string publishedUrl;
        public string publishedProxyUrl;
        public string viewToken;
        public bool detectSourceLanguage;
        public string sourceLanguage;
        public string language;
        public string indexingPreset;
        public string linguisticModelId;
        public string personModelId;
        public bool isAdult;
    }

    public class RangeObject
    {
        public string start;
        public string end;
        public string duration;
    }

    public class VideoRangeObject
    {
        public string videoId;
        public RangeObject range;
    }

    public class VideoIndex
    {
        public string accountId;
        public string id;
        public string partition;
        public string name;
        public string description;
        public string userName;
        public string created;
        public string privacyMode;
        public string state;
        public bool isOwned;
        public bool isEditable;
        public bool isBase;
        public float durationInSeconds;
        public SummarizedInsightsObject summarizedInsights;
        public List<VideoObject> videos;
        public List<VideoRangeObject> videosRanges;
        //public SocialObject social;
    }

    public class VideoInfo
    {
        public string id;
        public string name;
        public List<string> keywords;
        public List<string> knownFaces;
        public string transcript;

        public VideoInfo()
        {
            id = "";
            name = "";
            keywords = new List<string>();
            knownFaces = new List<string>();
            transcript = "";
        }
    }

    #endregion


    public static class VideoIndexerAPICall
    {
        [FunctionName("VideoIndexerAPICall")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var subscriptionKey = "YOUR_SUBSCRIPTION_KEY";
            var location = "northeurope"; // Change region if needed
            var accountId = "YOUR_ACCOUNT_ID";


            // Gets all the videos in the account
            List<string> videoList = await GetVideoList(subscriptionKey, location, accountId);
            List<VideoIndex> videoIndexes = new List<VideoIndex>();
            foreach (string videoId in videoList)
            {
                VideoIndex videoIndex = await GetVideoIndex(videoId, subscriptionKey, location, accountId);
                videoIndexes.Add(videoIndex);
            }

            Console.WriteLine("\n\n");

            // Get only important info
            List<VideoInfo> videosInfo = new List<VideoInfo>();
            foreach (VideoIndex videoIndex in videoIndexes)
            {
                Console.WriteLine("Video info:\n");
                VideoInfo videoInfo = new VideoInfo();
                videoInfo.id = videoIndex.id;
                Console.WriteLine("ID: " + videoInfo.id);
                videoInfo.name = videoIndex.name;
                Console.WriteLine("Name: " + videoInfo.name);
                Console.WriteLine("Keywords:");
                foreach (KeywordObject keyword in videoIndex.summarizedInsights.keywords)
                {
                    videoInfo.keywords.Add(keyword.name);
                    Console.WriteLine(keyword.name);
                }
                Console.WriteLine("\nKnown faces:");
                foreach (FaceObject face in videoIndex.summarizedInsights.faces)
                {
                    if (!face.name.Contains("Unknown"))
                    {
                        videoInfo.knownFaces.Add(face.name);
                        Console.WriteLine(face.name);
                    }
                }
                videosInfo.Add(videoInfo);
                Console.WriteLine("\n\n");
            }

            return videosInfo != null
                ? (ActionResult)new OkObjectResult(videosInfo)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        static async Task<List<string>> GetVideoList(string subscriptionKey, string location, string accountId)
        {
            List<string> videoList = new List<string>();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Getting account access token
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["allowEdit"] = "false";
            var uri = "https://api.videoindexer.ai/auth/" + location + "/Accounts/" + accountId + "/AccessToken?" + queryString;
            var response = await client.GetAsync(uri);
            var contents = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Account Access Token: \n" + contents);

            // Getting video list
            var accessToken = HttpUtility.ParseQueryString(string.Empty);
            contents = contents.Replace("\"", "");
            accessToken["accessToken"] = contents;
            var uri2 = "https://api.videoindexer.ai/" + location + "/Accounts/" + accountId + "/Videos?" + accessToken;
            var responseList = await client.GetAsync(uri2);
            if (responseList.IsSuccessStatusCode)
            {
                string contentsList = await responseList.Content.ReadAsStringAsync();

                // Listing video IDs
                var resultList = JsonConvert.DeserializeObject<VideoListObject>(contentsList);
                Console.WriteLine("Videos list: \n" + contentsList);
                Console.WriteLine("IDs list:");
                for (var i = 0; i < resultList.results.Count; i++)
                {
                    videoList.Add(resultList.results[i].id);
                    Console.WriteLine("Video " + i + ": ID " + videoList[i]);
                }
                return videoList;
            }
            else
            {
                throw new Exception((int)responseList.StatusCode + "-" + responseList.StatusCode.ToString());
            }
        }

        // Gets the index of a video
        static async Task<VideoIndex> GetVideoIndex(string videoId, string subscriptionKey, string location, string accountId)
        {
            VideoIndex videoIndex = new VideoIndex();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Getting video access token
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["allowEdit"] = "false";
            var uri = "https://api.videoindexer.ai/auth/" + location + "/Accounts/" + accountId + "/Videos/" + videoId + "/AccessToken?" + queryString;
            var response = await client.GetAsync(uri);
            var contents = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Video Access Token: \n" + contents);

            // Getting video index
            var accessToken = HttpUtility.ParseQueryString(string.Empty);
            contents = contents.Replace("\"", "");
            accessToken["accessToken"] = contents;
            var uri2 = "https://api.videoindexer.ai/" + location + "/Accounts/" + accountId + "/Videos/" + videoId + "/Index?" + accessToken;
            var responseIndex = await client.GetAsync(uri2);
            if (responseIndex.IsSuccessStatusCode)
            {
                var contentsIndex = await responseIndex.Content.ReadAsStringAsync();

                // Parsing index into object
                videoIndex = JsonConvert.DeserializeObject<VideoIndex>(contentsIndex);
                return videoIndex;
            }
            else
            {
                throw new Exception((int)responseIndex.StatusCode + "-" + responseIndex.StatusCode.ToString());
            }
        }

        static string CreateTranscript(List<TranscriptObject> transcriptList)
        {
            string transcript = "";

            return transcript;
        }

    
}
}
