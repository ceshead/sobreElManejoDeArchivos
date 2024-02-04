using System.IO; //desde la versión 6 no es necesario escribir estos 2 using
using System.Collections.Generic; //Pero para recordar su uso las vamos a dejar

//El siguiente código pasa el nombre de la carpeta stores como la ubicación en la que buscar los archivos.
var salesFiles = FindFiles("stores");//FindFiles es la función que creamos abajo de tipo IEnumerable
foreach (var file in salesFiles)
{
    Console.WriteLine(file);
}

//.NET expone la ruta de acceso completa al directorio actual a través del método Directory.GetCurrentDirectory.
var currentDirectory = Directory.GetCurrentDirectory();
Console.WriteLine($"Directorio actual: {currentDirectory}");

///Trabajo con directorios especiales
/// La enumeración System.Environment.SpecialFolder especifica constantes para recuperar rutas de acceso a carpetas especiales del sistema.
/// El código siguiente devuelve la ruta equivalente de la carpeta Mis documentos de Windows o el directorio HOME del usuario para cualquier sistema operativo, aunque el código se ejecute en Linux:
string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

///La clase Path se encuentra en el espacio de nombres System.IO de .NET y no es necesario instalarla.
///Por ejemplo, Windows usa la barra diagonal inversa (stores\201), y macOS usa la barra diagonal (stores/201).
///Para ayudarle a usar el carácter correcto, la clase Path contiene el campo DirectorySeparatorChar.
Console.WriteLine($"directory separator? stores{Path.DirectorySeparatorChar}201");

///Rutas de combinación
///Puede usar la clase Path para crear de forma automática rutas correctas para sistemas operativos específicos.
var storesDirectory=Path.Combine(currentDirectory,"stores");
Console.WriteLine(storesDirectory); // outputs: stores/
//Recuerde que debería usar la clase Path.Combine o Path.DirectorySeparatorChar 
//en lugar de cadenas codificadas de forma rígida porque el programa podría ejecutarse 
//en muchos sistemas operativos diferentes. 
//La clase Path siempre formatea las rutas correctamente para el sistema operativo en el que se está ejecutando.

///La clase Path también puede indicarle la extensión de un nombre de archivo. Si tiene un archivo y quiere saber si es JSON, puede usar la función Path.GetExtension.
Console.WriteLine(Path.GetExtension("sales.json")); // outputs: .json

///La clase Path contiene muchos métodos diferentes que realizan diversas acciones. Si quiere obtener el máximo de información posible sobre un directorio o un archivo, use la clase DirectoryInfo o FileInfo, respectivamente.
string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";
FileInfo info = new FileInfo(fileName);
Console.WriteLine($"Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}"); // And many more

///Ahora actualizamos la ruta de tienda pero esta vez no de manera rígida 
salesFiles=FindFiles(storesDirectory);

//Recorremos nuevamente salesFiles
foreach (var file in salesFiles)
{
    Console.WriteLine(file);
    //Observe que los nombres de archivo devueltos incluyen la ruta completa del sistema. 
    //Esta ruta se incluye porque el método Directory.GetCurrentDirectory devuelve la ruta completa a la ubicación actual.
}

///Creación de archivos y directorios
///Directory.CreateDirectory
var newDir=Path.Combine(Directory.GetCurrentDirectory(), "stores","205","newDir");
Directory.CreateDirectory(newDir);
///Si /stores/205 no existe todavía, se creará automáticamente. Creará los directorios y subdirectorios que se le hayan pasado.

///Comprobar si un directorio existe
///bool doesDirectoryExist = Directory.Exists(filePath);

///CREACIÓN DE ARCHIVOS
///Se pueden crear archivos mediante el método File.WriteAllText. Este método toma una ruta de acceso al archivo y los datos que se van a escribir en él. Si el archivo ya existe, se sobrescribe.
///Como ejemplo voy a guardar todo el código de este archivo Program.cs en greeting.txt
string filepath="/workspaces/sobreElManejoDeArchivos/FileSystem2/mslearn-dotnet-files/Program.cs";
// Lee todo el contenido del archivo y almacénalo en una variable
string esteArchivo = File.ReadAllText(filepath);
File.WriteAllText(Path.Combine(newDir,"greeting.txt"), $"Hello World!  {esteArchivo}");
//Debí haberlo creado dentro de un try catch pero como ejmplo lo dejaré así

//Creación de una función denominada FindFiles que tome un parámetro folderName.
//Esperamos como resultado una enumeración con los directorios en los que se encuenrtan archivos de tipo ".json"
IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension=Path.GetExtension(file);
        // The file name will contain the full path, so only check the end of it
        if (extension==".json") //if (file.EndsWith("sales.json"))
        {
            salesFiles.Add(file);//carga la lista con un nuevo elemento string
        }
    }
    return salesFiles;
}

///GIT
//Agregar Cambios al Área de Staging:
//git add .
//Realizar un Commit:
//git commit -m "Mensaje descriptivo de los cambios"
//Verificar Estado Después del Commit:
//git status
//Opcional: Subir Cambios al Repositorio Remoto:
//git push origin main
//Prueba de grabación en git 03/02/2024