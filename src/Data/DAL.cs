using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Data;

namespace MyBook.Data {
    /*
        Steps
        [X] Open and Close Connection
        [X] Add Params
        [X] Execute the instruction

    */
    public class DAL : IDisposable {
        private SqliteConnection Connection {get;set;}
        private ICollection<SqliteParameter> Parameters {get;set;}
        private readonly string ConnectionString;
        public DAL(string connection) {
           ConnectionString = connection;
           Connection = new  SqliteConnection(ConnectionString);                            
           Parameters = new List<SqliteParameter>();
        }

        private void OpenConnection() {                  
            Connection.Open();
        }
        public void AddParameter(string parameterName, object value) {
            Parameters.Add(new SqliteParameter(parameterName, value));
        }
        
        private void AddParameterToCommand(SqliteCommand command) {
            if(Parameters == null) 
                return;
                
            foreach(SqliteParameter param in Parameters) {
                command.Parameters.Add(param);
            }
        }

        private SqliteCommand PrepareCommand(string query) {     
            OpenConnection();                
            var command = new SqliteCommand(query,this.Connection);
            AddParameterToCommand(command);            
            return command;
        }

        public void ExecuteNonQuery(string query) {
            try {            
                var command = PrepareCommand(query);                
                command.ExecuteNonQuery();                                
            }catch(SqliteException ex) {
                throw new SqliteException("Problema na conexão com o banco de dados",ex.ErrorCode);
            }
        }

        public object ExecuteScalar(string query) {
            try {            
                var command = PrepareCommand(query);                
                return command.ExecuteScalar();                                
            }catch(SqliteException ex) {
                throw new SqliteException("Problema na conexão com o banco de dados",ex.ErrorCode);
            }
        }

        public SqliteDataReader ExecuteReader(string query) {
            try {
                var command = PrepareCommand(query);
                return command.ExecuteReader();
            }catch(SqliteException ex) {
                throw new SqliteException("Problema ao retornar dados do banco",ex.ErrorCode);
            }
        }   

        private void CloseConnection() {
            if(Connection != null || Connection.State != ConnectionState.Closed) {
                Connection.Close();    
            }
        }

        public void Dispose()
        {            
            CloseConnection();
        }
    }
}