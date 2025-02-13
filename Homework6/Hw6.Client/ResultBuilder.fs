﻿module Hw6.Client.ResultBuilder

type ResultBuilder() =
    member builder.Bind(a, f) : Result<'e, 'd> =
        match a with
        | Ok a -> f a
        | Error e -> Error e
    member builder.Return x : Result<'a, 'b> = Ok x
    member builder.ReturnFrom x : Result<'a, 'b> = x

let result = ResultBuilder()