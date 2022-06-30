////import Vue from 'vue'
////import VueRouter from 'vue-router'
////import ElementUI from 'element-ui'
////import locale from 'element-ui/lib/locale'
////import ruLocale from 'element-ui/lib/locale/lang/ru-RU'

////import axios from 'axios';

////Vue.use(VueRouter);
////locale.use(ruLocale);
////Vue.use(ElementUI, { ruLocale });
////Vue.prototype.$axios = axios;

function startOnLoad() {
    var app = new Vue({
        el: '#app',
        data: {
            courses: [],
            lessons: [],
            achievments: [],
            chats: [],
            botInfo: {},
            myTeacher: {},
            telegramUserId: '',
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
        },
        mounted() {
            //TODO: rewrite + authorize
            this.telegramUserId = window.location.search.substring(5);
            this.getBotInfo();
            this.getMyTeacher();
            this.getCourses();
            this.getAchievments();
            this.getChats();
            this.getScenarios();
        },
        computed: {
            // геттер вычисляемого значения
            achievmentsRand: function () {
                // `this` указывает на экземпляр vm
                return this.achievments.slice(0, Math.floor(Math.random() * 9))
            },
            percentageRand: function () {
                return Math.floor(Math.random() * 100);
            },
        },
        methods: {
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
                axios.post('/scenario', scenarioAddDto).then(function (response) {
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
                axios.post('/scenario', scenarioAddDto).then(function (response) {
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

                axios.put('/scenario', scenarioAddDto).then(function (response) {
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
                axios.delete('/scenario/' + guid).then(function (response) {
                    console.log(response);
                    self.getScenarios();
                }).catch(function (error) {
                    console.log(error);
                });
            },

            //Get courses
            getCourses: function () {
                var self = this;
                axios.get('/courses')
                    .then(function (response) {
                        self.courses = response.data;
                        if (self.courses !== null && self.courses) {
                            self.getLessons('8B7AFF9B-E2D0-494F-85BD-D29F96C6AB65');
                        }
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            //Get Lessons by course Guid
            getLessons: function (courseGuid) {
                var self = this;
                axios.get('/lessons/' + courseGuid)
                    .then(function (response) {
                        self.lessons = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            //Get Achievments
            getAchievments: function () {
                var self = this;
                axios.get('/achievment')
                    .then(function (response) {
                        self.achievments = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            //Get bot info
            getBotInfo: function () {
                var self = this;
                axios.get('/telegram/status')
                    .then(function (response) {
                        self.botInfo = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            //Get my current teacher
            getMyTeacher: function (telegramUserId) {
                var self = this;
                if (this.telegramUserId !== null && this.telegramUserId !== '') {
                    axios.get('/teachers/' + self.telegramUserId)
                        .then(function (response) {
                            self.myTeacher = response.data;
                        })
                        .catch(function (error) {
                            console.log(error);
                        });
                }
            },
            getChats: function () {
                var self = this;
                axios.get('/courses/chats')
                    .then(function (response) {
                        self.chats = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            getScenarios: function () {
                var self = this;
                axios.get('/scenario')
                    .then(function (response) {
                        self.scenarios = response.data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
        },
    })
}
startOnLoad();