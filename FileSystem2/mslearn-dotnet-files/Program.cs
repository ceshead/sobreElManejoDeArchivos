using System.IO; //desde la versión 6 no es necesario escribir estos 2 using
using System.Collections.Generic; //Pero para recordar su uso las vamos a dejar

//Este código pasa el nombre de la carpeta stores como la ubicación en la que buscar los archivos.
var salesFiles = FindFiles("stores");//FindFiles es la función de abajo de tipo IEnumerable
foreach (var file in salesFiles)
{
    Console.WriteLine(file);
}

//Creación una función denominada FindFiles que tome un parámetro folderName.
IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        // The file name will contain the full path, so only check the end of it
        if (file.EndsWith("sales.json"))
        {
           salesFiles.Add(file);//carga la lista con un nuevo elemento string
        }
    }

    return salesFiles;
}



