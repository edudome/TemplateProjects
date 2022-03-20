using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Template.gRPC.Service;

namespace Template.gRPC.Client.ApplicationServices
{
    public class BlogApplicationService
    {
        BlogService.BlogServiceClient _client;

        public BlogApplicationService(BlogService.BlogServiceClient client)
        {
            _client = client;
        }

        public Blog CreateBlog()
        {
            try
            {
                var response = _client.CreateBlog(new CreateBlogRequest()
                {
                    Blog = new Blog()
                    {
                        AuthorId = "Clement",
                        Title = "New blog!",
                        Content = "Hello world, this is a new blog"
                    }
                });

                return response.Blog;
            }
            catch (RpcException ex)
            {
                throw new Exception(ex.Status.Detail);
            }
        }

        public string ReadBlog()
        {
            try
            {
                var response = _client.ReadBlog(new ReadBlogRequest()
                {
                    BlogId = "5dc06aeb807d5b450456803d"
                });

                return response.Blog.ToString();
            }
            catch (RpcException ex)
            {
                throw new Exception(ex.Status.Detail);
            }
        }

        public string UpdateBlog(Blog blog)
        {
            try
            {
                blog.AuthorId = "Updated author";
                blog.Title = "Updated title";
                blog.Content = "Updated content";

                var response = _client.UpdateBlog(new UpdateBlogRequest()
                {
                    Blog = blog
                });

                return response.Blog.ToString();
            }
            catch (RpcException ex)
            {
                throw new Exception(ex.Status.Detail);
            }
        }

        public string DeleteBlog(Blog blog)
        {
            try
            {
                var response = _client.DeleteBlog(new DeleteBlogRequest() { BlogId = blog.Id });

                return "The blog with id " + response.BlogId + " was deleted";
            }
            catch (RpcException ex)
            {
                throw new Exception(ex.Status.Detail);
            }
        }

        public async Task ListBlog()
        {
            try
            {
                var response = _client.ListBlog(new ListBlogRequest() { });

                while (await response.ResponseStream.MoveNext())
                {
                    Console.WriteLine(response.ResponseStream.Current.Blog.ToString());
                }
            }
            catch (RpcException ex)
            {
                throw new Exception(ex.Status.Detail);
            }
        }
    }
}
