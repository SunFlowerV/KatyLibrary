using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatyLibrary.UsersSqlCommand
{
    public class ReadUserBook
    {
        public async static Task<IEnumerable<int>> ReadThisUserBook(string userId)
        {
            List<int> book = new List<int>();
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;MultipleActiveResultSets=true";
            string commandread = "SELECT [u].[BookId] FROM[UserContentdb].[dbo].[UserBooks] AS[u] WHERE[u].[UserId] = @userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(commandread, connection);
                SqlParameter userParameter = new SqlParameter("@userid", userId);
                command.Parameters.Add(userParameter);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    
                    while(await reader.ReadAsync())
                    {
                        int bookId = reader.GetInt32(0);
                        book.Add(bookId);
                        
                    }
                    
                }
                return book;
                
            }
        }

        
    }
}
