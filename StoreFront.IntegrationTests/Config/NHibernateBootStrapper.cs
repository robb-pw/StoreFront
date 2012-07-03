namespace StoreFront.IntegrationTests.Config
{
    using Core.Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    public class NHibernateBootStrapper
    {
        private static ISessionFactory sessionFactory;
        private static Configuration configuration;

        public static void Bootstrap()
        {
            configuration = Fluently.Configure()
                .Database(MsSqlCeConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey("StoreFrontDatabase")))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Product>(new NHibernateConfiguration())
                                .UseOverridesFromAssemblyOf<Product>()
                                .Conventions.AddFromAssemblyOf<Product>()
                            )
                          )
                .ExposeConfiguration(cfg => cfg.SetProperty("connection.release_mode", "on_close"))
                .BuildConfiguration();

            sessionFactory = configuration.BuildSessionFactory();
        }

        public static void UpdateSchema()
        {
            new SchemaUpdate(configuration).Execute(false, true);
        }

        public static void CreateSchema()
        {
            new SchemaExport(configuration).Execute(false,true,false);
        }

        public static ISession GetSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}
