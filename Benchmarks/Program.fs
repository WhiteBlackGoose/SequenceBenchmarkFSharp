open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running


[<MemoryDiagnoser>]
type Bench() =

    let listData = [ 1 .. 10 .. 10000 ]
    let arrayData = [| 1 .. 10 .. 10000 |]


    [<Benchmark(Description = "list to list via list with collect")>]
    member _.List2ListViaListWithCollect () =
        listData
        |> List.filter (fun c -> c > 30)
        |> List.map (fun c -> c * 2)
        |> List.filter (fun c -> c < 20000)
        |> List.map (fun c -> c * 3)
        |> List.collect (fun c -> [c + 1; c + 2; c + 3])
    
    [<Benchmark(Description = "list to list via seq with collect")>]
    member _.List2ListViaSeqWithCollect () =
        listData
        |> Seq.filter (fun c -> c > 30)
        |> Seq.map (fun c -> c * 2)
        |> Seq.filter (fun c -> c < 20000)
        |> Seq.map (fun c -> c * 3)
        |> Seq.collect (fun c -> seq { for i in 1 .. 3 do c + i })
        |> List.ofSeq
    
    [<Benchmark(Description = "array to array via array with collect")>]
    member _.Array2ArrayViaArrayWithCollect () =
        arrayData
        |> Array.filter (fun c -> c > 30)
        |> Array.map (fun c -> c * 2)
        |> Array.filter (fun c -> c < 20000)
        |> Array.map (fun c -> c * 3)
        |> Array.collect (fun c -> [| c + 1; c + 2; c + 3 |])
    
    [<Benchmark(Description = "array to array via seq with collect")>]
    member _.Array2ArrayViaSeqWithCollect () =
        arrayData
        |> Seq.filter (fun c -> c > 30)
        |> Seq.map (fun c -> c * 2)
        |> Seq.filter (fun c -> c < 20000)
        |> Seq.map (fun c -> c * 3)
        |> Seq.collect (fun c -> seq { for i in 1 .. 3 do c + i })
        |> Array.ofSeq
    
    [<Benchmark(Description = "list to list via list")>]
    member _.List2ListViaList () =
        listData
        |> List.filter (fun c -> c > 30)
        |> List.map (fun c -> c * 2)
        |> List.filter (fun c -> c < 20000)
        |> List.map (fun c -> c * 3)
    
    [<Benchmark(Description = "list to list via seq")>]
    member _.List2ListViaSeq () =
        listData
        |> Seq.filter (fun c -> c > 30)
        |> Seq.map (fun c -> c * 2)
        |> Seq.filter (fun c -> c < 20000)
        |> Seq.map (fun c -> c * 3)
        |> List.ofSeq
    
    [<Benchmark(Description = "array to array via array")>]
    member _.Array2ArrayViaArray () =
        arrayData
        |> Array.filter (fun c -> c > 30)
        |> Array.map (fun c -> c * 2)
        |> Array.filter (fun c -> c < 20000)
        |> Array.map (fun c -> c * 3)
    
    [<Benchmark(Description = "array to array via seq")>]
    member _.Array2ArrayViaSeq () =
        arrayData
        |> Seq.filter (fun c -> c > 30)
        |> Seq.map (fun c -> c * 2)
        |> Seq.filter (fun c -> c < 20000)
        |> Seq.map (fun c -> c * 3)
        |> Array.ofSeq


let verify () =
    let bench = Bench ()

    printfn $"{bench.List2ListViaList() |> Seq.sum}"
    printfn $"{bench.List2ListViaSeq() |> Seq.sum}"
    printfn $"{bench.Array2ArrayViaArray() |> Seq.sum}"
    printfn $"{bench.Array2ArrayViaSeq() |> Seq.sum}"

    printfn $"{bench.List2ListViaListWithCollect() |> Seq.sum}"
    printfn $"{bench.List2ListViaSeqWithCollect() |> Seq.sum}"
    printfn $"{bench.Array2ArrayViaArrayWithCollect() |> Seq.sum}"
    printfn $"{bench.Array2ArrayViaSeqWithCollect() |> Seq.sum}"

// verify ()
BenchmarkRunner.Run<Bench> () |> ignore