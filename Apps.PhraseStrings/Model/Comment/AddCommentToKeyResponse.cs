using Apps.PhraseStrings.Model.Job;
using Apps.PhraseStrings.Model.User;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.PhraseStrings.Model.Comment
{
    public class AddCommentToKeyResponse
    {
        [JsonProperty("id")]
        [Display("Comment ID")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("has_replies")]
        [Display("Has replies")]
        public bool HasReplies { get; set; }

        [JsonProperty("user")]
        public UserInfo User { get; set; } = new();

        [JsonProperty("created_at")]
        [Display("Created at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        [Display("Updated at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("locales")]
        public List<LocaleInfo> Locales { get; set; } = [];
    }

}
