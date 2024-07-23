using System.ComponentModel.DataAnnotations;

namespace Demo.DTO
{
    public record CreateUserDTO(
        [Required(ErrorMessage = "Please provide name")] string Name,
        [Required(ErrorMessage = "Please provide email")] string Email,
        [Required(ErrorMessage = "Please provide IsActive")] bool IsActive,
        [Required(ErrorMessage = "Please provide Gender id")] int GenderId,
        [Required(ErrorMessage = "Please provide user id of user Creating record")] int CreatedBy
        );
    public record UpdateUserDTO(
        [Required(ErrorMessage ="Please provide user id")]int UserId,
        string? Name,
        string? Email,
        bool? IsActive,
        int GenderId,
        [Required(ErrorMessage = "Please provide user id of user updating the record")] int UpdatedBy
        );
    public record UserDTO(
        int UserId,
        string Name,
        string Email,
        bool IsActive,
        string Gender,
        DateTimeOffset CreatedDate
    );

}
