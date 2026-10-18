using DoubleLinkedList;

var list = new DoublyLinkedList<string>();
var option = string.Empty;
var value = string.Empty;

do
{
    option = Menu();
    switch (option)
    {
        case "1":
            Console.Write("Enter a value: ");
            value = Console.ReadLine() ?? string.Empty;
            list.Add(value);
            Console.WriteLine($"The element '{value}' was added with exit");
            break;

        case "2":
            Console.WriteLine(list.ToString());
            break;

        case "3":
            Console.WriteLine(list.ToStringReverse());
            break;

        case "4":
            list.SortDescending();
            Console.WriteLine(list.ToString());
            break;

        case "5":
            var modes = list.GetModes();
            if (modes.Count == 0)
                Console.WriteLine("No mode.");
            else
                Console.WriteLine($"Mode(s): {string.Join(", ", modes)}");
            break;

        case "6":
            var frequencies = list.GetFrequencies();
            foreach (var pair in frequencies)
            {
                var bars = new string('*', pair.Value);
                Console.WriteLine($"{pair.Key,-10} {bars}");
            }
            break;

        case "7":
            Console.Write("Enter a value to search: ");
            value = Console.ReadLine() ?? string.Empty;
            Console.WriteLine(list.Exists(value) ? $" The element {value} exists." : $"The element {value} not found.");
            break;

        case "8":
            Console.Write("Enter a value to delete (first occurrence): ");
            value = Console.ReadLine() ?? string.Empty;
            list.DeleteOne(value);
            Console.WriteLine(list.ToString());
            break;

        case "9":
            Console.Write("Enter a value to delete (all occurrences): ");
            value = Console.ReadLine() ?? string.Empty;
            list.DeleteAll(value);
            Console.WriteLine(list.ToString());
            break;

        case "0":
            Console.WriteLine("Exiting...");
            break;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }

} while (option != "0");

string Menu()
{
    Console.WriteLine("1. Add");
    Console.WriteLine("2. Show forward");
    Console.WriteLine("3. Show backward");
    Console.WriteLine("4. Sort descending");
    Console.WriteLine("5. Show mode(s)");
    Console.WriteLine("6. Show chart");
    Console.WriteLine("7. Exists");
    Console.WriteLine("8. Delete one occurrence");
    Console.WriteLine("9. Delete all occurrences");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your option: ");
    return Console.ReadLine() ?? string.Empty;
}
