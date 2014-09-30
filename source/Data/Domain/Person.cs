using System.Collections.Generic;

namespace Demo.Data.Domain
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public OrganizationMembership MemberOf { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
