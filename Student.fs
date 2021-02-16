namespace StudentScores

type Student =
    {
        Surname : string
        GivenName : string
        Id : string
        MeanScore : float
        MinScore : float
        MaxScore : float
    }
    
module Student =
    let namePart i (s: string) =
        let elements = s.Split(',')
        elements.[i].Trim()

    let fromString (s: string) =
        let elements = s.Split('\t')
        let name = elements.[0]
        let given = namePart 1 name 
        let sur = namePart 0 name
        let id = elements.[1]
        let scores =
            elements
                    |> Array.skip 2
                    |> Array.map  (Float.fromStringOr 50.)
        let meanScore = scores |> Array.average
        let maxScore = scores |> Array.max
        let minScore = scores |> Array.min
        {
            Surname = sur
            GivenName = given
            Id = id
            MeanScore = meanScore
            MinScore = minScore
            MaxScore = maxScore
        }

    let printSummary (student : Student) =
        printfn "%s\t%s\t%s\t%0.1f\t%0.1f\t%0.1f" student.Surname student.GivenName student.Id student.MeanScore student.MinScore student.MaxScore
