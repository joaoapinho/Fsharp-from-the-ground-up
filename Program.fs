open System
open System.IO

module Float = 
    let tryFromString s =
        if s = "N/A" then 
            None
        else
            Some (float s)
    
    let fromStringOr def s =
        s
        |> tryFromString
        |> Option.defaultValue def

type Student =
    {
        Name : string
        Id : string
        MeanScore : float
        MinScore : float
        MaxScore : float
    }

module Student =
    let fromString (s: string) =
        let elements = s.Split('\t')
        let name = elements.[0]
        let id = elements.[1]
        let scores =
            elements
                    |> Array.skip 2
                    |> Array.map (Float.fromStringOr 50.0)
        let meanScore = scores |> Array.average
        let maxScore = scores |> Array.max
        let minScore = scores |> Array.min
        {
            Name = name
            Id = id
            MeanScore = meanScore
            MinScore = minScore
            MaxScore = maxScore
        }

    let printSummary (student : Student) =
        printfn "%s\t%s\t%0.1f\t%0.1f\t%0.1f" student.Name student.Id student.MeanScore student.MinScore student.MaxScore

let summarize filePath =
    let rows = File.ReadAllLines filePath
    let strudentCount = (rows |> Array.length) - 1
    printfn "Student c ount %i" strudentCount
    rows
    |> Array.skip 1
    |> Array.map Student.fromString //convert each line to a Student instance
    |> Array.sortByDescending(fun student -> student.MeanScore) //Sort by mean score (descending)
    |> Array.iter Student.printSummary //Print each student instance
  
[<EntryPoint>]
let main argv =
    if argv.Length = 1 then
        let filePath = argv.[0]
        if File.Exists filePath then
            printfn "Processing %s" filePath
            summarize filePath
            0
        else 
            printfn "File %s does not exist" filePath
            2
    else
        printfn "Please specify a file"
        1