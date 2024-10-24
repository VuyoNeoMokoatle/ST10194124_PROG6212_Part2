using Xunit;
using Moq;
using PART2_PROG622.Controllers;
using PART2_PROG622.Data;
using PART2_PROG622.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ClaimControllerTests
{
    private readonly ClaimController _controller;
    private readonly ApplicationDbContext _context;

    public ClaimControllerTests()
    {
        // Set up in-memory database for testing
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(options);

        // Set up controller with mocks
        var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        _controller = new ClaimController(_context, webHostEnvironmentMock.Object);
    }

    [Fact]
    public async Task Submit_ValidClaim_ReturnsRedirectToManageClaims()
    {
        // Arrange
        var claim = new Claim { Title = "Test Claim", Amount = 100 };

        // Act
        var result = await _controller.Submit(claim, null) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ManageClaims", result.ActionName);
    }

    [Fact]
    public async Task ManageClaims_ReturnsViewWithClaims()
    {
        // Arrange
        _context.Claims.Add(new Claim { Title = "Claim 1" });
        _context.SaveChanges();

        // Act
        var result = await _controller.ManageClaims() as ViewResult;
        var model = result.Model as List<Claim>;

        // Assert
        Assert.NotNull(model);
        Assert.Single(model);
    }
}
