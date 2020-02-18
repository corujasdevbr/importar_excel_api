using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile arquivo)
        {

            if (arquivo == null || arquivo.Length == 0)
                return BadRequest();

            using (var memoryStream = new MemoryStream())
            {
                await arquivo.CopyToAsync(memoryStream).ConfigureAwait(false);

                using (var package = new ExcelPackage(memoryStream))
                {
                    for (int i = 1; i <= package.Workbook.Worksheets.Count; i++)
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int totalRows = worksheet.Dimension.End.Column;
                        int totalCollumns = worksheet.Dimension.End.Row;
                        for (int j = 1; j <= totalRows; j++)
                        {
                            for (int k = 1; k <= totalCollumns; k++)
                            {
                                Console.WriteLine(package.Workbook.Worksheets[i].Cells[j, k].Value.ToString());
                            }
                        }
                    }
                }
            }


            return Ok();
        }
    }
}
