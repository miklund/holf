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
    * @param  {array}    arr     Array of elements to be transformed
    * @return {array}    New array with each element transformed
    */
    map: function (callback, arr) {
        "use strict";
        var result = [],
            i = 0,
            max = 0;

        for (i = 0, max = arr.length; i < max; i += 1) {
            result[i] = callback(arr[i]);
        }

        return result;
    },

    /**
     * Aggregate the elements of an array
     *
     * @method fold
     * @param  {function} callback Aggregation function
     * @param  {object}   seed      Seed value of the aggregation
     * @param  {array}    arr       Array of elements to be aggregated
     * @return {object}   The aggregated value
     */
    fold: function (callback, seed, arr) {
        "use strict";
        var result = seed,
            i = 0,
            max = 0;

        for (i = 0, max = arr.length; i < max; i += 1) {
            result = callback(result, arr[i]);
        }

        return result;
    },

    /**
     * Partitions elements of an array
     *
     * @method partition
     * @param  {function} callback Partition function
     * @param  {array}    arr      Array of elements to be partitions
     * @return {object}   An array with the partitions
     */
    partition: function (callback, arr) {
        "use strict";
        var result = [],
            i = 0,
            j = 0,
            sourceMax = 0,
            resultMax = 0,
            index = -1;
        for (i = 0, sourceMax = arr.length; i < sourceMax; i += 1) {
            index = callback(arr[i]);

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
            result[index].push(arr[i]);
        }

        return result;
    },

    /**
     * Reduce an array to a single item
     *
     * @method reduce
     * @param  {function} callback Aggregator function, callback(agg, item)
     * @param  {array}    arr      Array of items to be reduced
     * @return {object}   The object the array has been reduced to
     */
    reduce: function (callback, arr) {
        "use strict";
        var result = arr[0],
            i = 0,
            sourceMax = 0;

        if (arr[0] === undefined) {
            throw "can't run reduce on an empty array";
        }

        for (i = 1, sourceMax = arr.length; i < sourceMax; i += 1) {
            result = callback(result, arr[i]);
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
        Array.prototype.fold = function (seed, callback) {
            return holf.list.fold(callback, seed, this);
        };
    }

    if (typeof Array.prototype.partition !== "function") {
        Array.prototype.partition = function (callback) {
            return holf.list.partition(callback, this);
        };
    }
}());
