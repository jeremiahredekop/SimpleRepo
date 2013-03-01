using System;

namespace SimpleRepo
{
    public interface IRepository
    {
        T Load<T>(Guid key) where T : class;
        void Save<T>(T toSave, Guid key) where T : class;
    }
}