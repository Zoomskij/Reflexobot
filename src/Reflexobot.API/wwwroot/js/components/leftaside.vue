<template>
    <div class="left-aside">
        <div v-for="menuItem in menuItems" class="menu-item">
            <el-link :href="menuItem.url" type="primary" style="font-size:18px; padding-left:10px">{{menuItem.name}}</el-link>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'leftaside',
        data() {
            return {
                menuItems: [
                    { name: 'Цели', url: '#' },
                    { name: 'Курсы', url: '#/courses' },
                    { name: 'Сценарии', url: '#/scenario' },
                    { name: 'Персонажи', url: '#/teachers' },
                    { name: 'Достижения', url: '#/achievments' },
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