using System.Threading.Tasks;

namespace HomeworkOS.Services.Interfaces
{
	public interface iFileService
	{
		Task<string> ReadFileAsync(string sourceFilePath);

		Task<string> MoveFile(string sourceFileName, string destinationFileName);
	}
}
