﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSB
{
    public enum HTTP_METHOD { GET, POST, PUT, DELETE, HEAD, UNKNOWN }
    public enum HTTP_PROTOCOL { HTTP1_0, HTTP1_1, HTTP2_0, HTTP3_0, UNKNOWN }
    public class Request
    {
        //support-variables
        readonly string reqText = "";
        readonly string[] reqTextLns;


        //Request variables
        public bool validRequest = false;
        HTTP_METHOD _method = HTTP_METHOD.UNKNOWN;
        HTTP_PROTOCOL _protocol = HTTP_PROTOCOL.UNKNOWN; //HTTP1.0 ecc
        string _url = "";

        List<string> rawHeaders = new List<string>();


        public Request(byte[] data)
        {
            if (data == null)
            {
                reqTextLns = Array.Empty<String>();
                return;
            }
            reqText = Encoding.UTF8.GetString(data);
            reqTextLns = reqText.Split("\r\n");
            parseRequest();
            rawHeaders = new List<string>();
        }
        private void parseRequest()
        {
            try
            {
                string[] firstLine = reqTextLns[0].Split(" ");
                _method = HttpUtils.GetMethod(firstLine[0]);
                _url = firstLine[1];
                _protocol = HttpUtils.GetProtocol(firstLine[2]);
            }
            catch (Exception e)
            {
                Terminal.WriteLine("Invalid request, reason : " + e.Message, BG_COLOR.NERO, FG_COLOR.ROSSO);
                validRequest = false;
                return;
            }
            validRequest = true;
        }



        public override string ToString()
        {
            String str = _method.ToString() + " - " + _url + " - " + _protocol.ToString();
            return str;
        }
        public HTTP_METHOD METHOD => this._method;
        public HTTP_PROTOCOL PROTOCOL => _protocol;
        public string URL => _url;
    }


    public static class HttpUtils
    {
        public static string MethodAsString(HTTP_METHOD method) => method switch
        {
            HTTP_METHOD.GET => "GET",
            HTTP_METHOD.POST => "POST",
            HTTP_METHOD.PUT => "PUT",
            HTTP_METHOD.DELETE => "DELETE",
            HTTP_METHOD.HEAD => "HEAD",
            _ => "GET", //failsafe?
        };

        public static string ProtocolAsString(HTTP_PROTOCOL protocol) => protocol switch
        {
            HTTP_PROTOCOL.HTTP1_0 => "HTTP/1.0",
            HTTP_PROTOCOL.HTTP1_1 => "HTTP/1.1",
            HTTP_PROTOCOL.HTTP2_0 => "HTTP/2.0",
            HTTP_PROTOCOL.HTTP3_0 => "HTTP/3.0",
            _ => "HTTP/1.0",
        };
        public static HTTP_METHOD GetMethod(string data) => data switch
        {
            "GET" => HTTP_METHOD.GET,
            "POST" => HTTP_METHOD.POST,
            "PUT" => HTTP_METHOD.PUT,
            "DELETE" => HTTP_METHOD.DELETE,
            "HEAD" => HTTP_METHOD.HEAD,
            _ => throw new Exception("Unsupported Http Method"),
        };

        public static HTTP_PROTOCOL GetProtocol(string data) => data switch
        {
            "HTTP/1.0" => HTTP_PROTOCOL.HTTP1_0,
            "HTTP/1.1" => HTTP_PROTOCOL.HTTP1_1,
            "HTTP/2.0" => HTTP_PROTOCOL.HTTP2_0,
            "HTTP/3.0" => HTTP_PROTOCOL.HTTP3_0,
            _ => throw new Exception("Unsupported HTTP Protocol")
        };

    }

}
