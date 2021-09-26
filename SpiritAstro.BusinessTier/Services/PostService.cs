using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.Post;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Post;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SpiritAstro.BusinessTier.Commons.Utils;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IPostService
    {
        Task<PageResult<PostModel>> GetPosts(PostModel postFilter, int page, int limit);
        Task<PostModel> GetPostById(long postId);
        Task<long> CreatePost(CreatePostRequest createPostRequest);
        Task UpdatePost(long postId, UpdatePostRequest updatePostRequest);

        Task DeletePost(long postId);
    }
    public partial class PostService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 20;
        private const int LimitPaging = 20;
        public PostService(IUnitOfWork unitOfWork, IPostRepository repository, IMapper mapper) : base(unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<PageResult<PostModel>> GetPosts(PostModel postFilter, int page, int limit)
        {
            var (total, queryable) = Get().Where(p => p.DeletedAt == null).ProjectTo<PostModel>(_mapper)
                .DynamicFilter(postFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);

            return new PageResult<PostModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total
            };
        }

        public async Task<PostModel> GetPostById(long postId)
        {
            var postModel = await Get().Where(p => p.Id == postId).ProjectTo<PostModel>(_mapper).FirstOrDefaultAsync();
            if (postModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound, $"Cannot find any post matches with id = {postId}");
            }
            return postModel;
        }

        public async Task<long> CreatePost(CreatePostRequest createPostRequest)
        {
            var mapper = _mapper.CreateMapper();
            var post = mapper.Map<Post>(createPostRequest);
            
            post.CreatedAt = DateTimeOffset.Now;
            post.UpdatedAt = DateTimeOffset.Now;
            
            await CreateAsyn(post);
            return post.Id;
        }

        public async Task UpdatePost(long postId, UpdatePostRequest updatePostRequest)
        {
            var postInDb = await Get().FirstOrDefaultAsync(fp => fp.Id == postId);
            if (postInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound, $"Cannot find any post matches with id = {postId}");
            }

            var mapper = _mapper.CreateMapper();
            var postInRequest = mapper.Map<Post>(updatePostRequest);

            postInDb.Title = postInRequest.Title;
            postInDb.Content = postInRequest.Content;
            postInDb.CategoryId = postInRequest.CategoryId;
            postInDb.UpdatedAt = DateTimeOffset.Now;

            await UpdateAsyn(postInDb);
        }

        public async Task DeletePost(long postId)
        {
            var postInDb = await Get().FirstOrDefaultAsync(fp => fp.Id == postId);
            if (postInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any post matches with id = {postId}");
            }

            postInDb.DeletedAt = DateTimeOffset.Now;

            await UpdateAsyn(postInDb);
        }
    }
}
