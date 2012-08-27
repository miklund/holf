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

describe("Fold", function() {

  it("aggregates all integers to a sum", function() {

    // act
    result = [1, 2, 3].fold(0, function(agg, item) { return agg + item; });

    // assert
    expect(result).toBe(6);
  });

  it("join all numbers into a string", function() {

    // act
    result = [1, 2, 3].fold("", function(agg, item) { return agg + item.toString(); });

    // assert
    expect(result).toBe("123");
  });


  it("join all numbers into one number", function() {

    // act
    result = [1, 2, 3].fold(0, function(agg, item) {
      return agg * 10 + item;
    });

    // assert
    expect(result).toBe(123);
  });
});

describe("Partition", function() {

  it("splits odds and evens", function() {

    // act
    result = [1, 2, 3, 4, 5].partition(function (item) { return item % 2; });

    // assert
    expect(result[0]).toEqual([2, 4]);
    expect(result[1]).toEqual([1, 3, 5]);
  });

  it("must yield a number from callback", function() {

    // act
    result = function () {
      return [1, 2, 3].partition(function (item) { return "1"; });
    };

    // assert
    expect(result).toThrow("callback should return a positive number");
  });

  it("must yield a positive integer from callback", function() {

    // act
    result = function () {
      return [1, 2, 3].partition(function (item) { return -1; });
    };

    // assert
    expect(result).toThrow("callback should return a positive number");
  }); 
});


describe("Reduce", function () {

  it("should figure out maximum number in list", function () {

    // act
    // reduce already exists on array, calling holf explicit
    result = holf.list.reduce(function (a, b) {
      if (a > b) {
        return a;
      }

      return b;
    }, [1, 9, 2, 8, 7, 6]);

    // assert
    expect(result).toEqual(9);
  });
  
  it ("should join all numbers together", function () {
    
    // arrange
    var data = [1, 2, 3, 4, 5];
    
    // act
    result = holf.list.reduce(function (a, b) { return a * 10 + b; }, data);

    // assert
    expect(result).toBe(12345);
  });

  it ("must be provided with an non empty array", function () {

    // assert
    var data = [];

    // act
    result = function () {
      return holf.list.reduce(function (a, b) { return a; }, data);
    };

    // assert
    expect(result).toThrow("can't run reduce on an empty array");
  });
});
