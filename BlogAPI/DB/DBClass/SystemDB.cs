using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.DBClass
{
	public class SystemDB
	{
        private const string ReadOnlySession = "set session transaction isolation level read uncommitted";
        private const string ReadCommitSession = "set session transaction isolation level read committed";

        public static List<T> Query<T>(string connStr, string sqlStatement, object param = null,
            IDbTransaction trans = null, CommandType type = CommandType.Text)
        {

            List<T> result;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    conn.Execute(ReadOnlySession);
                    result = conn.Query<T>(sqlStatement, param, trans, commandType: type).AsList<T>();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return result;
        }

        public static async Task<List<T>> QueryAsync<T>(string connStr, string sqlStatement, object param = null,
            IDbTransaction trans = null, CommandType type = CommandType.Text)
        {
            List<T> result;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed) await conn.OpenAsync();
                    conn.Execute(ReadOnlySession);
                    result = (await conn.QueryAsync<T>(sqlStatement, param, trans, commandType: type)).AsList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return result;
        }



        public static T QueryFirstOrDefault<T>(string connStr, string sqlStatement, object param = null, IDbTransaction trans = null,
            CommandType type = CommandType.Text, bool NeedCommitData = false)
        {

            T result;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    if (NeedCommitData)
                    {
                        conn.Execute(ReadCommitSession);
                    }
                    else
                    {
                        conn.Execute(ReadOnlySession);
                    }
                    result = conn.QueryFirstOrDefault<T>(sqlStatement, param, trans, commandType: type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }

        public static async Task<T> QueryFirstOrDefaultAsync<T>(string connStr, string sqlStatement, object param = null, IDbTransaction trans = null,
            CommandType type = CommandType.Text, bool NeedCommitData = false)
        {
            T result;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed) await conn.OpenAsync();

                    if (NeedCommitData)
                    {
                        conn.Execute(ReadCommitSession);
                    }
                    else
                    {
                        conn.Execute(ReadOnlySession);
                    }
                    result = await conn.QueryFirstOrDefaultAsync<T>(sqlStatement, param, trans, commandType: type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }

        public static int Execute(string connStr, string sqlStatement, object param = null, bool DoTransaction = true)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                if (DoTransaction)
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            result = conn.Execute(sqlStatement, param, transaction);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                else
                {
                    try
                    {
                        result = conn.Execute(sqlStatement, param);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
            return result;
        }

        public static async Task<int> ExecuteAsync(string connStr, string sqlStatement, object param = null, bool DoTransaction = true)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                if (conn.State == ConnectionState.Closed) await conn.OpenAsync();
                if (DoTransaction)
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            result = await conn.ExecuteAsync(sqlStatement, param, transaction);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                else
                {
                    try
                    {
                        result = conn.Execute(sqlStatement, param);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
            return result;
        }


        public static T ExecuteScalar<T>(string connStr, string sqlStatement, object param = null)
        {
            T result;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        result = conn.ExecuteScalar<T>(sqlStatement, param, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }

            return result;
        }
        public static async Task<T> ExecuteScalarAsync<T>(string connStr, string sqlStatement, object param = null)
        {
            T result;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        result = await conn.ExecuteScalarAsync<T>(sqlStatement, param, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }

            return result;
        }
    }
}
