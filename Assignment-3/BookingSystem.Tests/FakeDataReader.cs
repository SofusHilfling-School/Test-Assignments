using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace BookingSystem.UnitTests
{
    /// <summary>
    /// A fake data reader that wraps either a single object or a list of type <typeparamref name="T"/> in a <see cref="IDataReader"/>.
    /// </summary>
    public class FakeDataReader<T> : IDataReader
    {
        private IEnumerator<T> _data;

        public object this[int i] => throw new NotImplementedException();

        public object this[string name] => throw new NotImplementedException();

        public int Depth => throw new NotImplementedException();

        public bool IsClosed => throw new NotImplementedException();

        public int RecordsAffected => throw new NotImplementedException();

        public int FieldCount => throw new NotImplementedException();

        /// <summary>
        /// Initialize a <see cref="FakeDataReader{T}"/> with a list of object in its result set. 
        /// <br/><br/>
        /// Replacement for a SELECT statement that returns more than on item.
        /// </summary>
        public FakeDataReader(IEnumerable<T> data)
        {
            _data = data.GetEnumerator();
            _data.Reset();
        }

        /// <summary>
        /// Initialize a <see cref="FakeDataReader{T}"/> with a single object in its result set.
        /// <br/><br/>
        /// Replacement for a SELECT statement that only returns one item.
        /// </summary>
        public FakeDataReader(T data) : this(new List<T> { data }) 
        { }

        /// <summary>
        /// Initialize a <see cref="FakeDataReader{T}"/> with an empty result set.
        /// <br/><br/>
        /// Replacement for a SELECT statement that dosent return anything.
        /// </summary>
        public FakeDataReader(): this(Enumerable.Empty<T>())
        { }

        public void Close()
            => Dispose();

        public void Dispose()
            => _data = null;
        

        public bool GetBoolean(int i)
            => (bool)GetValue(i);

        public byte GetByte(int i)
            => (byte)GetValue(i);

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
            => (char)GetValue(i);

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
            => GetFieldType(i).Name;
        

        public DateTime GetDateTime(int i)
            => (DateTime)GetValue(i);

        public decimal GetDecimal(int i)
            => (decimal)GetValue(i);

        public double GetDouble(int i)
            => (double)GetValue(i);

        public Type GetFieldType(int i)
        {
            Type genericType = typeof(T);
            PropertyInfo[] properties = genericType.GetProperties();
            PropertyInfo desiredProperty = properties[i];
            return desiredProperty.PropertyType;
        }

        public float GetFloat(int i)
            => (float)GetValue(i);

        public Guid GetGuid(int i)
            => (Guid)GetValue(i);

        public short GetInt16(int i)
            => (short)GetValue(i);

        public int GetInt32(int i)
            => (int)GetValue(i);

        public long GetInt64(int i)
            => (long)GetValue(i);

        public string GetName(int i)
        {
            Type genericType = typeof(T);
            PropertyInfo[] properties = genericType.GetProperties();
            PropertyInfo desiredProperty = properties[i];
            return desiredProperty.Name;
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
            => GetValue(i) as string;

        public object GetValue(int i)
        {
            T currentData = _data.Current;
            Type genericType = typeof(T);
            PropertyInfo[] properties = genericType.GetProperties();
            PropertyInfo desiredProperty = properties[i];
            return desiredProperty.GetValue(currentData);
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
            => _data.MoveNext();
    }
}
