using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

[TestClass]
public class BinarySearchTreeTests
{
    private BinarySearchTree bst;
    private StringBuilder consoleOutput;
    private StringWriter stringWriter;
    private TextWriter originalOutput;

    [TestInitialize]
    public void Setup()
    {
        bst = new BinarySearchTree();

        // Configurar la captura de salida de consola
        consoleOutput = new StringBuilder();
        stringWriter = new StringWriter(consoleOutput);
        originalOutput = Console.Out;
        Console.SetOut(stringWriter);
    }

    [TestCleanup]
    public void Cleanup()
    {
        // Restaurar la salida de consola original
        Console.SetOut(originalOutput);
        stringWriter.Dispose();
    }

    [TestMethod]
    public void InsertAndSearch_SingleValue_ReturnsTrue()
    {
        // Arrange & Act
        bst.Insert(50);

        // Assert
        Assert.IsTrue(bst.Search(50));
        Assert.IsFalse(bst.Search(25));
    }

    [TestMethod]
    public void InsertAndSearch_MultipleValues_ReturnsExpectedResults()
    {
        // Arrange
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(40);

        // Act & Assert
        Assert.IsTrue(bst.Search(50));
        Assert.IsTrue(bst.Search(30));
        Assert.IsTrue(bst.Search(70));
        Assert.IsTrue(bst.Search(20));
        Assert.IsTrue(bst.Search(40));
        Assert.IsFalse(bst.Search(10));
        Assert.IsFalse(bst.Search(60));
    }

    [TestMethod]
    public void Delete_LeafNode_RemovesNode()
    {
        // Arrange
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);

        // Act
        bst.Delete(30);

        // Assert
        Assert.IsTrue(bst.Search(50));
        Assert.IsTrue(bst.Search(70));
        Assert.IsFalse(bst.Search(30));
    }

    [TestMethod]
    public void Delete_NodeWithOneChild_RemovesNodeCorrectly()
    {
        // Arrange
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(20);

        // Act
        bst.Delete(30);

        // Assert
        Assert.IsTrue(bst.Search(50));
        Assert.IsTrue(bst.Search(20));
        Assert.IsFalse(bst.Search(30));
    }

    [TestMethod]
    public void Delete_NodeWithTwoChildren_RemovesNodeCorrectly()
    {
        // Arrange
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(40);

        // Act
        bst.Delete(30);

        // Assert
        Assert.IsTrue(bst.Search(50));
        Assert.IsTrue(bst.Search(40));
        Assert.IsTrue(bst.Search(70));
        Assert.IsTrue(bst.Search(20));
        Assert.IsFalse(bst.Search(30));
    }

    [TestMethod]
    public void Delete_RootNode_RemovesRootCorrectly()
    {
        // Arrange
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);

        // Act
        bst.Delete(50);

        // Assert
        Assert.IsFalse(bst.Search(50));
        Assert.IsTrue(bst.Search(30));
        Assert.IsTrue(bst.Search(70));
    }

    [TestMethod]
    public void Delete_NonExistentNode_DoesNotModifyTree()
    {
        // Arrange
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);

        // Act
        bst.Delete(100);

        // Assert
        Assert.IsTrue(bst.Search(50));
        Assert.IsTrue(bst.Search(30));
        Assert.IsTrue(bst.Search(70));
    }

    [TestMethod]
    public void InOrder_TraversalOrder_IsCorrect()
    {
        // Arrange
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(40);

        // Act
        consoleOutput.Clear();
        bst.InOrder();
        string output = consoleOutput.ToString().Trim();

        // Assert - Verificar que los números aparezcan en orden ascendente
        string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual("20", lines[0].Trim());
        Assert.AreEqual("30", lines[1].Trim());
        Assert.AreEqual("40", lines[2].Trim());
        Assert.AreEqual("50", lines[3].Trim());
        Assert.AreEqual("70", lines[4].Trim());
    }

    [TestMethod]
    public void PreOrder_TraversalOrder_IsCorrect()
    {
        // Arrange
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(40);

        // Act
        consoleOutput.Clear();
        bst.PreOrder();
        string output = consoleOutput.ToString().Trim();

        // Assert - Verificar que la raíz sea visitada primero, luego izquierda, luego derecha
        string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual("50", lines[0].Trim());
        Assert.AreEqual("30", lines[1].Trim());
        Assert.AreEqual("20", lines[2].Trim());
        Assert.AreEqual("40", lines[3].Trim());
        Assert.AreEqual("70", lines[4].Trim());
    }

    [TestMethod]
    public void PostOrder_TraversalOrder_IsCorrect()
    {
        // Arrange
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(40);

        // Act
        consoleOutput.Clear();
        bst.PostOrder();
        string output = consoleOutput.ToString().Trim();

        // Assert - Verificar que la raíz sea visitada al final
        string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        Assert.AreEqual("20", lines[0].Trim());
        Assert.AreEqual("40", lines[1].Trim());
        Assert.AreEqual("30", lines[2].Trim());
        Assert.AreEqual("70", lines[3].Trim());
        Assert.AreEqual("50", lines[4].Trim());
    }

    [TestMethod]
    public void EmptyTree_Search_ReturnsFalse()
    {
        // Assert
        Assert.IsFalse(bst.Search(50));
    }

    [TestMethod]
    public void EmptyTree_DeleteNode_DoesNothing()
    {
        // Act
        bst.Delete(50);

        // Assert - No debería lanzar excepciones
        Assert.IsFalse(bst.Search(50));
    }

    [TestMethod]
    public void Tree_InsertDuplicateValue_MaintainsStructure()
    {
        // Arrange
        bst.Insert(50);
        bst.Insert(30);

        // Act
        bst.Insert(30); // Insertar duplicado

        // Assert
        Assert.IsTrue(bst.Search(30));
        Assert.IsTrue(bst.Search(50));
    }

    [TestMethod]
    public void MinValue_CorrectlyIdentifiesMinimum()
    {
        // Arrange
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(10);

        // Act - Eliminar un nodo para forzar uso de MinValue
        bst.Delete(30);

        // Assert - Verificar que la estructura del árbol se mantiene correctamente
        Assert.IsFalse(bst.Search(30));
        Assert.IsTrue(bst.Search(20));
        Assert.IsTrue(bst.Search(10));
        Assert.IsTrue(bst.Search(50));
        Assert.IsTrue(bst.Search(70));
    }
}