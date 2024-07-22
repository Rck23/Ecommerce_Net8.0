
using System.ComponentModel.DataAnnotations;

namespace API.Dtos;
    public class RegistroDto
    {
          [Required]
    public string Nombre { get; set; }
    [Required]
    public string ApellidoPaterno { get; set; }
    [Required]
    public string ApellidoMaterno { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    }