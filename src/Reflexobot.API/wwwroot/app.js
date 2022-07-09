import Vue from 'vue'
import VueRouter from 'vue-router'
import ElementUI from 'element-ui'
import locale from 'element-ui/lib/locale'
import ruLocale from 'element-ui/lib/locale/lang/ru-RU'

import axios from 'axios';

Vue.prototype.$axios = axios;

import Index from "~/js/components/index.vue";
import Scenario from "~/js/components/scenario.vue";
import Courses from "~/js/components/courses.vue";
import Achievments from "~/js/components/achievments.vue";
import Teachers from "~/js/components/teachers.vue";
import RHeader from "~/js/components/rheader.vue";
import LeftAside from "~/js/components/leftaside.vue";


Vue.use(VueRouter);
locale.use(ruLocale);
Vue.use(ElementUI, { ruLocale });

Vue.component("scenario", Scenario);
Vue.component("rheader", RHeader);
Vue.component("left-aside", LeftAside);
Vue.component("achievments", Achievments);
Vue.component("teachers", Teachers);
Vue.component("index", Index);

function startOnLoad() {
    var router = new VueRouter({
        routes: [
            /* { path: '/', caseSensitive: false, component: Scenario },*/
            { path: '/', caseSensitive: false, component: Index },
            { path: '/teachers', caseSensitive: false, component: Teachers },
            { path: '/courses', caseSensitive: false, component: Courses },
            { path: '/scenario', caseSensitive: false, component: Scenario },
            { path: '/achievments', caseSensitive: false, component: Achievments },
        ]
    });
    Vue.config.devtools = true;

    var vv = new Vue({
        el: "#vue-router",
        router,
        data: {

        },
        methods: {
            back() {
                this.$router.go(-1);
            },

            isCurrentRoute(name) {
                return this.$route.name === name;
            },
        },
    });


    var app = new Vue({
        el: '#app',
        router,
        data: {
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
            this.getChats();
        },
        computed: {

        },
        methods: {
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