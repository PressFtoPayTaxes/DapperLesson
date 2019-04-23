using Dapper;
using DL.DataAccess.Abstract;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.DataAccess
{
    public class MailsRepository : IRepository<Mail>
    {
        private DbConnection _connection;

        public MailsRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            _connection = new SqlConnection(connectionString);
        }

        public void Add(Mail item)
        {
            var sqlQuery = "insert into Mails(Id, CreationDate, DeletedDate, Theme, Text, ReceiverId) values(@Id, @CreationDate, @DeletedDate, @Theme, @Text, @ReceiverId)";
            _connection.Execute(sqlQuery, item);
        }

        public void Delete(Guid id)
        {
            var sqlQuery = $"update Mails set DeletedDate = {DateTime.Now} where Id = @id";
            _connection.Execute(sqlQuery, id);
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<Mail> GetAll()
        {
            var sqlQuery = "select * from Mails";
            return _connection.Query<Mail>(sqlQuery).AsList();
        }

        public void Update(Mail item)
        {
            var sqlQuery = "update Mails set Theme = @Theme and Text = @Text where Id = @Id";
            _connection.Execute(sqlQuery, item);
        }
    }
}
