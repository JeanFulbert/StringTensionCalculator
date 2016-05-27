namespace StringTensionsCalculator

module Notes =
    let add (semitones:int) (note:Note) =
        match semitones with
        | s when s > 0 ->
            match semitones with
            | s when s % 12 = 0 -> { note with height = note.height + (s / 12) }
            | s when note.name <> NoteName.GSharp && s < (12 - int note.name) -> { note with name = enum (s + int note.name) }
            | _ -> note
        | _ -> note

