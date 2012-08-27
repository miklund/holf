/**
 * Higher order list functions
 *
 * @module holf
 */
var holf = holf || {};

/**
 * List functions that help you get round for-loops
 * @namespace holf
 * @class list
 */
holf.list = {

    /**
    * Map each element of array with callback
    *
    * @method map
    * @param  {function} callback Transform each element
    * @param  {array}    source   Array of elements to be transformed
    * @return {array}    New array with each element transformed
    */
    map: function (callback, source) {
        "use strict";
        var result = [],
            i = 0,
            sourceMax = 0;

        for (i = 0, sourceMax = source.length; i < sourceMax; i += 1) {
            result[i] = callback(source[i]);
        }

        return result;
    },

    /**
     * Aggregate the elements of an array
     *
     * @method fold
     * @param  {function} callback Aggregation function
     * @param  {object}   init     Initial value of the computation
     * @param  {array}    source   Array of elements to be aggregated
     * @return {object}   The accumulated value
     */
    fold: function (callback, init, source) {
        "use strict";
        var result = init,
            i = 0,
            sourceMax = 0;

        for (i = 0, sourceMax = source.length; i < sourceMax; i += 1) {
            result = callback(result, source[i]);
        }

        return result;
    },

    /**
     * Partitions elements of an array
     *
     * @method partition
     * @param  {function} callback Partition function
     * @param  {array}    source   Array of elements to become partitions
     * @return {object}   An array with the partitions
     */
    partition: function (callback, source) {
        "use strict";
        var result = [],
            i = 0,
            j = 0,
            sourceMax = 0,
            resultMax = 0,
            index = -1;

        for (i = 0, sourceMax = source.length; i < sourceMax; i += 1) {
            index = callback(source[i]);

            // sane check of index
            if (typeof index !== "number" || index < 0) {
                throw "callback should return a positive number";
            }

            resultMax = result.length;
            if (index > resultMax - 1) {
                // expand the result
                for (j = resultMax; j <= index; j += 1) {
                    result.push([]);
                }
            }

            // push item to bucket
            result[index].push(source[i]);
        }

        return result;
    },

    /**
     * Reduce an array to a single item
     *
     * @method reduce
     * @param  {function} callback Aggregator function, callback(acc, item)
     * @param  {array}    source   Array of items to be reduced
     * @return {object}   The object the array has been reduced to
     */
    reduce: function (callback, source) {
        "use strict";
        var result = source[0],
            i = 0,
            sourceMax = 0;

        if (source[0] === undefined) {
            throw "can't run reduce on an empty array";
        }

        for (i = 1, sourceMax = source.length; i < sourceMax; i += 1) {
            result = callback(result, source[i]);
        }

        return result;
    },

    /**
     * Collect many arrays into one array
     *
     * @method collect
     * @param  {function} callback Produces an array for each item
     * @param  {array}    arr      Input array to iterate
     * @return {array}    The result is one big array
     */
    collect : function (callback, source) {
        "use strict";
        var result = [],
            inner = [],
            i = 0,
            j = 0,
            sourceMax = 0,
            innerMax = 0;

        // iterate over arr
        for (i = 0, sourceMax = source.length; i < sourceMax; i += 1) {
            inner = callback(source[i]);

            // check that inner is array
            if (Object.prototype.toString.call(inner) !== '[object Array]') {
                throw "callback must produce an array";
            }

            // iterate over inner
            for (j = 0, innerMax = inner.length; j < innerMax; j += 1) {
                result.push(inner[j]);
            }
        }

        return result;
    },

    /**
     * Scan produces an array of computations
     *
     * @method scan
     * @param  {function} callback callback(acc, item) returns a calculation
     * @param  {object}   init     Initial value of accumulator
     * @param  {array}    source   Input array to calculate on
     * @return {array}    An array of computations made with callback
     */
    scan : function (callback, init, source) {
        "use strict";
        var result = [],
            acc = init,
            i = 0,
            sourceMax = 0;

        // iterate over source array
        for (i = 0, sourceMax = source.length; i < sourceMax; i += 1) {
            acc = callback(acc, source[i]);
            result.push(acc);
        }

        return result;
    }
};

// extend the array object with holf functions
(function () {
    "use strict";
    if (typeof Array.prototype.map !== "function") {
        Array.prototype.map = function (callback) {
            return holf.list.map(callback, this);
        };
    }

    if (typeof Array.prototype.fold !== "function") {
        Array.prototype.fold = function (callback, init) {
            return holf.list.fold(callback, init, this);
        };
    }

    if (typeof Array.prototype.partition !== "function") {
        Array.prototype.partition = function (callback) {
            return holf.list.partition(callback, this);
        };
    }

    // This will never be applied since reduce alreay exists
    // on Array. This declaration is here just due to completeness.
    if (typeof Array.prototype.reduce !== "function") {
        Array.prototype.reduce = function (callback) {
            return holf.list.reduce(callback, this);
        };
    }

    if (typeof Array.prototype.collect !== "function") {
        Array.prototype.collect = function (callback) {
            return holf.list.collect(callback, this);
        };
    }

    if (typeof Array.prototype.scan !== "function") {
        Array.prototype.scan = function (callback, init) {
            return holf.list.scan(callback, init, this);
        };
    }
}());
