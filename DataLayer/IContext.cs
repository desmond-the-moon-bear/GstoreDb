using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IContext<T, K> where K : IConvertible
    {
        void Create(T item);

        T Read(K key, bool useNavigationalProperties = false);

        IEnumerable<T> ReadAll(bool useNavigationalProperties = false);

        void Update(T item, bool useNavigationalProperties = false);

        void Delete(K key);
    }
}
