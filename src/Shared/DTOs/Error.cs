﻿namespace Shared.DTOs
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
    }
}