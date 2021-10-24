import 'shared/common';
import {NodesDirector} from './nodes-director';

const NODES_CONTAINER = $('#nodes-container');
const NODES_DIRECTOR = new NodesDirector(NODES_CONTAINER);

$(async () => {
    await NODES_DIRECTOR.Init();
});