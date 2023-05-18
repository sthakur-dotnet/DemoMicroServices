using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using ProductServices.Command;
using ProductServices.Models;
using ProductServices.Query;
using ProductServices.Query.Handler;


namespace ProductServices.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;

    public ProductController(ILogger<ProductController> logger,IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-all-product")]
    public async Task<List<Product>> GetAllProduct()
    {
        return await _mediator.Send(new GetAllProductQuery());
    }

    [HttpGet]
    [Route("get-product/{id}")]
    public ObjectResult GetProductById(int id)
    {
        var product = _mediator.Send(new GetProductQueryById(id)).Result;
        return product is null ? new NotFoundObjectResult($"Product with id {id} not found") : new OkObjectResult(product);
    }
    
    [HttpPost]
    [Route("add-product")]
    public ObjectResult AddProduct(Product product)
    {
        var successProduct = _mediator.Send(new SaveProductCommand(product));
        return Ok(successProduct);
    }
    
    [HttpGet("download-excel")]
    public IActionResult DownloadExcel()
    {
        // Retrieve data from the database using the DbContext
        var data =  _mediator.Send(new GetAllProductQuery());;

        // Generate the Excel file
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Sheet1");
            worksheet.Cells.LoadFromCollection(data.Result, true);
            byte[] fileBytes = package.GetAsByteArray();

            // Return the Excel file for download
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
        }
    }

        [HttpGet("download-pdf")]
        public async Task<IActionResult> DownloadPdf()
        {
            // Retrieve data from the database using the DbContext
            var data = await _mediator.Send(new GetAllProductQuery() );

            // Create a new PDF document
            var doc = new PdfDocument();

            // Create a new PDF page
            var page = doc.AddPage();

            // Create a PDF graphics object
            var gfx = XGraphics.FromPdfPage(page);

            // Set the initial y-coordinate for drawing
            var y = 50;

            // Create a font
            var font = new XFont("Arial", 12, XFontStyle.Regular);

            // Draw the data onto the page
            foreach (var item in data)
            {
                gfx.DrawString(item.Name, font, XBrushes.Black, new XRect(50, y, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(item.Name, font, XBrushes.Black, new XRect(150, y, page.Width, page.Height), XStringFormats.TopLeft);
                // ...

                y += 20; // Increase y-coordinate for the next row
            }

            // Create a memory stream to hold the PDF file
            var stream = new MemoryStream();

            // Save the PDF document to the memory stream
            doc.Save(stream);

            // Close the PDF document
            doc.Close();

            // Set the position of the stream back to the beginning
            stream.Position = 0;

            // Return the PDF file for download
            return File(stream, "application/pdf", "data.pdf");
        }


}



