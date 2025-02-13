module Hw6.App

open System
open System.Globalization
open Hw6.Calculator
open Hw6.ResultBuilder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe

[<CLIMutable>]
type CalculatorRequest = {
    value1: decimal;
    operation: string;
    value2: decimal;
}

let calculatorHandler: HttpHandler =
    fun next ctx ->
        let computed = result {
            let! parsed = ctx.TryBindQueryString<CalculatorRequest>(CultureInfo.InvariantCulture)
            let! parsedOperator = parseOperation parsed.operation
            return! calculate parsed.value1 parsedOperator parsed.value2
        }
        match computed with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx

let webApp =
    choose [
        GET >=> choose [
             route "/" >=> text "Use //calculate?value1=<VAL1>&operation=<OPERATION>&value2=<VAL2>"
             route "/calculate" >=> calculatorHandler
        ]
        setStatusCode 404 >=> text "Not Found" 
    ]
    
type Startup() =
    member _.ConfigureServices (services : IServiceCollection) =
        services.AddGiraffe() |> ignore

    member _.Configure (app : IApplicationBuilder) (_ : IHostEnvironment) (_ : ILoggerFactory) =
        app.UseGiraffe webApp
        
[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun whBuilder -> whBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()
    0