# gobal extension methods to help with debugging
class Enumerator
  # a nice printout of enumerated values up to 10
  def to_s
    result = "["
    i = 0
    self.each do |item|
      if (i += 1) > 10 then
        result += "..."
        break
      end
      result += ", " if i > 1
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
  end
end
