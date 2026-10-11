using CreditService.Application.DTOs;
using CreditService.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Api.Controllers;

[ApiController]
[Route("api/credit")]
public class CreditController : ControllerBase
{
    private ICreditAccountService _creditAccountService;
    public CreditController(ICreditAccountService creditAccountService)
    {
        _creditAccountService = creditAccountService;
    }
    [HttpGet]
    [Route("get-account-by-id/{id:long}")]
    public async Task<IActionResult> GetCreditAccount([FromRoute] long id, CancellationToken cancellationToken)
    {
        var creditAccount = await _creditAccountService.GetByIdAsync(id, cancellationToken);
        if (creditAccount is null)
        {
            return NotFound();
        }
        return Ok(creditAccount);
    }
    [HttpPost]
    [Route("create-account")]
    public async Task<IActionResult> CreateCreditAccount([FromBody] CreateCreditAccountRequest request, CancellationToken cancellationToken)
    {
        var creditAccount = await _creditAccountService.CreateAsync(request,cancellationToken);
        return Ok(new { creditAccount , message="Credit account created successfully" });
    }
    [HttpGet]
    [Route("get-accounts-by-user-id/{userId:long}")]
    public async Task<IActionResult> GetCreditAccountsByUserId([FromRoute] long userId, CancellationToken cancellationToken)
    {
        var creditAccounts = await _creditAccountService.GetByUserIdAsync(userId, cancellationToken);
        return Ok(creditAccounts);
    }
    [HttpPut]
    [Route("update-account/{id:long}")]
    public async Task<IActionResult> UpdateCreditAccount([FromRoute] long id, [FromBody] UpdateCreditAccountRequest request, CancellationToken cancellationToken)
    {
        var creditAccount = await _creditAccountService.UpdateAsync(id, request, cancellationToken);
        if (creditAccount is null)
        {
            return NotFound();
        }
        return Ok(creditAccount);
    }
    [HttpDelete]
    [Route("close-account/{id:long}")]
    public async Task<IActionResult> CloseCreditAccount([FromRoute] long id, CancellationToken cancellationToken)
    {
        var result = await _creditAccountService.CloseAsync(id, cancellationToken);
        if (!result)
        {
            return NotFound();
        }
        return Ok(new { message = "Credit account closed successfully" });
    }
}
