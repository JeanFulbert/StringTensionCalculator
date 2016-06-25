namespace StringTensionsCalculator.Tests

open System
open NUnit.Framework
open Swensen.Unquote
open StringTensionsCalculator
open FsCheck
open FsCheck.NUnit

module NoteAddOptOutOfBoundsSpecs =
    [<NoteProperty>]
    let ``Adding semitones higher than G#8 returns None`` (note:Note) =
        let actual = note |> Notes.addOpt 109
        actual =! None

    [<NoteProperty>]
    let ``Adding semitones lower than A0 returns None`` (note:Note) =
        let actual = note |> Notes.addOpt -109
        actual =! None
