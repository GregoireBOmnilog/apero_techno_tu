﻿using DemoPassword.Person_UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoPasswordTests
{
    public class PersonMappingTest
    {
        private const string fn1 = "Jean";
        private const string ln1 = "Biche";
        private const string em1 = "jean@biche.fr";

        private const string fn2 = "John";
        private const string ln2 = "Doe";
        private const string em2 = "john@doe.com";

        [Test]
        public void PersonModelToUserDtoTest_WithIndirections()
        {
            PersonModel personModel1 = new() { FirstName = fn1, LastName = ln1, Email = em1 };
            PersonModel personModel2 = new() { FirstName = fn2, LastName = ln2, Email = em2 };
            PersonModel[] personModelsArray = [personModel1, personModel2];
            PersonMapper personMapper = new();

            PersonDto[] personDtos = personMapper.PersonModelToUserDto(personModelsArray);

            Assert.AreEqual(fn1, personDtos[0].FirstName);
            Assert.AreEqual(ln1, personDtos[0].LastName);
            Assert.AreEqual(em1, personDtos[0].Email);

            Assert.AreEqual(fn2, personDtos[1].FirstName);
            Assert.AreEqual(ln2, personDtos[1].LastName);
            Assert.AreEqual(em2, personDtos[1].Email);
        }

        public void PersonModelToUserDtoTest_NoIndirections()
        {
            PersonDto[] personDtos = new PersonMapper().PersonModelToUserDto([
                new PersonModel { 
                    FirstName = "John", LastName = "Doe", Email = "john@doe.com" },
                new PersonModel { 
                    FirstName = "Jean", LastName = "Biche", Email = "jean@biche.fr" },
            ]);

            Assert.AreEqual("John", personDtos[0].FirstName);
            Assert.AreEqual("Doe", personDtos[0].LastName);
            Assert.AreEqual("john@doe.com", personDtos[0].Email);

            Assert.AreEqual("Jean", personDtos[1].FirstName);
            Assert.AreEqual("Biche", personDtos[1].LastName);
            Assert.AreEqual("jean@biche.fr", personDtos[1].Email);
        }
    }
}
