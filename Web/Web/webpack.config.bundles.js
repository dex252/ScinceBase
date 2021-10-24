'use strict';

let entryConfig = {
    'homeBundle': [
        'babel-polyfill',
        './Scripts/Custom/Home/index.js'
    ],
    'generatorBundle': [
        'babel-polyfill',
        './Scripts/Custom/Generator/index.js'
    ],
    'redactorBundle': [
        'babel-polyfill',
        './Scripts/Custom/Redactor/index.js'
    ]
};

module.exports = entryConfig;