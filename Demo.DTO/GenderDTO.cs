using System.ComponentModel.DataAnnotations;

namespace Demo.DTO
{
  
    public record GenderDTO(
        int GenderId,
        string GenderName
    );

}
