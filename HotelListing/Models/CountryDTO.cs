using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    // A data transfer object (DTO)
    // is an object that carries data between processes. 


    // DTO for Creating
    public class CreateCountryDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country name is too long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 3, ErrorMessage = "Short name is too long")]
        public string ShortName { get; set; }
    }

    // DTO for Reading
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public IList<HotelDTO> Hotels { get; set; }

    }


}
