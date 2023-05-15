namespace Portfolio_API.Models
{
	public class ResponseInfo
	{
			public ResponseInfo()
			{
				Code = ResultCode.Failure.ToString();
			}

			public string Code { get; set; }

			public string Message { get; set; }
		
	}
}
