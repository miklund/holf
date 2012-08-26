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
