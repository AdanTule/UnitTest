using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestApi.Service
{
    public class CrudService //Service class with DI
    {
        IDataStorage _dataStorage;
        public CrudService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
        public int Create(string name)
        {
            return _dataStorage.Insert(name);
        }

        public string Read()
        {
            return _dataStorage.Select();
        }

        public int Update(string oldName, string newName)
        {
            return _dataStorage.Update(oldName, newName);
        }

        public void Delete(string id)
        {
             _dataStorage.Delete(id);
        }
       
    }

    public interface IDataStorage //Interface for DI
    {
        int Insert(string name);

        string Select();

        int Update(string oldName, string newName);

        void Delete(string id);
    }
}
