using Grpc.Core;
using Template.gRPC.Service;
using static Template.gRPC.Service.BlogService;

namespace Template.gRPC.Service.Services
{
    public class BlogService : BlogServiceBase
    {
        public override Task<CreateBlogResponse> CreateBlog(CreateBlogRequest request, ServerCallContext context)
        {
            var blog = request.Blog;

            return Task.FromResult(new CreateBlogResponse()
            {
                Blog = blog
            });
        }

        public override async Task<ReadBlogResponse> ReadBlog(ReadBlogRequest request, ServerCallContext context)
        {
            var blogId = request.BlogId;

            if (1 == 2)
                throw new RpcException(new Status(StatusCode.NotFound, "The blog id " + blogId + " wasn't find"));

            Blog blog = new Blog()
            {
                AuthorId = "1",
                Title = "Titulo prueba",
                Content = "Contenido prueba"
            };

            return await Task.FromResult(new ReadBlogResponse() { Blog = blog });
        }

        public override async Task<UpdateBlogResponse> UpdateBlog(UpdateBlogRequest request, ServerCallContext context)
        {
            var blogId = request.Blog.Id;


            if (1 == 2)
                throw new RpcException(new Status(StatusCode.NotFound, "The blog id " + blogId + " wasn't find"));

            var blog = new Blog()
            {
                AuthorId = "2",
                Title = "Titulo prueba 2",
                Content = "Contenido prueba 2"
            };

            blog.Id = blogId;

            return await Task.FromResult(new UpdateBlogResponse() { Blog = blog });
        }

        public override async Task<DeleteBlogRepsponse> DeleteBlog(DeleteBlogRequest request, ServerCallContext context)
        {
            var blogId = request.BlogId;

            if (1 == 0)
                throw new RpcException(new Status(StatusCode.NotFound, "The blog with id " + blogId + " wasn't find"));

            return await Task.FromResult(new DeleteBlogRepsponse() { BlogId = blogId });
        }

        public override async Task ListBlog(ListBlogRequest request, IServerStreamWriter<ListBlogResponse> responseStream, ServerCallContext context)
        {
            await responseStream.WriteAsync(new ListBlogResponse()
            {
                Blog = new Blog()
                {
                    Id = "5",
                    AuthorId = "AuthorId prueba 5",
                    Content = "Contenido prueba 5",
                    Title = "Titulo prueba 5"
                }
            });
        }
    }
}
