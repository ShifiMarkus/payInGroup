//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Drawing;

//namespace payInGroup.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PostImageController : ControllerBase
//    {
//        [HttpPost]
//        public async Task<ActionResult<Image>> PostImage([FromForm] PostImageModel model)
//        {
//            if (_context.Images == null)
//            {
//                return Problem("Entity set 'MyDBContext.Images'  is null.");
//            }
//            if (model.File != null)
//            {
//                var uploadedFileLink = await _googleStorageManager.UploadFileAsync(model.File);
//                var image = new Image() { Url = uploadedFileLink };
//                _context.Images.Add(image);
//                await _context.SaveChangesAsync();
//                return CreatedAtAction("GetImage", new { id = image.Id }, image);
//            }
//            return NotFound();
//        }
//    }

//}
