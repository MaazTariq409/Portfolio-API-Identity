﻿namespace Portfolio_API.Models
{
	public class ResponseBuilder
	{
		public static ResponseObject GenerateResponse(string code, string message, object data = null, string role = null)
		{
			ResponseObject responseObject = new ResponseObject();



			responseObject.Data = data;
			responseObject.Result.Code = code;
            responseObject.Result.Message = message;
			responseObject.Role = role;



            return responseObject;
		}

		//public static ResponseInfo GenerateResponse(string code, string message)
		//{
		//	ResponseInfo responseObject = new ResponseInfo();



		//	responseObject.Code = code;
		//	responseObject.Message = message;



		//	return responseObject;
		//}
	}
}
