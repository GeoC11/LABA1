using System;
using System.Collections.Generic;

namespace TreeLab
{
    // Класс узла дерева
    public class TreeNode
    {
        public string Value { get; } //значение, которое хранит узел
        public List<TreeNode> Children { get; } //обобщённый список, который может содержать только объекты типа TreeNode

        // Конструктор узла
        public TreeNode(string value)
        {
            Value = value; /*value = переданному параметру*/
            Children = new List<TreeNode>();  /*новый список для потомков*/
        }

        // Метод добавления потомка
        public void AddChild(TreeNode child)
        {
            Children.Add(child); /*добавляем узел в конец списка*/
        }

        // Рекурсивный обход дерева
        public void Traverse(int level = 0) /*уровень вложенности*/
        {
            // Вывод текущего узла с отступом для уровня вложенности
            string indent = new string(' ', level * 2);
            Console.WriteLine($"{indent}├─ {Value}");

            // Рекурсивный обход всех потомков
            foreach (var child in Children) /*перебираем все элементы в списке children*/
            {
                child.Traverse(level + 1); /*Traverse() вызывает сам себя для каждого потомка, увеличивая уровень на 1*/
            }
        }


        // Метод вывода только листовых узлов
        public void PrintLeaves(int level = 0)
        {
            // Если у узла нет потомков, это лист
            if (Children.Count == 0)
            {
                string indent = new string(' ', level * 2); 
                Console.WriteLine($"{indent}├─ {Value}");
                return;
            }

            // Рекурсивно проверяем потомков
            foreach (var child in Children)
            {
                child.PrintLeaves(level + 1); /*если узел не лист то переходим на следующие уровни и проверяем там*/
            }
        }

        // Метод проверки, является ли узел листом
        public bool IsLeaf()
        {
            return Children.Count == 0;
        }
    }

    // Прога
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Демонстрация дерева с выводом листьев\n");

            // Создание дерева

            // Корневой узел (уровень 0)
            TreeNode root = new TreeNode("Корень");

            // Уровень 1
            TreeNode vetka1 = new TreeNode("Ветка 1");
            TreeNode vetka2 = new TreeNode("Ветка 2");
            TreeNode vetka3 = new TreeNode("Ветка 3");

            //Добавляем их как потомки корня
            root.AddChild(vetka1);
            root.AddChild(vetka2);
            root.AddChild(vetka3);

            // Уровень 2 (потомки Ветки 1)
            TreeNode leaf11 = new TreeNode("Лист 1.1");
            TreeNode vetka12 = new TreeNode("Ветка 1.2");
            vetka1.AddChild(leaf11);
            vetka1.AddChild(vetka12);

            // Уровень 2 (потомки Ветки 2)
            TreeNode leaf21 = new TreeNode("Лист 2.1");
            TreeNode leaf22 = new TreeNode("Лист 2.2");
            vetka2.AddChild(leaf21);
            vetka2.AddChild(leaf22);

            // Уровень 2 (потомки Ветки 3)
            TreeNode vetka31 = new TreeNode("Ветка 3.1");
            vetka3.AddChild(vetka31);

            // Уровень 3 (потомки Ветки 1.2)
            TreeNode leaf121 = new TreeNode("Лист 1.2.1");
            TreeNode leaf122 = new TreeNode("Лист 1.2.2");
            vetka12.AddChild(leaf121);
            vetka12.AddChild(leaf122);

            // Уровень 3 (потомки Ветки 3.1)
            TreeNode leaf311 = new TreeNode("Лист 3.1.1");
            TreeNode leaf312 = new TreeNode("Лист 3.1.2");
            vetka31.AddChild(leaf311);
            vetka31.AddChild(leaf312);

            // Полный обход дерева
            Console.WriteLine("Полный обход дерева\n");
            root.Traverse();

            // Вывод листовых узлов
            Console.WriteLine("\nВывод только листовых узлов\n");
            root.PrintLeaves();

            // Проверка узлов на лист
            Console.WriteLine("\nПроверка узлов на лист\n");

            Console.WriteLine($"Узел \"Корень\" является листом? {root.IsLeaf()}");
            Console.WriteLine($"Узел \"Ветка 1\" является листом? {vetka1.IsLeaf()}");
            Console.WriteLine($"Узел \"Ветка 1.2\" является листом? {vetka12.IsLeaf()}");
            Console.WriteLine($"Узел \"Лист 1.1\" является листом? {leaf11.IsLeaf()}");
            Console.WriteLine($"Узел \"Лист 2.1\" является листом? {leaf21.IsLeaf()}");
            Console.WriteLine($"Узел \"Лист 3.1.1\" является листом? {leaf311.IsLeaf()}");


            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}