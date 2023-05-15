// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var finder = new WordFinder.WordFinderAlt(new List<string> { 
    "vperrofdsp", 
    "nbperroere", 
    "asdasdassr",
    "perrolobor",
    "perroperro",
    "rpboloboas", 
    "ledfoasdfg", 
    "ordfbasdfg", 
    "brdfoasdfg", 
    "oodfgasdfg" }); // perro 7 lobo 4
var res = finder.Find(new List<string> { "perro", "lobo" });
foreach(var item in res)
{
    Console.WriteLine(item);
}
Console.ReadLine();
