﻿using System;
using System.Reflection;

namespace HSB
{
    public class Error : Servlet
    {
        private string errorMsg;
        //http error code
        private int errorCode;
        public Error(Request req, Response res, string errorMessage, int errorCode) : base(req, res)
        {
            Console.WriteLine("TESTING");
            this.errorCode = errorCode;
            this.errorMsg = errorMessage;
        }
        public override void ProcessGet(Request req, Response res)
        {
            string content = $"<h2>Errore {errorCode}</h2><hr>";
            content += "Stacktrace:<br>";
            content += errorMsg.Replace("\n", "<br>");
            content += "<hr>HSB-# Server " + Assembly.GetExecutingAssembly().GetName().Version;
            content += "<hr><h6>(c) 2021 - 2023 Lorenzo L. Concas</h6>";
            res.Send(content, MimeType.TEXT_HTML, errorCode);
        }
    }
}

