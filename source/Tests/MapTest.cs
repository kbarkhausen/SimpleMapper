using System.Collections.Generic;
using System.Linq;
using Demo.Data.Domain;
using Demo.Data.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMapper;

namespace Demo.Tests {
    
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class MapTest
    {
        private IMapper _sut;


        [TestInitialize]
        public void Init()
        {
        }
    
        [TestMethod]
        public void MapToExistingDto()
        {
            _sut = new Mapper();

            var person = new Person
            {
                FirstName = "Wiley",
                LastName = "Coyote",
                MemberOf = new OrganizationMembership
                {
                    OrganizationName = "Acme Inc.",
                    JobTitle = "Safety Officer"
                },
                Addresses = new List<Address>
                {
                    new Address
                    {
                        City = "Miami",
                        State = "Florida",
                        Country = "US"
                    },
                    new Address
                    {
                        City = "Dallas",
                        State = "Texas",
                        Country = "US"
                    }
                }
            };

            var personDto = new PersonDto();

            // do the map
            _sut.Map(person, personDto);
            _sut.Map(person.MemberOf, personDto);
            _sut.Map(person.Addresses, personDto.Addresses);

            Assert.AreEqual(string.Format("{0} {1}", person.FirstName, person.LastName), personDto.FullName);
            Assert.AreEqual(person.MemberOf.JobTitle, personDto.JobTitle);
            Assert.AreEqual(person.Addresses.Count, personDto.Addresses.Count());

            Assert.AreEqual(person.Addresses[0].City, personDto.Addresses[0].City);
            Assert.AreEqual(person.Addresses[1].City, personDto.Addresses[1].City);

        }

        [TestMethod]
        public void MaptToNewDto()
        {

            _sut = new Mapper();

            var person = new Person
            {
                FirstName = "Wiley",
                LastName = "Coyote",
                MemberOf = new OrganizationMembership
                {
                    OrganizationName = "Acme Inc.",
                    JobTitle = "Safety Officer"
                },
                Addresses = new List<Address>
                {
                    new Address
                    {
                        City = "Miami",
                        State = "Florida",
                        Country = "US"
                    },
                    new Address
                    {
                        City = "Dallas",
                        State = "Texas",
                        Country = "US"
                    }
                }
            };

            // do the map
            var personDto = _sut.Map<Person, PersonDto>(person);
            _sut.Map(person.MemberOf, personDto);
            _sut.Map(person.Addresses, personDto.Addresses);

            Assert.AreEqual(string.Format("{0} {1}", person.FirstName, person.LastName), personDto.FullName);
            Assert.AreEqual(person.MemberOf.JobTitle, personDto.JobTitle);

            Assert.AreEqual(person.Addresses[0].City, personDto.Addresses[0].City);
            Assert.AreEqual(person.Addresses[1].City, personDto.Addresses[1].City);
        }
    }
}
