using apiFullUni.Application.Interfaces;
using apiFullUni.Infrastructure.Data;
using apiFullUni.Infrastructure.Data.DAO;
using apiFullUni.Application.UseCases;
using apiFullUni.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do appsettings.json � carregada automaticamente
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Adicionando depend�ncias para o banco de dados e reposit�rios
builder.Services.AddScoped<ConnectionManager>();
builder.Services.AddScoped<ProdutoDAO>();
builder.Services.AddScoped<SerilogLogger>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<AdicionarProdutoUseCase>();
builder.Services.AddScoped<AtualizarProdutoUseCase>();
builder.Services.AddScoped<DeletarProdutoUseCase>();
builder.Services.AddScoped<ObterProdutoPorIdUseCase>();
builder.Services.AddScoped<ObterTodosProdutosUseCase>();
builder.Services.AddScoped<Conexao>();

// Add services to the container.
builder.Services.AddControllers();

// Configura��o do Swagger para documenta��o da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
