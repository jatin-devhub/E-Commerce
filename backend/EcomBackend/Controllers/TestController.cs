using EcomBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _db;
    public TestController(AppDbContext db) => _db = db;

    [HttpGet("db")]
    public async Task<IActionResult> Db() => Ok(new { Categories = await _db.Categories.CountAsync() });
}