// Implementación de la estructura básica del BST

public class TreeNode
{
    public int key;
    public TreeNode left, right;

    public TreeNode(int item)
    {
        key = item;
        left = right = null;
    }
}

public class BinarySearchTree
{
    public TreeNode root;

    public BinarySearchTree()
    {
        root = null;
    }

    // Implementación de la búsqueda de elementos
    public bool Search(int key)
    {
        return SearchRecursive(root, key);
    }

    private bool SearchRecursive(TreeNode root, int key)
    {
        if (root == null)
        {
            return false;
        }
        if (root.key == key)
        {
            return true;
        }
        else if (key > root.key)
        {
            return SearchRecursive(root.right, key);
        }
        else
        {
            return SearchRecursive(root.left, key);
        }
    }

    // Implementación de la inserción de elementos
    public void Insert(int key)
    {
        root = InsertRecursive(root, key);
        Console.WriteLine($"Se insertó {key} al árbol"); // esto se agregó para poder mostrar los valores que se insertaron
    }

    private TreeNode InsertRecursive(TreeNode root, int key)
    {
        if (root == null)
        {
            root = new TreeNode(key);
            return root;
        }

        if (key < root.key)
            root.left = InsertRecursive(root.left, key);
        else if (key > root.key)
            root.right = InsertRecursive(root.right, key);

        return root;
    }
    // Implementación de la eliminación de elemntos
    public void Delete(int key)
    {
        root = DeleteRecursive(root, key);
    }

    private TreeNode DeleteRecursive(TreeNode root, int key)
    {
        if (root == null)
            return root;

        if (key < root.key)
            root.left = DeleteRecursive(root.left, key);
        else if (key > root.key)
            root.right = DeleteRecursive(root.right, key);
        else
        {
            // Si el nodo es una hoja
            if (root.left == null && root.right == null)
                return null;

            // Si el nodo tiene un hijo
            if (root.left == null)
                return root.right;
            else if (root.right == null)
                return root.left;

            // Si el nodo tiene dos hijos, agarra el nodo más pequeño del subárbol derecho
            root.key = MinValue(root.right);

            // Elimina el nodo más pequeño del subárbol derecho
            root.right = DeleteRecursive(root.right, root.key);
        }
        return root;
    }

    private int MinValue(TreeNode root)
    {
        int minValue = root.key;
        while (root.left != null)
        {
            minValue = MinValue(root.left);
            root = root.left;
        }
        return minValue;
    }

    // Implementación de los diferentes tipos de recorridos

    // Inorden (izquierda, raíz, derecha)
    public void InOrder()
    {
        InOrderRecursive(root);
    }

    private void InOrderRecursive(TreeNode root)
    {
        if (root != null)
        {
            InOrderRecursive(root.left);
            Console.WriteLine(root.key + " ");
            InOrderRecursive(root.right);
        }
    }

    //Preorden (raíz, izquierda, derecha)
    public void PreOrder()
    {
        PreOrderRecursive(root);
    }

    private void PreOrderRecursive(TreeNode root)
    {
        if (root != null)
        {
            Console.WriteLine(root.key + " ");
            PreOrderRecursive(root.left);
            PreOrderRecursive(root.right);
        }
    }

    // Postorden (izquierda, derecha, raíz)
    public void PostOrder()
    {
        PostOrderRecursive(root);
    }

    private void PostOrderRecursive(TreeNode root)
    {
        if (root != null)
        {
            PostOrderRecursive(root.left);
            PostOrderRecursive(root.right);
            Console.WriteLine(root.key + " ");
        }
    }
}