using Common.Attributes;
using Common.Helpers;
using Dapper;
using Microsoft.AspNetCore.Http;
using ServiceModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helper
{
    //CREATE,READ, UPDATE AND DELETE
    public class CRUDGenerator<TInserData, TDbModel, TResponse> where TInserData : new()
    {
        private readonly TInserData _dataModel;
        private readonly IDbConnection _connection;
        private readonly string _commandText;
        private readonly string _dataInsert = $@"INSERT INTO  ""{typeof(TDbModel).Name + "s"}""({string.Join(",", typeof(TInserData).GetProperties().Select(s => @"""" + s.Name + @""""))},""CreateDate"") VALUES ({string.Join(",", typeof(TInserData).GetProperties().Select(s => "@" + s.Name))},now())";
        private readonly string _dataUpdate = $@"UPDATE   ""{typeof(TDbModel).Name + "s"}"" SET ";
        private readonly string _dataDelete = $@"DELETE FROM   ""{typeof(TDbModel).Name + "s"}"" WHERE  ";
        public CRUDGenerator(TInserData dataModel, IDbConnection connection, string commandText = null)
        {
            _dataModel = dataModel;
            _connection = connection;
            _commandText = commandText;
        }
        // Insert method
        public async Task<BaseResponseModel> GenerateInsert()
        {
            var cmd = new CommandDefinition(_dataInsert, _dataModel, commandType: CommandType.Text);
            var dataResult = await _connection.ExecuteAsync(cmd);
            return new BaseResponseModel
            {
                Success = dataResult > 0,
                HttpStatusCode = dataResult > 0 ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest
            };
        }
        // Update method
        public async Task<BaseResponseModel> GenerateUpdate()
        {
            var names = typeof(TInserData).GetProperties().Select(s => s.Name);
            IEnumerable<string> enumerable = names as string[] ?? names.ToArray();
            var updateCommand = _dataUpdate + string.Join(",", enumerable.Where(s => s != "Id").Select(s => @"""" + s + @$"""=@{s}")) + @"""UpdateDate""=now()" + " WHERE " + enumerable.Where(s => s == "Id").Select(s => @"""" + s + @$"""=@{s}").FirstOrDefault();
            var cmd = new CommandDefinition(updateCommand, _dataModel, commandType: CommandType.Text);
            var dataResult = await _connection.ExecuteAsync(cmd);
            return new BaseResponseModel
            {
                Success = dataResult > 0,
                HttpStatusCode = dataResult > 0 ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest
            };
        }
        //Soft Delete method
        public async Task<BaseResponseModel> GenerateSoftDelete(int userId)
        {
            var softDeleteCommand = _dataUpdate + $@"""DeleteDate""='{DateTime.Now.ToUniversalTime().AddHours(4)}'" + $@" WHERE ""Id"" ={userId}";
            var cmd = new CommandDefinition(softDeleteCommand, _dataModel, commandType: CommandType.Text);
            var dataResult = await _connection.ExecuteAsync(cmd);
            return new BaseResponseModel
            {
                Success = dataResult > 0,
                HttpStatusCode = dataResult > 0 ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest
            };
        }
        //Delete method
        public async Task<BaseResponseModel> GenerateDelete()
        {
            var deleteCommand = _dataDelete + typeof(TInserData).GetProperties().Select(s => @"""" + s.Name + @$"""=@{s.Name}").FirstOrDefault();
            var cmd = new CommandDefinition(deleteCommand, _dataModel, commandType: CommandType.Text);
            var dataResult = await _connection.ExecuteAsync(cmd);
            return new BaseResponseModel
            {
                Success = dataResult > 0,
                HttpStatusCode = dataResult > 0 ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest

            };
        }
        //Generate Select and TotalCountQuery
        public async Task<(IEnumerable<TResponse>, int)> GenerateSelectAndCount(bool generationWhere = false)
        {
            var command = (string.IsNullOrEmpty(_commandText)? GenerationSelect():_commandText) + GenerateJoin()+ (generationWhere ? GenerateSelectWhere() : string.Empty);
            var cmd = new CommandDefinition(command + GenerationPagging(), _dataModel, commandType: CommandType.Text);
            var dataResult = await _connection.QueryAsync<TResponse>(cmd);
            var totalCount = await _connection.ExecuteScalarAsync<int>(command);
            return (dataResult, totalCount);
        }
        #region Private Method
        string GenerationPagging()
        {
            var pagging = new StringBuilder();
            foreach (var item in typeof(TInserData).GetProperties().Where(s => !Attribute.IsDefined(s, typeof(NotMappedAttribute))))
            {
                var value = item.GetValue(_dataModel, null);
                if (value != null)
                {
                    if (item.Name.ToLower() == "takeall" && (item.PropertyType == typeof(bool?) || item.PropertyType == typeof(bool)) && (bool)value)
                        break;
                    else if ((item.PropertyType == typeof(int?) || item.PropertyType == typeof(int)))
                    {
                        if (item.Name.ToLower() == "limit")
                            pagging.Append(@$"Limit {value}");
                        else if (item.Name.ToLower() == "offset")
                            pagging.Append(@$" offset {value}");
                    }
                }
            }
            return pagging.ToString();
        }
        string GenerateSelectWhere()
        {
            var where = new StringBuilder();
            where.Append(" Where TRUE ");
            var tableName=$@"""{typeof(TDbModel).Name + "s"}""";
            foreach (var item in typeof(TInserData).GetProperties().Where(s => !Attribute.IsDefined(s, typeof(NotMappedAttribute)) && !Attribute.IsDefined(s,typeof(NotWhereAttribute))))
            {
                var value = item.GetValue(_dataModel, null);
                if (value != null && value.ToString() != "0")
                {
                    if(Attribute.IsDefined(item, typeof(JoinTableAttribute)))
                    {
                        var attValue = item.GetCustomAttributes(typeof(JoinTableAttribute), true).Cast<JoinTableAttribute>().Single();
                        tableName= "_"+attValue.PropertyName.ToLower();
                    }
                    else
                        tableName = $@"""{typeof(TDbModel).Name + "s"}""";
                    if ((item.PropertyType == typeof(int?) || item.PropertyType == typeof(int)))
                        where.Append($@" AND {tableName}.""" + item.Name + @$"""={value}");
                    else if (item.PropertyType == typeof(string))
                        where.Append($@" AND {tableName}.""" + item.Name + @$""" LIKE N'%@{value}%'");
                    else if (item.PropertyType == typeof(bool?) || item.PropertyType == typeof(bool))
                        where.Append($@" AND {tableName}.""" + item.Name + @$"""={value}");
                    else if (item.PropertyType == typeof(decimal?) || item.PropertyType == typeof(bool))
                        where.Append($@" AND {tableName}.""" + item.Name + @$"""={value}");
                    else if (item.PropertyType == typeof(double?) || item.PropertyType == typeof(double))
                        where.Append($@" AND {tableName}.""" + item.Name + @$"""={value}");
                    else if (item.PropertyType.IsEnum)
                        where.Append($@" AND {tableName}.""" + item.Name + @$"""={(int)value}");
                    else if (item.PropertyType == typeof(IList<int>) || item.PropertyType == typeof(IList<int?>))
                        where.Append(@$" AND {tableName}.""" + item.Name + @""" in(" + string.Join(" , ", (IList<int>)value) + ")");
                    else if (item.PropertyType == typeof(List<int>) || item.PropertyType == typeof(List<int?>))
                        where.Append(@$" AND {tableName}.""" + item.Name + @""" in(" + string.Join(" , ", (List<int>)value) + ")");
                }
            }
            return where.ToString();
        }
        string GenerateJoin()
        {
            var join = new StringBuilder();
            foreach (var item in typeof(TInserData).GetProperties().Where(s => !Attribute.IsDefined(s, typeof(NotMappedAttribute))
             && Attribute.IsDefined(s, typeof(JoinTableAttribute))))
            {
                var attValue = item.GetCustomAttributes(typeof(JoinTableAttribute), true).Cast<JoinTableAttribute>().Single();
                    join.Append($@"{attValue.JoinType} JOIN ""{ attValue.TableName}"" AS _{attValue.PropertyName.ToLower()} ON _{attValue.PropertyName.ToLower()}.""{ attValue.TargetPropertyName}""=""{typeof(TDbModel).Name + "s"}"".""{attValue.PropertyName}""");
            }
            return join.ToString();
        }
        string GenerationSelect()
        {
            var select = new StringBuilder();
            select.Append("SELECT ");
            var tableName = $@"""{typeof(TDbModel).Name + "s"}""";
            var selectItems = typeof(TResponse).GetProperties().Where(s => !Attribute.IsDefined(s, typeof(NotMappedAttribute)) && !Attribute.IsDefined(s, typeof(NotWhereAttribute)));
            var lastItemName = selectItems.Last()?.Name;
            foreach (var item in selectItems)
            {
                if (Attribute.IsDefined(item, typeof(JoinTableAttribute)))
                {
                    var attValue = item.GetCustomAttributes(typeof(JoinTableAttribute), true).Cast<JoinTableAttribute>().Single();
                    tableName = "_" + attValue.PropertyName.ToLower();
                }
                else
                    tableName = $@"""{typeof(TDbModel).Name + "s"}""";
                select.Append($@" {tableName}.""" + item.Name+@""" "+ (lastItemName.Equals(item.Name)?string.Empty:","));
            }
            select.Append($@"From ""{typeof(TDbModel).Name + "s"}""");
            return select.ToString();
        }
        #endregion
    }
}
