using Dapper;
using PassGuardianWS.DAL;
using PassGuardianWS.Interfaces;
using PassGuardianWS.Models;
using PassGuardianWS.Utils;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace PassGuardianWS.Service
{
    public class PasswordService : ICRUD<Password>, IPassword, IListable<Password>
    {
        private readonly Conexion conexion;

        public PasswordService()
        {
            conexion = new Conexion();
        }

        public bool Delete(Password entity)
        {
            string sql = $"DELETE FROM Passwords WHERE PasswordID = {entity.PasswordID}";

            var resul = conexion.DapperConexion.Execute(sql);

            return resul > 0;
        }

        public string GeneratePassword(int userId)
        {
            IConfigurationPG configurationService = new ConfigurationGPService();
            Configuration configuration = configurationService.GetById(userId);
            Random random = new Random();
            string characters = @"abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890%$#@?*+<>!_-=/\|^{}[]()~`'.,";
            int longitud = characters.Length;
            char _char;

            string password = string.Empty;
            for (int i = 0; i <= configuration.PasswordLength; i++)
            {
                _char = characters[random.Next(longitud)];
                password += _char.ToString();
            }

            return password;
        }

        public Password GetById(int id)
        {
            string sql = $"SELECT * FROM Passwords WHERE PasswordID = {id}";

            Password password = conexion.DapperConexion.QueryFirstOrDefault<Password>(sql);

            return password;
        }

        public bool Insert(Password entity)
        {
            string sql = $"INSERT INTO Passwords(UserName, ApplicationPassword, KeyPassword, ApplicationName, AppIicationID, LastChange, UserID)" +
                $"VALUES(@UserName, @ApplicationPassword, @KeyPassword, @ApplicationName, @AppIicationID, @LastChange, @UserID)";

            //Encrypting
            Aes aes = Aes.Create();
            entity.ApplicationPassword = Convert.ToBase64String(Encryption.Encrypt(entity.ApplicationPassword, aes.Key, aes.IV));
            entity.KeyPassword = $"{Convert.ToBase64String(aes.Key)}#&{Convert.ToBase64String(aes.IV)}";

            var result = conexion.DapperConexion.Execute(sql, entity);

            return result > 0;
        }

        public List<Password> List()
        {
            string sql = $"SELECT * FROM Passwords";

            List<Password> passwords = conexion.DapperConexion.QueryFirstOrDefault<List<Password>>(sql);
            return passwords;
        }

        //Get list by user
        public List<Password> ListById(int userId)
        {
            string sql = $"SELECT * FROM Passwords WHERE UserID = {userId}";

            var passwords = conexion.DapperConexion.Query<Password>(sql);
            return new List<Password>(passwords);
        }

        public bool Save(Password entity)
        {
            entity.LastChange = DateTime.Now;
            if (entity.PasswordID == 0)
                return Insert(entity);
            else
                return Update(entity);
        }

        public bool Update(Password entity)
        {
            string sql = $"UPDATE Passwords SET UserName = @UserName, ApplicationPassword = @ApplicationPassword, KeyPassword = @KeyPassword, " +
                $"ApplicationName = @ApplicationName, AppIicationID = @AppIicationID, LastChange = @LastChange, UserID = @UserID " +
                $"WHERE PasswordID = {entity.PasswordID}";

            //Encrypting
            Aes aes = Aes.Create();
            entity.ApplicationPassword = Convert.ToBase64String(Encryption.Encrypt(entity.ApplicationPassword, aes.Key, aes.IV));
            entity.KeyPassword = $"{Convert.ToBase64String(aes.Key)}#&{Convert.ToBase64String(aes.IV)}";
            var result = conexion.DapperConexion.Execute(sql, entity);

            return result > 0;
        }
    }
}
