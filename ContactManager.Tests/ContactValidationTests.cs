using ContactManager.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ContactManager.Tests;

public class ContactValidationTests
{
    // Método auxiliar para simular a validação do formulário
    private IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, ctx, validationResults, true);
        return validationResults;
    }

    [Fact]
    public void Contact_WithValidData_ShouldNotHaveValidationErrors()
    {
        // Arrange 
        var contact = new Contact
        {
            Name = "Mathews Costa",
            ContactPhone = "996480852",
            Email = "mathews@gmail.com"
        };

        // Act 
        var errors = ValidateModel(contact);

        // Assert 
        Assert.Empty(errors);
    }

    [Fact]
    public void Contact_WithNameLessThan6Chars_ShouldHaveValidationError()
    {
        // Arrange
        var contact = new Contact
        {
            Name = "João", // Inválido, tem menos de 6 caracteres
            ContactPhone = "123456789",
            Email = "teste@teste.com"
        };

        // Act
        var errors = ValidateModel(contact);

        // Assert
        Assert.NotEmpty(errors);
        Assert.Contains(errors, e => e.MemberNames.Contains("Name"));
    }

    [Fact]
    public void Contact_WithInvalidPhoneFormat_ShouldHaveValidationError()
    {
        // Arrange
        var contact = new Contact
        {
            Name = "Mathews Costa",
            ContactPhone = "123", // Inválido, precisa de 9 dígitos
            Email = "teste@teste.com"
        };

        // Act
        var errors = ValidateModel(contact);

        // Assert
        Assert.NotEmpty(errors);
        Assert.Contains(errors, e => e.MemberNames.Contains("ContactPhone"));
    }

    [Fact]
    public void Contact_WithInvalidEmail_ShouldHaveValidationError()
    {
        // Arrange
        var contact = new Contact
        {
            Name = "Mathews Costa",
            ContactPhone = "123456789",
            Email = "email_sem_arroba.com" // Inválido
        };

        // Act
        var errors = ValidateModel(contact);

        // Assert
        Assert.NotEmpty(errors);
        Assert.Contains(errors, e => e.MemberNames.Contains("Email"));
    }
}