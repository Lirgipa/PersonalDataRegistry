using Microsoft.AspNetCore.Mvc;

namespace PersonalDataDirectory.Controllers;

public partial class PersonController
{
    [HttpGet("relationship-report")]
    public async Task<IActionResult> GetRelationshipReport()
    {
        var report = await _personReportService.GetRelationshipReportAsync();
        return Ok(report);
    }
}