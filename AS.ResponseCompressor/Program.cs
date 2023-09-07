using AS.ResponseCompressor;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCompression(opt =>
{

	opt.Providers.Clear();
	opt.EnableForHttps = true;  //use anti forgery to protect from CRIME and BREACH attack
	opt.Providers.Add<BrotliCompressionProvider>();
	opt.Providers.Add<GzipCompressionProvider>();
	opt.Providers.Add<MyCompressor>();

	opt.MimeTypes = ResponseCompressionDefaults.MimeTypes;

});

builder.Services.Configure<BrotliCompressionProviderOptions>(_level =>
{
	_level.Level = System.IO.Compression.CompressionLevel.NoCompression;
});

builder.Services.Configure<GzipCompressionProviderOptions>(_level =>
{
	_level.Level = System.IO.Compression.CompressionLevel.Fastest;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseResponseCompression();

app.UseAuthorization();

app.MapControllers();


app.Run();

//Responses not natively compressed typically include CSS, JavaScript, HTML, XML, and JSON.-- use compressor for these type of returns
//Don't compress natively compressed assets, such as PNG files

//IF NO compressor added- The Brotli compression provider and Gzip compression provider are added by default to the array of compression providers.