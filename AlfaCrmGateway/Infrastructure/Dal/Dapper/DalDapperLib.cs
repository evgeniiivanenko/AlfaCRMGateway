using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Dapper;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.DTO.Entities;
using ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities;
using TRIINKOM.Dal.Dapper;
using TRIINKOM.Dal.Infrastructure;
using TRIINKOM.Infrastructure;

namespace ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper
{
    public class DalDapperLib
    {
        private const string IdPropertyName = "Id";

        private static readonly SqlMapper.IMemberMap IdMemberMap =
            SqlMapper.GetTypeMap(typeof(IEntity)).GetMember(IdPropertyName);

        #region CustomTypeMap class

        internal class CustomTypeMap : SqlMapper.ITypeMap
        {
            private readonly SqlMapper.ITypeMap _implSourceMap;
            private readonly SqlMapper.ITypeMap _interfaceSourceMap;

            public CustomTypeMap(SqlMapper.ITypeMap implSourceMap, SqlMapper.ITypeMap interfaceSourceMap)
            {
                _implSourceMap = implSourceMap;
                _interfaceSourceMap = interfaceSourceMap;
            }

            public ConstructorInfo FindConstructor(string[] names, Type[] types)
            {
                return _implSourceMap.FindConstructor(names, types);
            }

            public ConstructorInfo FindExplicitConstructor()
            {
                return _implSourceMap.FindExplicitConstructor();
            }

            public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
            {
                return _implSourceMap.GetConstructorParameter(constructor, columnName);
            }

            public SqlMapper.IMemberMap GetMember(string columnName)
            {
                return columnName == IdPropertyName
                    ? IdMemberMap
                    : _interfaceSourceMap.GetMember(columnName);
            }
        }

        #endregion CustomTypeMap class


        public static void Initialize(IAppController controller, string connectionString)
        {
            DataContext.DefaultScheme = "alf";

            controller.Register<IDbConnection>(() => new SqlConnection(connectionString));
            controller.Register<IDataContext, DataContext>();
            controller.Register<IUnitOfWorkFactory, UnitOfWorkFactory>();
            controller.Register(typeof(IRepository<>), typeof(RepositoryBase<>));
            controller.Register<IRepositoryFactory, RepositoryFactory>();
            controller.Register<IEntityFactory, EntityFactory>();

            var abstractAssembly = Assembly.GetAssembly(typeof(IAuthAlfaCRM));
            var implAssembly = Assembly.GetAssembly(typeof(AuthAlfaCRM));

            // Entities
            var entityTypes =
                abstractAssembly.GetTypes()
                    .Where(
                        t =>
                            t.Namespace == "ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.DTO.Entities")
                    .ToArray();

            var entityImplTypes =
                implAssembly.GetTypes()
                    .Where(
                        t =>
                            t.Namespace == "ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.DTO.Entities")
                    .ToArray();

            foreach (var entityImpl in entityImplTypes)
            {
                var inerfaceName = string.Concat("I", entityImpl.Name);
                var entityAbstract = entityTypes.Single(t => t.Name == inerfaceName);
                controller.Register(entityAbstract, entityImpl);

                // т.к. интерфейсы не имеют собственного конструктора, то делаем их мэппинг на их реализации и заимствуем 
                // конструтор оттуда
                SqlMapper.SetTypeMap(entityAbstract,
                    new CustomTypeMap(SqlMapper.GetTypeMap(entityImpl), SqlMapper.GetTypeMap(entityAbstract)));
            }

            // repositories
            var repositoryTypes =
                abstractAssembly.GetTypes()
                    .Where(
                        t =>
                            t.Namespace == "ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Infrastructure.Repositories" &&
                            t.Name.EndsWith("Repository"))
                    .ToArray();

            var repositoryImplTypes =
                implAssembly.GetTypes()
                    .Where(
                        t =>
                            t.Namespace == "ERIP.Sites.AlfaCrmGateway.Infrastructure.Dal.Dapper.Repositories" &&
                            t.Name.EndsWith("Repository"))
                    .ToArray();

            foreach (var repositoryImpl in repositoryImplTypes)
            {
                var inerfaceName = string.Concat("I", repositoryImpl.Name);
                var repositoryAbstract = repositoryTypes.Single(t => t.Name == inerfaceName);
                controller.Register(repositoryAbstract, repositoryImpl);
            }

        }
    }
}