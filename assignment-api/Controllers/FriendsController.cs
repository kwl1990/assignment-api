using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using assignment_api.Model;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;

namespace assignment_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly context _context;

        public FriendsController(context context)
        {
            _context = context;
        }

        //Create/Edit
        [HttpPost]
        public JsonResult CreateEdit(friends friend)
        {
            if (friend.Id == 0)
            {
                if (friend.Nickname == "")
                {
                    return new JsonResult(new { Status = "Failed", Message = "Nickname cannot be empty !"});
                }
                friend.LastUpdatedAt = DateTime.UtcNow;
                friend.CreatedAt =  DateTime.UtcNow;
                _context.Friends.Add(friend);
                _context.SaveChanges();
            }
            else {
                var friendDS = _context.Friends.Find(friend.Id);

                if (friendDS == null)
                {
                    return new JsonResult(NotFound());
                }

                if (friend.Nickname == "")
                {
                    return new JsonResult(new { Status = "Failed", Message = "Nickname cannot be empty !" });
                }

                
                friendDS.Nickname = friend.Nickname;
                friendDS.Age = friend.Age;
                friendDS.LastUpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();
            }

            return new JsonResult(Ok(friend));
        }

        // Get
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Friends.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
                   
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Friends.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Friends.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok());
        }

        [HttpGet]
        public JsonResult GetAll(string? search)
        {

            if (search == "" || search.IsNullOrEmpty()) {
                var result = _context.Friends.ToList();
                return new JsonResult(Ok(result));
            }
            else
            {
                var result = _context.Friends.Where(x => x.Nickname.Contains(search)).ToList();
                return new JsonResult(Ok(result));
            }

        }
    }
}
