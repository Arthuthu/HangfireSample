namespace HangfireAPI;

public class EnqueueJobs
{
    public async Task MeuPrimeiroJobFireandForget()
    {
        await Task.Run(() =>
        {
            Console.WriteLine("Fire and forget");
        });
    }

    public void TarefaJobPai()
    {
        Console.WriteLine("Esta é a tarefa pai");
    }

    public void TarefaJobFilho()
    {
       Console.WriteLine("Esta é a tarefa filho");
    }
}
