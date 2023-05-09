using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GeometryGym.Ifc;
using System.Linq;

namespace IFC_01
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Load the IFC file into the DatabaseIfc object
            DatabaseIfc db = new DatabaseIfc(@"C:\Users\ataca\OneDrive\Masaüstü\23S\SemanticModeling\01_Exercise\Spaces-withBld.ifc");

            // Create a list containing all entities in the IFC file
            var allEntities = db.ToList();

            // Use LINQ to select all IfcWall instances 
            var allWalls = from wall in db.OfType<IfcWall>() select wall;


            // Use lambda expressions to create a list containing all IfcWall instances
            var allWallsLambda = db.OfType<IfcWall>().ToList();


            // Iterate through all IfcWall instances selected with LINQ and print their GlobalId and Name
            foreach (var wall in allWalls)
            {
                string GlobalId = wall.GlobalId;
                string WallName = wall.Name;
                Console.WriteLine(GlobalId);
                Console.WriteLine(WallName);
            }

            // Use LINQ to select all IfcDoor instances
            var allDoors = from door in db.OfType<IfcDoor>() select door;

            // Use lambda expressions to create a list containing all IfcDoor instances
            var allDoorsLambda = db.OfType<IfcDoor>().ToList();

            // Iterate through all IfcDoor instances selected with LINQ and print their GlobalId and Name
            foreach (var door in allDoors)
            {
                string GlobalId = door.GlobalId;
                string DoorName = door.Name;
                Console.WriteLine(GlobalId);
                Console.WriteLine(DoorName);
            }

            // Iterate through all IfcDoor instances in the list created with lambda expressions and print their GlobalId and Name
            foreach (var door in allDoorsLambda)
            {
                string GlobalId = door.GlobalId;
                string DoorName = door.Name;
                Console.WriteLine(GlobalId);
                Console.WriteLine(DoorName);
            }

            // Comparing size of Walls and Doors 
            Console.WriteLine($"Total number of the wall entites: {allWalls.Count()} \nTotal number of the door entites: {allDoors.Count()}");
            //As we can see above there are 11 walls and 6 doors in the data.
            //It is expected results actually while mostly we have one or two doors for each room, there are usually four walls.


            // I found a wall that has two doors, its GlobalId: 3OIoKUJGfB9wDGaccZpWNf
            string targetWallGlobalId = ("3OIoKUJGfB9wDGaccZpWNf");

            // Define the path to your IFC file
            string ifcFilePath = @"C:\Users\ataca\OneDrive\Masaüstü\23S\SemanticModeling\01_Exercise\Spaces-withBld.ifc";

            // Create an instance of the ModelTraverser class and call the TraverseModel method
            ModelTraverser modelTraverser = new ModelTraverser(ifcFilePath);
            modelTraverser.TraverseModel(targetWallGlobalId);

            Console.ReadLine();
            
        }
    }
}
