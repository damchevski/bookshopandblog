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
    public class CommentService: ICommentService
    {
        private readonly ICommentRepository _CommentRepository;
        private readonly IUserRepository userRepository;
        private readonly ILogger<CommentService> logger;

        public CommentService(ICommentRepository CommentRepository, ILogger<CommentService> logger, IUserRepository userRepository)
        {
            this._CommentRepository = CommentRepository;
            this.logger = logger;
            this.userRepository = userRepository;
        }

        public async Task<Comment> AddComment(Comment Comment)
        {
            if (Comment.Id == null ||
                Comment.Content == null)
                return null;

            return await this._CommentRepository.AddComment(Comment);
        }

        public async Task<Comment> DeleteComment(Guid? id)
        {
            if (id == null)
                return null;

            return await this._CommentRepository.DeleteComment(id.Value);
        }

        public async Task<Comment> EditComment(Comment Comment)
        {
            if (Comment.Id == null)
                return null;

            return await this._CommentRepository.EditComment(Comment);
        }

        //todo getAllForPost

        public async Task<List<Comment>> GetAllCommentsForPost()
        {
            var result = new List<Comment>();

            return result;
        }

        public async Task<Comment> GetComment(Guid? id)
        {
            if (id == null)
                return null;

            return await this._CommentRepository.GetComment(id.Value);
        }
    }
}
