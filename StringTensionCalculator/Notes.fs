namespace StringTensionsCalculator

open System

module Notes =
    let private a4 = { name = NoteName.A; height = 4 }
    let private twelthRootOf2 = Math.Pow(2., 1./12.)

    let private intNote (note:Note) =
        (int note.name) + 12 * note.height

    let add (semitones:int) (note:Note) =
        let numNote = note |> intNote
        let numNoteAfterAdd = numNote + semitones
        
        let noteName = numNoteAfterAdd % 12
        let noteHeight = numNoteAfterAdd / 12

        match numNoteAfterAdd with
        | n when 0 <= n && n <= 108 -> Some { name = enum noteName; height = noteHeight }
        | _ -> None

    let semitonesTo (note:Note) (noteSource:Note) =
        let numNote = note |> intNote
        let numNoteSource = noteSource |> intNote
        numNote - numNoteSource        

    let frequency (frequencyA4:float<hz>) (note:Note) =
        let semitones = a4 |> semitonesTo note
        frequencyA4 * Math.Pow(twelthRootOf2, float semitones)

    let frequency440 = frequency 440.<hz>