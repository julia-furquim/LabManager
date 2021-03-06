using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;


var databaseConfig = new DatabaseConfig();


var databaseSetup = new DatabaseSetup(databaseConfig);

var ComputerRepository = new ComputerRepository(databaseConfig);

// Routing
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
   

    if(modelAction == "List")
    {
        Console.WriteLine("Computer List");
        var computers = ComputerRepository.GetAll();
        foreach(var computer in computers)
        {
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
        }
    }

    if(modelAction == "New")
    {
        Console.WriteLine("Computer New");
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];

        var computer =  new Computer(id, ram, processor);

        ComputerRepository.Save(computer);
    }
}


if(modelName == "Lab")
{
    var labRepository = new LabRepository();

    if(modelAction == "List")
    {
        Console.WriteLine("Lab List");
        var labs = labRepository.GetAll();
        foreach(var lab in labs)
        {
            Console.WriteLine($"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}");
        }

    }

    if(modelAction == "New")
    {
        Console.WriteLine("Lab New");
        var id = Convert.ToInt32(args[2]);
        var number = args[3];
        var name = args[4];
        var block = args[5];

        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Labs VALUES ($id, $number, $name, $block)";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$number", number);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$block", block);

        command.ExecuteNonQuery();
        connection.Close();
    }


    if(modelAction == "Show")
    {
        var id = Convert.ToInt32(args[2]);
        var computer = computerRepository.GetById(id);
        Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
    }

    if(modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Update(computer);
    }
  
   if(modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);
        computerRepository.Delete(id);
    }

}