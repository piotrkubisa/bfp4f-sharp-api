﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bfp4f_sharp_api
{
    class ApiCommunication
    {
        public const string BaseUrl = "http://battlefield.play4free.com/";

        protected string _languageUrl = "en";

        protected string _urlPath = "/";

        public HttpWebRequest request
        {
            get;
            set;
        }
        protected HttpWebResponse response;

        public ApiCommunication()
        {
        }

        public ApiCommunication(string urlPath)
        {
            _urlPath += urlPath;
        }

        public ApiCommunication(string lang, string urlPath)
        {
            _languageUrl = lang;
            _urlPath += urlPath;
        }

        public ApiCommunication(List<string> urlPath)
        {
            _urlPath = "";
            foreach (var item in urlPath)
            {
                _urlPath += "/" + item;
            }
        }

        public ApiCommunication(List<string> urlPath, Dictionary<string, string> urlParams)
        {
            _urlPath = "";

            foreach (var item in urlPath)
            {
                _urlPath += "/" + item;
            }

            _urlPath += "?";

            foreach (var item in urlParams)
            {
                _urlPath += item.Key + "=" + item.Value + "&";
            }

            _urlPath = _urlPath.Substring(0, _urlPath.Length - 1);
        }

        public void setLang(string lang)
        {
            switch (lang)
            {
                case "en":
                case "de":
                case "ru":
                case "fr":
                case "pl":
                    _languageUrl = lang;
                    break;
                default:
                    break;
            }
            
        }

        public void prepare()
        {
            request = (HttpWebRequest)WebRequest.Create(BaseUrl + _languageUrl + _urlPath);
            request.Accept = "application/json, text/javascript";
        }

        public string call()
        {
            response = (HttpWebResponse)request.GetResponse();
            string json = "";
            using (StreamReader responseStream = new StreamReader(response.GetResponseStream()))
            {
                json = responseStream.ReadToEnd();
                responseStream.Close();
                response.Close();
                return json;
            }
        }
    }
}