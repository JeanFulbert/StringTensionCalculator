namespace StringTensionsCalculator.Tests

open System
open NUnit.Framework
open Swensen.Unquote
open StringTensionsCalculator
open FsCheck
open FsCheck.NUnit

module NoteSpecs =
    [<NoteProperty>]
    let ``Note equals itself`` (note:Note) =
        note =! note