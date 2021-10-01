using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BSB.Data;
using BSB.Data.Entity;
using BSB.Service.Interface;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BSB.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostsController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        // GET: Posts
        public async Task<IActionResult> Index(string? topic)
        {
            ViewData["Topics"] = await _postService.GetTopics();
            return View(await _postService.GetAllPosts(topic));
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var post = await this._postService.GetPost(id);

            if (post == null) {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(string? commentContent, Guid postId)
        {
             
            if (ModelState.IsValid) {
                    if (commentContent != null && !commentContent.Trim().Equals(""))
                    {

                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        var res = await this._postService.CommentOnPost(commentContent, postId, userId);


                        if (res == null)
                            throw new Exception("All Fields Required");
                    }
                return RedirectToAction("Details", new { id = postId });
            }
            return View(postId);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,Topic")] Post post)
        {
            if (ModelState.IsValid) {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                post.Id = Guid.NewGuid();

                Post res = await this._postService.AddPost(post, userId);

                if (res == null)
                    throw new Exception("All Fields Required");

                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) {
                return NotFound();
            }

            var post = await this._postService.GetPost(id);
            if (post == null) {
                return NotFound();
            }
            return View(post);

        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Content,Likes,Topic,ByUserId,CommentsInPost,Id")] Post post)
        {
            if (id != post.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    var res = await this._postService.EditPost(post);

                    if (res == null)
                        return NotFound();

                }
                catch (DbUpdateConcurrencyException) {
                    if (await this._postService.GetPost(post.Id) == null) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) {
                return NotFound();
            }

            var post = await this._postService.GetPost(id);

            if (post == null) {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var res = await this._postService.DeletePost(id);

            if (res == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Like(Guid postId, string userEmail)
        {
            if (postId == null) {
                return NotFound();
            }

            var post = await this._postService.Like(postId, userEmail);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Unike(Guid postId, string userEmail)
        {
            if (postId == null) {
                return NotFound();
            }

            var post = await this._postService.Unlike(postId, userEmail);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteComment(Guid? id, Guid? postid)
        {
            if (id == null || postid == null)
            {
                return NotFound();
            }

            var result = await this._commentService.DeleteComment(id);

            if (result != null)
            {

                return View("Details",await this._postService.GetPost(postid));
            }

            return RedirectToAction("Index");
        }

    }
}
