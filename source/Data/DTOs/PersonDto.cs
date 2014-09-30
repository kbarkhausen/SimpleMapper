using System.Collections.Generic;

namespace Demo.Data.DTOs {
    public class PersonDto {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string OrganizationName { get; set; }
        public string JobTitle { get; set; }
        public List<AddressDto> Addresses { get; set; }

        public PersonDto()
        {
            Addresses = new List<AddressDto>();
        }
    }
}
