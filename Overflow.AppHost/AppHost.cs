using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Keycloak Identity Provider
var keycloak = builder.AddKeycloak("Keycloak", 6001)
    .WithDataVolume("keycloak-data");

// PostgreSQL Database
var postgres = builder.AddPostgres("postgres", port: 5432)
    .WithDataVolume("postgres-data")
    .WithPgAdmin();

// Question Database
var questionDb = postgres.AddDatabase("question-db");


var questionService = builder.AddProject<QuestionService>("question-svc")
    .WithReference(keycloak)
    .WithReference(questionDb)
    .WaitFor(keycloak)
    .WaitFor(questionDb);


builder.Build().Run();
