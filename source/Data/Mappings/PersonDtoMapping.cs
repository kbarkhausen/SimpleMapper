using Demo.Data.Domain;
using Demo.Data.DTOs;
using SimpleMapper;

namespace Demo.Data.Mappings
{
    public class PersonDtoMapping : IMapping
    {

        public IMapper Mapper { get; set; }

        /// <summary>
        /// Declares all of the mappings related to this target DTO
        /// </summary>
        public void Load()
        {
            Mapper.AddMap<Person, PersonDto>(
                   (source, target) =>
                   {
                       target.FirstName = source.FirstName;
                       target.LastName = source.LastName;
                       target.FullName = string.Format("{0} {1}", source.FirstName, source.LastName);
                   }
               );          

            Mapper.AddMap<OrganizationMembership, PersonDto>(
                    (source, target) =>
                    {
                        target.JobTitle = source.JobTitle;
                        target.OrganizationName = source.OrganizationName;
                    }
                );

        }
    }
}
