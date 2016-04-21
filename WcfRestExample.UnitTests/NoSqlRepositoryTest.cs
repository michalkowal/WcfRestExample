using LiteDB;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WcfRestExample.Common.Data.NoSql;
using WcfRestExample.Common.Infrastructure;

namespace WcfResExample.UnitTests
{
    public class TestEntity
    {
        public int ID
        {
            get; set;
        }
        public string Field1
        {
            get;
            set;
        }
        public bool Field2
        {
            get;
            set;
        }
    }

    [TestFixture]
    public class NoSqlRepositoryTest
    {
        private INoSqlWrapper _fakeDbWrapper;
        private ICollectionWrapper<TestEntity> _fakeCollection;
        private ILoggerExt _dummyLogger;
        private NoSqlRepository<TestEntity> _sut;

        [SetUp]
        public void Init()
        {
            _fakeDbWrapper = Substitute.For<INoSqlWrapper>();
            _fakeCollection = Substitute.For<ICollectionWrapper<TestEntity>>();
            _dummyLogger = Substitute.For<ILoggerExt>();

            _sut = new NoSqlRepository<TestEntity>(_fakeDbWrapper, _dummyLogger);
        }

        [Test]
        public void FindMethodTest()
        {
            List<TestEntity> data = new List<TestEntity>()
            {
                new TestEntity() { ID = 1, Field1 = "111", Field2 = true },
                new TestEntity() { ID = 2, Field1 = "222", Field2 = false }
            };
            _fakeCollection.FindAll().Returns(data);
            _fakeDbWrapper.Execute<TestEntity>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Func<ICollectionWrapper<TestEntity>, IEnumerable<TestEntity>>>())
                .Returns(x => ((Func<ICollectionWrapper<TestEntity>, IEnumerable<TestEntity>>)x[2]).Invoke(_fakeCollection));

            IEnumerable <TestEntity> result = _sut.Find(ent => ent.ID == 1);

            Assert.IsTrue(result.Count() == 1);
            Assert.AreSame(data[0], result.First());
        }

        [Test]
        public void FindMethodTest_Empty()
        {
            List<TestEntity> data = new List<TestEntity>()
            {
                new TestEntity() { ID = 1, Field1 = "111", Field2 = true },
                new TestEntity() { ID = 2, Field1 = "222", Field2 = false }
            };
            _fakeCollection.FindAll().Returns(data);
            _fakeDbWrapper.Execute<TestEntity>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Func<ICollectionWrapper<TestEntity>, IEnumerable<TestEntity>>>())
                .Returns(x => ((Func<ICollectionWrapper<TestEntity>, IEnumerable<TestEntity>>)x[2]).Invoke(_fakeCollection));

            IEnumerable<TestEntity> result = _sut.Find(ent => ent.ID == 3);

            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public void FindMethodTest_GetAll()
        {
            List<TestEntity> data = new List<TestEntity>()
            {
                new TestEntity() { ID = 1, Field1 = "111", Field2 = true },
                new TestEntity() { ID = 2, Field1 = "222", Field2 = false }
            };
            _fakeCollection.FindAll().Returns(data);
            _fakeDbWrapper.Execute<TestEntity>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Func<ICollectionWrapper<TestEntity>, IEnumerable<TestEntity>>>())
                .Returns(x => ((Func<ICollectionWrapper<TestEntity>, IEnumerable<TestEntity>>)x[2]).Invoke(_fakeCollection));

            IEnumerable<TestEntity> result = _sut.Find(ent => true);

            Assert.IsTrue(result.Count() == 2);
            Assert.AreSame(data[0], result.First());
            Assert.AreSame(data[1], result.Last());
        }

        [Test]
        public void GetByIdMethodTest()
        {
            List<TestEntity> data = new List<TestEntity>()
            {
                new TestEntity() { ID = 1, Field1 = "111", Field2 = true },
                new TestEntity() { ID = 2, Field1 = "222", Field2 = false }
            };
            _fakeCollection.FindById(1).Returns(data[0]);
            _fakeCollection.FindById(2).Returns(data[1]);
            _fakeDbWrapper.Execute<TestEntity>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Func<ICollectionWrapper<TestEntity>, TestEntity>>())
                .Returns(x => ((Func<ICollectionWrapper<TestEntity>, TestEntity>)x[2]).Invoke(_fakeCollection));

            TestEntity result = _sut.GetById(2);

            Assert.IsNotNull(result);
            Assert.AreSame(data[1], result);
        }

        [Test]
        public void GetByIdMethodTest_Empty()
        {
            List<TestEntity> data = new List<TestEntity>()
            {
                new TestEntity() { ID = 1, Field1 = "111", Field2 = true },
                new TestEntity() { ID = 2, Field1 = "222", Field2 = false }
            };
            _fakeCollection.FindById(1).Returns(data[0]);
            _fakeCollection.FindById(2).Returns(data[1]);
            _fakeDbWrapper.Execute<TestEntity>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Func<ICollectionWrapper<TestEntity>, TestEntity>>())
                .Returns(x => ((Func<ICollectionWrapper<TestEntity>, TestEntity>)x[2]).Invoke(_fakeCollection));

            TestEntity result = _sut.GetById(10);

            Assert.IsNull(result);
        }

        [Test]
        public void InsertMethodTest()
        {
            TestEntity dummy = new TestEntity();

            _fakeCollection.Insert(dummy).Returns(new BsonValue(1));
            _fakeDbWrapper.Execute<TestEntity>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Func<ICollectionWrapper<TestEntity>, int>>())
                .Returns(x => ((Func<ICollectionWrapper<TestEntity>, int>)x[2]).Invoke(_fakeCollection));

            int result = _sut.Insert(dummy);

            Assert.AreEqual(result, 1);
        }

        [Test]
        public void UpdateMethodTest_Truthy()
        {
            TestEntity dummy = new TestEntity();

            _fakeCollection.Update(dummy).Returns(true);
            _fakeDbWrapper.Execute<TestEntity>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Func<ICollectionWrapper<TestEntity>, bool>>())
                .Returns(x => ((Func<ICollectionWrapper<TestEntity>, bool>)x[2]).Invoke(_fakeCollection));

            bool result = _sut.Update(dummy);

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateMethodTest_Falsy()
        {
            TestEntity dummy = new TestEntity();

            _fakeCollection.Update(dummy).Returns(false);
            _fakeDbWrapper.Execute<TestEntity>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Func<ICollectionWrapper<TestEntity>, bool>>())
                .Returns(x => ((Func<ICollectionWrapper<TestEntity>, bool>)x[2]).Invoke(_fakeCollection));

            bool result = _sut.Update(dummy);

            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteMethodTest_Truthy()
        {
            _fakeCollection.Delete(1).Returns(true);
            _fakeDbWrapper.Execute<TestEntity>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Func<ICollectionWrapper<TestEntity>, bool>>())
                .Returns(x => ((Func<ICollectionWrapper<TestEntity>, bool>)x[2]).Invoke(_fakeCollection));

            bool result = _sut.Delete(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteMethodTest_Falsy()
        {
            _fakeCollection.Delete(1).Returns(false);
            _fakeDbWrapper.Execute<TestEntity>(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Func<ICollectionWrapper<TestEntity>, bool>>())
                .Returns(x => ((Func<ICollectionWrapper<TestEntity>, bool>)x[2]).Invoke(_fakeCollection));

            bool result = _sut.Delete(1);

            Assert.IsFalse(result);
        }
    }
}
