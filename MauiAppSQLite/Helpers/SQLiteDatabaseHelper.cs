using MauiAppSQLite.Models;
using SQLite;

namespace MauiAppSQLite.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string dbPath)
        {
            _conn = new SQLiteAsyncConnection(dbPath);
            _conn.CreateTableAsync<Product>().Wait();
        }

        public Task<int> Insert(Product product)
        {
            return _conn.InsertAsync(product);
        }

        public Task<List<Product>> Update(Product product)
        {
            string sql = "UPDATE Produto SET Nome=?, Quantidade=?, Preco=? WHERE Id=?";

            return _conn.QueryAsync<Product>(
                sql,
                product.Name,
                product.Quantity,
                product.Price,
                product.Id
            );
        }

        public Task<int> Delete(int id)
        {
            return _conn.DeleteAsync<Product>(id);
        }

        public Task<List<Product>> GetAll()
        {
            return _conn.Table<Product>().ToListAsync();
        }

        public Task<List<Product>> Search(string q)
        {
            string sql = "SELECT * Produto WHERE nome LIKE '%" + q + "%'";

            return _conn.QueryAsync<Product>(sql);
        }
    }
}