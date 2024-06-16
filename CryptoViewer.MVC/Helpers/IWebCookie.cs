﻿namespace CryptoViewer.MVC.Helpers
{
    public interface IWebCookie
    {
        void AddSecure(string cookieName, string value, int days = 0);

        void Add(string cookieName, string value, int days = 0);

        void Delete(string cookieName);

        string? Get(string cookieName);
    }
}
