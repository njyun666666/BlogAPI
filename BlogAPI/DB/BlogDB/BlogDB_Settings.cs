using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.DB.DBClass;
using BlogAPI.Enums;
using BlogAPI.Models.Settings;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DB.BlogDB
{
	public class BlogDB_Settings : IBlogDB_Settings
	{
		private string str_conn;

		public BlogDB_Settings(IDBConnection dBConnection)
		{
			str_conn = dBConnection.ConnectionBlogDB();
		}

		public async Task<BlogSettingModel> GetBlogSetting(string uid)
		{
			string sql = "select * from TB_Blog_Setting where UID=@in_uid";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);

			return await SystemDB.SingleQueryAsync<BlogSettingModel>(str_conn, sql, _params);
		}

		public async Task<int> Edit(string uid, BlogSettingModel model, string editor)
		{
			string sql = "UPDATE `TB_Blog_Setting`" +
						" SET `Title` = @in_title, `Status` = @in_status, `UpdateDate` = now(), `Editor` = @in_editor" +
						" WHERE `UID` = @in_uid ;";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);
			_params.Add("@in_title", model.Title.Trim(), DbType.String, size: 255);
			_params.Add("@in_status", model.Status, DbType.Int16);
			_params.Add("@in_editor", editor, DbType.String, size: 255);

			return await SystemDB.ExecuteAsync(str_conn, sql, _params);
		}

		public async Task<List<ArticleTypeModel>> ArticleTypeGet(string uid)
		{
			string sql = "select * from TB_Article_Type where UID=@in_uid and Status=1 order by Sort";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);

			return await SystemDB.QueryAsync<ArticleTypeModel>(str_conn, sql, _params);
		}
		public async Task<int> ArticleTypeAdd(string uid, string name)
		{
			int result = 0;

			name = name.Trim();

			using (MySqlConnection conn = new MySqlConnection(str_conn))
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				using (var transaction = conn.BeginTransaction())
				{
					try
					{
						// 判斷有沒有重複的類型
						string sql = "select exists(select 1 from TB_Article_Type where UID=@in_uid and Status=1 and name=@in_name)";

						DynamicParameters _params = new DynamicParameters();
						_params.Add("@in_uid", uid, DbType.String, size: 255);
						_params.Add("@in_name", name, DbType.String, size: 50);
						bool repeat = await conn.QueryFirstOrDefaultAsync<bool>(sql, _params, transaction);

						if (repeat)
						{
							return (int)ReturnCodeEnum.name_repeat;
						}


						// 取得最大的sort
						string sql_sort = "select sort from TB_Article_Type where UID=@in_uid and Status=1 order by sort desc limit 1";

						DynamicParameters _params_sort = new DynamicParameters();
						_params_sort.Add("@in_uid", uid, DbType.String, size: 255);
						int sort = await conn.QueryFirstOrDefaultAsync<int>(sql_sort, _params_sort, transaction);
						sort += 1;


						// insert
						string sql_insert = "INSERT INTO `TB_Article_Type`" +
											" (`Name`,`UID`,`Status`,`Sort`,`CreateDate`)" +
											" VALUES(@in_name, @in_uid, 1, @in_sort, now());";

						DynamicParameters _params_insert = new DynamicParameters();
						_params_insert.Add("@in_uid", uid, DbType.String, size: 255);
						_params_insert.Add("@in_name", name, DbType.String, size: 50);
						_params_insert.Add("@in_sort", sort, DbType.Int32);

						result = await conn.ExecuteAsync(sql_insert, _params_insert, transaction);

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
		public async Task<int> ArticleTypeEdit(string uid, string name, Int64 id)
		{
			int result = 0;

			name = name.Trim();

			using (MySqlConnection conn = new MySqlConnection(str_conn))
			{
				if (conn.State == ConnectionState.Closed) conn.Open();
				using (var transaction = conn.BeginTransaction())
				{
					try
					{
						// 判斷有沒有重複的類型
						string sql = "select exists(select 1 from TB_Article_Type where UID=@in_uid and Status=1 and name=@in_name)";

						DynamicParameters _params = new DynamicParameters();
						_params.Add("@in_uid", uid, DbType.String, size: 255);
						_params.Add("@in_name", name, DbType.String, size: 50);
						bool repeat = await conn.QueryFirstOrDefaultAsync<bool>(sql, _params, transaction);

						if (repeat)
						{
							return (int)ReturnCodeEnum.name_repeat;
						}




						// update
						string sql_update = " UPDATE `TB_Article_Type`" +
											" SET" +
											" `ID` = @in_id," +
											" `Name` = @in_name," +
											" `UpdateDate` = now()" +
											"  WHERE `ID` = @in_id and `UID`=@in_uid ;";


						DynamicParameters _params_update = new DynamicParameters();
						_params_update.Add("@in_id", id, DbType.Int64);
						_params_update.Add("@in_uid", uid, DbType.String, size: 255);
						_params_update.Add("@in_name", name, DbType.String, size: 50);

						result = await conn.ExecuteAsync(sql_update, _params_update, transaction);

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
		public async Task<int> ArticleTypeDelete(string uid, Int64 id)
		{
			string sql = "UPDATE `TB_Article_Type` SET `Status` = 0 WHERE `ID` = @in_id and `UID`=@in_uid ;";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_id", id, DbType.Int64);
			_params.Add("@in_uid", uid, DbType.String, size: 255);

			return await SystemDB.ExecuteAsync(str_conn, sql, _params);
		}
		public async Task<int> ArticleTypeSortEdit(string uid, List<int> ids)
		{
			string sql = "";
			int i = 1;

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);

			ids.ForEach(x =>
			{
				sql += $"UPDATE `TB_Article_Type` SET `Sort` = {i} WHERE `ID` = @in_id_{i} and `UID`=@in_uid ;";
				_params.Add("@in_id_" + i, x, DbType.Int32);
				i++;
			});

			return await SystemDB.ExecuteAsync(str_conn, sql, _params);
		}
	}
}
