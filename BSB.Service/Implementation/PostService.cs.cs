using BSB.Data.Entity;
using BSB.Repository.Interface;
using BSB.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Implementation
{
    public class PostService: IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<PostService> logger;

        public PostService(IPostRepository postRepository, ILogger<PostService> logger, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            this._postRepository = postRepository;
            this.logger = logger;
            this._userRepository = userRepository;
            this._commentRepository = commentRepository;
        }

        public async Task<Post> AddPost(Post Post, string userId)
        {
            if (Post.Id == null ||
                Post.Content == null)
                return null;

            Post.ByUser = _userRepository.Get(userId);
            Post.ByUserId = userId;
            Post.Likes = 0;

            return await this._postRepository.AddPost(Post);
        }

        public async Task<CommentInPost> CommentOnPost(string commentContent, Guid postId, string userId)
        {
            if (commentContent == null ||
                postId == null ||
                userId == null)
                return null;

            var comment = new Comment {
                Id = Guid.NewGuid(),
                Content = commentContent,
                ByUser = _userRepository.Get(userId),
            };

            var res = new CommentInPost {
                CommentId = comment.Id,
                Comment = comment,
                PostId = postId,
                Post = await _postRepository.GetPost(postId)
            };

            _commentRepository.Insert(res);

            return res;
        }

        public async Task<Post> DeletePost(Guid? id)
        {
            if (id == null)
                return null;

            return await this._postRepository.DeletePost(id.Value);
        }

        public async Task<Post> EditPost(Post Post)
        {
            if (Post.Id == null)
                return null;

            return await this._postRepository.EditPost(Post);
        }

        public async Task<List<Post>> GetAllPosts(string? topic)
        {
            var result = new List<Post>();

            if (topic == null)
                result = await this._postRepository.GetAll();
            else
                result = await this._postRepository.GetAllForTopic(topic);

            return result;
        }

        public async Task<Post> GetPost(Guid? id)
        {
            if (id == null)
                return null;

            return await this._postRepository.GetPost(id.Value);
        }

        public async Task<List<string>> GetTopics()
        {
            return await this._postRepository.GetAllTopics();
        }

        public async Task<Post> Like(Guid postId)
        {
            var post = await _postRepository.GetPost(postId);
            //fix user being to like a post infinite times
            post.Likes += 1;

            return await this._postRepository.EditPost(post);
        }
    }
}
