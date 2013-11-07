using System;
using SolrNet.Attributes;

namespace SolrDemo.Models
{
    public class User
    {
        [SolrUniqueKey("id")]
        public int Id { get; set; }

        [SolrField("creation_date")]
        public DateTimeOffset CreationDate { get; set; }

        [SolrField("reputation")]
        public int Reputation { get; set; }

        [SolrField("display_name")]
        public string DisplayName { get; set; }

        [SolrField("last_access_date")]
        public DateTimeOffset LastAccessDate { get; set; }

        [SolrField("location")]
        public string Location { get; set;}

        [SolrField("about_me")]
        public string AboutMe { get; set; }

        [SolrField("upvotes")]
        public int UpVotes { get; set; }

        [SolrField("downvotes")]
        public int DownVotes { get; set; }

        [SolrField("views")]
        public int Views { get; set; }

        [SolrField("age")]
        public int Age { get; set; }

        [SolrField("website_url")]
        public string WebsiteUrl { get; set; }


        public User ()
        {
        }
    }
}

