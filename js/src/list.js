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
    * Map each element of list with callback
    *
    * @method map
    * @param  {enumerable} list     List of elements to be transformed
    * @param  {function}   callback Transform each element
    * @return {enumerable} New list with each element transformed
    */
    map: function (list, callback) {
        "use strict";
        var result = [],
            i = 0,
            max = 0;

        for (i = 0, max = list.length; i < max; i += 1) {
            result[i] = callback(list[i]);
        }

        return result;
    }
};

// extend the array object with holf functions
(function () {
    "use strict";
    if (typeof Array.prototype.map !== "function") {
        Array.prototype.map = function (callback) {
            return holf.list.map(this, callback);
        };
    }
}());
