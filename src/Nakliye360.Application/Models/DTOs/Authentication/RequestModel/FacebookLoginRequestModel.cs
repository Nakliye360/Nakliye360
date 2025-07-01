using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakliye360.Application.Models.DTOs.Authentication.RequestModel
{
    public class FacebookLoginRequestModel
    {
        /// <summary>
        /// Facebook tarafından sağlanan erişim belirteci
        /// </summary>
        public string AuthToken { get; set; }
        /// <summary>
        /// Erişim belirteci ömrü (saniye cinsinden)
        /// </summary>
        public int AccessTokenLifeTime { get; set; } = 120; // Varsayılan değer 120 saniye
    }
}
