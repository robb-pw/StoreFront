namespace StoreFront.IntegrationTests.Repositories
{
    using Config;
    using NUnit.Framework;

    [TestFixture]
    public class ProductRepositoryFixture
    {
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            NHibernateBootStrapper.Bootstrap();
        }
    }
}