﻿/*global define, console */

define('bcms.datepicker', ['jquery', 'bcms'], function ($, bcms) {
    'use strict';

    var datepicker = {},
        links = {
            calendarImageUrl: '/file/bcms-root/Content/Styles/images/icn-calendar.png'
        },
        globalization = {};

    // Assign objects to module.
    datepicker.links = links;
    datepicker.globalization = globalization;
    
    datepicker.init = function () {
        console.log('Initializing bcms.datepicker module');

        $.fn.initializeDatepicker = function (options) {
            $(this).datepicker({
                showOn: 'button',
                buttonImage: links.calendarImageUrl,
                buttonImageOnly: true
            });
        };
    };

    bcms.registerInit(datepicker.init);

    return datepicker;
});
