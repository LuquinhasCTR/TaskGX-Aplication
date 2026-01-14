using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TaskGX.Dados
{
    public static class LigacaoDB
    {
        /// <summary>
        /// Obter a string de ligação (connection string) ao servidor de base de dados.
        /// Puxa do ficheiro App.config.
        /// </summary>
        public static string GetConnectionString()
        {
            return ConfigurationManager
                   .ConnectionStrings["MySqlConnection"]
                   .ConnectionString;
        }
    }
}