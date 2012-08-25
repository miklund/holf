namespace Holf.Map

open Xunit
open FsUnit.Xunit

type ``Given a list with ints`` ()=
  // data structures
  let data1 = [1; 2; 3; 4; 5]

  let memberEquals items1 items2 = (set items1 = set items2)

  [<Fact>] member test.
    ``when provided a double function, should double each member of the list`` ()=
      Holf.List.Map(((*) 2), data1) |> memberEquals [2; 4; 6; 8; 10] |> should be True

  [<Fact>] member test.
    ``when provided a half function, should change result list type to float`` ()=
      Holf.List.Map((fun (i : int) -> float(i) / 2.0), data1) |> memberEquals [0.5; 1.0; 1.5; 2.0; 2.5] |> should be True

  [<Fact>] member test.
    ``when provided ToString, should change type to a non primitive type`` ()=
      Holf.List.Map((fun (i : int) -> System.Convert.ToString(i)), data1) |> memberEquals ["1"; "2"; "3"; "4"; "5"] |> should be True


  