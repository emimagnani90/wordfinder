namespace Test
{
    public class Tests
    {
        private IEnumerable<string> _matrix;
        private IEnumerable<string> _stream;

        [SetUp]
        public void Setup()
        {
            this._matrix = new List<string> {
                "dotnetadot", // dot: 2 net: 1 
                "dfeunitaee", // unit: 1
                "testtestes", // test: 2
                "vbnfdgvrrt",
                "netfgherta", // net: 1
                "edotunitas", // dot:1 unit: 1
                "tsdsdsdsdc", 
                "jklpdotsdn", // dot: 1
                "nbmbomnbme",
                "ghfgtgfhgt" };
            // vertical test: 1 net: 2 dot: 1
            // dot: 5, net: 4 test: 3 unit: 2
            this._stream = new List<string> { "dot", "net", "test", "unit", "invalid", "oversizeword" };
        }

        [Test]
        public void Test1()
        {
            var finder = new WordFinder.WordFinder(this._matrix);
            var res = finder.Find(this._stream);

            Assert.IsTrue(res.ElementAt(0) == "dot" && res.ElementAt(1) == "net" && res.ElementAt(2) == "test" && res.ElementAt(3) == "unit" && res.Count() == 4);
        }

        [Test]
        public void Test2()
        {
            var finder = new WordFinder.WordFinderAlt(this._matrix);
            var res = finder.Find(this._stream);

            Assert.IsTrue(res.ElementAt(0) == "dot" && res.ElementAt(1) == "net" && res.ElementAt(2) == "test" && res.ElementAt(3) == "unit" && res.Count() == 4);
        }
    }
}