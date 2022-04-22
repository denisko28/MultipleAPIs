using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using HR_DAL.Connection.Abstract;
using HR_DAL.Exceptions;
using HR_DAL.Repositories.Abstract;

namespace HR_DAL.Repositories.Concrete
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly IConnectionFactory Connection;

        protected GenericRepository(IConnectionFactory connectionFactory)
        {
            Connection = connectionFactory;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            const string sql = "EXEC [GetAllProc] @Table_Name";
            var values = new { Table_Name = typeof(TEntity).Name};
            IEnumerable<TEntity> results = await Connection.Connect.QueryAsync<TEntity>(sql, values);
            return results;
        }

        public virtual async Task<TEntity> GetByIdAsync(int idParam)
        {
            const string? sql = "EXEC [GetByIdProc] @Table_Name, @Id";

            using (DbCommand command = (DbCommand)Connection.Connect.CreateCommand())
            {
                Type modelType = typeof(TEntity);
                if(Attribute.GetCustomAttribute(modelType, typeof(TableAttribute)) is not TableAttribute myAttribute)
                    throw new Exception("Entity doesn't have the Table attribute!");

                command.CommandText = sql;
                SqlParameter tableName = new SqlParameter("@Table_Name", myAttribute.Name);
                SqlParameter id = new SqlParameter("@Id", idParam);
                command.Parameters.Add(tableName);
                command.Parameters.Add(id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();

                        var instance = Activator.CreateInstance(modelType) as TEntity;
                        if (instance == null)
                            return instance ?? throw new Exception("Entity model doesn't correspond table!");
                        
                        PropertyInfo[] modelProps = modelType.GetProperties();
                        foreach (PropertyInfo srcProp in modelProps)
                        {
                            var value = reader[srcProp.Name];
                            srcProp.SetValue(instance, Convert.ChangeType(value, srcProp.PropertyType));
                        }
                        return instance ?? throw new Exception("Entity model doesn't correspond table!");
                    }
                    else 
                        throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(idParam));
                }
            }
        }

        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            const string? sql = "EXEC [GetByIdProc] @Table_Name, @Params, @Values";
            var values = new {
                Table_Name = typeof(TEntity).Name,
                Params = string.Join(", ", typeof(TEntity).GetProperties().Select(a => a.Name)),
                Values = string.Join(", ", typeof(TEntity).GetEnumValues()),
            };
            var added = await Connection.Connect.QuerySingleAsync<int>(sql, values);
            return added;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            var updated = await Connection.Connect.UpdateAsync(entity);
            return updated;
        }

        public virtual async Task DeleteByIdAsync(int idParam)
        {
            await GetByIdAsync(idParam);

            const string sql = "EXEC [DeleteByIdProc] @Table_Name, @Id";
            var values = new { Table_Name = typeof(TEntity).Name, Id = idParam };
            await Connection.Connect.ExecuteAsync(sql, values);
        }

        protected static string GetEntityNotFoundErrorMessage(int id) =>
            $"{typeof(TEntity).Name} with id {id} not found.";
    }
}
