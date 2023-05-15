namespace Portfolio_API.Models
{
	public class ResponseObject
	{
		public ResponseObject()
		{
			Result = new ResponseInfo();
		}
		public ResponseInfo Result { get; set; }
		public object Data { get; set; }
	}
}
