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
end

