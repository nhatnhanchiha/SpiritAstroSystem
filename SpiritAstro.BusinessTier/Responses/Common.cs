using System.Collections.Generic;

namespace SpiritAstro.BusinessTier.Responses
{
    public class MyResponse<T>
    {
        public const int Fail = 7;
        public const int Success = 0;
        public int Code { get; set; }
        public T Data { get; set; }
        public string Msg { get; set; }

        public static MyResponse<T> Result(int code, T data, string msg)
        {
            return new MyResponse<T>
            {
                Code = code,
                Data = data,
                Msg = msg
            };
        }

        public static MyResponse<T> OkWithMessage(string message)
        {
            return new MyResponse<T>
            {
                Code = Success,
                Msg = message
            };
        }

        public static MyResponse<T> OkWithData(T data)
        {
            return new MyResponse<T>
            {
                Code = Success,
                Data = data
            };
        }

        public static MyResponse<T> OkWithDetail(T data, string message)
        {
            return new MyResponse<T>
            {
                Code = Success,
                Data = data,
                Msg = message
            };
        }

        public static MyResponse<T> FailWithMessage(string message)
        {
            return new MyResponse<T>
            {
                Code = Fail,
                Msg = message
            };
        }

        public static MyResponse<T> FailWithData(T data)
        {
            return new MyResponse<T>
            {
                Code = Fail,
                Data = data
            };
        }

        public static MyResponse<T> FailWithDetail(T data, string message)
        {
            return new MyResponse<T>
            {
                Code = Fail,
                Data = data,
                Msg = message
            };
        }
    }
    
    public class PageResult<T>
    {
        public List<T> List { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}