using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class ConfigurationViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Bu Alan Gerekli")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu Alan Gerekli")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Bu Alan Gerekli")]
        public string Value { get; set; }
        [Required(ErrorMessage = "Bu Alan Gerekli")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Bu Alan Gerekli")]
        public string ApplicationName { get; set; }

    }
}
