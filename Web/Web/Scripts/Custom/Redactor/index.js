import 'shared/common';
import 'jquery-mask-plugin';

import { renderErrorMessage, renderError } from 'Scripts/Shared/errors';
import {EnumDirector} from './Enums/enum-director';

const ENUMS_BOTTON = $('.enums-button-input');
const ENUMS_NAME = $('.enums-name');
const ENUMS_INPUT = $('.enums-input');
const ENUMS_CONTAINER = $('#enums-tree-container');

const ENUMS_DIRECTOR = new EnumDirector(ENUMS_BOTTON, ENUMS_INPUT, ENUMS_NAME, ENUMS_CONTAINER);

$(async () => {
    await ENUMS_DIRECTOR.Init();
    ENUMS_DIRECTOR.InitEvents();
});