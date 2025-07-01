namespace Nakliye360.Application.Models.DTOs.Authentication.RequestModel
{
    public class GoogleLoginRequestModel
    {
        /// <summary>
        /// Google tarafından sağlanan kimlik doğrulama jetonu.
        /// </summary>
        public string IdToken { get; set; }
        /// <summary>
        /// Erişim jetonunun ömrü (saniye cinsinden).
        /// </summary>
        public int AccessTokenLifeTime { get; set; } = 120; // Varsayılan olarak 120 saniye (2 dakika) ayarlandı.
        /// <summary>
        /// 

    }
}
