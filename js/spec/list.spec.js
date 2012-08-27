describe("Map", function() {

  it("doubles a list of integers", function() {
    
    // act
    result = [1, 2, 3].map(function(item) { return item * 2; });

    // assert
    expect(result).toEqual([2, 4, 6]);
  });

  it("transforms a list into floats", function() {

    // act
    result = [1, 3, 5].map(function(item) { return item / 2; });

    // assert
    expect(result).toEqual([0.5, 1.5, 2.5]);
  });

  it("transforms a list of ints to strings", function() {

    // act
    result = [1, 2, 3].map(function(item) { return item.toString(); });

    // assert
    expect(result).toEqual(["1", "2", "3"]);
  });

  it("returns an empty string for empty input", function() {

    // act
    result = []

    // assert
    expect(result).toEqual([]);
  });
});
