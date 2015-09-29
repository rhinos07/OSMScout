namespace OpenSteetMapApi
{
    using NUnit.Framework;
    using OpenStreetMapPictures.TilesDownload;

    /// <summary>
    /// test the calculator class
    /// </summary>
    [TestFixture]
    public class CalculatorTest
    {
        /// <summary>
        ///  test the GetTiles function
        /// </summary>
        [Test]
        public void TestGetTiles()
        {
            int xtile = 0;
            int ytile = 0;

            Calculator.GetTiles(new Coordinate(2.7, -4.2), 10, ref xtile, ref ytile);

            Assert.AreEqual(500, xtile);
            Assert.AreEqual(504, ytile);

            Calculator.GetTiles(new Coordinate(47.6, 7.6), 8, ref xtile, ref ytile);

            Assert.AreEqual(133, xtile);
            Assert.AreEqual(89, ytile);

            Calculator.GetTiles(new Coordinate(47.9899, 7.6), 8, ref xtile, ref ytile);

            Assert.AreEqual(133, xtile);
            //Assert.AreEqual(88, ytile);  // ref http://oms.wff.ch/calc.php?long=7.031250&lat=47.9899 
            Assert.AreEqual(89 , ytile); // ref http://oms.wff.ch/calc.htm
            
        }

        /// <summary>
        /// test the GetTilesDoubleValue function
        /// </summary>
        [Test]
        public void TestGetTilesDoubleValue()
        {
            double xtile = 0;
            double ytile = 0;

            Calculator.GetTilesDoubleValue(new Coordinate(2.7, -4.2), 10, ref xtile, ref ytile);

            Assert.AreEqual(500.053, xtile , 0.001);
            Assert.AreEqual(504.3171, ytile, 0.001);
        }

        /// <summary>
        /// test
        /// </summary>
        [Test]
        public void TestGetLonLat()
        {

            Coordinate coord = Calculator.GetLonLat(133, 89, 8);
            
            Assert.AreEqual(7.03125, coord.Longitude, 0.0001);
            Assert.AreEqual(47.9899, coord.Latitude, 0.0001);

        }

        /// <summary>
        /// test the function GetCountTiles
        /// </summary>
        [Test]
        public void TestGetCountTiles()
        {
            Assert.AreEqual(32, Calculator.GetCountTiles(5));
            Assert.AreEqual(262144, Calculator.GetCountTiles(18));
        }
    }
}