using Core.Contracts;
using Repository.Data;
using Repository.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Core.Services;
using System.Security.Claims;

namespace API_Capas.Controllers
{
    [Route("v2/[Controller]")]
    [ApiController]
    public class PeopleController: ControllerBase
    {
        public IPeopleCore _peopleCore;

        public PeopleController(IPeopleCore peopleCore)
        {
            this._peopleCore = peopleCore;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll([FromHeader] string Host, [FromHeader(Name = "X-OwnerService")] string OwnerService)
        {
            try
            {
                // Way for catching claims of token
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                { 
                    IEnumerable<Claim> claims = identity.Claims;
                }

                List<User> users = this._peopleCore.GetAll();
                if (users.Count > 0)
                {
                    return Ok(new { ok = true, data = users, host = Host, OwnerService });
                }
                else
                {
                    return BadRequest("There aren't registered users");
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
          
        }

        [HttpPost("store")]
        public IActionResult Store(StorePeopleDTO peopleDTO)
        {
            try
            {

                if(String.IsNullOrEmpty(peopleDTO.DESCRIPTION) || peopleDTO.ID <= 0) 
                {
                    return BadRequest(new { ok = false, message = "Miss parameters" });
                }

                var response = this._peopleCore.Save(peopleDTO);

                if (response)
                {
                    return Ok(new { ok = true });
                }
                else
                {
                    return BadRequest("It was an error saving record");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(StorePeopleDTO peopleDTO)
        {
            try
            {
                if (String.IsNullOrEmpty(peopleDTO.DESCRIPTION) || String.IsNullOrEmpty(peopleDTO.PASSWORD))
                {
                    return BadRequest("Miss parameters");
                }

                StorePeopleDTO find = this._peopleCore.Find(peopleDTO.DESCRIPTION, peopleDTO.PASSWORD);

                if (find == null)
                {
                    return BadRequest(new { Error = "User or password wrong" });
                }

                var Token = JwtToken.Create(peopleDTO);

                return Ok(new { Token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        
    }
}
