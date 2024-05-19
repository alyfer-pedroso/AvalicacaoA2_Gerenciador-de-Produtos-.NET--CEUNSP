using System;
using System.Globalization;
using System.Text;

namespace Alyfer_A2
{
    internal class Program
    {
        static string user = "";
        static List<Product> products = new List<Product>();

        static void Main(string[] args)
        {
           AuthScreen();
        }

        static void AuthScreen()
        {
            int attemps = 2;

            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            string solutionDirectory = Directory.GetParent(Directory.GetParent(projectDirectory).FullName).FullName;
            string filePath = Path.Combine(solutionDirectory, "password.txt");

            if (!File.Exists(filePath))
            {
                string defaultText = "AlyferA2";
                File.WriteAllText(filePath, defaultText);
            }

            string authPass = File.ReadAllText(filePath);

            while (attemps > 0)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(@"
 ░█▀▄░█▀▀░█▄█░░░░░█░█░▀█▀░█▀█░█▀▄░█▀█░░░█▀█░░░█▀▀░█▀▀░█▀▀░█░█░█▀█░█▀▄░█▀█
 ░█▀▄░█▀▀░█░█░▄▄▄░▀▄▀░░█░░█░█░█░█░█░█░░░█▀█░░░▀▀█░█▀▀░█░█░█░█░█░█░█░█░█▀█
 ░▀▀░░▀▀▀░▀░▀░░░░░░▀░░▀▀▀░▀░▀░▀▀░░▀▀▀░░░▀░▀░░░▀▀▀░▀▀▀░▀▀▀░▀▀▀░▀░▀░▀▀░░▀░▀
 ░█▀█░█░█░█▀█░█░░░▀█▀░█▀█░█▀▀░█▀█░█▀█░░░█▀█░▀▀▄░█                        
 ░█▀█░▀▄▀░█▀█░█░░░░█░░█▀█░█░░░█▀█░█░█░░░█▀█░▄▀░░▀                        
 ░▀░▀░░▀░░▀░▀░▀▀▀░▀▀▀░▀░▀░▀▀▀░▀░▀░▀▀▀░░░▀░▀░▀▀▀░▀                        
");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" Usuario: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                user = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" Senha: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                string pass = MaskPassword("*");

                Console.ResetColor();

                if (pass != authPass)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (attemps > 1)
                    {
                        attemps--;
                        Console.Write("\n\n Autenticacao RECUSADA! Voce tem mais 1 tentativa. Tentar novamente? (s/n): ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        if (Console.ReadLine().ToLower() != "s")
                            StopConsole();
                    }
                    else
                    {
                        Console.WriteLine("\n\n Autenticacao RECUSADA! Tente novamente mais tarde.");
                        Console.ResetColor();
                        attemps--;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n Autenticacao ACEITA!");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n Carregando: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    for (int i = 0; i <= 30; i++)
                    {
                        Thread.Sleep(150);
                        Console.Write("#");
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("]");

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\n Inicializando o sistema");
                    for (int i = 0; i <= 2; i++)
                    {
                        Thread.Sleep(1000);
                        Console.Write(".");
                    }
                    HomeScreen();
                }
            }
            if (attemps < 1)
            {
                Thread.Sleep(1500);
                StopConsole();
            }
        }

        static void HomeScreen()
        {
            List<int> options = new List<int>() { 1, 2, 3, 4 };
            int option = 0;
            bool isAnOption = false;

            Console.ResetColor();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n Bem-vindo ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(user + "\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Opcoes: \n");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" 1");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" - Cadastrar produtos(s)");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" 2");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" – Visualizar produto(s)");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" 3");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" – Deletar produtos(s)");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" 4");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" – Finalizar programa\n");

            while (!isAnOption)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(" O que deseja fazer? ");
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    option = int.Parse(Console.ReadLine());

                    if (!options.Contains(option))
                        throw new Exception();

                    isAnOption = true;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n Opcao invalida!");
                }

            }

            switch(option) 
            {
                case 1:
                    RegisterProduct();
                    break;

                case 2:
                    ShowProducts();
                    break;

                case 3:
                    DeleteProduct(true);
                    break;

                case 4:
                    StopConsole();
                    break;

                default:
                    break;
            }

            
        }

        static void RegisterProduct()
        {
            bool onRegister = true;

            while (onRegister)
            {
                Console.ResetColor();
                Console.Clear();

                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n Entre com os dados do produto:");

                    int id = 0;

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\n Inserir ID automaticamente? (s/n): ");
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    if(Console.ReadLine().ToLower() == "s")
                    {
                        if (products.Count > 0)
                            id = products[products.Count - 1].Id + 1;
                        else
                            id = 1;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n Id: ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(id.ToString());
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n Id: ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        id = Convert.ToInt32(Console.ReadLine());

                        if (id == 0 || id == null || (products.Any(p => p.Id == id)))
                            throw new Exception();
                    }


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" Nome: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                        throw new Exception();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" Preco: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    double price = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    if (price <= 0 || price == null)
                        throw new Exception();

                    Product newP = new Product(id, name.Trim(), price);
                    products.Add(newP);

                    Console.Write("\n");

                    onRegister = false;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n Opcao invalida!");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    onRegister = false;
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" Deseja cadastrar outro produto? (s/n): ");
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (Console.ReadLine().ToLower() != "s")
                HomeScreen();
            else
                RegisterProduct();

        }

        static void ShowProducts()
        {
            int length = products.Count;

            Console.ResetColor();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n Carregando: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i <= 30; i++)
            {
                Thread.Sleep(100);
                Console.Write("#");
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("]");

            Console.ResetColor();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n Listagem dos produtos cadastrados:");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\n Total de produtos cadastrados: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(length.ToString());

            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (Product product in products)
            {
                product.PrintProduct();
            }

            if (length > 0) Console.Write("\n");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" Pressione qualquer tecla para continuar");
            Console.ReadKey();
            HomeScreen();
        }

        static void DeleteProduct(bool load)
        {
            bool onDeleting = true;

            Console.ResetColor();
            Console.Clear();

            if (load)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n Carregando: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                for (int i = 0; i <= 30; i++)
                {
                    Thread.Sleep(100);
                    Console.Write("#");
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("]");

                Console.ResetColor();
                Console.Clear();
            }

            while (onDeleting)
            {
                Console.ResetColor();
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n Deletar um produto:");

                if (products.Count < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n Nenhum produto cadastrado...");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\n Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    HomeScreen();
                    return;
                }

                Console.Write("\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                foreach (Product product in products)
                {
                    product.PrintProduct();
                }

                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\n Digite o ID do produto que será deletado ou 'n' para cancelar: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string decision = Console.ReadLine().ToLower();

                    if (decision == "n")
                    {
                        HomeScreen();
                    }
                    else if (products.Any(p => p.Id == Convert.ToInt32(decision)))
                    {
                        int idToDelete = Convert.ToInt32(decision);
                        products.RemoveAll(p => p.Id == idToDelete);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n Opcao invalida!");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    onDeleting = false;
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" Deseja deletar outro produto? (s/n): ");
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (Console.ReadLine().ToLower() != "s")
                HomeScreen();
            else
                DeleteProduct(false);

        }

        static string MaskPassword(string charMask)
        {
            StringBuilder pass = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                if(key.Key == ConsoleKey.Enter)
                    break;
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (pass.Length > 0)
                    {
                        pass.Remove(pass.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    pass.Append(key.KeyChar);
                    Console.Write(charMask);
                }
            }
            return pass.ToString();
        }

        static void StopConsole()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n Encerrando o sistema");
            for (int i = 0; i <= 2; i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\n Programa finalizado!\n\n");
            Console.ResetColor();

            Thread.Sleep(1000);
            Environment.Exit(0);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        Product() { }

        public Product (int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public void PrintProduct()
        {
            Console.WriteLine($" {"{"}ID={Id.ToString()}, Nome={Name}, Preco={Price.ToString("F2", CultureInfo.InvariantCulture)}{"}"}");
        }
    }
}
