using System;
using SolrNet.Attributes;
using System.Collections.Generic;

namespace SolrDemo.Models
{
    public class TransferPost
    {
        internal static Dictionary<PostType, string> PostTypeLookup = new Dictionary<PostType, string>
        {
            { PostType.Question, "Question" },
            { PostType.Answer, "Answer" },
            { PostType.OrphanedTagWiki, "Orphanted Tag Wiki" },
            { PostType.TagWikiExcerpt, "Tag Wiki Excerpt" },
            { PostType.TagWiki, "Tag Wiki" },
            { PostType.ModeratorNomination, "Moderator Nomination" },
            { PostType.WikiPlaceholder, "Wiki Placeholder" },
            { PostType.PrivilegeWiki, "Privilege Wiki" }
        };

        private const string _dateFormatString = "yyyy-MM-ddTHH:mm:ssZ";

        [SolrUniqueKey("id")]
        public int Id { get; set; }

        [SolrField("post_type")]
        public string PostTypeString { get; set; }

        [SolrField("accepted_answer_id")]
        public int? AcceptedAnswerId { get; set; }

        [SolrField("parent_id")]
        public int? ParentId { get; set; }

        [SolrField("creation_date")]
        public string CreationDate { get; set; }  

        [SolrField("score")]
        public int Score { get; set; }

        [SolrField("view_count")]
        public int ViewCount { get; set; }

        [SolrField("body")]
        public string Body { get; set; }

        [SolrField("last_editor_user_id")]
        public int LastEditorUserId { get; set; }

        [SolrField("last_editor_display_name")]
        public string LastEditorDisplayName { get; set; }

        [SolrField("last_edit_date")]
        public string LastEditDate { get; set; }

        [SolrField("last_activity_date")]
        public string LastActivityDate { get; set; }

        [SolrField("title")]
        public string Title { get; set; }

        [SolrField("tags")]
        public List<string> Tags { get; set; }

        [SolrField("answer_count")]
        public int AnswerCount { get; set; }

        [SolrField("comment_count")]
        public int CommentCount { get; set; }

        [SolrField("favorite_count")]
        public int FavoriteCount { get; set; }

        [SolrField("closed_date")]
        public string ClosedDate { get; set; }

        [SolrField("community_owned_date")]
        public string CommunityOwnedDate { get; set; }

        #region Owner fields
        [SolrField("owner_user_id")]
        public int OwnerUserId { get; set; }

        [SolrField("owner_creation_date")]
        public string OwnerCreationDate { get; set; }

        [SolrField("owner_reputation")]
        public int OwnerReputation { get; set; }

        [SolrField("owner_display_name")]
        public string OwnerDisplayName { get; set; }

        [SolrField("owner_last_access_date")]
        public string OwnerLastAccessDate { get; set; }

        [SolrField("owner_location")]
        public string OwnerLocation { get; set;}

        [SolrField("owner_about_me")]
        public string OwnerAboutMe { get; set; }

        [SolrField("owner_up_votes")]
        public int OwnerUpVotes { get; set; }

        [SolrField("owner_down_votes")]
        public int OwnerDownVotes { get; set; }

        [SolrField("owner_views")]
        public int OwnerViews { get; set; }

        [SolrField("owner_age")]
        public int OwnerAge { get; set; }

        [SolrField("owner_website_url")]
        public string OwnerWebsiteUrl { get; set; }
        #endregion

        public TransferPost (Post post, User owner)
        {
            Id = post.Id;
            PostTypeString = PostTypeLookup[post.PostType];
            AcceptedAnswerId = post.AcceptedAnswerId;
            ParentId = post.ParentId;
            CreationDate = post.CreationDate.ToString(_dateFormatString);
            Score = post.Score;
            ViewCount = post.ViewCount;
            Body = post.Body;
            LastEditorDisplayName = post.LastEditorDisplayName;
            LastEditDate = post.LastEditDate.HasValue ? post.LastEditDate.Value.ToString(_dateFormatString) : null;
            LastActivityDate = post.LastActivityDate.HasValue ? post.LastActivityDate.Value.ToString(_dateFormatString) : null;
            Title = post.Title;
            Tags = post.Tags;
            AnswerCount = post.AnswerCount;
            CommentCount = post.CommentCount;
            FavoriteCount = post.FavoriteCount;
            ClosedDate = post.ClosedDate.HasValue ? post.ClosedDate.Value.ToString(_dateFormatString) : null;
            CommunityOwnedDate = post.CommunityOwnedDate.HasValue ? post.CommunityOwnedDate.Value.ToString(_dateFormatString) : null;

            if (owner != null)
            {
                OwnerUserId = owner.Id;
                OwnerCreationDate = owner.CreationDate.ToString(_dateFormatString);
                OwnerReputation = owner.Reputation;
                OwnerDisplayName = owner.DisplayName;
                OwnerLastAccessDate = owner.LastAccessDate.ToString(_dateFormatString);
                OwnerLocation = owner.Location;
                OwnerAboutMe = owner.AboutMe;
                OwnerUpVotes = owner.UpVotes;
                OwnerDownVotes = owner.DownVotes;
                OwnerAge = owner.Age;
                OwnerWebsiteUrl = owner.WebsiteUrl;
            }
        }
    }
}

