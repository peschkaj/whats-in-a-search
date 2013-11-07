using System;
using System.Collections.Generic;
using SolrDemo.Models;
using SolrNet.Attributes;

namespace SolrDemo
{
    public enum PostType
    {
        Question = 1,
        Answer,
        OrphanedTagWiki,
        TagWikiExcerpt,
        TagWiki,
        ModeratorNomination,
        WikiPlaceholder,
        PrivilegeWiki
    }

    // schema from http://meta.stackoverflow.com/questions/2677/database-schema-documentation-for-the-public-data-dump-and-data-explorer
    public class Post
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

        public int Id { get; set; }
        public PostType PostType { get; set; }
        public string PostTypeString { get{ return PostTypeLookup[PostType]; } }
        public int? AcceptedAnswerId { get; set; }
        public int? ParentId { get; set; }
        public DateTimeOffset CreationDate { get; set; }   
        public int Score { get; set; }
        public int ViewCount { get; set; }
        public string Body { get; set; }
        public int OwnerUserId { get; set; }
        public User Owner { get; set; }
        public string OwnerDisplayName { get; set; }
        public int LastEditorUserId { get; set; }
        public string LastEditorDisplayName { get; set; }
        public DateTimeOffset? LastEditDate { get; set; }
        public DateTimeOffset? LastActivityDate { get; set; }
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public int AnswerCount { get; set; }
        public int CommentCount { get; set; }
        public int FavoriteCount { get; set; }
        public DateTimeOffset? ClosedDate { get; set; }
        public DateTimeOffset? CommunityOwnedDate { get; set; }

        public Post ()
        {
        }

        public TransferPost ToTransferPost()
        {
            return new TransferPost(this, this.Owner);
        }
    }
}

