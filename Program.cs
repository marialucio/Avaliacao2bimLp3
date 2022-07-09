using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Repositories;
using Avaliacao3BimLp3.Models;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Student")
{
    var studentRepository = new StudentRepository(databaseConfig);

    if(modelAction == "List")
    {
        Console.WriteLine("Student List");
        if(studentRepository.GetAll().Count() == 0)
        {
            Console.WriteLine("Nenhum estudante cadastrado...");
        }
        else
        {
            var students = studentRepository.GetAll();
            foreach(var student in students)
            {
                if(student.Former)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, formado");
                } 
                else
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, não formado");
                }
            }
        }
    }

    if(modelAction == "New")
    {
        if(studentRepository.ExistsById(args[2]))
        {
            Console.WriteLine($"Estudante com registro {args[2]} já existe");
        } 
        else 
        {
            var registration = args[2];
            var name = args[3];
            var city = args[4];
            var former = Convert.ToBoolean(args[5]);

            var student = new Student(registration, name, city, former);

            studentRepository.Save(student);

            Console.WriteLine($"Estudante {name} cadastrado com sucesso!");
        }
    }

    
    if(modelAction == "Delete")
    {
        if(studentRepository.ExistsById(args[2]))
        {
            studentRepository.Delete(args[2]);
            Console.WriteLine($"Estudant {args[2]} removido com sucesso");
        } 
        else 
        {
            Console.WriteLine($"Estudant {args[2]} não encontrado...");
        }
    }

    if(modelAction == "MarkAsFormed")
    {
        if(studentRepository.ExistsById(args[2]))
        {
            studentRepository.MarkAsFormed(args[2]);
            Console.WriteLine($"Estudant {args[2]} definido como formado");
        } 
        else 
        {
            Console.WriteLine($"Estudant {args[2]} não encontrado...");
        }
    }

    if(modelAction == "ListFormed")
    {
        Console.WriteLine("Student ListFormed");
        if(studentRepository.GetAll().Count() == 0)
        {
            Console.WriteLine("Nenhum estudante cadastrado...");
        }
        else
        {
            var students = studentRepository.GetAllFormed();
            foreach(var student in students)
            {
                Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, formado");
            }
        }
    }

    if(modelAction == "ListByCity")
    {
        Console.WriteLine("Student ListByCity");

        if(studentRepository.GetAllStudentByCity(args[2]).Count() == 0)
        {
            Console.WriteLine("Nenhum estudante cadastrado...");
        }
        else
        {
            var students = studentRepository.GetAllStudentByCity(args[2]);
            foreach(var student in students)
            {
                if(student.Former)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, formado");
                } 
                else
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, não formado");
                }
            }
        }
    }

    if(modelAction == "ListByCities")
    {
        var cities = new string[args.Length - 2];

        for(int i = 2; i < args.Length; i++)
        {
            cities[i - 2] = args[i];
        }

        if(studentRepository.GetAllByCities(cities).Count() == 0)
        {
            Console.WriteLine("Nenhum estudante cadastrado...");   
        }
        else
        {
            foreach(var student in studentRepository.GetAllByCities(cities))
            {
                if(student.Former)
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, formado");
                }
                else
                {
                    Console.WriteLine($"{student.Registration}, {student.Name}, {student.City}, não formado");
                }
            }
        }
    }

    if(modelAction == "Report")
    {
        if(args[2] == "CountByCities")
        {
            if(studentRepository.CountByCities().Count() == 0)
            {
                Console.WriteLine("Nenhum estudante cadastrado...");
            }
            else
            {
                foreach(var student in studentRepository.CountByCities())
                {
                    Console.WriteLine($"{student.AttributeName}, {student.StudentNumber}");
                }
            }
        }

        if(args[2] == "CountByFormed")
        {
            if(studentRepository.CountByFormed().Count() == 0)
            {
                Console.WriteLine("Nenhum estudante cadastrado...");   
            }
            else
            {
                foreach(var student in studentRepository.CountByFormed())
                {
                    if(student.AttributeName == "1")
                    {
                        Console.WriteLine($"Formado, {student.StudentNumber}");
                    }
                    else
                    {
                        Console.WriteLine($"Não formado, {student.StudentNumber}");
                    }
                }
            }
        }
    }
}