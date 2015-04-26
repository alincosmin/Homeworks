using System;
using System.Data.Entity;
using System.Linq;

namespace Commenter
{
    public class Poster : IPoster
    {
        public Poster()
        {
            var a = 1;
        }
        public void CleanUp()
        {
            using (var context = new Lab6Entities())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Comment");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE Post");
            }
        }

        public Post GetPostByTitle(string title)
        {
            using (var context = new Lab6Entities())
            {
                return
                    context.Posts.Include("Comment")
                        .FirstOrDefault(x => x.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public Post SubmitPost(Post post)
        {
            using (var context = new Lab6Entities())
            {
                context.Posts.Attach(post);
                context.Entry(post).State = post.Id == 0 ? EntityState.Added : EntityState.Modified;
                context.SaveChanges();
            }

            return post;
        }

        public Comment SubmitComment(Comment comment)
        {
            using (var context = new Lab6Entities())
            {
                context.Comments.Attach(comment);
                context.Entry(comment).State = comment.Id == 0 ? EntityState.Added : EntityState.Modified;
                context.SaveChanges();
            }

            return comment;
        }

        public void DeleteComment(Comment comment)
        {
            using (var context = new Lab6Entities())
            {
                context.Comments.Remove(comment);
                context.SaveChanges();
            }
        }
    }
}