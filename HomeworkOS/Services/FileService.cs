using HomeworkOS.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HomeworkOS.Services
{
	public class FileService : iFileService
	{
		public FileService()
		{
				
		}

		async Task<string> iFileService.MoveFile(string sourceFileName, string destinationFileName)
		{
			throw new NotImplementedException();
		}

		async Task<string> iFileService.ReadFileAsync(string sourceFilePath)
		{
			FileStream sourceStream = File.Open(sourceFilePath, FileMode.Open);
			var reader = new StreamReader(sourceStream);
			return await reader.ReadToEndAsync();
		}
	}
}
