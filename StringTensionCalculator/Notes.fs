namespace StringTensionsCalculator

open System

module Notes =
    let add (semitones:int) (note:Note) =
        let semitonesToPreviousA = -1 * int note.name
        let semitonesToNextA = 12 - int note.name
        match semitones with
        | 0 -> note
        | s when s > 0 ->
            match semitones with
            | s when s < semitonesToNextA -> { note with name = enum (s + int note.name) }
            | _ ->
                let semitonesFromNextA = semitones - semitonesToNextA
                let addHeight = (semitonesFromNextA / 12) + 1
                let addNameFromA = semitonesFromNextA % 12
                { name = enum addNameFromA; height = note.height + addHeight }
        | _ ->
            match semitones with
            | s when s >= semitonesToPreviousA -> { note with name = enum (int note.name + s) }
            | _ -> note
            (*
                let semitonesFromPreviousA = semitones - semitonesToPreviousA
                let removeHeight = Math.Abs((semitonesFromPreviousA / 12) + -1)
                let addNameFromA = Math.Abs(semitonesFromPreviousA % 12)
                { name = enum addNameFromA; height = note.height - addHeight }
            *)