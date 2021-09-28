using Dapper;
using Microsoft.AspNetCore.Http;
using ServiceModels;
using System;
using System.Collections.Generic;
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
        public CRUDGenerator(TInserData dataModel, IDbConnection connection, string commandText)
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
            var updateCommand = _dataUpdate + string.Join(",", enumerable.Where(s => s != "Id").Select(s => @"""" + s + @$"""=@{s}"))+ @"""UpdateDate""=now()" + " WHERE " + enumerable.Where(s => s == "Id").Select(s => @"""" + s + @$"""=@{s}").FirstOrDefault();
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
        public async Task<IEnumerable<TResponse>> GenerateSelectAndCount()
        {
            var selectCommand = _commandText;
            var cmd = new CommandDefinition(selectCommand, _dataModel, commandType: CommandType.Text);
            var dataResult = await _connection.QueryAsync<TResponse>(cmd);
            return dataResult;
        }
    }
}
