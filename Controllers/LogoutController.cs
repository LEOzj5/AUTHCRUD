using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LogoutController : ControllerBase
{
    private readonly ILogger<LogoutController> _logger;
    private readonly DatabaseContext _databaseContext;

    public LogoutController(ILogger<LogoutController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }


 [HttpGet] 
    public IActionResult logout(User user, string state) 
    {
       try
            {
                var _user = _databaseContext.Users.SingleOrDefault(o => o.Username == user.Username && o.Password == user.Password);
                if(state == "logout"&&_user.Username == user.Username)
                {
                    return Ok(new {logoutuser = user.Username, message = "logout successfully"});
                }
                else
                {
                    return Ok(new {message= "logout fail"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {result = ex.Message, message= "fail"});
            }
    }

}