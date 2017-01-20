using System.Threading.Tasks;
using System.Web.Http;
using Xm.Trial.Models;
using Xm.Trial.Models.Data;

namespace Xm.Trial.Controllers.Api
{
    public class LikeController : ApiController
    {
        private readonly DataContext _db = new DataContext();

        [HttpPost]
        public async Task<IHttpActionResult> Like(int id)
        {
            var post = await _db.Posts.FindAsync(id);

            if (post == null)
                return NotFound();

            post.Likes++;

            await _db.SaveChangesAsync();

            var viewModel = new LikeViewModel
                            {
                                Id = post.Id,
                                Count = post.Likes
                            };

            return Ok(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}