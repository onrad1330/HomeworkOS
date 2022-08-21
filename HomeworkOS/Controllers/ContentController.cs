using HomeworkOS.Responses;
using HomeworkOS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HomeworkOS.Controllers
{
	[ApiController]
	//[Route("api/[controller]")]
	public class ContentController : ControllerBase 
	{ 

		private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment { get; set; }
		private iFileService _fileManager;

		public ContentController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, iFileService fileManager)
		{
			_hostingEnvironment = hostingEnvironment;
			_fileManager = fileManager;
		}

		[HttpGet(Name = "/GetFileContent")]
		[Route("GetFileContent")]

		public async Task<Response> GetFileContent(string fileName)
		{

			string sourceFileName = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads", fileName);

			string input;
			try
			{
				input = await _fileManager.ReadFileAsync(sourceFileName);
			}
			catch (Exception ex)
			{
				return new Response(null, 500, ex.Message);
			}

			if (string.IsNullOrEmpty(input))
				return new Response(null, 200, "File is empty or does not exist");


			var xdoc = XDocument.Parse(input);
			var doc = new Document
			{
				Title = xdoc.Root.Element("title").Value,
				Text = xdoc.Root.Element("text").Value
			};

			return new Response(doc, 200, "");
		}


		[HttpGet(Name = "GetFile")]
		[Route("Download")]
		public async Task<IActionResult> Download()
		{
			string sourceFileName = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads", "test.xml");

			Stream stream = System.IO.File.OpenRead(sourceFileName);

			if (stream == null)
				return NotFound(); // returns a NotFoundResult with Status404NotFound response.

			return File(stream, "application/octet-stream"); // returns a FileStreamResult
		}
	}
}
