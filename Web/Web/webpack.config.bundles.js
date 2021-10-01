'use strict';

let entryConfig = {
    'homeBundle': [
        'babel-polyfill',
        './Scripts/Custom/Home/index.js'
    ],
    'generatorBundle': [
        'babel-polyfill',
        './Scripts/Custom/Generator/index.js'
    ]
};

module.exports = entryConfig;