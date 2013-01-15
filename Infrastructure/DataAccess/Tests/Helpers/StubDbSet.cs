using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Workfully.DomainModel.Test.Helpers
{
	public class StubDbSet<T> : IDbSet<T> where T : class
	{
		private HashSet<T> _data;

		public StubDbSet()
		{
			_data = new HashSet<T>();
		}

		public virtual T Find(params object[] keyValues)
		{
			//Assume there is an Id property
			return _data.FirstOrDefault(d => (int)typeof(T).GetProperty("Id").GetValue(d, null) == (int)keyValues[0]);
		}

		public T Add(T item)
		{
			_data.Add(item);
			return item;
		}

		public T Remove(T item)
		{
			_data.Remove(item);
			return item;
		}

		public T Attach(T item)
		{
			_data.Add(item);
			return item;
		}

		public void Detach(T item)
		{
			_data.Remove(item);
		}

		Type IQueryable.ElementType
		{
			get { return _data.AsQueryable().ElementType; }
		}

		Expression IQueryable.Expression
		{
			get { return _data.AsQueryable().Expression; }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return _data.AsQueryable().Provider; }
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		public T Create()
		{
			return Activator.CreateInstance<T>();
		}

		public ObservableCollection<T> Local
		{
			get
			{
				return new ObservableCollection<T>(_data);
			}
		}

		public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
		{
			return Activator.CreateInstance<TDerivedEntity>();
		}
	}

}
