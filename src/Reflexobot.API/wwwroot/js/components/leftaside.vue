<template>
    <div class="left-aside">
        <el-menu class="el-menu-vertical-demo">
            <router-link :to="menuItem.url" tag="div" v-for="menuItem in menuItems">
                <el-menu-item>
                    <i :class="menuItem.icon"></i>
                    <span style="font-size:18px; padding-left:10px">{{menuItem.name}}</span>
                </el-menu-item>
            </router-link>
        </el-menu>
    </div>
</template>

<script>
    export default {
        name: 'leftaside',
        data() {
            return {
                menuItems: [
                    { name: 'Цели', icon: 'el-icon-finished', url: '/goals' },
                    { name: 'Курсы', icon: 'el-icon-headset', url: '/courses' },
                    { name: 'Сценарии', icon: 'el-icon-connection', url: '/scenario' },
                    { name: 'Персонажи', icon: 'el-icon-user', url: '/teachers' },
                    { name: 'Достижения', icon: 'el-icon-medal-1', url: '/achievments' },
                ],
            }
        },
        computed: {

        },
        methods: {
    
        },
        created() {

        },
        mounted() {
            
        }
    }
</script>

<style>
    .left-aside {
        position: fixed;
        top: 0;
        bottom: 0;
        width: 100%;
        max-width: 256px;
        margin-top: 100px;
        display: flex;
        flex-direction: column;
        overflow-y: auto;
        background-color: #fff;
        z-index: 1000;
    }

    .menu-item {
        font-size: 24px;
        padding-top: 18px;
    }
</style>