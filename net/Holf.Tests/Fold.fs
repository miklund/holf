namespace Holf.Fold

open Xunit
open FsUnit.Xunit

type ``Given a list with ints`` ()=
  // data structures
  let data1 = [1; 2; 3; 4; 5]

  let memberEquals items1 items2 = (set items1 = set items2)
