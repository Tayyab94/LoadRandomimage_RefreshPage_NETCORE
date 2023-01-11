using LoadImages.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace LoadImages.Controllers
{
	public class CartItem
	{
		public string Name { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}

	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("/image")]
		public IActionResult GetRandomImage()
		{
			var staticPath = Path.Combine(Directory.GetCurrentDirectory(), "static");
			var imageFiles = Directory.GetFiles(staticPath);
			if (!imageFiles.Any())
			{
				return NotFound();
			}
			var random = new Random();
			var randomIndex = random.Next(0, imageFiles.Length);
			var imagePath = imageFiles[randomIndex];
			var fileExtension = Path.GetExtension(imagePath);
			var contentType = GetContentType(fileExtension);
			if (contentType == null)
			{
				return NotFound();
			}
			var image = System.IO.File.OpenRead(imagePath);
			return File(image, contentType);
		}

		private string GetContentType(string fileExtension)
		{
			var contentType = "image/" + fileExtension.Substring(1);
			if (fileExtension == ".jpeg")
			{
				contentType = "image/jpeg";
			}
			else if (fileExtension == ".png")
			{
				contentType = "image/png";
			}
			else if (fileExtension == ".gif")
			{
				contentType = "image/gif";
			}
			else if (fileExtension == ".svg")
			{
				contentType = "image/svg+xml";
			}
			else if (fileExtension == ".ico")
			{
				contentType = "image/x-icon";
			}
			return contentType;
		}


		//[HttpGet("/receipt")]
		//public IActionResult GenerateReceipt() {

		//	var fileName = "receipt.pdf";
		//	var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

		//	var cart = new List<CartItem>()
		//{
		//	new CartItem(){ Name = "item1", Quantity = 2, Price = 10 },
		//	new CartItem(){ Name = "item2", Quantity = 1, Price = 20 },
		//};

		//	using (var stream = new FileStream(filePath, FileMode.Create))
		//	{
		//		var pdf = new PdfDocument(new PdfWriter(stream));
		//		var doc = new Document(pdf);

		//		var header = new Paragraph("Receipt")
		//						.SetTextAlignment(TextAlignment.CENTER)
		//						.SetBold()
		//						.SetFontSize(20);
		//		doc.Add(header);
		//		doc.Add(new Paragraph(" ")); // adding some space between the header and body

		//		// Add table to display the cart items
		//		var table = new Table(new float[] { 2, 2, 2, 2 });
		//		table.AddHeaderCell("Name");
		//		table.AddHeaderCell("Quantity");
		//		table.AddHeaderCell("Price");
		//		table.AddHeaderCell("Total");

		//		decimal totalPrice = 0;
		//		foreach (var item in cart)
		//		{
		//			var totalItemPrice = item.Quantity * item.Price;
		//			table.AddCell(item.Name);
		//			table.AddCell(item.Quantity.ToString());
		//			table.AddCell(item.Price.ToString());
		//			table.AddCell(totalItemPrice.ToString());
		//			totalPrice += totalItemPrice;
		//		}
		//		doc.Add(table);
		//		doc.Add(new Paragraph(" "));
		//		doc.Add(new Paragraph("Total: " + totalPrice));
		//		doc.Close();
		//	}

		//	return File(filePath, "application/pdf", fileName);
		//}



	public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}