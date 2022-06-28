using LabManager.Models;
using Microsoft.Data.Sqlite;
using LabManager.Database;
using Dapper;

namespace LabManager.Repositories;

class LabRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public LabRepository(DatabaseConfig databaseConfig){
        _databaseConfig = databaseConfig;
    }
   
        public IEnumerable<Lab> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        var labs = connection.Query<Lab>("SELECT * FROM Labs");

        return labs;
    }

    public Lab Save(Lab lab)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        
        connection.Execute("INSERT INTO Labs VALUES (@id, @number, @name, @block)", lab);
        
        return lab;

    }

    public Lab GetById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        
        var lab = connection.QuerySingle<Lab>("SELECT * FROM Labs WHERE ID = (@id)", id);
    
        return lab;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        
        connection.Execute("DELETE FROM Labs WHERE ID = ($id)", new {Id = id});
        
    }

    public Lab Update(Lab lab)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
       
        connection.Execute("UPDATE Labs SET number = (@number), name = (@name), block = (@block) WHERE ID = (@id)", lab);
        

        return lab;
    }
     public bool ExitsById(int id)
    {

        using (var connection = new SqliteConnection(_databaseConfig.ConnectionString))
        {
            var sql = "SELECT count(id) FROM Labs WHERE id = (@Id)";
            var count = connection.ExecuteScalar<Boolean>(sql, new {Id = id});
            return count;
    }
    }
}