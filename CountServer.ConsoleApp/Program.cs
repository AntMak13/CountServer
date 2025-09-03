namespace CountServer.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Демонстрация потокобезопасного сервера");
            Console.WriteLine($"Начальное значение: {Server.GetCount()}");
            
            var readerTasks = new Task[5];
            var writerTasks = new Task[3];

            for (int i = 0; i < writerTasks.Length; i++)
            {
                int writerId = i + 1;
                writerTasks[i] = Task.Run(() => Writer(writerId));
            }

            for (int i = 0; i < readerTasks.Length; i++)
            {
                int readerId = i + 1;
                readerTasks[i] = Task.Run(() => Reader(readerId));
            }

            await Task.WhenAll(writerTasks);
            await Task.WhenAll(readerTasks);

            Console.WriteLine($"\nФинальное значение: {Server.GetCount()}");
        }

        static void Writer(int id)
        {
            for (int i = 0; i < 3; i++)
            {
                var valueToAdd = new Random().Next(1, 5);
                Console.WriteLine($"Писатель {id} пытается добавить {valueToAdd}...");
                
                Server.AddToCount(valueToAdd);
                
                Console.WriteLine($"Писатель {id} добавил {valueToAdd}");
                Thread.Sleep(100);
            }
        }

        static void Reader(int id)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Читатель {id} пытается прочитать...");
                
                var value = Server.GetCount();
                
                Console.WriteLine($"Читатель {id} прочитал: {value}");
                Thread.Sleep(150);
            }
        }
    }
}