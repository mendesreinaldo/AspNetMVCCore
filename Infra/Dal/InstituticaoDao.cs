using Infra.Interface;
using Microsoft.Extensions.Configuration;
using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Infra.Dal
{
    public class InstituticaoDao : IInstituicaoDao
    {
        readonly string _connectionString;

        public InstituticaoDao(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConnectionStrings");
        }

        public IQueryable<Instituicao> ObterInstituicoesClassificadaPorNome()
        {

            //try
            //{
            //    var sql = @"select InstituicaoID,Nome,Endereco from Instituicoes";

            //    using (var con = new SqlConnection(_connectionString))
            //    {
            //        var cmd = new SqlCommand(sql, con)
            //        {
            //            CommandType = CommandType.Text
            //        };

            //        con.Open();

            //        var reader = cmd.ExecuteReader();

            //        var instituicoes = new Instituicao[] { };

            //        while (reader.Read())
            //        {
            //            new Instituicao { InstituicaoID = Convert.ToInt32(reader["InstituicaoID"].ToString()), }  
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            throw new NotImplementedException();
        }
    }
}
