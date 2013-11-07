using System;

namespace SolrDemo.Models
{
    public class Badge
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string BadgeName { get; private set; }
        public DateTimeOffset AwardedAt { get; private set; }

        public Badge (int id, int userId, string name, DateTimeOffset awardedAt)
        {
            Id = id;
            UserId = userId;
            BadgeName = name;
            AwardedAt = awardedAt;
        }
    }
}

