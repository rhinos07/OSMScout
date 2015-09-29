namespace OpenSteetMapApi
{
    using NUnit.Framework;

    /// <summary>
    /// Test the Coordinate class
    /// </summary>
    [TestFixture]
    public class CoordinateTest
    {
        /// <summary>
        /// test the longitude
        /// </summary>
        [Test]
        public void TestLongitude()
        {
            Coordinate source = new Coordinate( 0 ,0 );
            Assert.AreEqual(0, source.Longitude);

            source = new Coordinate(0, -100);
            Assert.AreEqual(-100, source.Longitude);

            source = new Coordinate(0, -190);
            Assert.AreEqual(170, source.Longitude);

            source = new Coordinate(0, -360);
            Assert.AreEqual(0, source.Longitude);

            source = new Coordinate(0, 190);
            Assert.AreEqual(-170, source.Longitude);

            source = new Coordinate(0, 10000);
            Assert.AreEqual(-80, source.Longitude);
        }

        /// <summary>
        /// test the latitude
        /// </summary>
        [Test]
        public void TestLatitude()
        {
            Coordinate source = new Coordinate(0, 0);
            Assert.AreEqual(0, source.Latitude);

            source = new Coordinate(-100, 0);
            Assert.AreEqual(80, source.Latitude);

            source = new Coordinate(90, 0);
            Assert.AreEqual(90, source.Latitude);

            source = new Coordinate(-180, 0);
            Assert.AreEqual(0, source.Latitude);

            source = new Coordinate(-90, 0);
            Assert.AreEqual(-90, source.Latitude);

            source = new Coordinate(10000, 0);
            Assert.AreEqual(-80, source.Latitude);
        }
    }
}