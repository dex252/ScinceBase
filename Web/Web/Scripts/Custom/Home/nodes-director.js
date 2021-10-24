import { renderErrorMessage, renderError } from 'Scripts/Shared/errors';
import 'jstree';
import { Node, State, Attr } from '../../Shared/TreeTemplates/tree-models';

export class NodesDirector{
    constructor(container){
        this.Container = container;
    }

    async Init(){
        let data = await this.GetAllNodes();
        if(!data){
            return;
        }
 
        let nodes = this.ConvertToNodes(data);
        await this.TreeBuild(nodes);
 
        console.info(data);
    }

    ConvertToNodes(data){
        let nodes = [];

        for (let index = 0; index < data.length; index++) {
            let item = data[index];
            
            let text = item.Name;
            let id = item.Guid;
            let state = new State(index==0, false, false);

            nodes.push(new Node(id, '#', text, null, state));
            for (let i = 0; i < item.Attribures.length; i++) {
                let elem = item.Attribures[i];
                let text = elem.Name;

                let id = item.Guid + elem.Guid;
                let parent = item.Guid;
                let state = new State(false, false, false);
                nodes.push(new Node(id, parent, text, null, state));

                //TODO:переписать на стратегию по-человечески
                //Добавим нормальные значения
                this.AddNormalValues(elem, id, nodes, i);

            }
        }

        return nodes;
    }

    AddNormalValues(item, parent, nodes, index){
        if(item.IntervalSettings){
            this.AddNormalValuesInterval(item, parent, nodes, index);
            this.AddPeriod(item, parent, nodes, index);
            this.AddPeriodsInterval(item, parent, nodes, index);
            return;
        }

        if(item.IntegerSettings){
            this.AddNormalValuesInteger(item, parent, nodes, index);
            this.AddPeriod(item, parent, nodes, index);
            this.AddPeriodsInteger(item, parent, nodes, index);
            return;
        }

        if(item.BinarySettings){
            this.AddNormalValuesBinary(item, parent, nodes, index);
            this.AddPeriod(item, parent, nodes, index);
            this.AddPeriodsBinary(item, parent, nodes, index);
            return;
        }

        if(item.EnumSettings){
            this.AddNormalValuesEnums(item, parent, nodes, index);
            this.AddPeriod(item, parent, nodes, index);
            this.AddPeriodsEnums(item, parent, nodes, index);
            return;
        }
    }

    AddPeriod(item, parent, nodes, index){
        let text = 'Периоды';
        let id = parent + index + 'periods';
        let state = new State(true, false, false);

        nodes.push(new Node(id, parent, text, null, state));
    }

    AddPeriodsEnums(item, parent, nodes, index){

        for (let i = 0; i < item.Periods.length; i++) {
            let elem = item.Periods[i];
            let text = String(Number(elem.PeriodNumber));

            let parent_id = parent + index + 'periods';
            let id = parent_id + elem.PeriodNumber;

            let state = new State(true, false, false);
            nodes.push(new Node(id, parent_id, text, null, state));

            for (let j = 0; j < elem.EnumValues.length; j++) {
                let value_text = elem.EnumValues[j];
              
                let parent_value_id = id;
                let value_id = id + 'value' + j;
    
                let state = new State(true, false, false);
                nodes.push(new Node(value_id, parent_value_id, value_text, null, state));
    
    
            }

        }
        
    }

    AddPeriodsBinary(item, parent, nodes, index){

        for (let i = 0; i < item.Periods.length; i++) {
            let elem = item.Periods[i];
            let text = String(Number(elem.PeriodNumber));

            let parent_id = parent + index + 'periods';
            let id = parent_id + elem.PeriodNumber;

            let state = new State(true, false, false);
            nodes.push(new Node(id, parent_id, text, null, state));

            let value = 'Value: ' + String(Boolean(elem.BinaryValue));
            let value_id = id + 'value';
            nodes.push(new Node(value_id, id, value, null, state));

        }
        
    }

    AddPeriodsInteger(item, parent, nodes, index){

        for (let i = 0; i < item.Periods.length; i++) {
            let elem = item.Periods[i];
            let text = String(Number(elem.PeriodNumber));

            let parent_id = parent + index + 'periods';
            let id = parent_id + elem.PeriodNumber;

            let state = new State(true, false, false);
            nodes.push(new Node(id, parent_id, text, null, state));

            let value = 'Value: ' + elem.NumberValue;
            let value_id = id + 'value';
            nodes.push(new Node(value_id, id, value, null, state));

        }
        
    }

    AddPeriodsInterval(item, parent, nodes, index){

        for (let i = 0; i < item.Periods.length; i++) {
            let elem = item.Periods[i];
            let text = String(Number(elem.PeriodNumber));

            let parent_id = parent + index + 'periods';
            let id = parent_id + elem.PeriodNumber;

            let state = new State(true, false, false);
            nodes.push(new Node(id, parent_id, text, null, state));

            let min_text = 'Min: ' + elem.IntervalValue.Min;
            let min_id = id + 'min';
            nodes.push(new Node(min_id, id, min_text, null, state));
    
            let max_text = 'Max: ' + elem.IntervalValue.Max;
            let max_id = id + 'max';
            nodes.push(new Node(max_id, id, max_text, null, state));

        }
        
    }

    AddNormalValuesEnums(item, parent, nodes, index){
        let text_name = 'Название: ' + item.EnumSettings.Name;
        let id_name = parent + index + 'name';

        let text = 'Нормальные значения перечислений';
        let id = parent + index;
        let state = new State(true, false, false);

        nodes.push(new Node(id_name, parent, text_name, null, state));
        nodes.push(new Node(id, parent, text, null, state));

        for (let i = 0; i < item.EnumSettings.NormalValues.length; i++) {
            let elem = item.EnumSettings.NormalValues[i];
            let text = elem;

            let parent_id = id;
            let value_id = parent_id + i;

            let state = new State(true, false, false);
            nodes.push(new Node(value_id, parent_id, text, null, state));
        }
    }

    AddNormalValuesBinary(item, parent, nodes, index){
        let text = 'Нормальные бинарные значения';
        let id = parent + index;
        let state = new State(true, false, false);

        nodes.push(new Node(id, parent, text, null, state));

        let elem = item.BinarySettings.NormalValue;
        let value_text = String(Boolean(elem));

        let parent_id = id;
        let value_id = parent_id + index + 'normal';
        nodes.push(new Node(value_id, parent_id, value_text, null, state));
        
    }

    AddNormalValuesInteger(item, parent, nodes, index){
        let text = 'Нормальные целочисленные значения';
        let id = parent + index;
        let state = new State(true, false, false);

        nodes.push(new Node(id, parent, text, null, state));

        for (let i = 0; i < item.IntegerSettings.NormalValue.length; i++) {
            let elem = item.IntegerSettings.NormalValue[i];
            let text = Number(elem);

            let parent_id = id;
            let value_id = parent_id + i;

            let state = new State(true, false, false);
            nodes.push(new Node(value_id, parent_id, text, null, state));
        }
    }

    AddNormalValuesInterval(item, parent, nodes, index){
        let text = 'Нормальные интервальные значения';
        let id = parent + index;
        let state = new State(true, false, false);

        nodes.push(new Node(id, parent, text, null, state));

        let min_text = 'Min: ' + item.IntervalSettings.NormalInterval.Min;
        let min_id = id + 'min';
        nodes.push(new Node(min_id, id, min_text, null, state));

        let max_text = 'Max: ' + item.IntervalSettings.NormalInterval.Max;
        let max_id = id + 'max';
        nodes.push(new Node(max_id, id, max_text, null, state));
    }


    async TreeBuild(nodes){
        let container = this.Container;

        container.jstree('destroy');

        return new Promise((resolve, reject) => {
            container.jstree({
                'core':{
                    'data': nodes,
                    'multiple': true,
                    'themes':{
                        'name': 'default',
                        'url': false,
                        'icons': false
                    },
                    'plugins' : [ 'search', 'state' ]
                }
            });

            resolve(true);
          });
    }

    async GetAllNodes(){
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '../Home/Get',
                type: 'get',
                contentType: 'application/json; utf-8',
                cache: false,
                error: e => {
                    renderError(e);
                    reject();
                },
                success: response => {
                    console.info(response);
                    resolve(response);
                }
            });
          });
    }
}