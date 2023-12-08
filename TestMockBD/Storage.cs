using Notes.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestMockBD
{
    public class Storage : IStorage
    {
        public StorageContext StorageContext { get; private set; } 
        public Storage(StorageContext storageContext)
        {
            StorageContext = new StorageContext();
        }

        public T GetRepository<T>()
        {
            foreach(Type type in this.GetType().GetTypeInfo().Assembly.GetTypes())
            {
                if (typeof(T).GetTypeInfo().IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                {
                    T repository = (T)Activator.CreateInstance(type);
                    repository.SetSorageContext(StorageContext);
                    return repository;
                }
            }
            return default(T);
        }

        public void Save(){}
    }
}
