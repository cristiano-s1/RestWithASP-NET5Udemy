using System;
using System.Linq;
using System.Text;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Data.VO;
using System.Security.Cryptography;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository
{
    public class UserRepository : IUserRepository
    {
        // Injeção contexto
        private readonly RestFullContext _context;

        public UserRepository(RestFullContext context)
        {
            _context = context;
        }

        // Validar as credenciais do usuário
        public User ValidateCredentials(UserVO user)
        {
            //Criptografar a senha para depois comparar
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }

        public User ValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => (u.UserName == userName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => (u.UserName == userName));
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }
       
        //Atualizar informações de usuário
        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        // Método responsável por criptografar a senha
        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }


    }
}
