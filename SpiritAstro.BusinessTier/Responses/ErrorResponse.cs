using System;

namespace SpiritAstro.BusinessTier.Responses
{
    public class ErrorResponse : Exception
    {
        public ErrorDetailResponse Error { get; }

        public ErrorResponse()
        {
            
        }

        public ErrorResponse(int code, string message)
        {
            Error = new ErrorDetailResponse
            {
                Code = code,
                Message = message
            };
        }
    }
    
    public class ErrorDetailResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}