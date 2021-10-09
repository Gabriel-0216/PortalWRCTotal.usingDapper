using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class PostRepositorio : IRepository<Post>
    {
        IConfiguration _configuration;
        public PostRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnection()
        {
            var connection = $@"Server=DESKTOP-2L16HEL\SQLEXPRESS;Database=Blog;Trusted_Connection=True;MultipleActiveResultSets=true";
            return connection;
        }
        public int Add(Post objeto)
        {
            DynamicParameters param = new DynamicParameters();
            int rows = 0;
            var query = $@"INSERT INTO PostMelhorado(Assunto, Texto) VALUES (@Assunto, @Texto)";
            param.Add("Assunto", objeto.Assunto);
            param.Add("Texto", objeto.Texto);

            using(var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    rows = connection.Execute(query, param: param, commandType: System.Data.CommandType.Text);
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            return rows;

        }

        public Post Get(int id)
        {
            var postagem = new Post();
            DynamicParameters param = new DynamicParameters();
            var query = $@"SELECT Id, Assunto, Texto FROM PostMelhorado WHERE Id = @Id";
            param.Add("Id", id);
            using(var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    postagem = connection.Query<Post>(query, param: param, commandType: System.Data.CommandType.Text).FirstOrDefault();
                }
                catch(Exception)
                {
                    return null;
                }
                
            }
            return postagem;

        }

        public Post GetLast()
        {
            var postagem = new Post();
            var query = @$"SELECT TOP 1  Id, Assunto, Texto FROM PostMelhorado ORDER BY Id DESC";
            using(var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    postagem = connection.Query<Post>(query, commandType: System.Data.CommandType.Text).First(); ;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return postagem;
        }
        public List<Post> GetAll()
        {
            var listaPostagens = new List<Post>();

            var query = $@"SELECT ID, ASSUNTO, TEXTO FROM PostMelhorado";

            using(var connection = new SqlConnection(GetConnection()))
            {
                listaPostagens = connection.Query<Post>(query).ToList();
            }

            return listaPostagens;

        }

        public int Remove(int id)
        {
            int rows = 0;
            var query = $@"DELETE FROM PostMelhorado WHERE ID = @Id";
            var param = new DynamicParameters();
            param.Add("Id", id);
            using(var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    rows = connection.Execute(query, param: param, commandType: System.Data.CommandType.Text);

                }
                catch(Exception)
                {
                    return 0;
                }
            }

            return rows;
        }

        public int Update(Post objeto)
        {
            int rows = 0;
            var param = new DynamicParameters();
            var query = $@"UPDATE PostMelhorado SET ASSUNTO = @Assunto, TEXTO = @Texto WHERE ID = @Id";
            param.Add("Id", objeto.Id);
            param.Add("Assunto", objeto.Assunto);
            param.Add("Texto", objeto.Texto);
            using(var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    rows = connection.Execute(query, param: param, commandType: System.Data.CommandType.Text);
                }
                catch(Exception)
                {

                    return 0;
                }
            }
            return rows;
        }
    }
}
