namespace StoreFront.IntegrationTests.Config
{
    using System;
    using System.Reflection;
    using Core.Domain;
    using FluentNHibernate;
    using FluentNHibernate.Automapping;

    public class NHibernateConfiguration : DefaultAutomappingConfiguration
    {
        public override bool IsId(Member member)
        {
            return member.Name == "Id";
        }

        public override bool ShouldMap(Type type)
        {
            return type.Namespace == typeof(Product).Namespace && base.ShouldMap(type);
        }

        public override bool ShouldMap(Member member)
        {
            var prop = member.MemberInfo as PropertyInfo;

            if (prop != null && !prop.CanWrite)
            {
                return false;
            }

            return base.ShouldMap(member);
        }
    }
}