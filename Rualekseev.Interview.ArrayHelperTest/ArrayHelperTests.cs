using NUnit.Framework;

namespace Rualekseev.Interview.ArrayHelperTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { 2 }, 0)]
        [TestCase(new int[] { 2, 1 }, 2)]
        [TestCase(new int[] { 1, 2 }, 0)]
        [TestCase(new int[] { 1, 2, 3 }, 0)]
        [TestCase(new int[] { 3, 2, 1 }, 3)]
        [TestCase(new int[] { 1, 3, 2 }, 2)]
        [TestCase(new int[] { 2, 1, 3 }, 2)]
        [TestCase(new int[] { 3, 2, 1, 4, 5 }, 3)]
        [TestCase(new int[] { 1, 2, 5, 4, 3 }, 3)]
        [TestCase(new int[] { 1, 2, 4, 3, 5 }, 2)]
        [TestCase(new int[] { 1, 5, 4, 3, 2 }, 4)]
        [TestCase(new int[] { 2, 3, 4, 1, 9, 5, 6, 7, 8 }, 9)]
        public void FindUnsortedLength_Correct(int[] array, int expectedResult)
        {
            Assert.AreEqual(expectedResult, ArrayHelper.ArrayHelper.FindUnsortedLength(array));
        }
    }
}