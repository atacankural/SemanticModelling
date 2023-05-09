using System;
using System.Linq;
using GeometryGym.Ifc;

namespace IFC_01
{
    public class ModelTraverser
    {
        private DatabaseIfc _db;

        public ModelTraverser(string ifcFilePath)
        {
            _db = new DatabaseIfc(ifcFilePath);
        }

        public void TraverseModel(string targetWallGlobalId)
        {
            // Select the wall using its GlobalId
            var wall = _db.FirstOrDefault(x => x is IfcWall && (x as IfcWall).GlobalId.ToString() == targetWallGlobalId) as IfcWall;
            Console.WriteLine($"Selected Wall - Name: {wall.Name}, GlobalId: {wall.GlobalId}");

            // Find all instances of IfcOpeningElement related to the selected wall
            var openingElements = _db.Where<IfcRelVoidsElement>(x => x.RelatingBuildingElement == wall).Select(x => x.RelatedOpeningElement);

            Console.WriteLine($"Found {openingElements.Count()} opening elements for the selected wall");

            // Iterate through the opening elements
            foreach (var openingElement in openingElements)
            {
                Console.WriteLine($"\nOpening Element - GlobalId: {openingElement.GlobalId}");

                // Find the corresponding IfcDoor or IfcWindow that fills the opening element
                var fillingElement = _db.FirstOrDefault<IfcRelFillsElement>(x => x.RelatingOpeningElement == openingElement)?.RelatedBuildingElement;

                if (fillingElement != null)
                {
                    Console.WriteLine($"Filling Element - Type: {fillingElement.GetType().Name}, GlobalId: {fillingElement.GlobalId}");
                }
                else
                {
                    Console.WriteLine("No filling element found for this opening element");
                }
            }
        }
    }
}
