require './list'
require 'test/unit'

class Map < Test::Unit::TestCase

  def test_should_double_each_number_in_list
    # arrange
    data = [1, 2, 3]

    # act
    result = Holf::List.map(data) {|i| i * 2}

    # assert
    assert_equal([2, 4, 6], result.to_a)
  end

  def test_should_return_floats_in_result
    # arrange
    data = [1, 3, 5]

    # act
    result = Holf::List.map(data) {|i| i / 2.0}

    # assert
    assert_equal([0.5, 1.5, 2.5], result.to_a)
  end

  def test_should_return_not_primitive_type
    # arrange
    data = [1, 3, 5]

    # act
    result = Holf::List.map(data) {|i| "#{i}"}

    # assert
    assert_equal(["1", "3", "5"], result.to_a)
  end

  def test_should_return_empty_list_for_empty_input
    # arrange
    data = []

    # act
    result = Holf::List.map(data) {|i| i}

    # assert
    assert(result.to_a.empty?)
  end
end

class Fold < Test::Unit::TestCase
  
  def test_should_sum_all_numbers
    # arrange
    data = [1, 2, 3]

    # act
    result = Holf::List.fold(data, 0) {|agg, i| i + agg}

    # assert
    assert_equal(6, result)
  end

  def test_should_add_all_numbers_into_a_string
    # arrange
    data = [1, 2, 3]

    # act
    result = Holf::List.fold(data, '') {|agg, i| agg + i.to_s}

    # assert
    assert_equal("123", result)
  end
end

class Partition < Test::Unit::TestCase

  def test_should_partition_odds_and_evens
    # arrange
    data = [1, 2, 3, 4, 5]

    # act
    result = Holf::List.partition(data) {|i| i % 2}

    # assert
    assert_equal([2, 4], result[0])
    assert_equal([1, 3, 5], result[1])
  end

  def test_cannot_partition_when_yield_does_not_produce_integer
    # arrange
    data = [1]

    # assert
    assert_raise RuntimeError do

      # act
      result = Holf::List.partition(data) {|i| "not integer"} # fail
    end
  end

  def test_cannot_partition_when_yield_does_not_produce_positive_integer
    # arrange
    data = [1]

    # assert
    assert_raise RuntimeError do
      
      # act
      result = Holf::List.partition(data) {|i| -1} # fail
    end
  end
end

class Reduce < Test::Unit::TestCase

  def test_should_calculate_maximum_number
    # arrange
    data = [5, 2, 6, 1, 8, 0, 3]

    # act
    result = Holf::List.reduce(data) {|a, b| a > b ? a : b}

    # assert
    assert_equal(8, result)
  end

  def test_cannot_reduce_empty_list
    # arrange
    data = []

    # assert
    assert_raise RuntimeError do

      # act
      result = Holf::List.reduce(data) {|a, b| a}
    end
  end
end

class Expand < Test::Unit::TestCase

  def test_should_elaborate_on_taxes
    # arrange
    data = 10000.0

    # act
    result = Holf::List.expand(data) {|prev| prev - (prev * 0.3)}

    # assert
    assert_equal([10000.0, 7000.0, 4900.0], result.take(3))
  end

  def test_should_finish_enumeration_with_nil
    # arrange
    data = 10000.0

    # act
    result = Holf::List.expand(data) {|prev| prev > 20000 ? nil : (prev * 1.3)}

    # assert
    assert_equal([10000.0, 13000.0, 16900.0, 21970.0], result.to_a)
  end
end

class Collect < Test::Unit::TestCase

  def test_should_merge_yields_into_one_list
    # arrange
    data = [[1, 2], [3, 4, 5], [6]]

    # act
    result = Holf::List.collect(data) {|i| i}

    # assert
    assert_equal([1, 2, 3, 4, 5, 6], result.to_a)
  end
end

class Scan < Test::Unit::TestCase

  def test_should_produce_a_series_of_computations
    # arrange
    departures = Holf::List.expand(0) {|i| i == 20 ? 10 : 20}
    firstDeparture = 609 # minutes from 00:00

    # act
    timetable = Holf::List.scan(departures, firstDeparture) {|current, n| current + n}

    # map
    result = Holf::List.map(timetable.take(5)) {|time| "#{time / 60}:%02d" % (time % 60)}      

    # assert
    assert_equal(["10:09", "10:29", "10:39", "10:59", "11:09"], result.to_a)
  end
end
