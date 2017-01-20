using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xm.Trial.Models;
using Xm.Trial.Models.Data;

namespace Xm.Trial.Controllers
{
    public class BlogController : Controller
    {
        private readonly DataContext _db = new DataContext();

        public async Task<ActionResult> Index()
        {
            var posts = await _db.Posts
                                 .Select(p => new PostSnippetViewModel
                                              {
                                                  Id = p.Id,
                                                  Created = p.Created,
                                                  Title = p.Title,
                                                  Picture = p.Picture,
                                                  PictureCaption = p.PictureCaption,
                                                  Snippet = p.Snippet,
                                                  Author = p.Author
                                              })
                                 .ToArrayAsync();

            var viewModel = new BlogViewModel
                            {
                                Title = "Posts",
                                Posts = posts
                            };

            return View(viewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            var post = await _db.Posts
                                .Select(p => new PostViewModel
                                             {
                                                 Id = p.Id,
                                                 Created = p.Created,
                                                 Title = p.Title,
                                                 Picture = p.Picture,
                                                 PictureCaption = p.PictureCaption,
                                                 Text = p.Text,
                                                 Likes = p.Likes,
                                                 Author = p.Author,
                                                 Tags = p.Tags
                                             })
                                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return new HttpNotFoundResult();

            ViewBag.Title = post.Title;

            return View(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}