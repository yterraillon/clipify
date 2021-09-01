using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Infrastructure.Spotify.Webapi.Models
{
    /// <summary>
    /// https://developer.spotify.com/documentation/web-api/reference/#object-pagingobject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingObject<T>
    {
        /// <summary>
        /// A link to the Web API endpoint returning the full result of the request
        /// </summary>
        [JsonProperty("href")]
        public string Href { get; set; } = string.Empty;

        /// <summary>
        /// The requested content
        /// </summary>
        [JsonProperty("items")]
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        /// <summary>
        /// The maximum number of items in the response (as set in the query or by default).
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// URL to the next page of items. ( null if none)
        /// </summary>
        [JsonProperty("next")]
        public string? Next { get; set; } = string.Empty;

        /// <summary>
        /// The offset of the items returned (as set in the query or by default)
        /// </summary>
        [JsonProperty("offset")]
        public int Offset { get; set; }

        /// <summary>
        /// URL to the previous page of items. ( null if none)
        /// </summary>
        [JsonProperty("previous")]
        public string Previous { get; set; } = string.Empty;

        /// <summary>
        /// The total number of items available to return.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}