var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile($"appsettings.{ builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
      .AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console()
                .ReadFrom.Configuration(ctx.Configuration));
Log.Logger.Information("App is starting.....");

var connectionString = builder.Configuration.GetConnectionString("ExpressCargo");
builder.Services.AddLogging();
builder.Services.AddDbContextPool<ExpressCargoContext>(
                         opentions => opentions.UseSqlServer(connectionString: connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new ValidationFilter());
    options.EnableEndpointRouting = false;
}).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlRepository();
builder.Services.AddEmailHandler();
builder.Services.AddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
builder.Services.AddCookieAuthentication(builder.Configuration);
builder.Services.AddUtilityResolver();
builder.Services.AddContexts(connectionString);
builder.Services.AddConfigurationFiles(builder.Configuration);

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/build";
});

var app = builder.Build();

var version = builder.Configuration.GetSection("Version").Value;
using var serviceScope = ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
app.ConfigureSeedData(serviceScope);

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("App-Version", version);
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    await next();
});
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();

app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseCookiePolicy();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    if (app.Environment.IsDevelopment())
    {
        spa.UseReactDevelopmentServer(npmScript: "start");
    }
});

Log.Logger.Information($"App started successfully with version {version}");
app.Run();