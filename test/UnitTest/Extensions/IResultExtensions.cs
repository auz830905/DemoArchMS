﻿using Microsoft.AspNetCore.Http;

namespace UnitTest.Extensions
{
	public static class IResultExtensions
	{
        public static T? GetOkObjectResultValue<T>(this IResult result)
        {
            return (T?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?
                .GetProperty("Value",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
                .GetValue(result);
        }

        public static int? GetOkObjectResultStatusCode(this IResult result)
        {
            return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?
                .GetProperty("StatusCode",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
                .GetValue(result);
        }

        public static T? GetNoContentObjectResultValue<T>(this IResult result)
        {
            return (T?)Type.GetType("Microsoft.AspNetCore.Http.Result.NoContentResult, Microsoft.AspNetCore.Http.Results")?
                .GetProperty("Value",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
                .GetValue(result);
        }

        public static int? GetNoContentObjectResultStatusCode(this IResult result)
        {
            return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.NoContentResult, Microsoft.AspNetCore.Http.Results")?
                .GetProperty("StatusCode",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
                .GetValue(result);
        }

        public static T? GetBadRequestObjectResultValue<T>(this IResult result)
        {
            return (T?)Type.GetType("Microsoft.AspNetCore.Http.Result.BadRequestObjectResult, Microsoft.AspNetCore.Http.Results")?
                .GetProperty("Value",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
                .GetValue(result);
        }

        public static int? GetBadRequestObjectResultStatusCode(this IResult result)
        {
            return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.BadRequestObjectResult, Microsoft.AspNetCore.Http.Results")?
                .GetProperty("StatusCode",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
                .GetValue(result);

        }

        public static int? GetNotFoundResultValue(this IResult result)
        {
            return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.NotFoundObjectResult, Microsoft.AspNetCore.Http.Results")?
                .GetProperty("Value",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
                .GetValue(result);
        }

        public static int? GetNotFoundResultStatusCode(this IResult result)
        {
            return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.NotFoundObjectResult, Microsoft.AspNetCore.Http.Results")?
                .GetProperty("StatusCode",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
                .GetValue(result);
        }

    }
}

