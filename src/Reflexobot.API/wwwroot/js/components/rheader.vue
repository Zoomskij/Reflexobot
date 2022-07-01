<template>
    <div class="header">
        <el-row>
            <el-col :span="24">
                <div class="grid-content bg-purple-light" style="display:flex; justify-content: flex-end; align-items:center">
                    <el-button type="primary">Войти</el-button>
                </div>
            </el-col>
        </el-row>
    </div>
</template>

<script>
    export default {
        name: 'rheader',
        data() {
            return {
    
            }
        },
        computed: {

        },
        methods: {
            
        },
        mounted() {
            
        }
    }
</script>

<style>
    .el-row {
        margin-bottom: 20px;
        &:last-child

    {
        margin-bottom: 0;
    }

    }

    .el-col {
        border-radius: 4px;
    }

    .bg-purple-dark {
        background: #99a9bf;
    }

    .bg-purple {
        background: #d3dce6;
    }

    .bg-purple-light {
        background: #e5e9f2;
    }

    .grid-content {
        border-radius: 4px;
        min-height: 36px;
    }

    .row-bg {
        padding: 10px 0;
        background-color: #f9fafc;
    }
</style>