using System;

class Program
{
    static void Main(string[] args)
    {
        // Crea el árbol binario de búsqueda
        BinarySearchTree tree = new BinarySearchTree();

        // Se insertan elementos al árbol
        tree.Insert(60);
        tree.Insert(20);
        tree.Insert(50);
        tree.Insert(30);
        tree.Insert(40);
        tree.Insert(10);
        tree.Insert(90);

        // Buscar valores en el árbol
        Console.WriteLine($"Buscar 50 en el árbol: {tree.Search(50)}");
        Console.WriteLine($"Buscar 80 en el árbol: {tree.Search(80)}");

        // Eliminar eliminar elementos del árbol
        Console.WriteLine("Eliminar el número 50 del árbol");
        tree.Delete(50);
        Console.WriteLine($"Buscar 50 en el árbol: {tree.Search(50)}");


        // Implementación de los diferentes tipos de recorridos del árbol:

        // Inorden (izquierda, raíz, derecha)
        Console.WriteLine("Recorrido en orden del árbol:");
        tree.InOrder();

        //Preorden (raíz, izquierda, derecha)
        Console.WriteLine("Recorrido en preorden del árbol:");
        tree.PreOrder();

        // Postorden (izquierda, derecha, raíz)
        Console.WriteLine("Recorrido en postorden del árbol:");
        tree.PostOrder();
    }
}