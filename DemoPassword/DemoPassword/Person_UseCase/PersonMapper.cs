using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPassword.Person_UseCase
{
    public class MapperConfiguration { }
    public class PersonMapper
    {
        public PersonMapper(MapperConfiguration config) { }
        public PersonDto[] PersonModelToUserDto(PersonModel[] onModels)
        {
            return onModels.Select(onModel => new PersonDto
            {
            }).ToArray();
        }
    }
}
