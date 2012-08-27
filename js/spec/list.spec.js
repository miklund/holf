console.log('Running test suite for list.js');

describe("Map", function() {

  it("doubles a list of integers", function() {
    
    // act
    result = [1, 2, 3].map(function(item) { return item * 2; });

    // assert
    expect(result).toEqual([2, 4, 6]);
  });
});
