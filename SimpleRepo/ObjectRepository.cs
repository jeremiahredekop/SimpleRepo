using System;
using System.Collections.Generic;

namespace SimpleRepo
{
    public class ObjectRepository : IRepository
    {
        public ObjectRepository()
        {
            objects = new Dictionary<Guid, object>();
        }

        public Dictionary<Guid, object> objects;

        public T Load<T>(Guid key) where T : class
        {
            if (objects.ContainsKey(key))
            {
                return objects[key] as T;
            }
            return default(T);
        }

        public void Save<T>(T toSave, Guid key) where T : class
        {
            objects[key] = toSave;
        }
    }
}