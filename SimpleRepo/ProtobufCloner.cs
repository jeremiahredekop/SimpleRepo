using System;
using System.IO;
using Newtonsoft.Json;
using ProtoBuf;

namespace SimpleRepo
{
    public class JsonCloner : IRepository
    {
        private static T Clone<T>(T input) where T: class
        {
            if (input == null)
                return null;

            var innerString = JsonConvert.SerializeObject(input);
            var inner = JsonConvert.DeserializeObject<T>(innerString);
            return inner;
        }

        private readonly IRepository _inner;

        public JsonCloner(IRepository inner)
        {
            _inner = inner;
        }

        public T Load<T>(Guid key) where T : class
        {
            var toReturn = _inner.Load<T>(key);
            return Clone(toReturn);
        }

        public void Save<T>(T toSave, Guid key) where T : class
        {
            var innerToSave = Clone(toSave);
            _inner.Save(innerToSave, key);
        }
    }
}
