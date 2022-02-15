namespace FSharpLibrary

module Memo =
   
    // https://code.i-harness.com/ja/q/66a39f
    let groupSumBy (list:list<_>) =
        let dict = System.Collections.Generic.Dictionary() 
        for k, v in list do
            match dict.TryGetValue(k) with
            | true, n -> dict.[k] <- v + n
            | _ -> dict.Add(k, v)
        dict |> Seq.map (|KeyValue|) |> Seq.toList

    
    // inline ”Å
    // http://haskell.g.hatena.ne.jp/matarillo/?of=5
    let inline pow x n =  
        let rec pow x = function 
        | n when n <= 0 -> LanguagePrimitives.GenericOne 
        | 1 -> x 
        | n -> x * pow x (n-1) 
        pow x n 
