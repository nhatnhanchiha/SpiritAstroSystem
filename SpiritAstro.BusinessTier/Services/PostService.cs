﻿using AutoMapper;
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
        Task<PostModel> GetPostById(long postId);
        Task<long> CreatePost(CreatePostRequest createPostRequest);
        Task UpdatePost(long postId, UpdatePostRequest updatePostRequest);

        Task DeletePost(long postId);
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
                listPost = await Get().Where(p => p.DeletedAt == null).ProjectTo<PostModel>(_mapper).ToListAsync();
                //cache 1 giờ thôi
                await _redisService.CacheToRedis(CacheKey, listPost, TimeSpan.FromHours(1));
            }
            var (total, queryable) = listPost.AsQueryable()
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

        public async Task<PostModel> GetPostById(long postId)
        {
            var postModel = await Get().Where(p => p.Id == postId).ProjectTo<PostModel>(_mapper).FirstOrDefaultAsync();
            if (postModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any post matches with id = {postId}");
            }

            return postModel;
        }

        public async Task<long> CreatePost(CreatePostRequest createPostRequest)
        {
            var mapper = _mapper.CreateMapper();
            var post = mapper.Map<Post>(createPostRequest);

            var astrologerId = _accountService.GetAstrologerId();
            post.AstrologerId = astrologerId;

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
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any post matches with id = {postId}");
            }

            var mapper = _mapper.CreateMapper();
            var postInRequest = mapper.Map<Post>(updatePostRequest);

            var astrologerId = _accountService.GetAstrologerId();

            if (astrologerId != postInDb.AstrologerId)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "Insufficient permissions");
            }

            postInDb.Title = postInRequest.Title;
            postInDb.Content = postInRequest.Content;
            postInDb.CategoryId = postInRequest.CategoryId;
            postInDb.UpdatedAt = DateTimeOffset.Now;

            await UpdateAsyn(postInDb);
        }

        public async Task DeletePost(long postId)
        {
            var postInDb = await Get().FirstOrDefaultAsync(fp => fp.Id == postId);

            var astrologerId = _accountService.GetAstrologerId();

            if (astrologerId != postInDb.AstrologerId)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "Insufficient permissions");
            }

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