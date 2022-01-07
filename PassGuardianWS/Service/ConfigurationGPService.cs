using Dapper;
using PassGuardianWS.DAL;
using PassGuardianWS.Interfaces;
using PassGuardianWS.Models;
using System.Collections.Generic;

namespace PassGuardianWS.Service
{
    public class ConfigurationGPService : IConfigurationPG
    {
        private readonly Conexion conexion;

        public ConfigurationGPService()
        {
            conexion = new Conexion();
        }

        public bool Delete(Configuration entity)
        {
            string sql = $"DELETE FROM ConfigurationsPG WHERE UserID = {entity.UserID}";

            var result = conexion.DapperConexion.Execute(sql);

            return result > 0;
        }

        public Configuration GetById(int id)
        {
            string sql = $"SELECT * FROM ConfigurationsPG WHERE UserID = {id}";

            Configuration configuration = conexion.DapperConexion.QueryFirstOrDefault<Configuration>(sql);

            return configuration;
        }

        public bool Insert(Configuration entity)
        {
            string sql = "INSERT INTO ConfigurationsPG(UserID, ChangeFrequency, PasswordLength, Day, Week, Month)" +
                "VALUES(@UserID, @ChangeFrequency, @PasswordLength, @Day, @Week, @Month)";

            var result = conexion.DapperConexion.Execute(sql, entity);

            return result > 0;
        }

        public List<Configuration> List()
        {
            throw new System.NotImplementedException();
        }

        public bool Save(Configuration entity)
        {
            if (entity.ConfigurationID == 0)
                return Insert(entity);
            else
                return Update(entity);
        }

        public bool Update(Configuration entity)
        {
            string sql = $"UPDATE ConfigurationsPG SET UserID = @UserID, ChangeFrequency = @ChangeFrequency," +
                $"PasswordLength = @PasswordLength, Day = @Day, Week = @Week, Month = @Month WHERE ConfigurationID = {entity.ConfigurationID}";

            var result = conexion.DapperConexion.Execute(sql, entity);

            return result > 0;
        }
    }
}
