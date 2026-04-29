using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models;

public class Contact
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MinLength(6, ErrorMessage = "O nome deve ter mais de 5 caracteres.")]
    public string Name { get; set; } = string.Empty;

    // Chamamos a propriedade de 'ContactPhone' pois em C# uma propriedade não pode ter o mesmo nome da classe ('Contact').
    [Required(ErrorMessage = "O contacto é obrigatório.")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "O contacto deve ter exatamente 9 dígitos.")]
    public string ContactPhone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email inserido não é válido.")]
    public string Email { get; set; } = string.Empty;

    // Propriedade que vai gerir o Soft Delete pedido no requisito
    public bool IsDeleted { get; set; } = false;
}