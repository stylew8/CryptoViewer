﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.Auth_API.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode;
        public bool IsSuccess;
        public List<string> ErrorMessages;
        public object Result;

    }
}
