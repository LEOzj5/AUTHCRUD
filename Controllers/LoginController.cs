using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly DatabaseContext _databaseContext;

    public LoginController(ILogger<LoginController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }


    [HttpPost] // post method
    public IActionResult login(User user)
    {
        try
        {
            var _user = _databaseContext.Users.SingleOrDefault(o => o.Username == user.Username && o.Password == user.Password);
            if(_user.Password == user.Password)
            {
                return Ok(new {message= "logged in successfully"});
            }
            else
            {
                return Ok(new {message= "Error Not Found"});
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message = "Login Failed"});
        }
    }



    [HttpPut("{state}")] //change password 
        public IActionResult ChangePW(User user, string state)
        {
            try
            {
                var _user = _databaseContext.Users.SingleOrDefault(o => o.Username == user.Username);
                if(_user != null&&state == "Invalid username or password")
                {
                    if(_user.Username == user.Username)
                    {

                    _user.Password = user.Password;

                    _databaseContext.Users.Update(_user);
                    _databaseContext.SaveChanges();
                    return Ok(new {message= "Changed password successfully"});

                    }
                    else
                    {
                    return Ok(new {message= "Failed ti changed"});
                    }
                }
                else
                {
                    return Ok(new {message= "Failed to changed"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {result = ex.Message, message= "fail"});
            }
        }

}