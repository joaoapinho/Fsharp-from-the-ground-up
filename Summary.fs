namespace StudentScores

module Summary =

    open System.IO

    let summarize schoolCodesFilePath filePath =
        let rows = 
            File.ReadLines filePath
            |> Seq.cache 
        let strudentCount = (rows |> Seq.length) - 1
        let schoolCodes = SchoolCodes.load schoolCodesFilePath
        printfn "Student count %i" strudentCount
        rows
        |> Seq.skip 1
        |> Seq.map (Student.fromString schoolCodes) //convert each line to a Student instance
        |> Seq.sortByDescending (fun student -> student.MeanScore)
        |> Seq.iter Student.printSummary