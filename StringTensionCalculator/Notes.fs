namespace StringTensionsCalculator

open System

module Notes =
    let add (semitones:int) (note:Note) =
        let numNote = (int note.name) + 12 * note.height
        let numNoteAfterAdd = numNote + semitones

//        match numNoteAfterAdd with
//        | n when n < 0 || n > 108 -> None
//        | n ->
        let noteName = numNoteAfterAdd % 12
        let noteHeight = numNoteAfterAdd / 12
        //Some
        { name = enum noteName; height = noteHeight }