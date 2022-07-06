<template>
    <div class="block">
        <div style="display:flex; justify-content:space-between">
            <span><b>Сценарии</b></span>
            <el-button type="primary" size="small" @click="newScenarioVisible = true">Создать новый сценарий</el-button>
        </div>

        <el-tree :data="scenarios"
                 :prpps="treeProps"
                 empty-text="Сценарии загружаются..."
                 show-checkbox
                 node-key="id"
                 default-expand-all
                 :expand-on-click-node="false"
                 style="padding-top:10px">
            <span class="custom-tree-node" slot-scope="{ node, scenarios }">
                <span class="sticky">{{ convertLongLabel(node.label) }}</span>
                <span class="tree-buttons">

                    <el-button type="text"
                               size="mini"
                               @click="() => append(node)">
                        Добавить
                    </el-button>
                    <el-button type="text"
                               size="mini"
                               @click="() => edit(node)">
                        Редактировать
                    </el-button>
                    <el-button type="text"
                               size="mini"
                               @click="() => remove(node, scenarios)">
                        Удалить
                    </el-button>
                </span>
            </span>
        </el-tree>

        <el-dialog title="Создать новый сценарий" :visible.sync="newScenarioVisible">
            <el-input placeholder="/command" v-model="command"></el-input>

            <span slot="footer" class="dialog-footer">
                <el-button @click="newScenarioVisible = false">Отменить</el-button>
                <el-button type="primary" @click="createScenario()">Создать</el-button>
            </span>
        </el-dialog>


        <el-dialog title="Редактирование сценария" :visible.sync="dialogFormVisible">
            <span>Текст</span>
            <el-input type="textarea"
                      :autosize="{ minRows: 2, maxRows: 4}"
                      placeholder="Сценарий"
                      v-model="selectedNodeText">
            </el-input>
            <br />
            <span>Команда</span>
            <el-input type="text" v-model="selectedNodeCommand"></el-input>
            <br />
            <span>Тип. 1 - вопрос. 2 - ответ. 3 - результат.</span>
            <el-input type="text" v-model="selectedNodeType"></el-input>
            <span slot="footer" class="dialog-footer">
                <el-button @click="dialogFormVisible = false">Отменить</el-button>
                <el-button type="primary" @click="save()">Сохранить</el-button>
            </span>
        </el-dialog>
    </div>
</template>

<script>
    export default {
        name: 'scenario',
        data() {
            return {
                scenarios: [],
                treeProps: {
                    id: 'guid',
                    //children: 'children',
                    //label: 'item.text'
                },
                dialogFormVisible: false,
                newScenarioVisible: false,
                selectedNode: {},
                selectedNodeText: '',
                selectedNodeCommand: '',
                selectedNodeType: 0,
                command: ''
            }
        },
        computed: {

        },
        methods: {
            getScenarios: function () {
                var self = this;
                this.$axios.get('api/scenario')
                    .then(function (response) {
                        self.scenarios = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            convertLongLabel(text) {
                if (text.length < 150) {
                    return text;
                }
                return text.substring(0, 150) + "...";
            },
            createScenario() {
                this.newScenarioVisible = true;
            },
            //TREE METHODS 
            append(node) {
                var self = this;

                let parrentId = node.data.guid;
                const newChild = { guid: 15, label: 'testtest', children: [], };
                if (!node.children) {
                    this.$set(node, 'children', []);
                }
                node.children.push(newChild);

                var scenarioAddDto = {
                    parrentGuid: parrentId,
                    text: 'Текст для редактирования'
                }
                this.$axios.post('api/scenario', scenarioAddDto).then(function (response) {
                    console.log(response);
                    self.getScenarios();
                }).catch(function (error) {
                    console.log(error);
                });
            },

            edit(node) {
                this.selectedNode = node;
                this.selectedNodeText = node.data.text;
                this.selectedNodeCommand = node.data.command;
                this.selectedNodeType = node.data.type;
                this.dialogFormVisible = true;
            },

            createScenario() {
                var self = this;
                var scenarioAddDto = {
                    text: '',
                    command: this.command,
                }
                this.$axios.post('api/scenario', scenarioAddDto).then(function (response) {
                    console.log(response);
                    self.newScenarioVisible = false;
                    self.getScenarios();
                }).catch(function (error) {
                    console.log(error);
                });
            },

            save() {
                var self = this;
                var scenarioAddDto = {
                    guid: this.selectedNode.data.guid,
                    text: this.selectedNodeText,
                    command: this.selectedNodeCommand,
                    type: this.selectedNodeType
                }

                this.$axios.put('api/scenario', scenarioAddDto).then(function (response) {
                    console.log(response);
                    self.getScenarios();
                    self.dialogFormVisible = false;
                }).catch(function (error) {
                    console.log(error);
                });
            },

            remove(node, data) {
                var self = this;
                let guid = node.data.guid;
                this.$axios.delete('api/scenario/' + guid).then(function (response) {
                    console.log(response);
                    self.getScenarios();
                }).catch(function (error) {
                    console.log(error);
                });
            },
        },
        mounted() {
            this.getScenarios();
        }
    }
</script>

<style>

    span.tree-buttons {
        display: block;
    }

    span:not(.sticky) { 
    }

    span.sticky {
    }

    .custom-tree-node {
        flex: 1;
        display: flex;
        align-items: center;
        justify-content: space-between;
        font-size: 14px;
        padding-right: 8px;
        width: 500px;
    }
</style>