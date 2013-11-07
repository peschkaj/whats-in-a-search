using System;
using System.Linq;
using System.Collections.Generic;
using SolrDemo.Models;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using SolrDemo.Extensions;
using SolrNet;
using Microsoft.Practices.ServiceLocation;
using SolrNet.Impl;
using SolrNet.DSL;

namespace SolrDemo
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            const string basePath = "/Users/jeremiah/dba.stackexchange.com";
            Startup.Init<TransferPost>("http://localhost:8983/solr/dba.stackexchange.com");

            Console.WriteLine("Reading an XML");
            Console.WriteLine("  Loading users...");
            var users = ParseFile<User>(basePath, "Users.xml", ParseUser)
                    .Select(u => new { user_id = u.Id, user_value = u})
                    .Distinct()
                    .ToDictionary(item => item.user_id, item => item.user_value);

            Console.WriteLine("  Loading posts...");
            var posts = ParseFile<Post>(basePath, "Posts.xml", ParsePost);

            Console.WriteLine("  Adding users to posts...");
            Parallel.ForEach(posts, p => {
                if (users.ContainsKey(p.OwnerUserId))
                    p.Owner = users[p.OwnerUserId];

            });

            Console.WriteLine("  Adding posts to Solr...");
            LoadSolr(posts);

            Console.WriteLine("Done!");
        }

        private static IEnumerable<T> ParseFile<T>(string basePath, string file, Func<XmlReader, T> action)
        {
            var fileName = string.Format("{0}{1}{2}", basePath, System.IO.Path.DirectorySeparatorChar, file);

            var items = new List<T>();

            using (var reader = XmlReader.Create(fileName))
            {

                reader.MoveToContent();

                while (reader.Read())
                {
                    if (!(reader.NodeType == XmlNodeType.Element
                        && reader.Name == "row"))
                        continue;

                    if (!reader.HasAttributes)
                        continue;

                    items.Add(action(reader));
                }
            }

            return items;
        }
        
        private static User ParseUser(XmlReader reader)
        {
            var user = new User();

            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                case "Id":
                    user.Id = int.Parse(reader.Value);
                    break;
                case "Reputation":
                    user.Reputation = int.Parse(reader.Value);
                    break;
                case "CreationDate":
                    user.CreationDate = DateTimeOffset.Parse(reader.Value);
                    break;
                case "DisplayName":
                    user.DisplayName = reader.Value;
                    break;
                case "LastAccessDate":
                    user.LastAccessDate = DateTimeOffset.Parse(reader.Value);
                    break;
                case "WebsiteUrl":
                    user.WebsiteUrl = reader.Value;
                    break;
                case "Location":
                    user.Location = reader.Value;
                    break;
                case "AboutMe":
                    user.AboutMe = reader.Value;
                    break;
                case "Views":
                    user.Views = int.Parse(reader.Value);
                    break;
                case "UpVotes":
                    user.UpVotes = int.Parse(reader.Value);
                    break;
                case "DownVotes":
                    user.DownVotes = int.Parse(reader.Value);
                    break;
                case "EmailHash":
                    break;
                case "Age":
                    user.Age = int.Parse(reader.Value);
                    break;
                default:
                    break;
                }
            }

            return user;
        }

        private static Post ParsePost(XmlReader reader)
        {
            var post = new Post();

            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name) {
                case "Id":
                    post.Id = int.Parse(reader.Value);
                    break;
                case "PostTypeId":
                    post.PostType = (PostType)int.Parse(reader.Value);
                    break;
                case "AcceptedAnswerId":
                    post.AcceptedAnswerId = int.Parse(reader.Value);
                    break;
                case "ParentID":
                    post.ParentId = int.Parse(reader.Value);
                    break;
                case "CreationDate":
                    post.CreationDate = DateTimeOffset.Parse(reader.Value);
                    break;
                case "Score":
                    post.Score = int.Parse(reader.Value);
                    break;
                case "ViewCount":
                    post.ViewCount = int.Parse(reader.Value);
                    break;
                case "Body":
                    post.Body = reader.Value;
                    break;
                case "OwnerUserId": 
                    post.OwnerUserId = int.Parse(reader.Value);
                    break;
                case "OwnerDisplayName":
                    post.OwnerDisplayName = reader.Value;
                    break;
                case "LastEditorUserId":
                    post.LastEditorUserId = int.Parse(reader.Value);
                    break;
                case "LastEditorDisplayName":
                    post.LastEditorDisplayName = reader.Value;
                    break;
                case "LastEditDate":
                    post.LastEditDate = DateTimeOffset.Parse(reader.Value);
                    break;
                case "LastActivityDate":
                    post.LastActivityDate = DateTimeOffset.Parse(reader.Value);
                    break;
                case "Title":
                    post.Title = reader.Value;
                    break;
                case "Tags":
                    post.Tags = ParseTags(reader.Value);
                    break;
                case "AnswerCount":
                    post.AnswerCount = int.Parse(reader.Value);
                    break;
                case "CommentCount":
                    post.CommentCount = int.Parse(reader.Value);
                    break;
                case "FavoriteCount":
                    post.FavoriteCount = int.Parse(reader.Value);
                    break;
                case "ClosedDate":
                    post.ClosedDate = DateTimeOffset.Parse(reader.Value);
                    break;
                case "CommunityOwnedDate":
                    post.CommunityOwnedDate = DateTimeOffset.Parse(reader.Value);
                    break;
                default:
                    break;
                }
            }

            return post;
        }

        private static List<string> ParseTags(string tags)
        {
            return tags.Replace("><", ",")
                       .Replace("<", "")
                       .Replace(">", "")
                       .Split(',')
                       .ToList();
        }

        private static void LoadSolr(IEnumerable<Post> posts)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<TransferPost>>();

            int counter = 0;

            foreach (var post in posts)
            {
                solr.Add(post.ToTransferPost());
                counter++;

                if (counter % 10 == 0)
                {
                    counter = 0;
                    solr.Commit();
                }
#if DEBUG
                Console.Write(".");
#endif
            }

            solr.Commit();
        }
    }
}
