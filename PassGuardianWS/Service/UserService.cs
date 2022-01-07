using Dapper;
using PassGuardianWS.DAL;
using PassGuardianWS.Interfaces;
using PassGuardianWS.Models;
using System.Collections.Generic;

namespace PassGuardianWS.Service
{
    public class UserService : IUser
    {

        private readonly Conexion conexion;

        public UserService()
        {
            conexion = new Conexion();
        }

        public bool Delete(User entity)
        {
            string sql = $"DELETE FROM Users WHERE UserID = {entity.UserId}";
            var resul = conexion.DapperConexion.Execute(sql);

            return resul > 0;
        }

        public User GetById(int id)
        {
            string sql = $"SELECT * FROM Users WHERE UserID = {id}";
            User user = conexion.DapperConexion.QueryFirstOrDefault<User>(sql);

            return user;
        }

        

        public bool Insert(User entity)
        {
           
            string sql = "INSERT INTO Users(Email, UserName, Password) VALUES(@Email, @UserName, @Password)";
            var result = conexion.DapperConexion.Execute(sql, entity);

            
            return result > 0;
        }

        public bool Update(User entity)
        {
            string sql = $"UPDATE Users SET Email = @Email, UserName = @UserName, Password = @Password WHERE UserID = {entity.UserId}";
            var result = conexion.DapperConexion.Execute(sql, entity);

            return result > 0;
        }

        public bool Save(User user)
        {
            if (user.UserId == 0)
                return Insert(user);
            else
                return Update(user);
        }
    }
}
