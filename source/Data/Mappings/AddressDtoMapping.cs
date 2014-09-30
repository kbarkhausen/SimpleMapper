using System;
using System.Collections.Generic;
using Demo.Data.Domain;
using Demo.Data.DTOs;
using SimpleMapper;

namespace Demo.Data.Mappings
{
    public class AddressDtoMapping : IMapping
    {

        public IMapper Mapper { get; set; }

        /// <summary>
        /// Declares all of the mappings related to this target DTO
        /// </summary>
        public void Load()
        {
            Mapper.AddMap<List<Address>, List<AddressDto>>(
                (source, target) =>
                {
                    if (target == null)
                        throw new Exception("Cannot target an empty collection");
                    foreach (var sourceItem in source)
                    {
                        var targetItem = new AddressDto();
                        Mapper.Map(sourceItem, targetItem);
                        target.Add(targetItem);
                    }
                }
               );

            Mapper.AddMap<Address, AddressDto>(
                (source, target) =>
                {
                    target.Line1 = source.Line1;
                    target.City = source.City;
                    target.State = source.State;
                    target.Country = source.Country;
                }
               );
        }
    }
}
