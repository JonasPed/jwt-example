// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

var token = new TestJwtToken()
    .WithDepartment("dep")
    .WithEmail("email")
    .WithClientId("integration-test")
    .WithCvr("38163264")
    .WithExpiration(600)
    .Build();

Console.WriteLine(token);