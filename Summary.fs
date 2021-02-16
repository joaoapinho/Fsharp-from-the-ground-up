namespace StudentScores

module Summary =

    open System.IO
    
    let summarize filePath =
        let rows = File.ReadAllLines filePath
        let strudentCount = (rows |> Array.length) - 1
        printfn "Student count %i" strudentCount
        rows
        |> Array.skip 1
        |> Array.map Student.fromString //convert each line to a Student instance
        |> Array.sortByDescending(fun student -> student.MeanScore) //Sort by mean score (descending)
        |> Array.iter Student.printSummary //Print each student instance