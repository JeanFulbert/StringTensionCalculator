namespace StringTensionsCalculator

module Notes =
    let add (semitones:int) (note:Note) =
        let semitonesToNextA = 12 - int note.name
        match semitones with
        | 0 -> note
        | s when s > 0 ->
            match semitones with
            | s when s < semitonesToNextA -> { note with name = enum (s + int note.name) }
            | _ ->
                let semitonesFromNextA = semitones - semitonesToNextA
                let addHeight = (semitonesFromNextA / 12) + 1
                let addNameFromNextA = semitonesFromNextA % 12
                { name = enum addNameFromNextA; height = note.height + addHeight }
        | _ -> note