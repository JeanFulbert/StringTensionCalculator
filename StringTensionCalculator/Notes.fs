namespace StringTensionsCalculator

open System

module Notes =
    let add (semitones:int) (note:Note) =
        let numNote = (int note.name) + 12 * note.height
        let numNoteAfterAdd = numNote + semitones

        let noteName = numNoteAfterAdd % 12
        let noteHeight = numNoteAfterAdd / 12
        { name = enum noteName; height = noteHeight }
        
    let addOpt (semitones:int) (note:Note) =
        let numNote = (int note.name) + 12 * note.height
        let numNoteAfterAdd = numNote + semitones
        
        let noteName = numNoteAfterAdd % 12
        let noteHeight = numNoteAfterAdd / 12

        match numNoteAfterAdd with
        | n when 0 <= n && n <= 108 -> Some { name = enum noteName; height = noteHeight }
        | _ -> None
        