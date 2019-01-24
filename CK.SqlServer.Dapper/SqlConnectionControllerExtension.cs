using CK.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper
{
    public static class SqlConnectionControllerExtension
    {

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The number of rows affected.</returns>
        public static int Execute( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            return SqlMapper.Execute( cnn.GetDbConnection(), sql, param, cnn.Transaction, commandTimeout, commandType );
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <param name="cnn">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell selected as <see cref="object"/>.</returns>
        public static object ExecuteScalar( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.ExecuteScalar( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="cnn">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as <typeparamref name="T"/>.</returns>
        public static T ExecuteScalar<T>( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.ExecuteScalar<T>( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute parameterized SQL and return an <see cref="IDataReader"/>.
        /// </summary>
        /// <param name="cnn">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An <see cref="IDataReader"/> that can be used to iterate over the results of the SQL query.</returns>
        /// <remarks>
        /// This is typically used when the results of a query are not processed by Dapper, for example, used to fill a <see cref="DataTable"/>
        /// or <see cref="T:DataSet"/>.
        /// </remarks>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// DataTable table = new DataTable("MyTable");
        /// using (var reader = ExecuteReader(cnn, sql, param))
        /// {
        ///     table.Load(reader);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static IDataReader ExecuteReader( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.ExecuteReader( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Return a sequence of dynamic objects with properties matching the columns.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <remarks>Note: each row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        public static IEnumerable<dynamic> Query( this ISqlConnectionController cnn, string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.Query( cnn.Connection, sql, param, cnn.Transaction, buffered, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <remarks>Note: the row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        public static dynamic QueryFirst( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QueryFirst( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <remarks>Note: the row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        public static dynamic QueryFirstOrDefault( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QueryFirstOrDefault( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }


        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <remarks>Note: the row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        public static dynamic QuerySingle( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QuerySingle( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <remarks>Note: the row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        public static dynamic QuerySingleOrDefault( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QuerySingleOrDefault( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Executes a query, returning the data typed as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="buffered">Whether to buffer results in memory.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static IEnumerable<T> Query<T>( this ISqlConnectionController cnn, string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.Query<T>( cnn.Connection, sql, param, cnn.Transaction, buffered, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static T QueryFirst<T>( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QueryFirst<T>( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static T QueryFirstOrDefault<T>( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QueryFirstOrDefault<T>( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static T QuerySingle<T>( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QuerySingle<T>( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static T QuerySingleOrDefault<T>( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QuerySingleOrDefault<T>( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as <paramref name="type"/>.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="buffered">Whether to buffer results in memory.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static IEnumerable<object> Query( this ISqlConnectionController cnn, Type type, string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.Query( cnn.Connection, type, sql, param, cnn.Transaction, buffered, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as <paramref name="type"/>.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static object QueryFirst( this ISqlConnectionController cnn, Type type, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QueryFirst( cnn.Connection, type, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as <paramref name="type"/>.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static object QueryFirstOrDefault( this ISqlConnectionController cnn, Type type, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QueryFirstOrDefault( cnn.Connection, type, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as <paramref name="type"/>.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static object QuerySingle( this ISqlConnectionController cnn, Type type, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QuerySingle( cnn.Connection, type, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as <paramref name="type"/>.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static object QuerySingleOrDefault( this ISqlConnectionController cnn, Type type, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QuerySingleOrDefault( cnn.Connection, type, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static SqlMapper.GridReader QueryMultiple( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.QueryMultiple( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a multi-mapping query with 2 input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.Query( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }


        /// <summary>
        /// Perform a multi-mapping query with 3 input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.Query( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a multi-mapping query with 4 input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.Query( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a multi-mapping query with 5 input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.Query( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a multi-mapping query with 6 input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.Query( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a multi-mapping query with 7 input types. If you need more types -> use Query with Type[] parameter.
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TSeventh">The seventh type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.Query( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a multi-mapping query with an arbitrary number of input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="types">Array of types in the recordset.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static IEnumerable<TReturn> Query<TReturn>( this ISqlConnectionController cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.ExplicitOpen() )
            {
                return SqlMapper.Query( cnn.Connection, sql, types, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }


        /// <summary>
        /// Execute a query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <remarks>Note: each row can be accessed via "dynamic", or by casting to an IDictionary&lt;string,object&gt;</remarks>
        public static Task<IEnumerable<dynamic>> QueryAsync( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( cnn.IsExplicitlyOpened ? cnn.ExplicitOpenAsync() : null )
            {
                return SqlMapper.QueryAsync( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="flags">The behavior flags for this command.</param>
        /// <param name="cancellationToken">The cancellation token for this command.</param>
        /// <returns>
        /// A sequence of data of <typeparamref name="T"/>; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static async Task<IEnumerable<T>> QueryAsync<T>( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryAsync<T>( cnn.Connection, new CommandDefinition( sql, param, cnn.Transaction, commandTimeout, commandType, flags, cancellationToken ) );
            }
        }

        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        public static async Task<T> QueryFirstAsync<T>( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryFirstAsync<T>( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        public static async Task<T> QueryFirstOrDefaultAsync<T>( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryFirstAsync<T>( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        public static async Task<T> QuerySingleAsync<T>( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QuerySingleAsync<T>( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        public static async Task<T> QuerySingleOrDefaultAsync<T>( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QuerySingleOrDefaultAsync<T>( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        public static async Task<dynamic> QueryFirstAsync( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryFirstAsync( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }


        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        public static async Task<dynamic> QueryFirstOrDefaultAsync( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryFirstOrDefaultAsync( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        public static async Task<dynamic> QuerySingleAsync( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QuerySingleAsync( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        public static async Task<dynamic> QuerySingleOrDefaultAsync( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QuerySingleOrDefaultAsync( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static async Task<IEnumerable<object>> QueryAsync( this ISqlConnectionController cnn, Type type, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryAsync( cnn.Connection, type, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static async Task<object> QueryFirstAsync( this ISqlConnectionController cnn, Type type, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryFirstAsync( cnn.Connection, type, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static async Task<object> QueryFirstOrDefaultAsync( this ISqlConnectionController cnn, Type type, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryFirstOrDefaultAsync( cnn.Connection, type, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static async Task<object> QuerySingleAsync( this ISqlConnectionController cnn, Type type, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QuerySingleAsync( cnn.Connection, type, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static async Task<object> QuerySingleOrDefaultAsync( this ISqlConnectionController cnn, Type type, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QuerySingleOrDefaultAsync( cnn.Connection, type, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a command asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> ExecuteAsync( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.ExecuteAsync( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }


        /// <summary>
        /// Perform a asynchronous multi-mapping query with 2 input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryAsync( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a asynchronous multi-mapping query with 3 input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryAsync( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a asynchronous multi-mapping query with 4 input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryAsync( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a asynchronous multi-mapping query with 5 input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryAsync( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a asynchronous multi-mapping query with 6 input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryAsync( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a asynchronous multi-mapping query with 7 input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TSeventh">The seventh type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>( this ISqlConnectionController cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryAsync( cnn.Connection, sql, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Perform a asynchronous multi-mapping query with an arbitrary number of input types. 
        /// This returns a single type, combined from the raw types via <paramref name="map"/>.
        /// </summary>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="types">Array of types in the recordset.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of <typeparamref name="TReturn"/>.</returns>
        public static async Task<IEnumerable<TReturn>> QueryAsync<TReturn>( this ISqlConnectionController cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryAsync( cnn.Connection, sql, types, map, param, cnn.Transaction, buffered, splitOn, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="cnn">This SqlConnectionController.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        public static async Task<SqlMapper.GridReader> QueryMultipleAsync( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.QueryMultipleAsync( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute parameterized SQL and return an <see cref="IDataReader"/>.
        /// </summary>
        /// <param name="cnn">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An <see cref="IDataReader"/> that can be used to iterate over the results of the SQL query.</returns>
        /// <remarks>
        /// This is typically used when the results of a query are not processed by Dapper, for example, used to fill a <see cref="DataTable"/>
        /// or <see cref="T:DataSet"/>.
        /// </remarks>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// DataTable table = new DataTable("MyTable");
        /// using (var reader = ExecuteReader(cnn, sql, param))
        /// {
        ///     table.Load(reader);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public static async Task<IDataReader> ExecuteReaderAsync( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.ExecuteReaderAsync( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <param name="cnn">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as <see cref="object"/>.</returns>
        public static async Task<object> ExecuteScalarAsync( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.ExecuteScalarAsync( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="cnn">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as <typeparamref name="T"/>.</returns>
        public static async Task<T> ExecuteScalarAsync<T>( this ISqlConnectionController cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null )
        {
            using( await cnn.ExplicitOpenAsync().ConfigureAwait( false ) )
            {
                return await SqlMapper.ExecuteScalarAsync<T>( cnn.Connection, sql, param, cnn.Transaction, commandTimeout, commandType );
            }
        }

    }
}
