# gobal extension methods to help with debugging
class Enumerator
  # a nice printout of enumerated values up to 10
  def to_s
    result = "["
    self.take(11).each_with_index do |item, index|
      if (index == 10)  then
        result += "..."
        break
      end
      result += ", " if index > 0
      result += "#{item.to_s}"
    end
    return result + "]"
  end
  
  # use to_s in irb
  def inspect
    to_s
  end
end

# higher order list functions
module Holf

  class List
    # map a list of values to a new list
    # result is a lazy evaluated enumeration
    def self.map(list)
      enumerator = Enumerator.new do |e|
        list.each do |item|
          e.yield(yield(item))
        end
      end
    end

    # fold aggregates all values to an aggregated value
    def self.fold(list, init)
      result = init
      list.each do |item|
        result = yield(result, item)
      end
      return result
    end

    # partition splits a list into buckets
    def self.partition(list)
      result = []
      list.each do |item|
        index = yield(item)

        raise "Yield produced non positive integer" unless index.kind_of? Integer and index > -1

        if index > result.length - 1 then

          # expand result
          for i in result.length..index do
            result << []
          end
        end

        # push item to bucket
        result[index] << item
      end

      return result
    end

    # reduce a list to one item
    def self.reduce(list)
      result = nil, first = true
      
      list.each do |current|
        result = current and first = false if first
        result = yield(result, current)
      end

      # avoid missleading result
      raise "Cannot reduce an empty list" if first == true

      return result
    end

    # expand a value
    def self.expand(init)
      current = init
      Enumerator.new do |e|
        e.yield current
        while current = yield(current)
          e.yield current 
        end
      end
    end

    # collect
    def self.collect(list)
      Enumerator.new do |e|
        list.each do |sub|
          yield(sub).each do |item|
            e.yield yield(item)
          end
        end
      end
    end

    # enumerate each step in a computation
    def self.scan(list, state)
      result = state
      Enumerator.new do |e|
        list.each do |item|
          e.yield result = yield(result, item)
        end
      end
    end
  end
end

# extend array class with functions
class Array
  # WARNING: map is already a function on Array
  def h_map(&block)
    Holf::List::map(self, &block)
  end

  def fold(init, &block)
    Holf::List.fold(self, init, &block)
  end

  def partition(&block)
    Holf::List.partition(self, &block)
  end

  def reduce(&block)
    Holf::List.reduce(self, &block)
  end

  # WARNING: collect is already a function on Array
  def h_collect(&block)
    Holf::List.collect(self, &block)
  end

  def scan(state, &block)
    Holf::List.scan(self, state, &block)
  end
end

