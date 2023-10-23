using System.Web.Mvc;
using BusBookTicket.Application.MailKet.DTO.Request;
using BusBookTicket.Application.MailKet.Service;

namespace BusBookTicket.Application.MailKet.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[AllowAnonymous]
[Route("email")]
public class MailController : ControllerBase
{
    private readonly IMailService mailService;
    public MailController(IMailService mailService)
    {
        this.mailService = mailService;
    }
    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromForm]MailRequest request)
    {
        try
        {
            await mailService.SendEmailAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            throw;
        }
            
    }
}