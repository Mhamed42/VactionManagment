﻿using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;

namespace VactionManagment.Data
{
    public static class GeneralDBFunction
    {
       
        public static DataTable SqlDataTable(this DbContext context, string sqlQuery)
        {
            DbConnection conn = context.Database.GetDbConnection();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;

                var table = new DataTable();
                if (conn.State.Equals(ConnectionState.Closed)) { conn.Open(); }

                using (var reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                }

                if (conn.State.Equals(ConnectionState.Open)) { conn.Close(); }
                return table;
            }
        }
    }
}
