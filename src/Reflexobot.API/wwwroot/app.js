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


import Scenario from "~/js/components/scenario.vue";
import RHeader from "~/js/components/rheader.vue";

Vue.component("scenario", Scenario);
Vue.component("rheader", RHeader);

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
            telegramUserId: ''
        },
        mounted() {
            //TODO: rewrite + authorize
            this.telegramUserId = window.location.search.substring(5);
            this.getBotInfo();
            this.getMyTeacher();
            this.getCourses();
            this.getAchievments();
            this.getChats();
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
            }
        },
    })
}
startOnLoad();