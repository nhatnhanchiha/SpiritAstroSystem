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
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IPostService
    {
        Task<PageResult<PostModel>> GetPosts(PostModel postFilter, string[] fields, string sort, int page, int limit);
        Task<PageResult<PostModel>> GetPostsForAdmin(PostModel postFilter, string[] fields, string sort, int page, int limit);
        Task<PostModel> GetPostById(long postId);
        Task<long> CreatePost(CreatePostRequest createPostRequest, long astrologerId);
        Task UpdatePost(long userId, long postId, UpdatePostRequest updatePostRequest);
        Task DeletePost(long userId, long postId);
        Task Approve(long postId);
    }

    public partial class PostService
    {
        private const string CacheKey = "POST"; 
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 20;
        private const int LimitPaging = 20;
        private readonly IAccountService _accountService;
        private readonly IRedisService _redisService;

        public PostService(IUnitOfWork unitOfWork, IPostRepository repository, IMapper mapper,
            IAccountService accountService, IRedisService redisService) : base(unitOfWork, repository)
        {
            _accountService = accountService;
            _redisService = redisService;
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<PageResult<PostModel>> GetPosts(PostModel postFilter, string[] fields, string sort, int page,
            int limit)
        {
            var listPost = await _redisService.GetFromRedis<IList<PostModel>>(CacheKey);
            if (listPost == null)
            {
                listPost = await Get().Where(p => p.DeletedAt == null).OrderByDescending(p => p.CreatedAt).Skip(0).Take(888).ProjectTo<PostModel>(_mapper).ToListAsync();
                //cache 5 phút thôi
                await _redisService.CacheToRedis(CacheKey, listPost, TimeSpan.FromMinutes(5));
            }

            var queryable = listPost.AsQueryable();
            int total;
            
            if (postFilter.ZodiacIds is { Count: > 0 })
            {
                queryable = queryable.Where(p => p.Zodiacs.Any(z => postFilter.ZodiacIds.Contains(z.Id!.Value)));
            }
            
            (total, queryable) = queryable
                .DynamicFilter(postFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }

            if (fields.Length > 0)
            {
                queryable = queryable.Select<PostModel>(PostModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<PostModel>());
            }

            return new PageResult<PostModel>
            {
                List = queryable.ToList(),
                Page = page,
                Limit = limit,
                Total = total
            };
        }

        public async Task<PageResult<PostModel>> GetPostsForAdmin(PostModel postFilter, string[] fields, string sort, int page, int limit)
        {
            var (total, queryable) = Get().Where(p => p.DeletedAt == null).ProjectTo<PostModel>(_mapper)
                .DynamicFilter(postFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }

            if (fields.Length > 0)
            {
                queryable = queryable.Select<PostModel>(PostModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<PostModel>());
            }

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
            var postModel = await Get().Where(p => p.Id == postId && p.DeletedAt == null).ProjectTo<PostModel>(_mapper).FirstOrDefaultAsync();
            if (postModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any post matches with id = {postId}");
            }

            return postModel;
        }

        public async Task<long> CreatePost(CreatePostRequest createPostRequest, long astrologerId)
        {
            var mapper = _mapper.CreateMapper();
            var post = mapper.Map<Post>(createPostRequest);

            post.AstrologerId = astrologerId;

            post.CreatedAt = DateTimeOffset.Now;
            post.UpdatedAt = DateTimeOffset.Now;

            await CreateAsyn(post);
            return post.Id;
        }

        public async Task UpdatePost(long userId, long postId, UpdatePostRequest updatePostRequest)
        {
            var postInDb = await Get().FirstOrDefaultAsync(fp => fp.Id == postId && fp.DeletedAt == null);
            if (postInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any post matches with id = {postId}");
            }
            


            var mapper = _mapper.CreateMapper();
            var postInRequest = mapper.Map<Post>(updatePostRequest);
            
            if (userId != 0 && userId != postInDb.AstrologerId)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "Insufficient permissions");
            }
            
            postInDb.Title = postInRequest.Title;
            postInDb.Content = postInRequest.Content;
            postInDb.CategoryId = postInRequest.CategoryId;
            postInDb.ImageUrl = postInRequest.ImageUrl;
            postInDb.UpdatedAt = DateTimeOffset.Now;

            await UpdateAsyn(postInDb);
        }

        public async Task DeletePost(long userId, long postId)
        {
            var postInDb = await Get().FirstOrDefaultAsync(fp => fp.Id == postId);
            
            if (postInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any post matches with id = {postId}");
            }


            if (userId != 0 && userId != postInDb.AstrologerId)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "Insufficient permissions");
            }

            postInDb.DeletedAt = DateTimeOffset.Now;

            await UpdateAsyn(postInDb);
        }

        public async Task Approve(long postId)
        {
            var post = await Get().Where(p => p.Id == postId && p.DeletedAt == null).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any post matches with id = {postId}");
            }

            post.IsApprove = true;

            await UpdateAsyn(post);
        }
    }
}