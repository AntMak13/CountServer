namespace CountServer.Tests;

public class ServerTests
{
    [Fact]
    public void GetCount_Initially_ReturnsZero()
    {
        var result = Server.GetCount();
        Assert.Equal(0, result);
    }

    [Fact]
    public void AddToCount_WithValue_IncrementsCount()
    {
        var initial = Server.GetCount();
        
        Server.AddToCount(5);
        
        Assert.Equal(initial + 5, Server.GetCount());
    }
    
    [Fact]
    public async Task Writers_BlockReaders()
    {
        var readerResult = -1;
        var writeCompleted = false;
        var readStarted = new ManualResetEventSlim(false);

        // Act - писатель, который будет долго работать
        var writerTask = Task.Run(() =>
        {
            Server.AddToCount(1);
            Thread.Sleep(100); // Имитируем долгую запись
            writeCompleted = true;
        });

        // Читатель, который запустится во время записи
        var readerTask = Task.Run(() =>
        {
            readStarted.Set();
            readerResult = Server.GetCount();
        });

        readStarted.Wait(); // Ждем пока читатель начнет работу
        Thread.Sleep(50); // Даем время читателю заблокироваться

        // Assert - запись еще не завершена, чтение должно ждать
        Assert.False(writeCompleted);
            
        await writerTask;
        await readerTask;

        // После завершения чтение должно получить актуальное значение
        Assert.Equal(1, readerResult);
    }
    
}
