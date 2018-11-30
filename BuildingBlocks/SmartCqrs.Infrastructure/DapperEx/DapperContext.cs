using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SmartCqrs.Infrastructure.DapperEx
{
    public class DapperContext : IDisposable
    {
        public IDbConnection DbConnection { get; }

        public DapperContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("数据库连接字符串不能为空");
            }

            // 使用Dapper查询时，如果数据库字段采用snake_case命名法（如 user_name），但对应的实体类字段采用驼峰命名法（如UserName），
            // 查出来的字段数据无法绑定到相应的属性上，要解决此方法可以设置如下属性，参考文章如下
            //https://andrewlock.net/using-snake-case-column-names-with-dapper-and-postgresql/
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            DbConnection = new NpgsqlConnection(connectionString);
        }

        public IDbTransaction BeginTransaction() => DbConnection.BeginTransaction();
        public IDbTransaction BeginTransaction(IsolationLevel il) => DbConnection.BeginTransaction(il);
        public void ChangeDatabase(string databaseName) => DbConnection.ChangeDatabase(databaseName);
        public void Close() => DbConnection.Close();
        public IDbCommand CreateCommand() => DbConnection.CreateCommand();

        public int Execute(string sql, object param = null) => DbConnection.Execute(sql, param);
        public IDataReader ExecuteReader(string sql, object param = null) => DbConnection.ExecuteReader(sql, param);
        public object ExecuteScalar(string sql, object param = null) => DbConnection.ExecuteScalar(sql, param);
        public SqlMapper.GridReader QueryMultiple(string sql, object param = null) => DbConnection.QueryMultiple(sql, param);

        public IEnumerable<dynamic> Query(string sql, object param = null) => DbConnection.Query(sql, param);
        public dynamic QueryFirst(string sql, object param = null) => DbConnection.QueryFirst(sql, param);
        public dynamic QueryFirstOrDefault(string sql, object param = null) => DbConnection.QueryFirstOrDefault(sql, param);
        public dynamic QuerySingle(string sql, object param = null) => DbConnection.QuerySingle(sql, param);
        public dynamic QuerySingleOrDefaul(string sql, object param = null) => DbConnection.QuerySingleOrDefault(sql, param);

        public IEnumerable<T> Query<T>(string sql, object param = null) => DbConnection.Query<T>(sql, param);
        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            DbConnection.Query<TFirst, TSecond, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);

        public T QueryFirst<T>(string sql, object param = null) => DbConnection.QueryFirst<T>(sql, param);
        public T QueryFirstOrDefault<T>(string sql, object param = null) => DbConnection.QueryFirstOrDefault<T>(sql, param);
        public T QuerySingle<T>(string sql, object param = null) => DbConnection.QuerySingle<T>(sql, param);
        public T QuerySingleOrDefault<T>(string sql, object param = null) => DbConnection.QuerySingleOrDefault<T>(sql, param);

        public Task<int> ExecuteAsync(string sql, object param = null) => DbConnection.ExecuteAsync(sql, param);
        public Task<IDataReader> ExecuteReaderAsync(string sql, object param = null) => DbConnection.ExecuteReaderAsync(sql, param);
        public Task<object> ExecuteScalarAsync(string sql, object param = null) => DbConnection.ExecuteScalarAsync(sql, param);
        public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null) => DbConnection.QueryMultipleAsync(sql, param);

        public Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null) => DbConnection.QueryAsync(sql, param);
        public Task<dynamic> QueryFirstAsync(string sql, object param = null) => DbConnection.QueryFirstAsync(sql, param);
        public Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null) => DbConnection.QueryFirstOrDefaultAsync(sql, param);
        public Task<dynamic> QuerySingleAsync(string sql, object param = null) => DbConnection.QuerySingleAsync(sql, param);
        public Task<dynamic> QuerySingleOrDefaultAsync(string sql, object param = null) => DbConnection.QuerySingleOrDefaultAsync(sql, param);

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null) => DbConnection.QueryAsync<T>(sql, param);
        public Task<T> QueryFirstAsync<T>(string sql, object param = null) => DbConnection.QueryFirstAsync<T>(sql, param);
        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null) => DbConnection.QueryFirstOrDefaultAsync<T>(sql, param);
        public Task<T> QuerySingleAsync<T>(string sql, object param = null) => DbConnection.QuerySingleAsync<T>(sql, param);
        public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null) => DbConnection.QuerySingleOrDefaultAsync<T>(sql, param);

        public PagedData<T> GetPagedData<T>(string sql, object param = null, int pageSize = 20, int pageNumber = 1)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentException($"无效的{nameof(pageNumber)}参数值：{pageNumber}");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException($"无效的{nameof(pageSize)}参数值：{pageSize}");
            }

            if(string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException($"{nameof(sql)}参数值不能为空");
            }

            PagedData<T> pagedData = new PagedData<T>() { PageNumber = pageNumber, PageSize = pageSize };
            pagedData.TotalCount = Convert.ToInt32(ExecuteScalar($"select count(1) from ({sql}) t;", param));
            pagedData.TotalPage = Convert.ToInt32(Math.Ceiling(pagedData.TotalCount * 1.0M / pagedData.PageSize));

            int offset = 0;
            if(pageNumber > 1)
            {
                offset = (pageNumber - 1) * pageSize;
            }
            string pageSql = $"{sql} limit {pageSize} offset {offset};";
            pagedData.List = Query<T>(pageSql, param);

            return pagedData;
        }

        public Task<PagedData<T>> GetPagedDataAsync<T>(string sql, object param = null, int pageSize = 20, int pageNumber = 1)
        {
            return Task.FromResult(GetPagedData<T>(sql, param, pageSize, pageNumber));
        }

        public void Dispose()
        {
            if (DbConnection != null)
            {
                DbConnection.Close();
                DbConnection.Dispose();
            }
        }
    }
}
