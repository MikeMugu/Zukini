using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Zukini.Unit.Tests
{
    [TestClass]
    public class EnumerableExtensionsTest
    {
        [TestMethod]
        public void FirstOrErrorForEmptyList()
        {
            var errorMessage = "This error is expected";
            Assert.Throws<AssertionException>(() =>
                    Enumerable.Empty<object>().FirstOrError(errorMessage),
                "FirstOrError for empty list didn't throw exception");
        }

        [TestMethod]
        public void FirstOrErrorForNotFoundItem()
        {
            var items = Enumerable.Range(1, 10);
            var errorMessage = "This error is expected";

            var ex = Assert.Throws<AssertionException>(() =>
                    items.Where(x => x < 0).FirstOrError(errorMessage),
                "FirstOrError for non empty list didn't throw exception");

            Assert.AreEqual(ex.Message, errorMessage, "Expected exception error message is wrong");
        }

        [TestMethod]
        public void FirstOrErrorForExpectedItem()
        {
            var items = new List<string> {"one", "two", "three"};
            Assert.AreEqual("two", items.Where(_ => _ == "two").FirstOrError("Some error"),
                "FirstOrError for expected item");
        }

        [TestMethod]
        public void FirstOrErrorWithFunctionForExpectedItem()
        {
            var objects = new[] {"10", "30", 50, new object(), "70", "90"};
            Assert.AreEqual(50, objects.FirstOrError(it => it is int, "Error"),
                "FirstOrError with function for expected item");
        }

        [TestMethod]
        public void FirstOrErrorWithFunctionForNotFoundItem()
        {
            var errorMessage = "This error is expected";
            var ex = Assert.Throws<AssertionException>(() =>
                    Enumerable.Empty<int>().FirstOrError(it => it > 0, errorMessage),
                "FirstOrError with function for empty list does not throw exception");

            Assert.AreEqual(ex.Message, errorMessage, "Expected exception error message is wrong");
        }

        [TestMethod]
        public void ShuffleReturnsSimilarEnumerable()
        {
            var items = Enumerable.Range(-1000, 0).ToList();
            var shuffled = items.Shuffle();
            Assert.AreNotSame(items, shuffled, "Items not shuffled");
            Assert.That(shuffled, Is.EquivalentTo(items), "Items missing after shuffle");
        }

        [TestMethod]
        public void ShuffleEmptyEnumerable()
        {
            var empty = Enumerable.Empty<object>();
            Assert.IsEmpty(empty.Shuffle(), "Cannot shuffle empty list");
        }

        [TestMethod]
        public void RandomFirstOfEmptyThrowsError()
        {
            var errorMessage = "This error is expected";
            Assert.Throws<AssertionException>(() =>
                    Enumerable.Empty<object>().RandomFirst(errorMessage),
                "FirstOrError for empty list didn't throw exception");
        }

        [TestMethod]
        public void RandomFirstOfNonEmptyEnumerable()
        {
            var objects = new List<string> {"two", "", "one", "", "70", "90"};
            var resultList = new List<string>();
            for (var i = 0; i < objects.Count; i++)
            {
                var item = objects.RandomFirst();
                Assert.That(objects.Contains(item), "RandomFirst returns expected item");
                resultList.Add(item);
            }

            Assert.That(resultList, Is.Not.EquivalentTo(objects),
                "RandomFirst returns the same items as in original list");
        }

        [TestMethod]
        public void TakeOrErrorOfEmptyEnumerable()
        {
            var errorMessage = $"Unable to take {5} number of items.";
            var ex = Assert.Throws<AssertionException>(() =>
                    Enumerable.Empty<object>().TakeOrError(5),
                "FirstOrError for empty list didn't throw exception");

            Assert.AreEqual(ex.Message, errorMessage, "Expected exception error message is wrong");
        }

        [TestMethod]
        public void TakeOrErrorOfProperSizedEnumerable()
        {
            var limit = 5;
            var items = Enumerable.Range(0, 1000);
            var taken = items.TakeOrError(limit, "Error");
            var expected = Enumerable.Range(0, limit);

            Assert.AreEqual(expected, taken, "Taken items wrong");
        }

        [TestMethod]
        public void DistrinctByGroupObjects()
        {
            var items = new List<SomePlainPoco>
            {
                new SomePlainPoco {Id = 101, Name = "Pink Farting Weasel"},
                new SomePlainPoco {Id = 101, Name = "Arr Matey! A Hairy Bilge Rat!" },
                new SomePlainPoco {Id = 101, Name = "Stable Penguin" },
                new SomePlainPoco {Id = 102, Name = "Stable Penguin" }
            };

            var expItems = new List<SomePlainPoco>
            {
                new SomePlainPoco {Id = 101, Name = "Pink Farting Weasel"},
                new SomePlainPoco {Id = 102, Name = "Stable Penguin"}
            };

            var uniqueByField = items.DistinctBy(it => it.Id).ToList();
            Assert.That(uniqueByField, Is.EquivalentTo(expItems), "DistinctBy field not expected");
        }
    }

    internal class SomePlainPoco
    {
        public string Name { get; set; }
        public int Id { get; set; }

        protected bool Equals(SomePlainPoco other)
        {
            return Id == other.Id && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SomePlainPoco) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }
    }
}
