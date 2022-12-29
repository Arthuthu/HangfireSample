global using Hangfire;
using Hangfire.MemoryStorage;
using HangfireAPI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registra o hangfire nos serviços, neste caso em especifico a opção de utilização esta marcado para localmemory.
builder.Services.AddHangfire(op =>
{
    op.UseMemoryStorage();
});
builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHangfireDashboard();

app.UseHttpsRedirection();

var jobs = new EnqueueJobs();

//Executa um job quando o method é chamado
BackgroundJob.Enqueue(() => jobs.MeuPrimeiroJobFireandForget());

//Executa um job com intervalos de minutos/horas/dias/meses/anos
RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring job"), Cron.Monthly());

//Executa um job em uma data especifica
BackgroundJob.Schedule(() => Console.WriteLine("Delayed Job"), TimeSpan.FromDays(2));

//Executa um job apos outro job usando continuojobwith method
string jobIdTwo = BackgroundJob.Enqueue(() => jobs.TarefaJobPai());
BackgroundJob.ContinueJobWith(jobIdTwo, () => jobs.TarefaJobFilho());

string jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Tarefa pai sem method"));
BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Tarefa filho sem method"));

app.Run();