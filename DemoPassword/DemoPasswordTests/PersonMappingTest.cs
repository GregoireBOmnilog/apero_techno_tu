using DemoPassword.Person_UseCase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        IMapperConfiguration lowercaseMappingConfig = new LowerCaseMapperConfiguration();

        [Test]
        public void Test_LowerCaseMapping_PersonModelToUserDto_WithIndirections()
        {
            PersonModel personModel1 = new() { FirstName = fn1, LastName = ln1, Email = em1 };
            PersonModel personModel2 = new() { FirstName = fn2, LastName = ln2, Email = em2 };
            PersonModel[] personModelsArray = [personModel1, personModel2];
            PersonMapper personMapper = new(lowercaseMappingConfig);

            PersonDto[] dtos = personMapper.PersonModelToUserDto(personModelsArray);
            
            Assert.AreEqual(fn1.ToLower(), dtos[0].FirstName);
            Assert.AreEqual(fn2.ToLower(), dtos[1].FirstName);
            Assert.AreEqual(ln1.ToLower(), dtos[0].LastName);
            Assert.AreEqual(ln1.ToLower(), dtos[1].LastName);
            Assert.AreEqual(em1.ToLower(), dtos[0].Email);
            Assert.AreEqual(em2.ToLower(), dtos[1].Email);
        }

        public void Test_LowerCaseMapping_PersonModelToUserDto_FewerIndirections()
        {
            PersonDto[] dtos = new PersonMapper(new LowerCaseMapperConfiguration())
                .PersonModelToUserDto([
                    new PersonModel { 
                        FirstName = "John", LastName = "Doe", Email = "j@doe.com" },
                    new PersonModel { 
                        FirstName = "Jean", LastName = "Biche", Email = "j@biche.fr" },
                ]);

            Assert.AreEqual("john", dtos[0].FirstName);
            Assert.AreEqual("doe", dtos[0].LastName);
            Assert.AreEqual("j@doc.com", dtos[0].Email);

            Assert.AreEqual("jean", dtos[1].FirstName);
            Assert.AreEqual("biche", dtos[1].LastName);
            Assert.AreEqual("j@biche.fr", dtos[1].Email);
        }

        public void Test_LowerCaseMapping_PersonModelToUserDto_DetailsHidden()
        {
            PersonDto[] dtos = Service()
                .PersonModelToUserDto([
                    Model("John", "Doe", "j@doe.com"),
                    Model("Jean", "Biche", "j@biche.fr"),
                ]);

            Check(dtos, [
                ("john", "doe", "j@doe.com"),
                ("jean", "biche", "j@biche.fr")
            ]);
        }

        private void Check(PersonDto[] dtos, (string fname, string lname, string mail)[] values) => 
            dtos.ForEachPair(values, (dto, exp) => {
                Assert.AreEqual(exp.fname, dto.FirstName);
                Assert.AreEqual(exp.lname, dto.LastName);
                Assert.AreEqual(exp.mail, dto.Email);
            });

        private static PersonModel Model(string firstName, string lastName, string email) =>
            new PersonModel { FirstName = firstName, LastName = lastName, Email = email };

        private static PersonMapper Service() => new PersonMapper(new LowerCaseMapperConfiguration());


        public void Test_LowerCaseMapping_PersonModelToUserDto_BetterNames()
        {
            PersonDto[] mappedPersonDtos = LowerCaseMapper()
                .PersonModelToUserDto([
                    Person("John", "Doe", "j@doe.com"),
                    Person("Jean", "Biche", "j@biche.fr"),
                ]);

            AreEqual(mappedPersonDtos, [
                ("john", "doe", "j@doe.com"),
                ("jean", "biche", "j@biche.fr")
            ]);
        }

        private static PersonModel Person(string firstName, string lastName, string email) =>
            new PersonModel { FirstName = firstName, LastName = lastName, Email = email };

        private static PersonMapper LowerCaseMapper() => new PersonMapper(new LowerCaseMapperConfiguration());

        private void AreEqual(PersonDto[] dtos, (string fname, string lname, string mail)[] values)
        {
            dtos.ForEachPair(values, (dto, exp) =>
            {
                Assert.AreEqual(exp.fname, dto.FirstName);
                Assert.AreEqual(exp.lname, dto.LastName);
                Assert.AreEqual(exp.mail, dto.Email);
            });
        }

        public void Test_LowerCaseMapping_PersonModelToUserDto_Gherkin()
        {
            TestDriver
                .Given__A_lower_case_mapper()
                .When__Mapping_person_models_to_user_dtos([
                    ("John", "Doe", "j@doe.com"),
                    ("Jean", "Biche", "j@biche.fr")
                ])
                .Then__User_dtos_are([
                    ("john", "doe", "j@doe.com"),
                    ("jean", "biche", "j@biche.fr")
                ]);
        }

        private class TestDriver
        {
            private PersonMapper mapperToTest;
            private PersonDto[] mappedPersonDtos;

            public static TestDriver Given__A_lower_case_mapper() => new TestDriver
            {
                mapperToTest = new PersonMapper(new LowerCaseMapperConfiguration())
            };

            public TestDriver When__Mapping_person_models_to_user_dtos(params (string fname, string lname, string mail)[] inputValues)
            {
                var inputPersons = inputValues.Select(input => Person(input.fname, input.lname, input.mail));

                mappedPersonDtos = mapperToTest.PersonModelToUserDto(inputPersons.ToArray());

                return this;
            }

            public void Then__User_dtos_are(params (string fname, string lname, string mail)[] expectedValues)
            {
                
            }
        }
    }
}
