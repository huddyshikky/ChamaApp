﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Repository
{
    //public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    //{ 
    //    private readonly string _tableName; 
    //    private SqlConnection sqlConnection() 
    //    {
    //        string Id = "Default"
    //        return new sqlConnection(ConfigurationManager.ConnectionStrings[Id].ToString()); 
    //    }

    //    private IDbConnection CreateConnection()
    //    {
    //        var conn = sqlConnection();
    //        conn.Open();
    //        return conn;
    //    }
    //    private IEnumerable<PropertyInfo> GetProperties=>typeof(T).GetProperties();
    //    private static List GenerateListOfProperties(IEnumerable listOfProperties)
    //    {
    //        return (from prop in listOfProperties
    //                let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
    //                where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
    //                select prop.Name).ToList();
    //    }


    //    public GenericRepository(string tableName)
    //    {
    //       _tableName=tableName;
    //    }
    //    public async Task DeleteRowAsync(Guid id)
    //    {
    //        using (var connection = CreateConnection())
    //        {
    //            await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id=@Id", new { Id = id });
    //        }
    //    }

    //    public async Task<IEnumerable> GetAllAsync()
    //    {
    //        using (var connection = CreateConnection())
    //        {
    //            return await connection.QueryAsync($"SELECT * FROM {_tableName}");
    //        }
    //    }

    //    public async Task GetAsync(Guid id)
    //    {
    //        using (var connection = CreateConnection())
    //        {
    //            var result = await connection.QuerySingleOrDefaultAsync($"SELECT * FROM {_tableName} WHERE Id=@Id", new { Id = id });
    //            if (result == null)
    //                throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");

    //            return result;
    //        }
    //    }
    //    public async Task SaveRangeAsync(IEnumerable list)
    //    {
    //        var inserted = 0;
    //        var query = GenerateInsertQuery();
    //        using (var connection = CreateConnection())
    //        {
    //            inserted += await connection.ExecuteAsync(query, list);
    //        }

    //        return inserted;
    //    }

    //    public async Task InsertAsync(T t)
    //    {
    //        var insertQuery = GenerateInsertQuery();

    //        using (var connection = CreateConnection())
    //        {
    //            await connection.ExecuteAsync(insertQuery, t);
    //        }
    //    }

    //    private string GenerateInsertQuery()
    //    {
    //        var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");

    //        insertQuery.Append("(");

    //        var properties = GenerateListOfProperties(GetProperties);
    //        properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

    //        insertQuery
    //            .Remove(insertQuery.Length - 1, 1)
    //            .Append(") VALUES (");

    //        properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

    //        insertQuery
    //            .Remove(insertQuery.Length - 1, 1)
    //            .Append(")");

    //        return insertQuery.ToString();
    //    }


    //    public async Task UpdateAsync(T t)
    //    {
    //        var updateQuery = GenerateUpdateQuery();

    //        using (var connection = CreateConnection())
    //        {
    //            await connection.ExecuteAsync(updateQuery, t);
    //        }
    //    }

    //    private string GenerateUpdateQuery()
    //    {
    //        var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
    //        var properties = GenerateListOfProperties(GetProperties);

    //        properties.ForEach(property =>
    //        {
    //            if (!property.Equals("Id"))
    //            {
    //                updateQuery.Append($"{property}=@{property},");
    //            }
    //        });

    //        updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
    //        updateQuery.Append(" WHERE Id=@Id");

    //        return updateQuery.ToString();
    //    }
    //}
}
