using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace SimpleRepo.Tests
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [TestFixture]
    public class Class1 : SpecificationBase
    {
        private Person _clonedObject;
        private Person _initialPerson;

        protected override void  Given()
        {
            var guid = Guid.NewGuid();
 	        _initialPerson = new Person
                          {
                              Age = 19,
                              Name = "Hugo"
                          };

            var memoryDb = new ObjectRepository();
            var cloner = new JsonCloner(memoryDb);

            cloner.Save(_initialPerson, guid);
            _clonedObject = cloner.Load<Person>(guid);
        }

        [Then]
        public void the_two_objects_should_be_different_instances()
        {
            object.ReferenceEquals(_clonedObject, _initialPerson).Should().BeFalse();
        }

        [Then]
        public void the_cloned_object_should_match_properties()
        {
            _clonedObject.Age.Should().Be(_initialPerson.Age);
            _clonedObject.Name.Should().Be(_initialPerson.Name);
        }
    }


    public class SpecificationBase
    {
        [SetUp]
        public void Setup()
        {
            Given();
            When();
        }
        protected virtual void Given()
        { }
        protected virtual void When()
        { }
    }
 
 
    public class ThenAttribute : TestAttribute
    { }
}
