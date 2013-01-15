using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Autumn.Mvc.Infrastructure.DataAccess.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Workfully.DomainModel.Test.Helpers;

namespace Autumn.Mvc.Infrastructure.DataAccess.Tests
{
	[TestClass]
	public class ReadOnlyRepositoryTests
	{
		private Fixture _fixture;
		private Mock<IApplicationContext> _context;
		private StubDbSet<StubModel> _dbSet;
		private ReadOnlyRepository<StubModel, int> _repository;

		[TestInitialize]
		public void Initialize()
		{
			_fixture = new Fixture();
			_fixture.Customize(new AutoMoqCustomization());

			_dbSet = _fixture.Freeze<StubDbSet<StubModel>>();
			_context = _fixture.Freeze<Mock<IApplicationContext>>();
			_context.Setup(a => a.Set<StubModel>()).Returns(_dbSet);

			_repository = _fixture.CreateAnonymous<ReadOnlyRepository<StubModel, int>>();
		}

		#region Helpers

		/// <summary>
		/// Adds a number of items to the dbset
		/// </summary>
		/// <returns></returns>
		private IEnumerable<StubModel> PopulateDbSet()
		{
			var items = _fixture.CreateMany<StubModel>(25).ToArray();
			foreach (var item in items)
				_dbSet.Add(item);

			return items;
		}

		#endregion

		[TestMethod]
		public void ReadOnlyRepositoryCreate()
		{
			//Arrange
			
			//Act
			var repo = _fixture.CreateAnonymous<ReadOnlyRepository<StubModel, int>>();
			
			//Arrange
			Assert.IsNotNull(repo);
			_context.Verify();
		}

		[TestMethod]
		public void ReadOnlyRepositoryGet()
		{
			//Arrange
			var items = this.PopulateDbSet();

			//Act
			var results = _repository.Get();

			//Assert
			Assert.AreEqual(items.Count(), results.Count());
			Assert.AreSame(items.First(), results.First());
		}

		[TestMethod]
		public void ReadOnlyRepositoryGetSimpleFilter()
		{
			//Arrange
			var items = this.PopulateDbSet();
			
			//Act
			var result = _repository.Get(m => m.Id == items.First().Id);

			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual(items.First().Id, result.First().Id);
		}

		[TestMethod]
		public void ReadOnlyRepositoryGetComplexFilter()
		{
			//Arrange
			var items = this.PopulateDbSet();
			var subItems = items.Where(i => i.Name.IndexOf('0') < 13);

			//Act
			var result = _repository.Get(m => m.Name.IndexOf('0') < 13);

			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(subItems.Count(), result.Count());
			Assert.AreEqual(subItems.First().Id, result.First().Id);
			Assert.AreEqual(subItems.Last().Id, result.Last().Id);
		}

		[TestMethod]
		public void ReadOnlyRepositoryGetOrdered()
		{
			//Arrange
			var items = this.PopulateDbSet();
			
			//Act
			var result = _repository.Get(null, m => m.OrderByDescending(mi => mi.Id));

			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(items.Count(), result.Count());
			for (int i = 1; i < result.Count(); i++)
			{
				Assert.IsTrue(result.Skip(i).First().Id < result.Skip(i-1).First().Id);
			}
		}

		[TestMethod]
		public void ReadOnlyRepositoryGetById()
		{
			//Arrange
			var items = this.PopulateDbSet();

			//Act
			var result = _repository.GetById(items.Skip(5).First().Id);

			//Assert
			Assert.AreSame(items.Skip(5).First(), result);
		}
	}
}
