import { renderErrorMessage, renderError } from 'Scripts/Shared/errors';
import 'jstree';
import { Node, State, Attr } from '../../../Shared/TreeTemplates/tree-models';
//import 'styles/libs/dist/themes/default/style.min.css';
//import 'libs/jstree/dist/themes/default/style.min.css';

export class EnumDirector{

    /**
     * 
     * @param {JQuery<HTMLElement>} enumsButton 
     * @param {Query<HTMLElement>} enumsInput 
     */
    constructor(enumsButton, enumsInput, enumsName, enumsContainer){
        this.EnumsButton = enumsButton;
        this.EnumsInput = enumsInput;
        this.EnumsName = enumsName;
        this.EnumsContainer = enumsContainer;
    }

    async Init(){
       let data = await this.GetAllEnums();
       if(!data || !data.Values){
           return;
       }

       let nodes = this.ConvertToNodes(data.Values);
       await this.TreeBuild(nodes);

       console.info(data);
    }

    async TreeBuild(nodes){
        let container = this.EnumsContainer;

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

    ConvertToNodes(data){
        let nodes = [];

        for (let index = 0; index < data.length; index++) {
            let item = data[index];
            
            let text = item.Name;
            let id = item.Guid;
            let state = new State(true, false, false);

            nodes.push(new Node(id, '#', text, null, state));
            for (let i = 0; i < item.Values.length; i++) {
                let elem = item.Values[i];
                let text = elem;

                let id = item.Guid + i;
                let parent = item.Guid;
                let state = new State(true, false, false);
                nodes.push(new Node(id, parent, text, null, state));
            }
        }

        return nodes;
    }

    async GetAllEnums(){
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '../Redactor/GetAllEnums',
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

    InitEvents(){
        let DIRECTOR = this;

        this.EnumsButton.on('click', async e  => {
            let name = DIRECTOR.EnumsName.val();
            if(!name){
                return;
            }

            let inputVals = DIRECTOR.EnumsInput.val();

            inputVals = inputVals.replace(/\s/g, '').split(',');

            if(inputVals.length < 2){
                renderErrorMessage('Необходимо ввести хотя бы 2 значения перечислений');
                return;
            }

            try{
                await DIRECTOR.InsertNewEnum(inputVals, name);
                document.location.href = '../Redactor/Index';
            } catch(e){
              
            }
           
        });
    }


    async InsertNewEnum(enums, name){
        let data = {
            name: name,
            values:  enums,
        }

        data = JSON.stringify(data);

        return new Promise((resolve, reject) => {
            $.ajax({
                url: '../Redactor/InsertEnums',
                type: 'post',
                contentType: 'application/json; utf-8',
                data: data,
                cache: false,
                error: e => {
                    renderError(e);
                    reject();
                },
                success: response => {
                    console.info(response);
                    resolve();
                }
            });
          });

    }
}