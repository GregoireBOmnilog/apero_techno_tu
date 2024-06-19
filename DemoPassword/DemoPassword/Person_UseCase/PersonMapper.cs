using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPassword.Person_UseCase
{
    public class PersonMapper
    {
        public PersonDto[] PersonModelToUserDto(PersonModel[] onModels)
        {
            return onModels.Select(onModel => new PersonDto
            {
            }).ToArray();
        }
    }
}
