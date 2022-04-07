using System;
using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Data.Common;
using MultipleAPIs.HR_DAL.Connection.Abstract;
using MultipleAPIs.HR_DAL.Repositories.Abstract;
using MultipleAPIs.HR_DAL.Exceptions;

namespace MultipleAPIs.HR_DAL.Repositories.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected IConnectionFactory connection;
        public GenericRepository(IConnectionFactory connectionFactory)
        {
            connection = connectionFactory;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var sql = "EXEC [GetAllProc] @Table_Name";
            var values = new { Table_Name = typeof(TEntity).Name};
            IEnumerable<TEntity> results = await connection.Connect.QueryAsync<TEntity>(sql, values);
            return results;
        }

        public async Task<TEntity> GetByIdAsync(int IdParam)
        {
            var sql = "EXEC [GetByIdProc] @Table_Name, @Id";
            //var values = new { Table_Name = typeof(TEntity).Name, Id = IdParam };
            //TEntity result = await connection.Connect.QuerySingleAsync<TEntity>(sql, values);
            //return result;

            using (DbCommand command = (DbCommand)connection.Connect.CreateCommand())
            {
                Type modelType = typeof(TEntity);
                var MyAttribute = Attribute.GetCustomAttribute(modelType, typeof(TableAttribute)) as TableAttribute;
                if(MyAttribute == null)
                    throw new Exception("Entity doesn't have the Table attribute!");

                command.CommandText = sql;
                SqlParameter tableName = new SqlParameter("@Table_Name", MyAttribute.Name);
                SqlParameter Id = new SqlParameter("@Id", IdParam);
                command.Parameters.Add(tableName);
                command.Parameters.Add(Id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();

                        TEntity? instance = Activator.CreateInstance(modelType) as TEntity;
                        if (instance != null)
                        {
                            PropertyInfo[] modelProps = modelType.GetProperties();
                            foreach (PropertyInfo srcProp in modelProps)
                            {
                                var value = reader[srcProp.Name];
                                srcProp.SetValue(instance, Convert.ChangeType(value, srcProp.PropertyType));
                            }
                        }
                        return instance ?? throw new Exception("Enity model doesn't correspond table!");
                    }
                    else 
                        throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(IdParam));
                }
            }
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            var sql = "EXEC [GetByIdProc] @Table_Name, @Params, @Values";
            var values = new {
                Table_Name = typeof(TEntity).Name,
                Params = string.Join(", ", typeof(TEntity).GetProperties().Select(a => a.Name)),
                Values = string.Join(", ", typeof(TEntity).GetEnumValues()),
            };
            var added = await connection.Connect.QuerySingleAsync<int>(sql, values);
            return added;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var updated = await connection.Connect.UpdateAsync<TEntity>(entity);
            return updated;
        }

        public async Task DeleteByIdAsync(int IdParam)
        {
            // var deleted = await SqlMapperExtensions.DeleteAsync<TEntity>(connection.Connect, Id);
            // return deleted;

            await GetByIdAsync(IdParam);

            var sql = "EXEC [DeleteByIdProc] @Table_Name, @Id";
            var values = new { Table_Name = typeof(TEntity).Name, Id = IdParam };
            await connection.Connect.ExecuteAsync(sql, values);
        }

        protected static string GetEntityNotFoundErrorMessage(int id) =>
            $"{typeof(TEntity).Name} with id {id} not found.";
    }
}
