using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Xunit;
using System.Threading;
using CK.Core;
using Dapper;
using static CK.Testing.SqlServerTestHelper;

namespace CK.SqlServer.Dapper.Tests
{
    public abstract class TestBase : IDisposable
    {
        public static string ConnectionString => TestHelper.GetConnectionString();

        SqlStandardCallContext _callContext;
        protected ISqlConnectionController _controller;
        protected ISqlConnectionController controller => _controller ?? (_controller = GetOpenConnectionController());

        ISqlConnectionController GetOpenConnectionController()
        {
            _callContext = new SqlStandardCallContext( TestHelper.Monitor );
            var controller = _callContext[TestHelper.GetConnectionString()];
            controller.ExplicitOpen();
            return controller;
        }

        public static SqlConnection GetOpenConnection( bool mars = false )
        {
            var cs = ConnectionString;
            if( mars )
            {
                var scsb = new SqlConnectionStringBuilder( cs )
                {
                    MultipleActiveResultSets = true
                };
                cs = scsb.ConnectionString;
            }
            var connection = new SqlConnection( cs );
            connection.Open();
            return connection;
        }

        public SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection( ConnectionString );
            if( conn.State != ConnectionState.Closed ) throw new InvalidOperationException( "should be closed!" );
            return conn;
        }

        protected static CultureInfo ActiveCulture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
            set { Thread.CurrentThread.CurrentCulture = value; }
        }

        static TestBase()
        {
            Console.WriteLine( "Dapper: " + typeof( SqlMapper ).AssemblyQualifiedName );
            Console.WriteLine( "Using Connectionstring: {0}", ConnectionString );
            Console.WriteLine( ".NET: " + Environment.Version );
            TestHelper.OnlyOnce( () =>
            {
                TestHelper.LogToConsole = true;
                TestHelper.Monitor.Info( $"Current User: {Environment.UserDomainName}/{Environment.UserName}" );
                TestHelper.EnsureDatabase();
            } );
        }

        public void Dispose()
        {
            _callContext?.Dispose();
        }
    }

    [CollectionDefinition( Name, DisableParallelization = true )]
    public class NonParallelDefinition : TestBase
    {
        public const string Name = "NonParallel";
    }

}
