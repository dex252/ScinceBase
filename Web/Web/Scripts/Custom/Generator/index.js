import 'shared/common';
import 'jquery-mask-plugin';

import {renderErrorMessage, renderError} from 'Scripts/Shared/errors';

import { SetMask, SetDigitMask, ValidDigitDifference, DigitCompare} from './validation';

const COUNT_OF_CLASSES = $('#count-of-classes');
const COUNT_OF_PROPERTIES = $('#count-of-properties');
const INTEGER_MIN = $('#awailable-property-min');
const INTEGER_MAX = $('#awailable-property-max');
const NORMAL_MIN = $('#normal-property-min');
const NORMAL_MAX = $('#normal-property-max');
const COUNT_OF_PERIODS = $('#count-of-periods');
const PERIODS_MIN = $('#normal-periods-min');
const PERIODS_MAX = $('#normal-periods-max');

const OK_BUTTON = '#generator-button-ok';
const INPUT_FORM = $('#input-groups');

$(() => {
    SetMask(COUNT_OF_CLASSES, "99");
    SetMask(COUNT_OF_PROPERTIES, "999");
    SetMask(INTEGER_MIN, "99");
    SetMask(INTEGER_MAX, "99");
    SetMask(NORMAL_MIN, "99");
    SetMask(NORMAL_MAX, "99");
    SetDigitMask(COUNT_OF_PERIODS, "Z", /[1-5]/);
    SetMask(PERIODS_MIN, "99");
    SetMask(PERIODS_MAX, "99");

    $(document).on('click', OK_BUTTON, async (e) => {
        let valid = INPUT_FORM.valid();

        if (!valid) {
            return;
        }

        if(!ValidDigitDifference(INTEGER_MIN, INTEGER_MAX)){
            renderErrorMessage("Минимальное значение признака превышает максимальное");
            return;
        }

        if(!ValidDigitDifference(NORMAL_MIN, INTEGER_MAX)){
            renderErrorMessage("Минимальное нормальное значение признака превышает или равно максимальному значение признака");
            return;
        }

        if(!ValidDigitDifference(NORMAL_MAX, INTEGER_MAX)){
            renderErrorMessage("Максимальное нормальное значение признака превышает или равно максимальному значение признака");
            return;
        }

        if(!ValidDigitDifference(NORMAL_MIN, NORMAL_MAX)){
            renderErrorMessage("Минимальное нормальное значение признака превышает максимальное");
            return;
        }

        if(!ValidDigitDifference(PERIODS_MIN, PERIODS_MAX)){
            renderErrorMessage("Минимальное значение периода превышает максимальное");
            return;
        }

        Generate(INPUT_FORM);
    });

    $(document).on('change', INTEGER_MIN, () => DigitCompare(INTEGER_MIN));
    $(document).on('change', INTEGER_MAX, () => DigitCompare(INTEGER_MAX));
    $(document).on('change', NORMAL_MIN, () => DigitCompare(NORMAL_MIN));
    $(document).on('change', NORMAL_MAX, () => DigitCompare(NORMAL_MAX));
    $(document).on('change', PERIODS_MIN, () => DigitCompare(PERIODS_MIN));
    $(document).on('change', PERIODS_MAX, () => DigitCompare(PERIODS_MAX));

    function Generate(form){
        let data = {
            countOfClasses: Number(COUNT_OF_CLASSES.val()),
            countOfProperties:  Number(COUNT_OF_PROPERTIES.val()),
            awailablePropertyMin:  Number(INTEGER_MIN.val()),
            awailablePropertyMax:  Number(INTEGER_MAX.val()),
            normalPropertyMin:  Number(NORMAL_MIN.val()),
            normalPropertyMax:  Number(NORMAL_MAX.val()),
            countOfPeriods:  Number(COUNT_OF_PERIODS.val()),
            normalPeriodsMin:  Number(PERIODS_MIN.val()),
            normalPeriodsMax:  Number(PERIODS_MAX.val()),
        }

        data = JSON.stringify(data);

        $.ajax({
            url: '../Generator/SetGeneratorParametres',
            type: 'post',
            contentType: 'application/json; utf-8',
            data: data,
            cache: false,
            error: e => renderError(e),
            success: response => {
                console.info(response);
            }
        });
    }
});