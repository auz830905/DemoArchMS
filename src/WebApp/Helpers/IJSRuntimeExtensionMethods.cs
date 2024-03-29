﻿using Microsoft.JSInterop;

namespace WebApp.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<bool> Confirm(this IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("console.log", "example message");
            return await js.InvokeAsync<bool>("confirm", message);
        }

        public static async ValueTask MyFunction(this IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("myFunction", "message");
        }

        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content) =>
        js.InvokeAsync<object>(
         "localStorage.setItem",
         key, content
         );

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key) =>
            js.InvokeAsync<string>(
                "localStorage.getItem",
                key
                );

        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key) =>
            js.InvokeAsync<object>(
                "localStorage.removeItem",
                key
                );
    }
}