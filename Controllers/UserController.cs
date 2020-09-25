using System;
using System.Text.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Resporter.Models;
using Resporter.Services;

namespace Resporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // api/user/auth?username={value}
        [HttpPost("auth")]
        public async Task<ActionResult<string>> GetToken([FromQuery] string username)
        {
            ClaimsIdentity identity = await GetIdentity(username);
            if (identity == null)
                return BadRequest(new { errorText = "Invalid username." });

            DateTime now = DateTime.UtcNow;
            
            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return JsonSerializer.Serialize(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(string username)
        {
            User user = await _userService.GetUser(username);
            if (user != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
            else
                return null;
        }

        // api/user?username={value}
        [HttpGet]
        public async Task<ActionResult<User>> GetUser([FromQuery] string username)
        {
            User user = await _userService.GetUser(username);

            if (user == null)
                return NotFound();
            else
                return user;
        }

        [Authorize]
        [HttpGet("friends")]
        public async Task<ActionResult<List<string>>> GetUserFriendsIds()
        {
            string username = User.Identity.Name;
            User user = await _userService.GetUser(username);

            if (user.FriendsCount == 0)
                return NoContent();
            else
            {
                List<string> friendsIds = new List<string>();
                for (int i = 0; i < user.FriendsCount; i++)
                    friendsIds.Add(user.GetFriendIdByIndex(i));
            
                return friendsIds;
            }
        }

        // api/user/events/visited
        [Authorize]
        [HttpGet("events/visited")]
        public async Task<ActionResult<List<string>>> GetUserVisitedSportEventsIds()
        {
            string currentUsername = User.Identity.Name;
            User user = await _userService.GetUser(currentUsername);

            if (user.VisitedSportEventsCount == 0)
                return NoContent();
            else
            {
                List<string> visitedSportEventsIds = new List<string>();

                for (int i = 0; i < user.VisitedSportEventsCount; i++)
                    visitedSportEventsIds.Add(user.GetVisitedSportEventIdByIndex(i));

                return visitedSportEventsIds;
            }
        }

        // api/user/events/upcoming
        [Authorize]
        [HttpGet("events/upcoming")]
        public async Task<ActionResult<List<string>>> GetUserUpcomingSportEventsIds()
        {
            string currentUsername = User.Identity.Name;
            User user = await _userService.GetUser(currentUsername);

            if (user.UpcomingSportEventsCount == 0)
                return NoContent();
            else
            {
                List<string> upcomingSportEventsIds = new List<string>();

                for (int i = 0; i < user.UpcomingSportEventsCount; i++)
                    upcomingSportEventsIds.Add(user.GetUpcomingSportEventIdByIndex(i));

                return upcomingSportEventsIds;
            }
        }

        //[HttpGet]
        //public ActionResult<List<User>> Get() => _userService.Get();

        //[HttpGet("{id:length(24)}", Name = "GetUser")]
        //public ActionResult<User> Get(string id)
        //{
        //    User user = _userService.GetUser(id);

        //    if (user == null)
        //        return NotFound();
        //    else
        //        return user;
        //}

        // api/user/create?username={value}
        [HttpPost("create")]
        public async Task<ActionResult> CreateNewUser([FromQuery] string username)
        {
            User user = new User(username);
            await _userService.AddNew(user);
            return Ok();   
        }

        // api/user/add_friend?username={value}
        [Authorize]
        [HttpPut("add_friend")]
        public async void AddFriend([FromQuery] string username)
        {
            string currentUsername = User.Identity.Name;
            User user = await _userService.GetUser(currentUsername);
            User userToFriend = await _userService.GetUser(username);

            user.AddFriend(userToFriend);

            _userService.Update(user.Username, user);
            _userService.Update(userToFriend.Username, userToFriend);
        }

        //[HttpPut("{id:length(24)}")]
        //public IActionResult Update(string id, User userIn)
        //{
        //    User user = _userService.GetUser(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _userService.Update(id, userIn);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public IActionResult Delete(string id)
        //{
        //    User user = _userService.GetUser(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _userService.Remove(user.Username);

        //    return NoContent();
        //}
    }
}