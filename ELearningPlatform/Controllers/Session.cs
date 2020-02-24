/*
Seems to be not possible to make a Session class to control it due to dependecy injection

(Need to look more about it later)
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ELearningPlatform.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ELearningPlatform.Controllers
{
    public class Session : Controller
    {
        public const string SessionKeyEmail = "_Email";
        public const string SessionKeyRole = "_Code";
        public const string SessionKeyIsConfirmed = "_IsConfirmed";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public Session(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string SessionEmail { get
            {
                try
                {
                    return _session.GetString(SessionKeyEmail);
                }catch
                {
                    return null;
                }
            }
            set {
                try
                {
                    _session.SetString(SessionKeyEmail, value);
                }catch
                {
                    throw new UnauthorizedAccessException();
                }
            } 
        }
        public string SessionRole { get; set; }
        public string SessionUserIsCofirmed { get; set; }

        public bool IsSessionEmpty()
        {
            return string.IsNullOrEmpty(SessionEmail);
        }
    }
}
