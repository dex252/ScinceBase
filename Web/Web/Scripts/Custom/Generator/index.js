import 'shared/common';
import 'jquery-mask-plugin';

import { SetMask} from './validation';

const COUNT_OF_CLASSES = $('#count-of-classes');
const COUNT_OF_PROPERTIES = $('#count-of-properties');
const OK_BUTTON = '#generator-button-ok';
const INPUT_FORM = $('#input-groups');

$(() => {
    SetMask(COUNT_OF_CLASSES, "99");
    SetMask(COUNT_OF_PROPERTIES, "999");

    $(document).on('click', OK_BUTTON, async (e) => {
        let valid = INPUT_FORM.valid();

        if (!valid) {
            return;
        }
    });

});