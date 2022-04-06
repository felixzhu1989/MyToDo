﻿namespace MyToDo.Api.Service
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        public ApiResponse(bool status,object result)
        {
            Status = status;
            Result = result;
        }

        public ApiResponse(string message,bool status=false)
        {
            Message=message;
            Status = status;
        }
    }
}
