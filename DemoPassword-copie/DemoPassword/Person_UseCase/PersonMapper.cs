using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPassword.Person_UseCase
{
    public interface IMapperConfiguration { }
    public class PersonMapper
    {
        public PersonMapper(IMapperConfiguration config) { }
        public PersonDto[] PersonModelToUserDto(PersonModel[] onModels)
        {
            return onModels.Select(onModel => new PersonDto(
                onModel.FirstName, onModel.LastName, onModel.Email
                ))  .ToArray();
        }
    }

    public class LowerCaseMapperConfiguration : IMapperConfiguration { } 
}
