export class Node{
    constructor(id, parent, text, a_attr, state, data){
        this.id = id;
        this.parent = parent;
        this.text = text;
        this.a_attr = a_attr;
        this.state = state;
        this.data = data;
    }
}

export class Attr{
    constructor(Class){
        this.class = Class;
    }
}

export class State{
    constructor(opened, selected, disabled){
        this.opened = opened;
        this.selected = selected;
        this.disabled = disabled;
    }
}