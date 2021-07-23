using BlogAPI.DB.BlogDB.IBlogDB;
using BlogAPI.DB.DBClass;
using BlogAPI.Models.Settings;
using Dapper;
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

		public BlogSettingModel GetBlogSetting(string uid)
		{
			string sql = "select * from TB_Blog_Setting where UID=@in_uid";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);

			return SystemDB.SingleQuery<BlogSettingModel>(str_conn, sql, _params);
		}

		public int Edit(string uid, BlogSettingModel model, string editor)
		{
			string sql = "UPDATE `TB_Blog_Setting`" +
						" SET `Title` = @in_title, `Status` = @in_status, `UpdateDate` = now(), `Editor` = @in_editor" +
						" WHERE `UID` = @in_uid ;";

			DynamicParameters _params = new DynamicParameters();
			_params.Add("@in_uid", uid, DbType.String, size: 255);
			_params.Add("@in_title", model.Title, DbType.String, size: 255);
			_params.Add("@in_status", model.Status, DbType.Int16);
			_params.Add("@in_editor", editor, DbType.String, size: 255);

			return SystemDB.Update(str_conn, sql, _params);
		}
		
	}
}
