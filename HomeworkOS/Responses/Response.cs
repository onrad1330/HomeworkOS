namespace HomeworkOS.Responses
{
	public class Response
	{

		public Response(Document document, int statusCode, string errorMessage)
		{
			Document = document;
			StatusCode = statusCode;
			ErrorMessage = errorMessage;
		}

		public Response()
		{
				
		}

		public Document Document { get; set; }

		public int StatusCode { get; set; }
		public string ErrorMessage { get; set; }
	}
}
