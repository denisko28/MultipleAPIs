using System;
using System.Collections.Generic;
using System.Data;
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
        protected readonly IDbConnection Connection;

        protected GenericRepository(IConnectionFactory connectionFactory)
        {
            Connection = connectionFactory.GetConnection();
            Connection.Open();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            const string sql = "EXEC [GetAllProc] @Table_Name";
            var values = new { Table_Name = typeof(TEntity).Name};
            IEnumerable<TEntity> results = await Connection.QueryAsync<TEntity>(sql, values);
            return results;
        }

        public virtual async Task<TEntity> GetByIdAsync(int idParam)
        {
            const string? sql = "EXEC [GetByIdProc] @Table_Name, @Id";

            await using (DbCommand command = (DbCommand)Connection.CreateCommand())
            {
                Type modelType = typeof(TEntity);
                if(Attribute.GetCustomAttribute(modelType, typeof(TableAttribute)) is not TableAttribute myAttribute)
                    throw new Exception("Entity doesn't have the Table attribute!");

                command.CommandText = sql;
                SqlParameter tableName = new SqlParameter("@Table_Name", myAttribute.Name);
                SqlParameter id = new SqlParameter("@Id", idParam);
                command.Parameters.Add(tableName);
                command.Parameters.Add(id);

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (!reader.HasRows) 
                        throw new EntityNotFoundException(typeof(TEntity).Name, idParam);
                    
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
            }
        }

        protected static bool IsNumericType(object o)
        {   
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            var propInfo = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(x => x.Name.Equals("Id"));

            if(propInfo != null)
                propInfo.SetValue(entity, 0);
            
            const string? sql = "EXEC [InsertProc] @Table_Name, @Params, @Values";
            var properties = typeof(TEntity).GetProperties()
                .Select(p=> p).Where(p => p.Name != "Id").ToArray();
            List<object> objValues = new();
            foreach (var property in properties)
            {
                var temp = property.GetValue(entity, null)!;
                if (temp.GetType().Name == "DateTime")
                    temp = ((DateTime) temp).ToString("yyyy-MM-dd");
                if (!IsNumericType(temp))
                    temp = "'" + temp + "'";
                objValues.Add(temp);
            }
            var values = new {
                Table_Name = typeof(TEntity).Name,
                Params = string.Join(", ", properties.Select(a => a.Name)),
                Values = string.Join(", ", objValues)
            };
            var added = await Connection.QuerySingleAsync<int>(sql, values);
            return added;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            var updated = await Connection.UpdateAsync(entity);
            return updated;
        }

        public virtual async Task DeleteByIdAsync(int idParam)
        {
            await GetByIdAsync(idParam);

            const string sql = "EXEC [DeleteByIdProc] @Table_Name, @Id";
            var values = new { Table_Name = typeof(TEntity).Name, Id = idParam };
            await Connection.ExecuteAsync(sql, values);
        }
        
        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
